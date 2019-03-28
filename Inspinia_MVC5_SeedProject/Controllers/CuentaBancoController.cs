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
    public class CuentaBancoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: /CuentaBanco/
        [SessionManager("CuentasBanco/Index")]
        public ActionResult Index()
        {
            var tbcuentasbanco = db.tbCuentasBanco.Include(t => t.tbBanco);
            return View(tbcuentasbanco.ToList());
        }

        // GET: /CuentaBanco/Details/5
        [SessionManager("CuentasBanco/Details")]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbCuentasBanco tbCuentasBanco = db.tbCuentasBanco.Find(id);
            if (tbCuentasBanco == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            tbCuentasBanco.TipoCuentaList = cUtilities.TipoCuentaList();
            return View(tbCuentasBanco);
        }

        // GET: /CuentaBanco/Create
        [SessionManager("CuentasBanco/Create")]
        public ActionResult Create()
        {
            tbCuentasBanco CuentasBanco = new tbCuentasBanco();
            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre");
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
            /////////Aqui lleno la lista/////////
            CuentasBanco.TipoCuentaList = cUtilities.TipoCuentaList();
            return View(CuentasBanco);
        }

        // POST: /CuentaBanco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("CuentasBanco/Create")]
        public ActionResult Create([Bind(Include = "bcta_Id,ban_Id,mnda_Id,bcta_TipoCuenta,bcta_TotalCredito,bcta_TotalDebito,bcta_FechaApertura,bcta_Numero,bcta_UsuarioCrea,bcta_FechaCrea,bcta_UsuarioModifica,bcta_FechaModifica")] tbCuentasBanco tbCuentasBanco)
        {
            tbCuentasBanco CuentasBanco = new tbCuentasBanco();
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbCuentasBanco_Insert(
                        tbCuentasBanco.ban_Id,
                        tbCuentasBanco.mnda_Id,
                        tbCuentasBanco.bcta_TipoCuenta,
                        tbCuentasBanco.bcta_TotalCredito,
                        tbCuentasBanco.bcta_TotalDebito,
                        tbCuentasBanco.bcta_FechaApertura,
                        tbCuentasBanco.bcta_Numero,
                        Function.GetUser(),
                        Function.DatetimeNow());
                    foreach (UDP_Gral_tbCuentasBanco_Insert_Result cuentasbanco in list)
                        MensajeError = cuentasbanco.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", tbCuentasBanco.ban_Id);
                        ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbCuentasBanco.mnda_Id);
                        CuentasBanco.TipoCuentaList = cUtilities.TipoCuentaList();
                        Function.InsertBitacoraErrores("CuentaBanco/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbCuentasBanco);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", tbCuentasBanco.ban_Id);
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbCuentasBanco.mnda_Id);
                    CuentasBanco.TipoCuentaList = cUtilities.TipoCuentaList();
                    Function.InsertBitacoraErrores("CuentaBanco/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbCuentasBanco);
                }
            }
            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", tbCuentasBanco.ban_Id);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbCuentasBanco.mnda_Id);
            CuentasBanco.TipoCuentaList = cUtilities.TipoCuentaList();
            return View(tbCuentasBanco);
        }

        // GET: /CuentaBanco/Edit/5
        [SessionManager("CuentasBanco/Edit")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbCuentasBanco CuentasBanco = db.tbCuentasBanco.Find(id);
            if (CuentasBanco == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", CuentasBanco.ban_Id);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", CuentasBanco.mnda_Id);
            var Lista = cUtilities.TipoCuentaList();
            ViewBag.TipoCuentaList = new SelectList(Lista, "ID_TIPOCUENTA", "DESCRIPCION", CuentasBanco.bcta_TipoCuenta);
            return View(CuentasBanco);
        }
        // POST: /CuentaBanco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("CuentasBanco/Edit")]
        public ActionResult Edit([Bind(Include = "bcta_Id,ban_Id,mnda_Id,bcta_TipoCuenta,bcta_TotalCredito,bcta_TotalDebito,bcta_FechaApertura,bcta_Numero,bcta_UsuarioCrea,bcta_FechaCrea,bcta_UsuarioModifica,bcta_FechaModifica, tbUsuario, tbUsuario1")] tbCuentasBanco CuentasBanco)
        {
            var Lista = cUtilities.TipoCuentaList();
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbCuentasBanco_Update(
                        CuentasBanco.bcta_Id,
                        CuentasBanco.ban_Id,
                        CuentasBanco.mnda_Id,
                        CuentasBanco.bcta_TipoCuenta,
                        CuentasBanco.bcta_TotalCredito,
                        CuentasBanco.bcta_TotalDebito,
                        CuentasBanco.bcta_FechaApertura,
                        CuentasBanco.bcta_Numero,
                        CuentasBanco.bcta_UsuarioCrea,
                        CuentasBanco.bcta_FechaCrea,
                        Function.GetUser(),
                        Function.DatetimeNow());
                    foreach (UDP_Gral_tbCuentasBanco_Update_Result cuentasbanco in list)
                        MensajeError = cuentasbanco.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.TipoCuentaList = new SelectList(Lista, "ID_TIPOCUENTA", "DESCRIPCION", CuentasBanco.bcta_TipoCuenta);
                        ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", CuentasBanco.ban_Id);
                        ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", CuentasBanco.mnda_Id);
                        Function.InsertBitacoraErrores("CuentaBanco/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(CuentasBanco);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }

                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("CuentaBanco/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", CuentasBanco.ban_Id);
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", CuentasBanco.mnda_Id);
                    ViewBag.TipoCuentaList = new SelectList(Lista, "ID_TIPOCUENTA", "DESCRIPCION", CuentasBanco.bcta_TipoCuenta);
                    return View(CuentasBanco);
                }
            }
            ViewBag.TipoCuentaList = new SelectList(Lista, "ID_TIPOCUENTA", "DESCRIPCION", CuentasBanco.bcta_TipoCuenta);
            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", CuentasBanco.ban_Id);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", CuentasBanco.mnda_Id);
            return View(CuentasBanco);
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
