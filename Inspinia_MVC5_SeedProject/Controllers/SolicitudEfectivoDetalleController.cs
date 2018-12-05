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
    public class SolicitudEfectivoDetalleController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /SolicitudEfectivoDetalle/
        public ActionResult Index()
        {
            var tbsolicitudefectivodetalle = db.tbSolicitudEfectivoDetalle.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbDenominacion).Include(t => t.tbSolicitudEfectivo);
            return View(tbsolicitudefectivodetalle.ToList());
        }

        // GET: /SolicitudEfectivoDetalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudEfectivoDetalle tbSolicitudEfectivoDetalle = db.tbSolicitudEfectivoDetalle.Find(id);
            if (tbSolicitudEfectivoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudEfectivoDetalle);
        }

        // GET: /SolicitudEfectivoDetalle/Create
        public ActionResult Create()
        {
            ViewBag.soled_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.soled_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion");
            ViewBag.solef_Id = new SelectList(db.tbSolicitudEfectivo, "solef_Id", "solef_Id");
            return View();
        }

        // POST: /SolicitudEfectivoDetalle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="soled_Id,solef_Id,deno_Id,soled_CantidadSolicitada,soled_CantidadEntregada,soled_UsuarioCrea,soled_FechaCrea,soled_UsuarioModifica,soled_FechaModifica")] tbSolicitudEfectivoDetalle tbSolicitudEfectivoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.tbSolicitudEfectivoDetalle.Add(tbSolicitudEfectivoDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.soled_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivoDetalle.soled_UsuarioCrea);
            ViewBag.soled_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivoDetalle.soled_UsuarioModifica);
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbSolicitudEfectivoDetalle.deno_Id);
            ViewBag.solef_Id = new SelectList(db.tbSolicitudEfectivo, "solef_Id", "solef_Id", tbSolicitudEfectivoDetalle.solef_Id);
            return View(tbSolicitudEfectivoDetalle);
        }

        // GET: /SolicitudEfectivoDetalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudEfectivoDetalle tbSolicitudEfectivoDetalle = db.tbSolicitudEfectivoDetalle.Find(id);
            if (tbSolicitudEfectivoDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.soled_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivoDetalle.soled_UsuarioCrea);
            ViewBag.soled_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivoDetalle.soled_UsuarioModifica);
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbSolicitudEfectivoDetalle.deno_Id);
            ViewBag.solef_Id = new SelectList(db.tbSolicitudEfectivo, "solef_Id", "solef_Id", tbSolicitudEfectivoDetalle.solef_Id);
            return View(tbSolicitudEfectivoDetalle);
        }

        // POST: /SolicitudEfectivoDetalle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="soled_Id,solef_Id,deno_Id,soled_CantidadSolicitada,soled_CantidadEntregada,soled_UsuarioCrea,soled_FechaCrea,soled_UsuarioModifica,soled_FechaModifica")] tbSolicitudEfectivoDetalle tbSolicitudEfectivoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSolicitudEfectivoDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.soled_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivoDetalle.soled_UsuarioCrea);
            ViewBag.soled_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivoDetalle.soled_UsuarioModifica);
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbSolicitudEfectivoDetalle.deno_Id);
            ViewBag.solef_Id = new SelectList(db.tbSolicitudEfectivo, "solef_Id", "solef_Id", tbSolicitudEfectivoDetalle.solef_Id);
            return View(tbSolicitudEfectivoDetalle);
        }

        // GET: /SolicitudEfectivoDetalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudEfectivoDetalle tbSolicitudEfectivoDetalle = db.tbSolicitudEfectivoDetalle.Find(id);
            if (tbSolicitudEfectivoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudEfectivoDetalle);
        }

        // POST: /SolicitudEfectivoDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSolicitudEfectivoDetalle tbSolicitudEfectivoDetalle = db.tbSolicitudEfectivoDetalle.Find(id);
            db.tbSolicitudEfectivoDetalle.Remove(tbSolicitudEfectivoDetalle);
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
