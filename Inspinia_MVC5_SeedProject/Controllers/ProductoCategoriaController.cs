using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using Newtonsoft.Json;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class ProductoCategoriaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /ProductoCategoria/
        [SessionManager("ProductoCategoria/Index")]
        public ActionResult Index()
        {
            try { ViewBag.smserror = TempData["smserror"].ToString(); } catch { }


            var tbproductocategoria = db.tbProductoCategoria.Include(t => t.tbProductoSubcategoria);
            //ViewBag.smserror = "";
            return View(db.tbProductoCategoria.ToList());
        }

        // GET: /ProductoCategoria/Details/5
        [SessionManager("ProductoCategoria/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            if (tbProductoCategoria == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbProductoCategoria);
        }

        [HttpPost]
        public JsonResult removeSubCategoria(tbProductoSubcategoria borrado)
        {
            var list = (List<tbProductoSubcategoria>)Session["tbProductoSubCategoria"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.pscat_Id == borrado.pscat_Id);
                list.Remove(itemToRemove);
                Session["tbProductoSubCategoria"] = list;
            }

            return Json(list, JsonRequestBehavior.AllowGet);

        }

        // GET: /ProductoCategoria/Create
        [SessionManager("ProductoCategoria/Create")]
        public ActionResult Create()
        {
            Session["tbProductoSubcategoria"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("ProductoCategoria/Create")]
        public ActionResult Create([Bind(Include = "pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica, pcat_EsActivo")] tbProductoCategoria tbProductoCategoria)
        {
            IEnumerable<object> cate = null;
            IEnumerable<object> sub = null;
            string MsjError = "";
            string mensajeDetail = "";
            var listaSubCategoria = (List<tbProductoSubcategoria>)Session["tbProductoSubcategoria"];
            if (ModelState.IsValid)
            {
                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {
                        cate = db.UDP_Inv_tbProductoCategoria_Insert(tbProductoCategoria.pcat_Nombre, Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Inv_tbProductoCategoria_Insert_Result categoria in cate)
                            MsjError = categoria.MensajeError;
                        if (MsjError.StartsWith("-1"))
                        {
                            Function.InsertBitacoraErrores("ProductoCategoria/Create", MsjError, "Create");
                            ModelState.AddModelError("", "1. No se pudo insertar el registro, favor contacte al administrador." + MsjError);
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
                                                                                    , Convert.ToInt16(MsjError),
                                                                                    Function.GetUser(), Function.DatetimeNow(),
                                                                                    subcategoria.pscat_ISV
                                                                                    );
                                        foreach (UDP_Inv_tbProductoSubcategoria_Insert_Result ProdSubCate in sub)
                                            mensajeDetail = ProdSubCate.MensajeError;
                                        if (mensajeDetail.StartsWith("-1"))
                                        {
                                            Function.InsertBitacoraErrores("ProductoCategoria/Create", mensajeDetail, "Create");
                                            ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                            return View(tbProductoCategoria);
                                        }
                                    }
                                }
                            }
                            _Tran.Complete();
                        }

                    }
                    catch (Exception Ex)
                    {
                        
                        Ex.Message.ToString();
                        string msj = Function.InsertBitacoraErrores("ProductoCategoria/Create", Ex.Message.ToString(), "Create");
                        ModelState.AddModelError("", "2. No se pudo insertar el registro, favor contacte al administrador");
                        return View(tbProductoCategoria);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(tbProductoCategoria);
        }

        // GET: /ProductoCategoria/Edit/5
        [SessionManager("ProductoCategoria/Edit")]
        public ActionResult Edit(int? id)
        {
            try
            {
                ViewBag.smserror = TempData["smserror"].ToString();
            }
            catch { }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            if (tbProductoCategoria == null)
            {
                return RedirectToAction("NotFound", "Login");
            }

            Session["tbProductoSubcategoria"] = null;
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

        [HttpPost]
        public JsonResult GetSubCate(int pscat_Id)
        {
            var list = db.SDP_tbProductoSubcategoria_Select(pscat_Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateSubCategoria(tbProductoSubcategoria EditarSubCategoria)
        {
            string Msj = "";
            try
            {
                IEnumerable<object> list = null;

                tbProductoSubcategoria subcater = db.tbProductoSubcategoria.Find(EditarSubCategoria.pscat_Id);
                list = db.UDP_Inv_tbProductoSubcategoria_Update(
                                                        EditarSubCategoria.pscat_Id,
                                                        EditarSubCategoria.pscat_Descripcion,
                                                       EditarSubCategoria.pcat_Id,
                                                       EditarSubCategoria.pscat_UsuarioCrea,
                                                       subcater.pscat_FechaCrea,
                                                      Function.GetUser(), Function.DatetimeNow(),
                                                      EditarSubCategoria.pscat_ISV
                    );
                foreach (UDP_Inv_tbProductoSubcategoria_Update_Result subcate in list)
                    Msj = subcate.MensajeError;

                if (Msj.StartsWith("-1"))
                {
                    Msj = "-1";
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Guardo el registro");
                Msj = "-1";
            }
            return Json(Msj, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("ProductoCategoria/Edit")]
        public ActionResult Edit(int? id, [Bind(Include = "pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica")] tbProductoCategoria tbProductoCategoria)
        {
            IEnumerable<object> cate = null;
            IEnumerable<object> subcate = null;
            string MsjError = "";
            string MensajeError = "";
            var List = (List<tbProductoSubcategoria>)Session["tbProductoSubCategoria"];
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
                                              , Function.GetUser(),
                                              Function.DatetimeNow());
                        foreach (UDP_Inv_tbProductoCategoria_Update_Result ProductoCategoria in cate)
                            MsjError = ProductoCategoria.MensajeError;

                        if (MsjError.StartsWith("-1"))
                        {
                            Function.InsertBitacoraErrores("ProductoCategoria/Edit", MsjError, "Edit");
                            ModelState.AddModelError("", "1. No se pudo actualizar el registro, favor contacte al administrador.");
                            return View(tbProductoCategoria);
                        }
                        else
                        {
                            if (List != null && List.Count > 0)
                            {
                                foreach (tbProductoSubcategoria subcategoria in List)
                                {
                                    subcate = db.UDP_Inv_tbProductoSubcategoria_Insert(subcategoria.pscat_Descripcion
                                                                                , Convert.ToInt16(MsjError),
                                                                                Function.GetUser(), Function.DatetimeNow(),
                                                                                subcategoria.pscat_ISV
                                                                                );

                                    foreach (UDP_Inv_tbProductoSubcategoria_Insert_Result ProdSubCate in subcate)
                                        MensajeError = ProdSubCate.MensajeError;
                                    if (MensajeError.StartsWith("-1"))
                                    {
                                        string esto = Function.InsertBitacoraErrores("ProductoCategoria/Edit", MensajeError, "Edit");
                                        ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                        return View(tbProductoCategoria);
                                    }
                                }
                            }
                            _Tran.Complete();
                            return RedirectToAction("Edit/" + MsjError);
                        }
                    }
                    catch (Exception Ex)
                    {
                        Function.InsertBitacoraErrores("ProductoCategoria/Edit", Ex.Message.ToString(), "Edit");
                        ModelState.AddModelError("", "2. No se pudo actualizar el registro, favor contacte al administrador.");
                        return RedirectToAction("Edit/" + MsjError);
                    }
                }
            }
            return View(tbProductoCategoria);
        }

        //funciona 
        [SessionManager("ProductoCategoria/Delete")]
        public ActionResult EliminarProductoCategoria(int? id)
        {

            try
            {
                tbProductoCategoria obj = db.tbProductoCategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
               
                list = db.UDP_Inv_tbProductoCategoria_Delete(id);
                foreach (UDP_Inv_tbProductoCategoria_Delete_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-2"))
                {
                    TempData["smserror"] = " No se puede eliminar el dato porque tiene dependencia.";
                    ViewBag.smserror = TempData["smserror"];

                    ModelState.AddModelError("", "No se puede borrar el registro");
                    return RedirectToAction("Edit/" + id);
                }

                else
                {

                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se puede borrar el registro");
                return RedirectToAction("Index");
            }




        }
        ///APLICAR ESTE DELETE 
        public ActionResult DeteleSub(int? id)
        {

            try
            {
                tbProductoSubcategoria obj = db.tbProductoSubcategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoSubCategoria_Delete(id);
                foreach (UDP_Inv_tbProductoSubCategoria_Delete_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith ("-2"))
                {
                    TempData["smserror"] = " No se puede eliminar el dato porque tiene dependencia.";
                    ViewBag.smserror = TempData["smserror"];

                    ModelState.AddModelError("", "No se puede borrar el registro");
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
                ModelState.AddModelError("", "No se puede borrar el registro");
                return RedirectToAction("Index");
            }

        }

        public ActionResult ActivarCateValidacion(int? id)
        {

            try
            {
                tbProductoCategoria obj = db.tbProductoCategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoCategoria_Update_Estado_Validacion(id, Function.GetUser(), Function.DatetimeNow(), Helpers.CategoriaActivo);
                foreach (UDP_Inv_tbProductoCategoria_Update_Estado_Validacion_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-2"))
                {
                    TempData["smserror"] = " No se puede eliminar el dato porque tiene dependencia.";
                    ViewBag.smserror = TempData["smserror"];

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

        public ActionResult ActivarSubValidacion(int? id)
        {

            try
            {
                tbProductoSubcategoria obj = db.tbProductoSubcategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoSubCategoria_Update_Estado_Validacion(id, Helpers.SubcategoriaActivo, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbProductoSubCategoria_Update_Estado_Validacion_Result obje in list)
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

        public ActionResult InactivarSubValidacion(int? id)
        {

            try
            {
                tbProductoSubcategoria obj = db.tbProductoSubcategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoSubCategoria_Update_Estado_Validacion(id, Helpers.SubcategoriaInactivo, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbProductoSubCategoria_Update_Estado_Validacion_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-2"))
                {
                    TempData["smserror"] = " No se puede cambiar el estado del dato porque tiene dependencia.";
                    ViewBag.smserror = TempData["smserror"];
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


        public ActionResult InactivarCateValidacion(int? id)
        {

            try
            {
                tbProductoCategoria obj = db.tbProductoCategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoCategoria_Update_Estado_Validacion(id, Function.GetUser(), Function.DatetimeNow(), Helpers.CategoriaInactivo);
                foreach (UDP_Inv_tbProductoCategoria_Update_Estado_Validacion_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-2"))
                {
                    TempData["smserror"] = " No se puede cambiar el estado del dato porque tiene dependencia.";
                    ViewBag.smserror = TempData["smserror"];

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

