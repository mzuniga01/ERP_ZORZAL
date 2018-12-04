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
    public class PuntoEmisionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /PuntoEmision/
        public ActionResult Index()
        {
            var tbpuntoemision = db.tbPuntoEmision.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbpuntoemision.ToList());
        }

        // GET: /PuntoEmision/Details/5
        public ActionResult Details(int? id)
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
            ViewBag.pemi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.pemi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: /PuntoEmision/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="pemi_Id,pemi_NumeroCAI,pemi_UsuarioCrea,pemi_FechaCrea,pemi_UsuarioModifica,pemi_FechaModifica")] tbPuntoEmision tbPuntoEmision)
        {
            if (ModelState.IsValid)
            {
                db.tbPuntoEmision.Add(tbPuntoEmision);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pemi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmision.pemi_UsuarioCrea);
            ViewBag.pemi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmision.pemi_UsuarioModifica);
            return View(tbPuntoEmision);
        }

        // GET: /PuntoEmision/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.pemi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmision.pemi_UsuarioCrea);
            ViewBag.pemi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmision.pemi_UsuarioModifica);
            return View(tbPuntoEmision);
        }

        // POST: /PuntoEmision/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="pemi_Id,pemi_NumeroCAI,pemi_UsuarioCrea,pemi_FechaCrea,pemi_UsuarioModifica,pemi_FechaModifica")] tbPuntoEmision tbPuntoEmision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPuntoEmision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pemi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmision.pemi_UsuarioCrea);
            ViewBag.pemi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmision.pemi_UsuarioModifica);
            return View(tbPuntoEmision);
        }

        // GET: /PuntoEmision/Delete/5
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
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
