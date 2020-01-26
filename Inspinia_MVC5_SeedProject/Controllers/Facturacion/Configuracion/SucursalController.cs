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
    public class SucursalController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        // GET: /Sucursal/
        public ActionResult Index()
        {
            var tbsucursal = db.tbSucursal.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbMunicipio).Include(t => t.tbBodega).Include(t => t.tbPuntoEmision);
            return View(tbsucursal.ToList());
        }

        // GET: /Sucursal/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSucursal tbSucursal = db.tbSucursal.Find(id);
            if (tbSucursal == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbSucursal);
        }




        public JsonResult Listado_CAI()
        {
            IEnumerable<object> list = null;
            try
            {
                list = db.Listado_CAI().ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToList();
                list = null;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: /Sucursal/Create
        public ActionResult Create()
        {
            ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            var D = db.tbSucursal.Select(x => x.pemi_Id).ToList();
            ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI");
            var Bodegas = db.tbBodega.Select(s => new
            {
                bod_Id = s.bod_Id,
                bod_Nombre = string.Concat(s.mun_Codigo + " - " + s.bod_Nombre)
            }).ToList();

            ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre");

            return View();
        }

        // POST: /Sucursal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="suc_Id,mun_Codigo,bod_Id,pemi_Id,suc_Descripcion,suc_Correo,suc_Direccion,suc_Telefono,suc_UsuarioCrea,suc_FechaCrea,suc_UsuarioModifica,suc_FechaModifica")] tbSucursal tbSucursal)
        {
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            var Bodegas = db.tbBodega.Select(s => new
            {
                bod_Id = s.bod_Id,
                bod_Nombre = string.Concat(s.mun_Codigo + " - " + s.bod_Nombre)
            }).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbSucursal_Insert( tbSucursal.mun_Codigo,
                                                            tbSucursal.bod_Id,
                                                            tbSucursal.pemi_Id,
                                                            tbSucursal.suc_Descripcion,
                                                            tbSucursal.suc_Correo,
                                                            tbSucursal.suc_Direccion,
                                                            tbSucursal.suc_Telefono,
                                                            Function.GetUser(),
                                                            Function.DatetimeNow());
                    foreach (UDP_Vent_tbSucursal_Insert_Result Exoneracion in list)
                        MensajeError = Exoneracion.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbSucursal.mun_Codigo);
                        ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                        ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSucursal.bod_Id);
                        ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursal.pemi_Id);
                        ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursal.bod_Id);
                        ModelState.AddModelError("", "No se pudo insertar el registro, contacte al administrador");
                        ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                        return View(tbSucursal);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbSucursal.mun_Codigo);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSucursal.bod_Id);
                ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursal.pemi_Id);
                ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursal.bod_Id);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                return View(tbSucursal);
            }
            catch (Exception Ex)
            {
                ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbSucursal.mun_Codigo);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSucursal.bod_Id);
                ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursal.pemi_Id);
                ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursal.bod_Id);
                ModelState.AddModelError("", "Error al Agregar Registro " + Ex.Message.ToString());
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                return View(tbSucursal);
            }
        }

        // GET: /Sucursal/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSucursal tbSucursal = db.tbSucursal.Find(id);
            if (tbSucursal == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioCrea);
            ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioModifica);
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbSucursal.tbMunicipio.tbDepartamento.dep_Codigo);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbSucursal.mun_Codigo);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSucursal.bod_Id);
            var D = db.tbSucursal.Select(x => x.pemi_Id).ToList();
            ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI");
            var Bodegas = db.tbBodega.Select(s => new
            {
                bod_Id = s.bod_Id,
                bod_Nombre = string.Concat(s.mun_Codigo + " - " + s.bod_Nombre)
            }).ToList();

            ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursal.bod_Id);

            return View(tbSucursal);
        }

        // POST: /Sucursal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(short? id, [Bind(Include = "suc_Id,mun_Codigo,bod_Id,pemi_Id,suc_Descripcion,suc_Correo,suc_Direccion,suc_Telefono,suc_UsuarioCrea,suc_FechaCrea")] tbSucursal tbSucursal)
        {
            var Bodegas = db.tbBodega.Select(s => new
            {
                bod_Id = s.bod_Id,
                bod_Nombre = string.Concat(s.mun_Codigo + " - " + s.bod_Nombre)
            }).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    tbSucursal pSucursal = db.tbSucursal.Find(id);
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbSucursal_Update(tbSucursal.suc_Id,
                                                            tbSucursal.mun_Codigo,
                                                            tbSucursal.bod_Id,
                                                            tbSucursal.pemi_Id,
                                                            tbSucursal.suc_Descripcion,
                                                            tbSucursal.suc_Correo,
                                                            tbSucursal.suc_Direccion,
                                                            tbSucursal.suc_Telefono,
                                                            pSucursal.suc_UsuarioCrea,
                                                            pSucursal.suc_FechaCrea, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Vent_tbSucursal_Update_Result Exoneracion in list)
                        MensajeError = Exoneracion.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioCrea);
                        ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioModifica);
                        ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbSucursal.tbMunicipio.tbDepartamento.dep_Codigo);
                        ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbSucursal.mun_Codigo);
                        ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSucursal.bod_Id);
                        ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursal.pemi_Id);
                        ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursal.bod_Id);
                        return View(tbSucursal);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioCrea);
                ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioModifica);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbSucursal.tbMunicipio.tbDepartamento.dep_Codigo);
                ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbSucursal.mun_Codigo);
                ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSucursal.bod_Id);
                ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursal.pemi_Id);
                ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursal.bod_Id);
                return View(tbSucursal);
            }
            catch (Exception Ex)
            {
                ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioCrea);
                ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioModifica);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbSucursal.tbMunicipio.tbDepartamento.dep_Codigo);
                ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbSucursal.mun_Codigo);
                ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSucursal.bod_Id);
                ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursal.pemi_Id);
                ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursal.bod_Id);
                ModelState.AddModelError("", "Error al Agregar Registro " + Ex.Message.ToString());
                return View(tbSucursal);
            } 
            
        }

        // GET: /Sucursal/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSucursal tbSucursal = db.tbSucursal.Find(id);
            if (tbSucursal == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbSucursal);
        }

        // POST: /Sucursal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbSucursal tbSucursal = db.tbSucursal.Find(id);
            db.tbSucursal.Remove(tbSucursal);
            db.SaveChanges();
            return RedirectToAction("Index");
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
        public JsonResult GetMunicipios(string CodDepartamento)
        {
            var list = db.spGetMunicipios1(CodDepartamento).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


    }
}
