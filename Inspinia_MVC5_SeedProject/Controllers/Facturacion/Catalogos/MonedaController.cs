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
    public class MonedaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        // GET: /Moneda/
        [SessionManager("Moneda/Index")]
        public ActionResult Index()
        {
            var tbMoneda = db.tbMoneda.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbMoneda.ToList());
        }

        // GET: /Moneda/Details/5
        [SessionManager("Moneda/Details")]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            if (tbMoneda == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbMoneda);
        }

        // GET: /Moneda/Create
        [SessionManager("Moneda/Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Moneda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Moneda/Create")]
        public ActionResult Create([Bind(Include="mnda_Id,mnda_Abreviatura,mnda_Nombre,mnda_UsuarioCrea,mnda_FechaCrea,mnda_UsuarioModifica,mnda_FechaModifica")] tbMoneda tbMoneda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.tbMoneda.Any(a => a.mnda_Abreviatura == tbMoneda.mnda_Abreviatura) || (db.tbMoneda.Any(a => a.mnda_Nombre == tbMoneda.mnda_Nombre)))
                    {
                        ModelState.AddModelError("", "Ya existe una moneda con ese nombre");
                        return View(tbMoneda);
                    } 
             
                    else
                    {
                        var MensajeError = "";
                        IEnumerable<object> list = null;
                        list = db.UDP_Gral_tbMoneda_Insert(tbMoneda.mnda_Abreviatura, tbMoneda.mnda_Nombre, Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Gral_tbMoneda_Insert_Result Moneda in list)
                            MensajeError = Moneda.MensajeError;
                        if (MensajeError.StartsWith("-1"))
                        {
                            Function.InsertBitacoraErrores("Moneda/Create", MensajeError, "Create");
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            return View(tbMoneda);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("Moneda/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbMoneda);
                }
            }
            return View(tbMoneda);
        }

        // GET: /Moneda/Edit/5
        [SessionManager("Moneda/Edit")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            if (tbMoneda == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbMoneda);
        }

        // POST: /Moneda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Moneda/Edit")]
        public ActionResult Edit([Bind(Include= "mnda_Id,mnda_Abreviatura,mnda_Nombre,mnda_UsuarioCrea,mnda_FechaCrea,mnda_UsuarioModifica,mnda_FechaModifica, tbUsuario, tbUsuario1")] tbMoneda tbMoneda)
        {

            try
            {

                if (ModelState.IsValid)
                {  
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbMoneda_Update(tbMoneda.mnda_Id, tbMoneda.mnda_Abreviatura, tbMoneda.mnda_Nombre, tbMoneda.mnda_UsuarioCrea,tbMoneda.mnda_FechaCrea, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Gral_tbMoneda_Update_Result Moneda in list)
                        MensajeError = Moneda.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("Moneda/Edit", MensajeError, "Edit");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbMoneda);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception Ex)
            {
                Function.InsertBitacoraErrores("Moneda/Edit", Ex.Message.ToString(), "Edit");
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return View(tbMoneda);
            }

            return View(tbMoneda);
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
