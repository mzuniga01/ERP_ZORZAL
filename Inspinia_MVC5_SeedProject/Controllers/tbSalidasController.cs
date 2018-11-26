    

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

    public class tbSalidasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbSalidas/

        public ActionResult Index()

        {



            return View(db.tbSalida.ToList());


        }

        // GET: /tbSalidas/Details/5

        public ActionResult Details(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbSalida tbSalida = db.tbSalida.Find(id);

            if (tbSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbSalida);
        }
        public ActionResult Detalles()

        {
            return View();
        }

        // GET: /tbSalidas/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Crear()
        {
            return View();
        }

        // POST: /tbSalidas/Create

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include="slda_Codigo,slda_FechadeSalida,bdg_Codigo,inv_Codigo,sld_Transporte,tpds_Codigo,std_Codigo")] tbSalida tbSalida)

        {
            if (ModelState.IsValid)
            {

                db.tbSalida.Add(tbSalida);

                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(tbSalida);
        }

        // GET: /tbSalidas/Edit/5

        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbSalida tbSalida = db.tbSalida.Find(id);

            if (tbSalida == null)
            {
                return HttpNotFound();
            }

            return View(tbSalida);
        }
        public ActionResult Editar()

        {
            return View();
        }

        // POST: /tbSalidas/Edit/5

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include="slda_Codigo,slda_FechadeSalida,bdg_Codigo,inv_Codigo,sld_Transporte,tpds_Codigo,std_Codigo")] tbSalida tbSalida)

        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSalida).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(tbSalida);
        }

        // GET: /tbSalidas/Delete/5

        public ActionResult Delete(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbSalida tbSalida = db.tbSalida.Find(id);

            if (tbSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbSalida);
        }

        // POST: /tbSalidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)

        {

            tbSalida tbSalida = db.tbSalida.Find(id);

            db.tbSalida.Remove(tbSalida);

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
