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
    public class ObjetoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Objeto/
        public ActionResult Index()
        {
            return View(db.tbObjeto.ToList());
        }

        // GET: /Objeto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbObjeto tbObjeto = db.tbObjeto.Find(id);
            if (tbObjeto == null)
            {
                return HttpNotFound();
            }
            return View(tbObjeto);
        }

        // GET: /Objeto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Objeto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="obj_Id,obj_Pantalla,obj_UsuarioCrea,obj_FechaCrea,obj_UsuarioModifica,obj_FechaModifica")] tbObjeto tbObjeto)
        {
            if (ModelState.IsValid)
            {
                db.tbObjeto.Add(tbObjeto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbObjeto);
        }

        // GET: /Objeto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbObjeto tbObjeto = db.tbObjeto.Find(id);
            if (tbObjeto == null)
            {
                return HttpNotFound();
            }
            return View(tbObjeto);
        }

        // POST: /Objeto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="obj_Id,obj_Pantalla,obj_UsuarioCrea,obj_FechaCrea,obj_UsuarioModifica,obj_FechaModifica")] tbObjeto tbObjeto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbObjeto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbObjeto);
        }

        // GET: /Objeto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbObjeto tbObjeto = db.tbObjeto.Find(id);
            if (tbObjeto == null)
            {
                return HttpNotFound();
            }
            return View(tbObjeto);
        }

        // POST: /Objeto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbObjeto tbObjeto = db.tbObjeto.Find(id);
            db.tbObjeto.Remove(tbObjeto);
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
        public ActionResult DetallePrueba()
        {
            return View();
        }
        public ActionResult CrearPrueba()
        {
            return View();
        }
        public ActionResult EditarPrueba()
        {
            return View();
        }
    }
}
