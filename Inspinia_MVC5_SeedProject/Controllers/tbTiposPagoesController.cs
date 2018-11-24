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
    public class tbTiposPagoesController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbTiposPagoes/
        public ActionResult Index()
        {
            return View(db.tbTiposPago.ToList());
        }

        // GET: /tbTiposPagoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposPago tbTiposPago = db.tbTiposPago.Find(id);
            if (tbTiposPago == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposPago);
        }

        // GET: /tbTiposPagoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbTiposPagoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tpago_Codigo,tpago_Descripcion")] tbTiposPago tbTiposPago)
        {
            if (ModelState.IsValid)
            {
                db.tbTiposPago.Add(tbTiposPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTiposPago);
        }

        // GET: /tbTiposPagoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposPago tbTiposPago = db.tbTiposPago.Find(id);
            if (tbTiposPago == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposPago);
        }

        // POST: /tbTiposPagoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="tpago_Codigo,tpago_Descripcion")] tbTiposPago tbTiposPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTiposPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTiposPago);
        }

        // GET: /tbTiposPagoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposPago tbTiposPago = db.tbTiposPago.Find(id);
            if (tbTiposPago == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposPago);
        }

        // POST: /tbTiposPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTiposPago tbTiposPago = db.tbTiposPago.Find(id);
            db.tbTiposPago.Remove(tbTiposPago);
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
