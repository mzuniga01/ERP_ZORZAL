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
    public class PagoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Pago/
        public ActionResult Index()
        {
            var tbpago = db.tbPago.Include(t => t.tbFactura).Include(t => t.tbTipoPago);
            return View(tbpago.ToList());
        }

        // GET: /Pago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPago tbPago = db.tbPago.Find(id);
            if (tbPago == null)
            {
                return HttpNotFound();
            }
            return View(tbPago);
        }

        // GET: /Pago/Create
        public ActionResult Create()
        {
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo");
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion");
            return View();
        }

        // POST: /Pago/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="pago_Id,fact_Codigo,tpa_Id,pago_Totalpagos,pago_Totalcambio,pago_Emisor,pago_Cuenta,pago_FechaVencimiento,pago_Titular_,pago_UsuarioCrea,pago_FechaCrea,pago_UsuarioModifica,pago_FechaModifica")] tbPago tbPago)
        {
            if (ModelState.IsValid)
            {
                db.tbPago.Add(tbPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbPago.fact_Codigo);
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion", tbPago.tpa_Id);
            return View(tbPago);
        }

        // GET: /Pago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPago tbPago = db.tbPago.Find(id);
            if (tbPago == null)
            {
                return HttpNotFound();
            }
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbPago.fact_Codigo);
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion", tbPago.tpa_Id);
            return View(tbPago);
        }

        // POST: /Pago/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="pago_Id,fact_Codigo,tpa_Id,pago_Totalpagos,pago_Totalcambio,pago_Emisor,pago_Cuenta,pago_FechaVencimiento,pago_Titular_,pago_UsuarioCrea,pago_FechaCrea,pago_UsuarioModifica,pago_FechaModifica")] tbPago tbPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbPago.fact_Codigo);
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion", tbPago.tpa_Id);
            return View(tbPago);
        }

        // GET: /Pago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPago tbPago = db.tbPago.Find(id);
            if (tbPago == null)
            {
                return HttpNotFound();
            }
            return View(tbPago);
        }

        // POST: /Pago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPago tbPago = db.tbPago.Find(id);
            db.tbPago.Remove(tbPago);
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
