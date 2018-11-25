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
    public class tbDetallesFacturasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbDetallesFacturas/
        public ActionResult Index()
        {
            var tbdetallesfactura = db.tbDetallesFactura.Include(t => t.tbFacturas);
            return View(tbdetallesfactura.ToList());
        }

        // GET: /tbDetallesFacturas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDetallesFactura tbDetallesFactura = db.tbDetallesFactura.Find(id);
            if (tbDetallesFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbDetallesFactura);
        }

        // GET: /tbDetallesFacturas/Create
        public ActionResult Create()
        {
            ViewBag.fact_Codigo = new SelectList(db.tbFacturas, "fact_Codigo", "clte_Rtn");
            return View();
        }

        // POST: /tbDetallesFacturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="fact_Codigo,prod_Codigo,dfact_Cantidad,dfact_Descuento")] tbDetallesFactura tbDetallesFactura)
        {
            if (ModelState.IsValid)
            {
                db.tbDetallesFactura.Add(tbDetallesFactura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fact_Codigo = new SelectList(db.tbFacturas, "fact_Codigo", "clte_Rtn", tbDetallesFactura.fact_Codigo);
            return View(tbDetallesFactura);
        }

        // GET: /tbDetallesFacturas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDetallesFactura tbDetallesFactura = db.tbDetallesFactura.Find(id);
            if (tbDetallesFactura == null)
            {
                return HttpNotFound();
            }
            ViewBag.fact_Codigo = new SelectList(db.tbFacturas, "fact_Codigo", "clte_Rtn", tbDetallesFactura.fact_Codigo);
            return View(tbDetallesFactura);
        }

        // POST: /tbDetallesFacturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="fact_Codigo,prod_Codigo,dfact_Cantidad,dfact_Descuento")] tbDetallesFactura tbDetallesFactura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbDetallesFactura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fact_Codigo = new SelectList(db.tbFacturas, "fact_Codigo", "clte_Rtn", tbDetallesFactura.fact_Codigo);
            return View(tbDetallesFactura);
        }

        // GET: /tbDetallesFacturas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDetallesFactura tbDetallesFactura = db.tbDetallesFactura.Find(id);
            if (tbDetallesFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbDetallesFactura);
        }

        // POST: /tbDetallesFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbDetallesFactura tbDetallesFactura = db.tbDetallesFactura.Find(id);
            db.tbDetallesFactura.Remove(tbDetallesFactura);
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
