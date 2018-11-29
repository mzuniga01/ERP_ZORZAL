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
    public class tbCajasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbCajas/
        public ActionResult Index()
        {
            var tbcaja = db.tbCaja1.Include(t => t.tbSucursal);
            return View(tbcaja.ToList());
        }

        // GET: /tbCajas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCaja tbCaja = db.tbCaja.Find(id);
            if (tbCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbCaja);
        }

        // GET: /tbCajas/Create
        public ActionResult Create()
        {
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id");
            return View();
        }

        // POST: /tbCajas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="cja_Codigo,cja_Descripcion,sucur_Codigo,cja_UsuarioCrea,cja_FechaCrea,cja_UsuarioModifica,cja_FechaModifica")] tbCaja tbCaja)
        {
            if (ModelState.IsValid)
            {
                db.tbCaja.Add(tbCaja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbCaja.cja_Descripcion);
            return View(tbCaja);
        }

        // GET: /tbCajas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCaja tbCaja = db.tbCaja.Find(id);
            if (tbCaja == null)
            {
                return HttpNotFound();
            }
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbCaja.cja_Descripcion);
            return View(tbCaja);
        }

        // POST: /tbCajas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="cja_Codigo,cja_Descripcion,sucur_Codigo,cja_UsuarioCrea,cja_FechaCrea,cja_UsuarioModifica,cja_FechaModifica")] tbCaja tbCaja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCaja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbCaja.cja_Descripcion);
            return View(tbCaja);
        }

        // GET: /tbCajas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCaja tbCaja = db.tbCaja.Find(id);
            if (tbCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbCaja);
        }

        // POST: /tbCajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbCaja tbCaja = db.tbCaja.Find(id);
            db.tbCaja.Remove(tbCaja);
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
