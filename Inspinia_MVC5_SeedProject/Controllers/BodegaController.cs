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
    public class BodegaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Bodega/
        public ActionResult Index()
        {
            var tbbodega = db.tbBodega.Include(t => t.tbUsuario).Include(t => t.tbMunicipio).Include(t => t.tbEstadoMovimiento);
            return View(tbbodega.ToList());
        }

        // GET: /Bodega/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBodega tbBodega = db.tbBodega.Find(id);
            if (tbBodega == null)
            {
                return HttpNotFound();
            }
            return View(tbBodega);
        }

        // GET: /Bodega/Create
        public ActionResult Create()
        {
            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo");
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            return View();
        }

        // POST: /Bodega/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="bod_Id,bod_ResponsableBodega,bod_Direccion,bod_Correo,bod_Telefono,usu_Id,mun_Codigo,estm_Id,bod_UsuarioCrea,bod_FechaCrea,bod_UsuarioModifica,bod_FechaModifica")] tbBodega tbBodega)
        {
            if (ModelState.IsValid)
            {
                db.tbBodega.Add(tbBodega);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbBodega.usu_Id);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbBodega.mun_Codigo);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbBodega.estm_Id);
            return View(tbBodega);
        }

        // GET: /Bodega/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBodega tbBodega = db.tbBodega.Find(id);
            if (tbBodega == null)
            {
                return HttpNotFound();
            }
            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbBodega.usu_Id);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbBodega.mun_Codigo);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbBodega.estm_Id);
            return View(tbBodega);
        }

        // POST: /Bodega/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="bod_Id,bod_ResponsableBodega,bod_Direccion,bod_Correo,bod_Telefono,usu_Id,mun_Codigo,estm_Id,bod_UsuarioCrea,bod_FechaCrea,bod_UsuarioModifica,bod_FechaModifica")] tbBodega tbBodega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbBodega).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbBodega.usu_Id);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbBodega.mun_Codigo);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbBodega.estm_Id);
            return View(tbBodega);
        }

        // GET: /Bodega/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBodega tbBodega = db.tbBodega.Find(id);
            if (tbBodega == null)
            {
                return HttpNotFound();
            }
            return View(tbBodega);
        }

        // POST: /Bodega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbBodega tbBodega = db.tbBodega.Find(id);
            db.tbBodega.Remove(tbBodega);
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

       public ActionResult Editar()
        {
            return View();
        }

        public ActionResult Crear()
        {
            return View();
        }

        public ActionResult Detalles()
        {
            return View();
        }

        public ActionResult Index_Bodega()
        {
            return View();
        }

    }
}
