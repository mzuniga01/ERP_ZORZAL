using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class InventarioFisicoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /InventarioFisico/
        public ActionResult Index()
        {
            var tbinventariofisico = db.tbInventarioFisico.Include(t => t.tbEstadoInventarioFisico);
            return View(tbinventariofisico.ToList());
        }

        // GET: /InventarioFisico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            if (tbInventarioFisico == null)
            {
                return HttpNotFound();
            }
            return View(tbInventarioFisico);
        }

        // GET: /InventarioFisico/Create
        public ActionResult Create()
        {
            ViewBag.estif_Id = new SelectList(db.tbEstadoInventarioFisico, "estif_Id", "estif_Descripcion");
            return View();
        }

        public ActionResult Detalle()
        {
            return View();
        }

        public ActionResult Editar()
        {
            return View();
        }
        // POST: /InventarioFisico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="invf_Id,invf_Descripcion,invf_ResponsableBodega,bod_Id,estif_Id,invf_FechaInventario,invf_UsuarioCrea,invf_FechaCrea,invf_UsuarioModifica,invf_FechaModifica")] tbInventarioFisico tbInventarioFisico)
        {
            if (ModelState.IsValid)
            {
                db.tbInventarioFisico.Add(tbInventarioFisico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.estif_Id = new SelectList(db.tbEstadoInventarioFisico, "estif_Id", "estif_Descripcion", tbInventarioFisico.estif_Id);
            return View(tbInventarioFisico);
        }

        // GET: /InventarioFisico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            if (tbInventarioFisico == null)
            {
                return HttpNotFound();
            }
            ViewBag.estif_Id = new SelectList(db.tbEstadoInventarioFisico, "estif_Id", "estif_Descripcion", tbInventarioFisico.estif_Id);
            return View(tbInventarioFisico);
        }

        // POST: /InventarioFisico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="invf_Id,invf_Descripcion,invf_ResponsableBodega,bod_Id,estif_Id,invf_FechaInventario,invf_UsuarioCrea,invf_FechaCrea,invf_UsuarioModifica,invf_FechaModifica")] tbInventarioFisico tbInventarioFisico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbInventarioFisico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.estif_Id = new SelectList(db.tbEstadoInventarioFisico, "estif_Id", "estif_Descripcion", tbInventarioFisico.estif_Id);
            return View(tbInventarioFisico);
        }

        // GET: /InventarioFisico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            if (tbInventarioFisico == null)
            {
                return HttpNotFound();
            }
            return View(tbInventarioFisico);
        }

        // POST: /InventarioFisico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            db.tbInventarioFisico.Remove(tbInventarioFisico);
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
