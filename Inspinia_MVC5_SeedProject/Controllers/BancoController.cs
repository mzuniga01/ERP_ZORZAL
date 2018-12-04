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
    public class BancoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Banco/
        public ActionResult Index()
        {
            return View(db.tbBanco.ToList());
        }

        // GET: /Banco/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanco tbBanco = db.tbBanco.Find(id);
            if (tbBanco == null)
            {
                return HttpNotFound();
            }
            return View(tbBanco);
        }

        // GET: /Banco/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Banco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ban_Id,ban_Nombre,ban_NombreContacto,ban_TelefonoContacto,ban_UsuarioCrea,ban_FechaCrea,ban_UsuarioModifica,ban_FechaModifica")] tbBanco tbBanco)
        {
            if (ModelState.IsValid)
            {
                db.tbBanco.Add(tbBanco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbBanco);
        }

        // GET: /Banco/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanco tbBanco = db.tbBanco.Find(id);
            if (tbBanco == null)
            {
                return HttpNotFound();
            }
            return View(tbBanco);
        }

        // POST: /Banco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ban_Id,ban_Nombre,ban_NombreContacto,ban_TelefonoContacto,ban_UsuarioCrea,ban_FechaCrea,ban_UsuarioModifica,ban_FechaModifica")] tbBanco tbBanco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbBanco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbBanco);
        }

        // GET: /Banco/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanco tbBanco = db.tbBanco.Find(id);
            if (tbBanco == null)
            {
                return HttpNotFound();
            }
            return View(tbBanco);
        }

        // POST: /Banco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbBanco tbBanco = db.tbBanco.Find(id);
            db.tbBanco.Remove(tbBanco);
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
