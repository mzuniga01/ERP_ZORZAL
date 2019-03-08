using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Transactions;
using System.IO;
using System.Web.Services;
using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Dataset;
using ERP_GMEDINA.Reports;
using CrystalDecisions.CrystalReports.Engine;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;

namespace ERP_ZORZAL.Controllers
{
    public class EntradaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /Entrada/
        [SessionManager("Entrada/Index")]
        public ActionResult Index()
        {
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion");
            var tbentrada = db.tbEntrada.Include(t => t.tbBodega).Include(t => t.tbEstadoMovimiento).Include(t => t.tbProveedor).Include(t => t.tbTipoEntrada);
            return View(tbentrada.ToList());
        }

        // GET: /Entrada/Details/5
        [SessionManager("Entrada/Details")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbEntrada tbEntrada = await db.tbEntrada.FindAsync(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbEntrada.ent_UsuarioCrea).usu_NombreUsuario;
            var UsuarioModfica = tbEntrada.ent_UsuarioModifica;
            if (UsuarioModfica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModfica).usu_NombreUsuario;
            };
            if (tbEntrada == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            //vista parcial de entrada detalle
            ViewBag.ent_Id = new SelectList(db.tbEntrada, "ent_Id", "ent_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            return View(tbEntrada);
        }

        // GET: /Entrada/Create
        [HttpPost]
        public JsonResult GetRTNProveedor(int codigoProveedor)
        {
            try { ViewBag.smserror = TempData["smserror"].ToString(); } catch { }
            var list = db.SPGetRTNproveedor(codigoProveedor).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public RedirectResult RedirectToAspx()
        {
            return Redirect("~/Reports/ReporteTipoEntrada.aspx");
        }
        [HttpPost]
        public ActionResult ExportReportGeneral(tbEntrada tbentrada, string ent_FechaElaboracion)
        {
            var TipoEntrada = tbentrada.tent_Id;
            var FechaElaboracion = Convert.ToDateTime(ent_FechaElaboracion);
            var estado = tbentrada.estm_Id;
            var bodega = tbentrada.bod_Id;

            ReporteTipoEntrada EntradaRV = new ReporteTipoEntrada();
            ReportDocument rd = new ReportDocument();
            Reportes EntradaDST = new Reportes();
            var EntradaTableAdapter = new UDV_Inv_TipoEntradaTableAdapter();

            //ERP_GMEDINA.Reports.CachedImprimirEntradaGeneral entradas = new ERP_GMEDINA.Reports.CachedImprimirEntradaGeneral();
            //ERP_GMEDINA.Dataset.EntradaTableAdapters.UDV_Inv_TipoEntradaTableAdapter entradasGe = new ERP_GMEDINA.Dataset.EntradaTableAdapters.UDV_Inv_TipoEntradaTableAdapter();
            //ERP_GMEDINA.Dataset.Entrada.UDV_Inv_TipoEntradaDataTable entradadatatable = new ERP_GMEDINA.Dataset.Entrada.UDV_Inv_TipoEntradaDataTable();
            //entradasGe.Fill(entradadatatable, TipoEntrada, estado, bodega, FechaElaboracion);
            //var resuldataset = entradasGe.GetData(TipoEntrada, estado, bodega, FechaElaboracion).ToList();
            //ReportDocument rd = new ReportDocument();
            //var pathr = "ImprimirEntradaGeneral.rpt";
            //rd.Load(Path.Combine(Server.MapPath("~/Reports"), pathr));
            //rd.SetDataSource(resuldataset);
            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();
            try
            {
                EntradaTableAdapter.Fill(EntradaDST.UDV_Inv_TipoEntrada, TipoEntrada, estado, bodega, FechaElaboracion);

                EntradaRV.SetDataSource(EntradaDST);
                //SalidaRV.PrintToPrinter(1, false, 0, 0);
                Stream stream = EntradaRV.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                //var Url = "../ReportViewer/ReporteEntrada.aspx";
                //return RedirectToAction(Url);
                //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //stream.Seek(0, SeekOrigin.Begin);
                EntradaRV.Close();
                EntradaRV.Dispose();
                string fileName = "Entrada_List.pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                return File(stream, "application/pdf");

            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                throw;
            }
        }


        //para imprimir entra por id
        public ActionResult ExportReport(int? id)
        {
            var ent_id = Convert.ToInt32(id);
            //ERP_GMEDINA.Dataset.EntradaTableAdapters.UDV_Inv_EntradaPorIdTableAdapter entradasID = new ERP_GMEDINA.Dataset.EntradaTableAdapters.UDV_Inv_EntradaPorIdTableAdapter();
            //ERP_GMEDINA.Dataset.Entrada.UDV_Inv_EntradaPorIdDataTable entradadatatableId = new ERP_GMEDINA.Dataset.Entrada.UDV_Inv_EntradaPorIdDataTable();
            //entradasID.Fill(entradadatatableId, ent_id);
            //var resuldatasetId = entradasID.GetData(ent_id).ToList();
            ImprimirEntradaPorId EntradaRVId = new ImprimirEntradaPorId();
            ReportDocument rdId = new ReportDocument();
            Reportes EntradaDSTId = new Reportes();
            var EntradaTableAdapterId = new UDV_Inv_EntradaPorIdTableAdapter();
            //ReportDocument rd = new ReportDocument();
            //var url = "ImprimirEntrada.aspx";
            //rd.Load(Path.Combine(Server.MapPath("~/ReportViewer"), url));
            //rd.SetDataSource(resuldatasetId);
            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();
            try
            {

                EntradaTableAdapterId.Fill(EntradaDSTId.UDV_Inv_EntradaPorId, ent_id);

                EntradaRVId.SetDataSource(EntradaDSTId);
                //SalidaRV.PrintToPrinter(1, false, 0, 0);
                Stream stream = EntradaRVId.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                //var Url = "../ReportViewer/ReporteEntrada.aspx";
                //return RedirectToAction(Url);
                //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //stream.Seek(0, SeekOrigin.Begin);
                EntradaRVId.Close();
                EntradaRVId.Dispose();
                string fileName = "ImprimirEntrada.pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                return File(stream, "application/pdf");
                //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //stream.Seek(0, SeekOrigin.Begin);
                //string fileName = "ImprimirEntrada.pdf";
                //Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                //window.open('pagpdf.php', '_blank');
                //return View(from Entrada in db.tbEntrada.Take(10)
                //            select Entrada);
                //var Url = "../ReportViewer/ImprimirEntrada.aspx";
                //return RedirectToAction(Url);
                //{
                //    PageMargins margins = rd.PrintOptions.PageMargins;/* TODO ERROR: Skipped SkippedTokensTrivia */
                //    margins.bottomMargin = 200;/* TODO ERROR: Skipped SkippedTokensTrivia */
                //    margins.leftMargin = 200;/* TODO ERROR: Skipped SkippedTokensTrivia */
                //    margins.rightMargin = 50;/* TODO ERROR: Skipped SkippedTokensTrivia */
                //    margins.topMargin = 100;/* TODO ERROR: Skipped SkippedTokensTrivia */
                //    rd.PrintOptions.ApplyPageMargins(margins);/* TODO ERROR: Skipped SkippedTokensTrivia */
                //}


                //rd.PrintToPrinter(1, false, 0, 0);
                //return View();
            }
            catch
            {
                throw;
            }
        }

        [SessionManager("Entrada/Create")]
        public ActionResult Create()
        {
            try { ViewBag.smserror = TempData["smserror"].ToString(); } catch { }
            string UserName = "";
            int idUser = 0;
            GeneralFunctions Login = new GeneralFunctions();
            List<tbUsuario> User = Login.getUserInformation();
            tbBodega tbBod = new tbBodega();
            foreach (tbUsuario Usuario in User)
            {
                UserName = Usuario.usu_Nombres + " " + Usuario.usu_Apellidos;
                idUser = Convert.ToInt32(Usuario.emp_Id);
            }

            ViewBag.bod_Id = new SelectList(db.tbBodega.Where(x => x.bod_ResponsableBodega == idUser).ToList(), "bod_Id", "bod_Nombre");
            ViewBag.tdev_Id = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion");
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion");
            ViewBag.ent_RazonDevolucion = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion");

            //vista parcial de entrada detalle
            ViewBag.ent_Id = new SelectList(db.tbEntrada, "ent_Id", "ent_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.bod_Idd = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.Producto = db.SDP_Inv_tbProducto_Select().ToList();
            Session["_CrearDetalleEntrada"] =null;
            return View();
        }

        // GET: /Entrada/Edit/5
        [SessionManager("Entrada/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            
            if (tbEntrada == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            //*****PuntoEmisionDetalle
            string cas = "uni_IdList";
            System.Web.HttpContext.Current.Items[cas] = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            

            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.bod_Id);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbEntrada.tbEstadoMovimiento.estm_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbEntrada.tbProveedor.prov_Id);
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tbTipoEntrada.tent_Id);
            ViewBag.tdev_Id = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion", tbEntrada.ent_RazonDevolucion);
            ViewBag.bbod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.ent_BodegaDestino);

            //vista parcial de entrada detalle
            ViewBag.ent_Id = new SelectList(db.tbEntrada, "ent_Id", "ent_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.bod_Idd = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");

            ViewBag.Producto = db.SDP_Inv_tbProducto_Select().ToList();
            Session["_CrearDetalleEntrada"] =null;
            return View(tbEntrada);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //para añadir datos temporales a la tabla
        [HttpPost]
        public JsonResult Guardardetalleentrada(tbEntradaDetalle EntradaDetalle, string codigoproducto)
        {
            var datos = "";
            decimal cantvieja = 0;
            decimal cantnueva = 0;
            codigoproducto = EntradaDetalle.prod_Codigo;
            decimal data_cantidad = EntradaDetalle.entd_Cantidad;
            List<tbEntradaDetalle> sessionentradadetalle = new List<tbEntradaDetalle>();
            var list = (List<tbEntradaDetalle>)Session["_CrearDetalleEntrada"];
            if (list == null)
            {
                sessionentradadetalle.Add(EntradaDetalle);
                Session["_CrearDetalleEntrada"] = sessionentradadetalle;
            }
            else
            {
                foreach (var t in list)
                    if (t.prod_Codigo == codigoproducto)
                    {
                        datos = codigoproducto;
                        foreach (var viejo in list)
                            if (viejo.prod_Codigo == EntradaDetalle.prod_Codigo)
                                cantvieja = viejo.entd_Cantidad;
                        cantnueva = cantvieja + data_cantidad;
                        t.entd_Cantidad = cantnueva;
                        return Json(datos, JsonRequestBehavior.AllowGet);
                    }
                list.Add(EntradaDetalle);
                Session["_CrearDetalleEntrada"] = list;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            //{
            //    list.Add(EntradaDetalle);
            //    Session["CrearDetalleEntrada"] = list;
            //}
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        //para actualizar detalle de la entrada del modal
        [HttpPost]
        public ActionResult GetDetalleEntrada(int? entd_Id)
        {
            var list = db.SDP_tbentradadetalle_Select(entd_Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //para q actualize la tabla
        [HttpPost]
        public JsonResult UpdateEntradaDetalle(tbEntradaDetalle Editardetalle)
        {
            string Msj = "";
            var maestro = Editardetalle.ent_Id;
            try
            {
                IEnumerable<object> list = null;
                tbEntradaDetalle entr = db.tbEntradaDetalle.Find(Editardetalle.entd_Id);
                list = db.UDP_Inv_tbEntradaDetalle_Update(Editardetalle.entd_Id
                                                            , Editardetalle.ent_Id
                                                           , Editardetalle.prod_Codigo
                                                           , Editardetalle.entd_Cantidad
                                                           ,Editardetalle.entd_UsuarioCrea,
                                                           entr.entd_FechaCrea,
                                                           Function.GetUser(), Function.DatetimeNow()
                                    );
                 foreach (UDP_Inv_tbEntradaDetalle_Update_Result detalle in list)
                    Msj = detalle.MensajeError;

                if (Msj.StartsWith("-1"))
                {
                    Msj = "-1";
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Guardo el registro");
                Msj = "-1";
            }
           
            return Json("Edit/" + maestro);

        }
        //para borrar registros en la tabla temporal
        [HttpPost]
        public JsonResult Eliminardetalleentrada(tbEntradaDetalle EntradaDetalle)
        {
            var list = (List<tbEntradaDetalle>)Session["_CrearDetalleEntrada"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.prod_Codigo == EntradaDetalle.prod_Codigo);
                list.Remove(itemToRemove);
                Session["_CrearDetalleEntrada"] = list;
                if (list.Count == 0)
                {
                    Session["_CrearDetalleEntrada"] = null;
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Entrada/Create")]
        public ActionResult Create([Bind(Include = "ent_FechaElaboracion,bod_Id,estm_Id,prov_Id,ent_FacturaCompra,ent_FechaCompra,fact_Id,ent_RazonDevolucion,ent_BodegaDestino,tent_Id")] tbEntrada tbEntrada)
        {
            IEnumerable<object> ENTRADA = null;
            IEnumerable<object> DETALLE = null;
            tbEntrada.estm_Id = Helpers.EntradaEmitida;
            string MensajeError = "";
            string MsjError = "";
            
            var listaDetalle = (List<tbEntradaDetalle>)Session["_CrearDetalleEntrada"];

            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.bod_Id);
            ViewBag.tdev_Id = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion", tbEntrada.ent_RazonDevolucion);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbEntrada.prov_Id);
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
            ViewBag.ent_BodegaDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.ent_BodegaDestino);
            ViewBag.Producto = db.SDP_Inv_tbProducto_Select().ToList();
            
            if (ModelState.IsValid)
            {
                if (listaDetalle == null)
                {
                    TempData["smserror"] = " No Puede Ingresar Una Entrada Sin Detalle.";
                    ViewBag.smserror = TempData["smserror"];
                    return View();
                }
                else
                {
                    using (TransactionScope _Tran = new TransactionScope())
                    {
                        try
                        {
                            ENTRADA = db.UDP_Inv_tbEntrada_Insert(
                                                                tbEntrada.ent_FechaElaboracion,
                                                                tbEntrada.bod_Id,
                                                                tbEntrada.estm_Id,
                                                                tbEntrada.prov_Id,
                                                                tbEntrada.ent_FacturaCompra,
                                                                tbEntrada.ent_FechaCompra,
                                                                tbEntrada.fact_Id,
                                                                tbEntrada.ent_RazonDevolucion,
                                                                tbEntrada.ent_BodegaDestino,
                                                                tbEntrada.tent_Id,
                                                                Function.GetUser(), Function.DatetimeNow());
                            foreach (UDP_Inv_tbEntrada_Insert_Result Entrada in ENTRADA)
                                MsjError = Entrada.MensajeError;

                            if (MsjError.StartsWith("-1"))
                            {
                                Function.InsertBitacoraErrores("Entrada/Create", MsjError, "Create");
                                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                return View(tbEntrada);
                            }
                            else if (MsjError.StartsWith("-2"))
                            {
                                Function.InsertBitacoraErrores("Entrada/Create", MsjError, "Create");
                                ModelState.AddModelError("", "El codigo de la FACTURA ya Existe.");
                                return View(tbEntrada);
                            }
                            else
                            {
                                if (listaDetalle != null)
                                {
                                    if (listaDetalle.Count > 0)
                                    {
                                        foreach (tbEntradaDetalle entd in listaDetalle)
                                        {
                                            DETALLE = db.UDP_Inv_tbEntradaDetalle_Insert(Convert.ToInt16(MsjError)
                                                                                        , entd.prod_Codigo
                                                                                        , entd.entd_Cantidad,
                                                                                        Function.GetUser(), Function.DatetimeNow());
                                            foreach (UDP_Inv_tbEntradaDetalle_Insert_Result B_detalle in DETALLE)
                                                MensajeError = B_detalle.MensajeError;
                                            if (MensajeError.StartsWith("-1"))
                                            {
                                                Function.InsertBitacoraErrores("Entrada/Create", MsjError, "Create");
                                                ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                                return View(tbEntrada);
                                            }
                                        }
                                    }
                                }
                                _Tran.Complete();
                            }
                        }
                        catch (Exception Ex)
                        {
                            Function.InsertBitacoraErrores("Entrada/Create", Ex.Message.ToString(), "Create");
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            return View(tbEntrada);
                        }
                        //ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.bod_Id);
                        //ViewBag.tdev_Id = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion", tbEntrada.ent_RazonDevolucion);
                        //ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbEntrada.prov_Id);
                        //ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
                        //ViewBag.ent_BodegaDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.ent_BodegaDestino);
                        //ViewBag.Producto = db.SDP_Inv_tbProducto_Select().ToList();
                    }
                }
                return RedirectToAction("Index");
            }
            //listaDetalle = (List<tbEntradaDetalle>)Session["CrearDetalleEntrada"];
            return View(tbEntrada);
        }

        // POST: /Entrada/Edit/5
        //Para q edite la master y el detalle
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Entrada/Edit")]
        public ActionResult Edit(int? id, [Bind(Include = "ent_Id,ent_NumeroFormato,ent_FechaElaboracion,bod_Id,prov_Id,ent_FacturaCompra,ent_FechaCompra,fact_Id,ent_RazonDevolucion,ent_BodegaDestino,tent_Id,ent_usuarioCrea,ent_FechaCrea,ent_UsuarioModifica,ent_FechaModifica")] tbEntrada tbEntrada)
        {
            
            IEnumerable<object> ENTRADA = null;
            IEnumerable<object> DETALLE = null;
            string MensajeError = "";
            string MsjError = "";
            var listaDetalle = (List<tbEntradaDetalle>)Session["_CrearDetalleEntrada"];
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.bod_Id);
            ViewBag.ent_RazonDevolucion = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion", tbEntrada.ent_RazonDevolucion);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbEntrada.prov_Id);
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
            ViewBag.ent_BodegaDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.ent_BodegaDestino);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.Producto = db.SDP_Inv_tbProducto_Select().ToList();

            if (ModelState.IsValid)
            {
                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {

                        ENTRADA = db.UDP_Inv_tbEntrada_Update(tbEntrada.ent_Id,
                                                                        tbEntrada.ent_NumeroFormato,
                                                                        tbEntrada.ent_FechaElaboracion,
                                                                        tbEntrada.bod_Id,
                                                                        tbEntrada.prov_Id,
                                                                        tbEntrada.ent_FacturaCompra,
                                                                        tbEntrada.ent_FechaCompra,
                                                                        tbEntrada.fact_Id, 
                                                                        tbEntrada.ent_RazonDevolucion,
                                                                        tbEntrada.ent_BodegaDestino,
                                                                        tbEntrada.tent_Id,
                                                                        tbEntrada.ent_UsuarioCrea,
                                                                        tbEntrada.ent_FechaCrea,
                                                                        Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Inv_tbEntrada_Update_Result Entrada in ENTRADA)
                            MsjError = Entrada.MensajeError;

                        if (MsjError.StartsWith("-1"))
                        {
                            Function.InsertBitacoraErrores("Entrada/Edit", MsjError, "Edit");
                            ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                            return View(tbEntrada);
                        }
                        else
                        {
                            if (listaDetalle != null)
                            {
                                if (listaDetalle.Count > 0)
                                {
                                    foreach (tbEntradaDetalle entd in listaDetalle)
                                    {
                                        entd.entd_UsuarioCrea = 1;
                                        entd.entd_FechaCrea = DateTime.Now;

                                        DETALLE = db.UDP_Inv_tbEntradaDetalle_Insert(Convert.ToInt16(MsjError)
                                                                                    , entd.prod_Codigo
                                                                                    , entd.entd_Cantidad,
                                                                                    Function.GetUser(), Function.DatetimeNow());
                                        foreach (UDP_Inv_tbEntradaDetalle_Insert_Result B_detalle in DETALLE)
                                            MensajeError = B_detalle.MensajeError;
                                        if (MensajeError.StartsWith("-1"))
                                        {
                                            Function.InsertBitacoraErrores("Entrada/Edit", MsjError, "Edit");
                                            ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                            return View(tbEntrada);
                                        }
                                    }
                                }
                            }
                            _Tran.Complete();
                        }
                    }
                    catch (Exception Ex)
                    {
                        Function.InsertBitacoraErrores("Entrada/Create", Ex.Message.ToString(), "Create");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbEntrada);
                    }
                }
                return RedirectToAction("Index");
            }
            //ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.bod_Id);
            //ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbEntrada.estm_Id);
            //ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbEntrada.prov_Id);
            //ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
            //ViewBag.ent_BodegaDestino = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbEntrada.ent_BodegaDestino);
            //ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            //ViewBag.ent_RazonDevolucion = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion", tbEntrada.ent_RazonDevolucion);
            //ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            //ViewBag.Producto = db.SDP_Inv_tbProducto_Select().ToList();
            return View(tbEntrada);
        }

        [HttpPost]
        public JsonResult ProductosEnter(string cod_Barras)
        {
            var list = db.SP_tbEntrada_ProductosRepetidos(cod_Barras).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EstadoInactivar(int? id)
        {

            try
            {
                tbEntrada obj = db.tbEntrada.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbEntrada_Update_Estado(id, Helpers.EntradaInactivada, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbEntrada_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + id);
            }


            //return RedirectToAction("Index");
        }
        //para que cambie estado a inactivar
        public ActionResult Estadoactivar(int? id)
        {

            try
            {
                tbEntrada obj = db.tbEntrada.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbEntrada_Update_Estado(id, Helpers.EntradaEmitida, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbEntrada_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + id);
            }


            //return RedirectToAction("Index");
        }

        //para que Anular una entrada
        public ActionResult EstadoAnular(tbEntrada cambiaAnular)
        {
             
            //var maestro = cambiaAnular.ent_Id;
            tbEntrada obj = db.tbEntrada.Find(cambiaAnular.ent_Id);
            try
            {

                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbEntrada_Update_Anular(cambiaAnular.ent_Id, Helpers.EntradaAnulada, cambiaAnular.entd_RazonAnulada, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbEntrada_Update_Anular_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    //return RedirectToAction("Edit/" + cambiaAnular.ent_Id);
                }
                else
                {
                    return RedirectToAction("Edit/" + obj);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + obj);
            }
            return RedirectToAction("Edit/" + obj);
            //return RedirectToAction("Index");
        }
        //para Aplicar una entrada
        public ActionResult EstadoAplicar(int? id)
        {

            try
            {
                tbEntrada obj = db.tbEntrada.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbEntrada_Update_Estado(id, Helpers.EntradaAplicada, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbEntrada_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + id);
            }

        }
    }
}
