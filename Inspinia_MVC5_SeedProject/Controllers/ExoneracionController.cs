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
    public class ExoneracionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Exoneracion/
        public ActionResult Index()
        {
            var tbexoneracion = db.tbExoneracion.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente);
            return View(tbexoneracion.ToList());
        }

        // GET: /Exoneracion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbExoneracion tbExoneracion = db.tbExoneracion.Find(id);
            if (tbExoneracion == null)
            {
                return HttpNotFound();
            }
            return View(tbExoneracion);
        }

        // GET: /Exoneracion/Create
        public ActionResult Create()
        {
            //ViewBag.exo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.exo_UsuarioModifa = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte");
            //return View();
            ViewBag.Cliente = db.tbCliente.ToList();
            return View();
        }

        // POST: /Exoneracion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="exo_Id,exo_Documento,exo_ExoneracionActiva,exo_FechaInicialVigencia,exo_FechaIFinalVigencia,clte_Id,exo_UsuarioCrea,exo_FechaCrea,exo_UsuarioModifa,exo_FechaModifica")] tbExoneracion tbExoneracion)
        {
            if (ModelState.IsValid)
            {
                db.tbExoneracion.Add(tbExoneracion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.exo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioCrea);
            ViewBag.exo_UsuarioModifa = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioModifa);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
            return View(tbExoneracion);
        }

        // GET: /Exoneracion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbExoneracion tbExoneracion = db.tbExoneracion.Find(id);
            if (tbExoneracion == null)
            {
                return HttpNotFound();
            }
            //ViewBag.exo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioCrea);
            //ViewBag.exo_UsuarioModifa = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioModifa);
            //ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
            //return View(tbExoneracion);
            ViewBag.Cliente = db.tbCliente.ToList();
            return View();
        }

        // POST: /Exoneracion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="exo_Id,exo_Documento,exo_ExoneracionActiva,exo_FechaInicialVigencia,exo_FechaIFinalVigencia,clte_Id,exo_UsuarioCrea,exo_FechaCrea,exo_UsuarioModifa,exo_FechaModifica")] tbExoneracion tbExoneracion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbExoneracion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.exo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioCrea);
            ViewBag.exo_UsuarioModifa = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioModifa);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
            return View(tbExoneracion);
        }

        // GET: /Exoneracion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbExoneracion tbExoneracion = db.tbExoneracion.Find(id);
            if (tbExoneracion == null)
            {
                return HttpNotFound();
            }
            return View(tbExoneracion);
        }

        // POST: /Exoneracion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbExoneracion tbExoneracion = db.tbExoneracion.Find(id);
            db.tbExoneracion.Remove(tbExoneracion);
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
