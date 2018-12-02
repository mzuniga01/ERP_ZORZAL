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
    public class CuentaBancoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /CuentaBanco/
        public ActionResult Index()
        {
            var tbcuentasbanco = db.tbCuentasBanco.Include(t => t.tbBanco);
            return View(tbcuentasbanco.ToList());
        }

        // GET: /CuentaBanco/Details/5
        public ActionResult Details(short? id)
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

        // GET: /CuentaBanco/Create
        public ActionResult Create()
        {
            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre");
            return View();
        }

        // POST: /CuentaBanco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="bcta_Id,ban_Id,mnda_Id,bcta_TipoCuenta,bcta_TotalCredito,bcta_TotalDebito,bcta_FechaApertura,bcta_Numero,bcta_UsuarioCrea,bcta_FechaCrea,bcta_UsuarioModifica,bcta_FechaModifica")] tbCuentasBanco tbCuentasBanco)
        {
            if (ModelState.IsValid)
            {
                db.tbCuentasBanco.Add(tbCuentasBanco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", tbCuentasBanco.ban_Id);
            return View(tbCuentasBanco);
        }

        // GET: /CuentaBanco/Edit/5
        public ActionResult Edit(short? id)
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
            return View(tbCuentasBanco);
        }

        // POST: /CuentaBanco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="bcta_Id,ban_Id,mnda_Id,bcta_TipoCuenta,bcta_TotalCredito,bcta_TotalDebito,bcta_FechaApertura,bcta_Numero,bcta_UsuarioCrea,bcta_FechaCrea,bcta_UsuarioModifica,bcta_FechaModifica")] tbCuentasBanco tbCuentasBanco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCuentasBanco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", tbCuentasBanco.ban_Id);
            return View(tbCuentasBanco);
        }

        // GET: /CuentaBanco/Delete/5
        public ActionResult Delete(short? id)
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

        // POST: /CuentaBanco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
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
