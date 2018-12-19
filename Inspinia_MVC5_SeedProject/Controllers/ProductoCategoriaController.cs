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
    public class ProductoCategoriaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /ProductoCategoria/
        public ActionResult Index()
        {
            var tbproductocategoria = db.tbProductoCategoria.Include(t => t.tbProductoSubcategoria);
            return View(db.tbProductoCategoria.ToList());
        }

        // GET: /ProductoCategoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            if (tbProductoCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbProductoCategoria);
        }


        // GET: /ProductoCategoria/Create
        public ActionResult Create()
        {
            LlenarLista();
            return View();
        }

        private void LlenarLista()
        {

            ViewBag.CateList = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre", "seleccione");
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pcat_Id", "pscat_Id");
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pcat_Id", "pcat_Nombre");

        }

        // POST: /ProductoCategoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica")] tbProductoCategoria tbProductoCategoria)
        {
            
            if (ModelState.IsValid)
            { 
                try
                {
                    IEnumerable<object> list = null;
                    var MsjError = "";
                    list = db.UDP_Inv_tbProductoCategoria_Insert(tbProductoCategoria.pcat_Nombre);
                    foreach (UDP_Inv_tbProductoCategoria_Insert_Result categoria in list)
                        MsjError = categoria.MensajeError;
                    if (MsjError.Substring(0, 2) == "-1")
                    {
                        ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro");
                }
            return RedirectToAction("Index");
        }
            else
            {
                ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
            }
            
            return View(tbProductoCategoria);
       
    }

        // GET: /ProductoCategoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            if (tbProductoCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbProductoCategoria);
        }

        // POST: /ProductoCategoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include= "pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea")]
        tbProductoCategoria tbProductoCategoria)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    tbProductoCategoria categorias = db.tbProductoCategoria.Find(id);
                    IEnumerable<object> list = null;
                    var MsjError = "";
                    //list = db.UDP_Inv_tbProductoCategoria_Update(tbProductoCategoria.pcat_Id,
                    //    tbProductoCategoria.pcat_Nombre, categorias.pcat_UsuarioCrea,
                    //    categorias.pcat_FechaCrea);
                    foreach (UDP_Inv_tbProductoCategoria_Update_Result categoria in list) 
                        MsjError = categoria.MensajeError;

                    if (MsjError.Substring(0, 2) == "-1")
                    {
                        ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");
                        return RedirectToAction("Index");
                    }
                    else      
                     {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro");
                }

                return RedirectToAction("Index");
            }
            return View(tbProductoCategoria);
        }
        // GET: /ProductoCategoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            if (tbProductoCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbProductoCategoria);
        }

        // POST: /ProductoCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            //IEnumerable<object> list = null;
            //var MsjError = "";
            //list = db.UDP_Inv_tbProductoCategoria_Delete(tbProductoCategoria.pcat_Id);
            //foreach (UDP_Inv_tbProductoCategoria_Delete_Result categoria in list)
            //    MsjError = categoria.MensajeError;
            //db.tbProductoCategoria.Remove(tbProductoCategoria);
            //db.SaveChanges();
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
        //VISTAS DE ProductoSubCategoía

        [HttpPost]
        public JsonResult GuardarSubCategoria(tbProductoSubcategoria tbsubcategoria)
        {
            List<tbProductoSubcategoria> sessionCate = new List<tbProductoSubcategoria>();
            var list = (List<tbProductoSubcategoria>)Session["Descripcion"];
            if (list == null)
            {
                sessionCate.Add(tbsubcategoria);
                Session["Descripcion"] = sessionCate;
            }
            else
            {
                list.Add(tbsubcategoria);
                Session["Descripcion"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }



    }
}
