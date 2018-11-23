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
    public class tbSubcategoriasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbSubcategorias/
        public ActionResult Index()
        {
            return View(db.tbSubcategoria.ToList());
        }

        // GET: /tbSubcategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSubcategoria tbSubcategoria = db.tbSubcategoria.Find(id);
            if (tbSubcategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbSubcategoria);
        }
        public ActionResult DetallesPrueba() {
            return View();
        }
        public ActionResult EditarPrueba()
        {
            return View();
        }
        public ActionResult CrearPrueba()
        {
            return View();
        }
        // GET: /tbSubcategorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbSubcategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SubCateg_IDSubCategoria,SubCateg_IDCategoria,SubCateg_Descripcion")] tbSubcategoria tbSubcategoria)
        {
            if (ModelState.IsValid)
            {
                db.tbSubcategoria.Add(tbSubcategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbSubcategoria);
        }

        // GET: /tbSubcategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSubcategoria tbSubcategoria = db.tbSubcategoria.Find(id);
            if (tbSubcategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbSubcategoria);
        }

        // POST: /tbSubcategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SubCateg_IDSubCategoria,SubCateg_IDCategoria,SubCateg_Descripcion")] tbSubcategoria tbSubcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSubcategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbSubcategoria);
        }

        // GET: /tbSubcategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSubcategoria tbSubcategoria = db.tbSubcategoria.Find(id);
            if (tbSubcategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbSubcategoria);
        }

        // POST: /tbSubcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSubcategoria tbSubcategoria = db.tbSubcategoria.Find(id);
            db.tbSubcategoria.Remove(tbSubcategoria);
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
