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
    public class tbArqueoCajasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbArqueoCajas/
        public ActionResult Index()
        {
            return View(db.tbArqueoCaja.ToList());
        }

        // GET: /tbArqueoCajas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbArqueoCaja tbArqueoCaja = db.tbArqueoCaja.Find(id);
            if (tbArqueoCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbArqueoCaja);
        }

        // GET: /tbArqueoCajas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbArqueoCajas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="arcj_Codigo,cjs_Codigo,arcj_FechaArqueo,arcj_AperturaCaja,arcj_CierreCaja,suc_Codigo,VentasDias")] tbArqueoCaja tbArqueoCaja)
        {
            if (ModelState.IsValid)
            {
                db.tbArqueoCaja.Add(tbArqueoCaja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbArqueoCaja);
        }

        // GET: /tbArqueoCajas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbArqueoCaja tbArqueoCaja = db.tbArqueoCaja.Find(id);
            if (tbArqueoCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbArqueoCaja);
        }

        // POST: /tbArqueoCajas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="arcj_Codigo,cjs_Codigo,arcj_FechaArqueo,arcj_AperturaCaja,arcj_CierreCaja,suc_Codigo,VentasDias")] tbArqueoCaja tbArqueoCaja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbArqueoCaja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbArqueoCaja);
        }

        // GET: /tbArqueoCajas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbArqueoCaja tbArqueoCaja = db.tbArqueoCaja.Find(id);
            if (tbArqueoCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbArqueoCaja);
        }

        // POST: /tbArqueoCajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbArqueoCaja tbArqueoCaja = db.tbArqueoCaja.Find(id);
            db.tbArqueoCaja.Remove(tbArqueoCaja);
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
