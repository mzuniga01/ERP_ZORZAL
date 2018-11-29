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
    public class RolesController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Roles/
        public ActionResult Index()
        {
            return View(db.tbRoles.ToList());
        }

        // GET: /Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRoles tbRoles = db.tbRoles.Find(id);
            if (tbRoles == null)
            {
                return HttpNotFound();
            }
            return View(tbRoles);
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Detalle ()
        {
            return View();
        }
        public ActionResult Editar()
        {
            return View();
        }
        // POST: /Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="rol_Id,rol_Descripcion,rol_UsuarioCrea,rol_FechaCrea,rol_UsuarioModifica,rol_FechaModifica")] tbRoles tbRoles)
        {
            if (ModelState.IsValid)
            {
                db.tbRoles.Add(tbRoles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbRoles);
        }

        // GET: /Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRoles tbRoles = db.tbRoles.Find(id);
            if (tbRoles == null)
            {
                return HttpNotFound();
            }
            return View(tbRoles);
        }

        // POST: /Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="rol_Id,rol_Descripcion,rol_UsuarioCrea,rol_FechaCrea,rol_UsuarioModifica,rol_FechaModifica")] tbRoles tbRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbRoles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbRoles);
        }

        // GET: /Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRoles tbRoles = db.tbRoles.Find(id);
            if (tbRoles == null)
            {
                return HttpNotFound();
            }
            return View(tbRoles);
        }

        // POST: /Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbRoles tbRoles = db.tbRoles.Find(id);
            db.tbRoles.Remove(tbRoles);
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
