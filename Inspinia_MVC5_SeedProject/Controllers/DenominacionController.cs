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
    public class DenominacionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /Denominacion/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Denominacion/Index"))
                {
                    var tbdenominacion = db.tbDenominacion.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbMoneda);
                    return View(tbdenominacion.ToList());
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
        // GET: /Denominacion/Details/5
        public ActionResult Details(short? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Denominacion/Details"))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    tbDenominacion tbDenominacion = db.tbDenominacion.Find(id);
                    if (tbDenominacion == null)
                    {
                        return HttpNotFound();
                    }
                    return View(tbDenominacion);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /Denominacion/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Denominacion/Create"))
                {
                    tbDenominacion TipoDenominacion = new tbDenominacion();
                    TipoDenominacion.DenominacionList = cUtilities.DenominacionList();

                    ViewBag.deno_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.deno_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
                    return View(TipoDenominacion);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // POST: /Denominacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "deno_Id,deno_Descripcion,deno_Tipo,deno_valor,mnda_Id,deno_UsuarioCrea,deno_FechaCrea,deno_UsuarioModifica,deno_FechaModifica")] tbDenominacion tbDenominacion)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Denominacion/Create"))
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            string MensajeError = "";
                            IEnumerable<object> list = null;
                            list = db.UDP_Gral_tbDenominacion_Insert(tbDenominacion.deno_Descripcion, tbDenominacion.deno_Tipo, tbDenominacion.deno_valor, tbDenominacion.mnda_Id, Function.GetUser(), Function.DatetimeNow());
                            foreach (UDP_Gral_tbDenominacion_Insert_Result Denominacion in list)
                                MensajeError = Denominacion.MensajeError;
                            if (MensajeError == "-1")
                            {
                                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                return View(tbDenominacion);
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        catch (Exception Ex)
                        {
                            Ex.Message.ToString();
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            return View(tbDenominacion);
                        }
                        //db.tbDenominacion.Add(tbDenominacion);
                        //db.SaveChanges();
                        //return RedirectToAction("Index");




                    }
                    ViewBag.deno_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacion.deno_UsuarioCrea);
                    ViewBag.deno_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacion.deno_UsuarioModifica);
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);

                    tbDenominacion TipoDenominacion = new tbDenominacion();
                    TipoDenominacion.DenominacionList = cUtilities.DenominacionList();
                    return View(tbDenominacion);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }


        // GET: /Denominacion/Edit/5
        public ActionResult Edit(short? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Denominacion/Edit"))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    tbDenominacion tbDenominacion = db.tbDenominacion.Find(id);
                    if (tbDenominacion == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.deno_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacion.deno_UsuarioCrea);
                    ViewBag.deno_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacion.deno_UsuarioModifica);
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);

                    tbDenominacion TipoDenominacion = db.tbDenominacion.Find(id);
                    TipoDenominacion.DenominacionList = cUtilities.DenominacionList();
                    return View(tbDenominacion);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // POST: /Denominacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "deno_Id,deno_Descripcion,deno_Tipo,deno_valor,mnda_Id,deno_UsuarioCrea,deno_FechaCrea,deno_UsuarioModifica,deno_FechaModifica ,tbUsuario, tbUsuario1")] tbDenominacion tbDenominacion)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("Denominacion/Edit"))
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            string MensajeError = "";
                            IEnumerable<object> list = null;
                            list = db.UDP_Gral_tbDenominacion_Update(tbDenominacion.deno_Id,
                                                                    tbDenominacion.deno_Descripcion,
                                                                    tbDenominacion.deno_Tipo,
                                                                    tbDenominacion.deno_valor,
                                                                    tbDenominacion.mnda_Id,
                                                                    tbDenominacion.deno_UsuarioCrea,
                                                                    tbDenominacion.deno_FechaCrea,
                                                                    Function.GetUser(),
                                                                    Function.DatetimeNow());
                            foreach (UDP_Gral_tbDenominacion_Update_Result Denominacion in list)
                                MensajeError = Denominacion.MensajeError;
                            if (MensajeError == "-1")
                            {
                                ModelState.AddModelError("", "No se ha podido actualizar el registro, favor contacte al administrador.");
                                return View(tbDenominacion);
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }

                            //db.Entry(tbDenominacion).State = EntityState.Modified;
                            //db.SaveChanges();
                            //return RedirectToAction("Index");
                        }
                        catch (Exception Ex)
                        {
                            Ex.Message.ToString();
                            ModelState.AddModelError("", "No se ha podido actualizar el registro, favor contacte al administrador.");
                            return View(tbDenominacion);
                        }
                    }
                    ViewBag.deno_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacion.deno_UsuarioCrea);
                    ViewBag.deno_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacion.deno_UsuarioModifica);
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbDenominacion.mnda_Id);

                    tbDenominacion TipoDenominacion = new tbDenominacion();
                    TipoDenominacion.DenominacionList = cUtilities.DenominacionList();
                    return View(tbDenominacion);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
        // GET: /Denominacion/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDenominacion tbDenominacion = db.tbDenominacion.Find(id);
            if (tbDenominacion == null)
            {
                return HttpNotFound();
            }
            return View(tbDenominacion);
        }

        // POST: /Denominacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbDenominacion tbDenominacion = db.tbDenominacion.Find(id);
            db.tbDenominacion.Remove(tbDenominacion);
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
