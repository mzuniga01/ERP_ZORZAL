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
    public class BancoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        // GET: /Banco/
        [SessionManager("Banco/Index")]
        public ActionResult Index()
        {
            return View(db.tbBanco.ToList());
        }

        // GET: /Banco/Details/5
        [SessionManager("Banco/Details")]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbBanco tbBanco = db.tbBanco.Find(id);
            if (tbBanco == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbBanco);
        }

        // GET: /Banco/Create
        [SessionManager("Banco/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Banco/Create")]
        public ActionResult Create([Bind(Include = "ban_Id,ban_Nombre,ban_NombreContacto,ban_TelefonoContacto,ban_UsuarioCrea,ban_FechaCrea,ban_UsuarioModifica,ban_FechaModifica")] tbBanco tbBanco)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (db.tbBanco.Any(a => a.ban_Nombre == tbBanco.ban_Nombre))
                    {
                        ModelState.AddModelError("", "Ya existe este Banco.");
                        return View(tbBanco);
                    }
                    else
                    {
                        //////////Aqui va la lista//////////////
                        string MensajeError = "";
                        IEnumerable<object> list = null;
                        list = db.UDP_Gral_tbBanco_Insert(tbBanco.ban_Nombre, tbBanco.ban_NombreContacto, tbBanco.ban_TelefonoContacto, Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Gral_tbBanco_Insert_Result banco in list)
                            MensajeError = banco.MensajeError.ToString();
                        if (MensajeError.StartsWith("-1"))
                        {
                            Function.InsertBitacoraErrores("Banco/Create", MensajeError, "Create");
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            return View(tbBanco);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("Banco/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbBanco);
                }
            }
            return View(tbBanco);
        }

        // GET: /Banco/Edit/5
        [SessionManager("Banco/Edit")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbBanco tbBanco = db.tbBanco.Find(id);
            if (tbBanco == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbBanco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Banco/Edit")]
        public ActionResult Edit([Bind(Include = "ban_Id,ban_Nombre,ban_NombreContacto,ban_TelefonoContacto,ban_UsuarioCrea,ban_FechaCrea,ban_UsuarioModifica,ban_FechaModifica,tbUsuario, tbUsuario1")] tbBanco tbBanco)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbBanco_Update(tbBanco.ban_Id,
                        tbBanco.ban_Nombre,
                        tbBanco.ban_NombreContacto,
                        tbBanco.ban_TelefonoContacto,
                        tbBanco.ban_UsuarioCrea,
                        tbBanco.ban_FechaCrea,
                        Function.GetUser(),
                        Function.DatetimeNow());
                    foreach (UDP_Gral_tbBanco_Update_Result banco in list)
                        MensajeError = banco.MensajeError.ToString();
                    if (MensajeError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("Banco/Edit", MensajeError, "Edit");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbBanco);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("Banco/Edit", Ex.Message.ToString(), "Edit");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return View(tbBanco);
                }
            }
            return View(tbBanco);
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
