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
    public class ActividadEconomicaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /ActividadEconomica/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("ActividadEconomica/Index"))
                    {
                        return View(db.tbActividadEconomica.ToList());
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

        // GET: /ActividadEconomica/Details/5
        public ActionResult Details(short? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("ActividadEconomica/Details"))
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

        // GET: /ActividadEconomica/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("ActividadEconomica/Create"))
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

        // POST: /ActividadEconomica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="acte_Id,acte_Descripcion,acte_UsuarioCrea,acte_FechaCrea,acte_UsuarioModifica,acte_FechaModifica")] tbActividadEconomica tbActividadEconomica)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("ActividadEconomica/Create"))
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

        // GET: /ActividadEconomica/Edit/5
        public ActionResult Edit(short? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("ActividadEconomica/Edit"))
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

        // POST: /ActividadEconomica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "acte_Id,acte_Descripcion,acte_UsuarioCrea,acte_FechaCrea,acte_UsuarioModifica,acte_FechaModifica, tbUsuario, tbUsuario1")] tbActividadEconomica tbActividadEconomica)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("ActividadEconomica/Edit"))
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
