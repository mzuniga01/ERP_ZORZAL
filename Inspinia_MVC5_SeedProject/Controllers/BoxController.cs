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
                                                Detalle.bodd_Id,
                                                Detalle.sal_Cantidad,
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
            return View(tbBox);
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

        //public JsonResult UpdateSalidaDetalle(tbSalidaDetalle ACTUALIZAR_tbSalidaDetalle)
        //{
        //    string Msj = "";
        //    try
        //    {
        //        tbBox vBox = db.tbBox.Find(ACTUALIZAR_tbSalidaDetalle.box_Codigo);
        //        IEnumerable<object> list = null;
        //            list = db.UDP_Inv_tbSalidaDetalle_Update(ACTUALIZAR_tbSalidaDetalle.sald_Id,
        //                                                ACTUALIZAR_tbSalidaDetalle.sal_Id,
        //                                                 ACTUALIZAR_tbSalidaDetalle.bodd_Id,
        //                                                 ACTUALIZAR_tbSalidaDetalle.sal_Cantidad,
        //                                                 vBox.box_UsuarioCrea,
        //                                                 vBox.box_FechaCrea
        //                                                 );
        //        foreach (UDP_Inv_tbSalidaDetalle_Update_Result Isd in list)
        //            Msj = Isd.MensajeError;

        //        if (Msj.Substring(0, 2) == "-1")
        //        {
        //            ModelState.AddModelError("", "No se Actualizo el registro");


        //        }
        //        else
        //        {
        //            //return View("Edit/" + bod_Id);
        //            return Json("Index");
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Ex.Message.ToString();
        //        ModelState.AddModelError("", "No se Actualizo el registro");
        //    }
        //    return Json("Index");
        //}
        
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
                tbSalidaDetalle vSalidaDetalle = db.tbSalidaDetalle.Find(EditarSalidaDetalle.sald_Id);
                list = db.UDP_Inv_tbSalidaDetalle_Update(
                                                         EditarSalidaDetalle.sald_Id,
                                                        EditarSalidaDetalle.sal_Id,
                                                        EditarSalidaDetalle.bodd_Id,
                                                        EditarSalidaDetalle.sal_Cantidad,
                                                        EditarSalidaDetalle.box_Codigo,
                                                        vSalidaDetalle.sald_UsuarioCrea,
                                                        vSalidaDetalle.sald_FechaCrea
                    );
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
        //public ActionResult EliminarModal(int sal_Id)
        //{
        //    try
        //    {
        //        tbSalidaDetalle obj = db.tbSalidaDetalle.Find(sal_Id);
        //        IEnumerable<object> list = null;
        //        var MsjError = ""; list = db.UDP_Gral_tbMunicipio_Delete(sal_Id);
        //        foreach (UDP_Gral_tbMunicipio_Delete_Result mun in list)
        //            MsjError = mun.MensajeError;
        //        if (MsjError.StartsWith("-1The DELETE statement conflicted with the REFERENCE constraint"))
        //        {
        //            TempData["smserror"] = " No se puede eliminar el dato porque tiene dependencia."; ViewBag.smserror = TempData["smserror"];
        //            ModelState.AddModelError("", "No se puede borrar el registro");
        //            return RedirectToAction("Edit/" + dep_Codigo);
        //        }
        //        else
        //        {
        //            return RedirectToAction("Edit/" + dep_Codigo);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Ex.Message.ToString();
        //        ModelState.AddModelError("", "No se Actualizo el registro");
        //        return RedirectToAction("Edit/" + dep_Codigo);
        //    }

            //return RedirectToAction("Index");
        }

    }
//}

