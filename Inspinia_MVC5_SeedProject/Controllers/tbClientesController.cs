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
    public class tbClientesController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbClientes/
        public ActionResult Index()
        {
            var tbclientes = db.tbClientes.Include(t => t.tbPersonasJuridicas);
            return View(tbclientes.ToList());
        }

        // GET: /tbClientes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbClientes tbClientes = db.tbClientes.Find(id);
            if (tbClientes == null)
            {
                return HttpNotFound();
            }
            return View(tbClientes);
        }

        // GET: /tbClientes/Create
        public ActionResult Create()
        {
            ViewBag.clte_RTN = new SelectList(db.tbPersonasJuridicas, "clte_RTN", "clte_RazonSocial");
            return View();
        }

        // POST: /tbClientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="clte_RTN")] tbClientes tbClientes)
        {
            if (ModelState.IsValid)
            {
                db.tbClientes.Add(tbClientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clte_RTN = new SelectList(db.tbPersonasJuridicas, "clte_RTN", "clte_RazonSocial", tbClientes.clte_RTN);
            return View(tbClientes);
        }

        // GET: /tbClientes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbClientes tbClientes = db.tbClientes.Find(id);
            if (tbClientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.clte_RTN = new SelectList(db.tbPersonasJuridicas, "clte_RTN", "clte_RazonSocial", tbClientes.clte_RTN);
            return View(tbClientes);
        }

        // POST: /tbClientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="clte_RTN")] tbClientes tbClientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbClientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clte_RTN = new SelectList(db.tbPersonasJuridicas, "clte_RTN", "clte_RazonSocial", tbClientes.clte_RTN);
            return View(tbClientes);
        }

        // GET: /tbClientes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbClientes tbClientes = db.tbClientes.Find(id);
            if (tbClientes == null)
            {
                return HttpNotFound();
            }
            return View(tbClientes);
        }

        // POST: /tbClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbClientes tbClientes = db.tbClientes.Find(id);
            db.tbClientes.Remove(tbClientes);
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
