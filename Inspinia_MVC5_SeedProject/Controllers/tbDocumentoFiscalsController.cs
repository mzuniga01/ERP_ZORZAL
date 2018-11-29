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
    public class tbDocumentoFiscalsController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbDocumentoFiscals/
        public ActionResult Index()
        {
            return View(db.tbDocumentoFiscal.ToList());
        }

        // GET: /tbDocumentoFiscals/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDocumentoFiscal tbDocumentoFiscal = db.tbDocumentoFiscal.Find(id);
            if (tbDocumentoFiscal == null)
            {
                return HttpNotFound();
            }
            return View(tbDocumentoFiscal);
        }

        // GET: /tbDocumentoFiscals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbDocumentoFiscals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="dfisc_Codigo,dfisc_Descripcion,dfisc_UsuarioCrea,dfisc_FechaCrea,dfisc_UsuarioModifica,dfisc_FechaModifica")] tbDocumentoFiscal tbDocumentoFiscal)
        {
            if (ModelState.IsValid)
            {
                db.tbDocumentoFiscal.Add(tbDocumentoFiscal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbDocumentoFiscal);
        }

        // GET: /tbDocumentoFiscals/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDocumentoFiscal tbDocumentoFiscal = db.tbDocumentoFiscal.Find(id);
            if (tbDocumentoFiscal == null)
            {
                return HttpNotFound();
            }
            return View(tbDocumentoFiscal);
        }

        // POST: /tbDocumentoFiscals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="dfisc_Codigo,dfisc_Descripcion,dfisc_UsuarioCrea,dfisc_FechaCrea,dfisc_UsuarioModifica,dfisc_FechaModifica")] tbDocumentoFiscal tbDocumentoFiscal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbDocumentoFiscal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbDocumentoFiscal);
        }

        // GET: /tbDocumentoFiscals/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDocumentoFiscal tbDocumentoFiscal = db.tbDocumentoFiscal.Find(id);
            if (tbDocumentoFiscal == null)
            {
                return HttpNotFound();
            }
            return View(tbDocumentoFiscal);
        }

        // POST: /tbDocumentoFiscals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbDocumentoFiscal tbDocumentoFiscal = db.tbDocumentoFiscal.Find(id);
            db.tbDocumentoFiscal.Remove(tbDocumentoFiscal);
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
