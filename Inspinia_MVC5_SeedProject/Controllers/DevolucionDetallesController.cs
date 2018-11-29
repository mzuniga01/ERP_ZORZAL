using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_ZORZAL.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class DevolucionDetallesController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /DevolucionDetalles/
        public ActionResult Index()
        {
            return View(db.tbDevolucionDetalle.ToList());
        }

        // GET: /DevolucionDetalles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucionDetalle tbDevolucionDetalle = db.tbDevolucionDetalle.Find(id);
            if (tbDevolucionDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbDevolucionDetalle);
        }

        // GET: /DevolucionDetalles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DevolucionDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ddv_Codigo,dev_Codigo,prod_Codigo,ddv_CantidadProducto,ddv_Descripcion,ddv_UsuarioCrea,ddv_FechaCrea,ddv_UsuarioModifa,ddv_FechaModifa")] tbDevolucionDetalle tbDevolucionDetalle)
        {
            if (ModelState.IsValid)
            {
                db.tbDevolucionDetalle.Add(tbDevolucionDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbDevolucionDetalle);
        }

        // GET: /DevolucionDetalles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucionDetalle tbDevolucionDetalle = db.tbDevolucionDetalle.Find(id);
            if (tbDevolucionDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbDevolucionDetalle);
        }

        // POST: /DevolucionDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ddv_Codigo,dev_Codigo,prod_Codigo,ddv_CantidadProducto,ddv_Descripcion,ddv_UsuarioCrea,ddv_FechaCrea,ddv_UsuarioModifa,ddv_FechaModifa")] tbDevolucionDetalle tbDevolucionDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbDevolucionDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbDevolucionDetalle);
        }

        // GET: /DevolucionDetalles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucionDetalle tbDevolucionDetalle = db.tbDevolucionDetalle.Find(id);
            if (tbDevolucionDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbDevolucionDetalle);
        }

        // POST: /DevolucionDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbDevolucionDetalle tbDevolucionDetalle = db.tbDevolucionDetalle.Find(id);
            db.tbDevolucionDetalle.Remove(tbDevolucionDetalle);
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
