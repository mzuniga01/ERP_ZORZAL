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

namespace ERP_ZORZAL.Controllers
{
    public class ProveedorController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        // GET: /Proveedor/
        [SessionManager("Proveedor/Index")]
        public ActionResult Index()
        {
            return View(db.tbProveedor.ToList());
        }

        // GET: /Proveedor/Details/5
        [SessionManager("Proveedor/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbProveedor);
        }

        // GET: /Proveedor/Create
        [SessionManager("Proveedor/Create")]
        public ActionResult Create()
        {
            try
            {
                ViewBag.smserror = TempData["smserror"].ToString();
            }
            catch { }
            ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
            return View();
        }


        [HttpPost]
        public JsonResult GetActividadEconomica(short? acte_Id)
        {
            var list = db.spGetActividadEconomica(acte_Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        // GET: /Proveedor/Edit/5
        [SessionManager("Proveedor/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.Actividad = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion", tbProveedor.acte_Id);
            return View(tbProveedor);
        }

        [HttpPost]
        [SessionManager("Proveedor/Create")]
        public JsonResult GuardarProveedor(string prov_RTN, string prov_Nombre, string prov_NombreContacto, string prov_Direccion, string prov_Email, string prov_Telefono,short? acte_Id)
        {
            var MsjError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> List = null;
                    List = db.UDP_Inv_tbProveedor_Insert(prov_Nombre, prov_NombreContacto, prov_Direccion, prov_Email, prov_Telefono, prov_RTN, acte_Id, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Inv_tbProveedor_Insert_Result Proveedor in List)
                        MsjError = Proveedor.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("Proveedor/Edit", MsjError, "Edit");
                    }
                    else if (MsjError.StartsWith("-2"))
                    {
                        Function.InsertBitacoraErrores("Proveedor/Edit", MsjError, "Edit");
                    }
                }
                catch (Exception Ex)
                {
                    MsjError = "-1";
                    Function.InsertBitacoraErrores("Proveedor/Edit", Ex.Message.ToString(), "Edit");
                }
            }
            return Json(MsjError, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionManager("Proveedor/Edit")]
        public JsonResult ActualizarProveedor( int? prov_Id, string prov_RTN, string prov_Nombre, string prov_NombreContacto, string prov_Direccion, string prov_Email, string prov_Telefono, short? acte_Id)
        {
            var MsjError = "";
            tbProveedor tbProveedor = db.tbProveedor.Find(prov_Id);
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> List = null;
                    List = db.UDP_Inv_tbProveedor_Update(prov_Id,prov_Nombre, prov_NombreContacto, prov_Direccion, prov_Email, prov_Telefono, prov_RTN, acte_Id, Function.GetUser(), Function.DatetimeNow(), Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Inv_tbProveedor_Update_Result Proveedor in List)
                        MsjError = Proveedor.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("Proveedor/Edit", MsjError, "Edit");
                    }
                    else if (MsjError == "-2")
                    {
                        Function.InsertBitacoraErrores("Proveedor/Edit", MsjError, "Edit");
                    }
                }
                catch (Exception Ex)
                {
                    MsjError = "-1";
                    Function.InsertBitacoraErrores("Proveedor/Edit", Ex.Message.ToString(), "Edit");
                }
                ViewBag.Actividad = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion", tbProveedor.acte_Id);
            }
            return Json(MsjError, JsonRequestBehavior.AllowGet);
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
