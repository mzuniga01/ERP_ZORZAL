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
    public class tbBodegasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbBodegas/
        public ActionResult Index()
        {
            return View(db.tbBodega.ToList());
        }

        // GET: /tbBodegas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBodega tbBodega = db.tbBodega.Find(id);
            if (tbBodega == null)
            {
                return HttpNotFound();
            }
            return View(tbBodega);
        }

        // GET: /tbBodegas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbBodegas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Bdga_Codigo,Bdga_Encargado,Bdga_Contacto,estad_Codigo,dpto_Codigo,mpio_Codigo,Bdga_Capacidad,Bdga_Zona,tpbdga_Codigo")] tbBodega tbBodega)
        {
            if (ModelState.IsValid)
            {
                db.tbBodega.Add(tbBodega);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbBodega);
        }

        // GET: /tbBodegas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBodega tbBodega = db.tbBodega.Find(id);
            if (tbBodega == null)
            {
                return HttpNotFound();
            }
            return View(tbBodega);
        }

        // POST: /tbBodegas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Bdga_Codigo,Bdga_Encargado,Bdga_Contacto,estad_Codigo,dpto_Codigo,mpio_Codigo,Bdga_Capacidad,Bdga_Zona,tpbdga_Codigo")] tbBodega tbBodega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbBodega).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbBodega);
        }

        // GET: /tbBodegas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBodega tbBodega = db.tbBodega.Find(id);
            if (tbBodega == null)
            {
                return HttpNotFound();
            }
            return View(tbBodega);
        }

        // POST: /tbBodegas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbBodega tbBodega = db.tbBodega.Find(id);
            db.tbBodega.Remove(tbBodega);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult editar()
        {
            return View("editar");
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
