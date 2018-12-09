using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_ZORZAL.Controllers
{
    public class NotaCreditoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /NotaCredito/
        public ActionResult Index()
        {
            var tbnotacredito = db.tbNotaCredito.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbDevolucion).Include(t => t.tbSucursal);
            return View(tbnotacredito.ToList());
        }

        // GET: /NotaCredito/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
            if (tbNotaCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbNotaCredito);
        }

        // GET: /NotaCredito/Create
        public ActionResult Create()
        {
            //ViewBag.nocre_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.nocre_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte");
            ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Id");
            //ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");
            //return View();
            ViewBag.Cliente = db.tbCliente.ToList();
            return View();
        }

        // POST: /NotaCredito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="nocre_Id,nocre_Codigo,dev_Id,clte_Id,suc_Id,nocre_Anulado,nocre_FechaEmision,nocre_MotivoEmision,nocre_Monto,nocre_UsuarioCrea,nocre_FechaCrea,nocre_UsuarioModifica,nocre_FechaModifica,nocre_Estado")] tbNotaCredito tbNotaCredito)
        {
            if (ModelState.IsValid)
            {
                db.tbNotaCredito.Add(tbNotaCredito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nocre_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNotaCredito.nocre_UsuarioCrea);
            ViewBag.nocre_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNotaCredito.nocre_UsuarioModifica);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbNotaCredito.clte_Id);
            ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Id", tbNotaCredito.dev_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbNotaCredito.suc_Id);
            return View(tbNotaCredito);
        }

        // GET: /NotaCredito/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
            if (tbNotaCredito == null)
            {
                return HttpNotFound();
            }
            ViewBag.nocre_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNotaCredito.nocre_UsuarioCrea);
            ViewBag.nocre_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNotaCredito.nocre_UsuarioModifica);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbNotaCredito.clte_Id);
            ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Id", tbNotaCredito.dev_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbNotaCredito.suc_Id);
            return View(tbNotaCredito);
        }

        // POST: /NotaCredito/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="nocre_Id,nocre_Codigo,dev_Id,clte_Id,suc_Id,nocre_Anulado,nocre_FechaEmision,nocre_MotivoEmision,nocre_Monto,nocre_UsuarioCrea,nocre_FechaCrea,nocre_UsuarioModifica,nocre_FechaModifica,nocre_Estado")] tbNotaCredito tbNotaCredito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbNotaCredito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nocre_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNotaCredito.nocre_UsuarioCrea);
            ViewBag.nocre_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNotaCredito.nocre_UsuarioModifica);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbNotaCredito.clte_Id);
            ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Id", tbNotaCredito.dev_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbNotaCredito.suc_Id);
            return View(tbNotaCredito);
        }

        // GET: /NotaCredito/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
            if (tbNotaCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbNotaCredito);
        }

        // POST: /NotaCredito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
            db.tbNotaCredito.Remove(tbNotaCredito);
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
