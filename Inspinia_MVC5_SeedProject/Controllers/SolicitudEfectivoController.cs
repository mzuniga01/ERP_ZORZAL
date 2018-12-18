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
    public class SolicitudEfectivoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /SolicitudEfectivo/
        public ActionResult Index()
        {
            var tbsolicitudefectivo = db.tbSolicitudEfectivo.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbUsuario2).Include(t => t.tbMoneda).Include(t => t.tbMovimientoCaja);
            return View(tbsolicitudefectivo.ToList());
        }

        // GET: /SolicitudEfectivo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            if (tbSolicitudEfectivo == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudEfectivo);
        }

        // GET: /SolicitudEfectivo/Create
        public ActionResult Create()
        {
            ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id");

            ViewBag.Denominacion = db.tbDenominacion.ToList();
            ViewBag.SolicitudEdectivoDetalle = db.tbSolicitudEfectivoDetalle.ToList();
            return View();
        }

        // POST: /SolicitudEfectivo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="solef_Id,mocja_Id,solef_EsApertura,solef_FechaEntrega,solef_UsuarioEntrega,mnda_Id,solef_EsAnulada,solef_UsuarioCrea,solef_FechaCrea,solef_UsuarioModifica,solef_FechaModifica")] tbSolicitudEfectivo tbSolicitudEfectivo)
        {
            if (ModelState.IsValid)
            {
                db.tbSolicitudEfectivo.Add(tbSolicitudEfectivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioCrea);
            ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioEntrega);
            ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbSolicitudEfectivo.mocja_Id);
            return View(tbSolicitudEfectivo);
        }

        // GET: /SolicitudEfectivo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            if (tbSolicitudEfectivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioCrea);
            ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioEntrega);
            ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbSolicitudEfectivo.mocja_Id);
            return View(tbSolicitudEfectivo);
        }

        // POST: /SolicitudEfectivo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="solef_Id,mocja_Id,solef_EsApertura,solef_FechaEntrega,solef_UsuarioEntrega,mnda_Id,solef_EsAnulada,solef_UsuarioCrea,solef_FechaCrea,solef_UsuarioModifica,solef_FechaModifica")] tbSolicitudEfectivo tbSolicitudEfectivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSolicitudEfectivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioCrea);
            ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioEntrega);
            ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbSolicitudEfectivo.mocja_Id);
            return View(tbSolicitudEfectivo);
        }

        // GET: /SolicitudEfectivo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            if (tbSolicitudEfectivo == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudEfectivo);
        }

        // POST: /SolicitudEfectivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            db.tbSolicitudEfectivo.Remove(tbSolicitudEfectivo);
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
