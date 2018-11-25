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
    public class C_tbPersonasNaturalesController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /C_tbPersonasNaturales/
        public ActionResult Index()
        {
            var c_tbpersonasnaturales = db.C_tbPersonasNaturales.Include(c => c.tbClientes);
            return View(c_tbpersonasnaturales.ToList());
        }

        // GET: /C_tbPersonasNaturales/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbPersonasNaturales c_tbPersonasNaturales = db.C_tbPersonasNaturales.Find(id);
            if (c_tbPersonasNaturales == null)
            {
                return HttpNotFound();
            }
            return View(c_tbPersonasNaturales);
        }

        // GET: /C_tbPersonasNaturales/Create
        public ActionResult Create()
        {
            ViewBag.clte_RTN = new SelectList(db.tbClientes, "clte_RTN", "clte_RTN");
            return View();
        }

        // POST: /C_tbPersonasNaturales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="clte_RTN,clte_Nombre,clte_Apellido,clte_Direccion")] C_tbPersonasNaturales c_tbPersonasNaturales)
        {
            if (ModelState.IsValid)
            {
                db.C_tbPersonasNaturales.Add(c_tbPersonasNaturales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clte_RTN = new SelectList(db.tbClientes, "clte_RTN", "clte_RTN", c_tbPersonasNaturales.clte_RTN);
            return View(c_tbPersonasNaturales);
        }

        // GET: /C_tbPersonasNaturales/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbPersonasNaturales c_tbPersonasNaturales = db.C_tbPersonasNaturales.Find(id);
            if (c_tbPersonasNaturales == null)
            {
                return HttpNotFound();
            }
            ViewBag.clte_RTN = new SelectList(db.tbClientes, "clte_RTN", "clte_RTN", c_tbPersonasNaturales.clte_RTN);
            return View(c_tbPersonasNaturales);
        }

        // POST: /C_tbPersonasNaturales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="clte_RTN,clte_Nombre,clte_Apellido,clte_Direccion")] C_tbPersonasNaturales c_tbPersonasNaturales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_tbPersonasNaturales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clte_RTN = new SelectList(db.tbClientes, "clte_RTN", "clte_RTN", c_tbPersonasNaturales.clte_RTN);
            return View(c_tbPersonasNaturales);
        }

        // GET: /C_tbPersonasNaturales/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbPersonasNaturales c_tbPersonasNaturales = db.C_tbPersonasNaturales.Find(id);
            if (c_tbPersonasNaturales == null)
            {
                return HttpNotFound();
            }
            return View(c_tbPersonasNaturales);
        }

        // POST: /C_tbPersonasNaturales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            C_tbPersonasNaturales c_tbPersonasNaturales = db.C_tbPersonasNaturales.Find(id);
            db.C_tbPersonasNaturales.Remove(c_tbPersonasNaturales);
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
