using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_ZORZAL.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class MonedaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Moneda/
        public ActionResult Index()
        {
            return View(db.tbMoneda.ToList());
        }

        // GET: /Moneda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            if (tbMoneda == null)
            {
                return HttpNotFound();
            }
            return View(tbMoneda);
        }

        // GET: /Moneda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Moneda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="mnda_Id,mnda_Iso,mnda_Nombre,mnda_UsuarioCrea,mnda_FechaCrea,mnda_UsuarioModifica,mnda_FechaModifica")] tbMoneda tbMoneda)
        {
            if (ModelState.IsValid)
            {
                db.tbMoneda.Add(tbMoneda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbMoneda);
        }

        // GET: /Moneda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            if (tbMoneda == null)
            {
                return HttpNotFound();
            }
            return View(tbMoneda);
        }

        // POST: /Moneda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="mnda_Id,mnda_Iso,mnda_Nombre,mnda_UsuarioCrea,mnda_FechaCrea,mnda_UsuarioModifica,mnda_FechaModifica")] tbMoneda tbMoneda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbMoneda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbMoneda);
        }

        // GET: /Moneda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            if (tbMoneda == null)
            {
                return HttpNotFound();
            }
            return View(tbMoneda);
        }

        // POST: /Moneda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            db.tbMoneda.Remove(tbMoneda);
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
