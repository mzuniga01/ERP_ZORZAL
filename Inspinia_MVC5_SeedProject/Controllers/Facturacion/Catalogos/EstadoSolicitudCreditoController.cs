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
    public class EstadoSolicitudCreditoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        // GET: /EstadoSolicitudCredito/
        [SessionManager("EstadoSolicitudCredito/Index")]
        public ActionResult Index()
        {
            var tbestadosolicitudcredito = db.tbEstadoSolicitudCredito.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbestadosolicitudcredito.ToList());
        }

        // GET: /EstadoSolicitudCredito/Details/5
        [SessionManager("EstadoSolicitudCredito/Details")]
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbEstadoSolicitudCredito tbEstadoSolicitudCredito = db.tbEstadoSolicitudCredito.Find(id);
            if (tbEstadoSolicitudCredito == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbEstadoSolicitudCredito);

        }

        // GET: /EstadoSolicitudCredito/Create
        [SessionManager("EstadoSolicitudCredito/Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EstadoSolicitudCredito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("EstadoSolicitudCredito/Create")]
        public ActionResult Create([Bind(Include = "escre_Id,escre_Descripcion,escre_UsuarioCrea,escre_UsuarioModifica,escre_FechaAgrego,escre_FechaModifica")] tbEstadoSolicitudCredito tbEstadoSolicitudCredito)
        {
            try
            {
                //
                if (db.tbEstadoSolicitudCredito.Any(a => a.escre_Descripcion == tbEstadoSolicitudCredito.escre_Descripcion && a.escre_Descripcion == tbEstadoSolicitudCredito.escre_Descripcion))
                {
                    ModelState.AddModelError("", "Ya Existe Un Estado Solicitud Crédito con esa Descripción.");
                }
                //
                if (ModelState.IsValid)
                {
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbEstadoSolicitudCredito_Insert(tbEstadoSolicitudCredito.escre_Descripcion,
                                    Function.GetUser(),
                                    Function.DatetimeNow());
                    foreach (UDP_Vent_tbEstadoSolicitudCredito_Insert_Result EstadoSolicitudCredito in list)
                        MensajeError = EstadoSolicitudCredito.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("EstadoSolicitudCredito/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbEstadoSolicitudCredito);
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
                return View(tbEstadoSolicitudCredito);
            }
            return View(tbEstadoSolicitudCredito);
        }
        // POST: /EstadoSolicitudCredito/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // GET: /EstadoSolicitudCredito/Edit/5
        [SessionManager("EstadoSolicitudCredito/Edit")]
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbEstadoSolicitudCredito tbEstadoSolicitudCredito = db.tbEstadoSolicitudCredito.Find(id);
            if (tbEstadoSolicitudCredito == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbEstadoSolicitudCredito);
        }

        // POST: /EstadoSolicitudCredito/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("EstadoSolicitudCredito/Edit")]
        public ActionResult Edit([Bind(Include = "escre_Id,escre_Descripcion,escre_UsuarioCrea,escre_UsuarioModifica,escre_FechaAgrego,escre_FechaModifica,tbUsuario,tbUsuario1")] tbEstadoSolicitudCredito tbEstadoSolicitudCredito)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //////////Aqui va la lista//////////////
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbEstadoSolicitudCredito_Update(tbEstadoSolicitudCredito.escre_Id,
                        tbEstadoSolicitudCredito.escre_Descripcion,
                        tbEstadoSolicitudCredito.escre_UsuarioCrea,
                        tbEstadoSolicitudCredito.escre_UsuarioModifica,
                        tbEstadoSolicitudCredito.escre_FechaAgrego,
                        tbEstadoSolicitudCredito.escre_FechaModifica);
                    foreach (UDP_Vent_tbEstadoSolicitudCredito_Update_Result EstadoSolicitudCredito in list)
                        MensajeError = EstadoSolicitudCredito.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("EstadoSolicitudCredito/Edit", MensajeError, "Edit");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbEstadoSolicitudCredito);
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
                return View(tbEstadoSolicitudCredito);
            }
            return View(tbEstadoSolicitudCredito);
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
