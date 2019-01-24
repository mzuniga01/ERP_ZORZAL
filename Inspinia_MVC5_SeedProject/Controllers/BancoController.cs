using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_ZORZAL.Controllers
{
    public class BancoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /Banco/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Banco/Index"))
                {
                    return View(db.tbBanco.ToList());
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /Banco/Details/5
        public ActionResult Details(short? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Banco/Details"))
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
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /Banco/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Banco/Create"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ban_Id,ban_Nombre,ban_NombreContacto,ban_TelefonoContacto,ban_UsuarioCrea,ban_FechaCrea,ban_UsuarioModifica,ban_FechaModifica")] tbBanco tbBanco)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Banco/Create"))
                {
                    if (ModelState.IsValid)
                    {
                        try
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
                        catch (Exception Ex)
                        {
                            Function.InsertBitacoraErrores("Banco/Create", Ex.Message.ToString(), "Create");
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            return View(tbBanco);
                        }
                    }
                    return View(tbBanco);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /Banco/Edit/5
        public ActionResult Edit(short? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Banco/Edit"))
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
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "ban_Id,ban_Nombre,ban_NombreContacto,ban_TelefonoContacto,ban_UsuarioCrea,ban_FechaCrea,ban_UsuarioModifica,ban_FechaModifica,tbUsuario, tbUsuario1")] tbBanco tbBanco)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Banco/Edit"))
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            //////////Aqui va la lista//////////////
                            string MensajeError = "";
                            IEnumerable<object> list = null;
                            list = db.UDP_Gral_tbBanco_Update(tbBanco.ban_Id, tbBanco.ban_Nombre, tbBanco.ban_NombreContacto, tbBanco.ban_TelefonoContacto, tbBanco.ban_UsuarioCrea, tbBanco.ban_FechaCrea, Function.GetUser(), Function.DatetimeNow());
                            foreach (UDP_Gral_tbBanco_Update_Result banco in list)
                                MensajeError = banco.MensajeError.ToString();
                            if (MensajeError.StartsWith("-1"))
                            {
                                Function.InsertBitacoraErrores("Banco/Create", MensajeError, "Edit");
                                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                return View(tbBanco);
                            }
                            else
                            {
                                return RedirectToAction("Index");
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
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
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
