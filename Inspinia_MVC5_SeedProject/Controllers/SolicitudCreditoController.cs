using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_ZORZAL.Controllers
{
    public class SolicitudCreditoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /SolicitudCredito/
        public ActionResult Index()
        {
            var tbsolicitudcredito = db.tbSolicitudCredito.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbEstadoSolicitudCredito);
            ViewBag.SolicitudCreditoAprobar = db.tbSolicitudCredito.ToList();
            return View(tbsolicitudcredito.ToList());
        }
        public ActionResult IndexSolicitud()
        {
            var tbsolicitudcredito = db.tbSolicitudCredito.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbEstadoSolicitudCredito);
            return View(tbsolicitudcredito.ToList());
        }



        // GET: /SolicitudCredito/Details/5
        public ActionResult Details(int? id)
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

        // GET: /SolicitudCredito/Create
        public ActionResult Create()
        {
            //ViewBag.cred_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.cred_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion");
            //ViewBag.bod_Nombre = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.escre_Descripcion = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion");

            tbSolicitudCredito SolicitudCredito = new tbSolicitudCredito();
            SolicitudCredito.escre_Id = Helpers.SolicitudPendiente;

            ViewBag.Cliente = db.tbCliente.ToList();
            return View();
        }

        // POST: /SolicitudCredito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cred_Id,clte_Id,escre_Id,cred_FechaSolicitud,cred_MontoSolicitado,cred_DiasSolicitado")] tbSolicitudCredito tbSolicitudCredito)
        {
            ViewBag.escre_Id = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion", tbSolicitudCredito.escre_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
if (ModelState.IsValid)
                {
            try
            {
                
                    //db.tbTipoIdentificacion.Add(tbTipoIdentificacion);
                    //db.SaveChanges();
                    //return RedirectToAction("Index");

                    var MensajeError = 0;
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
                    if (MensajeError == -1)
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
        }
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tbSolicitudCredito.Add(tbSolicitudCredito);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.cred_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudCredito.cred_UsuarioCrea);
        //    ViewBag.cred_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudCredito.cred_UsuarioModifica);
        //    ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbSolicitudCredito.clte_Id);
        //    ViewBag.escre_Id = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion", tbSolicitudCredito.escre_Id);
        //    return View(tbSolicitudCredito);


        //}

        // GET: /SolicitudCredito/Edit/5
        public ActionResult Edit(int? id)
        {
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

            ViewBag.SolicitudCreditoAprobar = db.tbSolicitudCredito.ToList();

            return View(tbSolicitudCredito);
        }

        // POST: /SolicitudCredito/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="cred_Id,clte_Id,escre_Id,cred_FechaSolicitud,cred_FechaAprobacion,cred_MontoSolicitado,cred_MontoAprobado,cred_DiasSolicitado,cred_DiasAprobado,cred_UsuarioCrea,cred_FechaCrea,cred_UsuarioModifica,cred_FechaModifica")] tbSolicitudCredito tbSolicitudCredito)
        {
            ViewBag.escre_Descripcion = new SelectList(db.tbEstadoSolicitudCredito, "escre_Id", "escre_Descripcion");
            ViewBag.Aprobacion = db.tbSolicitudCredito.ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    //////////Aqui va la lista//////////////

                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbSolicitudCredito_Update(tbSolicitudCredito.cred_Id,
                        tbSolicitudCredito.clte_Id,
                        tbSolicitudCredito.escre_Id,
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
                    if (MensajeError == -1)
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
            }



            return View(tbSolicitudCredito);
            //if (ModelState.IsValid)
            //{
            //    db.Entry(tbEstadoSolicitudCredito).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.escre_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEstadoSolicitudCredito.escre_UsuarioCrea);
            //ViewBag.escre_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEstadoSolicitudCredito.escre_UsuarioModifica);
            //return View(tbEstadoSolicitudCredito);
        }
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
            try
            {
                var MensajeError = 0;
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
                if (MensajeError == -1)
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
        }

    }
}
