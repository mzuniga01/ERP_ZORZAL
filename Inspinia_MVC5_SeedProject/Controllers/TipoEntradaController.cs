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
    public class TipoEntradaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /TipoEntrada/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /TipoEntrada/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoEntrada tbTipoEntrada = db.tbTipoEntrada.Find(id);
            if (tbTipoEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoEntrada);
        }

        // GET: /TipoEntrada/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TipoEntrada/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tent_Id,tent_Descripcion,tent_UsuarioCrea,tent_FechaCrea,tent_UsuarioModifica,tent_FechaModifica")] tbTipoEntrada tbTipoEntrada)
        {
            if (ModelState.IsValid)
            {
                db.tbTipoEntrada.Add(tbTipoEntrada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTipoEntrada);
        }

        // GET: /TipoEntrada/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoEntrada tbTipoEntrada = db.tbTipoEntrada.Find(id);
            if (tbTipoEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoEntrada);
        }

        // POST: /TipoEntrada/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="tent_Id,tent_Descripcion,tent_UsuarioCrea,tent_FechaCrea,tent_UsuarioModifica,tent_FechaModifica")] tbTipoEntrada tbTipoEntrada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTipoEntrada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTipoEntrada);
        }

        // GET: /TipoEntrada/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoEntrada tbTipoEntrada = db.tbTipoEntrada.Find(id);
            if (tbTipoEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoEntrada);
        }

        // POST: /TipoEntrada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbTipoEntrada tbTipoEntrada = db.tbTipoEntrada.Find(id);
            db.tbTipoEntrada.Remove(tbTipoEntrada);
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
