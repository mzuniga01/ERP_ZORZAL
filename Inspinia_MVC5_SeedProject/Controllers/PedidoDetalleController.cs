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
    public class PedidoDetalleController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /PedidoDetalle/
        public ActionResult Index()
        {
            var tbpedidodetalle = db.tbPedidoDetalle.Include(t => t.tbProducto);
            return View(tbpedidodetalle.ToList());
        }

        // GET: /PedidoDetalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPedidoDetalle tbPedidoDetalle = db.tbPedidoDetalle.Find(id);
            if (tbPedidoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbPedidoDetalle);
        }

        // GET: /PedidoDetalle/Create
        public ActionResult Create()
        {
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            return View();
        }

        // POST: /PedidoDetalle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="pdet_Id,ped_Codigo,prod_Codigo,ped_Descripcion,pdet_Cantidad,pdet_CantidadEntregada,pdet_UsuarioCrea,pdet_FechaCrea,pdet_UsuarioModifica,pdet_FechaModifica")] tbPedidoDetalle tbPedidoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.tbPedidoDetalle.Add(tbPedidoDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbPedidoDetalle.prod_Codigo);
            return View(tbPedidoDetalle);
        }

        // GET: /PedidoDetalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPedidoDetalle tbPedidoDetalle = db.tbPedidoDetalle.Find(id);
            if (tbPedidoDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbPedidoDetalle.prod_Codigo);
            return View(tbPedidoDetalle);
        }

        // POST: /PedidoDetalle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="pdet_Id,ped_Codigo,prod_Codigo,ped_Descripcion,pdet_Cantidad,pdet_CantidadEntregada,pdet_UsuarioCrea,pdet_FechaCrea,pdet_UsuarioModifica,pdet_FechaModifica")] tbPedidoDetalle tbPedidoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPedidoDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", tbPedidoDetalle.prod_Codigo);
            return View(tbPedidoDetalle);
        }

        // GET: /PedidoDetalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPedidoDetalle tbPedidoDetalle = db.tbPedidoDetalle.Find(id);
            if (tbPedidoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbPedidoDetalle);
        }

        // POST: /PedidoDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPedidoDetalle tbPedidoDetalle = db.tbPedidoDetalle.Find(id);
            db.tbPedidoDetalle.Remove(tbPedidoDetalle);
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
