using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_ZORZAL.Controllers
{
    public class UsuarioController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Usuario/
        public ActionResult Index()
        {
            return View(db.tbUsuario.ToList());
        }

        public ActionResult ModificarPass(int? id)
        {
            ViewBag.UserID = id;
           return View();
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
            return View();
        }

        // POST: /Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="usu_Id,usu_NombreUsuario,usu_Password,usu_Nombres,usu_Apellidos,usu_Correo,usu_EsActivo,usu_RazonInactivo,usu_EsAdministrador")] tbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                db.tbUsuario.Add(tbUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            ViewBag.User_ID = id;
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuario);
        }

        // POST: /Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="usu_Id,usu_NombreUsuario,usu_Password,usu_Nombres,usu_Apellidos,usu_Correo,usu_EsActivo,usu_RazonInactivo,usu_EsAdministrador")] tbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
    }
}
