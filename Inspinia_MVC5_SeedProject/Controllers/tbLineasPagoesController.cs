
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

    public class tbLineasPagoesController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbLineasPagoes/

        public ActionResult Index()

        {



            return View(db.tbLineasPago.ToList());


        }

        // GET: /tbLineasPagoes/Details/5

        public ActionResult Details(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbLineasPago tbLineasPago = db.tbLineasPago.Find(id);

            if (tbLineasPago == null)
            {
                return HttpNotFound();
            }
            return View(tbLineasPago);
        }

        // GET: /tbLineasPagoes/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /tbLineasPagoes/Create

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include="lnpago_Codigo,lnpago_Plazo,lnpago_Monto")] tbLineasPago tbLineasPago)

        {
            if (ModelState.IsValid)
            {

                db.tbLineasPago.Add(tbLineasPago);

                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(tbLineasPago);
        }

        // GET: /tbLineasPagoes/Edit/5

        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbLineasPago tbLineasPago = db.tbLineasPago.Find(id);

            if (tbLineasPago == null)
            {
                return HttpNotFound();
            }

            return View(tbLineasPago);
        }

        // POST: /tbLineasPagoes/Edit/5

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include="lnpago_Codigo,lnpago_Plazo,lnpago_Monto")] tbLineasPago tbLineasPago)

        {
            if (ModelState.IsValid)
            {
                db.Entry(tbLineasPago).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(tbLineasPago);
        }

        // GET: /tbLineasPagoes/Delete/5

        public ActionResult Delete(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbLineasPago tbLineasPago = db.tbLineasPago.Find(id);

            if (tbLineasPago == null)
            {
                return HttpNotFound();
            }
            return View(tbLineasPago);
        }

        // POST: /tbLineasPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)

        {

            tbLineasPago tbLineasPago = db.tbLineasPago.Find(id);

            db.tbLineasPago.Remove(tbLineasPago);

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
