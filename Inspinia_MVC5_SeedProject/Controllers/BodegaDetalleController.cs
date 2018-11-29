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
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="bodde_Id,prod_Codigo,bod_Id,bodde_CantidadMinima,bodde_CantidadMaxima,bodde_PuntoReorden,bodde_UsuarioCrea,bodde_FechaCrea,bodde_UsuarioModifica,bodde_FechaModifica")] tbBodegaDetalle tbBodegaDetalle)
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
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="bodde_Id,prod_Codigo,bod_Id,bodde_CantidadMinima,bodde_CantidadMaxima,bodde_PuntoReorden,bodde_UsuarioCrea,bodde_FechaCrea,bodde_UsuarioModifica,bodde_FechaModifica")] tbBodegaDetalle tbBodegaDetalle)
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

        public ActionResult Crear()
        {
            return View();
        }
        public ActionResult Editar()
        {
            return View();
        }
        public ActionResult Detalles()
        {
            return View();
        }
        public ActionResult Index_BodegaDetalle()
        {
            return View();
        }

    }
}
