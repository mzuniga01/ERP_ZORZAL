using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class MunicipioController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Municipio/
        public ActionResult Index()
        {
            var tbmunicipio = db.tbMunicipio.Include(t => t.tbDepartamento);
            return View(tbmunicipio.ToList());
        }

        // GET: /Municipio/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMunicipio tbMunicipio = db.tbMunicipio.Find(id);
            if (tbMunicipio == null)
            {
                return HttpNotFound();
            }
            return View(tbMunicipio);
        }

        public ActionResult DetallePrueba()
        {
            return View();
        }

        public ActionResult CrearPrueba()
        {
            return View();
        }
        public ActionResult EditarPrueba()
        {
            return View();
        }

        // GET: /Municipio/Create
        public ActionResult Create()
        {
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            return View();
        }

        // POST: /Municipio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="mun_Codigo,dep_Codigo,mun_Nombre,mun_UsuarioCrea,mun_FechaCrea,mun_UsuarioModifica,mun_FechaModifica")] tbMunicipio tbMunicipio)
        {
            if (ModelState.IsValid)
            {
                db.tbMunicipio.Add(tbMunicipio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbMunicipio.dep_Codigo);
            return View(tbMunicipio);
        }

        // GET: /Municipio/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMunicipio tbMunicipio = db.tbMunicipio.Find(id);
            if (tbMunicipio == null)
            {
                return HttpNotFound();
            }
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbMunicipio.dep_Codigo);
            return View(tbMunicipio);
        }

        // POST: /Municipio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="mun_Codigo,dep_Codigo,mun_Nombre,mun_UsuarioCrea,mun_FechaCrea,mun_UsuarioModifica,mun_FechaModifica")] tbMunicipio tbMunicipio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbMunicipio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbMunicipio.dep_Codigo);
            return View(tbMunicipio);
        }

        // GET: /Municipio/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMunicipio tbMunicipio = db.tbMunicipio.Find(id);
            if (tbMunicipio == null)
            {
                return HttpNotFound();
            }
            return View(tbMunicipio);
        }

        // POST: /Municipio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbMunicipio tbMunicipio = db.tbMunicipio.Find(id);
            db.tbMunicipio.Remove(tbMunicipio);
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
