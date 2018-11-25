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
    public class tbExoneracionesController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbExoneraciones/
        public ActionResult Index()
        {
            return View(db.tbExoneraciones.ToList());
        }

        // GET: /tbExoneraciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbExoneraciones tbExoneraciones = db.tbExoneraciones.Find(id);
            if (tbExoneraciones == null)
            {
                return HttpNotFound();
            }
            return View(tbExoneraciones);
        }

        // GET: /tbExoneraciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbExoneraciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ex_Codigo,clte_RTN,ex_FechaEmision,ex_FechaInicial,ex_FechaFinal,ex_Imagen")] tbExoneraciones tbExoneraciones)
        {
            if (ModelState.IsValid)
            {
                db.tbExoneraciones.Add(tbExoneraciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbExoneraciones);
        }

        // GET: /tbExoneraciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbExoneraciones tbExoneraciones = db.tbExoneraciones.Find(id);
            if (tbExoneraciones == null)
            {
                return HttpNotFound();
            }
            return View(tbExoneraciones);
        }

        // POST: /tbExoneraciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ex_Codigo,clte_RTN,ex_FechaEmision,ex_FechaInicial,ex_FechaFinal,ex_Imagen")] tbExoneraciones tbExoneraciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbExoneraciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbExoneraciones);
        }

        // GET: /tbExoneraciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbExoneraciones tbExoneraciones = db.tbExoneraciones.Find(id);
            if (tbExoneraciones == null)
            {
                return HttpNotFound();
            }
            return View(tbExoneraciones);
        }

        // POST: /tbExoneraciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbExoneraciones tbExoneraciones = db.tbExoneraciones.Find(id);
            db.tbExoneraciones.Remove(tbExoneraciones);
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
