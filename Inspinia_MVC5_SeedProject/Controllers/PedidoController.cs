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
    public class PedidoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Pedido/
        public ActionResult Index()
        {
            var tbpedido = db.tbPedido.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbEstadoPedido).Include(t => t.tbFactura).Include(t => t.tbSucursal);
            return View(tbpedido.ToList());
        }
        public ActionResult IndexFacturar()
        {
            var tbpedido = db.tbPedido.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbEstadoPedido).Include(t => t.tbFactura).Include(t => t.tbSucursal);
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
            //ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.ped_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte");
            //ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo");
            //ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");
            
            ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion");
            ViewBag.Cliente = db.tbCliente.ToList();
            return View();
        }

        // POST: /Pedido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "ped_Id,esped_Id,ped_FechaElaboracion,ped_FechaEntrega,clte_Id,suc_Id,fact_Id,ped_UsuarioCrea,ped_FechaCrea,ped_UsuarioModifica,ped_FechaModifica")] tbPedido tbPedido)
        {
            if (ModelState.IsValid)
            {
                db.tbPedido.Add(tbPedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
            ViewBag.ped_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioModifica);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbPedido.clte_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPedido.fact_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbPedido.suc_Id);
            ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion", tbPedido.esped_Id);
      
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
            ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
            ViewBag.ped_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioModifica);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbPedido.clte_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPedido.fact_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbPedido.suc_Id);
            ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion", tbPedido.esped_Id);
            ViewBag.Producto = db.tbProducto.ToList();
            return View(tbPedido);
        }

        // POST: /Pedido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "ped_Id,esped_Id,ped_FechaElaboracion,ped_FechaEntrega,clte_Id,suc_Id,fact_Id,ped_UsuarioCrea,ped_FechaCrea,ped_UsuarioModifica,ped_FechaModifica")] tbPedido tbPedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
            ViewBag.ped_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioModifica);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbPedido.clte_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPedido.fact_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbPedido.suc_Id);
            ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion", tbPedido.esped_Id);
            ViewBag.Producto = db.tbProducto.ToList();
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
