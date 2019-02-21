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

namespace ERP_GMEDINA.Controllers
{
    public class ProductoCategoriaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /ProductoCategoria/
        public ActionResult Index()
        {
            try { ViewBag.smserror = TempData["smserror"].ToString(); } catch { }
           

            var tbproductocategoria = db.tbProductoCategoria.Include(t => t.tbProductoSubcategoria);
            //ViewBag.smserror = "";
            return View(db.tbProductoCategoria.ToList());
        }

        // GET: /ProductoCategoria/Details/5
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
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("ProductoCategoria/Create"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }
                if (Function.GetUserRols("ProductoCategoria/Create"))
                {
                    Session["tbProductoSubcategoria"] = null;
                    return View();
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
            ////
           
        }


        // POST: /ProductoCategoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica, pcat_EsActivo")] tbProductoCategoria tbProductoCategoria)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("ProductoCategoria/Create"))
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

                                cate = db.UDP_Inv_tbProductoCategoria_Insert(tbProductoCategoria.pcat_Nombre, Function.GetUser(), DateTime.Now);
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
                                                                                            Function.GetUser(), Function.DatetimeNow(),
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
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /ProductoCategoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("ProductoCategoria/Edit"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }
                if (Function.GetUserRols("ProductoCategoria/Edit"))
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
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
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
                
                    
                list = db.UDP_Inv_tbProductoSubcategoria_Update(
                                                        EditarSubCategoria.pscat_Id,
                                                        EditarSubCategoria.pscat_Descripcion,
                                                       EditarSubCategoria.pcat_Id,
                                                      EditarSubCategoria.pscat_UsuarioCrea,
                                                      EditarSubCategoria.pscat_FechaCrea,
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

        // POST: /ProductoCategoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include= "pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica,pcat_EsActivo")] tbProductoCategoria tbProductoCategoria)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("ProductoCategoria/Edit"))
                {
                    IEnumerable<object> cate = null;
                    IEnumerable<object> subcate = null;
                    var idMaster = 0;
                    var MsjError = "";
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
                                                      DateTime.Now);
                                foreach (UDP_Inv_tbProductoCategoria_Update_Result ProductoCategoria in cate)


                                    idMaster = Convert.ToInt32(ProductoCategoria.MensajeError);

                                if (MsjError == "-1")
                                {
                                    ModelState.AddModelError("", "No se Actualizó el Registro");
                                    ViewBag.pcat_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProductoCategoria.pcat_UsuarioModifica);
                                    ViewBag.pcat_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProductoCategoria.pcat_UsuarioCrea);
                                    return View(tbProductoCategoria);
                                }
                                else
                                {
                                    if (List != null)
                                    {
                                        if (List.Count > 0)
                                        {

                                            foreach (tbProductoSubcategoria subcategoria in List)
                                            {
                                               

                                                subcate = db.UDP_Inv_tbProductoSubcategoria_Insert(subcategoria.pscat_Descripcion
                                                                                            , idMaster
                                                                                            , Function.GetUser(), Function.DatetimeNow(),
                                                                                            subcategoria.pscat_ISV);

                                                foreach (UDP_Inv_tbProductoSubcategoria_Insert_Result ProdSubCate in subcate)

                                                //if (MensajeError == "-1")
                                                {
                                                    ModelState.AddModelError("", "No se Actualizó el Registro");

                                                    //}
                                                    //else
                                                    //{
                                                    //    _Tran.Complete();
                                                    //    return RedirectToAction("Index");
                                                }
                                            }
                                        }
                                    }

                                    //else
                                    {
                                        _Tran.Complete();
                                        return RedirectToAction("Edit/" + idMaster);
                                    }

                                }

                            }
                            catch (Exception Ex)
                            {
                                Ex.Message.ToString();
                                ModelState.AddModelError("", "No se Actualizó el Registro");
                                ViewBag.pcat_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProductoCategoria.pcat_UsuarioModifica);
                                ViewBag.pcat_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProductoCategoria.pcat_UsuarioCrea);
                                //MsjError = "-1";
                            }
                        }
                        return RedirectToAction("Index");
                    }
                    ViewBag.pcat_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProductoCategoria.pcat_UsuarioModifica);
                    ViewBag.pcat_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProductoCategoria.pcat_UsuarioCrea);
                    return View(tbProductoCategoria);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");

        }

        //funciona 
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

        public ActionResult ActivarCate(int? id)
        {

            try
            {
                tbProductoCategoria obj = db.tbProductoCategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoCategoria_Update_Estado(id, Helpers.CategoriaActivo);
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
                list = db.UDP_Inv_tbProductoCategoria_Update_Estado(id, Helpers.CategoriaInactivo);
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

        public ActionResult InactivarSub(int? id)
        {

            try
            {
                tbProductoSubcategoria obj = db.tbProductoSubcategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoSubCategoria_Update_Estado(id, Helpers.SubcategoriaInactivo);
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
        }

        public ActionResult ActivarSub(int? id)
        {

            try
            {
                tbProductoSubcategoria obj = db.tbProductoSubcategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoSubCategoria_Update_Estado(id, Helpers.SubcategoriaActivo);
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
          }

    }

    }

