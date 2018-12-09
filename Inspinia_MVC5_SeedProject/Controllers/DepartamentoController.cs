using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_ZORZAL.Controllers
{
    public class DepartamentoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Departamento/
        public ActionResult Index()
        {
            return View(db.tbDepartamento.ToList());
        }
        public ActionResult _IndexMunicipio()
        {
            return View();
        }
        public ActionResult _IndexMunicipio_Botones()
        {
            return View();
        }
        // GET: /Departamento/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamento);
        }

        // GET: /Departamento/Create
        public ActionResult Create()
        {
            ViewBag.dep_Codigo = new SelectList( db.tbDepartamento, "dep_Codigo", "dep_Nombre", "Seleccione");
            return View();
        }

        // POST: /Departamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="dep_Codigo,dep_Nombre,dep_UsuarioCrea,dep_FechaCrea,dep_UsuarioModifica,dep_FechaModifica")] tbDepartamento tbDepartamento)
        {
            if (ModelState.IsValid)
            {
                db.tbDepartamento.Add(tbDepartamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbDepartamento);
        }

        // GET: /Departamento/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamento);
        }

        // POST: /Departamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="dep_Codigo,dep_Nombre,dep_UsuarioCrea,dep_FechaCrea,dep_UsuarioModifica,dep_FechaModifica")] tbDepartamento tbDepartamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbDepartamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbDepartamento);
        }

        // GET: /Departamento/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamento);
        }

        // POST: /Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            db.tbDepartamento.Remove(tbDepartamento);
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
