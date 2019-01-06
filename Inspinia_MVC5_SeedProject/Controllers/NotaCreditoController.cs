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
    public class NotaCreditoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /NotaCredito/
        public ActionResult Index()
        {
            var tbnotacredito = db.tbNotaCredito.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbDevolucion).Include(t => t.tbSucursal);
            return View(tbnotacredito.ToList());
        }

        // GET: /NotaCredito/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
            if (tbNotaCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbNotaCredito);
        }

        // GET: /NotaCredito/Create
        public ActionResult Create()
        {
            tbNotaCredito NotaCredito = new tbNotaCredito();
            ViewBag.Cliente = db.tbCliente.ToList();
           ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "nocre_Id,nocre_Codigo,dev_Id,clte_Id,suc_Id,nocre_Anulado,nocre_FechaEmision,nocre_MotivoEmision,nocre_Monto,nocre_UsuarioCrea,nocre_FechaCrea,nocre_UsuarioModifica,nocre_FechaModifica,nocre_Estado")] tbNotaCredito tbNotaCredito)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbNotaCredito_Insert(
                        tbNotaCredito.nocre_Codigo, 
                        tbNotaCredito.dev_Id, 
                        tbNotaCredito.clte_Id,
                        tbNotaCredito.suc_Id,
                        tbNotaCredito.nocre_Anulado,
                        tbNotaCredito.nocre_FechaEmision,
                        tbNotaCredito.nocre_MotivoEmision,
                        tbNotaCredito.nocre_Monto,
                        tbNotaCredito.nocre_Estado);
                    foreach (UDP_Vent_tbNotaCredito_Insert_Result NotaCredito in list)
                        MensajeError = NotaCredito.MensajeError;
                    if (MensajeError == -1)
                    {
                        ModelState.AddModelError("", "No se pudo Insertar el registro, favor contacte al administrador.");
                        return View(tbNotaCredito);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch(Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se pudo Editar el registro, favor contacte al administrador.");
                    return View(tbNotaCredito);
                }
            }
            return View(tbNotaCredito);
        }

        // GET: /NotaCredito/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
            if (tbNotaCredito == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
            return View(tbNotaCredito);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "nocre_Id,nocre_Codigo,dev_Id,clte_Id,suc_Id,nocre_Anulado,nocre_FechaEmision,nocre_MotivoEmision,nocre_Monto,nocre_UsuarioCrea,nocre_FechaCrea,nocre_UsuarioModifica,nocre_FechaModifica,nocre_Estado,tbUsuario,tbUsuario1")] tbNotaCredito tbNotaCredito)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbNotaCredito_Update(tbNotaCredito.nocre_Id, 
                        tbNotaCredito.nocre_Codigo, 
                        tbNotaCredito.dev_Id,
                        tbNotaCredito.clte_Id, 
                        tbNotaCredito.suc_Id, 
                        tbNotaCredito.nocre_Anulado,
                        tbNotaCredito.nocre_FechaEmision,
                        tbNotaCredito.nocre_MotivoEmision,
                        tbNotaCredito.nocre_Monto,
                        tbNotaCredito.nocre_UsuarioCrea,
                        tbNotaCredito.nocre_FechaCrea,
                        tbNotaCredito.nocre_Estado);
                    foreach (UDP_Vent_tbNotaCredito_Update_Result NotaCredito in list)
                        MensajeError = NotaCredito.MensajeError;
                    if (MensajeError == -1)
                    {
                        ModelState.AddModelError("", "No se pudo Editar el registro, favor contacte al administrador.");
                        return View(tbNotaCredito);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch(Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se pudo Editar el registro, favor contacte al administrador.");
                    return View(tbNotaCredito);
                }
            }
            return View(tbNotaCredito);
        }

        // GET: /NotaCredito/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
            if (tbNotaCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbNotaCredito);
        }

        // POST: /NotaCredito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
            db.tbNotaCredito.Remove(tbNotaCredito);
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
