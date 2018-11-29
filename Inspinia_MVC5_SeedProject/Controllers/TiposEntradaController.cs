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
    public class TiposEntradaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /TiposEntrada/
        public ActionResult Index()
        {
            return View(db.tbTiposEntrada.ToList());
        }

        // GET: /TiposEntrada/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposEntrada tbTiposEntrada = db.tbTiposEntrada.Find(id);
            if (tbTiposEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposEntrada);
        }

        // GET: /TiposEntrada/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TiposEntrada/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tent_Id,tent_Descripcion,tent_UsuarioCrea,tent_FechaCrea,tent_UsuarioModifica,tent_FechaModifica")] tbTiposEntrada tbTiposEntrada)
        {
            if (ModelState.IsValid)
            {
                db.tbTiposEntrada.Add(tbTiposEntrada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTiposEntrada);
        }

        // GET: /TiposEntrada/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposEntrada tbTiposEntrada = db.tbTiposEntrada.Find(id);
            if (tbTiposEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposEntrada);
        }

        // POST: /TiposEntrada/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="tent_Id,tent_Descripcion,tent_UsuarioCrea,tent_FechaCrea,tent_UsuarioModifica,tent_FechaModifica")] tbTiposEntrada tbTiposEntrada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTiposEntrada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTiposEntrada);
        }

        // GET: /TiposEntrada/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposEntrada tbTiposEntrada = db.tbTiposEntrada.Find(id);
            if (tbTiposEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposEntrada);
        }

        // POST: /TiposEntrada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbTiposEntrada tbTiposEntrada = db.tbTiposEntrada.Find(id);
            db.tbTiposEntrada.Remove(tbTiposEntrada);
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

        public ActionResult index_Tent()
        {
            return View();
        }
        public ActionResult Editar()
        {
            return View();
        }
        public ActionResult Crear()
        {
            return View();
        }
        public ActionResult Detalles()
        {
            return View();
        }

    }
}
