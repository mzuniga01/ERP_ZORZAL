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
    public class tbPedidosController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbPedidos/
        public ActionResult Index()
        {
            return View(db.tbPedidos.ToList());
        }

        // GET: /tbPedidos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPedidos tbPedidos = db.tbPedidos.Find(id);
            if (tbPedidos == null)
            {
                return HttpNotFound();
            }
            return View(tbPedidos);
        }

        // GET: /tbPedidos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbPedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ped_CodPedido,su_CodSucursal,ped_FechaPedido,ped_FechaEntrega,ctle_RTN")] tbPedidos tbPedidos)
        {
            if (ModelState.IsValid)
            {
                db.tbPedidos.Add(tbPedidos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbPedidos);
        }

        // GET: /tbPedidos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPedidos tbPedidos = db.tbPedidos.Find(id);
            if (tbPedidos == null)
            {
                return HttpNotFound();
            }
            return View(tbPedidos);
        }

        // POST: /tbPedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ped_CodPedido,su_CodSucursal,ped_FechaPedido,ped_FechaEntrega,ctle_RTN")] tbPedidos tbPedidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPedidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbPedidos);
        }

        // GET: /tbPedidos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPedidos tbPedidos = db.tbPedidos.Find(id);
            if (tbPedidos == null)
            {
                return HttpNotFound();
            }
            return View(tbPedidos);
        }

        // POST: /tbPedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbPedidos tbPedidos = db.tbPedidos.Find(id);
            db.tbPedidos.Remove(tbPedidos);
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
