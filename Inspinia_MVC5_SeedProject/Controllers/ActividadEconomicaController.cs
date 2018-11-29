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
    public class ActividadEconomicaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /ActividadEconomica/
        public ActionResult Index()
        {
            return View(db.tbActividadEconomica.ToList());
        }

        // GET: /ActividadEconomica/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            if (tbActividadEconomica == null)
            {
                return HttpNotFound();
            }
            return View(tbActividadEconomica);
        }

        // GET: /ActividadEconomica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ActividadEconomica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="acte_Id,acte_Descripcion,acte_UsuarioCrea,acte_FechaCrea,acte_UsuarioModifica,acte_FechaModifica")] tbActividadEconomica tbActividadEconomica)
        {
            if (ModelState.IsValid)
            {
                db.tbActividadEconomica.Add(tbActividadEconomica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbActividadEconomica);
        }

        // GET: /ActividadEconomica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            if (tbActividadEconomica == null)
            {
                return HttpNotFound();
            }
            return View(tbActividadEconomica);
        }

        // POST: /ActividadEconomica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="acte_Id,acte_Descripcion,acte_UsuarioCrea,acte_FechaCrea,acte_UsuarioModifica,acte_FechaModifica")] tbActividadEconomica tbActividadEconomica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbActividadEconomica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbActividadEconomica);
        }

        // GET: /ActividadEconomica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            if (tbActividadEconomica == null)
            {
                return HttpNotFound();
            }
            return View(tbActividadEconomica);
        }

        // POST: /ActividadEconomica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            db.tbActividadEconomica.Remove(tbActividadEconomica);
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
