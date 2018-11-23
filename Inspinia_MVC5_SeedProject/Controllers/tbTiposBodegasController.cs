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

    public class tbTiposBodegasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbTiposBodegas/

        public ActionResult Index()

        {



            return View(db.tbTiposBodega.ToList());


        }

        // GET: /tbTiposBodegas/Details/5

        public ActionResult Details(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbTiposBodega tbTiposBodega = db.tbTiposBodega.Find(id);

            if (tbTiposBodega == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposBodega);
        }
        public ActionResult Details()
        {
            return View();
        }
        public ActionResult Detalle()
        {
            return View();
        }


        // GET: /tbTiposBodegas/Create
        public ActionResult Create()
        {

            return View();
        }

        public ActionResult Crear()
        {

            return View();
        }

       

        // POST: /tbTiposBodegas/Create

        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 

        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include="tpbdg_Codigo,tpbdg_Descripcion")] tbTiposBodega tbTiposBodega)

        {
            if (ModelState.IsValid)
            {

                db.tbTiposBodega.Add(tbTiposBodega);

                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(tbTiposBodega);
        }

        

        // GET: /tbTiposBodegas/Edit/5

        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbTiposBodega tbTiposBodega = db.tbTiposBodega.Find(id);

            if (tbTiposBodega == null)
            {
                return HttpNotFound();
            }

            return View(tbTiposBodega);
        }

        public ActionResult Editar()
        {

            return View();
        }
        // POST: /tbTiposBodegas/Edit/5

        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 

        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include="tpbdg_Codigo,tpbdg_Descripcion")] tbTiposBodega tbTiposBodega)

        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTiposBodega).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(tbTiposBodega);
        }

        // GET: /tbTiposBodegas/Delete/5

        public ActionResult Delete(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbTiposBodega tbTiposBodega = db.tbTiposBodega.Find(id);

            if (tbTiposBodega == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposBodega);
        }

        // POST: /tbTiposBodegas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)

        {

            tbTiposBodega tbTiposBodega = db.tbTiposBodega.Find(id);

            db.tbTiposBodega.Remove(tbTiposBodega);

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
