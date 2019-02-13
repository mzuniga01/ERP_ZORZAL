using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class EstadoFacturaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /EstadoFactura/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("EstadoFactura/Index"))
                    {
                        return View(db.tbEstadoFactura.ToList());
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

        // GET: /EstadoFactura/Details/5
        public ActionResult Details(byte? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("EstadoFactura/Details"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbEstadoFactura tbEstadoFactura = db.tbEstadoFactura.Find(id);
                        if (tbEstadoFactura == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        return View(tbEstadoFactura);
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

        // GET: /EstadoFactura/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("EstadoFactura/Create"))
                    {
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

        // POST: /EstadoFactura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "esfac_Id,esfac_Descripcion,esfac_UsuarioCrea,esfac_UsuarioModifico,esfac_FechaCrea,esfac_FechaModifico")] tbEstadoFactura tbEstadoFactura)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("EstadoFactura/Create"))
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                //////////Aqui va la lista//////////////
                                string MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbEstadoFactura_Insert(tbEstadoFactura.esfac_Descripcion, Function.GetUser(),
                                                Function.DatetimeNow());
                                foreach (UDP_Vent_tbEstadoFactura_Insert_Result estado in list)
                                    MensajeError = estado.MensajeError;
                                if (MensajeError.StartsWith("-1"))
                                {
                                    Function.InsertBitacoraErrores("EstadoFactura/Create", MensajeError, "Create");
                                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                    return View(tbEstadoFactura);
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                            }
                            catch (Exception Ex)
                            {
                                Function.InsertBitacoraErrores("EstadoFactura/Create", Ex.Message.ToString(), "Create");
                                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                return View(tbEstadoFactura);
                            }
                        }
                        return View(tbEstadoFactura);
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

        // GET: /EstadoFactura/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("EstadoFactura/Edit"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbEstadoFactura tbEstadoFactura = db.tbEstadoFactura.Find(id);
                        if (tbEstadoFactura == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        return View(tbEstadoFactura); 
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

        // POST: /EstadoFactura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "esfac_Id,esfac_Descripcion,esfac_UsuarioCrea,esfac_UsuarioModifico,esfac_FechaCrea,esfac_FechaModifico, tbUsuario, tbUsuario1")] tbEstadoFactura tbEstadoFactura)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("EstadoFactura/Edit"))
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                //////////Aqui va la lista//////////////
                                string MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbEstadoFactura_Update(tbEstadoFactura.esfac_Id,
                                    tbEstadoFactura.esfac_Descripcion,
                                    tbEstadoFactura.esfac_UsuarioCrea,
                                    tbEstadoFactura.esfac_FechaCrea,
                                    Function.GetUser(),
                                                Function.DatetimeNow());
                                foreach (UDP_Vent_tbEstadoFactura_Update_Result estado in list)
                                    MensajeError = estado.MensajeError;
                                if (MensajeError.StartsWith("-1"))
                                {
                                    Function.InsertBitacoraErrores("EstadoFactura/Edit", MensajeError, "Edit");
                                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                    return View(tbEstadoFactura);
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                            }
                            catch (Exception Ex)
                            {
                                Function.InsertBitacoraErrores("EstadoFactura/Edit", Ex.Message.ToString(), "Edit");
                                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                return View(tbEstadoFactura);
                            }
                        }
                        return View(tbEstadoFactura);
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
