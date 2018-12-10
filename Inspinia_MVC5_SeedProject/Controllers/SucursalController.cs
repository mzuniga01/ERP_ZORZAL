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
    public class SucursalController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSucursal tbSucursal = db.tbSucursal.Find(id);
            if (tbSucursal == null)
            {
                return HttpNotFound();
            }
            return View(tbSucursal);
        }

        // GET: /Sucursal/Create
        public ActionResult Create()
        {
            ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI");
            return View();
        }

        // POST: /Sucursal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="suc_Id,mun_Codigo,bod_Id,pemi_Id,suc_Correo,suc_Direccion,suc_Telefono,suc_UsuarioCrea,suc_FechaCrea,suc_UsuarioModifica,suc_FechaModifica")] tbSucursal tbSucursal)
        {
            if (ModelState.IsValid)
            {
                db.tbSucursal.Add(tbSucursal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioCrea);
            ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioModifica);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbSucursal.mun_Codigo);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSucursal.bod_Id);
            ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursal.pemi_Id);
            return View(tbSucursal);
        }

        // GET: /Sucursal/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSucursal tbSucursal = db.tbSucursal.Find(id);
            if (tbSucursal == null)
            {
                return HttpNotFound();
            }
            ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioCrea);
            ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioModifica);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbSucursal.mun_Codigo);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSucursal.bod_Id);
            ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursal.pemi_Id);
            return View(tbSucursal);
        }

        // POST: /Sucursal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="suc_Id,mun_Codigo,bod_Id,pemi_Id,suc_Correo,suc_Direccion,suc_Telefono,suc_UsuarioCrea,suc_FechaCrea,suc_UsuarioModifica,suc_FechaModifica")] tbSucursal tbSucursal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSucursal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioCrea);
            ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursal.suc_UsuarioModifica);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbSucursal.mun_Codigo);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSucursal.bod_Id);
            ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursal.pemi_Id);
            return View(tbSucursal);
        }

        // GET: /Sucursal/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSucursal tbSucursal = db.tbSucursal.Find(id);
            if (tbSucursal == null)
            {
                return HttpNotFound();
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
    }
}
