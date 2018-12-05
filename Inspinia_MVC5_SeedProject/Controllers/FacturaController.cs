using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_ZORZAL.Models;

namespace ERP_ZORZAL.Controllers
{
    public class FacturaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Factura/
        public ActionResult Index()
        {
            return View(db.tbFactura.ToList());
        }

        // GET: /Factura/Details/5
        public ActionResult Details(long? id)
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
            //ERP_ZORZALEntities dc = new ERP_ZORZALEntities();
            //var item = dc.tbFacturaDetalle.ToList();
            //tbFactura Factura = new tbFactura();
            //ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
            //ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion");
            //ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");
            ViewBag.Producto = db.tbProducto.ToList();

            return View();
        }

        // POST: /Factura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="fact_Id,fact_Codigo,fact_Fecha,esfac_Id,cja_Id,suc_Id,clte_Id,pemi_NumeroCAI,fact_AlCredito,fact_DiasCredito,fact_PorcentajeDescuento,fact_AutorizarDescuento,fact_Vendedor,clte_RTN_Identidad_Pasaporte,clte_Nombres,fact_UsuarioCrea,fact__FechaCrea,fact__UsuarioModifica,fact_FechaModifica")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                db.tbFactura.Add(tbFactura);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion");
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");
            return View(tbFactura);
        }

        //Get: Sucursal/Create




        // GET: /Factura/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            ViewBag.Producto = db.tbProducto.ToList();
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }

        // POST: /Factura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="fact_Id,fact_Codigo,fact_Fecha,esfac_Id,cja_Id,suc_Id,clte_Id,pemi_NumeroCAI,fact_AlCredito,fact_DiasCredito,fact_PorcentajeDescuento,fact_AutorizarDescuento,fact_Vendedor,clte_RTN_Identidad_Pasaporte,clte_Nombres,fact_UsuarioCrea,fact__FechaCrea,fact__UsuarioModifica,fact_FechaModifica")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbFactura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbFactura);
        }

        // GET: /Factura/Delete/5
        public ActionResult Delete(long? id)
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
        public ActionResult DeleteConfirmed(long id)
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
