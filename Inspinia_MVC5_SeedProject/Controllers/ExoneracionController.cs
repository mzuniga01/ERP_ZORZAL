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
    public class ExoneracionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        public ActionResult ClientesnoExonerado()
        {

            return View(db.UDP_Vent_listExoneracion_Select);
        }


        // GET: /Exoneracion/
        public ActionResult Index()
        {
            var tbexoneracion = db.tbExoneracion.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente);

            return View(tbexoneracion.ToList());
        }

        // GET: /Exoneracion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbExoneracion tbExoneracion = db.tbExoneracion.Find(id);
            if (tbExoneracion == null)
            {
                return HttpNotFound();
            }
            return View(tbExoneracion);
        }

        // GET: /Exoneracion/Create
        public ActionResult Create()
        {
            //ViewBag.exo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.exo_UsuarioModifa = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte");
            //return View();
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
            return View();
        }

        // POST: /Exoneracion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="exo_Id,exo_Documento,exo_ExoneracionActiva,exo_FechaInicialVigencia,exo_FechaIFinalVigencia,clte_Id,exo_UsuarioCrea,exo_FechaCrea,exo_UsuarioModifa,exo_FechaModifica")] tbExoneracion tbExoneracion)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbExoneracion_Insert(tbExoneracion.exo_Documento,
                                                            tbExoneracion.exo_ExoneracionActiva,
                                                            tbExoneracion.exo_FechaInicialVigencia,
                                                            tbExoneracion.exo_FechaIFinalVigencia,
                                                            tbExoneracion.clte_Id);
                    foreach (UDP_Vent_tbExoneracion_Insert_Result Exoneracion in list)
                        MensajeError = Exoneracion.MensajeError;
                    if (MensajeError == -1)
                    {
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                    db.tbExoneracion.Add(tbExoneracion);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", "Error al Agregar Registro " + Ex.Message.ToString());
                ViewBag.Cliente = db.tbCliente.ToList();
                ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                return View(tbExoneracion);
            }

            ViewBag.exo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioCrea);
            ViewBag.exo_UsuarioModifa = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioModifa);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
            return View(tbExoneracion);
        }

        // GET: /Exoneracion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbExoneracion tbExoneracion = db.tbExoneracion.Find(id);
            if (tbExoneracion == null)
            {
                return HttpNotFound();
            }
            ViewBag.exo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioCrea);
            ViewBag.exo_UsuarioModifa = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioModifa);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
            return View(tbExoneracion);
            
            //return View();
        }

        // POST: /Exoneracion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id,[Bind(Include= "exo_Id,exo_Documento,exo_ExoneracionActiva,exo_FechaInicialVigencia,exo_FechaIFinalVigencia,clte_Id,exo_UsuarioCrea,exo_FechaCrea")] tbExoneracion tbExoneracion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tbExoneracion pExoneracion = db.tbExoneracion.Find(id);
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbExoneracion_Update(tbExoneracion.exo_Id,
                                                            tbExoneracion.exo_Documento,
                                                            tbExoneracion.exo_ExoneracionActiva,
                                                            tbExoneracion.exo_FechaInicialVigencia,
                                                            tbExoneracion.exo_FechaIFinalVigencia,
                                                            tbExoneracion.clte_Id,
                                                            pExoneracion.exo_UsuarioCrea,
                                                            pExoneracion.exo_FechaCrea);
                    foreach (UDP_Vent_tbExoneracion_Update_Result Exoneracion in list)
                        MensajeError = Exoneracion.MensajeError;
                    if (MensajeError == -1)
                    {
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                    
                    return RedirectToAction("Index");

                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", "Error al Agregar Registro " + Ex.Message.ToString());
                ViewBag.Cliente = db.tbCliente.ToList();
                ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                return View(tbExoneracion);
            }
            ViewBag.exo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioCrea);
            ViewBag.exo_UsuarioModifa = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioModifa);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
            return View(tbExoneracion);
        }

        // GET: /Exoneracion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbExoneracion tbExoneracion = db.tbExoneracion.Find(id);
            if (tbExoneracion == null)
            {
                return HttpNotFound();
            }
            return View(tbExoneracion);
        }

        // POST: /Exoneracion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbExoneracion tbExoneracion = db.tbExoneracion.Find(id);
            db.tbExoneracion.Remove(tbExoneracion);
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
        public JsonResult InactivarCliente(int CodExoneracion, bool Activo)
        {
            var list = db.UDP_Vent_tbExoneracion_Estado(CodExoneracion, Helpers.ClienteInactivo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActivarCliente(int CodExoneracion, bool Activo)
        {
            var list = db.UDP_Vent_tbExoneracion_Estado(CodExoneracion, Helpers.ClienteActivo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
