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
    public class RolesUsuarioController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /RolesUsuario/
        public ActionResult Index()
        {
            var tbrolesusuario = db.tbRolesUsuario.Include(t => t.tbRol).Include(t => t.tbUsuario);
            return View(tbrolesusuario.ToList());
        }

        // GET: /RolesUsuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRolesUsuario tbRolesUsuario = db.tbRolesUsuario.Find(id);
            if (tbRolesUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbRolesUsuario);
        }

        // GET: /RolesUsuario/Create
        public ActionResult Create()
        {
            ViewBag.rol_Id = new SelectList(db.tbRol, "rol_Id", "rol_Descripcion");
            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: /RolesUsuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="rolu_Id,rol_Id,usu_Id,rolu_UsuarioCrea,rolu_FechaCrea,rolu_UsuarioModifica,rolu_FechaModifica")] tbRolesUsuario tbRolesUsuario)
        {
            if (ModelState.IsValid)
            {
                db.tbRolesUsuario.Add(tbRolesUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.rol_Id = new SelectList(db.tbRol, "rol_Id", "rol_Descripcion", tbRolesUsuario.rol_Id);
            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbRolesUsuario.usu_Id);
            return View(tbRolesUsuario);
        }

        // GET: /RolesUsuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRolesUsuario tbRolesUsuario = db.tbRolesUsuario.Find(id);
            if (tbRolesUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.rol_Id = new SelectList(db.tbRol, "rol_Id", "rol_Descripcion", tbRolesUsuario.rol_Id);
            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbRolesUsuario.usu_Id);
            return View(tbRolesUsuario);
        }

        // POST: /RolesUsuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="rolu_Id,rol_Id,usu_Id,rolu_UsuarioCrea,rolu_FechaCrea,rolu_UsuarioModifica,rolu_FechaModifica")] tbRolesUsuario tbRolesUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbRolesUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rol_Id = new SelectList(db.tbRol, "rol_Id", "rol_Descripcion", tbRolesUsuario.rol_Id);
            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbRolesUsuario.usu_Id);
            return View(tbRolesUsuario);
        }

        // GET: /RolesUsuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRolesUsuario tbRolesUsuario = db.tbRolesUsuario.Find(id);
            if (tbRolesUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbRolesUsuario);
        }

        // POST: /RolesUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbRolesUsuario tbRolesUsuario = db.tbRolesUsuario.Find(id);
            db.tbRolesUsuario.Remove(tbRolesUsuario);
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
