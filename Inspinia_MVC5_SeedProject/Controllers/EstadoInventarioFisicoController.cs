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
    public class EstadoInventarioFisicoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: EstadoInventarioFisico
        [SessionManager("EstadoInventarioFisico/Index")]
        public ActionResult Index()
        {
            return View(db.tbEstadoInventarioFisico.ToList());
        }

        // GET: /EstadoInventarioFisico/Details/5
        [SessionManager("EstadoInventarioFisico/Details")]
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbEstadoInventarioFisico tbestadoinventariofisico = db.tbEstadoInventarioFisico.Find(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbestadoinventariofisico.estif_UsuarioCrea).usu_NombreUsuario;
            if (tbestadoinventariofisico == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbestadoinventariofisico);
        }

        // GET: /EstadoInventarioFisico/Create
        [SessionManager("EstadoInventarioFisico/Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EstadoInventarioFisico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("EstadoInventarioFisico/Create")]
        public ActionResult Create([Bind(Include = "estif_Descripcion")] tbEstadoInventarioFisico tbestadoinventarioFisico)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Inv_tbEstadoInventarioFisico_Insert(tbestadoinventarioFisico.estif_Id, tbestadoinventarioFisico.estif_Descripcion, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Inv_tbEstadoInventarioFisico_Insert_Result EstadoInventarioFisico in List)
                        Msj = EstadoInventarioFisico.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("EstadoInventarioFisico/Create", Msj, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbestadoinventarioFisico);
                    }
                    if (Msj.StartsWith("-2"))
                    {
                        Function.InsertBitacoraErrores("EstadoInventarioFisico/Create", Msj, "Create");
                        ModelState.AddModelError("", "Ya existe un estado con el mismo nombre.");
                        return View(tbestadoinventarioFisico);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("EstadoInventarioFisico/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbestadoinventarioFisico);
                }
            }
            return View(tbestadoinventarioFisico);
        }

        // GET: /EstadoMovimiento/Edit/5
        [SessionManager("EstadoInventarioFisico/Edit")]
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
            tbEstadoInventarioFisico tbestadoinventariofisico = db.tbEstadoInventarioFisico.Find(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbestadoinventariofisico.estif_UsuarioCrea).usu_NombreUsuario;
            var UsuarioModfica = tbestadoinventariofisico.estif_UsuarioModifica;
            if (UsuarioModfica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModfica).usu_NombreUsuario;
            };
            if (tbestadoinventariofisico == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbestadoinventariofisico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("EstadoInventarioFisico/Edit")]
        public ActionResult Edit(byte? id, [Bind(Include = "estif_Id,estif_Descripcion,estif_UsuarioCrea,estif_FechaCrea")] tbEstadoInventarioFisico tbestadoinventariofisico)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tbEstadoInventarioFisico Edittbestadoinventariofisico = db.tbEstadoInventarioFisico.Find(id);
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Inv_tbEstadoInventarioFisico_Update(tbestadoinventariofisico.estif_Id, tbestadoinventariofisico.estif_Descripcion, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Inv_tbEstadoInventarioFisico_Update_Result estadoinventariofisico in List)
                        Msj = estadoinventariofisico.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("EstadoInventarioFisico/Edit", Msj, "Edit");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbestadoinventariofisico);
                    }
                    if (Msj.StartsWith("-2"))
                    {
                        Function.InsertBitacoraErrores("EstadoInventarioFisico/Create", Msj, "Create");
                        ModelState.AddModelError("", "Ya existe un estado con el mismo nombre.");
                        return View(tbestadoinventariofisico);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("EstadoInventarioFisico/Edit", Ex.Message.ToString(), "Edit");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return View(tbestadoinventariofisico);
                }
            }
            return View(tbestadoinventariofisico);
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