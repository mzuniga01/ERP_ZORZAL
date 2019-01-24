using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Transactions;

namespace ERP_ZORZAL.Controllers
{
    public class SolicitudCreditoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
     //   private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /SolicitudCredito/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("SolicitudCredito/Index"))
                {
                    try
                    {
                        /////////////////////////////CODIGO
                        var tbsolicitudcredito = db.tbSolicitudCredito.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbEstadoSolicitudCredito);
                        ViewBag.SolicitudCreditoAprobar = db.tbSolicitudCredito.ToList();
                        return View(tbsolicitudcredito.ToList());
                        ///////////////////////////////CODIGO
                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "Conexión fállida, intente de nuevo");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
            /*
            var tbsolicitudcredito = db.tbSolicitudCredito.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbEstadoSolicitudCredito);
            ViewBag.SolicitudCreditoAprobar = db.tbSolicitudCredito.ToList();
            return View(tbsolicitudcredito.ToList());*/
        }
        public ActionResult IndexSolicitud()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("SolicitudCredito/Index"))
                {
                    try
                    {
                        /////////////////////////////CODIGO
                        var tbsolicitudcredito = db.tbSolicitudCredito.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbEstadoSolicitudCredito);
                        return View(tbsolicitudcredito.ToList());
                        ///////////////////////////////CODIGO
                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "Conexión fállida, intente de nuevo");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
           /* var tbsolicitudcredito = db.tbSolicitudCredito.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbEstadoSolicitudCredito);
            return View(tbsolicitudcredito.ToList());*/
        }



        // GET: /SolicitudCredito/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.SolicitudCreditoAprobar = db.tbSolicitudCredito.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudCredito tbSolicitudCredito = db.tbSolicitudCredito.Find(id);
            if (tbSolicitudCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudCredito);
        }

        // GET: /SolicitudCredito/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("SolicitudCredito/Index"))
                {
                    try
                    {
                        /////////////////////////////CODIGO
                        //ViewBag.cred_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                        //ViewBag.cred_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                        ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion");
                        //ViewBag.bod_Nombre = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
                        ViewBag.escre_Descripcion = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion");

                        tbSolicitudCredito SolicitudCredito = new tbSolicitudCredito();
                        SolicitudCredito.escre_Id = Helpers.SolicitudPendiente;

                        ViewBag.Cliente = db.tbCliente.ToList();
                        return View();
                        ///////////////////////////////CODIGO
                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "Conexión fállida, intente de nuevo");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
            /* 
             //ViewBag.cred_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
             //ViewBag.cred_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
             ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion");
             //ViewBag.bod_Nombre = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
             ViewBag.escre_Descripcion = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion");

             tbSolicitudCredito SolicitudCredito = new tbSolicitudCredito();
             SolicitudCredito.escre_Id = Helpers.SolicitudPendiente;

             ViewBag.Cliente = db.tbCliente.ToList();
             return View();*/
        }

        // POST: /SolicitudCredito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cred_Id,clte_Id,escre_Id,cred_FechaSolicitud,cred_MontoSolicitado,cred_DiasSolicitado")] tbSolicitudCredito tbSolicitudCredito)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("SolicitudCredito/Index"))
                {
                    try
                    {
                        /////////////////////////////CODIGO
                        ViewBag.escre_Id = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion", tbSolicitudCredito.escre_Id);
                        ViewBag.Cliente = db.tbCliente.ToList();
                        if (ModelState.IsValid)
                        {
                            try
                            {


                                var MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbSolicitudCredito_Insert(
                                    tbSolicitudCredito.clte_Id,
                                    tbSolicitudCredito.escre_Id,
                                    tbSolicitudCredito.cred_FechaSolicitud,
                                           //   tbSolicitudCredito.cred_FechaAprobacion,
                                           tbSolicitudCredito.cred_MontoSolicitado,
                                    //  tbSolicitudCredito.cred_MontoAprobado,
                                    tbSolicitudCredito.cred_DiasSolicitado);
                                foreach (UDP_Vent_tbSolicitudCredito_Insert_Result SolicitudCredito in list)
                                    MensajeError = SolicitudCredito.MensajeError;
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
                                var errors = ModelState.Values.SelectMany(v => v.Errors);
                                Ex.Message.ToString();
                            }

                            return View(tbSolicitudCredito);
                        }
                        return View(tbSolicitudCredito);
                        ///////////////////////////////CODIGO
                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "Conexión fállida, intente de nuevo");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");

      /*      ViewBag.escre_Id = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion", tbSolicitudCredito.escre_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
            if (ModelState.IsValid)
            {
                try
                {


                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbSolicitudCredito_Insert(
                        tbSolicitudCredito.clte_Id,
                        tbSolicitudCredito.escre_Id,
                        tbSolicitudCredito.cred_FechaSolicitud,
                               //   tbSolicitudCredito.cred_FechaAprobacion,
                               tbSolicitudCredito.cred_MontoSolicitado,
                        //  tbSolicitudCredito.cred_MontoAprobado,
                        tbSolicitudCredito.cred_DiasSolicitado);
                    foreach (UDP_Vent_tbSolicitudCredito_Insert_Result SolicitudCredito in list)
                        MensajeError = SolicitudCredito.MensajeError;
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
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    Ex.Message.ToString();
                }

                return View(tbSolicitudCredito);
            }
            return View(tbSolicitudCredito);*/
        }
     
      // GET: /SolicitudCredito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("SolicitudCredito/Index"))
                {
                    try
                    {
                        /////////////////////////////CODIGO
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        tbSolicitudCredito tbSolicitudCredito = db.tbSolicitudCredito.Find(id);

                        //Para que sirva la redireccion*

                        ViewBag.Aprobacion = db.tbSolicitudCredito.ToList();
                        if (tbSolicitudCredito == null)
                        {
                            return HttpNotFound();
                        }
                        // ViewBag.cred_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudCredito.cred_UsuarioCrea);
                        //ViewBag.cred_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudCredito.cred_UsuarioModifica);
                        ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbSolicitudCredito.clte_Id);
                        ViewBag.escre_Descripcion = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion");
                        ViewBag.escre_Id = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion", tbSolicitudCredito.escre_Id);
                        ViewBag.Cliente = db.tbCliente.ToList();
                        ViewBag.SolicitudCreditoAprobar = db.tbSolicitudCredito.ToList();

                        return View(tbSolicitudCredito);
                        ///////////////////////////////CODIGO
                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "Conexión fállida, intente de nuevo");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
            /* if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             tbSolicitudCredito tbSolicitudCredito = db.tbSolicitudCredito.Find(id);

             //Para que sirva la redireccion*

             ViewBag.Aprobacion = db.tbSolicitudCredito.ToList();
             if (tbSolicitudCredito == null)
             {
                 return HttpNotFound();
             }
             // ViewBag.cred_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudCredito.cred_UsuarioCrea);
             //ViewBag.cred_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudCredito.cred_UsuarioModifica);
             ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbSolicitudCredito.clte_Id);
             ViewBag.escre_Descripcion = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion");
             ViewBag.escre_Id = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion", tbSolicitudCredito.escre_Id);
             ViewBag.Cliente = db.tbCliente.ToList();
             ViewBag.SolicitudCreditoAprobar = db.tbSolicitudCredito.ToList();

             return View(tbSolicitudCredito);*/
        }

        // POST: /SolicitudCredito/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

            //INICIO DEL EDIT COMENTNTADO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cred_Id,clte_Id,escre_Id,cred_FechaSolicitud,cred_FechaAprobacion,cred_MontoSolicitado,cred_MontoAprobado,cred_DiasSolicitado,cred_DiasAprobado,cred_UsuarioCrea,cred_FechaCrea,cred_UsuarioModifica,cred_FechaModifica, tbUsuario, tbUsuario1")] tbSolicitudCredito tbSolicitudCredito)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("SolicitudCredito/Index"))
                {
                    try
                    {
                        /////////////////////////////CODIGO
                        ViewBag.escre_Descripcion = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion");
                        ViewBag.Aprobacion = db.tbSolicitudCredito.ToList();
                        try
                        {
                            if (ModelState.IsValid)
                            {
                                //////////Aqui va la lista//////////////

                                var MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbSolicitudCredito_Update(tbSolicitudCredito.cred_Id,
                                    tbSolicitudCredito.clte_Id,
                                    tbSolicitudCredito.escre_Id = 1,
                                    tbSolicitudCredito.cred_FechaSolicitud,
                                    tbSolicitudCredito.cred_FechaAprobacion,
                                    tbSolicitudCredito.cred_MontoSolicitado,
                                    tbSolicitudCredito.cred_MontoAprobado,
                                    tbSolicitudCredito.cred_DiasSolicitado,
                                    tbSolicitudCredito.cred_DiasAprobado,
                                    tbSolicitudCredito.cred_UsuarioCrea,
                                    tbSolicitudCredito.cred_FechaCrea,
                                    tbSolicitudCredito.cred_UsuarioModifica,
                                    tbSolicitudCredito.cred_FechaModifica);
                                foreach (UDP_Vent_tbSolicitudCredito_Update_Result SolicitudCredito in list)
                                    MensajeError = SolicitudCredito.MensajeError;
                                if (MensajeError == "-1")
                                {
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            Ex.Message.ToString();
                            ViewBag.Cliente = db.tbCliente.ToList();
                        }


                        ViewBag.Cliente = db.tbCliente.ToList();
                        return View(tbSolicitudCredito);
                        ///////////////////////////////CODIGO
                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "Conexión fállida, intente de nuevo");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
            /*  ViewBag.escre_Descripcion = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion");
              ViewBag.Aprobacion = db.tbSolicitudCredito.ToList();
              try
              {
                  if (ModelState.IsValid)
                  {
                      //////////Aqui va la lista//////////////

                      var MensajeError = "";
                      IEnumerable<object> list = null;
                      list = db.UDP_Vent_tbSolicitudCredito_Update(tbSolicitudCredito.cred_Id,
                          tbSolicitudCredito.clte_Id,
                          tbSolicitudCredito.escre_Id = 1,
                          tbSolicitudCredito.cred_FechaSolicitud,
                          tbSolicitudCredito.cred_FechaAprobacion,
                          tbSolicitudCredito.cred_MontoSolicitado,
                          tbSolicitudCredito.cred_MontoAprobado,
                          tbSolicitudCredito.cred_DiasSolicitado,
                          tbSolicitudCredito.cred_DiasAprobado,
                          tbSolicitudCredito.cred_UsuarioCrea,
                          tbSolicitudCredito.cred_FechaCrea,
                          tbSolicitudCredito.cred_UsuarioModifica,
                          tbSolicitudCredito.cred_FechaModifica);
                      foreach (UDP_Vent_tbSolicitudCredito_Update_Result SolicitudCredito in list)
                          MensajeError = SolicitudCredito.MensajeError;
                      if (MensajeError == "-1")
                      {
                      }
                      else
                      {
                          return RedirectToAction("Index");
                      }
                  }
              }
              catch (Exception Ex)
              {
                  Ex.Message.ToString();
                  ViewBag.Cliente = db.tbCliente.ToList();
              }


              ViewBag.Cliente = db.tbCliente.ToList();
              return View(tbSolicitudCredito);*/

        }


        //FINAL DEL EDIT COMENTNTADO



        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tbSolicitudCredito).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.cred_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudCredito.cred_UsuarioCrea);
        //    ViewBag.cred_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudCredito.cred_UsuarioModifica);
        //    ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbSolicitudCredito.clte_Id);
        //    ViewBag.escre_Id = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion", tbSolicitudCredito.escre_Id);
        //    return View(tbSolicitudCredito);
        //}

        // GET: /SolicitudCredito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudCredito tbSolicitudCredito = db.tbSolicitudCredito.Find(id);
            if (tbSolicitudCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudCredito);
        }

        // POST: /SolicitudCredito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSolicitudCredito tbSolicitudCredito = db.tbSolicitudCredito.Find(id);
            db.tbSolicitudCredito.Remove(tbSolicitudCredito);
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
        public JsonResult AceptarSolicitud(int CodSolicitud, int estado)
        {
            var list = db.UDP_Vent_tbSolicitudCredito_Estado(CodSolicitud, estado).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateSolicitudCredito(tbSolicitudCredito EditSolicitudCredito)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("SolicitudCredito/Index"))
                {
                    try
                    {
                        /////////////////////////////CODIGO
                        try
                        {
                            var MensajeError = "";
                            IEnumerable<object> list = null;
                            list = db.UDP_Vent_tbSolicitudCredito_Aprobar(
                                        EditSolicitudCredito.cred_Id,
                                        EditSolicitudCredito.escre_Id,
                                        EditSolicitudCredito.cred_FechaAprobacion,
                                        EditSolicitudCredito.cred_MontoSolicitado,
                                        EditSolicitudCredito.cred_MontoAprobado,
                                        EditSolicitudCredito.cred_DiasSolicitado,
                                        EditSolicitudCredito.cred_DiasAprobado,
                                        EditSolicitudCredito.cred_UsuarioCrea,
                                        EditSolicitudCredito.cred_FechaCrea,
                                        EditSolicitudCredito.cred_UsuarioModifica,
                                        EditSolicitudCredito.cred_FechaModifica);
                            foreach (UDP_Vent_tbSolicitudCredito_Aprobar_Result SolicitudAprobada in list)
                                MensajeError = SolicitudAprobada.MensajeError;
                            if (MensajeError == "-1")
                            {
                                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                return PartialView("_AprobarSolicitudCredito");
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        catch (Exception Ex)
                        {
                            Ex.Message.ToString();
                            ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                            return PartialView("_AprobarSolicitudCredito", EditSolicitudCredito);
                        }
                        ///////////////////////////////CODIGO
                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "Conexión fállida, intente de nuevo");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
            /* try
             {
                 var MensajeError = "";
                 IEnumerable<object> list = null;
                 list = db.UDP_Vent_tbSolicitudCredito_Aprobar(
                             EditSolicitudCredito.cred_Id,
                             EditSolicitudCredito.escre_Id,
                             EditSolicitudCredito.cred_FechaAprobacion,
                             EditSolicitudCredito.cred_MontoSolicitado,
                             EditSolicitudCredito.cred_MontoAprobado,
                             EditSolicitudCredito.cred_DiasSolicitado,
                             EditSolicitudCredito.cred_DiasAprobado,
                             EditSolicitudCredito.cred_UsuarioCrea,
                             EditSolicitudCredito.cred_FechaCrea,
                             EditSolicitudCredito.cred_UsuarioModifica,
                             EditSolicitudCredito.cred_FechaModifica);
                 foreach (UDP_Vent_tbSolicitudCredito_Aprobar_Result SolicitudAprobada in list)
                     MensajeError = SolicitudAprobada.MensajeError;
                 if (MensajeError == "-1")
                     {
                         ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                         return PartialView("_AprobarSolicitudCredito");
                     }
                     else
                     {
                         return RedirectToAction("Index");
                     }
             }
             catch (Exception Ex)
             {
                 Ex.Message.ToString();
                 ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                 return PartialView("_AprobarSolicitudCredito", EditSolicitudCredito);
             }
             */
        }
        [HttpPost]
        public JsonResult DenegarSolCredito(int credID, byte Denegado)
        {
            var list = db.UDP_Vent_tbSolicitudCredito_Denegar(credID, Helpers.SolicitudDenegado).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}



