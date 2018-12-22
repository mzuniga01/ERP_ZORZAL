using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class FacturaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Factura/
        public ActionResult Index()
        {
            var tbfactura = db.tbFactura.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCaja).Include(t => t.tbCliente).Include(t => t.tbEstadoFactura).Include(t => t.tbSucursal);
            return View(tbfactura.ToList());
        }

        public ActionResult IndexFacturar()
        {

            return View(db.tbPedido.ToList());
        }


        // GET: /Factura/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }
        public ActionResult _IndexCliente()
        {
            return PartialView();
        }

        // GET: /Factura/Create
        public ActionResult Create()
        {
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion");
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion");
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");
            ViewBag.Cliente = db.tbCliente.ToList();
            return View();
        }

        // POST: /Factura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="fact_Id,fact_Codigo,fact_Fecha,esfac_Id,cja_Id,suc_Id,clte_Id,pemi_NumeroCAI,fact_AlCredito,fact_DiasCredito,fact_PorcentajeDescuento,fact_AutorizarDescuento,fact_Vendedor,clte_Identificacion,clte_Nombres,fact_UsuarioCrea,fact_FechaCrea,fact_UsuarioModifica,fact_FechaModifica")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    long MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbFactura_Insert(
                        tbFactura.fact_Codigo,
                        tbFactura.fact_Fecha,
                        tbFactura.esfac_Id,
                        tbFactura.cja_Id,
                        tbFactura.suc_Id,
                        tbFactura.clte_Id,
                        tbFactura.pemi_NumeroCAI,
                        tbFactura.fact_AlCredito,
                        tbFactura.fact_DiasCredito,
                        tbFactura.fact_PorcentajeDescuento,
                        tbFactura.fact_AutorizarDescuento,
                        tbFactura.fact_Vendedor,
                        tbFactura.clte_Identificacion,
                        tbFactura.clte_Nombres);
                    foreach (UDP_Vent_tbFactura_Insert_Result Factura in list)
                        MensajeError = Factura.MensajeError;
                    if (MensajeError == -1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
                    ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion");
                    ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");
                    ViewBag.Producto = db.tbProducto.ToList();
                    ViewBag.Cliente = db.tbCliente.ToList();
                    ViewBag.Cliente = db.tbCliente.ToList();
                }

                return RedirectToAction("Index");
            }

            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbFactura.cja_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbFactura.clte_Id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbFactura.suc_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
            return View(tbFactura);
        }

        // GET: /Factura/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbFactura.cja_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbFactura.clte_Id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbFactura.suc_Id);
            return View(tbFactura);
        }

        // POST: /Factura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="fact_Id,fact_Codigo,fact_Fecha,esfac_Id,cja_Id,suc_Id,clte_Id,pemi_NumeroCAI,fact_AlCredito,fact_DiasCredito,fact_PorcentajeDescuento,fact_AutorizarDescuento,fact_Vendedor,clte_Identificacion,clte_Nombres,fact_UsuarioCrea,fact_FechaCrea,fact_UsuarioModifica,fact_FechaModifica")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    long MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbFactura_Update(
                        tbFactura.fact_Id,
                        tbFactura.fact_Codigo,
                        tbFactura.fact_Fecha,
                        tbFactura.pemi_NumeroCAI,
                        tbFactura.fact_AlCredito,
                        tbFactura.fact_DiasCredito,
                        tbFactura.fact_PorcentajeDescuento,
                        tbFactura.fact_AutorizarDescuento,
                        tbFactura.fact_Vendedor,
                        tbFactura.clte_Identificacion,
                        tbFactura.clte_Nombres,
                        tbFactura.fact_UsuarioCrea,
                        tbFactura.fact_FechaCrea);
                    foreach (UDP_Vent_tbFactura_Update_Result Factura in list)
                        MensajeError = Factura.MensajeError;
                    if (MensajeError == -1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
                    ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion");
                    ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");
                    ViewBag.Producto = db.tbProducto.ToList();
                    ViewBag.Cliente = db.tbCliente.ToList();
                }

                return RedirectToAction("Index");
            }
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbFactura.cja_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbFactura.clte_Id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbFactura.suc_Id);
            return View(tbFactura);
        }

        // GET: /Factura/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }

        // POST: /Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tbFactura tbFactura = db.tbFactura.Find(id);
            db.tbFactura.Remove(tbFactura);
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
        public JsonResult GetEmpleados(string term)
        {
            var results = db.UDV_Inv_Nombre_Empleado.Where(s => term == null || s.Empleados.ToLower().Contains(term.ToLower())).Select(x => new { id = x.emp_Id, value = x.Empleados }).Take(5).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}
