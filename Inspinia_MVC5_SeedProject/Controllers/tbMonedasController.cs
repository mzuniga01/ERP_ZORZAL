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
    public class tbMonedasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbMonedas/
        public ActionResult Index()
        {
            return View(db.tbMonedas.ToList());
        }

        // GET: /tbMonedas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMonedas tbMonedas = db.tbMonedas.Find(id);
            if (tbMonedas == null)
            {
                return HttpNotFound();
            }
            return View(tbMonedas);
        }

        // GET: /tbMonedas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbMonedas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="mnda_ISO,mnda_Nombre,mnda_Pais")] tbMonedas tbMonedas)
        {
            if (ModelState.IsValid)
            {
                db.tbMonedas.Add(tbMonedas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbMonedas);
        }

        // GET: /tbMonedas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMonedas tbMonedas = db.tbMonedas.Find(id);
            if (tbMonedas == null)
            {
                return HttpNotFound();
            }
            return View(tbMonedas);
        }

        // POST: /tbMonedas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="mnda_ISO,mnda_Nombre,mnda_Pais")] tbMonedas tbMonedas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbMonedas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbMonedas);
        }

        // GET: /tbMonedas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMonedas tbMonedas = db.tbMonedas.Find(id);
            if (tbMonedas == null)
            {
                return HttpNotFound();
            }
            return View(tbMonedas);
        }

        // POST: /tbMonedas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    tbMonedas tbMonedas = db.tbMonedas.Find(id);
        //    db.tbMonedas.Remove(tbMonedas);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        // GET: /tbMonedas/Create
        public ActionResult EditM()
        {
            return View();
        }

        // POST: /tbMonedas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1([Bind(Include = "mnda_ISO,mnda_Nombre,mnda_Pais")] tbMonedas tbMonedas)
        {
            if (ModelState.IsValid)
            {
                db.tbMonedas.Add(tbMonedas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbMonedas);
        }

        // GET: /tbMonedas/Details/5
        public ActionResult Details1()
        {
            
            return View();
        }


    }
}
