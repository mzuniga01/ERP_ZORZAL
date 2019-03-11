using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web.Mvc;
using Newtonsoft.Json;
using ERP_GMEDINA.Attribute;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;
using ERP_GMEDINA.Reports;
using ERP_GMEDINA.Dataset;

namespace ERP_ZORZAL.Controllers
{
    public class BoxController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        private GeneralFunctions Function = new GeneralFunctions();

        public object ACTUALIZAR_tbBoxDetalle { get; private set; }

        // GET: /Box/
        [SessionManager("Box/Index")]
        public ActionResult Index()
        {
            ViewBag.Box = new tbBox();
            return View(db.tbBox.ToList());
        }

        // GET: /Box/Details/5
        [SessionManager("Box/Details")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbBox tbBox = db.tbBox.Find(id);
            if (tbBox == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbBox);
        }

        // GET: /Box/Create
        [SessionManager("Box/Create")]
        public ActionResult Create()
        {
            ViewBag.ent_Id = new SelectList(db.tbEntrada, "ent_Id", "ent_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            var User = Usuario();
            Session["BoxDetalle"] = null;
            ViewBag.bod_Id = new SelectList(db.tbBodega.Where(x => x.bod_ResponsableBodega == User), "bod_Id", "bod_Nombre");
            ViewBag.Producto = db.tbBodegaDetalle.ToList();
            return View();
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

        public ActionResult _Producto()
        {
            return View();
        }

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

        // POST: /Box/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Box/Create")]
        public ActionResult Create([Bind(Include = "box_Codigo,box_Descripcion,bod_Id")] tbBox tbBox)
        {
            ViewBag.Producto = db.tbProducto.ToList();
            var list = (List<tbBoxDetalle>)Session["BoxDetalle"];
            string MensajeError = "0";
            string MensajeErrorDetalle = "0";
            IEnumerable<object> listBox = null;
            IEnumerable<object> listBoxDetalle = null;

            if (db.tbBox.Any(a => a.box_Codigo == tbBox.box_Codigo))
            {
                ModelState.AddModelError("", "Ya existe una caja con ese código");
            }
            if (ModelState.IsValid)
            {
                ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
                ViewBag.Producto = db.tbBodegaDetalle.ToList();
                try
                {
                    using (TransactionScope Tran = new TransactionScope())
                    {
                        listBox = db.UDP_Inv_tbBox_Insert(
                                                tbBox.box_Codigo,
                                                tbBox.box_Descripcion,
                                                tbBox.bod_Id,
                                                Function.GetUser(), Function.DatetimeNow()
                                                );
                        foreach (UDP_Inv_tbBox_Insert_Result Box in listBox)
                            MensajeError = Box.MensajeError;
                        if (MensajeError.StartsWith("-1"))
                        {
                            LlenarListas();
                            Function.InsertBitacoraErrores("Box/Create", MensajeError, "Create");
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            return View(tbBox);
                        }
                        else
                        {
                            if (list != null)
                            {
                                if (list.Count != 0)
                                {
                                    foreach (tbBoxDetalle Detalle in list)
                                    {
                                        Detalle.box_Codigo = MensajeError;
                                        listBoxDetalle = db.UDP_Inv_tbBoxDetalle_Insert(Detalle.box_Codigo,
                                                                                        Detalle.prod_Codigo,
                                                                                        Detalle.boxd_Cantidad, Function.GetUser(), Function.DatetimeNow());
                                        foreach (UDP_Inv_tbBoxDetalle_Insert_Result spDetalle in listBoxDetalle)
                                        {
                                            MensajeErrorDetalle = spDetalle.MensajeError;
                                            if (MensajeErrorDetalle.StartsWith("-1"))
                                            {
                                                LlenarListas();
                                                Function.InsertBitacoraErrores("Box/Create", MensajeErrorDetalle, "Create");
                                                ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                                return View(tbBox);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("Box/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    LlenarListas();
                    return View(tbBox);
                }
            }
            else
            {
                ViewBag.Producto = db.tbBodegaDetalle.ToList();
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(tbBox);
            }
        }

       

        // GET: /Box/Edit/5
        [SessionManager("Box/Edit")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbBox tbBox = db.tbBox.Find(id);
            if (tbBox == null)
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
            Session["BoxDetalle"] = null;

            ViewBag.Cod_Box = id;
            ViewBag.bod_Id = new SelectList(db.tbBodega.Where(x => x.bod_ResponsableBodega == idUser).ToList(), "bod_Id", "bod_Nombre");
            ViewBag.bod_Prod = db.tbBodega.Where(x => x.bod_ResponsableBodega == idUser).Select(x => x.bod_Id).First();
            ViewBag.Producto = db.tbBodegaDetalle.ToList();
            return View(tbBox);
        }

        public JsonResult getBoxDetalle(int boxd_Id)
        {
            IEnumerable<object> list = null;
            try
            {
                list = db.SDP_Inv_tbBoxDetalle_Select(boxd_Id).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
                //list = db.tbBoxDetalle.Find(boxd_Id).;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return Json("Fallo", JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public ActionResult EditBoxDetalle(tbBoxDetalle data)
        {
            try
            {
                tbBoxDetalle pBoxDetalle = db.tbBoxDetalle.Find(data.boxd_Id);
                var MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Inv_tbBoxDetalle_Update(data.boxd_Id,
                                                    pBoxDetalle.box_Codigo,
                                                    data.prod_Codigo,
                                                    data.boxd_Cantidad, pBoxDetalle.boxd_UsuarioCrea, pBoxDetalle.boxd_FechaCrea, Function.GetUser(), Function.DatetimeNow());

                foreach (UDP_Inv_tbBoxDetalle_Update_Result RSBoxDetalle in list)
                    MensajeError = RSBoxDetalle.MensajeError;
                if (MensajeError == "-1")
                {
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return PartialView("_EditBoxDetalle");
                }
                else
                {
                    return RedirectToAction("Edit", "Box", new { @id = data.box_Codigo });
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return PartialView("_EditBoxDetalle", data);
            }
        }
        [HttpPost]
        public JsonResult DeleteBoxDetalle(int boxd_Id)
        {
            try
            {
                var list = db.UDP_Inv_tbBoxDetalle_Delete(boxd_Id);
                return Json("Exito", JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return Json("Fallo", JsonRequestBehavior.AllowGet);
            }
        }
        // POST: /Box/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Box/Edit")]
        public ActionResult Edit(string id, [Bind(Include = "box_Codigo,box_Descripcion,bod_Id,box_UsuarioCrea,box_FechaCrea")] tbBox tbBox)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tbBox vBox = db.tbBox.Find(id);
                    IEnumerable<object> List = null;
                    string MsjError = "";
                    List = db.UDP_Inv_tbBox_Update(tbBox.box_Codigo, tbBox.box_Descripcion, tbBox.bod_Id, tbBox.box_Estado, vBox.box_UsuarioCrea, vBox.box_FechaCrea, Function.GetUser(), Function.DatetimeNow());

                    foreach (UDP_Inv_tbBox_Update_Result Box in List)
                        MsjError = Box.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        LlenarListas();
                        Function.InsertBitacoraErrores("Box/Edit", MsjError, "Edit");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    LlenarListas();
                    Function.InsertBitacoraErrores("Box/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Producto = db.tbBodegaDetalle.ToList();
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(tbBox);
            }
        }

        //[HttpPost]
        public ActionResult PackingList(string id)
        {
            //string codCaja = box.box_Codigo;
            ReportDocument rd = new ReportDocument();
            Stream stream = null;
            ERP_GMEDINA.Reports.ImprimirPackingList PackinglistRV = new ERP_GMEDINA.Reports.ImprimirPackingList();
            ERP_GMEDINA.Reports.ImprimirPackingListPorCaja PackinglistPorCajaRV = new ERP_GMEDINA.Reports.ImprimirPackingListPorCaja();
            Reportes PACK = new Reportes();
            var PACKTableAdapter = new UDV_Inv_PackingListTableAdapter();
            var PACKTableAdapter_Caja = new UDV_Inv_PackingList_CajaTableAdapter();
            var caja = db.tbSalidaDetalle.Where(x => x.box_Codigo == id).ToList();

            if (caja.Count() == 0)
            {
                try
                {
                    PACKTableAdapter_Caja.Fill(PACK.UDV_Inv_PackingList_Caja, id);
                    PackinglistPorCajaRV.SetDataSource(PACK);
                    stream = PackinglistPorCajaRV.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);

                    PackinglistPorCajaRV.Close();
                    PackinglistPorCajaRV.Dispose();

                    string fileName = "PackingList.pdf";
                    Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                    return File(stream, "application/pdf");
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    throw;
                }
            }
            else
            {
                try
                {
                    PACKTableAdapter.Fill(PACK.UDV_Inv_PackingList, id);
                    PackinglistRV.SetDataSource(PACK);
                    stream = PackinglistRV.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);

                    PackinglistRV.Close();
                    PackinglistRV.Dispose();

                    string fileName = "PackingList.pdf";
                    Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                    return File(stream, "application/pdf");
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    throw;
                }
            }
             
           
        }


        public ActionResult ExportReport(string id)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ImprimirPackingList.rpt"));
            var tbSalidaDetalle = db.tbSalidaDetalle.ToList();
            var tbBox = db.tbBox.ToList();
            var tbProducto = db.tbProducto.ToList();
            var tbUnidadMedida = db.tbUnidadMedida.ToList();
            var tbUsuario = db.tbUsuario.ToList();
            var tbSalida = db.tbSalida.ToList();
            var tbBodega = db.tbBodega.ToList();
            var todo = (from sd in tbSalidaDetalle
                        join bx in tbBox on sd.box_Codigo equals bx.box_Codigo
                        join p in tbProducto on sd.prod_Codigo equals p.prod_Codigo
                        join u in tbUnidadMedida on p.uni_Id equals u.uni_Id
                        join usu in tbUsuario on bx.box_UsuarioCrea equals usu.usu_Id
                        join s in tbSalida on sd.sald_Id equals s.sal_Id
                        join bd in tbBodega on s.bod_Id equals bd.bod_Id
                        where sd.box_Codigo == id
                        select new
                        {
                            box_Codigo = sd.box_Codigo,
                            box_Descripcion = bx.box_Descripcion,
                            prod_Codigo =sd.prod_Codigo,
                            prod_Descripcion = p.prod_Descripcion,
                            prod_Talla = p.prod_Talla,
                            uni_Descripcion = u.uni_Descripcion,
                            sald_Cantidad = sd.sald_Cantidad,
                            bod_Nombre = bd.bod_Nombre,
                            sal_BodDestino = s.sal_BodDestino,
                            usu_Nombre = usu.usu_Nombres + "" + usu.usu_Apellidos,
                            box_FechaCrea = bx.box_FechaCrea
                        }).ToList();

            rd.SetDataSource(todo);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf");
            }
            catch
            {
                throw;
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //[HttpPost]
        //public JsonResult SaveBoxDetalle(tbBoxDetalle BoxDetalle)
        //{
        //    List<tbBoxDetalle> sessionBoxDetalle = new List<tbBoxDetalle>();
        //    var list = (List<tbBoxDetalle>)Session["BoxDetalle"];
        //    if (list == null)
        //    {
        //        sessionBoxDetalle.Add(BoxDetalle);
        //        Session["BoxDetalle"] = sessionBoxDetalle;
        //    }
        //    else
        //    {
        //        list.Add(BoxDetalle);
        //        Session["BoxDetalle"] = list;
        //    }
        //    return Json("Exito", JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult GetBox(int boxd_Id)
        //{
        //    var list = db.SDP_Inv_tbBoxDetalle_Select(boxd_Id).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult UpdateBoxDetalle(tbBoxDetalle EditarBoxDetalle)
        {
            string Msj = "";
            try
            {
                IEnumerable<object> list = null;

                tbBoxDetalle vsalida = db.tbBoxDetalle.Find(EditarBoxDetalle.boxd_Id);
                list = db.UDP_Inv_tbBoxDetalle_Update(EditarBoxDetalle.boxd_Id,
                                                         EditarBoxDetalle.box_Codigo,
                                                         EditarBoxDetalle.prod_Codigo,
                                                        EditarBoxDetalle.boxd_Cantidad,
                                                        EditarBoxDetalle.boxd_UsuarioCrea,
                                                        EditarBoxDetalle.boxd_FechaCrea, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbBoxDetalle_Update_Result salida in list)
                    Msj = salida.MensajeError;

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
            return Json(Msj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveBoxDetalle(tbBoxDetalle BoxDetalle)
        {
            var list = (List<tbBoxDetalle>)Session["tbBoxDetalle"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.boxd_UsuarioCrea == BoxDetalle.boxd_UsuarioCrea);
                list.Remove(itemToRemove);
                Session["tbBoxDetalle"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarProductoSubCategoria(int? id)
        {
            try
            {
                tbBoxDetalle obj = db.tbBoxDetalle.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbBoxDetalle_Delete(id);
                foreach (UDP_Inv_tbBoxDetalle_Delete_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se puede borrar el registro");
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se puede borrar el registro");
                return RedirectToAction("Index");
            }

            //return RedirectToAction("Index");
        }

        public JsonResult GuardarBoxDetalle(tbBoxDetalle GuardarSalidas)
        {
            {
                string MsjError = "";

                try
                {
                    IEnumerable<object> list = null;
                    list = db.UDP_Inv_tbBoxDetalle_Insert(GuardarSalidas.box_Codigo,
                                                            GuardarSalidas.prod_Codigo,
                                                            GuardarSalidas.boxd_Cantidad,
                                                            Function.GetUser(), Function.DatetimeNow());

                    foreach (UDP_Inv_tbBoxDetalle_Insert_Result sal in list)
                        MsjError = (sal.MensajeError);

                    if (MsjError.Substring(0, 1) == "-1")
                    {
                        ModelState.AddModelError("", "No se Guardo el Registro");
                    }
                    else
                    {
                        return Json("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro");
                }
                return Json("Index");
            }
        }

        private void LlenarListas()
        {
            ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.Denominacion = db.tbDenominacion.ToList();
            List<tbMoneda> MonedaList = db.tbMoneda.ToList();
            ViewBag.MonedaList = new SelectList(MonedaList, "mnda_Id", "mnda_Nombre");
            ViewBag.SolicitudEdectivoDetalle = db.tbSolicitudEfectivoDetalle.ToList();
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

        public JsonResult SaveNewDetail(tbBoxDetalle BoxDetalle)
        {
            var list = (List<tbBoxDetalle>)Session["BoxDetalle"];
            var MensajeError = "0";
            var MensajeErrorDetalle = "0";
            IEnumerable<object> listBoxDetalle = null;

            try
            {
                if (list != null)
                {
                    if (list.Count != 0)
                    {
                        foreach (tbBoxDetalle Detalle in list)
                        {
                            Detalle.box_Codigo = MensajeError;
                            listBoxDetalle = db.UDP_Inv_tbBoxDetalle_Insert(
                                BoxDetalle.box_Codigo,
                                Detalle.prod_Codigo,
                                Detalle.boxd_Cantidad, Function.GetUser(), Function.DatetimeNow());
                            foreach (UDP_Inv_tbBoxDetalle_Insert_Result spDetalle in listBoxDetalle)
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
       
        public JsonResult SaveBoxDetalle(tbBoxDetalle BoxDetalle, string data_producto)
        {
            var datos = "";
            decimal cantvieja = 0;
            decimal cantnueva = 0;
            data_producto = BoxDetalle.prod_Codigo;
            decimal data_cantidad = BoxDetalle.boxd_Cantidad;
            List<tbBoxDetalle> sessionBoxDetalle = new List<tbBoxDetalle>();
            var list = (List<tbBoxDetalle>)Session["BoxDetalle"];
            if (list == null)
            {
                sessionBoxDetalle.Add(BoxDetalle);
                Session["BoxDetalle"] = sessionBoxDetalle;
            }
            else
            {
                foreach (var t in list)
                    if (t.prod_Codigo == data_producto)
                    {
                        datos = data_producto;
                        foreach (var viejo in list)
                            if (viejo.prod_Codigo == BoxDetalle.prod_Codigo)
                                cantvieja = viejo.boxd_Cantidad;
                        cantnueva = cantvieja + data_cantidad;
                        t.boxd_Cantidad = cantnueva;
                        return Json(datos, JsonRequestBehavior.AllowGet);
                    }
                list.Add(BoxDetalle);
                Session["BoxDetalle"] = list;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveBoxDetalles(tbBoxDetalle BoxDetalle)
        {
            var list = (List<tbBoxDetalle>)Session["BoxDetalle"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.prod_Codigo == BoxDetalle.prod_Codigo);
                list.Remove(itemToRemove);
                Session["BoxDetalle"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}