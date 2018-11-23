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
    public class tbTiposSalidaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbTiposSalida/
        public ActionResult Index()
        {
            return View(db.tbTiposSalida.ToList());
        }

        // GET: /tbTiposSalida/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposSalida tbTiposSalida = db.tbTiposSalida.Find(id);
            if (tbTiposSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposSalida);
        }
        public ActionResult DetallePrueba()
        {
            return View();
        }
        // GET: /tbTiposSalida/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CrearPrueba()
        {
            return View();
        }

        // POST: /tbTiposSalida/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tpds_Codigo,tpds_Descripcion")] tbTiposSalida tbTiposSalida)
        {
            if (ModelState.IsValid)
            {
                db.tbTiposSalida.Add(tbTiposSalida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTiposSalida);
        }

        // GET: /tbTiposSalida/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposSalida tbTiposSalida = db.tbTiposSalida.Find(id);
            if (tbTiposSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposSalida);
        }

        // POST: /tbTiposSalida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="tpds_Codigo,tpds_Descripcion")] tbTiposSalida tbTiposSalida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTiposSalida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTiposSalida);
        }
        public ActionResult EditarPrueba()
        {
            return View();
        }
        // GET: /tbTiposSalida/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposSalida tbTiposSalida = db.tbTiposSalida.Find(id);
            if (tbTiposSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposSalida);
        }

        // POST: /tbTiposSalida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTiposSalida tbTiposSalida = db.tbTiposSalida.Find(id);
            db.tbTiposSalida.Remove(tbTiposSalida);
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
