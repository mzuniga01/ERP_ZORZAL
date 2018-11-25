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
    public class tbPagosController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbPagos/
        public ActionResult Index()
        {
            return View(db.tbPagos.ToList());
        }

        // GET: /tbPagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPagos tbPagos = db.tbPagos.Find(id);
            if (tbPagos == null)
            {
                return HttpNotFound();
            }
            return View(tbPagos);
        }

        // GET: /tbPagos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbPagos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="pgCodPago,fact_Codigo,pgFechaPago,pgMonto,tcam_Codigo")] tbPagos tbPagos)
        {
            if (ModelState.IsValid)
            {
                db.tbPagos.Add(tbPagos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbPagos);
        }

        // GET: /tbPagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPagos tbPagos = db.tbPagos.Find(id);
            if (tbPagos == null)
            {
                return HttpNotFound();
            }
            return View(tbPagos);
        }

        // POST: /tbPagos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="pgCodPago,fact_Codigo,pgFechaPago,pgMonto,tcam_Codigo")] tbPagos tbPagos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPagos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbPagos);
        }

        // GET: /tbPagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPagos tbPagos = db.tbPagos.Find(id);
            if (tbPagos == null)
            {
                return HttpNotFound();
            }
            return View(tbPagos);
        }

        // POST: /tbPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPagos tbPagos = db.tbPagos.Find(id);
            db.tbPagos.Remove(tbPagos);
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
