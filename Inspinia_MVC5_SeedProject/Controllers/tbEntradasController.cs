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
    public class tbEntradasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbEntradas/
        public ActionResult Index()
        {
            return View(db.tbEntrada.ToList());
        }

        // GET: /tbEntradas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            if (tbEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbEntrada);
        }

        // GET: /tbEntradas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbEntradas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="IdEntrada,IdProducto,IdUnidad,FechaEntrada,IdBodega,IdUsuario,E_IdEntrada,E_IdInvetario,E_IdProducto,E_IdUnidad,E_PrecioUnitario,E_FechaEntrada,E_IdBodega,E_IdUsuario")] tbEntrada tbEntrada)
        {
            if (ModelState.IsValid)
            {
                db.tbEntrada.Add(tbEntrada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbEntrada);
        }

        // GET: /tbEntradas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            if (tbEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbEntrada);
        }

        // POST: /tbEntradas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="IdEntrada,IdProducto,IdUnidad,FechaEntrada,IdBodega,IdUsuario,E_IdEntrada,E_IdInvetario,E_IdProducto,E_IdUnidad,E_PrecioUnitario,E_FechaEntrada,E_IdBodega,E_IdUsuario")] tbEntrada tbEntrada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEntrada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbEntrada);
        }

        // GET: /tbEntradas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            if (tbEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbEntrada);
        }

        // POST: /tbEntradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            db.tbEntrada.Remove(tbEntrada);
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

        public ActionResult Editar_prueba()
        {
         
            return View();
        }
        public ActionResult Crear_prueba()
        {

            return View();
        }
    }
}
