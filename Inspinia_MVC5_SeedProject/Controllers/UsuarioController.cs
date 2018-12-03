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
    public class UsuarioController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Usuario/
        public ActionResult Index()
        {
            //var tbusuario = db.tbUsuario.Include(t => t.tbRolesUsuario);
            return View(/*tbusuario.ToList()*/);
        }

        // GET: /Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuario);
        }

        // GET: /Usuario/Create
        public ActionResult Create()
        {
            ViewBag.rolusu_Id = new SelectList(db.tbRolesUsuario, "rolusu_Id", "rolusu_Id");
            return View();
        }

        // POST: /Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="usu_Id,usu_NombreUsuario,rolusu_Id,usu_Password,usu_Nombre,usu_Apellido,usu_Correo,edo_IdEstado,usu_RazonEstado,usu_EsAdministrador")] tbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                db.tbUsuario.Add(tbUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.rolusu_Id = new SelectList(db.tbRolesUsuario, "usu_Id", "usu_Id", tbUsuario.usu_Id);
            return View(tbUsuario);
        }

        // GET: /Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.rolusu_Id = new SelectList(db.tbRolesUsuario, "usu_Id", "usu_Id", tbUsuario.usu_Id);
            return View(tbUsuario);
        }

        // POST: /Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="usu_Id,usu_NombreUsuario,rolusu_Id,usu_Password,usu_Nombre,usu_Apellido,usu_Correo,edo_IdEstado,usu_RazonEstado,usu_EsAdministrador")] tbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rolusu_Id = new SelectList(db.tbRolesUsuario, "usu_Id", "usu_Id", tbUsuario.usu_Id);
            return View(tbUsuario);
        }

        // GET: /Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuario);
        }

        // POST: /Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            db.tbUsuario.Remove(tbUsuario);
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

    }
}
