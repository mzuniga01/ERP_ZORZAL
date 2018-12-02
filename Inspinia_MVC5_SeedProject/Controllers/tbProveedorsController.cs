using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_ZORZAL.Models;

namespace ERP_ZORZAL.Controllers
{
    public class tbProveedorsController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbProveedors/
        public ActionResult Index()
        {
            return View(db.tbProveedor.ToList());
        }

        // GET: /tbProveedors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return HttpNotFound();
            }
            return View(tbProveedor);
        }

        // GET: /tbProveedors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbProveedors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="prov_Id,prov_Nombre,prov_NombreContacto,prov_Direccion,prov_Email,prov_Telefono,prov_UsuarioCrea,prov_FechaCrea,prov_UsuarioModifica,prov_FechaModifica")] tbProveedor tbProveedor)
        {
            if (ModelState.IsValid)
            {
                db.tbProveedor.Add(tbProveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbProveedor);
        }

        // GET: /tbProveedors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return HttpNotFound();
            }
            return View(tbProveedor);
        }

        // POST: /tbProveedors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="prov_Id,prov_Nombre,prov_NombreContacto,prov_Direccion,prov_Email,prov_Telefono,prov_UsuarioCrea,prov_FechaCrea,prov_UsuarioModifica,prov_FechaModifica")] tbProveedor tbProveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbProveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbProveedor);
        }

        // GET: /tbProveedors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return HttpNotFound();
            }
            return View(tbProveedor);
        }

        // POST: /tbProveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            db.tbProveedor.Remove(tbProveedor);
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
