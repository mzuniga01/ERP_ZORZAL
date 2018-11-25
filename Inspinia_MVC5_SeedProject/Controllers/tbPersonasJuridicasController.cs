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
    public class tbPersonasJuridicasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbPersonasJuridicas/
        public ActionResult Index()
        {
            var tbpersonasjuridicas = db.tbPersonasJuridicas.Include(t => t.tbClientes);
            return View(tbpersonasjuridicas.ToList());
        }

        // GET: /tbPersonasJuridicas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPersonasJuridicas tbPersonasJuridicas = db.tbPersonasJuridicas.Find(id);
            if (tbPersonasJuridicas == null)
            {
                return HttpNotFound();
            }
            return View(tbPersonasJuridicas);
        }

        // GET: /tbPersonasJuridicas/Create
        public ActionResult Create()
        {
            ViewBag.clte_RTN = new SelectList(db.tbClientes, "clte_RTN", "clte_RTN");
            return View();
        }

        // POST: /tbPersonasJuridicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="clte_RTN,clte_RazonSocial,clte_RazonComercial,clte_Telefono,clte_Email")] tbPersonasJuridicas tbPersonasJuridicas)
        {
            if (ModelState.IsValid)
            {
                db.tbPersonasJuridicas.Add(tbPersonasJuridicas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clte_RTN = new SelectList(db.tbClientes, "clte_RTN", "clte_RTN", tbPersonasJuridicas.clte_RTN);
            return View(tbPersonasJuridicas);
        }

        // GET: /tbPersonasJuridicas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPersonasJuridicas tbPersonasJuridicas = db.tbPersonasJuridicas.Find(id);
            if (tbPersonasJuridicas == null)
            {
                return HttpNotFound();
            }
            ViewBag.clte_RTN = new SelectList(db.tbClientes, "clte_RTN", "clte_RTN", tbPersonasJuridicas.clte_RTN);
            return View(tbPersonasJuridicas);
        }

        // POST: /tbPersonasJuridicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="clte_RTN,clte_RazonSocial,clte_RazonComercial,clte_Telefono,clte_Email")] tbPersonasJuridicas tbPersonasJuridicas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPersonasJuridicas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clte_RTN = new SelectList(db.tbClientes, "clte_RTN", "clte_RTN", tbPersonasJuridicas.clte_RTN);
            return View(tbPersonasJuridicas);
        }

        // GET: /tbPersonasJuridicas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPersonasJuridicas tbPersonasJuridicas = db.tbPersonasJuridicas.Find(id);
            if (tbPersonasJuridicas == null)
            {
                return HttpNotFound();
            }
            return View(tbPersonasJuridicas);
        }

        // POST: /tbPersonasJuridicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbPersonasJuridicas tbPersonasJuridicas = db.tbPersonasJuridicas.Find(id);
            db.tbPersonasJuridicas.Remove(tbPersonasJuridicas);
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
