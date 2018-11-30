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
    public class AccesoRolController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /AccesoRol/
        public ActionResult Index()
        {
            var tbaccesorol = db.tbAccesoRol.Include(t => t.obj_Id).Include(t => t.obj_Id).Include(t => t.tbRoles).Include(t => t.tbRolesUsuario);
            return View(tbaccesorol.ToList());
        }

        public ActionResult CrearPrueba()
        {
            return View();
        }
        public ActionResult DetallePrueba()
        {
            return View();
        }
        public ActionResult EditarPrueba()
        {
            return View();
        }

        // GET: /AccesoRol/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAccesoRol tbAccesoRol = db.tbAccesoRol.Find(id);
            if (tbAccesoRol == null)
            {
                return HttpNotFound();
            }
            return View(tbAccesoRol);
        }

        // GET: /AccesoRol/Create
        public ActionResult Create()
        {
            ViewBag.rol_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla");
            ViewBag.obj_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla");
            ViewBag.rol_Id = new SelectList(db.tbRoles, "rol_Id", "rol_Descripcion");
            ViewBag.rol_Id = new SelectList(db.tbRolesUsuario, "rolusu_Id", "rolusu_Id");
            return View();
        }

        // POST: /AccesoRol/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="acrol_Id,rol_Id,obj_Id,acrol_Descripcion,acrol_UsuarioCrea,acrol_FechaCrea,acrol_UsuarioModifica,acrol_FechaModifica")] tbAccesoRol tbAccesoRol)
        {
            if (ModelState.IsValid)
            {
                db.tbAccesoRol.Add(tbAccesoRol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.rol_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla", tbAccesoRol.rol_Id);
            ViewBag.obj_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla", tbAccesoRol.obj_Id);
            ViewBag.rol_Id = new SelectList(db.tbRoles, "rol_Id", "rol_Descripcion", tbAccesoRol.rol_Id);
            ViewBag.rol_Id = new SelectList(db.tbRolesUsuario, "rolusu_Id", "rolusu_Id", tbAccesoRol.rol_Id);
            return View(tbAccesoRol);
        }

        // GET: /AccesoRol/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAccesoRol tbAccesoRol = db.tbAccesoRol.Find(id);
            if (tbAccesoRol == null)
            {
                return HttpNotFound();
            }
            ViewBag.rol_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla", tbAccesoRol.rol_Id);
            ViewBag.obj_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla", tbAccesoRol.obj_Id);
            ViewBag.rol_Id = new SelectList(db.tbRoles, "rol_Id", "rol_Descripcion", tbAccesoRol.rol_Id);
            ViewBag.rol_Id = new SelectList(db.tbRolesUsuario, "rolusu_Id", "rolusu_Id", tbAccesoRol.rol_Id);
            return View(tbAccesoRol);
        }

        // POST: /AccesoRol/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="acrol_Id,rol_Id,obj_Id,acrol_Descripcion,acrol_UsuarioCrea,acrol_FechaCrea,acrol_UsuarioModifica,acrol_FechaModifica")] tbAccesoRol tbAccesoRol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbAccesoRol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rol_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla", tbAccesoRol.rol_Id);
            ViewBag.obj_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla", tbAccesoRol.obj_Id);
            ViewBag.rol_Id = new SelectList(db.tbRoles, "rol_Id", "rol_Descripcion", tbAccesoRol.rol_Id);
            ViewBag.rol_Id = new SelectList(db.tbRolesUsuario, "rolusu_Id", "rolusu_Id", tbAccesoRol.rol_Id);
            return View(tbAccesoRol);
        }

        // GET: /AccesoRol/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAccesoRol tbAccesoRol = db.tbAccesoRol.Find(id);
            if (tbAccesoRol == null)
            {
                return HttpNotFound();
            }
            return View(tbAccesoRol);
        }

        // POST: /AccesoRol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAccesoRol tbAccesoRol = db.tbAccesoRol.Find(id);
            db.tbAccesoRol.Remove(tbAccesoRol);
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
