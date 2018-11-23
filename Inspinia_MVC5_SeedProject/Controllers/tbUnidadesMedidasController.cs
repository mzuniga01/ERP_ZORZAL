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
    public class tbUnidadesMedidasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbUnidadesMedidas/
        public ActionResult Index()
        {
            return View(db.tbUnidadesMedida.ToList());
        }

        // GET: /tbUnidadesMedidas/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbUnidadesMedida tbUnidadesMedida = db.tbUnidadesMedida.Find(id);
        //    if (tbUnidadesMedida == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbUnidadesMedida);
        //}
        public ActionResult Details()
        {
            
            return View();
        }


        // GET: /tbUnidadesMedidas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbUnidadesMedidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="udsm_Codigo,udsm_DESCRIPCION")] tbUnidadesMedida tbUnidadesMedida)
        {
            if (ModelState.IsValid)
            {
                db.tbUnidadesMedida.Add(tbUnidadesMedida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbUnidadesMedida);
        }

        // GET: /tbUnidadesMedidas/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbUnidadesMedida tbUnidadesMedida = db.tbUnidadesMedida.Find(id);
        //    if (tbUnidadesMedida == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbUnidadesMedida);
        //}

        public ActionResult Edit()
        {
            return View();
        }





        // POST: /tbUnidadesMedidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="udsm_Codigo,udsm_DESCRIPCION")] tbUnidadesMedida tbUnidadesMedida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbUnidadesMedida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbUnidadesMedida);
        }

        // GET: /tbUnidadesMedidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUnidadesMedida tbUnidadesMedida = db.tbUnidadesMedida.Find(id);
            if (tbUnidadesMedida == null)
            {
                return HttpNotFound();
            }
            return View(tbUnidadesMedida);
        }

        // POST: /tbUnidadesMedidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbUnidadesMedida tbUnidadesMedida = db.tbUnidadesMedida.Find(id);
            db.tbUnidadesMedida.Remove(tbUnidadesMedida);
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
