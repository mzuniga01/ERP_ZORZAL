using CrystalDecisions.CrystalReports.Engine;
using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;

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
            var tbsalida = db.tbSalida;
            return View(tbsalida.ToList());
        }

        public ActionResult ExportReport()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Salida.rpt"));
            var tbsalida = db.tbSalida.ToList();
            var todo = (from r in tbsalida
                        select new
                        {
                            sal_Id = r.sal_Id,
                            fact_Codigo = r.fact_Codigo,
                            bod_Id = r.bod_Id,
                            tsal_Id = r.tsal_Id
                        }).ToList();

            rd.SetDataSource(todo);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Salida_List.pdf");
            }
            catch
            {
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
            int idUser = 0;
            try
            {
                Session["SalidaDetalle"] = null;
                ViewBag.sal_BodDestino = db.SDP_tbBodega_Listado(3).ToList();
                GeneralFunctions Login = new GeneralFunctions();
                List<tbUsuario> User = Login.getUserInformation();
                foreach (tbUsuario Usuario in User)
                {
                    idUser = Convert.ToInt32(Usuario.emp_Id);
                }

                ViewBag.bod_Id = new SelectList(db.tbBodega.Where(x => x.bod_ResponsableBodega == idUser).ToList(), "bod_Id", "bod_Nombre");
                ViewBag.sal_BodDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
                ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
                ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion");
                ViewBag.Producto = db.tbBodegaDetalle.ToList();
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
        public ActionResult Create([Bind(Include = "bod_Id,fact_Id,fact_Codigo,sal_FechaElaboracion,estm_Id,tsal_Id,sal_RazonDevolucion, sal_BodDestino, sal_EsAnulada, sal_RazonAnulada")] tbSalida tbSalida)
        {
            int idUser = 0;
            try
            {
                GeneralFunctions Login = new GeneralFunctions();
                List<tbUsuario> User = Login.getUserInformation();
                foreach (tbUsuario Usuario in User)
                {
                    idUser = Convert.ToInt32(Usuario.emp_Id);
                }
                ViewBag.Producto = db.tbBodegaDetalle.ToList();
                ViewBag.bod_Id = new SelectList(db.tbBodega.Where(x => x.bod_ResponsableBodega == idUser).ToList(), "bod_Id", "bod_Nombre");
                ViewBag.sal_BodDestinosa = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
                ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
                ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion");
                ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
                ViewBag.sal_BodDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
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
                        listSalida = db.UDP_Inv_tbSalida_Insert(tbSalida.bod_Id, vFactura, tbSalida.sal_FechaElaboracion, tbSalida.estm_Id, tbSalida.tsal_Id, tbSalida.sal_BodDestino, tbSalida.sal_EsAnulada, tbSalida.sal_RazonAnulada, tbSalida.sal_RazonDevolucion, Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Inv_tbSalida_Insert_Result Salida in listSalida)
                            MensajeError = Salida.MensajeError;
                        if (MensajeError == "-1")
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
                    ViewBag.Producto = db.tbBodegaDetalle.ToList();
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return View(tbSalida);
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());

                ViewBag.Producto = db.tbBodegaDetalle.ToList();
            }
            Session["SalidaDetalle"] = null;
            return View(tbSalida);
        }

        public ActionResult GenReporte(tbSalida tbSalida)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Salida.rpt"));
            var tbsalida = db.tbSalida.ToList();
            var todo = db.SDP_Inv_Salida_Imprimir(13).ToList();

            rd.SetDataSource(todo);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Salida_List.pdf");
            }
            catch
            {
                throw;
            }
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
                list = db.SDP_Inv_tbBodegaDetalle_Select_Producto(tbBodegaDetalle.bod_Id).ToList();
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
            ViewBag.IdSal = id;
            ViewBag.bod_Id = new SelectList(db.tbBodega.Where(x => x.bod_ResponsableBodega == idUser).ToList(), "bod_Id", "bod_Nombre");

            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbSalida.estm_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbSalida.fact_Id);
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion", tbSalida.tsal_Id);

            ViewBag.sal_BodDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSalida.sal_BodDestino);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbSalida.estm_Id);
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion", tbSalida.tsal_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbSalida.fact_Id);
            ViewBag.box_Codigo = new SelectList(db.tbSalidaDetalle, "sald_Id", "box_Codigo", tbSalida.estm_Id);

            ViewBag.sal_Id = new SelectList(db.tbProductoSubcategoria, "sal_Id", "sal_Id", tbSalida.sal_Id);
            ViewBag.Producto = db.tbBodegaDetalle.ToList();

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
                var CantidadBodegaDetalle = db.tbBodegaDetalle.Where(x =>
                x.bod_Id == bod_Id
                &&
                x.prod_Codigo == prod_Codigo).Select(c => c.bodd_CantidadExistente).SingleOrDefault();
                return Json(CantidadBodegaDetalle, JsonRequestBehavior.AllowGet);
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
            ViewBag.sal_BodDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSalida.sal_BodDestino);
            if (ModelState.IsValid)
            {
                try
                {
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

                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Inv_tbSalida_Update(tbSalida.sal_Id,
                                                    tbSalida.bod_Id,
                                                    tbSalida.fact_Id,
                                                    tbSalida.sal_FechaElaboracion,
                                                    tbSalida.estm_Id,
                                                    tbSalida.tsal_Id,
                                                    tbSalida.sal_BodDestino,
                                                    tbSalida.sal_EsAnulada,
                                                    tbSalida.sal_RazonAnulada,
                                                    tbSalida.sal_RazonDevolucion,
                                                    pSalida.sal_UsuarioCrea,
                                                    pSalida.sal_FechaCrea,
                                                    Function.GetUser(), Function.DatetimeNow());

                    foreach (UDP_Inv_tbSalida_Update_Result ListaPrecio in list)
                        MensajeError = ListaPrecio.MensajeError;
                    if (MensajeError == "-1")
                    {
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.sal_FechaCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalida.sal_FechaCrea);
                    ViewBag.sal_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalida.sal_UsuarioModifica);
                    ViewBag.Producto = db.tbBodegaDetalle.ToList();
                }
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

        public JsonResult FacturaExist(string fact_Codigo)
        {
            IEnumerable<object> fact = null;
            string Message = "";
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
                        //.Select(x => x.fact_Id).SingleOrDefault()
                        var vFactura = db.tbFactura.Where(x => x.fact_Codigo == fact_Codigo).ToList();
                        if (vFactura.Count() > 0)
                        {
                            var vFacturaID = db.tbFactura.Where(x => x.fact_Codigo == fact_Codigo).Select(x => x.fact_Id).First();
                            var vSalida = db.tbSalida.Where(x => x.fact_Id == vFacturaID).ToList();
                            if (vSalida.Count() > 0)
                            {
                                Message = "Ya existe una Salida con ese Codigo de Factura";
                            }
                            else
                            {
                                Message = "";
                            }
                        }
                        else
                        {
                            Message = "No Existe Esa Factura";
                        }
                    }
                }
                return Json(Message, JsonRequestBehavior.AllowGet);
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
                    byte Estado = 2;
                    IEnumerable<object> list = null;
                    tbSalida tbSalida = db.tbSalida.Find(id);
                    list = db.UDP_Inv_tbSalida_Update(tbSalida.sal_Id,
                                                    tbSalida.bod_Id,
                                                    tbSalida.fact_Id,
                                                    tbSalida.sal_FechaElaboracion,
                                                    Estado,
                                                    tbSalida.tsal_Id,
                                                    tbSalida.sal_BodDestino,
                                                    tbSalida.sal_EsAnulada,
                                                    tbSalida.sal_RazonAnulada,
                                                    tbSalida.sal_RazonDevolucion,
                                                    tbSalida.sal_UsuarioCrea,
                                                    tbSalida.sal_FechaCrea,
                                                    Function.GetUser(), Function.DatetimeNow());

                    foreach (UDP_Inv_tbSalida_Update_Result Salida in list)
                        MensajeError = Salida.MensajeError;
                    if (MensajeError == "-1")
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

        public ActionResult Imprimir(int id)
        {
            //Salida SalidaRD = new Salida();
            //SalidaRD.SetParameterValue("@sal_Id", id);
            //var si = SalidaRD;

            ReportDocument rd = new ReportDocument();
            //    Reports.Salida SalidaRD = new Reports.Salida();
            //    SalidaRD.SetParameterValue("@sal_Id", id);

            //    SalidaRD.SetDataSource(rd);

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Salida.rpt"));
            //    //var tbsalida = db.tbSalida.ToList();
            var todo = db.SDP_Inv_Salida_Imprimir(id).ToList();

            rd.SetDataSource(todo);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Salida_List.pdf");
            }
            catch
            {
                throw;
            }
        }

        public JsonResult Anular(tbSalida Salida)
        {
            try
            {
                var MensajeError = "";
                bool EsAnulada = true;
                IEnumerable<object> list = null;
                tbSalida vSalida = db.tbSalida.Find(Salida.sal_Id);
                list = db.UDP_Inv_tbSalida_Update(vSalida.sal_Id,
                                                vSalida.bod_Id,
                                                vSalida.fact_Id,
                                                vSalida.sal_FechaElaboracion,
                                                vSalida.estm_Id,
                                                vSalida.tsal_Id,
                                                vSalida.sal_BodDestino,
                                                EsAnulada,
                                                vSalida.sal_RazonDevolucion,
                                                Salida.sal_RazonAnulada,
                                                vSalida.sal_UsuarioCrea,
                                                vSalida.sal_FechaCrea,
                                                Function.GetUser(), Function.DatetimeNow());

                foreach (UDP_Inv_tbSalida_Update_Result RSSalida in list)
                    MensajeError = RSSalida.MensajeError;
                if (MensajeError == "-1")
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
            decimal cantvieja = 0;
            decimal cantnueva = 0;
            data_producto = SalidaDetalle.prod_Codigo;
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
                foreach (var t in list)
                    if (t.prod_Codigo == data_producto)
                    {
                        datos = data_producto;
                        foreach (var viejo in list)
                            if (viejo.prod_Codigo == SalidaDetalle.prod_Codigo)
                                cantvieja = viejo.sald_Cantidad;
                        cantnueva = cantvieja + data_cantidad;
                        t.sald_Cantidad = cantnueva;
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