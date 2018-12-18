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
    public class PagoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Pago/
        public ActionResult Index()
        {
            var tbpago = db.tbPago.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCuentasBanco).Include(t => t.tbFactura).Include(t => t.tbTipoPago);
            return View(tbpago.ToList());
        }

        // GET: /Pago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPago tbPago = db.tbPago.Find(id);
            if (tbPago == null)
            {
                return HttpNotFound();
            }
            return View(tbPago);
        }

        // GET: /Pago/Create
        public ActionResult Create()
        {
            ViewBag.pago_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.pago_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.bcta_Id = new SelectList(db.tbCuentasBanco, "bcta_Id", "bcta_Numero");
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo");
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion");

            ViewBag.Factura = db.tbFactura.ToList();
            ViewBag.Cliente = db.tbCliente.ToList();
            return View();
        }

        // POST: /Pago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="pago_Id,fact_Id,tpa_Id,pago_FechaElaboracion,pago_SaldoAnterior,pago_TotalPago,pago_TotalCambio,pago_Emisor,bcta_Id,pago_FechaVencimiento,pago_Titular,pago_UsuarioCrea,pago_FechaCrea,pago_UsuarioModifica,pago_FechaModifica")] tbPago tbPago)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbPago_Insert(tbPago.fact_Id, tbPago.tpa_Id, tbPago.pago_FechaElaboracion, tbPago.pago_SaldoAnterior, tbPago.pago_TotalPago, tbPago.pago_TotalCambio, tbPago.pago_Emisor, tbPago.bcta_Id, tbPago.pago_FechaVencimiento, tbPago.pago_Titular);
                    foreach (UDP_Vent_tbPago_Insert_Result pago in list)
                        MensajeError = pago.MensajeError;
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
                    ModelState.AddModelError("", "Error al agregar el registro " + Ex.Message.ToString());
                    return View(tbPago);
                }
              
            }

            ViewBag.pago_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPago.pago_UsuarioCrea);
            ViewBag.pago_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPago.pago_UsuarioModifica);
            ViewBag.bcta_Id = new SelectList(db.tbCuentasBanco, "bcta_Id", "bcta_Numero", tbPago.bcta_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPago.fact_Id);
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion", tbPago.tpa_Id);
            return View(tbPago);
        }

        // GET: /Pago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPago tbPago = db.tbPago.Find(id);
            if (tbPago == null)
            {
                return HttpNotFound();
            }
            ViewBag.pago_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPago.pago_UsuarioCrea);
            ViewBag.pago_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPago.pago_UsuarioModifica);
            ViewBag.bcta_Id = new SelectList(db.tbCuentasBanco, "bcta_Id", "bcta_Numero", tbPago.bcta_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPago.fact_Id);
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion", tbPago.tpa_Id);
            return View(tbPago);
        }

        // POST: /Pago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="pago_Id,fact_Id,tpa_Id,pago_FechaElaboracion,pago_SaldoAnterior,pago_TotalPago,pago_TotalCambio,pago_Emisor,bcta_Id,pago_FechaVencimiento,pago_Titular,pago_UsuarioCrea,pago_FechaCrea,pago_UsuarioModifica,pago_FechaModifica")] tbPago tbPago)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbPago_Update(tbPago.pago_Id, tbPago.fact_Id, tbPago.tpa_Id, tbPago.pago_FechaElaboracion, tbPago.pago_SaldoAnterior, tbPago.pago_TotalPago, tbPago.pago_TotalCambio, tbPago.pago_Emisor, tbPago.bcta_Id, tbPago.pago_FechaVencimiento, tbPago.pago_Titular);
                    foreach (UDP_Vent_tbPago_Update_Result pago in list)
                        MensajeError = pago.MensajeError;
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
                    ModelState.AddModelError("", "Error al actualizar el registro " + Ex.Message.ToString());
                    return View(tbPago);
                }

                
            }
            ViewBag.pago_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPago.pago_UsuarioCrea);
            ViewBag.pago_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPago.pago_UsuarioModifica);
            ViewBag.bcta_Id = new SelectList(db.tbCuentasBanco, "bcta_Id", "bcta_Numero", tbPago.bcta_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPago.fact_Id);
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion", tbPago.tpa_Id);
            return View(tbPago);
        }

        // GET: /Pago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPago tbPago = db.tbPago.Find(id);
            if (tbPago == null)
            {
                return HttpNotFound();
            }
            return View(tbPago);
        }

        // POST: /Pago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPago tbPago = db.tbPago.Find(id);
            db.tbPago.Remove(tbPago);
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
