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
    public class ObjetoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /Objeto/
        [SessionManager("Objeto/Index")]
        public ActionResult Index()
        {
            return View(db.tbObjeto.ToList());
        }

        // GET: /Objeto/Details/5
        [SessionManager("Objeto/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbObjeto tbObjeto = db.tbObjeto.Find(id);
            if (tbObjeto == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbObjeto);
        }

        // GET: /Objeto/Create
        [SessionManager("Objeto/Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Objeto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Objeto/Create")]
        public ActionResult Create([Bind(Include = "obj_Id,obj_Pantalla,obj_Referencia,obj_UsuarioCrea,obj_FechaCrea,obj_UsuarioModifica,obj_FechaModifica,obj_Estado")] tbObjeto tbObjeto)
        {
            if (db.tbObjeto.Any(a => a.obj_Pantalla == tbObjeto.obj_Pantalla ))
            {
                ModelState.AddModelError("", "Ya existe un objeto con este nombre de Pantalla, favor registrar otro");
            }
            if (db.tbObjeto.Any(a =>  a.obj_Referencia == tbObjeto.obj_Referencia))
            {
                ModelState.AddModelError("", "Ya existe un objeto con esta Referencia, favor registrar otro");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> list = null;
                    string MsjError = "";
                    list = db.UDP_Acce_tbObjeto_Insert(tbObjeto.obj_Pantalla, tbObjeto.obj_Referencia, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Acce_tbObjeto_Insert_Result obejto in list)
                        MsjError = obejto.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("Objeto/Create", MsjError, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbObjeto);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("Objeto/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador."); ;
                    return View(tbObjeto);
                }
            }
            return View(tbObjeto);
        }


        // GET: /Objeto/Edit/5
        [SessionManager("Objeto/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbObjeto tbObjeto = db.tbObjeto.Find(id);
            if (tbObjeto == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.obj_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioModifica);
            ViewBag.obj_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioCrea);
            return View(tbObjeto);
        }

        // POST: /Objeto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Objeto/Edit")]
        public ActionResult Edit(int? id, [Bind(Include = "obj_Id, obj_Pantalla,obj_Referencia,obj_UsuarioCrea,obj_FechaCrea,obj_UsuarioModifica,obj_FechaModifica,obj_Estado")] tbObjeto tbObjeto)
        {
            //if (db.tbObjeto.Any(a => a.obj_Pantalla == tbObjeto.obj_Pantalla))
            //{
            //    ModelState.AddModelError("", "Ya existe esta pantalla, Favor registrar otra");
            //}
            if (db.tbObjeto.Any(a => a.obj_Pantalla == tbObjeto.obj_Pantalla && a.obj_Id != tbObjeto.obj_Id))
            {
                ModelState.AddModelError("", "Ya existe una pantalla con el mismo nombre");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    tbObjeto obj = db.tbObjeto.Find(id);
                    IEnumerable<object> list = null;
                    var MsjError = "";
                    list = db.UDP_Acce_tbObjeto_Update(tbObjeto.obj_Id,
                                                         tbObjeto.obj_Pantalla,
                                                         tbObjeto.obj_Referencia
                                                         , tbObjeto.obj_UsuarioCrea
                                                         , tbObjeto.obj_FechaCrea
                                                        , Function.GetUser()
                                                        , Function.DatetimeNow());
                    foreach (UDP_Acce_tbObjeto_Update_Result obje in list)
                        MsjError = obje.MensajeError;

                    if (MsjError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("Objeto/Edit", MsjError, "Edit");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        ViewBag.obj_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioModifica);
                        ViewBag.obj_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioCrea);
                        return View(tbObjeto);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("Objeto/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    ViewBag.obj_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioModifica);
                    ViewBag.obj_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioCrea);
                    return View(tbObjeto);
                }

            }

            ViewBag.obj_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioModifica);
            ViewBag.obj_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioCrea);
            return View(tbObjeto);
        }
        //para que cambie estado a activar
        [SessionManager("Objeto/InactivarEstado")]
        public ActionResult EstadoInactivar(int? id)
        {
            try
            {
                tbObjeto obj = db.tbObjeto.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Acce_tbObjeto_Update_Estado(id, Helpers.ObjetoInactivo, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Acce_tbObjeto_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-1"))
                {
                    Function.InsertBitacoraErrores("Objeto/EstadoInactivar", MsjError, "EstadoInactivar");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Function.InsertBitacoraErrores("Objeto/EstadoInactivar", Ex.Message.ToString(), "EstadoInactivar");
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return RedirectToAction("Edit/" + id);
            }
        }
        //para que cambie estado a inactivar
        [SessionManager("Objeto/ActivarEstado")]
        public ActionResult Estadoactivar(int? id)
        {
            try
            {
                tbObjeto obj = db.tbObjeto.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Acce_tbObjeto_Update_Estado(id, Helpers.ObjetoActivo, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Acce_tbObjeto_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    Function.InsertBitacoraErrores("Objeto/Estadoactivar", MsjError, "Estadoactivar");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Function.InsertBitacoraErrores("Objeto/Estadoactivar", Ex.Message.ToString(), "Estadoactivar");
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return RedirectToAction("Edit/" + id);
            }
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
