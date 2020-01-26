using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Globalization;
using System.Threading;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class DenominacionController : Controller
    {
        //__________Cultura___________
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            const string culture = "es-HN";
            CultureInfo ci = CultureInfo.GetCultureInfo(culture);

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        //____________________________

        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        tbDenominacion TipoDenominacion = new tbDenominacion();
        // GET: /Denominacion/
        [SessionManager("Denominacion/Index")]
        public ActionResult Index()
        {
            var tbdenominacion = db.tbDenominacion.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbMoneda);
            return View(tbdenominacion.ToList());
        }
        // GET: /Denominacion/Details/5
        [SessionManager("Denominacion/Details")]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbDenominacion tbDenominacion = db.tbDenominacion.Find(id);
            if (tbDenominacion == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbDenominacion);
        }

        // GET: /Denominacion/Create
        [SessionManager("Denominacion/Create")]
        public ActionResult Create()
        {
            TipoDenominacion.DenominacionList = Function.DenominacionList();
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
            return View(TipoDenominacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Denominacion/Create")]
        public ActionResult Create([Bind(Include = "deno_Id,deno_Descripcion,deno_Tipo,deno_valor,mnda_Id,deno_UsuarioCrea,deno_FechaCrea,deno_UsuarioModifica,deno_FechaModifica")] tbDenominacion tbDenominacion)
        {
            if (db.tbDenominacion.Any(a => a.mnda_Id == tbDenominacion.mnda_Id && a.deno_valor == tbDenominacion.deno_valor))
            {
                ModelState.AddModelError("", "Ya Existe Una Moneda con  denominación de este valor.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbDenominacion_Insert(tbDenominacion.deno_Descripcion, tbDenominacion.deno_Tipo, tbDenominacion.deno_valor, tbDenominacion.mnda_Id, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Gral_tbDenominacion_Insert_Result Denominacion in list)
                        MensajeError = Denominacion.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);
                        TipoDenominacion.DenominacionList = Function.DenominacionList();
                        Function.InsertBitacoraErrores("Denominacion/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbDenominacion);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);
                    TipoDenominacion.DenominacionList = Function.DenominacionList();
                    Function.InsertBitacoraErrores("Denominacion/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbDenominacion);
                }
            }
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);
            TipoDenominacion.DenominacionList = Function.DenominacionList();
            return View(tbDenominacion);
        }

        // GET: /Denominacion/Edit/5
        [SessionManager("Denominacion/Edit")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbDenominacion tbDenominacion = db.tbDenominacion.Find(id);
            if (tbDenominacion == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);
            tbDenominacion TipoDenominacion = db.tbDenominacion.Find(id);
            TipoDenominacion.DenominacionList = Function.DenominacionList();
            return View(tbDenominacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Denominacion/Edit")]
        public ActionResult Edit([Bind(Include = "deno_Id,deno_Descripcion,deno_Tipo,deno_valor,mnda_Id,deno_UsuarioCrea,deno_FechaCrea,deno_UsuarioModifica,deno_FechaModifica ,tbUsuario, tbUsuario1")] tbDenominacion tbDenominacion)
        {
            if (db.tbDenominacion.Any(a => a.mnda_Id == tbDenominacion.mnda_Id && a.deno_valor == tbDenominacion.deno_valor && a.deno_Id != tbDenominacion.deno_Id))
            {
                ModelState.AddModelError("", "Este valor ya esta registrado para esta moneda.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbDenominacion_Update(tbDenominacion.deno_Id,
                                                            tbDenominacion.deno_Descripcion,
                                                            tbDenominacion.deno_Tipo,
                                                            tbDenominacion.deno_valor,
                                                            tbDenominacion.mnda_Id,
                                                            tbDenominacion.deno_UsuarioCrea,
                                                            tbDenominacion.deno_FechaCrea,
                                                            Function.GetUser(),
                                                            Function.DatetimeNow());
                    foreach (UDP_Gral_tbDenominacion_Update_Result Denominacion in list)
                        MensajeError = Denominacion.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);
                        TipoDenominacion.DenominacionList = Function.DenominacionList();
                        Function.InsertBitacoraErrores("Denominacion/Edit", MensajeError, "Edit");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbDenominacion);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);
                    TipoDenominacion.DenominacionList = Function.DenominacionList();
                    Function.InsertBitacoraErrores("Denominacion/Edit", Ex.Message.ToString(), "Edit");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return View(tbDenominacion);
                }
            }
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);
            TipoDenominacion.DenominacionList = Function.DenominacionList();
            return View(tbDenominacion);
        }

        [HttpGet]
        public ActionResult GDatosEncabezado(short uso)
        {
            var list = db.UDP_Gral_tbDenominacion_using(uso).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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
