using CrystalDecisions.CrystalReports.Engine;
using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Services;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;
using ERP_GMEDINA.Dataset;

namespace ERP_GMEDINA.Controllers
{
    public class SalidaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        private GeneralFunctions Function = new GeneralFunctions();

        // GET: /Salida/
        [SessionManager("Salida/Index")]
        public ActionResult Index()
        {
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion");
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");

            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion");
            var tbsalida = db.tbSalida;
            return View(tbsalida.ToList());
        }

        //[WebMethod]
        //public static object GetProductoList(int bod_Id)
        //{
        //    IEnumerable<object> list = null;
        //    List<tbBodegaDetalle> lista = new List<tbBodegaDetalle>();
        //    //try
        //    //{
        //    //    //string json = JsonConvert.SerializeObject(lista);
        //    //}
        //    //catch (Exception Ex)
        //    //{
        //    //    var msj = Ex.Message.ToString();
        //    //    //< object > list = new { Result = "", msj };
        //    //    //object list = new { "", msj };
        //    //}
        //    object json = new { data = lista };
        //    return json;
        //    //return Json(list, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetProducto(int? bod_Id)
        {
            IEnumerable<object> list = null;
            //list = db.tbBodegaDetalle.Select(p => new
            //{
            //    prod_Codigo = p.prod_Codigo,
            //    bodd_CantidadExistente = p.bodd_CantidadExistente
            //}).ToList();
            try
            {
                list = db.SDP_Inv_tbBodegaDetalle_Select_Producto(bod_Id).ToList();
            }
            catch (Exception Ex)
            {
                var msj = Ex.Message.ToString();
                //< object > list = new { Result = "", msj };
                //object list = new { "", msj };
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public int Usuario()
        {
            int idUser = 0;
            try
            {
                List<tbUsuario> User = Function.getUserInformation();
                foreach (tbUsuario Usuario in User)
                {
                    idUser = Convert.ToInt32(Usuario.emp_Id);

                }
                return idUser;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return 0;
            }
        }

        //public ActionResult GenerarReporte(tbSalida tbSalida, string FechaElaboracion)
        //{
        //    ReportDocument rd = new ReportDocument();
        //    UDV_Inv_Salida_Imprimir_ReporteTableAdapter SalidaTableAdapter = new UDV_Inv_Salida_Imprimir_ReporteTableAdapter();
        //     var SalidaDataTable = new Salida.UDV_Inv_Salida_Imprimir_ReporteDataTable();
        //    IEnumerable<object> SalidaResult = null;
        //    //var vFechaElaboracion = Convert.ToDateTime(FechaElaboracion);
        //    //SalidaTableAdapter.Fill(SalidaDataTable, vFechaElaboracion, tbSalida.tsal_Id, tbSalida.bod_Id, tbSalida.estm_Id);
        //    //Salida = SalidaTableAdapter.GetData(vFechaElaboracion, tbSalida.tsal_Id, tbSalida.bod_Id, tbSalida.estm_Id).ToList();
        //    SalidaTableAdapter.Fill(SalidaDataTable);

        //    SalidaResult = SalidaTableAdapter.GetData().ToList();
        //    rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Salida.rpt"));

        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    //var TipoSalida = tbSalida.tsal_Id;
        //    //var Bodega = tbSalida.bod_Id;
        //    //var Estado = tbSalida.estm_Id;

        //    try
        //    {
        //        rd.SetDataSource(SalidaResult);

        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return File(stream, "application/pdf", "Salida_List.pdf");
        //    }
        //    catch (Exception Ex)
        //    {
        //        Ex.Message.ToString();
        //        throw;
        //    }
        //}
        public RedirectResult RedirectToAspx()
        {
            return Redirect("~/Reports/Salida.aspx");
        }

        //public void ObtenerParametros(tbSalida tbSalida, string FechaElaboracion)
        //{
        //    object jsonlist = new { tbSalida, FechaElaboracion };
        //    //Redirect("~/Reports/Salida.aspx");
        //    return Response.Redirect("~/Reports/Salida.aspx");
        //}

        public ActionResult GenerarReporte(tbSalida tbSalida, string FechaElaboracion)
        {
            var vFechaElaboracion = Convert.ToDateTime(FechaElaboracion);
            Reports.Salida SalidaRV = new Reports.Salida();

            Reportes SalidaDST = new Reportes();

            var SalidaTableAdapter = new UDV_Inv_Salida_Imprimir_ReporteTableAdapter();

            //var SalidaDataTable = new SalidaDS.UDV_Inv_Salida_Imprimir_ReporteDataTable();
            //SalidaTableAdapter.Fill(SalidaDataTable);
            //SalidaTableAdapter.GetData();

            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //var TipoSalida = tbSalida.tsal_Id;
            //var Bodega = tbSalida.bod_Id;
            //var Estado = tbSalida.estm_Id;
            //CrystalDecisions.ReportSource = SalidaRV;
            try
            {
                SalidaTableAdapter.Fill(SalidaDST.UDV_Inv_Salida_Imprimir_Reporte, vFechaElaboracion, tbSalida.tsal_Id, tbSalida.bod_Id, tbSalida.estm_Id);

                SalidaRV.SetDataSource(SalidaDST);
                //SalidaRV.PrintToPrinter(1, false, 0, 0);
                Stream stream = SalidaRV.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                //return File(stream, "application/pdf", "Salida_List.pdf");

                SalidaRV.Close();
                SalidaRV.Dispose();
                //return Redirect("~/Reports/Salida.aspx");
                string fileName = "Salida_List.pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                return File(stream, "application/pdf");
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                throw;
            }
        }

        // GET: /Salida/Details/5
        [SessionManager("Salida/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            if (tbSalida == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbSalida);
        }

        // GET: /Salida/Create
        [SessionManager("Salida/Create")]
        public ActionResult Create()
        {
            try
            {
                Session["SalidaDetalle"] = null;
                int idUser = Usuario();
                var vbod_Id = (from bodega in db.tbBodega where bodega.bod_ResponsableBodega == idUser select new { bodId = bodega.bod_Id, bod_Nombre = bodega.bod_Nombre }).FirstOrDefault();
                ViewBag.BodegaSelec = vbod_Id.bod_Nombre;
                ViewBag.bod_Id = vbod_Id.bodId;
                //ViewBag.bod_Id = new SelectList(db.tbBodega.Where(x => x.bod_ResponsableBodega == idUser).ToList(), "bod_Id", "bod_Nombre");
                //ViewBag.sal_BodDestino = db.SDP_tbBodega_Listado(3).ToList();
                ViewBag.sal_BodDestino = new SelectList(db.tbBodega.Where(x => x.bod_Id != vbod_Id.bodId), "bod_Id", "bod_Nombre");
                ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
                ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion");
                ViewBag.Factura = db.tbFactura.Where(x => x.fact_EsAnulada != Helpers.fact_EsAnulada && x.esfac_Id == Helpers.esfac_Pagada || x.esfac_Id == Helpers.esfac_PagoPendiente).ToList();
                ViewBag.Producto = db.tbBodegaDetalle.Where(x => x.bod_Id == vbod_Id.bodId && x.bodd_CantidadExistente > x.bodd_CantidadMinima).ToList();
                //ViewBag.Box = (from Box in db.tbBox where !db.tbSalidaDetalle.Any(es => (es.box_Codigo == Box.box_Codigo))).ToList();
                ViewBag.Box = db.tbBox.Where(s => !db.tbSalidaDetalle.Where(es => es.box_Codigo == s.box_Codigo).Any()).ToList();
                ViewBag.tdev_Id = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion");
                return View();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return RedirectToAction("Index");
            }
        }

        // POST: /Salida/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Salida/Create")]
        public ActionResult Create([Bind(Include = "bod_Id,fact_Id,fact_Codigo,sal_FechaElaboracion,estm_Id,tsal_Id,tdev_Id, sal_BodDestino, sal_EsAnulada, sal_RazonAnulada")] tbSalida tbSalida)
        {
            int idUser = Usuario();
            var vbod_Id = (from bodega in db.tbBodega where bodega.bod_ResponsableBodega == idUser select new { bodId = bodega.bod_Id, bod_Nombre = bodega.bod_Nombre }).FirstOrDefault();
            try
            {
                ViewBag.BodegaSelec = vbod_Id.bod_Nombre;
                ViewBag.bod_Id = vbod_Id.bodId;
                ViewBag.Producto = db.tbBodegaDetalle.Where(x => x.bod_Id == vbod_Id.bodId && x.bodd_CantidadExistente > x.bodd_CantidadMinima).ToList();
                ViewBag.Factura = db.tbFactura.Where(x => x.fact_EsAnulada != Helpers.fact_EsAnulada && x.esfac_Id == Helpers.esfac_Pagada || x.esfac_Id == Helpers.esfac_PagoPendiente).ToList();
                ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
                ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion");
                ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
                ViewBag.sal_BodDestino = new SelectList(db.tbBodega.Where(x => x.bod_Id != vbod_Id.bodId), "bod_Id", "bod_Nombre");
                ViewBag.tdev_Id = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion");

                var list = (List<tbSalidaDetalle>)Session["SalidaDetalle"];

                var MensajeError = "0";
                var MensajeErrorDetalle = "0";
                IEnumerable<object> listSalida = null;
                IEnumerable<object> listSalidaDetalle = null;
                if (ModelState.IsValid)
                {
                    using (TransactionScope Tran = new TransactionScope())
                    {
                        var vFactura = db.tbFactura.Where(x => x.fact_Codigo == tbSalida.fact_Codigo).Select(x => x.fact_Id).SingleOrDefault();
                        //tbSalida.bod_Id
                        listSalida = db.UDP_Inv_tbSalida_Insert(tbSalida.bod_Id, vFactura, tbSalida.sal_FechaElaboracion, Helpers.sal_Emitida, tbSalida.tsal_Id, tbSalida.sal_BodDestino, tbSalida.sal_EsAnulada, tbSalida.tdev_Id, tbSalida.sal_RazonAnulada, Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Inv_tbSalida_Insert_Result Salida in listSalida)
                            MensajeError = Salida.MensajeError;
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbSalida);
                        }
                        else
                        {
                            var MsjError = Convert.ToInt32(MensajeError);
                            if (MsjError > 0)
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
                                    {
                                        foreach (tbSalidaDetalle Detalle in list)
                                        {
                                            var box_Codigo = "0";
                                            Detalle.box_Codigo = MensajeError;
                                            listSalidaDetalle = db.UDP_Inv_tbSalidaDetalle_Insert(
                                                MsjError,
                                                Detalle.prod_Codigo,
                                                Detalle.sald_Cantidad,
                                                box_Codigo
                                                , Function.GetUser(), Function.DatetimeNow());
                                            foreach (UDP_Inv_tbSalidaDetalle_Insert_Result spDetalle in listSalidaDetalle)
                                            {
                                                MensajeErrorDetalle = spDetalle.MensajeError;
                                                if (MensajeError == "-1")
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbSalida);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                Session["SalidaDetalle"] = null;
                                return View(tbSalida);
                            }
                        }
                        Tran.Complete();
                        Session["SalidaDetalle"] = null;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.Producto = db.tbBodegaDetalle.Where(x => x.bod_Id == vbod_Id.bodId && x.bodd_CantidadExistente < x.bodd_CantidadMinima).ToList();
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return View(tbSalida);
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());

                ViewBag.Producto = db.tbBodegaDetalle.Where(x => x.bod_Id == vbod_Id.bodId && x.bodd_CantidadExistente < x.bodd_CantidadMinima).ToList();
            }
            Session["SalidaDetalle"] = null;
            return View(tbSalida);
        }

        public JsonResult BodegaDestino(int? id)
        {
            IEnumerable<object> list = null;
            try
            {
                list = db.SDP_tbBodega_Listado(id).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBox(string box_Codigo,int? bod_Id)
        {
            IEnumerable<object> list = null;
            try
            {
                list = (from Box in db.tbBox
                        join Boxd in db.tbBoxDetalle on Box.box_Codigo equals Boxd.box_Codigo
                        join Producto in db.tbProducto on Boxd.prod_Codigo equals Producto.prod_Codigo
                        join Subcategoria in db.tbProductoSubcategoria on Producto.pscat_Id equals Subcategoria.pscat_Id
                        join Categoria in db.tbProductoCategoria on Subcategoria.pcat_Id equals Categoria.pcat_Id
                        join UniMedida in db.tbUnidadMedida on Producto.uni_Id equals UniMedida.uni_Id
                        where Box.box_Codigo == box_Codigo && Box.bod_Id == bod_Id 
                        select new {
                            prod_Codigo = Boxd.prod_Codigo,
                            prod_Descripcion = Producto.prod_Descripcion,
                            prod_Color = Producto.prod_Color,
                            prod_Marca=Producto.prod_Marca,
                            prod_Modelo=Producto.prod_Modelo,
                            prod_Talla=Producto.prod_Talla,
                            prod_CodigoBarras= Producto.prod_CodigoBarras,
                            pcat_Nombre=Categoria.pcat_Nombre,
                            pscat_Descripcion = Subcategoria.pscat_Descripcion,
                            uni_Descripcion=UniMedida.uni_Descripcion,
                            boxd_Cantidad = Boxd.boxd_Cantidad,
                            box_Codigo = Box.box_Codigo
                        }).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BodegaDestinoEdit(int? id)
        {
            int list = 0;
            try
            {
                list = db.tbSalida.Find(id).sal_BodDestino;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProdList(tbBodegaDetalle tbBodegaDetalle)
        {
            IEnumerable<object> list = null;

            try
            {
                //tbBodegaDetalle.bod_Id
                list = db.SDP_Inv_tbBodegaDetalle_Select_Producto(4).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            object jsonlist = new { d = list };
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProdCodBar(string prod_CodigoBarras, int bod_Id)
        {
            IEnumerable<object> list = null;
            try
            {
                list = db.SDP_Inv_tbProducto_Select_CodBar(prod_CodigoBarras, bod_Id).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveNewDatail(tbSalidaDetalle SalidaDetalle)
        {
            var list = (List<tbSalidaDetalle>)Session["SalidaDetalle"];
            var MensajeError = "0";
            var MensajeErrorDetalle = "0";
            IEnumerable<object> listSalidaDetalle = null;
            try
            {
                if (list != null)
                {
                    if (list.Count != 0)
                    {
                        foreach (tbSalidaDetalle Detalle in list)
                        {
                            var Exits = db.tbSalidaDetalle.Where(
                                x => x.prod_Codigo == Detalle.prod_Codigo
                                &&
                                x.sal_Id == SalidaDetalle.sal_Id).FirstOrDefault();

                            if (Exits != null)
                            {
                                var Cantidad = db.tbSalidaDetalle.Where(
                                x => x.prod_Codigo == Detalle.prod_Codigo
                                &&
                                x.sal_Id == SalidaDetalle.sal_Id).Select(c => c.sald_Cantidad).FirstOrDefault();

                                var pSalidaDetalle = db.tbSalidaDetalle.Where(x => x.sal_Id == SalidaDetalle.sal_Id).Select(c => c.sald_Id).FirstOrDefault();

                                decimal CantidadNew = Convert.ToDecimal(Cantidad) + Convert.ToDecimal(Detalle.sald_Cantidad);
                                tbSalidaDetalle vSalidaDetalle = db.tbSalidaDetalle.Find(pSalidaDetalle);
                                listSalidaDetalle = db.UDP_Inv_tbSalidaDetalle_Update(pSalidaDetalle,
                                                                    vSalidaDetalle.sal_Id,
                                                                    Detalle.prod_Codigo,
                                                                    CantidadNew,
                                                                    vSalidaDetalle.box_Codigo,
                                                                    vSalidaDetalle.sald_UsuarioCrea,
                                                                    vSalidaDetalle.sald_FechaCrea,
                                                                    Function.GetUser(),
                                                                    Function.DatetimeNow());

                                foreach (UDP_Inv_tbSalidaDetalle_Update_Result RSSalidaDetalle in listSalidaDetalle)
                                    MensajeError = RSSalidaDetalle.MensajeError;
                                if (MensajeError == "-1")
                                {
                                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                    //return PartialView("_EditSalidaDetalle");
                                    return Json(ModelState, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                var box_Codigo = "0";
                                Detalle.box_Codigo = MensajeError;
                                listSalidaDetalle = db.UDP_Inv_tbSalidaDetalle_Insert(
                                    SalidaDetalle.sal_Id,
                                    Detalle.prod_Codigo,
                                    Detalle.sald_Cantidad,
                                    box_Codigo
                                    , Function.GetUser(), Function.DatetimeNow());
                                foreach (UDP_Inv_tbSalidaDetalle_Insert_Result spDetalle in listSalidaDetalle)
                                {
                                    MensajeErrorDetalle = spDetalle.MensajeError;
                                    if (MensajeError == "-1")
                                    {
                                        ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                        return Json("", JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Vacio");
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        // GET: /Salida/Edit
        [SessionManager("Salida/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            Session["SalidaDetalle"] = null;
            if (tbSalida == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            int idUser = Usuario();

            ViewBag.IdSal = id;
            if (tbSalida.tsal_Id == Helpers.sal_Venta)
            {
                ViewBag.vbfact_Codigo = db.tbFactura.Find(tbSalida.fact_Id).fact_Codigo.ToString();
            }
            else
            {
                ViewBag.fact_Codigo = "0";
            }

            ViewBag.bod_Id = new SelectList(db.tbBodega.Where(x => x.bod_ResponsableBodega == idUser).ToList(), "bod_Id", "bod_Nombre");
            ViewBag.tdev_Id = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion", tbSalida.tdev_Id);

            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbSalida.estm_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbSalida.fact_Id);
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion", tbSalida.tsal_Id);

            ViewBag.sal_BodDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSalida.sal_BodDestino);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbSalida.estm_Id);
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion", tbSalida.tsal_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbSalida.fact_Id);
            ViewBag.box_Codigo = new SelectList(db.tbSalidaDetalle, "sald_Id", "box_Codigo", tbSalida.estm_Id);

            ViewBag.sal_Id = new SelectList(db.tbProductoSubcategoria, "sal_Id", "sal_Id", tbSalida.sal_Id);
            ViewBag.Producto = db.tbBodegaDetalle.Where(x => x.bod_Id == tbSalida.bod_Id && x.bodd_CantidadExistente > x.bodd_CantidadMinima).ToList();

            return View(tbSalida);
        }

        [HttpPost]
        public JsonResult getSalidaDetalle(string sald_Id)
        {
            IEnumerable<object> list = null;
            try
            {
                var dsad = Convert.ToInt32(sald_Id);
                list = db.SDP_Inv_tbSalidaDetalle_Edit_Select(dsad).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteSalidaDetalle(string sald_Id)
        {
            IEnumerable<object> list = null;
            try
            {
                var vsald_Id = Convert.ToInt32(sald_Id);
                list = db.UDP_Inv_tbSalidaDetalle_Delete(vsald_Id).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // POST: /Salida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult EditSalidaDetalle(tbSalidaDetalle data)
        {
            try
            {
                tbSalidaDetalle pSalidaDetalle = db.tbSalidaDetalle.Find(data.sald_Id);
                var MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Inv_tbSalidaDetalle_Update(data.sald_Id,
                                                    data.sal_Id,
                                                    data.prod_Codigo,
                                                    data.sald_Cantidad,
                                                    data.box_Codigo, pSalidaDetalle.sald_UsuarioCrea, pSalidaDetalle.sald_FechaCrea, Function.GetUser(), Function.DatetimeNow());

                foreach (UDP_Inv_tbSalidaDetalle_Update_Result RSSalidaDetalle in list)
                    MensajeError = RSSalidaDetalle.MensajeError;
                if (MensajeError == "-1")
                {
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    //return PartialView("_EditSalidaDetalle");
                    return Json(ModelState, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ViewBag.SalID = Convert.ToInt32(pSalidaDetalle.sal_Id);
                    return Json(data.sal_Id, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return Json(ModelState, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Cantidad(int bod_Id, string prod_Codigo)
        {
            try
            {
                var CantidadExistente = db.tbBodegaDetalle.Where(x =>
                x.bod_Id == bod_Id
                &&
                x.prod_Codigo == prod_Codigo).Select(c => c.bodd_CantidadExistente).FirstOrDefault();

                var CantidadMinima = db.tbBodegaDetalle.Where(x =>
                 x.bod_Id == bod_Id
                 &&
                 x.prod_Codigo == prod_Codigo).Select(c => c.bodd_CantidadMinima).FirstOrDefault();

                //var CantidadSalida = db.tbSalida.Where(x => x.sal_EsAnulada == false && x.estm_Id == Helpers.sal_Emitida).Select(p => p.sal_Id).ToList();
                var CantidadSalidaDetalle = db.SDP_Inv_Cantidad_Salida_Emitida(prod_Codigo).Select(x => x.sald_Cantidad).SingleOrDefault();
                object CantidadPermitida = null;
                var vCantidad = CantidadExistente - CantidadMinima;
                if (CantidadSalidaDetalle == null)
                {
                    var CantidadAceptada = vCantidad;
                    CantidadPermitida = new { CantidadAceptada, CantidadMinima };
                }
                else
                {
                    var CantidadAceptada = vCantidad - CantidadSalidaDetalle;
                    CantidadPermitida = new { CantidadAceptada, CantidadMinima };
                }

                //if (CantidadPermitida == 0 && CantidadMinima > 0)
                //{
                //    var CantidadMinimaA = "Cantidad Minima Alcanzada solo hay en existencia: "+ CantidadMinima+" de este producto";
                //    return Json(CantidadMinimaA, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(CantidadPermitida, JsonRequestBehavior.AllowGet);
                //}

                return Json(CantidadPermitida, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return Json(ModelState, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: /Salida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Salida/Edit")]
        public ActionResult Edit(int? id, [Bind(Include = "sal_Id, bod_Id,fact_Id,fact_Codigo,sal_FechaElaboracion,estm_Id,tsal_Id,  sal_RazonDevolucion, sal_UsuarioCrea, sal_FechaCrea,tbFactura_fact_Codigo, sal_BodDestino")] tbSalida tbSalida)
        {
            using (TransactionScope _Tran = new TransactionScope())
            {
                try
                {
                    ViewBag.sal_BodDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSalida.sal_BodDestino);
                    if (ModelState.IsValid)
                    {
                        var list = (List<tbSalidaDetalle>)Session["SalidaDetalle"];
                        var MensajeError = "0";
                        var MensajeErrorDetalle = "0";
                        IEnumerable<object> listSalidaDetalle = null;
                        IEnumerable<object> listSalida = null;
                        ViewBag.tdev_Id = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion", tbSalida.tdev_Id);

                        ViewBag.sal_BodDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSalida.sal_BodDestino);
                        ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
                        ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSalida.bod_Id);
                        ViewBag.bod_Nombre = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
                        ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbSalida.estm_Id);
                        ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbSalida.fact_Id);
                        ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion", tbSalida.tsal_Id);
                        ViewBag.Producto = db.tbBodegaDetalle.ToList();
                        ViewBag.Cliente = db.tbCliente.ToList();

                        tbSalida pSalida = db.tbSalida.Find(id);

                        listSalida = db.UDP_Inv_tbSalida_Update(tbSalida.sal_Id,
                                                        tbSalida.bod_Id,
                                                        tbSalida.fact_Id,
                                                        tbSalida.sal_FechaElaboracion,
                                                        tbSalida.estm_Id,
                                                        tbSalida.tsal_Id,
                                                        tbSalida.sal_BodDestino,
                                                        tbSalida.sal_EsAnulada,
                                                        tbSalida.tdev_Id, 
                                                        tbSalida.sal_RazonAnulada,
                                                        pSalida.sal_UsuarioCrea,
                                                        pSalida.sal_FechaCrea,
                                                        Function.GetUser(), Function.DatetimeNow());

                        foreach (UDP_Inv_tbSalida_Update_Result vSalida in listSalida)
                            MensajeError = vSalida.MensajeError;
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbSalida);
                        }
                        else
                        {
                            var MsjError = Convert.ToInt32(MensajeError);
                            if (MsjError > 0)
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
                                    {
                                        foreach (tbSalidaDetalle Detalle in list)
                                        {
                                            var Exits = db.tbSalidaDetalle.Where(
                                                x => x.prod_Codigo == Detalle.prod_Codigo
                                                &&
                                                x.sal_Id == Detalle.sal_Id).FirstOrDefault();

                                            if (Exits != null)
                                            {
                                                var Cantidad = db.tbSalidaDetalle.Where(
                                                x => x.prod_Codigo == Detalle.prod_Codigo
                                                &&
                                                x.sal_Id == Detalle.sal_Id).Select(c => c.sald_Cantidad).FirstOrDefault();

                                                var pSalidaDetalle = db.tbSalidaDetalle.Where(x => x.sal_Id == Detalle.sal_Id).Select(c => c.sald_Id).FirstOrDefault();
                                                tbSalidaDetalle vSalidaDetalle = db.tbSalidaDetalle.Find(pSalidaDetalle);

                                                decimal CantidadNew = Convert.ToDecimal(Exits.sald_Cantidad) + Convert.ToDecimal(Detalle.sald_Cantidad);
                                                listSalidaDetalle = db.UDP_Inv_tbSalidaDetalle_Update(Exits.sald_Id,
                                                                                    Exits.sal_Id,
                                                                                    Detalle.prod_Codigo,
                                                                                    CantidadNew,
                                                                                    Exits.box_Codigo,
                                                                                    Exits.sald_UsuarioCrea,
                                                                                    Exits.sald_FechaCrea,
                                                                                    Function.GetUser(),
                                                                                    Function.DatetimeNow());

                                                foreach (UDP_Inv_tbSalidaDetalle_Update_Result RSSalidaDetalle in listSalidaDetalle)
                                                    MensajeError = RSSalidaDetalle.MensajeError;
                                                if (MensajeError.StartsWith("-1"))
                                                {
                                                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                                    //return PartialView("_EditSalidaDetalle");
                                                    return Json(ModelState, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            else
                                            {
                                                var box_Codigo = "0";
                                                Detalle.box_Codigo = MensajeError;
                                                listSalidaDetalle = db.UDP_Inv_tbSalidaDetalle_Insert(
                                                    Detalle.sal_Id,
                                                    Detalle.prod_Codigo,
                                                    Detalle.sald_Cantidad,
                                                    box_Codigo
                                                    , Function.GetUser(), Function.DatetimeNow());
                                                foreach (UDP_Inv_tbSalidaDetalle_Insert_Result spDetalle in listSalidaDetalle)
                                                {
                                                    MensajeErrorDetalle = spDetalle.MensajeError;
                                                    if (MensajeError.StartsWith("-1"))
                                                    {
                                                        ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                        return Json("", JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                Session["SalidaDetalle"] = null;
                                return View(tbSalida);
                            }
                        }
                        _Tran.Complete();
                        Session["SalidaDetalle"] = null;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Producto = db.tbBodegaDetalle.ToList();
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                    }
                    ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
                    ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSalida.bod_Id);
                    ViewBag.bod_Nombre = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
                    ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbSalida.estm_Id);
                    ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbSalida.fact_Id);
                    ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion", tbSalida.tsal_Id);
                    ViewBag.Producto = db.tbBodegaDetalle.ToList();
                    Session["SalidaDetalle"] = null;
                    return View(tbSalida);
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.sal_FechaCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalida.sal_FechaCrea);
                    ViewBag.sal_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalida.sal_UsuarioModifica);
                    ViewBag.Producto = db.tbBodegaDetalle.ToList();
                }
            }
            Session["SalidaDetalle"] = null;
            return RedirectToAction("Index");
        }

        public JsonResult FacturaExist(string fact_Codigo)
        {
            string Message = "";
            long vFacturaIDS = 0;
            int SalidaFact = 0;
            IEnumerable<object> vSalida = null;
            object vreturn = null;
            try
            {
                if (fact_Codigo == "")
                {
                    Message = "Inserte una Factura";
                }
                else
                {
                    if (fact_Codigo == "***-***-**-********")
                    {
                    }
                    else
                    {
                        if (fact_Codigo == "___-___-__-________")
                        {
                            Message = "Inserte una Factura";
                        }
                        else
                        {
                            //.Select(x => x.fact_Id).SingleOrDefault()
                            var vFactura = db.tbFactura.Where(x => x.fact_Codigo == fact_Codigo).ToList();
                           if (vFactura.Count() > 0)
                            {
                                var vFacturaID = (from factura in db.tbFactura
                                                  where factura.fact_Codigo == fact_Codigo
                                                  select new { fact_Id = factura.fact_Id, fact_EsAnulada = factura.fact_EsAnulada, esfac_Id = factura.esfac_Id}).FirstOrDefault();


                                //db.tbFactura.Where(x => x.fact_Codigo == fact_Codigo && x.esfac_Id == Helpers.esfac_Pagada || x.esfac_Id == Helpers.esfac_PagoPendiente && x.fact_EsAnulada != true ).Select(x => x.fact_Id);
                                if (vFacturaID.fact_EsAnulada)
                                {
                                    Message = "Factura Anulada";

                                }
                                else
                                {
                                    if (vFacturaID.esfac_Id == Helpers.esfac_Pagada || vFacturaID.esfac_Id == Helpers.esfac_PagoPendiente)
                                    {
                                        vSalida = db.tbSalida.Where(x => x.fact_Id == vFacturaID.fact_Id && x.sal_EsAnulada != true).ToList();
                                        if (vSalida.Count() > 0)
                                        {
                                            Message = "Ya existe una Salida con ese Codigo de Factura";
                                            SalidaFact = db.tbSalida.Where(x => x.fact_Id == vFacturaID.fact_Id && x.sal_EsAnulada != true).Select(x =>x.sal_Id).FirstOrDefault();
                                        }
                                        else
                                        {
                                            Message = "Factura Disponible";
                                        }
                                    }
                                    else
                                    {
                                        Message = "Factura No Disponible";
                                        //vFacturaIDS = vFacturaID.fact_Id;
                                        //vSalida = db.tbSalida.Where(x => x.fact_Id == vFacturaIDS && x.sal_EsAnulada != true).ToList();
                                        //if (vSalida.Count() > 0)
                                        //{
                                        //    Message = "Ya existe una Salida con ese Codigo de Factura";
                                        //}
                                        //else
                                        //{
                                        //    Message = "Factura Disponible";
                                        //}
                                    }
                                }
                            }
                            else
                            {
                                Message = "No Existe Esa Factura";
                            }
                        }
                    }
                }
                vreturn = new { Message, SalidaFact };
                return Json(vreturn, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(Ex, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Aplicar(int? id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    tbSalida tbSalida = db.tbSalida.Find(id);
                    list = db.UDP_Inv_tbSalida_Update(tbSalida.sal_Id,
                                                    tbSalida.bod_Id,
                                                    tbSalida.fact_Id,
                                                    tbSalida.sal_FechaElaboracion,
                                                    Helpers.sal_Aplicada,
                                                    tbSalida.tsal_Id,
                                                    tbSalida.sal_BodDestino,
                                                    tbSalida.sal_EsAnulada,
                                                    tbSalida.tdev_Id, 
                                                    tbSalida.sal_RazonAnulada,
                                                    tbSalida.sal_UsuarioCrea,
                                                    tbSalida.sal_FechaCrea,
                                                    Function.GetUser(), Function.DatetimeNow());

                    foreach (UDP_Inv_tbSalida_Update_Result Salida in list)
                        MensajeError = Salida.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
            }

            return RedirectToAction("Index");
        }

        public JsonResult Anular(tbSalida Salida)
        {
            try
            {
                var MensajeError = "";

                IEnumerable<object> list = null;
                tbSalida vSalida = db.tbSalida.Find(Salida.sal_Id);
                list = db.UDP_Inv_tbSalida_Update(vSalida.sal_Id,
                                                vSalida.bod_Id,
                                                vSalida.fact_Id,
                                                vSalida.sal_FechaElaboracion,
                                                Helpers.sal_Anulada,
                                                vSalida.tsal_Id,
                                                vSalida.sal_BodDestino,
                                                Helpers.sal_EsAnulada,
                                                vSalida.tdev_Id,
                                                Salida.sal_RazonAnulada,
                                                vSalida.sal_UsuarioCrea,
                                                vSalida.sal_FechaCrea,
                                                Function.GetUser(), Function.DatetimeNow());

                foreach (UDP_Inv_tbSalida_Update_Result RSSalida in list)
                    MensajeError = RSSalida.MensajeError;
                if (MensajeError.StartsWith("-1"))
                {
                }
                else
                {
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveSalidaDetalle(tbSalidaDetalle SalidaDetalle, string data_producto)
        {
            var datos = "";
            decimal CantidadVieja = 0;
            decimal CantidadNueva = 0;
            //data_producto = SalidaDetalle.prod_Codigo;
            decimal data_cantidad = SalidaDetalle.sald_Cantidad;
            List<tbSalidaDetalle> sessionSalidaDetalle = new List<tbSalidaDetalle>();
            var list = (List<tbSalidaDetalle>)Session["SalidaDetalle"];
            if (list == null)
            {
                sessionSalidaDetalle.Add(SalidaDetalle);
                Session["SalidaDetalle"] = sessionSalidaDetalle;
            }
            else
            {
                foreach (var vSalidaDetalle in list)
                    if (vSalidaDetalle.prod_Codigo == data_producto)
                    {
                        datos = data_producto;
                        foreach (var viejo in list)
                            if (viejo.prod_Codigo == SalidaDetalle.prod_Codigo)
                                CantidadVieja = viejo.sald_Cantidad;
                        CantidadNueva = CantidadVieja + data_cantidad;
                        vSalidaDetalle.sald_Cantidad = CantidadNueva;
                        return Json(datos, JsonRequestBehavior.AllowGet);
                    }
                list.Add(SalidaDetalle);
                Session["SalidaDetalle"] = list;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveSalidaDetalle(tbSalidaDetalle SalidaDetalle)
        {
            var list = (List<tbSalidaDetalle>)Session["SalidaDetalle"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.prod_Codigo == SalidaDetalle.prod_Codigo);
                list.Remove(itemToRemove);
                Session["SalidaDetalle"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveCreateSalidaDetalle(tbSalidaDetalle tbSalidaDetalle)
        {
            var MensajeError = "";
            IEnumerable<object> listSalidaDetalle = null;
            try
            {
                var box_Codigo = "0";
                listSalidaDetalle = db.UDP_Inv_tbSalidaDetalle_Insert(
                    tbSalidaDetalle.sal_Id,
                    tbSalidaDetalle.prod_Codigo,
                    tbSalidaDetalle.sald_Cantidad,
                    box_Codigo
                    , Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbSalidaDetalle_Insert_Result spDetalle in listSalidaDetalle)
                {
                    MensajeError = spDetalle.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                        //return View(tbSalidaDetalle);
                    }
                }
            }
            catch (Exception Ex)
            {
                MensajeError = Ex.Message.ToString();
                //ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", CreatePuntoEmisionDetalle.dfisc_Id);
                ModelState.AddModelError("", MensajeError);
            }
            return Json(MensajeError, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}