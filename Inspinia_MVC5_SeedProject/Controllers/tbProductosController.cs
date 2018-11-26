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
    public class tbProductosController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbProductos/
        public ActionResult Index()
        {
            return View(db.tbProductos.ToList());
        }

        // GET: /tbProductos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductos tbProductos = db.tbProductos.Find(id);
            if (tbProductos == null)
            {
                return HttpNotFound();
            }
            return View(tbProductos);
        }

        public ActionResult Detalles()
        {
            return View();
        }
        // GET: /tbProductos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbProductos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="prod_Codigo,prod_Descripcion,prod_FechaCreacion,prod_PrecioCompraVenta,prod_Marca,Categ_prod_IDCategoria,SubCateg_prod_IDSubCategoria,estad_prod_Codigo")] tbProductos tbProductos)
        {
            if (ModelState.IsValid)
            {
                db.tbProductos.Add(tbProductos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbProductos);
        }

        // GET: /tbProductos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductos tbProductos = db.tbProductos.Find(id);
            if (tbProductos == null)
            {
                return HttpNotFound();
            }
            return View(tbProductos);
        }

        public ActionResult Editar()
        {
            return View();
        }

        // POST: /tbProductos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="prod_Codigo,prod_Descripcion,prod_FechaCreacion,prod_PrecioCompraVenta,prod_Marca,Categ_prod_IDCategoria,SubCateg_prod_IDSubCategoria,estad_prod_Codigo")] tbProductos tbProductos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbProductos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbProductos);
        }

        // GET: /tbProductos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductos tbProductos = db.tbProductos.Find(id);
            if (tbProductos == null)
            {
                return HttpNotFound();
            }
            return View(tbProductos);
        }

        // POST: /tbProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbProductos tbProductos = db.tbProductos.Find(id);
            db.tbProductos.Remove(tbProductos);
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
