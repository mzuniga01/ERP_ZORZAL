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
    public class SalidaDetalleController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /SalidaDetalle/
        public ActionResult Index()
        {
            var tbsalidadetalle = db.tbSalidaDetalle.Include(t => t.tbProducto);
            return View(tbsalidadetalle.ToList());
        }

        // GET: /SalidaDetalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalidaDetalle tbSalidaDetalle = db.tbSalidaDetalle.Find(id);
            if (tbSalidaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbSalidaDetalle);
        }

        // GET: /SalidaDetalle/Create
        public ActionResult Create()
        {
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            return View();
        }

        // POST: /SalidaDetalle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="sald_Id,prod_Codigo,bod_Id,bod_ResponsableBodega,sald_UsuarioCrea,sald_FechaCrea,sald_UsuarioModifica,sald_FechaModifica")] tbSalidaDetalle tbSalidaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.tbSalidaDetalle.Add(tbSalidaDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbSalidaDetalle.prod_Codigo);
            return View(tbSalidaDetalle);
        }

        // GET: /SalidaDetalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalidaDetalle tbSalidaDetalle = db.tbSalidaDetalle.Find(id);
            if (tbSalidaDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbSalidaDetalle.prod_Codigo);
            return View(tbSalidaDetalle);
        }

        // POST: /SalidaDetalle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="sald_Id,prod_Codigo,bod_Id,bod_ResponsableBodega,sald_UsuarioCrea,sald_FechaCrea,sald_UsuarioModifica,sald_FechaModifica")] tbSalidaDetalle tbSalidaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSalidaDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbSalidaDetalle.prod_Codigo);
            return View(tbSalidaDetalle);
        }

        // GET: /SalidaDetalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalidaDetalle tbSalidaDetalle = db.tbSalidaDetalle.Find(id);
            if (tbSalidaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbSalidaDetalle);
        }

        // POST: /SalidaDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSalidaDetalle tbSalidaDetalle = db.tbSalidaDetalle.Find(id);
            db.tbSalidaDetalle.Remove(tbSalidaDetalle);
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
