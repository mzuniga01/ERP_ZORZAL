using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class EstadoFacturaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /EstadoFactura/
        public ActionResult Index()
        {
            return View(db.tbEstadoFactura.ToList());
        }

        // GET: /EstadoFactura/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoFactura tbEstadoFactura = db.tbEstadoFactura.Find(id);
            if (tbEstadoFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoFactura);
        }

        // GET: /EstadoFactura/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EstadoFactura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "esfac_Id,esfac_Descripcion,esfac_UsuarioCrea,esfac_UsuarioModifico,esfac_FechaCrea,esfac_FechaModifico")] tbEstadoFactura tbEstadoFactura)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbEstadoFactura_Insert(tbEstadoFactura.esfac_Descripcion);
                    foreach (UDP_Vent_tbEstadoFactura_Insert_Result estado in list)
                        MensajeError = estado.MensajeError;
                    if (MensajeError == -1)
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbEstadoFactura);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbEstadoFactura);
                }
            }
            return View(tbEstadoFactura);
        }

        // GET: /EstadoFactura/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoFactura tbEstadoFactura = db.tbEstadoFactura.Find(id);
            if (tbEstadoFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoFactura);
        }

        // POST: /EstadoFactura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "esfac_Id,esfac_Descripcion,esfac_UsuarioCrea,esfac_UsuarioModifico,esfac_FechaCrea,esfac_FechaModifico, tbUsuario, tbUsuario1")] tbEstadoFactura tbEstadoFactura)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //////////Aqui va la lista//////////////
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbEstadoFactura_Update(tbEstadoFactura.esfac_Id, tbEstadoFactura.esfac_Descripcion, tbEstadoFactura.esfac_UsuarioCrea, tbEstadoFactura.esfac_FechaCrea);
                    foreach (UDP_Vent_tbEstadoFactura_Update_Result estado in list)
                        MensajeError = estado.MensajeError;
                    if (MensajeError == -1)
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbEstadoFactura);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbEstadoFactura);
                }
            }
            return View(tbEstadoFactura);
        }


        // GET: /EstadoFactura/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoFactura tbEstadoFactura = db.tbEstadoFactura.Find(id);
            if (tbEstadoFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoFactura);
        }

        // POST: /EstadoFactura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbEstadoFactura tbEstadoFactura = db.tbEstadoFactura.Find(id);
            db.tbEstadoFactura.Remove(tbEstadoFactura);
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
    }
}
