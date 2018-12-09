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
    public class ProductoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Producto/
        public ActionResult Index()
        {
            var tbproducto = db.tbProducto.Include(t => t.tbUsuario).Include(t => t.tbUnidadMedida);
            return View(tbproducto.ToList());
        }

        // GET: /Producto/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return HttpNotFound();
            }
            return View(tbProducto);
        }

        // GET: /Producto/Create
        public ActionResult Create()
        {
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
            return View();
        }

        // POST: /Producto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="prod_Codigo,prod_Descripcion,prod_Marca,prod_Modelo,prod_Talla,prod_Color,pscat_Id,uni_Id,prod_UsuarioCrea,prod_FechaCrea,prod_UsuarioModifica,prod_FechaModifica")] tbProducto tbProducto)
        {
            if (ModelState.IsValid)
            {
                db.tbProducto.Add(tbProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
            return View(tbProducto);
        }

        // GET: /Producto/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
            return View(tbProducto);
        }

        // POST: /Producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="prod_Codigo,prod_Descripcion,prod_Marca,prod_Modelo,prod_Talla,prod_Color,pscat_Id,uni_Id,prod_UsuarioCrea,prod_FechaCrea,prod_UsuarioModifica,prod_FechaModifica")] tbProducto tbProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
            return View(tbProducto);
        }

        // GET: /Producto/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return HttpNotFound();
            }
            return View(tbProducto);
        }

        // POST: /Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbProducto tbProducto = db.tbProducto.Find(id);
            db.tbProducto.Remove(tbProducto);
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
