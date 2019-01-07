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
            
            return View();
        }


        // POST: /ProductoCategoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica, pcat_EsActivo")] tbProductoCategoria tbProductoCategoria)
        {
            IEnumerable<object> cate = null;
            IEnumerable<object> sub = null;
            var idMaster = 0;
            var MsjError = "";
            var listaSubCategoria = (List<tbProductoSubcategoria>)Session["tbProductoSubcategoria"];
            if (ModelState.IsValid)
            {

                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {

                        cate = db.UDP_Inv_tbProductoCategoria_Insert(tbProductoCategoria.pcat_Nombre);
                        foreach (UDP_Inv_tbProductoCategoria_Insert_Result categoria in cate)
                            idMaster = Convert.ToInt32(categoria.MensajeError);
                        if (MsjError == "-")
                        {
                            ModelState.AddModelError("", "No se Guardo el Registro");
                            return View(tbProductoCategoria);
                        }
                        else
                        {
                            if (listaSubCategoria != null)
                            {
                                if (listaSubCategoria.Count > 0)
                                {
                                    foreach (tbProductoSubcategoria subcategoria in listaSubCategoria)
                                    {
                                        sub = db.UDP_Inv_tbProductoSubcategoria_Insert(subcategoria.pscat_Descripcion
                                                                                    , idMaster,
                                                                                    subcategoria.pscat_ISV
                                                                                    );
                                        foreach (UDP_Inv_tbProductoSubcategoria_Insert_Result ProdSubCate in sub)

                                        //if (MensajeError == "-1")
                                        {
                                            ModelState.AddModelError("", "No se Guardo el Registro");
                                            //return View(tbProductoCategoria);
                                            //}
                                            //else
                                            //{
                                            //    _Tran.Complete();
                                            //    return RedirectToAction("Index");
                                        }
                                    }
                                }
                            }

                            //else
                            {
                                _Tran.Complete();
                                //return RedirectToAction("Index");
                            }

                        }

                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        //ModelState.AddModelError("", "No se Guardo el Registro");
                        //return View(tbProductoCategoria);
                        MsjError = "-1";
                    }
                }
                return RedirectToAction("Index");
            }
          
            return View(tbProductoCategoria);
        }

        [HttpPost]
        public JsonResult GuardarSubCategoria(tbProductoSubcategoria tbsubcategoria)
        {
            List<tbProductoSubcategoria> sessionsubCate = new List<tbProductoSubcategoria>();
            var list = (List<tbProductoSubcategoria>)Session["tbProductoSubCategoria"];
            if (list == null)
            {
                sessionsubCate.Add(tbsubcategoria);
                Session["tbProductoSubCategoria"] = sessionsubCate;
            }
            else
            {
                list.Add(tbsubcategoria);
                Session["tbProductoSubCategoria"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
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
        public JsonResult UpdateSubCategoria(tbProductoSubcategoria ActualizarSubCategoria)
        {
            string Msj = "";
            try
            {
                IEnumerable<object> list = null;
                list = db.UDP_Inv_tbProductoSubcategoria_Update(
                                                        ActualizarSubCategoria.pscat_Id,
                                                        ActualizarSubCategoria.pscat_Descripcion,
                                                       ActualizarSubCategoria.pcat_Id,
                                                       ActualizarSubCategoria.pscat_UsuarioCrea,
                                                      ActualizarSubCategoria.pscat_FechaCrea,
                                                      ActualizarSubCategoria.pscat_ISV
                    );
                foreach (UDP_Inv_tbProductoSubcategoria_Update_Result subcate in list)
                    Msj = subcate.MensajeError;

                if (Msj.Substring(0, 2) == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");


                }
                else
                {
                    //return View("Edit/" + pscat_Id);
                    return Json("Index");
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
            }
            return Json("Index");
        }

        // POST: /ProductoCategoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include= "pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica, pcat_EsActivo, tbUsuario,tbUsuario1")] tbProductoCategoria tbProductoCategoria)
        {
            IEnumerable<object> cate = null;
            IEnumerable<object> sub = null;
            var idMaster = 0;
            var MsjError = "";
            var list = (List<tbProductoSubcategoria>)Session["tbProductoSubCategoria"];
            if (ModelState.IsValid)
            {

                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {

                        cate = db.UDP_Inv_tbProductoCategoria_Update(tbProductoCategoria.pcat_Id,
                                                                     tbProductoCategoria.pcat_Nombre,
                                                                     tbProductoCategoria.pcat_UsuarioCrea,
                                                                     tbProductoCategoria.pcat_FechaCrea
                            );
                        foreach (UDP_Inv_tbBodega_Update_Result categoria in cate)
                            idMaster = Convert.ToInt32(categoria.MensajeError);
                        if (MsjError == "-")
                        {
                            ModelState.AddModelError("", "No se Actualizó el Registro");
                            return View(tbProductoCategoria);
                        }
                        else
                        {
                            if (list != null)
                            {
                                if (list.Count > 0)
                                {
                                    foreach (tbProductoSubcategoria subcate in list)
                                    {
                                        sub = db.UDP_Inv_tbProductoSubcategoria_Update(subcate.pscat_Id,
                                                                                        subcate.pscat_Descripcion,
                                                                                        idMaster,
                                                                                        subcate.pscat_UsuarioCrea,
                                                                                        subcate.pscat_FechaCrea,
                                                                                        subcate.pscat_ISV
                                                                                    );
                                        foreach (UDP_Inv_tbBodegaDetalle_Update_Result ProSubCate in sub)

                                        //if (MensajeError == "-1")
                                        {
                                            ModelState.AddModelError("", "No se Actualizó el Registro");
                                            return View(tbProductoCategoria);
                                            //}
                                            //else
                                            //{
                                            //    _Tran.Complete();
                                            //    return RedirectToAction("Index");
                                        }
                                    }
                                }
                            }

                            //else
                            {
                                _Tran.Complete();
                                //return RedirectToAction("Index");
                            }

                        }

                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "No se Actualizó el Registro");
                        return View(tbProductoCategoria);
                        //MsjError = "-1";
                    }
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
        // GET: /ProductoSubcategoria/Delete/5
        public ActionResult EliminarSub(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoSubcategoria tbproductosubcategoria = db.tbProductoSubcategoria.Find(id);
            if (tbproductosubcategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbproductosubcategoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            db.tbProductoCategoria.Remove(tbProductoCategoria);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        
        [HttpPost, ActionName("EliminarSub")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedSub(int id)
        {

            tbProductoSubcategoria tbProductoSubcategoria = db.tbProductoSubcategoria.Find(id);
            db.tbProductoSubcategoria.Remove(tbProductoSubcategoria);
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

        [HttpPost]
        public JsonResult removeSubCategoria(tbProductoSubcategoria subcate)
        {
            var list = (List<tbProductoSubcategoria>)Session["tbProductoSubCategoria"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.pscat_Id == subcate.pscat_Id);
                list.Remove(itemToRemove);
                Session["tbProductoSubCategoria"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);

        }
        public ActionResult ActivarCate(int? id)
        {

            try
            {
                tbProductoCategoria obj = db.tbProductoCategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoCategoria_Update_Estado(id, EstadoCategoria.Activo);
                foreach (UDP_Inv_tbProductoCategoria_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + id);
            }


            //return RedirectToAction("Index");
        }



        public ActionResult InactivarCate(int? id)
        {

            try
            {
                tbProductoCategoria obj = db.tbProductoCategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoCategoria_Update_Estado(id, EstadoCategoria.Inactivo);
                foreach (UDP_Inv_tbProductoCategoria_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + id);
            }
        }


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
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id); ;
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + id);
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
                foreach (UDP_Inv_tbProductoSubCategoria_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + id);
            }
          }

    }

    }

