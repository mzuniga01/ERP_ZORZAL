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
    public class tbSalidaDetallesController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbSalidaDetalles/
        public ActionResult Index()
        {
            var tbsalidadetalle = db.tbSalidaDetalle.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbProducto).Include(t => t.tbSalida);
            return View(tbsalidadetalle.ToList());
        }

        // GET: /tbSalidaDetalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalidaDetalle tbSalidaDetalle = db.tbSalidaDetalle.Find(id);
            if (tbSalidaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbSalidaDetalle);
        }

        // GET: /tbSalidaDetalles/Create
        public ActionResult Create()
        {
            ViewBag.sald_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.sald_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.sal_Id = new SelectList(db.tbSalida, "sal_Id", "box_Codigo");
            return View();
        }

        // POST: /tbSalidaDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="sald_Id,sal_Id,prod_Codigo,sal_Cantidad,sald_UsuarioCrea,sald_FechaCrea,sald_UsuarioModifica,sald_FechaModifica")] tbSalidaDetalle tbSalidaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.tbSalidaDetalle.Add(tbSalidaDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sald_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalidaDetalle.sald_UsuarioCrea);
            ViewBag.sald_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalidaDetalle.sald_UsuarioModifica);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbSalidaDetalle.prod_Codigo);
            ViewBag.sal_Id = new SelectList(db.tbSalida, "sal_Id", "box_Codigo", tbSalidaDetalle.sal_Id);
            return View(tbSalidaDetalle);
        }

        // GET: /tbSalidaDetalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalidaDetalle tbSalidaDetalle = db.tbSalidaDetalle.Find(id);
            if (tbSalidaDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.sald_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalidaDetalle.sald_UsuarioCrea);
            ViewBag.sald_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalidaDetalle.sald_UsuarioModifica);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbSalidaDetalle.prod_Codigo);
            ViewBag.sal_Id = new SelectList(db.tbSalida, "sal_Id", "box_Codigo", tbSalidaDetalle.sal_Id);
            return View(tbSalidaDetalle);
        }
        public ActionResult _EditSalidaDetalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalidaDetalle tbSalidaDetalle = db.tbSalidaDetalle.Find(id);
            if (tbSalidaDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.sald_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalidaDetalle.sald_UsuarioCrea);
            ViewBag.sald_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalidaDetalle.sald_UsuarioModifica);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbSalidaDetalle.prod_Codigo);
            ViewBag.sal_Id = new SelectList(db.tbSalida, "sal_Id", "box_Codigo", tbSalidaDetalle.sal_Id);
            return View(tbSalidaDetalle);
        }
        // POST: /tbSalidaDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="sald_Id,sal_Id,prod_Codigo,sal_Cantidad,sald_UsuarioCrea,sald_FechaCrea,sald_UsuarioModifica,sald_FechaModifica")] tbSalidaDetalle tbSalidaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSalidaDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sald_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalidaDetalle.sald_UsuarioCrea);
            ViewBag.sald_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalidaDetalle.sald_UsuarioModifica);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbSalidaDetalle.prod_Codigo);
            ViewBag.sal_Id = new SelectList(db.tbSalida, "sal_Id", "box_Codigo", tbSalidaDetalle.sal_Id);
            return View(tbSalidaDetalle);
        }

        // GET: /tbSalidaDetalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalidaDetalle tbSalidaDetalle = db.tbSalidaDetalle.Find(id);
            if (tbSalidaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbSalidaDetalle);
        }

        // POST: /tbSalidaDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSalidaDetalle tbSalidaDetalle = db.tbSalidaDetalle.Find(id);
            db.tbSalidaDetalle.Remove(tbSalidaDetalle);
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
