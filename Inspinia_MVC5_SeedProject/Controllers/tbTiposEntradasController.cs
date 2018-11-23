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
    public class tbTiposEntradasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbTiposEntradas/
        public ActionResult Index()
        {
            return View(db.tbTiposEntrada.ToList());
        }

        // GET: /tbTiposEntradas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposEntrada tbTiposEntrada = db.tbTiposEntrada.Find(id);
            if (tbTiposEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposEntrada);
        }

        public ActionResult Detalles()
        {
            return View();
        }

        // GET: /tbTiposEntradas/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Crear()
        {
            return View();
        }

        // POST: /tbTiposEntradas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tpde_Codigo,tpde_Descripcion")] tbTiposEntrada tbTiposEntrada)
        {
            if (ModelState.IsValid)
            {
                db.tbTiposEntrada.Add(tbTiposEntrada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTiposEntrada);
        }

        // GET: /tbTiposEntradas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposEntrada tbTiposEntrada = db.tbTiposEntrada.Find(id);
            if (tbTiposEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposEntrada);
        }

        public ActionResult Editar()
        {
            return View();
        }

        // POST: /tbTiposEntradas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="tpde_Codigo,tpde_Descripcion")] tbTiposEntrada tbTiposEntrada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTiposEntrada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTiposEntrada);
        }

        // GET: /tbTiposEntradas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposEntrada tbTiposEntrada = db.tbTiposEntrada.Find(id);
            if (tbTiposEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposEntrada);
        }

        // POST: /tbTiposEntradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTiposEntrada tbTiposEntrada = db.tbTiposEntrada.Find(id);
            db.tbTiposEntrada.Remove(tbTiposEntrada);
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
