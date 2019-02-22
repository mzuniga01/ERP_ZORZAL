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
    public class ActividadEconomicaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /ActividadEconomica/
        [SessionManager("ActividadEconomica/Index")]
        public ActionResult Index()
        {
            return View(db.tbActividadEconomica.ToList());
        }

        // GET: /ActividadEconomica/Details/5
        [SessionManager("ActividadEconomica/Details")]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            if (tbActividadEconomica == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbActividadEconomica);
        }

        // GET: /ActividadEconomica/Create
        [SessionManager("ActividadEconomica/Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ActividadEconomica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("ActividadEconomica/Create")]
        public ActionResult Create([Bind(Include = "acte_Id,acte_Descripcion,acte_UsuarioCrea,acte_FechaCrea,acte_UsuarioModifica,acte_FechaModifica")] tbActividadEconomica tbActividadEconomica)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbActividadEconomica_Insert(tbActividadEconomica.acte_Descripcion, Function.GetUser(), Function.DatetimeNow());

                    foreach (UDP_Gral_tbActividadEconomica_Insert_Result ActividadEconomica in list)
                        MensajeError = ActividadEconomica.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("ActividadEconomica/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbActividadEconomica);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(tbActividadEconomica);
            }
            catch (Exception Ex)
            {
                Function.InsertBitacoraErrores("ActividadEconomica/Create", Ex.Message.ToString(), "Create");
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbActividadEconomica);
            }
        }

        // GET: /ActividadEconomica/Edit/5
        [SessionManager("ActividadEconomica/Edit")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            if (tbActividadEconomica == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbActividadEconomica);
        }

        // POST: /ActividadEconomica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("ActividadEconomica/Edit")]
        public ActionResult Edit([Bind(Include = "acte_Id,acte_Descripcion,acte_UsuarioCrea,acte_FechaCrea,acte_UsuarioModifica,acte_FechaModifica, tbUsuario, tbUsuario1")] tbActividadEconomica tbActividadEconomica)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbActividadEconomica_Update(tbActividadEconomica.acte_Id, tbActividadEconomica.acte_Descripcion, tbActividadEconomica.acte_UsuarioCrea, tbActividadEconomica.acte_FechaCrea, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Gral_tbActividadEconomica_Update_Result ActividadEconomica in list)
                        MensajeError = ActividadEconomica.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("ActividadEconomica/Edit", MensajeError, "Edit");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbActividadEconomica);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception Ex)
            {
                Function.InsertBitacoraErrores("ActividadEconomica/Edit", Ex.Message.ToString(), "Edit");
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return View(tbActividadEconomica);
            }
            return View(tbActividadEconomica);
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
