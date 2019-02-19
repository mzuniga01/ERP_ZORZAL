using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace ERP_ZORZAL.Controllers
{
    public class EntradaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Entrada/
        public ActionResult Index()
        {
            
            var tbentrada = db.tbEntrada.Include(t => t.tbBodega).Include(t => t.tbEstadoMovimiento).Include(t => t.tbProveedor).Include(t => t.tbTipoEntrada);
            return View(tbentrada.ToList());
        }

        // GET: /Entrada/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
                return HttpNotFound();
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
        //para imprimir entra por id
       
        [HttpPost]
        public ActionResult ExportReportGeneral(string TipoEntrada, DateTime? FechaElaboracion)
        {
            ReportDocument rd = new ReportDocument();
            if (TipoEntrada == "1")
            {
                var pathr = "ImprimirEntradaCompra.rpt";
                rd.Load(Path.Combine(Server.MapPath("~/Reports"), pathr));
            }
            else if (TipoEntrada == "2")
            {
                var pathr = "ImprimirEntradaDevolucion.rpt";
                rd.Load(Path.Combine(Server.MapPath("~/Reports"), pathr));
            }
            else if (TipoEntrada == "3")
            {
                var pathr = "ImprimirEntradaTraslado.rpt";
                rd.Load(Path.Combine(Server.MapPath("~/Reports"), pathr));
            }
            //rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ImprimirEntradaCompra.rpt"));
            //var todo = (from r in db.tbEntrada
            //            where r.tent_Id == 1
            //            select new
            //            {
            //                ent_Id = r.ent_Id
            //            }).ToList();

            var tbEntrada2 = db.SDP_tbentradaImprimir_Select(Convert.ToInt32(TipoEntrada), FechaElaboracion).ToList();
            rd.SetDataSource(tbEntrada2);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            //Response.Write("<script>");
            //Response.Write("window.open('~/ImprimirEntradaCompra.rpt.pdf', '_newtab');");
            //Response.Write("</script>");

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                string fileName = "Entrada_List.pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                return File(stream, "application/pdf");


                //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //stream.Seek(0, SeekOrigin.Begin);
                //return File(stream, "application/pdf", "Entrada_List.pdf");


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
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                throw;
            }
        }

        //para imprimir entra por id
        public ActionResult ExportReport(int? id)
        {
            ReportDocument rd = new ReportDocument();
             rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ImprimirEntradaPorId.rpt"));
            
            var tbEntrada = db.tbEntrada.ToList();
            var tbProveedor = db.tbProveedor.ToList();
            var tbBodega = db.tbBodega.ToList();
            var tbEstadoMovimiento = db.tbEstadoMovimiento.ToList();
            var tbTipoEntrada = db.tbTipoEntrada.ToList();
            var tbTipoDevolucion = db.tbTipoDevolucion.ToList();
            var tbentradadetalle = db.tbEntradaDetalle.ToList();
            var tbProducto = db.tbProducto.ToList();
            var tbUnidadMedida = db.tbUnidadMedida.ToList();
            var todo = (from r in tbEntrada
                        join pro in tbProveedor on r.prov_Id equals pro.prov_Id
                        join bod in tbBodega on r.bod_Id equals bod.bod_Id
                        join esta in tbEstadoMovimiento on r.estm_Id equals esta.estm_Id
                        join tent in tbTipoEntrada on r.tent_Id equals tent.tent_Id
                        //join tdel in tbTipoDevolucion on r.ent_RazonDevolucion equals tdel.tdev_Id
                        join deta in tbentradadetalle on r.ent_Id equals deta.ent_Id
                        join prod in tbProducto on deta.prod_Codigo equals prod.prod_Codigo
                        join unid in tbUnidadMedida on prod.uni_Id equals unid.uni_Id
                        where r.ent_Id == id
                        select new
                        {
                            ent_Id = r.ent_Id,
                            ent_NumeroFormato = r.ent_NumeroFormato,
                            bod_Nombre = bod.bod_Nombre,
                            ent_FechaElaboracion = r.ent_FechaElaboracion,
                            prov_Nombre = pro.prov_Nombre,
                            estm_Descripcion = esta.estm_Descripcion,
                            tent_Descripcion = tent.tent_Descripcion,
                            //ent_FacturaCompra =r.ent_FacturaCompra,
                            //ent_FechaCompra = r.ent_FechaCompra,
                            //tdev_descripcion = tdel.tdev_Descripcion,
                            //fact_Id = r.fact_Id,
                            //ent_BodegaDestino = r.ent_BodegaDestino,
                            entd_Cantidad = deta.entd_Cantidad,
                            prod_Codigo = prod.prod_Codigo,
                            prod_Descripcion = prod.prod_Descripcion,
                            uni_Descripcion = unid.uni_Descripcion
                        }).ToList();
            rd.SetDataSource(todo);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                string fileName = "Entrada_List.pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                //window.open('pagpdf.php', '_blank');
                return File(stream, "application/pdf");
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
            //ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
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
            Session["CrearDetalleEntrada"] =null;
            return View();
        }
        
        // GET: /Entrada/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            
            if (tbEntrada == null)
            {
                return HttpNotFound();
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
            Session["CrearDetalleEntrada"] =null;
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
        public JsonResult Guardardetalleentrada(tbEntradaDetalle EntradaDetalle)
        {
            List<tbEntradaDetalle> sessionentradadetalle = new List<tbEntradaDetalle>();
            var list = (List<tbEntradaDetalle>)Session["CrearDetalleEntrada"];
            if (list == null)
            {
                sessionentradadetalle.Add(EntradaDetalle);
                Session["CrearDetalleEntrada"] = sessionentradadetalle;
            }
            else
            {
                list.Add(EntradaDetalle);
                Session["CrearDetalleEntrada"] = list;
            }
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

                list = db.UDP_Inv_tbEntradaDetalle_Update(Editardetalle.entd_Id
                                                            , Editardetalle.ent_Id
                                                           , Editardetalle.prod_Codigo
                                                           , Editardetalle.entd_Cantidad
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
            //return Json(Msj, JsonRequestBehavior.AllowGet);
            return Json("Edit/" + maestro);

        }
        //para borrar registros en la tabla temporal
        [HttpPost]
        public JsonResult Eliminardetalleentrada(tbEntradaDetalle eliminardetalle)
        {
            var list = (List<tbEntradaDetalle>)Session["_CrearDetalleEntrada"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.ent_Id == eliminardetalle.ent_Id);
                list.Remove(itemToRemove);
                Session["_CrearDetalleEntrada"] = list;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
        // POST: /Entrada/Create
        //Para inserte en la master y la detalle

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ent_FechaElaboracion,bod_Id,estm_Id,prov_Id,ent_FacturaCompra,ent_FechaCompra,fact_Id,ent_RazonDevolucion,ent_BodegaDestino,tent_Id")] tbEntrada tbEntrada)
        {
            //var tipoEntrada = tbEntrada.tent_Id;
            //if (tipoEntrada == 1)
            //{
            //    Campos de Traslado
            //    if (tbEntrada.ent_BodegaDestino != null)
            //    {
            //        tbEntrada.ent_BodegaDestino = 9999;
            //    }

            //    campos de devolucion
            //    if (tbEntrada.ent_RazonDevolucion == null)
            //    {
            //        tbEntrada.ent_RazonDevolucion = "##";
            //    }
            //    if (tbEntrada.fact_Id == null)
            //    {
            //        tbEntrada.fact_Id = 9999;
            //    }
            //}
            //if (tipoEntrada == 2)
            //{
            //    campos de Compra
            //    if (tbEntrada.fact_Id == null)
            //    {
            //        tbEntrada.fact_Id = 9999;
            //    }
            //    if (tbEntrada.ent_FacturaCompra == null)
            //    {
            //        tbEntrada.ent_FacturaCompra = "99";
            //    }
            //    if (tbEntrada.ent_FechaCompra == null)
            //    {
            //        tbEntrada.ent_FechaCompra = Convert.ToDateTime("####-##-##");
            //    }
            //    campos de Traslado
            //    if (tbEntrada.ent_BodegaDestino != null)
            //    {
            //        tbEntrada.ent_BodegaDestino = 9999;
            //    }
            //}
            //if (tipoEntrada == 3)
            //{
            //    campos de Compra
            //    if (tbEntrada.fact_Id == null)
            //    {
            //        tbEntrada.fact_Id = 9999;
            //    }
            //    if (tbEntrada.ent_FacturaCompra == null)
            //    {
            //        tbEntrada.ent_FacturaCompra = "####";
            //    }
            //    if (tbEntrada.ent_FechaCompra == null)
            //    {
            //        tbEntrada.ent_FechaCompra = Convert.ToDateTime("####-##-##");
            //    }
            //    campos de devolucion
            //    if (tbEntrada.ent_RazonDevolucion == null)
            //    {
            //        tbEntrada.ent_RazonDevolucion = "##";
            //    }
            //    if (tbEntrada.fact_Id == null)
            //    {
            //        tbEntrada.fact_Id = 9999;
            //    }
            //}
            IEnumerable<object> ENTRADA = null;
            IEnumerable<object> DETALLE = null;
            tbEntrada.estm_Id = Helpers.EntradaEmitida;
            var idMaster = 0;
            var MensajeError = "";
            var MsjError = "";
            
            var listaDetalle = (List<tbEntradaDetalle>)Session["CrearDetalleEntrada"];

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
                                                                tbEntrada.tent_Id);
                            foreach (UDP_Inv_tbEntrada_Insert_Result Entrada in ENTRADA)
                                idMaster = Convert.ToInt32(Entrada.MensajeError);

                            if (MsjError == "-")
                            {
                                ModelState.AddModelError("", "No se guardo el registro");
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
                                            DETALLE = db.UDP_Inv_tbEntradaDetalle_Insert(idMaster
                                                                                        , entd.prod_Codigo
                                                                                        , entd.entd_Cantidad);
                                            foreach (UDP_Inv_tbEntradaDetalle_Insert_Result B_detalle in DETALLE)

                                            //if (MensajeError == "-1")
                                            {
                                                ModelState.AddModelError("", "No se Guardo el Registro");
                                                //return View(tbEntrada);
                                                //}
                                                //else
                                                //{
                                                //    _Tran.Complete();
                                                //    return RedirectToAction("Index");
                                            }
                                        }
                                    }
                                }
                                {
                                    _Tran.Complete();
                                    //return RedirectToAction("Index");
                                }

                            }

                        }
                        catch (Exception Ex)
                        {
                            Ex.Message.ToString();
                            //ModelState.AddModelError("", "No se Guardo el Registro");
                            //return View(tbBodega);
                            MsjError = "-1";
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
            
            return View(tbEntrada);
        }

        // POST: /Entrada/Edit/5
        //Para q edite la master y el detalle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "ent_Id,ent_NumeroFormato,ent_FechaElaboracion,bod_Id,prov_Id,ent_FacturaCompra,ent_FechaCompra,fact_Id,ent_RazonDevolucion,ent_BodegaDestino,tent_Id,ent_usuarioCrea,ent_FechaCrea,ent_UsuarioModifica,ent_FechaModifica")] tbEntrada tbEntrada)
        {
            
            IEnumerable<object> ENTRADA = null;
            IEnumerable<object> DETALLE = null;
            var idMaster = 0;
            var MensajeError = "";
            var MsjError = "";
            var listaDetalle = (List<tbEntradaDetalle>)Session["CrearDetalleEntrada"];
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
                                                                        tbEntrada.ent_FechaCrea);
                        foreach (UDP_Inv_tbEntrada_Update_Result Entrada in ENTRADA)
                            idMaster = Convert.ToInt32(Entrada.MensajeError);

                        if (MsjError == "-")
                        {
                            ModelState.AddModelError("", "No se guardo el registro");
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

                                        DETALLE = db.UDP_Inv_tbEntradaDetalle_Insert(idMaster
                                                                                    ,entd.prod_Codigo
                                                                                    ,entd.entd_Cantidad);
                                        foreach (UDP_Inv_tbEntradaDetalle_Insert_Result B_detalle in DETALLE)

                                        //if (MensajeError == "-1")
                                        {
                                            ModelState.AddModelError("", "No se Guardo el Registro");
                                            //return View(tbEntrada);
                                            //}
                                            //else
                                            //{
                                            //    _Tran.Complete();
                                            //    return RedirectToAction("Index");
                                        }
                                    }
                                }
                            }
                            {
                                _Tran.Complete();
                                //return RedirectToAction("Index");
                            }

                        }

                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        //ModelState.AddModelError("", "No se Guardo el Registro");
                        //return View(tbBodega);
                        MsjError = "-1";
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






        public ActionResult EstadoInactivar(int? id)
        {

            try
            {
                tbEntrada obj = db.tbEntrada.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbEntrada_Update_Estado(id, Helpers.EntradaInactivada);
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
                list = db.UDP_Inv_tbEntrada_Update_Estado(id, Helpers.EntradaEmitida);
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
                list = db.UDP_Inv_tbEntrada_Update_Anular(cambiaAnular.ent_Id, Helpers.EntradaAnulada, cambiaAnular.entd_RazonAnulada);
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
                list = db.UDP_Inv_tbEntrada_Update_Estado(id, Helpers.EntradaAplicada);
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
