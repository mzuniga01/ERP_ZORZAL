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
    public class DenominacionArqueoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /DenominacionArqueo/
        [SessionManager("DenominacionArqueo/Index")]
        public ActionResult Index()
        {
            var tbdenominacionarqueo = db.tbDenominacionArqueo.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbDenominacion)/*.Include(t => t.tbMovimientoCaja)*/;
            return View(tbdenominacionarqueo.ToList());

        }

        // GET: /DenominacionArqueo/Details/5
        [SessionManager("DenominacionArqueo/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbDenominacionArqueo tbDenominacionArqueo = db.tbDenominacionArqueo.Find(id);
            if (tbDenominacionArqueo == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbDenominacionArqueo);
        }

        // GET: /DenominacionArqueo/Create
        [SessionManager("DenominacionArqueo/Create")]
        public ActionResult Create()
        {
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion");
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("DenominacionArqueo/Create")]
        public ActionResult Create([Bind(Include = "mocja_Id,deno_Id,arqde_CantidadDenominacion,arqde_MontoDenominacion")] tbDenominacionArqueo tbDenominacionArqueo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = string.Empty;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbDenominacionArqueo_Insert(tbDenominacionArqueo.mocja_Id,
                        tbDenominacionArqueo.deno_Id,
                        tbDenominacionArqueo.arqde_CantidadDenominacion,
                        tbDenominacionArqueo.arqde_MontoDenominacion, Function.GetUser(),
                                    Function.DatetimeNow());
                    foreach (UDP_Vent_tbDenominacionArqueo_Insert_Result denoarq in list)
                        MensajeError = denoarq.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbDenominacionArqueo.mocja_Id);
                        ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbDenominacionArqueo.deno_Id);
                        Function.InsertBitacoraErrores("DenominacionArqueo/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbDenominacionArqueo);
                    }
                    else
                        return RedirectToAction("Index");
                }
                catch (Exception Ex)
                {
                    ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbDenominacionArqueo.mocja_Id);
                    ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbDenominacionArqueo.deno_Id);
                    Function.InsertBitacoraErrores("DenominacionArqueo/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbDenominacionArqueo);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbDenominacionArqueo.mocja_Id);
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbDenominacionArqueo.deno_Id);
            return View(tbDenominacionArqueo);
        }

        // GET: /DenominacionArqueo/Edit/5
        [SessionManager("DenominacionArqueo/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbDenominacionArqueo tbDenominacionArqueo = db.tbDenominacionArqueo.Find(id);
            if (tbDenominacionArqueo == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbDenominacionArqueo.deno_Id);
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbDenominacionArqueo.mocja_Id);
            return View(tbDenominacionArqueo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("DenominacionArqueo/Edit")]
        public ActionResult Edit([Bind(Include = "arqde_Id,mocja_Id,deno_Id,arqde_CantidadDenominacion,arqde_MontoDenominacion,arqde_UsuarioCrea,arqde_FechaCrea,arqde_UsuarioModifica,arqde_FechaModifica,tbUsuario,tbUsuario1")] tbDenominacionArqueo tbDenominacionArqueo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = string.Empty;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbDenominacionArqueo_Update(tbDenominacionArqueo.arqde_Id, tbDenominacionArqueo.mocja_Id, tbDenominacionArqueo.deno_Id,
                        tbDenominacionArqueo.arqde_CantidadDenominacion,
                        tbDenominacionArqueo.arqde_MontoDenominacion,
                        tbDenominacionArqueo.arqde_UsuarioCrea,
                        tbDenominacionArqueo.arqde_FechaCrea, Function.GetUser(),
                                    Function.DatetimeNow());
                    foreach (UDP_Vent_tbDenominacionArqueo_Update_Result denoarq in list)
                        MensajeError = denoarq.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbDenominacionArqueo.mocja_Id);
                        ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbDenominacionArqueo.deno_Id);
                        Function.InsertBitacoraErrores("DenominacionArqueo/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbDenominacionArqueo);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbDenominacionArqueo.mocja_Id);
                    ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbDenominacionArqueo.deno_Id);
                    Function.InsertBitacoraErrores("DenominacionArqueo/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return View(tbDenominacionArqueo);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbDenominacionArqueo.mocja_Id);
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbDenominacionArqueo.deno_Id);

            return View(tbDenominacionArqueo);
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
