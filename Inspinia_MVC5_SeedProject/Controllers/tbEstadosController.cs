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

    public class tbEstadosController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbEstados/

        public ActionResult Index()

        {



            return View(db.tbEstados.ToList());


        }

        // GET: /tbEstados/Details/5

        public ActionResult Details(string id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbEstados tbEstados = db.tbEstados.Find(id);

            if (tbEstados == null)
            {
                return HttpNotFound();
            }
            return View(tbEstados);
        }

        // GET: /tbEstados/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /tbEstados/Create

        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 

        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include="estad_Codigo,estad_Descripcion")] tbEstados tbEstados)

        {
            if (ModelState.IsValid)
            {

                db.tbEstados.Add(tbEstados);

                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(tbEstados);
        }

        // GET: /tbEstados/Edit/5

        public ActionResult Edit(string id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbEstados tbEstados = db.tbEstados.Find(id);

            if (tbEstados == null)
            {
                return HttpNotFound();
            }

            return View(tbEstados);
        }

        // POST: /tbEstados/Edit/5

        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 

        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include="estad_Codigo,estad_Descripcion")] tbEstados tbEstados)

        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEstados).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(tbEstados);
        }

        // GET: /tbEstados/Delete/5

        public ActionResult Delete(string id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbEstados tbEstados = db.tbEstados.Find(id);

            if (tbEstados == null)
            {
                return HttpNotFound();
            }
            return View(tbEstados);
        }

        // POST: /tbEstados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(string id)

        {

            tbEstados tbEstados = db.tbEstados.Find(id);

            db.tbEstados.Remove(tbEstados);

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
