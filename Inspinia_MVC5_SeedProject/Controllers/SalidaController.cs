using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_ZORZAL.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class SalidaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Salida/
        public ActionResult Index()
        {
            //var tbsalida = db.tbSalida.Include(t => t.tbBodega).Include(t => t.tbBodega1).Include(t => t.tbCaja).Include(t => t.tbFactura);
            //return View(tbsalida.ToList());
            return View();
        }

        // GET: /Salida/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            if (tbSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbSalida);
        }

        // GET: /Salida/Create
        public ActionResult Create()
        {
            ViewBag.sald_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega");
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega");
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo");
            return View();
        }

        // POST: /Salida/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="sal_Codigo,bod_Id,fact_Codigo,sal_FechaSalida,sald_Id,estm_Id,sal_Destino,cja_Id,sal_UsuarioCrea,sal_FechaCrea,sal_UsuarioModifica,sal_FechaModifica")] tbSalida tbSalida)
        {
            if (ModelState.IsValid)
            {
                db.tbSalida.Add(tbSalida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sald_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSalida.sald_Id);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSalida.bod_Id);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbSalida.cja_Id);
            //ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbSalida.fact_Codigo);
            return View(tbSalida);
        }

        // GET: /Salida/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            if (tbSalida == null)
            {
                return HttpNotFound();
            }
            ViewBag.sald_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSalida.sald_Id);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSalida.bod_Id);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbSalida.cja_Id);
            //ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbSalida.fact_Codigo);
            return View(tbSalida);
        }

        // POST: /Salida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="sal_Codigo,bod_Id,fact_Codigo,sal_FechaSalida,sald_Id,estm_Id,sal_Destino,cja_Id,sal_UsuarioCrea,sal_FechaCrea,sal_UsuarioModifica,sal_FechaModifica")] tbSalida tbSalida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSalida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sald_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSalida.sald_Id);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSalida.bod_Id);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbSalida.cja_Id);
            //ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbSalida.fact_Codigo);
            return View(tbSalida);
        }

        // GET: /Salida/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            if (tbSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbSalida);
        }

        // POST: /Salida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSalida tbSalida = db.tbSalida.Find(id);
            db.tbSalida.Remove(tbSalida);
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
        public ActionResult Crear()
        {
            return View();
        }
        public ActionResult Editar()
        {
            return View();
        }
        public ActionResult Detalles()
        {
            return View();
        }
        public ActionResult Index_Salida()
        {
            return View();
        }
    }
}
