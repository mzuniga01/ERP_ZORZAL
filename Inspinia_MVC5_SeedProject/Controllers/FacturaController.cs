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
    public class FacturaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Factura/
        public ActionResult Index()
        {
            var tbfactura = db.tbFactura.Include(t => t.tbCaja1).Include(t => t.tbCliente).Include(t => t.tbEstadoFactura).Include(t => t.tbPago1).Include(t => t.tbSucursal);
            return View(tbfactura.ToList());
        }

        // GET: /Factura/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }

        // GET: /Factura/Create
        public ActionResult Create()
        {
            ViewBag.cja_Codigo = new SelectList(db.tbCaja, "cja_Codigo", "cja_Descripcion");
            ViewBag.clte_id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_IDT_PASSP");
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion");
            ViewBag.pago_Id = new SelectList(db.tbPago, "pago_Id", "fact_Codigo");
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id");
            return View();
        }

        // POST: /Factura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="fact_Codigo,fact_Fecha,esfac_Id,pago_Id,cja_Codigo,sucur_Codigo,clte_id,peCodigo,fact_UsuarioCrea,fact__FechaCrea,fact__UsuarioModifica,fact_FechaModica")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                db.tbFactura.Add(tbFactura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cja_Codigo = new SelectList(db.tbCaja, "cja_Codigo", "cja_Descripcion", tbFactura.cja_Codigo);
            ViewBag.clte_id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_IDT_PASSP", tbFactura.clte_id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.pago_Id = new SelectList(db.tbPago, "pago_Id", "fact_Codigo", tbFactura.pago_Id);
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbFactura.sucur_Codigo);
            return View(tbFactura);
        }

        // GET: /Factura/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            ViewBag.cja_Codigo = new SelectList(db.tbCaja, "cja_Codigo", "cja_Descripcion", tbFactura.cja_Codigo);
            ViewBag.clte_id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_IDT_PASSP", tbFactura.clte_id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.pago_Id = new SelectList(db.tbPago, "pago_Id", "fact_Codigo", tbFactura.pago_Id);
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbFactura.sucur_Codigo);
            return View(tbFactura);
        }

        // POST: /Factura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="fact_Codigo,fact_Fecha,esfac_Id,pago_Id,cja_Codigo,sucur_Codigo,clte_id,peCodigo,fact_UsuarioCrea,fact__FechaCrea,fact__UsuarioModifica,fact_FechaModica")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbFactura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cja_Codigo = new SelectList(db.tbCaja, "cja_Codigo", "cja_Descripcion", tbFactura.cja_Codigo);
            ViewBag.clte_id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_IDT_PASSP", tbFactura.clte_id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.pago_Id = new SelectList(db.tbPago, "pago_Id", "fact_Codigo", tbFactura.pago_Id);
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbFactura.sucur_Codigo);
            return View(tbFactura);
        }

        // GET: /Factura/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }

        // POST: /Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbFactura tbFactura = db.tbFactura.Find(id);
            db.tbFactura.Remove(tbFactura);
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
