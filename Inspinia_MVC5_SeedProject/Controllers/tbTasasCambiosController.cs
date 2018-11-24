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
    public class tbTasasCambiosController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbTasasCambios/
        public ActionResult Index()
        {
            return View(db.tbTasasCambio.ToList());
        }

        // GET: /tbTasasCambios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTasasCambio tbTasasCambio = db.tbTasasCambio.Find(id);
            if (tbTasasCambio == null)
            {
                return HttpNotFound();
            }
            return View(tbTasasCambio);
        }

        // GET: /tbTasasCambios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbTasasCambios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tcam_Codigo,mnda_ISO,tcam_ISOMonedaDestino,tcam_FechaTasasCambio")] tbTasasCambio tbTasasCambio)
        {
            if (ModelState.IsValid)
            {
                db.tbTasasCambio.Add(tbTasasCambio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTasasCambio);
        }

        // GET: /tbTasasCambios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTasasCambio tbTasasCambio = db.tbTasasCambio.Find(id);
            if (tbTasasCambio == null)
            {
                return HttpNotFound();
            }
            return View(tbTasasCambio);
        }

        // POST: /tbTasasCambios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="tcam_Codigo,mnda_ISO,tcam_ISOMonedaDestino,tcam_FechaTasasCambio")] tbTasasCambio tbTasasCambio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTasasCambio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTasasCambio);
        }

        // GET: /tbTasasCambios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTasasCambio tbTasasCambio = db.tbTasasCambio.Find(id);
            if (tbTasasCambio == null)
            {
                return HttpNotFound();
            }
            return View(tbTasasCambio);
        }

        // POST: /tbTasasCambios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTasasCambio tbTasasCambio = db.tbTasasCambio.Find(id);
            db.tbTasasCambio.Remove(tbTasasCambio);
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
