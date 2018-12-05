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
    public class ArqueoCajaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /ArqueoCaja/
        public ActionResult Index()
        {
            var tbarqueocaja = db.tbArqueoCaja.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCaja);
            return View(tbarqueocaja.ToList());
        }

        // GET: /ArqueoCaja/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbArqueoCaja tbArqueoCaja = db.tbArqueoCaja.Find(id);
            if (tbArqueoCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbArqueoCaja);
        }

        // GET: /ArqueoCaja/Create
        public ActionResult Create()
        {
            ViewBag.aqcja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.aqcja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
            return View();
        }

        // POST: /ArqueoCaja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="aqcja_Id,cja_Id,aqcja_Fecha,aqcja_FechaInicio,aqcja_FechaFin,aqcja_SaldoInicial,aqcja_SaldoFinal,aqcja_MontoEfectivo,aqcja_MontoCheque,aqcja_MontoTCoTD,aqcja_MontoNotaCredito,aqcja_MontoCupon,aqcja_UsuarioCrea,aqcja_FechaCrea,aqcja_UsuarioModifica,aqcja_FechaModifica")] tbArqueoCaja tbArqueoCaja)
        {
            if (ModelState.IsValid)
            {
                db.tbArqueoCaja.Add(tbArqueoCaja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.aqcja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbArqueoCaja.aqcja_UsuarioCrea);
            ViewBag.aqcja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbArqueoCaja.aqcja_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbArqueoCaja.cja_Id);
            return View(tbArqueoCaja);
        }

        // GET: /ArqueoCaja/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbArqueoCaja tbArqueoCaja = db.tbArqueoCaja.Find(id);
            if (tbArqueoCaja == null)
            {
                return HttpNotFound();
            }
            ViewBag.aqcja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbArqueoCaja.aqcja_UsuarioCrea);
            ViewBag.aqcja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbArqueoCaja.aqcja_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbArqueoCaja.cja_Id);
            return View(tbArqueoCaja);
        }

        // POST: /ArqueoCaja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="aqcja_Id,cja_Id,aqcja_Fecha,aqcja_FechaInicio,aqcja_FechaFin,aqcja_SaldoInicial,aqcja_SaldoFinal,aqcja_MontoEfectivo,aqcja_MontoCheque,aqcja_MontoTCoTD,aqcja_MontoNotaCredito,aqcja_MontoCupon,aqcja_UsuarioCrea,aqcja_FechaCrea,aqcja_UsuarioModifica,aqcja_FechaModifica")] tbArqueoCaja tbArqueoCaja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbArqueoCaja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aqcja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbArqueoCaja.aqcja_UsuarioCrea);
            ViewBag.aqcja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbArqueoCaja.aqcja_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbArqueoCaja.cja_Id);
            return View(tbArqueoCaja);
        }

        // GET: /ArqueoCaja/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbArqueoCaja tbArqueoCaja = db.tbArqueoCaja.Find(id);
            if (tbArqueoCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbArqueoCaja);
        }

        // POST: /ArqueoCaja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbArqueoCaja tbArqueoCaja = db.tbArqueoCaja.Find(id);
            db.tbArqueoCaja.Remove(tbArqueoCaja);
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
