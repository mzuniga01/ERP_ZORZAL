using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class EstadoSolicitudCreditoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /EstadoSolicitudCredito/
        public ActionResult Index()
        {
            return View(db.tbEstadoSolicitudCredito.ToList());
        }

        // GET: /EstadoSolicitudCredito/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoSolicitudCredito tbEstadoSolicitudCredito = db.tbEstadoSolicitudCredito.Find(id);
            if (tbEstadoSolicitudCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoSolicitudCredito);
        }

        // GET: /EstadoSolicitudCredito/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EstadoSolicitudCredito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="escre_Id,escre_Descripcion,escre_UsuarioCrea,escre_UsuarioModifico,escre_FechaAgrego,escre_FechaModifico")] tbEstadoSolicitudCredito tbEstadoSolicitudCredito)
        {
            if (ModelState.IsValid)
            {
                db.tbEstadoSolicitudCredito.Add(tbEstadoSolicitudCredito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbEstadoSolicitudCredito);
        }

        // GET: /EstadoSolicitudCredito/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoSolicitudCredito tbEstadoSolicitudCredito = db.tbEstadoSolicitudCredito.Find(id);
            if (tbEstadoSolicitudCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoSolicitudCredito);
        }

        // POST: /EstadoSolicitudCredito/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="escre_Id,escre_Descripcion,escre_UsuarioCrea,escre_UsuarioModifico,escre_FechaAgrego,escre_FechaModifico")] tbEstadoSolicitudCredito tbEstadoSolicitudCredito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEstadoSolicitudCredito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbEstadoSolicitudCredito);
        }

        // GET: /EstadoSolicitudCredito/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoSolicitudCredito tbEstadoSolicitudCredito = db.tbEstadoSolicitudCredito.Find(id);
            if (tbEstadoSolicitudCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoSolicitudCredito);
        }

        // POST: /EstadoSolicitudCredito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbEstadoSolicitudCredito tbEstadoSolicitudCredito = db.tbEstadoSolicitudCredito.Find(id);
            db.tbEstadoSolicitudCredito.Remove(tbEstadoSolicitudCredito);
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
