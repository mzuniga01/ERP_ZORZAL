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
    public class TiposSalidaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /TiposSalida/
        public ActionResult Index()
        {
            return View(db.tbTiposSalida.ToList());
        }

        // GET: /TiposSalida/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposSalida tbTiposSalida = db.tbTiposSalida.Find(id);
            if (tbTiposSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposSalida);
        }

        public ActionResult Crear()
        {
            
            return View();
        }

        public ActionResult Editar()
        {

            return View();
        }

        public ActionResult Detalles()
        {

            return View();
        }

        public ActionResult Index2()
        {

            return View();
        }

        public ActionResult Detalles2()
        {

            return View();
        }
        // GET: /TiposSalida/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TiposSalida/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tsal_Id,tsal_Descripcion,tsal_UsuarioCrea,tsal_FechaCrea,tsal_UsuarioModifica,tsal_FechaModifica")] tbTiposSalida tbTiposSalida)
        {
            if (ModelState.IsValid)
            {
                db.tbTiposSalida.Add(tbTiposSalida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTiposSalida);
        }

        // GET: /TiposSalida/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposSalida tbTiposSalida = db.tbTiposSalida.Find(id);
            if (tbTiposSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposSalida);
        }

        // POST: /TiposSalida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="tsal_Id,tsal_Descripcion,tsal_UsuarioCrea,tsal_FechaCrea,tsal_UsuarioModifica,tsal_FechaModifica")] tbTiposSalida tbTiposSalida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTiposSalida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTiposSalida);
        }

        // GET: /TiposSalida/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTiposSalida tbTiposSalida = db.tbTiposSalida.Find(id);
            if (tbTiposSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbTiposSalida);
        }

        // POST: /TiposSalida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbTiposSalida tbTiposSalida = db.tbTiposSalida.Find(id);
            db.tbTiposSalida.Remove(tbTiposSalida);
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
