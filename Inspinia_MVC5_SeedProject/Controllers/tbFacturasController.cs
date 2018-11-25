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
    public class tbFacturasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbFacturas/
        public ActionResult Index()
        {
            var tbfacturas = db.tbFacturas.Include(t => t.tbDetallesFactura);
            return View(tbfacturas.ToList());
        }

        // GET: /tbFacturas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFacturas tbFacturas = db.tbFacturas.Find(id);
            if (tbFacturas == null)
            {
                return HttpNotFound();
            }
            return View(tbFacturas);
        }

        // GET: /tbFacturas/Create
        public ActionResult Create()
        {
            ViewBag.fact_Codigo = new SelectList(db.tbDetallesFactura, "fact_Codigo", "prod_Codigo");
            return View();
        }

        // POST: /tbFacturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="fact_Codigo,clte_Rtn,tpago_Codigo,fpago_Codigo,scs_Codigo,Usuario_Nombre,tcam_Codigo,fact_Fecha")] tbFacturas tbFacturas)
        {
            if (ModelState.IsValid)
            {
                db.tbFacturas.Add(tbFacturas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fact_Codigo = new SelectList(db.tbDetallesFactura, "fact_Codigo", "prod_Codigo", tbFacturas.fact_Codigo);
            return View(tbFacturas);
        }

        // GET: /tbFacturas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFacturas tbFacturas = db.tbFacturas.Find(id);
            if (tbFacturas == null)
            {
                return HttpNotFound();
            }
            ViewBag.fact_Codigo = new SelectList(db.tbDetallesFactura, "fact_Codigo", "prod_Codigo", tbFacturas.fact_Codigo);
            return View(tbFacturas);
        }

        // POST: /tbFacturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="fact_Codigo,clte_Rtn,tpago_Codigo,fpago_Codigo,scs_Codigo,Usuario_Nombre,tcam_Codigo,fact_Fecha")] tbFacturas tbFacturas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbFacturas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fact_Codigo = new SelectList(db.tbDetallesFactura, "fact_Codigo", "prod_Codigo", tbFacturas.fact_Codigo);
            return View(tbFacturas);
        }

        // GET: /tbFacturas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFacturas tbFacturas = db.tbFacturas.Find(id);
            if (tbFacturas == null)
            {
                return HttpNotFound();
            }
            return View(tbFacturas);
        }

        // POST: /tbFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbFacturas tbFacturas = db.tbFacturas.Find(id);
            db.tbFacturas.Remove(tbFacturas);
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
