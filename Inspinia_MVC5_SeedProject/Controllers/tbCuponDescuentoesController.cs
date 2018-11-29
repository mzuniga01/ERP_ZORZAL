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
    public class tbCuponDescuentoesController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbCuponDescuentoes/
        public ActionResult Index()
        {
            var tbcupondescuento = db.tbCuponDescuento.Include(t => t.tbSucursal);
            return View(tbcupondescuento.ToList());
        }

        // GET: /tbCuponDescuentoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCuponDescuento tbCuponDescuento = db.tbCuponDescuento.Find(id);
            if (tbCuponDescuento == null)
            {
                return HttpNotFound();
            }
            return View(tbCuponDescuento);
        }

        // GET: /tbCuponDescuentoes/Create
        public ActionResult Create()
        {
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id");
            return View();
        }

        // POST: /tbCuponDescuentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="cd_IDCuponDescuento,sucur_Codigo,cd_FechaEmision,cd_FechaVencimiento,cd_PorcentajeDescuento,cd_MontoDescuento,cd_UsuarioCrea,cd_FechaCrea,cd_UsuarioModifa,cd_FechaModifa")] tbCuponDescuento tbCuponDescuento)
        {
            if (ModelState.IsValid)
            {
                db.tbCuponDescuento.Add(tbCuponDescuento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbCuponDescuento.sucur_Codigo);
            return View(tbCuponDescuento);
        }

        // GET: /tbCuponDescuentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCuponDescuento tbCuponDescuento = db.tbCuponDescuento.Find(id);
            if (tbCuponDescuento == null)
            {
                return HttpNotFound();
            }
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbCuponDescuento.sucur_Codigo);
            return View(tbCuponDescuento);
        }

        // POST: /tbCuponDescuentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="cd_IDCuponDescuento,sucur_Codigo,cd_FechaEmision,cd_FechaVencimiento,cd_PorcentajeDescuento,cd_MontoDescuento,cd_UsuarioCrea,cd_FechaCrea,cd_UsuarioModifa,cd_FechaModifa")] tbCuponDescuento tbCuponDescuento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCuponDescuento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbCuponDescuento.sucur_Codigo);
            return View(tbCuponDescuento);
        }

        // GET: /tbCuponDescuentoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCuponDescuento tbCuponDescuento = db.tbCuponDescuento.Find(id);
            if (tbCuponDescuento == null)
            {
                return HttpNotFound();
            }
            return View(tbCuponDescuento);
        }

        // POST: /tbCuponDescuentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCuponDescuento tbCuponDescuento = db.tbCuponDescuento.Find(id);
            db.tbCuponDescuento.Remove(tbCuponDescuento);
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
