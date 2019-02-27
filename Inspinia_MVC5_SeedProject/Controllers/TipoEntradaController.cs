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
    public class TipoEntradaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: /TipoEntrada/
        [SessionManager("TipoEntrada/Index")]
        public ActionResult Index()
        {
            return View(db.tbTipoEntrada.ToList());
        }

        // GET: /TipoEntrada/Details/5
        [SessionManager("TipoEntrada/Details")]
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoEntrada tbTipoEntrada = db.tbTipoEntrada.Find(id);
            if (tbTipoEntrada == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbTipoEntrada);
        }

        // GET: /TipoEntrada/Create
        [SessionManager("TipoEntrada/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("TipoEntrada/Create")]
        public ActionResult Create([Bind(Include = "tent_Id,tent_Descripcion,tent_UsuarioCrea,tent_FechaCrea,tent_UsarioModifica,tent_FechaCrea")] tbTipoEntrada tbTipoEntrada)
        {
            if (db.tbTipoEntrada.Any(a => a.tent_Descripcion == tbTipoEntrada.tent_Descripcion))
            {
                ModelState.AddModelError("", "Ya existe este tipo de Entrada, Favor registrar otra");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> list = null;
                    var MsjError = "";
                    list = db.UDP_Inv_tbTipoEntrada_Insert(tbTipoEntrada.tent_Descripcion, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Inv_tbTipoEntrada_Insert_Result TipoEntrada in list)
                        MsjError = TipoEntrada.MensajeError;
                    if (MsjError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo almacenar el registro");
                        return View(tbTipoEntrada);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro");
                }
                return RedirectToAction("Index");
            }
            return View(tbTipoEntrada);
        }

        // GET: /TipoEntrada/Edit/5
        [SessionManager("TipoEntrada/Edit")]
        public ActionResult Edit(byte? id)
        {
            ViewBag.id = id;
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoEntrada tbTipoEntrada = db.tbTipoEntrada.Find(id);
            if (tbTipoEntrada == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.tent_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoEntrada.tent_UsuarioCrea);
            ViewBag.tent_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoEntrada.tent_UsuarioModifica);

            return View(tbTipoEntrada);
        }

        // POST: /TipoEntrada/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("TipoEntrada/Edit")]
        public ActionResult Edit(byte? id, [Bind(Include = "tent_Id,tent_Descripcion,tent_UsuarioCrea,tent_FechaCrea, tent_UsuarioModifica, tent_FechaModifica")] tbTipoEntrada tbTipoEntrada)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tbTipoEntrada TipoEntrada = db.tbTipoEntrada.Find(id);
                    IEnumerable<object> list = null;
                    string MsjError = "";
                    list = db.UDP_Inv_tbTipoEntrada_Update(tbTipoEntrada.tent_Id
                                                            , tbTipoEntrada.tent_Descripcion
                                                            , tbTipoEntrada.tent_UsuarioCrea
                                                            , tbTipoEntrada.tent_FechaCrea, Function.GetUser()
                                                        , Function.DatetimeNow());
                    foreach (UDP_Inv_tbTipoEntrada_Update_Result tent in list)
                        MsjError = tent.MensajeError;

                    if (MsjError.Substring(0, 2) == "-1")
                    {
                        ModelState.AddModelError("", "No se guardo el cambio");
                        ViewBag.tent_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoEntrada.tent_UsuarioCrea);
                        ViewBag.tent_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoEntrada.tent_UsuarioModifica);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //db.Entry(tbTipoEntrada).State = EntityState.Modified;
                        //db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ViewBag.tent_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoEntrada.tent_UsuarioCrea);
                    ViewBag.tent_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoEntrada.tent_UsuarioModifica);
                    ModelState.AddModelError("", "No se guardo el cambio");
                }
                return RedirectToAction("Index");
            }
            ViewBag.tent_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoEntrada.tent_UsuarioCrea);
            ViewBag.tent_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoEntrada.tent_UsuarioModifica);
            return View(tbTipoEntrada);
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
