using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_ZORZAL.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class SucursalsController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Sucursals/
        public ActionResult Index()
        {
            return View(db.tbSucursal.ToList());
        }

        // GET: /Sucursals/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSucursal tbSucursal = db.tbSucursal.Find(id);
            if (tbSucursal == null)
            {
                return HttpNotFound();
            }
            return View(tbSucursal);
        }

        // GET: /Sucursals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Sucursals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="sucur_Codigo,mun_Id,sucur_Correo,sucur_Direccion,sucur_Telefono,sucur_UsuarioCrea,sucur_FechaCrea,sucur_UsuarioModifica,sucur_FechaModifica")] tbSucursal tbSucursal)
        {
            if (ModelState.IsValid)
            {
                db.tbSucursal.Add(tbSucursal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbSucursal);
        }

        // GET: /Sucursals/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSucursal tbSucursal = db.tbSucursal.Find(id);
            if (tbSucursal == null)
            {
                return HttpNotFound();
            }
            return View(tbSucursal);
        }

        // POST: /Sucursals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="sucur_Codigo,mun_Id,sucur_Correo,sucur_Direccion,sucur_Telefono,sucur_UsuarioCrea,sucur_FechaCrea,sucur_UsuarioModifica,sucur_FechaModifica")] tbSucursal tbSucursal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSucursal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbSucursal);
        }

        // GET: /Sucursals/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSucursal tbSucursal = db.tbSucursal.Find(id);
            if (tbSucursal == null)
            {
                return HttpNotFound();
            }
            return View(tbSucursal);
        }

        // POST: /Sucursals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbSucursal tbSucursal = db.tbSucursal.Find(id);
            db.tbSucursal.Remove(tbSucursal);
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
