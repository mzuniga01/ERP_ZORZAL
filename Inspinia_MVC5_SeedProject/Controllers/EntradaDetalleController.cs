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
    public class EntradaDetalleController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /EntradaDetalle/
        public ActionResult Index()
        {
            //var tbentradadetalle = db.tbEntradaDetalle.Include(t => t.tbProducto);
            //return View(tbentradadetalle.ToList());
            return View();
        }

        // GET: /EntradaDetalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntradaDetalle tbEntradaDetalle = db.tbEntradaDetalle.Find(id);
            if (tbEntradaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbEntradaDetalle);
        }

        // GET: /EntradaDetalle/Create
        public ActionResult Create()
        {
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            return View();
        }

        // POST: /EntradaDetalle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "entd_Codigo,ent_Codigo,prod_Codigo,entd_Cantidad,entd_UsuarioCrea,entd_FechaCrea,entd_UsuarioModifica,entd_FechaModifica")] tbEntradaDetalle tbEntradaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.tbEntradaDetalle.Add(tbEntradaDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbEntradaDetalle.prod_Codigo);
            return View(tbEntradaDetalle);
        }

        // GET: /EntradaDetalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntradaDetalle tbEntradaDetalle = db.tbEntradaDetalle.Find(id);
            if (tbEntradaDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbEntradaDetalle.prod_Codigo);
            return View(tbEntradaDetalle);
        }

        // POST: /EntradaDetalle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "entd_Codigo,ent_Codigo,prod_Codigo,entd_Cantidad,entd_UsuarioCrea,entd_FechaCrea,entd_UsuarioModifica,entd_FechaModifica")] tbEntradaDetalle tbEntradaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEntradaDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbEntradaDetalle.prod_Codigo);
            return View(tbEntradaDetalle);
        }

        // GET: /EntradaDetalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntradaDetalle tbEntradaDetalle = db.tbEntradaDetalle.Find(id);
            if (tbEntradaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbEntradaDetalle);
        }

        // POST: /EntradaDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEntradaDetalle tbEntradaDetalle = db.tbEntradaDetalle.Find(id);
            db.tbEntradaDetalle.Remove(tbEntradaDetalle);
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
        public ActionResult DetallePrueba()
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
