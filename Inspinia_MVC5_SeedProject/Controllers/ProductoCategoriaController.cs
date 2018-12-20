using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Transactions;

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
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre", "Seleccione");
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
        public ActionResult Create([Bind(Include = "pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica, pcat_Estado")] tbProductoCategoria tbProductoCategoria)
        {
            IEnumerable<object> list = null;
            IEnumerable<object> lista = null;
            var MensajeError = "";
            var MsjError = "";
            var listasubcategoria = (List<tbProductoSubcategoria>)Session["tbSubCategoria"];
            if (ModelState.IsValid)
            {
                using (TransactionScope _Tran = new TransactionScope())
                {

                    try
                    {

                        //list = db.UDP_Inv_tbProductoCategoria_Insert(tbProductoCategoria.pcat_Nombre);
                        //foreach (UDP_Inv_tbProductoCategoria_Insert_Result categoria in list)
                        //    MsjError = categoria.MensajeError;
                        if (MsjError.Substring(0, 2) == "-1")
                        {
                            ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");
                            return View(tbProductoCategoria);
                        }
                        else
                        {
                            if (listasubcategoria != null)
                            {
                                if (listasubcategoria.Count > 0)
                                {
                                    foreach (tbProductoSubcategoria sub in listasubcategoria)
                                    {

                                        //lista = db.UDP_Inv_tbProductoSubcategoria_Insert(sub.pscat_Descripcion, sub.pcat_Id, sub.pscat_EsActiva);
                                        //foreach (UDP_Inv_tbProductoSubcategoria_Update_Result subcategoria in lista)
                                            //MensajeError = (subcategoria.MensajeError);

                                        if (MensajeError.Substring(0, 1) == "")
                                        {
                                            ModelState.AddModelError("", "No se Guardo el Registro");
                                            return View(tbProductoCategoria);
                                        }
                                        else
                                        {
                                            _Tran.Complete();
                                            return RedirectToAction("Index");
                                        }
                                    }
                                }
                            }

                            else
                            {
                                _Tran.Complete();
                                return RedirectToAction("Index");
                            }

                        }

                    }

                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "No se Guardo el registro");
                        return View(tbProductoCategoria);
                    }

                }
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


        // POST: /ProductoCategoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include= "pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica,pcat_Estado,tbUsuario,tbUsuario1")] tbProductoCategoria tbProductoCategoria)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    tbProductoCategoria categorias = db.tbProductoCategoria.Find(id);
                    IEnumerable<object> list = null;
                    var MsjError = "";
                    list = db.UDP_Inv_tbProductoCategoria_Update(tbProductoCategoria.pcat_Id,
                        tbProductoCategoria.pcat_Nombre,
                        tbProductoCategoria.pcat_UsuarioCrea,
                        tbProductoCategoria.pcat_FechaCrea,
                        tbProductoCategoria.pcat_Estado);
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

      



        public ActionResult ActivarSub(int? id)
        {

            try
            {
                tbProductoSubcategoria obj = db.tbProductoSubcategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoSubCategoria_Update_Estado(id, EstadoSubCategoria.Activo);
                foreach (UDP_Inv_tbProductoSubCategoria_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
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
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Index");
            }


            //return RedirectToAction("Index");
        }
        public ActionResult ActivarCate(int? id)
        {

            try
            {
                //tbProductoCategoria obj = db.tbProductoCategoria.Find(id);
                //IEnumerable<object> list = null;
                var MsjError = "";
                //list = db.UDP_Inv_tbProductoCategoria_Update_Estado(id, Helpers.Activo);
                //foreach (UDP_Inv_tbProductoCategoria_Update_Estado_Result obje in list)
                //MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
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
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Index");
            }


            //return RedirectToAction("Index");
        }

        public ActionResult InactivarSub(int? id)
        {

            try
            {
                tbProductoSubcategoria obj = db.tbProductoSubcategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoSubCategoria_Update_Estado(id, EstadoSubCategoria.Inactivo);
                foreach (UDP_Inv_tbProductoSubcategoria_Update_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
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
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Index");
            }
          }


        public ActionResult InactivarCate(int? id)
        {

            try
            {
                tbProductoCategoria obj = db.tbProductoCategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                //list = db.UDP_Inv_tbProductoCategoria_Update_Estado(id, Helpers.Inactivo);
                //foreach (UDP_Inv_tbProductoCategoria_Update_Estado_Result obje in list)
                    //MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
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
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Index");
            }
        }

    }

    }

