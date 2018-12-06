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
    public class BodegaDetalleController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /BodegaDetalle/
        public ActionResult Index()
        {
            var tbbodegadetalle = db.tbBodegaDetalle.Include(t => t.tbBodega).Include(t => t.tbProducto);
            return View(tbbodegadetalle.ToList());
        }

        // GET: /BodegaDetalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBodegaDetalle tbBodegaDetalle = db.tbBodegaDetalle.Find(id);
            if (tbBodegaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbBodegaDetalle);
        }

        // GET: /BodegaDetalle/Create
        public ActionResult Create()
        {
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            return View();
        }

        // POST: /BodegaDetalle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="bodd_Id,prod_Codigo,bod_Id,bodd_CantidadMinima,bodd_CantidadMaxima,bodd_PuntoReorden,bodd_UsuarioCrea,bodd_FechaCrea,bodd_UsuarioModifica,bodd_FechaModifica,bodd_Costo,bodd_CostoPromedio")] tbBodegaDetalle tbBodegaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.tbBodegaDetalle.Add(tbBodegaDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbBodegaDetalle.bod_Id);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbBodegaDetalle.prod_Codigo);
            return View(tbBodegaDetalle);
        }

        // GET: /BodegaDetalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBodegaDetalle tbBodegaDetalle = db.tbBodegaDetalle.Find(id);
            if (tbBodegaDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbBodegaDetalle.bod_Id);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbBodegaDetalle.prod_Codigo);
            return View(tbBodegaDetalle);
        }

        // POST: /BodegaDetalle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="bodd_Id,prod_Codigo,bod_Id,bodd_CantidadMinima,bodd_CantidadMaxima,bodd_PuntoReorden,bodd_UsuarioCrea,bodd_FechaCrea,bodd_UsuarioModifica,bodd_FechaModifica,bodd_Costo,bodd_CostoPromedio")] tbBodegaDetalle tbBodegaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbBodegaDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbBodegaDetalle.bod_Id);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbBodegaDetalle.prod_Codigo);
            return View(tbBodegaDetalle);
        }

        // GET: /BodegaDetalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBodegaDetalle tbBodegaDetalle = db.tbBodegaDetalle.Find(id);
            if (tbBodegaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbBodegaDetalle);
        }

        // POST: /BodegaDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbBodegaDetalle tbBodegaDetalle = db.tbBodegaDetalle.Find(id);
            db.tbBodegaDetalle.Remove(tbBodegaDetalle);
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
