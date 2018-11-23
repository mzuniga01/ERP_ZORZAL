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
    public class tbCategoriasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbCategorias/
        public ActionResult Index()
        {
            return View(db.tbCategoria.ToList());
        }

        // GET: /tbCategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCategoria tbCategoria = db.tbCategoria.Find(id);
            if (tbCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbCategoria);
        }

        // GET: /tbCategorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Categ_IDCategoria,Categ_Descripcion")] tbCategoria tbCategoria)
        {
            if (ModelState.IsValid)
            {
                db.tbCategoria.Add(tbCategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbCategoria);
        }

        // GET: /tbCategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCategoria tbCategoria = db.tbCategoria.Find(id);
            if (tbCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbCategoria);
        }

        // POST: /tbCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Categ_IDCategoria,Categ_Descripcion")] tbCategoria tbCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbCategoria);
        }

        // GET: /tbCategorias/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCategoria tbCategoria = db.tbCategoria.Find(id);
            if (tbCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbCategoria);
        }

        // POST: /tbCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCategoria tbCategoria = db.tbCategoria.Find(id);
            db.tbCategoria.Remove(tbCategoria);
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
        /////////////////////////VISTAS DE PRUEBA 
        public ActionResult DetailsPrueba()
        {
            return View();

        }
        public ActionResult CrearPrueba()
        {
            return View();

        }
        public ActionResult EditarPrueba()
        {
            return View();

        }
    }
}
