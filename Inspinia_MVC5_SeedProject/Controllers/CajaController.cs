using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using ERP_GMEDINA.Reports;
using ERP_GMEDINA.Dataset;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class CajaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /Caja/
        [SessionManager("Caja/Index")]
        public ActionResult Index()
        {
            return View(db.tbCaja.ToList());
        }

        // GET: /Caja/Details/5
        [SessionManager("Caja/Details")]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbCaja tbCaja = db.tbCaja.Find(id);
            if (tbCaja == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbCaja);
        }

        // GET: /Caja/Create
        [SessionManager("Caja/Create")]
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Caja/Create"))
                    {
                        int idUser = 0;
                        GeneralFunctions Login = new GeneralFunctions();
                        List<tbUsuario> User = Login.getUserInformation();
                        foreach (tbUsuario Usuario in User)
                        {
                            idUser = Convert.ToInt32(Usuario.emp_Id);
                        }
                        ViewBag.usu_Id = idUser;
                        ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                        ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }

            else
                return RedirectToAction("Index", "Login");
    }

        // POST: /Caja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Caja/Create")]
        public ActionResult Create([Bind(Include = "cja_Id,cja_Descripcion,suc_Id,cja_UsuarioCrea,cja_FechaCrea,cja_UsuarioModifica,cja_FechaModifica")] tbCaja tbCaja)
        {
            List<tbUsuario> List = Function.getUserInformation();
            int SucursalId = 0;
            foreach (tbUsuario User in List)
            {
                SucursalId = (int)User.suc_Id;
            }
            var Sucursal = db.tbSucursal.Select(s => new
            {
                suc_Id = s.suc_Id,
                suc_Descripcion = s.suc_Descripcion
            }).Where(x => x.suc_Id == SucursalId).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbCaja_Insert(tbCaja.cja_Descripcion, tbCaja.suc_Id, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Vent_tbCaja_Insert_Result caja in list)
                        MensajeError = caja.MensajeError.ToString();
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                        Function.InsertBitacoraErrores("Caja/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbCaja);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                return View(tbCaja);
            }
            catch (Exception Ex)
            {
                ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                Function.InsertBitacoraErrores("Caja/Create", Ex.Message.ToString(), "Create");
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbCaja);
            }
        }

        // GET: /Caja/Edit/5
        [SessionManager("Caja/Edit")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbCaja tbCaja = db.tbCaja.Find(id);
            if (tbCaja == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            List<tbUsuario> List = Function.getUserInformation();
            int SucursalId = 0;
            foreach (tbUsuario User in List)
            {
                SucursalId = (int)User.suc_Id;
            }
            var Sucursal = db.tbSucursal.Select(s => new
            {
                suc_Id = s.suc_Id,
                suc_Descripcion = s.suc_Descripcion
            }).Where(x => x.suc_Id == SucursalId).ToList();
            ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
            return View(tbCaja);
        }

        // POST: /Caja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Caja/Edit")]
        public ActionResult Edit([Bind(Include = "cja_Id,cja_Descripcion,suc_Id,cja_UsuarioCrea,cja_FechaCrea,cja_UsuarioModifica,cja_FechaModifica,tbUsuario,tbUsuario1")] tbCaja tbCaja)
        {
            List<tbUsuario> List = Function.getUserInformation();
            int SucursalId = 0;
            foreach (tbUsuario User in List)
            {
                SucursalId = (int)User.suc_Id;
            }
            var Sucursal = db.tbSucursal.Select(s => new
            {
                suc_Id = s.suc_Id,
                suc_Descripcion = s.suc_Descripcion
            }).Where(x => x.suc_Id == SucursalId).ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbCaja_Update(tbCaja.cja_Id, tbCaja.cja_Descripcion, tbCaja.suc_Id, tbCaja.cja_UsuarioCrea, tbCaja.cja_FechaCrea, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Vent_tbCaja_Update_Result caja in list)
                        MensajeError = caja.MensajeError.ToString();
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                        Function.InsertBitacoraErrores("Caja/Edit", MensajeError, "Edit");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbCaja);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                    Function.InsertBitacoraErrores("Caja/Edit", Ex.Message.ToString(), "Edit");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return View(tbCaja);
                }
            }
            ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
            return View(tbCaja);
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
