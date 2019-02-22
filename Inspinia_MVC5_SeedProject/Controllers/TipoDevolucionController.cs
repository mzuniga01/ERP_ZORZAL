using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class TipoDevolucionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /TipoDevolucion/
        public ActionResult Index()
        {

            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("TipoDevolucion/Index"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }

                if (Function.GetUserRols("TipoDevolucion/Index"))
                {
                    //AQUI
                    var tbtipodevolucion = db.tbTipoDevolucion.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
                    return View(tbtipodevolucion.ToList());
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");

           
        }

        // GET: /TipoDevolucion/Details/5
        public ActionResult Details(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("TipoDevolucion/Details"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }

                if (Function.GetUserRols("TipoDevolucion/Details"))
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    tbTipoDevolucion tbTipoDevolucion = db.tbTipoDevolucion.Find(id);
                    if (tbTipoDevolucion == null)
                    {
                        return RedirectToAction("NotFound", "Login");
                    }
                    return View(tbTipoDevolucion);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");

           
        }

        // GET: /TipoDevolucion/Create
        public ActionResult Create()
        {

            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("TipoDevolucion/Create"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }

                if (Function.GetUserRols("TipoDevolucion/Create"))
                {
                    ViewBag.tdev_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.tdev_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
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

        // POST: /TipoDevolucion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tdev_Id,tdev_Descripcion,tdev_UsuarioCrea,tdev_FechaCrea,tdev_UsuarioModifica,tdev_FechaModifica")] tbTipoDevolucion tbTipoDevolucion)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("TipoDevolucion/Create"))
                {
                    if (db.tbTipoDevolucion.Any(a => a.tdev_Descripcion == tbTipoDevolucion.tdev_Descripcion))
                    {
                        ModelState.AddModelError("", "Ya existe este tipo Devolucion, Favor registrar otra");
                    }
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            IEnumerable<object> list = null;
                            var MsjError = "";
                            list = db.UDP_Inv_tbTipoDevolucion_Insert(tbTipoDevolucion.tdev_Descripcion, Function.GetUser(), DateTime.Now);
                            foreach (UDP_Inv_tbTipoDevolucion_Insert_Result obejto in list)
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
                            return View(tbTipoDevolucion);
                        }
                        return RedirectToAction("Index");
                    }
                    return View(tbTipoDevolucion);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /TipoDevolucion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Function.GetUserLogin())
            {

                if (Function.Sesiones("TipoDevolucion/Edit"))
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
                    tbTipoDevolucion tbTipoDevolucion = db.tbTipoDevolucion.Find(id);
                    if (tbTipoDevolucion == null)
                    {
                        return RedirectToAction("NotFound", "Login");
                    }
                    ViewBag.tdev_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDevolucion.tdev_UsuarioCrea);
                    ViewBag.tdev_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDevolucion.tdev_UsuarioModifica);
                    return View(tbTipoDevolucion);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
           
        }

        // POST: /TipoDevolucion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include="tdev_Id,tdev_Descripcion,tdev_UsuarioCrea,tdev_FechaCrea,tdev_UsuarioModifica,tdev_FechaModifica")] tbTipoDevolucion tbTipoDevolucion)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("TipoDevolucion/Edit"))
                {
                    if (db.tbTipoDevolucion.Any(a => a.tdev_Descripcion == tbTipoDevolucion.tdev_Descripcion && a.tdev_Id != tbTipoDevolucion.tdev_Id))
                    {
                        ModelState.AddModelError("", "Ya existe una pantalla con el mismo nombre");
                    }
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            tbTipoDevolucion obj = db.tbTipoDevolucion.Find(id);
                            IEnumerable<object> list = null;
                            var MsjError = "";
                            list = db.UDP_Inv_tbTipoDevolucion_Update(tbTipoDevolucion.tdev_Id,
                                                                 tbTipoDevolucion.tdev_Descripcion
                                                                 
                                                                 , tbTipoDevolucion.tdev_UsuarioCrea
                                                                 , tbTipoDevolucion.tdev_FechaCrea
                                                                , Function.GetUser()
                                                                , DateTime.Now);
                            foreach (UDP_Inv_tbTipoDevolucion_Update_Result obje in list)
                                MsjError = obje.MensajeError;

                            if (MsjError.StartsWith("-1"))
                            {
                                ModelState.AddModelError("", "No se actualizó el registro");
                                ViewBag.tdev_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDevolucion.tdev_UsuarioCrea);
                                ViewBag.tdev_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDevolucion.tdev_UsuarioModifica);
                                return View(tbTipoDevolucion);
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
                            ViewBag.tdev_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDevolucion.tdev_UsuarioCrea);
                            ViewBag.tdev_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDevolucion.tdev_UsuarioModifica);
                            return View(tbTipoDevolucion);
                        }

                    }
                    ViewBag.tdev_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDevolucion.tdev_UsuarioCrea);
                    ViewBag.tdev_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDevolucion.tdev_UsuarioModifica);
                    return View(tbTipoDevolucion);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");

          
        }

        // GET: /TipoDevolucion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoDevolucion tbTipoDevolucion = db.tbTipoDevolucion.Find(id);
            if (tbTipoDevolucion == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbTipoDevolucion);
        }

        // POST: /TipoDevolucion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoDevolucion tbTipoDevolucion = db.tbTipoDevolucion.Find(id);
            db.tbTipoDevolucion.Remove(tbTipoDevolucion);
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
