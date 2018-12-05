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
    public class DenominacionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Denominacion/
        public ActionResult Index()
        {
            var tbdenominacion = db.tbDenominacion.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbMoneda);


          

            return View(tbdenominacion.ToList());

           

        }

        // GET: /Denominacion/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDenominacion tbDenominacion = db.tbDenominacion.Find(id);
            if (tbDenominacion == null)
            {
                return HttpNotFound();
            }
            return View(tbDenominacion);
        }

        // GET: /Denominacion/Create
        public ActionResult Create()
        {

            

            ViewBag.deno_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.deno_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
            ViewBag.mnda_Nombre = new SelectList(db.tbMoneda, "mnda_Id","mnda_Nombre");
           

            return View();
        }

        // POST: /Denominacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "deno_Id,deno_Descripcion,deno_Tipo,deno_valor,mnda_Id")] tbDenominacion tbDenominacion)
        {
            if (ModelState.IsValid)
            {
                db.tbDenominacion.Add(tbDenominacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.deno_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacion.deno_UsuarioCrea);
            ViewBag.deno_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacion.deno_UsuarioModifica);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);
            return View(tbDenominacion);
        }

        // GET: /Denominacion/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDenominacion tbDenominacion = db.tbDenominacion.Find(id);
            if (tbDenominacion == null)
            {
                return HttpNotFound();
            }

            ////tbDenominacion Denominacion = new tbDenominacion();
            ////Denominacion. = cDenominaciones.deno_Tipo();

            ViewBag.deno_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacion.deno_UsuarioCrea);
            ViewBag.deno_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacion.deno_UsuarioModifica);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);
            return View(tbDenominacion);
        }
        // POST: /Denominacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="deno_Id,deno_Descripcion,deno_Tipo,deno_valor,mnda_Id,deno_UsuarioCrea,deno_FechaCrea,deno_UsuarioModifica,deno_FechaModifica")] tbDenominacion tbDenominacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbDenominacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.deno_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacion.deno_UsuarioCrea);
            ViewBag.deno_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacion.deno_UsuarioModifica);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);
            return View(tbDenominacion);
        }

        // GET: /Denominacion/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDenominacion tbDenominacion = db.tbDenominacion.Find(id);
            if (tbDenominacion == null)
            {
                return HttpNotFound();
            }
            return View(tbDenominacion);
        }

        // POST: /Denominacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbDenominacion tbDenominacion = db.tbDenominacion.Find(id);
            db.tbDenominacion.Remove(tbDenominacion);
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
