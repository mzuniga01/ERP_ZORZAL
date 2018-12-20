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
    public class CuponDescuentoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /CuponDescuento/
        public ActionResult Index()
        {
            var tbcupondescuento = db.tbCuponDescuento.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbSucursal);
            return View(tbcupondescuento.ToList());
        }

        // GET: /CuponDescuento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCuponDescuento tbCuponDescuento = db.tbCuponDescuento.Find(id);
            if (tbCuponDescuento == null)
            {
                return HttpNotFound();
            }
            return View(tbCuponDescuento);
        }

        // GET: /CuponDescuento/Create
        public ActionResult Create()
        {
            //ViewBag.cdto_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.cdto_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
            return View();
        }

        // POST: /CuponDescuento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="cdto_ID,suc_Id,cdto_FechaEmision,cdto_FechaVencimiento,cdto_PorcentajeDescuento,cdto_MontoDescuento,cdto_MaximoMontoDescuento,cdto_Redimido,cdto_Anulado,cdto_UsuarioCrea,cdto_FechaCrea,cdto_UsuarioModifica,cdto_FechaModifica,cdto_FechaRedencion")] tbCuponDescuento tbCuponDescuento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbCuponDescuento_Insert(tbCuponDescuento.suc_Id, tbCuponDescuento.cdto_FechaEmision, 
                                                     tbCuponDescuento.cdto_FechaVencimiento, tbCuponDescuento.cdto_PorcentajeDescuento,
                                                     tbCuponDescuento.cdto_MontoDescuento, tbCuponDescuento.cdto_MaximoMontoDescuento,
                                                     tbCuponDescuento.cdto_Redimido, tbCuponDescuento.cdto_Anulado);
                    foreach (UDP_Vent_tbCuponDescuento_Insert_Result CuponDescuento in list)
                        MensajeError = CuponDescuento.MensajeError;
                    if (MensajeError == -1)
                    {
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch(Exception Ex)
                {
                    ModelState.AddModelError("", "Error al agregar el registro" + Ex.Message.ToString());
                    return View(tbCuponDescuento);
                }
            }
            return View(tbCuponDescuento);
        }

        // GET: /CuponDescuento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCuponDescuento tbCuponDescuento = db.tbCuponDescuento.Find(id);
            if (tbCuponDescuento == null)
            {
                return HttpNotFound();
            }
            //ViewBag.cdto_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCuponDescuento.cdto_UsuarioModifica);
            //ViewBag.cdto_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCuponDescuento.cdto_UsuarioCrea);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion", tbCuponDescuento.suc_Id);
            return View(tbCuponDescuento);
        }

        // POST: /CuponDescuento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "cdto_ID,suc_Id,cdto_FechaEmision,cdto_FechaVencimiento,cdto_PorcentajeDescuento,cdto_MontoDescuento,cdto_MaximoMontoDescuento,cdto_Redimido,cdto_Anulado,cdto_UsuarioCrea,cdto_FechaCrea,cdto_UsuarioModifica,cdto_FechaModifica,cdto_FechaRedencion, tbUsuario, tbUsuario1")] tbCuponDescuento tbCuponDescuento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbCuponDescuento_Update(tbCuponDescuento.cdto_ID,tbCuponDescuento.suc_Id, tbCuponDescuento.cdto_FechaEmision, tbCuponDescuento.cdto_FechaVencimiento, tbCuponDescuento.cdto_PorcentajeDescuento,
                        tbCuponDescuento.cdto_MontoDescuento, tbCuponDescuento.cdto_MaximoMontoDescuento, tbCuponDescuento.cdto_Redimido,
                        tbCuponDescuento.cdto_Anulado, tbCuponDescuento.cdto_UsuarioCrea, tbCuponDescuento.cdto_FechaCrea);
                    foreach (UDP_Vent_tbCuponDescuento_Update_Result CuponDescuento in list)
                        MensajeError = CuponDescuento.MensajeError;
                    if (MensajeError == -1)
                    {
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch(Exception Ex)
                {
                    ModelState.AddModelError("", "Error al Editar el registro" + Ex.Message.ToString());
                    return View(tbCuponDescuento);
                }
                
            }
            
            return View(tbCuponDescuento);
        }

        // GET: /CuponDescuento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCuponDescuento tbCuponDescuento = db.tbCuponDescuento.Find(id);
            if (tbCuponDescuento == null)
            {
                return HttpNotFound();
            }
            return View(tbCuponDescuento);
        }

        // POST: /CuponDescuento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCuponDescuento tbCuponDescuento = db.tbCuponDescuento.Find(id);
            db.tbCuponDescuento.Remove(tbCuponDescuento);
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
    }
}