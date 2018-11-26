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
    public class tbEmpaqueCajasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbEmpaqueCajas/
        public ActionResult Index()
        {
            return View(db.tbEmpaqueCaja.ToList());
        }

        // GET: /tbEmpaqueCajas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpaqueCaja tbEmpaqueCaja = db.tbEmpaqueCaja.Find(id);
            if (tbEmpaqueCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpaqueCaja);
        }

        // GET: /tbEmpaqueCajas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbEmpaqueCajas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="EmpCaja_IDEmpaqueCaja,EmpCaja_BodegaDespacho,EmpCaja_BodegaRecibe,EmpCaja_NombreProducto,EmpCaja_Cantidad,EmpCaja_Observaciones")] tbEmpaqueCaja tbEmpaqueCaja)
        {
            if (ModelState.IsValid)
            {
                db.tbEmpaqueCaja.Add(tbEmpaqueCaja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbEmpaqueCaja);
        }

        // GET: /tbEmpaqueCajas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpaqueCaja tbEmpaqueCaja = db.tbEmpaqueCaja.Find(id);
            if (tbEmpaqueCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpaqueCaja);
        }

        // POST: /tbEmpaqueCajas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="EmpCaja_IDEmpaqueCaja,EmpCaja_BodegaDespacho,EmpCaja_BodegaRecibe,EmpCaja_NombreProducto,EmpCaja_Cantidad,EmpCaja_Observaciones")] tbEmpaqueCaja tbEmpaqueCaja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEmpaqueCaja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbEmpaqueCaja);
        }

        // GET: /tbEmpaqueCajas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpaqueCaja tbEmpaqueCaja = db.tbEmpaqueCaja.Find(id);
            if (tbEmpaqueCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpaqueCaja);
        }

        // POST: /tbEmpaqueCajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEmpaqueCaja tbEmpaqueCaja = db.tbEmpaqueCaja.Find(id);
            db.tbEmpaqueCaja.Remove(tbEmpaqueCaja);
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
        /////////////////////////VISTAS DE PRUEBA 
        public ActionResult DetailsPrueba()
        {
            return View();

        }
        public ActionResult CrearPrueba()
        {
            return View();

        }
        public ActionResult EditarPrueba()
        {
            return View();

        }
    }
}
