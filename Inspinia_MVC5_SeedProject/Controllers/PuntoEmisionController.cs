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
    public class PuntoEmisionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /PuntoEmision/
        public ActionResult Index()
        {
            var tbpuntoemision = db.tbPuntoEmision.Include(t => t.tbSucursal);
            return View(tbpuntoemision.ToList());
        }

        // GET: /PuntoEmision/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPuntoEmision tbPuntoEmision = db.tbPuntoEmision.Find(id);
            if (tbPuntoEmision == null)
            {
                return HttpNotFound();
            }
            return View(tbPuntoEmision);
        }

        // GET: /PuntoEmision/Create
        public ActionResult Create()
        {
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id");
            return View();
        }

        // POST: /PuntoEmision/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="pe_Codigo,sucur_Codigo,pe_NumeroCAI,pe_UsuarioCrea,pe_FechaCrea,pe_UsuarioModifica,pe_FechaModifica")] tbPuntoEmision tbPuntoEmision)
        {
            if (ModelState.IsValid)
            {
                db.tbPuntoEmision.Add(tbPuntoEmision);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbPuntoEmision.sucur_Codigo);
            return View(tbPuntoEmision);
        }

        // GET: /PuntoEmision/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPuntoEmision tbPuntoEmision = db.tbPuntoEmision.Find(id);
            if (tbPuntoEmision == null)
            {
                return HttpNotFound();
            }
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbPuntoEmision.sucur_Codigo);
            return View(tbPuntoEmision);
        }

        // POST: /PuntoEmision/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="pe_Codigo,sucur_Codigo,pe_NumeroCAI,pe_UsuarioCrea,pe_FechaCrea,pe_UsuarioModifica,pe_FechaModifica")] tbPuntoEmision tbPuntoEmision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPuntoEmision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbPuntoEmision.sucur_Codigo);
            return View(tbPuntoEmision);
        }

        // GET: /PuntoEmision/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPuntoEmision tbPuntoEmision = db.tbPuntoEmision.Find(id);
            if (tbPuntoEmision == null)
            {
                return HttpNotFound();
            }
            return View(tbPuntoEmision);
        }

        // POST: /PuntoEmision/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbPuntoEmision tbPuntoEmision = db.tbPuntoEmision.Find(id);
            db.tbPuntoEmision.Remove(tbPuntoEmision);
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
