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
    public class tbCajasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbCajas/
        public ActionResult Index()
        {
            return View(db.tbCajas.ToList());
        }

        // GET: /tbCajas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCajas tbCajas = db.tbCajas.Find(id);
            if (tbCajas == null)
            {
                return HttpNotFound();
            }
            return View(tbCajas);
        }

        // GET: /tbCajas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbCajas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ca_CodCaja,ca_Descripcion,ca_CodSucursal")] tbCajas tbCajas)
        {
            if (ModelState.IsValid)
            {
                db.tbCajas.Add(tbCajas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbCajas);
        }

        // GET: /tbCajas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCajas tbCajas = db.tbCajas.Find(id);
            if (tbCajas == null)
            {
                return HttpNotFound();
            }
            return View(tbCajas);
        }

        // POST: /tbCajas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ca_CodCaja,ca_Descripcion,ca_CodSucursal")] tbCajas tbCajas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCajas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbCajas);
        }

        // GET: /tbCajas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCajas tbCajas = db.tbCajas.Find(id);
            if (tbCajas == null)
            {
                return HttpNotFound();
            }
            return View(tbCajas);
        }

        // POST: /tbCajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbCajas tbCajas = db.tbCajas.Find(id);
            db.tbCajas.Remove(tbCajas);
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
