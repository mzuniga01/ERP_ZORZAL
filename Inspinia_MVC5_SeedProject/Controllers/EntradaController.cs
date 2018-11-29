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
    public class EntradaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Entrada/
        public ActionResult Index()
        {
            var tbentrada = db.tbEntrada.Include(t => t.tbTiposEntrada).Include(t => t.tbBodega).Include(t => t.tbEstadoMovimiento).Include(t => t.tbFactura).Include(t => t.tbProveedor);
            return View(tbentrada.ToList());
        }

        // GET: /Entrada/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            if (tbEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbEntrada);
        }

        // GET: /Entrada/Create
        public ActionResult Create()
        {
            ViewBag.tent_Id = new SelectList(db.tbTiposEntrada, "tent_Id", "tent_Descripcion");
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega");
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo");
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_NombreContacto");
            return View();
        }

        // POST: /Entrada/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ent_Id,ent_NumeroFormato,ent_Fecha,bod_Id,estm_Id,prov_Id,ent_CompraNumero,ent_CompraFecha,fact_Codigo,ent_RazonDevolucion,ent_BodegaDestino,tent_Id,ent_UsuarioCrea,ent_FechaCrea,ent_UsuarioModifica,ent_FechaModifica")] tbEntrada tbEntrada)
        {
            if (ModelState.IsValid)
            {
                db.tbEntrada.Add(tbEntrada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tent_Id = new SelectList(db.tbTiposEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbEntrada.bod_Id);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbEntrada.estm_Id);
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbEntrada.fact_Codigo);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_NombreContacto", tbEntrada.prov_Id);
            return View(tbEntrada);
        }

        // GET: /Entrada/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            if (tbEntrada == null)
            {
                return HttpNotFound();
            }
            ViewBag.tent_Id = new SelectList(db.tbTiposEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbEntrada.bod_Id);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbEntrada.estm_Id);
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbEntrada.fact_Codigo);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_NombreContacto", tbEntrada.prov_Id);
            return View(tbEntrada);
        }

        // POST: /Entrada/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ent_Id,ent_NumeroFormato,ent_Fecha,bod_Id,estm_Id,prov_Id,ent_CompraNumero,ent_CompraFecha,fact_Codigo,ent_RazonDevolucion,ent_BodegaDestino,tent_Id,ent_UsuarioCrea,ent_FechaCrea,ent_UsuarioModifica,ent_FechaModifica")] tbEntrada tbEntrada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEntrada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tent_Id = new SelectList(db.tbTiposEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbEntrada.bod_Id);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbEntrada.estm_Id);
            ViewBag.fact_Codigo = new SelectList(db.tbFactura, "fact_Codigo", "cja_Codigo", tbEntrada.fact_Codigo);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_NombreContacto", tbEntrada.prov_Id);
            return View(tbEntrada);
        }

        // GET: /Entrada/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            if (tbEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbEntrada);
        }

        // POST: /Entrada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            db.tbEntrada.Remove(tbEntrada);
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
        public ActionResult DetallePrueba()
        {
            return View();
        }
        public ActionResult CrearPrueba()
        {
            return View();
        }
        public ActionResult EditarPrueba()
        {
            return View();
        }
    }
}
