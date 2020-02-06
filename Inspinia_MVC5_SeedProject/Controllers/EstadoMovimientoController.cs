using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class EstadoMovimientoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();

        // GET: /EstadoMovimiento/
        [SessionManager("EstadoMovimiento/Index")]
        public ActionResult Index()
        {
            return View(db.tbEstadoMovimiento.ToList());
        }

        // GET: /EstadoMovimiento/Details/5
        [SessionManager("EstadoMovimiento/Details")]
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbEstadoMovimiento tbEstadoMovimiento = db.tbEstadoMovimiento.Find(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbEstadoMovimiento.estm_UsuarioCrea).usu_NombreUsuario;
            if (tbEstadoMovimiento == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbEstadoMovimiento);
        }

        // GET: /EstadoMovimiento/Create
        [SessionManager("EstadoMovimiento/Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EstadoMovimiento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("EstadoMovimiento/Create")]
        public ActionResult Create([Bind(Include = "estm_Descripcion")] tbEstadoMovimiento tbEstadoMovimiento)
        {

            


                if (ModelState.IsValid)
                {

          
                try
                    {
                        IEnumerable<Object> List = null;
                        string Msj = "";
                        List = db.UDP_Inv_tbEstadoMovimiento_Insert(tbEstadoMovimiento.estm_Id, tbEstadoMovimiento.estm_Descripcion, Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Inv_tbEstadoMovimiento_Insert_Result EstadoMovimientos in List)
                            Msj = EstadoMovimientos.MensajeError;
                        if(Msj.StartsWith("-1"))
                        {
                            Function.InsertBitacoraErrores("EstadoMovimiento/Create", Msj, "Create");
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            return View(tbEstadoMovimiento);
                        }
                    if (Msj.StartsWith("-2"))
                    {
                        Function.InsertBitacoraErrores("EstadoMovimiento/Create", Msj, "Create");
                        ModelState.AddModelError("", "Ya existe un estado con el mismo nombre.");
                        return View(tbEstadoMovimiento);
                    }
                    else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception Ex)
                    {
                        Function.InsertBitacoraErrores("EstadoMovimiento/Create", Ex.Message.ToString(), "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbEstadoMovimiento);
                    }
                }
                return View(tbEstadoMovimiento);
        }
        // GET: /EstadoMovimiento/Edit/5
        [SessionManager("EstadoMovimiento/Edit")]
        public ActionResult Edit(byte? id)
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
            tbEstadoMovimiento tbEstadoMovimiento = db.tbEstadoMovimiento.Find(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbEstadoMovimiento.estm_UsuarioCrea).usu_NombreUsuario;
            var UsuarioModfica = tbEstadoMovimiento.estm_UsuarioModifica;
            if (UsuarioModfica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModfica).usu_NombreUsuario;
            };
            if (tbEstadoMovimiento == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbEstadoMovimiento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("EstadoMovimiento/Edit")]
        public ActionResult Edit(byte? id, [Bind(Include = "estm_Id,estm_Descripcion,estm_UsuarioCrea,estm_FechaCrea")] tbEstadoMovimiento tbEstadoMovimiento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tbEstadoMovimiento VtbEstadoMovimiento = db.tbEstadoMovimiento.Find(id);
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Inv_tbEstadoMovimiento_Update(tbEstadoMovimiento.estm_Id, tbEstadoMovimiento.estm_Descripcion, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Inv_tbEstadoMovimiento_Update_Result EstadoMovimiento in List)
                        Msj = EstadoMovimiento.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("EstadoMovimiento/Edit", Msj, "Edit");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbEstadoMovimiento);
                    }
                    if (Msj.StartsWith("-2"))
                    {
                        Function.InsertBitacoraErrores("EstadoMovimiento/Create", Msj, "Create");
                        ModelState.AddModelError("", "Ya existe un estado con el mismo nombre.");
                        return View(tbEstadoMovimiento);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("EstadoMovimiento/Edit", Ex.Message.ToString(), "Edit");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return View(tbEstadoMovimiento);
                }
            }
            return View(tbEstadoMovimiento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [SessionManager("EstadoMovimiento/Delete")]
        public ActionResult Eliminar(byte id)
        {
            tbEstadoMovimiento obj = db.tbEstadoMovimiento.Find(id);
            try
            {
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbEstadoMovimiento_Delete(id);
                foreach (UDP_Inv_tbEstadoMovimiento_Delete_Result Estm in list)
                    MsjError = Estm.MensajeError;

                if (MsjError.StartsWith("-1The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    TempData["smserror"] = "No se pudo eliminar el registro porque posee dependencias, favor contacte al administrador.";
                    ViewBag.smserror = TempData["smserror"];
                    ModelState.AddModelError("", "No se puede borrar el registro");
                    Function.InsertBitacoraErrores("EstadoMovimiento/Eliminar", "No se puede borrar el registro", "Eliminar");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                Function.InsertBitacoraErrores("EstadoMovimiento/Eliminar", Ex.Message.ToString(), "Eliminar");
                ModelState.AddModelError("", "No se pudo eliminar el registro, favor contacte al administrador.");
                return View(obj);
            }
        }
    }
}
