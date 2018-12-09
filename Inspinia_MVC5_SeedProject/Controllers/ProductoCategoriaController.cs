using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_ZORZAL.Controllers
{
    public class ProductoCategoriaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /ProductoCategoria/
        public ActionResult Index()
        {
            var tbproductocategoria = db.tbProductoCategoria.Include(t => t.tbUsuario);
            return View(tbproductocategoria.ToList());
        }

        // GET: /ProductoCategoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            if (tbProductoCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbProductoCategoria);
        }

        // GET: /ProductoCategoria/Create
        public ActionResult Create()
        {
            ViewBag.pcat_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: /ProductoCategoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica")] tbProductoCategoria tbProductoCategoria)
        {
            if (ModelState.IsValid)
            {
                db.tbProductoCategoria.Add(tbProductoCategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pcat_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProductoCategoria.pcat_UsuarioModifica);
            return View(tbProductoCategoria);
        }

        // GET: /ProductoCategoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            if (tbProductoCategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.pcat_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProductoCategoria.pcat_UsuarioModifica);
            return View(tbProductoCategoria);
        }

        // POST: /ProductoCategoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica")] tbProductoCategoria tbProductoCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbProductoCategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pcat_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProductoCategoria.pcat_UsuarioModifica);
            return View(tbProductoCategoria);
        }

        // GET: /ProductoCategoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            if (tbProductoCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbProductoCategoria);
        }

        // POST: /ProductoCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            db.tbProductoCategoria.Remove(tbProductoCategoria);
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
