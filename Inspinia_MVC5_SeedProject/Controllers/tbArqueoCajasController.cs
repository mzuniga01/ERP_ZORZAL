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
    public class tbArqueoCajasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbArqueoCajas/
        public ActionResult Index()
        {
            var tbarqueocaja = db.tbArqueoCaja.Include(t => t.tbSucursal).Include(t => t.tbCaja1);
            return View(tbarqueocaja.ToList());
        }

        // GET: /tbArqueoCajas/Details/5
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

        // GET: /tbArqueoCajas/Create
        public ActionResult Create()
        {
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id");
            ViewBag.cja_Codigo = new SelectList(db.tbCaja, "cja_Codigo", "cja_Descripcion");
            return View();
        }

        // POST: /tbArqueoCajas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="aqcja_Codigo,sucur_Codigo,cja_Codigo,aqcja_Fecha,aqcja_HoraInicio,aqcja_HoraFinal,aqcja_SaldoInicial,aqcja_SaldoFinal,aqcja_MontoEfectivo,aqcja_MontoCheque,aqcja_Total,aqcja_Diferencia,aqcja_UsuarioCrea,aqcja_FechaCrea,aqcja_UsuarioModifica,aqcja_FechaModifico")] tbArqueoCaja tbArqueoCaja)
        {
            if (ModelState.IsValid)
            {
                db.tbArqueoCaja.Add(tbArqueoCaja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbArqueoCaja.sucur_Codigo);
            ViewBag.cja_Codigo = new SelectList(db.tbCaja, "cja_Codigo", "cja_Descripcion", tbArqueoCaja.cja_Codigo);
            return View(tbArqueoCaja);
        }

        // GET: /tbArqueoCajas/Edit/5
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
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbArqueoCaja.sucur_Codigo);
            ViewBag.cja_Codigo = new SelectList(db.tbCaja, "cja_Codigo", "cja_Descripcion", tbArqueoCaja.cja_Codigo);
            return View(tbArqueoCaja);
        }

        // POST: /tbArqueoCajas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="aqcja_Codigo,sucur_Codigo,cja_Codigo,aqcja_Fecha,aqcja_HoraInicio,aqcja_HoraFinal,aqcja_SaldoInicial,aqcja_SaldoFinal,aqcja_MontoEfectivo,aqcja_MontoCheque,aqcja_Total,aqcja_Diferencia,aqcja_UsuarioCrea,aqcja_FechaCrea,aqcja_UsuarioModifica,aqcja_FechaModifico")] tbArqueoCaja tbArqueoCaja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbArqueoCaja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbArqueoCaja.sucur_Codigo);
            ViewBag.cja_Codigo = new SelectList(db.tbCaja, "cja_Codigo", "cja_Descripcion", tbArqueoCaja.cja_Codigo);
            return View(tbArqueoCaja);
        }

        // GET: /tbArqueoCajas/Delete/5
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

        // POST: /tbArqueoCajas/Delete/5
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
