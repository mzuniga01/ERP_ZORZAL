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
    public class PedidoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Pedido/
        public ActionResult Index()
        {
            var tbpedido = db.tbPedido.Include(t => t.tbCliente).Include(t => t.tbSucursal);
            return View(tbpedido.ToList());
        }

        // GET: /Pedido/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPedido tbPedido = db.tbPedido.Find(id);
            if (tbPedido == null)
            {
                return HttpNotFound();
            }
            return View(tbPedido);
        }

        // GET: /Pedido/Create
        public ActionResult Create()
        {
            ViewBag.clte_id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_IDT_PASSP");
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id");
            return View();
        }

        // POST: /Pedido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ped_Codigo,ped_Fecha,ped_FechaEntrega,clte_id,sucur_Codigo,ped_UsuarioCrea,ped_FechaCrea,ped_UsuarioModifica,ped_FechaModica")] tbPedido tbPedido)
        {
            if (ModelState.IsValid)
            {
                db.tbPedido.Add(tbPedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clte_id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_IDT_PASSP", tbPedido.clte_Id);
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbPedido.suc_Id);
            return View(tbPedido);
        }

        // GET: /Pedido/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPedido tbPedido = db.tbPedido.Find(id);
            if (tbPedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.clte_id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_IDT_PASSP", tbPedido.clte_Id);
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbPedido.suc_Id);
            return View(tbPedido);
        }

        // POST: /Pedido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ped_Codigo,ped_Fecha,ped_FechaEntrega,clte_id,sucur_Codigo,ped_UsuarioCrea,ped_FechaCrea,ped_UsuarioModifica,ped_FechaModica")] tbPedido tbPedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clte_id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_IDT_PASSP", tbPedido.clte_Id);
            ViewBag.sucur_Codigo = new SelectList(db.tbSucursal, "sucur_Codigo", "mun_Id", tbPedido.suc_Id);
            return View(tbPedido);
        }

        // GET: /Pedido/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPedido tbPedido = db.tbPedido.Find(id);
            if (tbPedido == null)
            {
                return HttpNotFound();
            }
            return View(tbPedido);
        }

        // POST: /Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPedido tbPedido = db.tbPedido.Find(id);
            db.tbPedido.Remove(tbPedido);
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
