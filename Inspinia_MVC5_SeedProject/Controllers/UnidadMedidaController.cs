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
    public class UnidadMedidaController : Controller
    {
        internal ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /UnidadMedida/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("UnidadMedida/Index"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }

                if (Function.GetUserRols("UnidadMedida/Index"))
                {
                    return View(db.tbUnidadMedida.ToList());
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /UnidadMedida/Details/5
        public ActionResult Details(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("UnidadMedida/Details"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }

                if (Function.GetUserRols("UnidadMedida/Details"))
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
                    if (tbUnidadMedida == null)
                    {
                        return RedirectToAction("NotFound", "Login");
                    }
                    return View(tbUnidadMedida);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /UnidadMedida/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("UnidadMedida/Create"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }

                if (Function.GetUserRols("UnidadMedida/Create"))
                {
                    return View();
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="uni_Descripcion,uni_Abreviatura,uni_UsuarioCrea,uni_FechaCrea")] tbUnidadMedida tbUnidadMedida)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("UnidadMedida/Create"))
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            IEnumerable<object> List = null;
                            var MsjError = "0";
                            List = db.UDP_Gral_tbUnidadMedida_Insert(tbUnidadMedida.uni_Descripcion, tbUnidadMedida.uni_Abreviatura, Function.GetUser(), DateTime.Now);
                            foreach (UDP_Gral_tbUnidadMedida_Insert_Result uni in List)
                                MsjError = uni.MensajeError;

                            if (MsjError.StartsWith("-1"))
                            {
                                ModelState.AddModelError("", "No se guardó el registro, Contacte al Administrador");
                                return View(tbUnidadMedida);
                            }
                            else if (MsjError.StartsWith("0"))
                            {
                                ModelState.AddModelError("", "La Unidad de Medida ya Existe");
                                return View(tbUnidadMedida);
                            }
                            
                                return RedirectToAction("Index");
                            
                        }
                        catch (Exception Ex)
                        {
                            Ex.Message.ToString();
                            ModelState.AddModelError("", "No se guardó el registro, Contacte al Administrador");
                            return View(tbUnidadMedida);
                        }
                        
                    }
                    else
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        return RedirectToAction("Index");
                    }
                    return View(tbUnidadMedida);
                }
                
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);

                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /UnidadMedida/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("UnidadMedida/Edit"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }

                if (Function.GetUserRols("UnidadMedida/Edit"))
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
                    if (tbUnidadMedida == null)
                    {
                        return RedirectToAction("NotFound", "Login");
                    }
                    return View(tbUnidadMedida);
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id,[Bind(Include= "uni_Id,uni_Descripcion,uni_Abreviatura,uni_UsuarioCrea, uni_FechaCrea,uni_UsuarioModifica,uni_FechaModifica")] tbUnidadMedida tbUnidadMedida)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("UnidadMedida/Edit"))
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            tbUnidadMedida UnidadMedida = db.tbUnidadMedida.Find(id);
                            IEnumerable<object> List = null;
                            var MsjError = "";
                            List = db.UDP_Gral_tbUnidadMedida_Update(tbUnidadMedida.uni_Id,
                                                                        tbUnidadMedida.uni_Descripcion,
                                                                        tbUnidadMedida.uni_Abreviatura,
                                                                        tbUnidadMedida.uni_UsuarioCrea,
                                                                        tbUnidadMedida.uni_FechaCrea
                                                                        , Function.GetUser(), DateTime.Now);
                            foreach (UDP_Gral_tbUnidadMedida_Update_Result uni in List)
                                MsjError = uni.MensajeError;

                            if (MsjError.StartsWith("-1"))
                            {
                                ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                                return View(tbUnidadMedida);
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }

                        }
                        catch (Exception Ex)
                        {
                            Ex.Message.ToString();
                            ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                            return View(tbUnidadMedida);
                        }
                    }
                    return View(tbUnidadMedida);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        //GET: /UnidadMedida/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
            if (tbUnidadMedida == null)
            {
                return HttpNotFound();
            }
            return View(tbUnidadMedida);
        }

        // POST: /UnidadMedida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
            db.tbUnidadMedida.Remove(tbUnidadMedida);
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
