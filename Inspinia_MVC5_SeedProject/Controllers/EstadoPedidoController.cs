using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class EstadoPedidoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: /EstadoPedido/
        [SessionManager("EstdadoPedido/Index")]
        public ActionResult Index()
        {

            return View(db.tbEstadoPedido.ToList());
        }

        [SessionManager("EstdadoPedido/Details")]
        // GET: /EstadoPedido/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoPedido tbEstadoPedido = db.tbEstadoPedido.Find(id);
            if (tbEstadoPedido == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoPedido);
        }

        [SessionManager("EstdadoPedido/Create")]
        // GET: /EstadoPedido/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EstadoPedido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("EstdadoPedido/Create")]
        public ActionResult Create([Bind(Include = "esped_Id,esped_Descripcion,esped_UsuarioCrea,esped_FechaCrea,esped_UsuarioModifica,esped_FechaModifica")] tbEstadoPedido tbEstadoPedido)
        {
            try
            {
                if (db.tbEstadoPedido.Any(a => a.esped_Descripcion == tbEstadoPedido.esped_Descripcion && a.esped_Descripcion == tbEstadoPedido.esped_Descripcion))
                {
                    ModelState.AddModelError("", "Ya Existe Un Estado Pedido con esa Descripción.");
                }
                if (ModelState.IsValid)
                {
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbEstadoPedido_Insert(tbEstadoPedido.esped_Descripcion,
                                    Function.GetUser(),
                                    Function.DatetimeNow());
                    foreach (UDP_Vent_tbEstadoPedido_Insert_Result EstadoPedido1 in list)
                        MensajeError = EstadoPedido1.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("EstadoPedido/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbEstadoPedido);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }

            }
            catch (Exception Ex)
            {
                Function.InsertBitacoraErrores("EstadoSolicitudCredito/Create", Ex.Message.ToString(), "Create");
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbEstadoPedido);
            }
            return View(tbEstadoPedido);
        }




        [SessionManager("EstdadoPedido/Edit")]
        // GET: /EstadoPedido/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoPedido tbEstadoPedido = db.tbEstadoPedido.Find(id);
            if (tbEstadoPedido == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoPedido);
        }

        // POST: /EstadoPedido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("EstdadoPedido/Edit")]
        public ActionResult Edit([Bind(Include = "esped_Id,esped_Descripcion,esped_UsuarioCrea,esped_FechaCrea,esped_UsuarioModifica,esped_FechaModifica")] tbEstadoPedido tbEstadoPedido)
        {
            try
            {
                //
                if (db.tbEstadoPedido.Any(a => a.esped_Descripcion == tbEstadoPedido.esped_Descripcion && a.esped_Descripcion == tbEstadoPedido.esped_Descripcion && a.esped_Descripcion != tbEstadoPedido.esped_Descripcion))

                {
                    ModelState.AddModelError("", "Esta Descripción ya esta registrada.");
                }

                //
                if (ModelState.IsValid)
                {
                    //////////Aqui va la lista//////////////
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    tbEstadoPedido EstadoPedidos = db.tbEstadoPedido.Find(tbEstadoPedido.esped_Id);
                    list = db.UDP_Vent_tbEstadoPedido_Update(tbEstadoPedido.esped_Id,
                        tbEstadoPedido.esped_Descripcion,
                        EstadoPedidos.esped_UsuarioCrea,
                        Function.GetUser(),
                        EstadoPedidos.esped_FechaCrea,
                        Function.DatetimeNow());
                    foreach (UDP_Vent_tbEstadoPedido_Update_Result EstadoPedido1 in list)
                        MensajeError = EstadoPedido1.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("EstadoSolicitudCredito/Edit", MensajeError, "Edit");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbEstadoPedido);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception Ex)
            {
                Function.InsertBitacoraErrores("EstadoSolicitudCredito/Edit", Ex.Message.ToString(), "Edit");
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return View(tbEstadoPedido);
            }
                return View(tbEstadoPedido);
            }

        // GET: /EstadoPedido/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoPedido tbEstadoPedido = db.tbEstadoPedido.Find(id);
            if (tbEstadoPedido == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoPedido);
        }

        // POST: /EstadoPedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbEstadoPedido tbEstadoPedido = db.tbEstadoPedido.Find(id);
            db.tbEstadoPedido.Remove(tbEstadoPedido);
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
