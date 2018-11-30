using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_ZORZAL.Models;

namespace ERP_ZORZAL.Controllers
{
    public class CajasController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Cajas/
        public ActionResult Index()
        {
            var tbcaja = db.tbCaja.Include(t => t.tbSalidaDetalle);
            return View(tbcaja.ToList());
        }

        // GET: /Cajas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCaja tbCaja = db.tbCaja.Find(id);
            if (tbCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbCaja);
        }

        // GET: /Cajas/Create
        public ActionResult Create()
        {
            ViewBag.sald_Id = new SelectList(db.tbSalidaDetalle, "sald_Id", "prod_Codigo");
            return View();
        }

        // POST: /Cajas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cja_Id,cja_Descripcion,cja_UsuarioCrea,cja_FechaCrea,cja_UsuarioModifica,cja_FechaModifica,sald_Id")] tbCaja tbCaja)
        {
            if (ModelState.IsValid)
            {
                db.tbCaja.Add(tbCaja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sald_Id = new SelectList(db.tbSalidaDetalle, "sald_Id", "prod_Codigo", tbCaja.sald_Id);
            return View(tbCaja);
        }

        // GET: /Cajas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCaja tbCaja = db.tbCaja.Find(id);
            if (tbCaja == null)
            {
                return HttpNotFound();
            }
            ViewBag.sald_Id = new SelectList(db.tbSalidaDetalle, "sald_Id", "prod_Codigo", tbCaja.sald_Id);
            return View(tbCaja);
        }

        // POST: /Cajas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cja_Id,cja_Descripcion,cja_UsuarioCrea,cja_FechaCrea,cja_UsuarioModifica,cja_FechaModifica,sald_Id")] tbCaja tbCaja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCaja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sald_Id = new SelectList(db.tbSalidaDetalle, "sald_Id", "prod_Codigo", tbCaja.sald_Id);
            return View(tbCaja);
        }

        // GET: /Cajas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCaja tbCaja = db.tbCaja.Find(id);
            if (tbCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbCaja);
        }

        // POST: /Cajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCaja tbCaja = db.tbCaja.Find(id);
            db.tbCaja.Remove(tbCaja);
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
        public ActionResult IndexC()
        {
            return View();
        }
        public ActionResult Editar()
        {
            return View();
        }
        public ActionResult Crear()
        {
            return View();
        }
        public ActionResult Detalle()
        {
            return View();
        }
    }
    }
