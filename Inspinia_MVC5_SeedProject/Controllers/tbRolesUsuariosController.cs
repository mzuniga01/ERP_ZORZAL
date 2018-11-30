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
    public class tbRolesUsuariosController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /tbRolesUsuarios/
        public ActionResult Index()
        {
            return View(db.tbRolesUsuario.ToList());
        }

        // GET: /tbRolesUsuarios/Details/5
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

        // GET: /tbRolesUsuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /tbRolesUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="rolusu_Id,rol_Id,usu_Id,rolusu_UsuarioCrea,rolusu_FechaCrea,rolusu_UsuarioModifica,rolusu_FechaModifica")] tbRolesUsuario tbRolesUsuario)
        {
            if (ModelState.IsValid)
            {
                db.tbRolesUsuario.Add(tbRolesUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbRolesUsuario);
        }

        // GET: /tbRolesUsuarios/Edit/5
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
            return View(tbRolesUsuario);
        }

        // POST: /tbRolesUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="rolusu_Id,rol_Id,usu_Id,rolusu_UsuarioCrea,rolusu_FechaCrea,rolusu_UsuarioModifica,rolusu_FechaModifica")] tbRolesUsuario tbRolesUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbRolesUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbRolesUsuario);
        }

        // GET: /tbRolesUsuarios/Delete/5
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

        // POST: /tbRolesUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbRolesUsuario tbRolesUsuario = db.tbRolesUsuario.Find(id);
            db.tbRolesUsuario.Remove(tbRolesUsuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult editar()
        {
            
            return View("editar");
        }

        public ActionResult detalles()
        {

            return View("detalles");
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
