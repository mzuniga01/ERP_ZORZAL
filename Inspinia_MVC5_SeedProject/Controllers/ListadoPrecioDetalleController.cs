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
    public class ListadoPrecioDetalleController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /ListadoPrecioDetalle/
        public ActionResult Index()
        {
            var tblistadopreciodetalle = db.tbListadoPrecioDetalle.Include(t => t.tbListaPrecio);
            return View(tblistadopreciodetalle.ToList());
        }

        // GET: /ListadoPrecioDetalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbListadoPrecioDetalle tbListadoPrecioDetalle = db.tbListadoPrecioDetalle.Find(id);
            if (tbListadoPrecioDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbListadoPrecioDetalle);
        }

        // GET: /ListadoPrecioDetalle/Create
        public ActionResult Create()
        {
            ViewBag.listp_Id = new SelectList(db.tbListaPrecio, "listp_Id", "prod_Codigo");
            return View();
        }

        // POST: /ListadoPrecioDetalle/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="listp_Id,prod_Codigo,lispd_PrecioMayorista,lispd_Preciominorista,lispd_Fechainiciovigencia,lispd_Fechaifinalvigencia,lispd_DescCaja,lispd_DescGerente,lispd_UsuarioCrea,lispd_FechaCrea,lispd_UsuarioModifica,lispd_FechaModifica")] tbListadoPrecioDetalle tbListadoPrecioDetalle)
        {
            if (ModelState.IsValid)
            {
                db.tbListadoPrecioDetalle.Add(tbListadoPrecioDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.listp_Id = new SelectList(db.tbListaPrecio, "listp_Id", "prod_Codigo", tbListadoPrecioDetalle.listp_Id);
            return View(tbListadoPrecioDetalle);
        }

        // GET: /ListadoPrecioDetalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbListadoPrecioDetalle tbListadoPrecioDetalle = db.tbListadoPrecioDetalle.Find(id);
            if (tbListadoPrecioDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.listp_Id = new SelectList(db.tbListaPrecio, "listp_Id", "prod_Codigo", tbListadoPrecioDetalle.listp_Id);
            return View(tbListadoPrecioDetalle);
        }

        // POST: /ListadoPrecioDetalle/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="listp_Id,prod_Codigo,lispd_PrecioMayorista,lispd_Preciominorista,lispd_Fechainiciovigencia,lispd_Fechaifinalvigencia,lispd_DescCaja,lispd_DescGerente,lispd_UsuarioCrea,lispd_FechaCrea,lispd_UsuarioModifica,lispd_FechaModifica")] tbListadoPrecioDetalle tbListadoPrecioDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbListadoPrecioDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.listp_Id = new SelectList(db.tbListaPrecio, "listp_Id", "prod_Codigo", tbListadoPrecioDetalle.listp_Id);
            return View(tbListadoPrecioDetalle);
        }

        // GET: /ListadoPrecioDetalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbListadoPrecioDetalle tbListadoPrecioDetalle = db.tbListadoPrecioDetalle.Find(id);
            if (tbListadoPrecioDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbListadoPrecioDetalle);
        }

        // POST: /ListadoPrecioDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbListadoPrecioDetalle tbListadoPrecioDetalle = db.tbListadoPrecioDetalle.Find(id);
            db.tbListadoPrecioDetalle.Remove(tbListadoPrecioDetalle);
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
