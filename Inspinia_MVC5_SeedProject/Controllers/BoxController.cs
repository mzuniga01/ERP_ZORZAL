using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web.Mvc;
using Newtonsoft.Json;


namespace ERP_ZORZAL.Controllers
{
    public class BoxController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        public object ACTUALIZAR_tbSalidaDetalle { get; private set; }

        // GET: /Box/
        public ActionResult Index()
        {
            ViewBag.Salida = new tbSalida();
            return View(db.tbBox.ToList());
        }

        // GET: /Box/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBox tbBox = db.tbBox.Find(id);
            if (tbBox == null)
            {
                return HttpNotFound();
            }
            return View(tbBox);
        }

        // GET: /Box/Create
        public ActionResult Create()
        {
            ViewBag.ent_Id = new SelectList(db.tbEntrada, "ent_Id", "ent_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.bod_Idd = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.Producto = db.tbProducto.ToList();
            return View();

        }

        public ActionResult _Producto()
        {
            return View();
        }

        // POST: /Box/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "box_Codigo,box_Descripcion")] tbBox tbBox)
        {
            var list = (List<tbSalidaDetalle>)Session["SalidaDetalle"];
            var MensajeError = "0";
            var MensajeErrorDetalle = "0";
            IEnumerable<object> listBox = null;
            IEnumerable<object> listSalidaDetalle = null;
            if (ModelState.IsValid)
            {
                try
                {
                    using (TransactionScope Tran = new TransactionScope())
                    {
                        listBox = db.UDP_Inv_tbBox_Insert(
                                                tbBox.box_Codigo,
                                                tbBox.box_Descripcion
                                                );
                        foreach (UDP_Inv_tbBox_Insert_Result Box in listBox)
                            MensajeError = Box.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbBox);
                        }
                        else
                        {
                            if (MensajeError == tbBox.box_Codigo)
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
                                    {
                                        foreach (tbSalidaDetalle Detalle in list)
                                        {
                                            var Sal = 0;
                                            Detalle.box_Codigo = MensajeError;
                                            listSalidaDetalle = db.UDP_Inv_tbSalidaDetalle_Insert(
                                                Sal,
                                                Detalle.prod_Codigo,
                                                Detalle.sald_Cantidad,
                                                tbBox.box_Codigo
                                                );
                                            foreach (UDP_Inv_tbSalidaDetalle_Insert_Result spDetalle in listSalidaDetalle)
                                            {
                                                MensajeErrorDetalle = spDetalle.MensajeError;
                                                if (MensajeError == "-1")
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbBox);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbBox);
                            }

                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");

                    ViewBag.Denominacion = db.tbDenominacion.ToList();
                    List<tbMoneda> MonedaList = db.tbMoneda.ToList();
                    ViewBag.MonedaList = new SelectList(MonedaList, "mnda_Id", "mnda_Nombre");

                    ViewBag.SolicitudEdectivoDetalle = db.tbSolicitudEfectivoDetalle.ToList();
                }

            }

            return View(tbBox);
        }


        public JsonResult SaveNewDatail(tbSalidaDetalle SalidaDetalle)
        {
            var list = (List<tbSalidaDetalle>)Session["SalidaDetalle"];
            var MensajeError = "0";
            var MensajeErrorDetalle = "0";
            IEnumerable<object> listSalidaDetalle = null;
            if (ModelState.IsValid)
            {
                try
                {

                    var box_Codigo = "0";
                    listSalidaDetalle = db.UDP_Inv_tbSalidaDetalle_Insert(
                        SalidaDetalle.sal_Id,
                        SalidaDetalle.prod_Codigo,
                        SalidaDetalle.sald_Cantidad,
                        box_Codigo
                        );
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
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        // GET: /Box/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBox tbBox = db.tbBox.Find(id);
            if (tbBox == null)
            {
                return HttpNotFound();
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
            ViewBag.bod_Prod = db.tbBodega.Where(x => x.bod_ResponsableBodega == idUser).Select(x => x.bod_Id).SingleOrDefault();
            
            ViewBag.Producto = db.tbBodegaDetalle.ToList();

            return View(tbBox);
        }

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
        public ActionResult EditSalidaDetalle(tbSalidaDetalle data)
        {
            try
            {

                tbSalidaDetalle pSalidaDetalle = db.tbSalidaDetalle.Find(data.sald_Id);
                var MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Inv_tbSalidaDetalle_Update(data.sald_Id,
                                                    pSalidaDetalle.sal_Id,
                                                    data.prod_Codigo,
                                                    data.sald_Cantidad,
                                                    data.box_Codigo);

                foreach (UDP_Inv_tbSalidaDetalle_Update_Result RSSalidaDetalle in list)
                    MensajeError = RSSalidaDetalle.MensajeError;
                if (MensajeError == "-1")
                {
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return PartialView("_EditSalidaDetalle");
                }
                else
                {
                    return RedirectToAction("Edit", "Salida", new { @id = pSalidaDetalle.sal_Id });
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return PartialView("_EditSalidaDetalle", data);
            }
        }

        // POST: /Box/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id,[Bind(Include = "box_Codigo,box_Descripcion,box_UsuarioCrea,box_FechaCrea")] tbBox tbBox)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tbBox vBox = db.tbBox.Find(id);
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Inv_tbBox_Update(tbBox.box_Codigo, tbBox.box_Descripcion, tbBox.box_Estado, vBox.box_UsuarioCrea, vBox.box_FechaCrea);

                    foreach (UDP_Inv_tbBox_Update_Result Box in List)
                        MsjError = Box.MensajeError;

                    if (MsjError == "-1")
                    {
                        ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
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
                    ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                    return RedirectToAction("Index");
                }
            }
            else
            {
             
       var errors = ModelState.Values.SelectMany(v => v.Errors);
    }
            ViewBag.box_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbBox.box_UsuarioModifica);
            ViewBag.box_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbBox.box_UsuarioCrea);
            return View(tbBox);
        }

      
        
        // GET: /Box/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBox tbBox = db.tbBox.Find(id);
            if (tbBox == null)
            {
                return HttpNotFound();
            }
            return View(tbBox);
        }

        // POST: /Box/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbBox tbBox = db.tbBox.Find(id);
            db.tbBox.Remove(tbBox);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [HttpPost]
        public JsonResult SaveSalidaDetalle(tbSalidaDetalle SalidaDetalle)
        {
            List<tbSalidaDetalle> sessionSalidaDetalle = new List<tbSalidaDetalle>();
            var list = (List<tbSalidaDetalle>)Session["SalidaDetalle"];
            if (list == null)
            {
                sessionSalidaDetalle.Add(SalidaDetalle);
                Session["SalidaDetalle"] = sessionSalidaDetalle;
            }
            else
            {
                list.Add(SalidaDetalle);
                Session["SalidaDetalle"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetBox(int sald_Id)
        {
            var list = db.SDP_Inv_tbSalidaDetalle_Select(sald_Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateSalidaDetalle(tbSalidaDetalle EditarSalidaDetalle)
        {
            string Msj = "";
            try
            {
                IEnumerable<object> list = null;

                tbSalidaDetalle vsalida = db.tbSalidaDetalle.Find(EditarSalidaDetalle.sald_Id);
                list = db.UDP_Inv_tbSalidaDetalle_Update(EditarSalidaDetalle.sald_Id,
                                                         EditarSalidaDetalle.sald_Id, 
                                                         EditarSalidaDetalle.prod_Codigo,
                                                        EditarSalidaDetalle.sald_Cantidad, 
                                                        EditarSalidaDetalle.box_Codigo);
                foreach (UDP_Inv_tbSalidaDetalle_Update_Result salida in list)
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
        public JsonResult RemoveSalidaDetalle(tbSalidaDetalle SalidaDetalle)
        {
            var list = (List<tbSalidaDetalle>)Session["tbSalidaDetalle"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.sald_UsuarioCrea == SalidaDetalle.sald_UsuarioCrea);
                list.Remove(itemToRemove);
                Session["tbSalidaDetalle"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarProductoSubCategoria(int? id)
        {
            try
            {
                tbSalidaDetalle obj = db.tbSalidaDetalle.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbSalidaDetalle_Delete(id);
                foreach (UDP_Inv_tbSalidaDetalle_Delete_Result obje in list)
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



        public JsonResult GuardarSalidaDetalle(tbSalidaDetalle GuardarSalidas)
        {
            {
                string MsjError = "";

                try
                {
                    IEnumerable<object> list = null;
                    list = db.UDP_Inv_tbSalidaDetalle_Insert(GuardarSalidas.sal_Id,
                                                            GuardarSalidas.prod_Codigo,
                                                            GuardarSalidas.sald_Cantidad,
                                                            GuardarSalidas.box_Codigo);


                    foreach (UDP_Inv_tbSalidaDetalle_Insert_Result sal in list)
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
    }
}



