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
    public class ObjetoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /Objeto/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("Objeto/Index"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }
                if (Function.GetUserRols("Objeto/Index"))
                {
                    return View(db.tbObjeto.ToList());
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /Objeto/Details/5
        public ActionResult Details(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("Objeto/Details"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }
                if (Function.GetUserRols("Objeto/Details"))
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    tbObjeto tbObjeto = db.tbObjeto.Find(id);
                    if (tbObjeto == null)
                    {
                        return RedirectToAction("NotFound", "Login");
                    }
                    return View(tbObjeto);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /Objeto/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("Objeto/Create"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }
                if (Function.GetUserRols("Objeto/Create"))
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

        // POST: /Objeto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "obj_Id,obj_Pantalla,obj_Referencia,obj_UsuarioCrea,obj_FechaCrea,obj_UsuarioModifica,obj_FechaModifica,obj_Estado")] tbObjeto tbObjeto)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Objeto/Create"))
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            IEnumerable<object> list = null;
                            var MsjError = "";
                            list = db.UDP_Acce_tbObjeto_Insert(tbObjeto.obj_Pantalla, tbObjeto.obj_Referencia, Function.GetUser(), DateTime.Now);
                            foreach (UDP_Acce_tbObjeto_Insert_Result obejto in list)
                                MsjError = obejto.MensajeError;
                            if (MsjError.StartsWith("-1"))
                            {
                                ModelState.AddModelError("", "No se guardó el registro");
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        catch (Exception Ex)
                        {
                            Ex.Message.ToString();
                            ModelState.AddModelError("", "No se guardó el registro");
                            return View(tbObjeto);
                        }
                        return RedirectToAction("Index");
                    }
                    return View(tbObjeto);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }


        // GET: /Objeto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("Objeto/Edit"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }
                if (Function.GetUserRols("Objeto/Edit"))
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    tbObjeto tbObjeto = db.tbObjeto.Find(id);
                    if (tbObjeto == null)
                    {
                        return RedirectToAction("NotFound", "Login");
                    }
                    ViewBag.obj_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioModifica);
                    ViewBag.obj_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioCrea);
                    return View(tbObjeto);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // POST: /Objeto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id,[Bind(Include="obj_Id, obj_Pantalla,obj_Referencia,obj_UsuarioCrea,obj_FechaCrea,obj_UsuarioModifica,obj_FechaModifica,obj_Estado")] tbObjeto tbObjeto)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Objeto/Edit"))
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            tbObjeto obj = db.tbObjeto.Find(id);
                            IEnumerable<object> list = null;
                            var MsjError = "";
                            list = db.UDP_Acce_tbObjeto_Update(tbObjeto.obj_Id,
                                                                 tbObjeto.obj_Pantalla,
                                                                 tbObjeto.obj_Referencia
                                                                 ,tbObjeto.obj_UsuarioCrea
                                                                 ,tbObjeto.obj_FechaCrea
                                                                ,Function.GetUser()
                                                                ,DateTime.Now);
                            foreach (UDP_Acce_tbObjeto_Update_Result obje in list)
                                MsjError = obje.MensajeError;

                            if (MsjError.StartsWith("-1"))
                            {
                                ModelState.AddModelError("", "No se actualizó el registro");
                                ViewBag.obj_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioModifica);
                                ViewBag.obj_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioCrea);
                                return View(tbObjeto);
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        catch (Exception Ex)
                        {
                            Ex.Message.ToString();
                            ModelState.AddModelError("", "No se actualizó el registro");
                            ViewBag.obj_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioModifica);
                            ViewBag.obj_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioCrea);
                            return View(tbObjeto);
                        }

                    }
                    ViewBag.obj_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioModifica);
                    ViewBag.obj_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbObjeto.obj_UsuarioCrea);
                    return View(tbObjeto);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
        //para que cambie estado a activar
        public ActionResult EstadoInactivar(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Objeto/Edit"))
                {
                    try
                    {
                        tbObjeto obj = db.tbObjeto.Find(id);
                        IEnumerable<object> list = null;
                        var MsjError = "";
                        list = db.UDP_Acce_tbObjeto_Update_Estado(id, Helpers.ObjetoInactivo, Function.GetUser(), DateTime.Now);
                        foreach (UDP_Acce_tbObjeto_Update_Estado_Result obje in list)
                            MsjError = obje.MensajeError;

                        if (MsjError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "No se actualizó el registro");
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
                        ModelState.AddModelError("", "No se actualizó el registro");
                        return RedirectToAction("Edit/" + id);
                    }
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
        //para que cambie estado a inactivar
        public ActionResult Estadoactivar(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Objeto/Edit"))
                {
                    try
                    {
                        tbObjeto obj = db.tbObjeto.Find(id);
                        IEnumerable<object> list = null;
                        var MsjError = "";
                        list = db.UDP_Acce_tbObjeto_Update_Estado(id, Helpers.ObjetoActivo, Function.GetUser(), DateTime.Now);
                        foreach (UDP_Acce_tbObjeto_Update_Estado_Result obje in list)
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
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }


        // GET: /Objeto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbObjeto tbObjeto = db.tbObjeto.Find(id);
            if (tbObjeto == null)
            {
                return HttpNotFound();
            }
            return View(tbObjeto);
        }

        // POST: /Objeto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbObjeto tbObjeto = db.tbObjeto.Find(id);
            db.tbObjeto.Remove(tbObjeto);
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
