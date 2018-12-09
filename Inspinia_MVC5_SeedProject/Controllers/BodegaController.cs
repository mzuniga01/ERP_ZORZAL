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
            ViewBag.mun_Nombre = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.bod_EsActiva = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            //ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.bodd_Id = new SelectList(db.tbBodegaDetalle, "bodd_Id", "prod_Codigo");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "pscat_Id");
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
            //ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pcat_Id");
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "uni_Id");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.Producto = db.tbProducto.ToList();
            ViewBag.pcat_Id_Nombre = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");
            ViewBag.pscat_Id_Nombre = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pcat_Id");

            return View();
        }

        // POST: /Bodega/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="bod_ResponsableBodega,bod_Direccion,bod_Correo,bod_Telefono,usu_Id,mun_Codigo,bod_EsActiva,bod_Nombre")] tbBodega tbBodega)
        {
            if (ModelState.IsValid)
            {
                //db.tbBodega.Add(tbBodega);
                //db.SaveChanges();
                //try
                //{
                //    IEnumerable<object> List = null;
                //    String MsjError = "";
                //    List = db.UDP_Inv_tbBodega_Insert(t)
                //}
                //catch(Exception Ex)
                //{

                //}




                return RedirectToAction("Index");
            }

            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbBodega.usu_Id);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbBodega.mun_Codigo);
            ViewBag.bod_EsActiva = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbBodega.bod_EsActiva);
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
            ViewBag.bod_EsActiva = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbBodega.bod_EsActiva);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbBodega.mun_Codigo);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbBodega.mun_Codigo);
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.pcat_Id_Nombre = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");
            ViewBag.pscat_Id_Nombre = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pcat_Id");
            ViewBag.Producto = db.tbProducto.ToList();

            return View(tbBodega);
        }

        // POST: /Bodega/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="bod_Id,bod_ResponsableBodega,bod_Direccion,bod_Correo,bod_Telefono,usu_Id,mun_Codigo,bod_EsActiva,bod_UsuarioCrea,bod_FechaCrea,bod_UsuarioModifica,bod_FechaModifica,bod_Nombre")] tbBodega tbBodega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbBodega).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbBodega.usu_Id);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbBodega.mun_Codigo);
            ViewBag.bod_EsActiva = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbBodega.bod_EsActiva);
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

        public ActionResult _EditBodegaDetalle()
        {
            return PartialView();
        }


    }
}
