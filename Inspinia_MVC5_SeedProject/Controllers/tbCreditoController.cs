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
    public class tbCreditoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbCredito/
        public ActionResult Index()
        {
            return View(db.tbCredito.ToList());
        }

        // GET: /tbCredito/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCredito tbCredito = db.tbCredito.Find(id);
            if (tbCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbCredito);
        }

        // GET: /tbCredito/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbCredito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="cred_Codigo,clte_RTNCliente,lnpago_Codigo,edo_Codigo,cred_FechaSolicitud,cred_FechaAprobacion,cred_MontoSolicitud,cred_MontoCredito,cred_LimiteCredito")] tbCredito tbCredito)
        {
            if (ModelState.IsValid)
            {
                db.tbCredito.Add(tbCredito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbCredito);
        }
        // GET: /tbCredito/Create
        public ActionResult CreateSolicitud()
        {
            return View();
        }

        // POST: /tbCredito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSolicitud([Bind(Include = "cred_Codigo,clte_RTNCliente,lnpago_Codigo,edo_Codigo,cred_FechaSolicitud,cred_FechaAprobacion,cred_MontoSolicitud,cred_MontoCredito,cred_LimiteCredito")] tbCredito tbCredito)
        {
            if (ModelState.IsValid)
            {
                db.tbCredito.Add(tbCredito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbCredito);
        }

        // GET: /tbCredito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCredito tbCredito = db.tbCredito.Find(id);
            if (tbCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbCredito);
        }

        // POST: /tbCredito/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="cred_Codigo,clte_RTNCliente,lnpago_Codigo,edo_Codigo,cred_FechaSolicitud,cred_FechaAprobacion,cred_MontoSolicitud,cred_MontoCredito,cred_LimiteCredito")] tbCredito tbCredito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCredito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbCredito);
        }

        // GET: /tbCredito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCredito tbCredito = db.tbCredito.Find(id);
            if (tbCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbCredito);
        }

        // POST: /tbCredito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCredito tbCredito = db.tbCredito.Find(id);
            db.tbCredito.Remove(tbCredito);
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
        // GET: /tbCredito/Details/5
        public ActionResult Details1()
        {

            return View();
        }

        // GET: /tbCredito/
        public ActionResult ListadoGerenteCredito()
        {
            return View(db.tbCredito.ToList());
        }

        // GET: /tbCredito/Details/5
        public ActionResult Details2()
        {

            return View();
        }

        // GET: /tbCredito/Create
        public ActionResult CreateSolicitud2()
        {
            return View();
        }

    }
}
