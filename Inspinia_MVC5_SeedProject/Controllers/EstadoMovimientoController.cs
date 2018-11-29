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
    public class EstadoMovimientoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /EstadoMovimiento/
        public ActionResult Index()
        {
            return View(db.tbEstadoMovimiento.ToList());
        }
        public ActionResult CrearPrueba()
        {
            return View();
        }
        public ActionResult DetallePrueba()
        {
            return View();
        }
        public ActionResult EditarPrueba()
        {
            return View();
        }

        // GET: /EstadoMovimiento/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoMovimiento tbEstadoMovimiento = db.tbEstadoMovimiento.Find(id);
            if (tbEstadoMovimiento == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoMovimiento);
        }

        // GET: /EstadoMovimiento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EstadoMovimiento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="estm_Id,estm_Descripcion,estm_UsuarioCrea,estm_FechaCrea,estm_UsuarioModifica,estm_FechaModifica")] tbEstadoMovimiento tbEstadoMovimiento)
        {
            if (ModelState.IsValid)
            {
                db.tbEstadoMovimiento.Add(tbEstadoMovimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbEstadoMovimiento);
        }

        // GET: /EstadoMovimiento/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoMovimiento tbEstadoMovimiento = db.tbEstadoMovimiento.Find(id);
            if (tbEstadoMovimiento == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoMovimiento);
        }

        // POST: /EstadoMovimiento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="estm_Id,estm_Descripcion,estm_UsuarioCrea,estm_FechaCrea,estm_UsuarioModifica,estm_FechaModifica")] tbEstadoMovimiento tbEstadoMovimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEstadoMovimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbEstadoMovimiento);
        }

        // GET: /EstadoMovimiento/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoMovimiento tbEstadoMovimiento = db.tbEstadoMovimiento.Find(id);
            if (tbEstadoMovimiento == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoMovimiento);
        }

        // POST: /EstadoMovimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbEstadoMovimiento tbEstadoMovimiento = db.tbEstadoMovimiento.Find(id);
            db.tbEstadoMovimiento.Remove(tbEstadoMovimiento);
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
