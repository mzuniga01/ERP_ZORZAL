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
    public class tbTiposDocumentoesController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbTiposDocumentoes/
        public ActionResult Index()
        {
            return View(db.tbTiposDocumento.ToList());
        }

        // GET: /tbTiposDocumentoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposDocumento tbTiposDocumento = db.tbTiposDocumento.Find(id);
            if (tbTiposDocumento == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposDocumento);
        }

        // GET: /tbTiposDocumentoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbTiposDocumentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="docCodTipoDocumento,docNombreDocumento")] tbTiposDocumento tbTiposDocumento)
        {
            if (ModelState.IsValid)
            {
                db.tbTiposDocumento.Add(tbTiposDocumento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTiposDocumento);
        }

        // GET: /tbTiposDocumentoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposDocumento tbTiposDocumento = db.tbTiposDocumento.Find(id);
            if (tbTiposDocumento == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposDocumento);
        }

        // POST: /tbTiposDocumentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="docCodTipoDocumento,docNombreDocumento")] tbTiposDocumento tbTiposDocumento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTiposDocumento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTiposDocumento);
        }

        // GET: /tbTiposDocumentoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposDocumento tbTiposDocumento = db.tbTiposDocumento.Find(id);
            if (tbTiposDocumento == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposDocumento);
        }

        // POST: /tbTiposDocumentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbTiposDocumento tbTiposDocumento = db.tbTiposDocumento.Find(id);
            db.tbTiposDocumento.Remove(tbTiposDocumento);
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
