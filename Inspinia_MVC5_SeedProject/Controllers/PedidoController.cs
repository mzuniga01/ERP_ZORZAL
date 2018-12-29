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
            var tbpedido = db.tbPedido.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbEstadoPedido).Include(t => t.tbSucursal);
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
            ViewBag.Producto = db.tbProducto.ToList();
            tbPedido Pedido = new tbPedido();
            Pedido.suc_Id = 1;
            return View(Pedido);
        }

        // POST: /Pedido/Create
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "esped_Id,ped_FechaElaboracion,ped_FechaEntrega,clte_Id,suc_Id,fact_Id")] tbPedido tbPedido)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //db.tbTipoIdentificacion.Add(tbTipoIdentificacion);
                    //db.SaveChanges();
                    //return RedirectToAction("Index");

                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbPedido_Insert(tbPedido.esped_Id,
                                                       tbPedido.ped_FechaElaboracion,
                                                       tbPedido.ped_FechaEntrega,
                                                       tbPedido.clte_Id,
                                                       tbPedido.suc_Id,
                                                       tbPedido.fact_Id);
                    foreach (UDP_Vent_tbPedido_Insert_Result Pedido in list)
                        MensajeError = Pedido.MensajeError;
                    if (MensajeError == "-1")
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    Ex.Message.ToString();
                }

                ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
                ViewBag.ped_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioModifica);
                ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbPedido.clte_Id);
                ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPedido.fact_Id);
                ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbPedido.suc_Id);
                ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
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
        public ActionResult Edit(int? id,[Bind(Include= "ped_Id,esped_Id,ped_FechaElaboracion,ped_FechaEntrega,clte_Id,suc_Id,fact_Id,ped_UsuarioCrea,ped_FechaCrea")] tbPedido tbPedido)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tbPedido vPedido = db.tbPedido.Find(id);
                    //db.tbTipoIdentificacion.Add(tbTipoIdentificacion);
                    //db.SaveChanges();
                    //return RedirectToAction("Index");

                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbPedido_Update(tbPedido.ped_Id,
                                                       tbPedido.esped_Id,
                                                       tbPedido.ped_FechaElaboracion,
                                                       tbPedido.ped_FechaEntrega,
                                                       tbPedido.clte_Id,
                                                       tbPedido.suc_Id,
                                                       tbPedido.fact_Id,
                                                       vPedido.ped_UsuarioCrea,
                                                       vPedido.ped_FechaCrea);
                    foreach (UDP_Vent_tbPedido_Update_Result Pedido in list)
                        MensajeError = Pedido.MensajeError;
                    if (MensajeError == "-1")
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    Ex.Message.ToString();
                }

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
