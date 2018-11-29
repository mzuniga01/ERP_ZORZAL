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
    public class tbSolicitudEfectivoesController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbSolicitudEfectivoes/
        public ActionResult Index()
        {
            var tbsolicitudefectivo = db.tbSolicitudEfectivo.Include(t => t.tbCaja1);
            return View(tbsolicitudefectivo.ToList());
        }

        // GET: /tbSolicitudEfectivoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            if (tbSolicitudEfectivo == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudEfectivo);
        }

        // GET: /tbSolicitudEfectivoes/Create
        public ActionResult Create()
        {
            ViewBag.cja_Codigo = new SelectList(db.tbCaja, "cja_Codigo", "cja_Descripcion");
            return View();
        }

        // POST: /tbSolicitudEfectivoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="solef_Id,cja_Codigo,solef_MontoSolicitud,solef_UsuarioCrea,solef_FechaCrea,solef_UsuarioModifica,solef_FechaModifica")] tbSolicitudEfectivo tbSolicitudEfectivo)
        {
            if (ModelState.IsValid)
            {
                db.tbSolicitudEfectivo.Add(tbSolicitudEfectivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cja_Codigo = new SelectList(db.tbCaja, "cja_Codigo", "cja_Descripcion", tbSolicitudEfectivo.cja_Codigo);
            return View(tbSolicitudEfectivo);
        }

        // GET: /tbSolicitudEfectivoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            if (tbSolicitudEfectivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.cja_Codigo = new SelectList(db.tbCaja, "cja_Codigo", "cja_Descripcion", tbSolicitudEfectivo.cja_Codigo);
            return View(tbSolicitudEfectivo);
        }

        // POST: /tbSolicitudEfectivoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="solef_Id,cja_Codigo,solef_MontoSolicitud,solef_UsuarioCrea,solef_FechaCrea,solef_UsuarioModifica,solef_FechaModifica")] tbSolicitudEfectivo tbSolicitudEfectivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSolicitudEfectivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cja_Codigo = new SelectList(db.tbCaja, "cja_Codigo", "cja_Descripcion", tbSolicitudEfectivo.cja_Codigo);
            return View(tbSolicitudEfectivo);
        }

        // GET: /tbSolicitudEfectivoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            if (tbSolicitudEfectivo == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudEfectivo);
        }

        // POST: /tbSolicitudEfectivoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            db.tbSolicitudEfectivo.Remove(tbSolicitudEfectivo);
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
