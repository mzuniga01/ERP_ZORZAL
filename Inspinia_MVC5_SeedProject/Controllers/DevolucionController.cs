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
    public class DevolucionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Devolucion/
        public ActionResult Index()
        {
            var tbdevolucion = db.tbDevolucion.Include(t => t.tbFactura).Include(t => t.tbFactura);
            return View(tbdevolucion.ToList());
        }

        // GET: /Devolucion/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            if (tbDevolucion == null)
            {
                return HttpNotFound();
            }
            return View(tbDevolucion);
        }

        // GET: /Devolucion/Create
        public ActionResult Create()
        {
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo");
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo");
            return View();
        }

        // POST: /Devolucion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="dev_Id,fact_Codigo,dev_Fecha,clte_id,dev_UsuarioCrea,dev_FechaCrea,dev_UsuarioModifica,dev_FechaModifica")] tbDevolucion tbDevolucion)
        {
            if (ModelState.IsValid)
            {
                db.tbDevolucion.Add(tbDevolucion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbDevolucion.fact_Codigo);
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbDevolucion.fact_Codigo);
            return View(tbDevolucion);
        }

        // GET: /Devolucion/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            if (tbDevolucion == null)
            {
                return HttpNotFound();
            }
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbDevolucion.fact_Codigo);
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbDevolucion.fact_Codigo);
            return View(tbDevolucion);
        }

        // POST: /Devolucion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="dev_Id,fact_Codigo,dev_Fecha,clte_id,dev_UsuarioCrea,dev_FechaCrea,dev_UsuarioModifica,dev_FechaModifica")] tbDevolucion tbDevolucion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbDevolucion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbDevolucion.fact_Codigo);
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbDevolucion.fact_Codigo);
            return View(tbDevolucion);
        }

        // GET: /Devolucion/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            if (tbDevolucion == null)
            {
                return HttpNotFound();
            }
            return View(tbDevolucion);
        }

        // POST: /Devolucion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            db.tbDevolucion.Remove(tbDevolucion);
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
