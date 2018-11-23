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
    public class tbTiposInventariosController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbTiposInventarios/
        public ActionResult Index()
        {
            return View(db.tbTiposInventario.ToList());
        }

        // GET: /tbTiposInventarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposInventario tbTiposInventario = db.tbTiposInventario.Find(id);
            if (tbTiposInventario == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposInventario);
        }


        // GET: /tbTiposInventarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbTiposInventarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tpinv_Codigo,tpinv_Descripcion")] tbTiposInventario tbTiposInventario)
        {
            if (ModelState.IsValid)
            {
                db.tbTiposInventario.Add(tbTiposInventario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTiposInventario);
        }

        // GET: /tbTiposInventarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposInventario tbTiposInventario = db.tbTiposInventario.Find(id);
            if (tbTiposInventario == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposInventario);
        }

        // POST: /tbTiposInventarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="tpinv_Codigo,tpinv_Descripcion")] tbTiposInventario tbTiposInventario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTiposInventario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTiposInventario);
        }

        // GET: /tbTiposInventarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposInventario tbTiposInventario = db.tbTiposInventario.Find(id);
            if (tbTiposInventario == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposInventario);
        }

        // POST: /tbTiposInventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTiposInventario tbTiposInventario = db.tbTiposInventario.Find(id);
            db.tbTiposInventario.Remove(tbTiposInventario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult edtar()
        {
            
            return View("edtar");
        }

        public ActionResult detalles()
        {

            return View("detalles");
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
