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
    public class ProductoSubcategoriaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /ProductoSubcategoria/
        public ActionResult Index()
        {
            var tbproductosubcategoria = db.tbProductoSubcategoria.Include(t => t.tbEstadoMovimiento).Include(t => t.tbProductoCategoria);
            return View(tbproductosubcategoria.ToList());
        }

        // GET: /ProductoSubcategoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoSubcategoria tbProductoSubcategoria = db.tbProductoSubcategoria.Find(id);
            if (tbProductoSubcategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbProductoSubcategoria);
        }

        // GET: /ProductoSubcategoria/Create
        public ActionResult Create()
        {
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");
            return View();
        }

        // POST: /ProductoSubcategoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="pscat_Id,pscat_Descripcion,pcat_Id,estm_Id,pscat_UsuarioCrea,pscat_FechaCrea,pscat_UsuarioModifica,pscat_FechaModifica")] tbProductoSubcategoria tbProductoSubcategoria)
        {
            if (ModelState.IsValid)
            {
                db.tbProductoSubcategoria.Add(tbProductoSubcategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbProductoSubcategoria.estm_Id);
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre", tbProductoSubcategoria.pcat_Id);
            return View(tbProductoSubcategoria);
        }

        // GET: /ProductoSubcategoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoSubcategoria tbProductoSubcategoria = db.tbProductoSubcategoria.Find(id);
            if (tbProductoSubcategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbProductoSubcategoria.estm_Id);
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre", tbProductoSubcategoria.pcat_Id);
            return View(tbProductoSubcategoria);
        }

        // POST: /ProductoSubcategoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="pscat_Id,pscat_Descripcion,pcat_Id,estm_Id,pscat_UsuarioCrea,pscat_FechaCrea,pscat_UsuarioModifica,pscat_FechaModifica")] tbProductoSubcategoria tbProductoSubcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbProductoSubcategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbProductoSubcategoria.estm_Id);
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre", tbProductoSubcategoria.pcat_Id);
            return View(tbProductoSubcategoria);
        }

        // GET: /ProductoSubcategoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoSubcategoria tbProductoSubcategoria = db.tbProductoSubcategoria.Find(id);
            if (tbProductoSubcategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbProductoSubcategoria);
        }

        // POST: /ProductoSubcategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbProductoSubcategoria tbProductoSubcategoria = db.tbProductoSubcategoria.Find(id);
            db.tbProductoSubcategoria.Remove(tbProductoSubcategoria);
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

        public ActionResult Editar()
        {
            return View();
        }
        public ActionResult Detalles()
        {
            return View();
        }
        public ActionResult Crear()
        {
            return View();
        }
        public ActionResult Index_Pro_SubC()
        {
            return View();
        }

    }
}
