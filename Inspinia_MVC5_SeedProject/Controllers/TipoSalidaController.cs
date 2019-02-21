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
    public class TipoSalidaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: /TipoSalida/
        public ActionResult Index()
        {
            if (Function.Sesiones("TipoSalida/Index"))
            {

            }
            else
            {
                return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
            }
            return View(db.tbTipoSalida.ToList());
        }

        // GET: /TipoSalida/Details/5
        public ActionResult Details(byte? id)
        {
            if (Function.Sesiones("TipoSalida/Details"))
            {

            }
            else
            {
                return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
            }

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoSalida tbTipoSalida = db.tbTipoSalida.Find(id);
            //ViewBag.UsuarioCrea = db.tbUsuario.Find(tbTipoSalida.tsal_UsuarioCrea).usu_NombreUsuario;
            //var UsuarioModifica = tbTipoSalida.tsal_UsuarioModifica;
            //if (UsuarioModifica == null)
            //{
            //    ViewBag.UsuarioModifica = "";
            //}
            //else
            //{
            //    ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModifica).usu_NombreUsuario;
            //};
            if (tbTipoSalida == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbTipoSalida);
        }

        // GET: /TipoSalida/Create
        public ActionResult Create()
        {
            if (Function.Sesiones("TipoSalida/Create"))
            {

            }
            else
            {
                return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
            }

            return View();
        }

        // POST: /TipoSalida/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tsal_Id,tsal_Descripcion,tsal_UsuarioCrea,tsal_FechaCrea,tsal_UsarioModifica,tsal_FechaCrea")] tbTipoSalida tbTipoSalida)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("TipoSalida/Create"))
                {
                    if (db.tbTipoSalida.Any(a => a.tsal_Descripcion == tbTipoSalida.tsal_Descripcion))
                    {
                        ModelState.AddModelError("", "La Descripcion ya Existe.");

                    }
                    if (ModelState.IsValid)
                    {
                        //db.tbTipoSalida.Add(tbTipoSalida);
                        //db.SaveChanges();
                        try
                        {
                            IEnumerable<object> List = null;
                            var MsjError = "0";
                            List = db.UDP_Inv_tbTipoSalida_Insert(tbTipoSalida.tsal_Descripcion, Function.GetUser(), DateTime.Now);
                            foreach (UDP_Inv_tbTipoSalida_Insert_Result TipoSalida in List)
                                MsjError = TipoSalida.MensajeError;

                            //        if (MsjError.StartsWith("-1"))
                            //        {
                            //            ModelState.AddModelError("Error", "No se Guardo el registro , Contacte al Administrador");
                            //            return View("Create");
                            //        }
                            //        else if (MsjError.StartsWith("0"))
                            //        {
                            //            ModelState.AddModelError("", "La Descripcion ya Existe");
                            //            return View(tbTipoSalida);
                            //        }
                            //        return RedirectToAction("Index");
                            //    }
                            //    catch (Exception Ex)
                            //    {
                            //        Ex.Message.ToString();
                            //        ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");

                            //        return View(tbTipoSalida);
                            //    }

                            //}
                            //else
                            //{
                            //    var errors = ModelState.Values.SelectMany(v => v.Errors);
                            //    return RedirectToAction("Index");
                            //}

 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                           
                            if (MsjError.StartsWith("-1"))
                            {
                                ModelState.AddModelError("Error", "No se Guardo el registro , Contacte al Administrador");
                                return View(tbTipoSalida);
                            }
                          
                            else
                            {
                                return RedirectToAction("Index");
                            }

                        }
                        catch (Exception Ex)
                        {
                            Ex.Message.ToString();
                            ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");

                            return View(tbTipoSalida);
                        }

                    }
                    else
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                    }
                    return View(tbTipoSalida);
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }


        // GET: /TipoSalida/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (Function.Sesiones("TipoSalida/Edit"))
            {

            }
            else
            {
                return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
            }

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoSalida tbTipoSalida = db.tbTipoSalida.Find(id);
           
            if (tbTipoSalida == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbTipoSalida);
        }

        // POST: /TipoSalida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(byte? id, [Bind(Include= "tsal_Id,tsal_Descripcion,tsal_UsuarioCrea,tsal_FechaCrea,tsal_UsuarioModifica,tsal_FechaModifica")] tbTipoSalida tbTipoSalida)
        {
            if (db.tbTipoSalida.Any(a => a.tsal_Descripcion == tbTipoSalida.tsal_Descripcion))
            {
                ModelState.AddModelError("", "La Descripcion ya Existe.");
                ViewBag.UsuarioCrea = db.tbUsuario.Find(tbTipoSalida.tsal_UsuarioCrea).usu_NombreUsuario;

            }
            if (ModelState.IsValid)
            {
                try
                {
                    tbTipoSalida TipoSalida = db.tbTipoSalida.Find(id);
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Inv_tbTipoSalida_Update(tbTipoSalida.tsal_Id,
                                                            tbTipoSalida.tsal_Descripcion
                                                            , tbTipoSalida.tsal_UsuarioCrea
                                                                 , tbTipoSalida.tsal_FechaCrea
                                                             , Function.GetUser()
                                                                , DateTime.Now);
                    foreach (UDP_Inv_tbTipoSalida_Update_Result tsal in List)
                        MsjError = tsal.MensajeError;

                    //if (MsjError == "-1")
                    //{
                    //    ModelState.AddModelError("", "No se guardo el cambio");
                    //    //return RedirectToAction("Index");
                    //}
                    //else
                    //{
                    //    return RedirectToAction("Index");
                    //}
                    if (MsjError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("Error", "No se Guardo el registro , Contacte al Administrador");
                        return View(tbTipoSalida);
                    }
                    //else if (MsjError.StartsWith("0"))
                    //{
                    //    ModelState.AddModelError("", "La Descripcion ya Existe");
                    //    return View(tbTipoSalida);
                    //}
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");

                    return View(tbTipoSalida);
                }
               
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(tbTipoSalida);
        }

        // GET: /TipoSalida/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoSalida tbTipoSalida = db.tbTipoSalida.Find(id);
            if (tbTipoSalida == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbTipoSalida);
        }

        // POST: /TipoSalida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbTipoSalida tbTipoSalida = db.tbTipoSalida.Find(id);
            db.tbTipoSalida.Remove(tbTipoSalida);
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
        public JsonResult GetTipoSalidaExist(string Descripcion)
        {
            var list = db.tbTipoSalida.Where(s => s.tsal_Descripcion == Descripcion).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
     

    }
}
