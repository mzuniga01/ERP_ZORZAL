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
    public class InventarioFisicoDetalleController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /InventarioFisicoDetalle/
        public ActionResult Index()
        {
            var tbinventariofisicodetalle = db.tbInventarioFisicoDetalle.Include(t => t.tbUnidadesMedida).Include(t => t.tbProducto).Include(t => t.tbInventarioFisico);
            return View(tbinventariofisicodetalle.ToList());
        }

        // GET: /InventarioFisicoDetalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioFisicoDetalle tbInventarioFisicoDetalle = db.tbInventarioFisicoDetalle.Find(id);
            if (tbInventarioFisicoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbInventarioFisicoDetalle);
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

        // GET: /InventarioFisicoDetalle/Create
        public ActionResult Create()
        {
            ViewBag.uni_Id = new SelectList(db.tbUnidadesMedida, "uni_Id", "uni_Descripcion");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.invf_Id = new SelectList(db.tbInventarioFisico, "invf_Id", "invf_Descripcion");
            return View();
        }

        // POST: /InventarioFisicoDetalle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="invfd_Id,invf_Id,prod_Codigo,invfd_Cantidad,uni_Id,invfd_UsuarioCrea,invfd_FechaCrea,invfd_UsuarioModifica,invfd_FechaModifica")] tbInventarioFisicoDetalle tbInventarioFisicoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.tbInventarioFisicoDetalle.Add(tbInventarioFisicoDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.uni_Id = new SelectList(db.tbUnidadesMedida, "uni_Id", "uni_Descripcion", tbInventarioFisicoDetalle.uni_Id);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbInventarioFisicoDetalle.prod_Codigo);
            ViewBag.invf_Id = new SelectList(db.tbInventarioFisico, "invf_Id", "invf_Descripcion", tbInventarioFisicoDetalle.invf_Id);
            return View(tbInventarioFisicoDetalle);
        }

        // GET: /InventarioFisicoDetalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioFisicoDetalle tbInventarioFisicoDetalle = db.tbInventarioFisicoDetalle.Find(id);
            if (tbInventarioFisicoDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.uni_Id = new SelectList(db.tbUnidadesMedida, "uni_Id", "uni_Descripcion", tbInventarioFisicoDetalle.uni_Id);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbInventarioFisicoDetalle.prod_Codigo);
            ViewBag.invf_Id = new SelectList(db.tbInventarioFisico, "invf_Id", "invf_Descripcion", tbInventarioFisicoDetalle.invf_Id);
            return View(tbInventarioFisicoDetalle);
        }

        // POST: /InventarioFisicoDetalle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="invfd_Id,invf_Id,prod_Codigo,invfd_Cantidad,uni_Id,invfd_UsuarioCrea,invfd_FechaCrea,invfd_UsuarioModifica,invfd_FechaModifica")] tbInventarioFisicoDetalle tbInventarioFisicoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbInventarioFisicoDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.uni_Id = new SelectList(db.tbUnidadesMedida, "uni_Id", "uni_Descripcion", tbInventarioFisicoDetalle.uni_Id);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbInventarioFisicoDetalle.prod_Codigo);
            ViewBag.invf_Id = new SelectList(db.tbInventarioFisico, "invf_Id", "invf_Descripcion", tbInventarioFisicoDetalle.invf_Id);
            return View(tbInventarioFisicoDetalle);
        }

        // GET: /InventarioFisicoDetalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioFisicoDetalle tbInventarioFisicoDetalle = db.tbInventarioFisicoDetalle.Find(id);
            if (tbInventarioFisicoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbInventarioFisicoDetalle);
        }

        // POST: /InventarioFisicoDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInventarioFisicoDetalle tbInventarioFisicoDetalle = db.tbInventarioFisicoDetalle.Find(id);
            db.tbInventarioFisicoDetalle.Remove(tbInventarioFisicoDetalle);
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
