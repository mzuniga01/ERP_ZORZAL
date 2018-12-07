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
    public class RolController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Rol/
        public ActionResult Index()
        {
            return View(db.tbRol.ToList());
        }

        public ActionResult _IndexAccesoRol()
        {
            return View();
        }

        public ActionResult _EditAccesoRol()
        {
            return View();
        }

        public ActionResult _IndexAccesoRol_Botones()
        {
            return View();
        }

        public ActionResult _IndexAccesoRol_Create()
        {
            return View();
        }

        public ActionResult _DetailsAccesoRol()
        {
            return PartialView();
        }

        public ActionResult _CreateAccesoRol()
        {
            
            return View();
        }

        // GET: /Rol/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRol tbRol = db.tbRol.Find(id);
            if (tbRol == null)
            {
                return HttpNotFound();
            }
            return View(tbRol);
        }

        // GET: /Rol/Create
        public ActionResult Create()
        {
            ViewBag.obj_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla");
            return View();
        }

        // POST: /Rol/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="rol_Id,rol_Descripcion,rol_UsuarioCrea,rol_FechaCrea,rol_UsuarioModifica,rol_FechaModifica")] tbRol tbRol)
        {
            if (ModelState.IsValid)
            {
                db.tbRol.Add(tbRol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbRol);
        }

        // GET: /Rol/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.obj_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla");
            tbRol tbRol = db.tbRol.Find(id);
            if (tbRol == null)
            {
                return HttpNotFound();
            }
            return View(tbRol);
        }

        // POST: /Rol/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="rol_Id,rol_Descripcion,rol_UsuarioCrea,rol_FechaCrea,rol_UsuarioModifica,rol_FechaModifica")] tbRol tbRol)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(tbRol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbRol);
        }

        // GET: /Rol/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRol tbRol = db.tbRol.Find(id);
            if (tbRol == null)
            {
                return HttpNotFound();
            }
            return View(tbRol);
        }

        // POST: /Rol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbRol tbRol = db.tbRol.Find(id);
            db.tbRol.Remove(tbRol);
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
