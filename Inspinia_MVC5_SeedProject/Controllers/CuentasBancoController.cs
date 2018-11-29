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
    public class CuentasBancoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /CuentasBanco/
        public ActionResult Index()
        {
            var tbcuentasbanco = db.tbCuentasBanco.Include(t => t.tbBanco).Include(t => t.tbMoneda);
            return View(tbcuentasbanco.ToList());
        }

        // GET: /CuentasBanco/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCuentasBanco tbCuentasBanco = db.tbCuentasBanco.Find(id);
            if (tbCuentasBanco == null)
            {
                return HttpNotFound();
            }
            return View(tbCuentasBanco);
        }

        // GET: /CuentasBanco/Create
        public ActionResult Create()
        {
            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre");
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Iso");
            return View();
        }

        // POST: /CuentasBanco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="bcta_Id,ban_Id,mnda_Id,bcta_NombreContacto,bcta_TipoCuenta,bcta_TotalCredito,bcta_TotalDebito,bcta_SaldoCuenta,bcta_FechaApertura,bcta_Num,bcta_UsuarioCrea,bcta_FechaCrea,bcta_UsuarioModifica,bcta_FechaModifica")] tbCuentasBanco tbCuentasBanco)
        {
            if (ModelState.IsValid)
            {
                db.tbCuentasBanco.Add(tbCuentasBanco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", tbCuentasBanco.ban_Id);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Iso", tbCuentasBanco.mnda_Id);
            return View(tbCuentasBanco);
        }

        // GET: /CuentasBanco/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCuentasBanco tbCuentasBanco = db.tbCuentasBanco.Find(id);
            if (tbCuentasBanco == null)
            {
                return HttpNotFound();
            }
            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", tbCuentasBanco.ban_Id);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Iso", tbCuentasBanco.mnda_Id);
            return View(tbCuentasBanco);
        }

        // POST: /CuentasBanco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="bcta_Id,ban_Id,mnda_Id,bcta_NombreContacto,bcta_TipoCuenta,bcta_TotalCredito,bcta_TotalDebito,bcta_SaldoCuenta,bcta_FechaApertura,bcta_Num,bcta_UsuarioCrea,bcta_FechaCrea,bcta_UsuarioModifica,bcta_FechaModifica")] tbCuentasBanco tbCuentasBanco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCuentasBanco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", tbCuentasBanco.ban_Id);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Iso", tbCuentasBanco.mnda_Id);
            return View(tbCuentasBanco);
        }

        // GET: /CuentasBanco/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCuentasBanco tbCuentasBanco = db.tbCuentasBanco.Find(id);
            if (tbCuentasBanco == null)
            {
                return HttpNotFound();
            }
            return View(tbCuentasBanco);
        }

        // POST: /CuentasBanco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCuentasBanco tbCuentasBanco = db.tbCuentasBanco.Find(id);
            db.tbCuentasBanco.Remove(tbCuentasBanco);
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
