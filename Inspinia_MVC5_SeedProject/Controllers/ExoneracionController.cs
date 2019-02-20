﻿using System;
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
    public class ExoneracionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        public ActionResult ClientesnoExonerado()
        {
            return View(db.UDP_Vent_listExoneracion_Select);
        }
        
        // GET: /Exoneracion/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Exoneracion/Index"))
                    {
                        var tbexoneracion = db.tbExoneracion.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente);
                        return View(tbexoneracion.ToList());
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

        // GET: /Exoneracion/Details/5
        public ActionResult Details(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Exoneracion/Details"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbExoneracion tbExoneracion = db.tbExoneracion.Find(id);
                        if (tbExoneracion == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        return View(tbExoneracion);
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

        // GET: /Exoneracion/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Exoneracion/Create"))
                    {
                        ViewBag.Cliente = db.tbCliente.ToList();
                        ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
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

        // POST: /Exoneracion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="exo_Id,exo_Documento,exo_ExoneracionActiva,exo_FechaInicialVigencia,exo_FechaIFinalVigencia,clte_Id,exo_UsuarioCrea,exo_FechaCrea,exo_UsuarioModifa,exo_FechaModifica")] tbExoneracion tbExoneracion)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Exoneracion/Create"))
                    {
                        try
                        {
                            if (ModelState.IsValid)
                            {
                                string MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbExoneracion_Insert(tbExoneracion.exo_Documento,
                                                                        Helpers.ExoneracionActiva,
                                                                        tbExoneracion.exo_FechaInicialVigencia,
                                                                        tbExoneracion.exo_FechaIFinalVigencia,
                                                                        tbExoneracion.clte_Id, 
                                                                        Function.GetUser(),
                                                                        Function.DatetimeNow());
                                foreach (UDP_Vent_tbExoneracion_Insert_Result Exoneracion in list)
                                    MensajeError = Exoneracion.MensajeError;
                                if (MensajeError.StartsWith("-1"))
                                {
                                    ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                                    ViewBag.Cliente = db.tbCliente.ToList();
                                    ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                                    Function.InsertBitacoraErrores("Exoneracion/Create", MensajeError, "Create");
                                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                    return View(tbExoneracion);
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                            }
                            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                            ViewBag.Cliente = db.tbCliente.ToList();
                            ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                            return View(tbExoneracion);
                        }
                        catch (Exception Ex)
                        {
                            Function.InsertBitacoraErrores("Exoneracion/Create", Ex.Message.ToString(), "Create");
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                            ViewBag.Cliente = db.tbCliente.ToList();
                            ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                            return View(tbExoneracion);
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

        // GET: /Exoneracion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Exoneracion/Edit"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbExoneracion tbExoneracion = db.tbExoneracion.Find(id);
                        if (tbExoneracion == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        ViewBag.exo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioCrea);
                        ViewBag.exo_UsuarioModifa = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioModifa);
                        ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                        ViewBag.Cliente = db.tbCliente.ToList();
                        ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                        return View(tbExoneracion);
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

        // POST: /Exoneracion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id,[Bind(Include= "exo_Id,exo_Documento,exo_ExoneracionActiva,exo_FechaInicialVigencia,exo_FechaIFinalVigencia,clte_Id,exo_UsuarioCrea,exo_FechaCrea")] tbExoneracion tbExoneracion)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Exoneracion/Edit"))
                    {
                        try
                        {
                            if (ModelState.IsValid)
                            {
                                tbExoneracion pExoneracion = db.tbExoneracion.Find(id);
                                string MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbExoneracion_Update(tbExoneracion.exo_Id,
                                                                        tbExoneracion.exo_Documento,
                                                                        pExoneracion.exo_ExoneracionActiva,
                                                                        tbExoneracion.exo_FechaInicialVigencia,
                                                                        tbExoneracion.exo_FechaIFinalVigencia,
                                                                        tbExoneracion.clte_Id,
                                                                        pExoneracion.exo_UsuarioCrea,
                                                                        pExoneracion.exo_FechaCrea, Function.GetUser(),
                                                Function.DatetimeNow());
                                foreach (UDP_Vent_tbExoneracion_Update_Result Exoneracion in list)
                                    MensajeError = Exoneracion.MensajeError;
                                if (MensajeError.StartsWith("-1"))
                                {
                                    ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                                    ViewBag.Cliente = db.tbCliente.ToList();
                                    ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                                    Function.InsertBitacoraErrores("Exoneracion/Create", MensajeError, "Create");
                                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                    return View(tbExoneracion);
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                            }
                            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                            ViewBag.Cliente = db.tbCliente.ToList();
                            ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                            return View(tbExoneracion);
                        }
                        catch (Exception Ex)
                        {
                            Function.InsertBitacoraErrores("Exoneracion/Create", Ex.Message.ToString(), "Create");
                            ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                            ViewBag.Cliente = db.tbCliente.ToList();
                            ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                            return View(tbExoneracion);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
        [HttpPost]
        public JsonResult InactivarCliente(int CodExoneracion, bool Activo)
        {
            var list = db.UDP_Vent_tbExoneracion_Estado(CodExoneracion, Helpers.ExoneracionInactiva, Function.GetUser(), Function.DatetimeNow()).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActivarCliente(int CodExoneracion, bool Activo)
        {
            var list = db.UDP_Vent_tbExoneracion_Estado(CodExoneracion, Helpers.ExoneracionActiva, Function.GetUser(), Function.DatetimeNow()).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
