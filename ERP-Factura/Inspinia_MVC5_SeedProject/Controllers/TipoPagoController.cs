using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_ZORZAL.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class TipoPagoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /TipoPago/
        public ActionResult Index()
        {
            return View(db.tbTipoPago.ToList());
        }

        // GET: /TipoPago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
            if (tbTipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoPago);
        }

        // GET: /TipoPago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TipoPago/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tpa_Id,tpa_Descripcion,tpa_Emisor,tpa_Cuenta,tpa_FechaVencimiento,tpa_Titular,tpa_UsuarioCrea,tpa_FechaCrea,tpa_UsuarioModifica,tpa_FechaModifica")] tbTipoPago tbTipoPago)
        {
            if (ModelState.IsValid)
            {
                db.tbTipoPago.Add(tbTipoPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTipoPago);
        }

        // GET: /TipoPago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
            if (tbTipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoPago);
        }

        // POST: /TipoPago/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="tpa_Id,tpa_Descripcion,tpa_Emisor,tpa_Cuenta,tpa_FechaVencimiento,tpa_Titular,tpa_UsuarioCrea,tpa_FechaCrea,tpa_UsuarioModifica,tpa_FechaModifica")] tbTipoPago tbTipoPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTipoPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTipoPago);
        }

        // GET: /TipoPago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
            if (tbTipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoPago);
        }

        // POST: /TipoPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
            db.tbTipoPago.Remove(tbTipoPago);
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
