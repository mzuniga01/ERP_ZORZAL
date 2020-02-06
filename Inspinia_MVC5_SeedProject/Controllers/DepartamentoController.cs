using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Transactions;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class DepartamentoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        // GET: /Departamento/
        [SessionManager("Departamento/Index")]
        public ActionResult Index()
        {
            try
            {
                return View(db.tbDepartamento.ToList());
            }
            catch (Exception Ex)
            {
                Function.InsertBitacoraErrores("Departamento/Index", Ex.Message.ToString(), "Index");
                ModelState.AddModelError("", "Conexión fállida, intente de nuevo");
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult _IndexMunicipio_Botones()
        {
            return View();
        }
        // GET: /Departamento/Details/5
        [SessionManager("Departamento/Details")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbDepartamento);
        }
        // GET: /Departamento/Create
        [SessionManager("Departamento/Create")]
        public ActionResult Create()
        {
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", "Seleccione");
            ViewBag.dep_Codigo = new SelectList(db.tbMunicipio, "dep_Codigo", "dep_Nombre", "Seleccione");
            Session["tbMunicipio"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Departamento/Create")]
        public ActionResult Create([Bind(Include = "dep_Codigo,dep_Nombre,dep_UsuarioCrea,dep_FechaCrea,dep_UsuarioModifica,dep_FechaModifica")] tbDepartamento tbDepartamento)
        {
            IEnumerable<object> list = null;
            IEnumerable<object> lista = null;
            string MensajeError = "";
            string MsjError = "";
            var listMunicipios = (List<tbMunicipio>)Session["tbMunicipio"];

            if (db.tbDepartamento.Any(a => a.dep_Nombre == tbDepartamento.dep_Nombre))
            {
                ModelState.AddModelError("", "Ya existe un Departamento con ese Nombre, agregue otro.");
            }
            if (ModelState.IsValid)
            {
                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {
                        list = db.UDP_Gral_tbDepartamento_Insert(tbDepartamento.dep_Codigo, tbDepartamento.dep_Nombre, Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Gral_tbDepartamento_Insert_Result departamento in list)
                            MsjError = departamento.MensajeError;
                        if (MsjError.StartsWith("-1"))
                        {
                            Function.InsertBitacoraErrores("Departamento/Create", MsjError, "Create");
                            ModelState.AddModelError("", "Ya existe un Departamento con ese Código, agregue otro.");
                            return View(tbDepartamento);
                        }
                        else
                        {
                            if (listMunicipios != null)
                            {
                                if (listMunicipios.Count > 0)
                                {
                                    foreach (tbMunicipio mun in listMunicipios)
                                    {
                                        lista = db.UDP_Gral_tbMunicipio_Insert(mun.mun_Codigo, tbDepartamento.dep_Codigo, mun.mun_Nombre, Function.GetUser(), Function.DatetimeNow());
                                        foreach (UDP_Gral_tbMunicipio_Insert_Result municipios in lista)
                                            MensajeError = municipios.MensajeError;
                                        if (MensajeError.StartsWith("-1"))
                                        {
                                            Function.InsertBitacoraErrores("Departamento/Create", MensajeError, "Create");
                                            ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                            return View(tbDepartamento);
                                        }
                                    }
                                }
                            }
                            _Tran.Complete();
                        }
                    }
                    catch (Exception Ex)
                    {
                        Function.InsertBitacoraErrores("Departamento/Create", Ex.Message.ToString(), "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbDepartamento);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(tbDepartamento);
        }

        // GET: /Departamento/Edit/5
        [SessionManager("Departamento/Edit")]
        public ActionResult Edit(string id)
        {
            Session["tbMunicipio"] = null;
            try
            {
                ViewBag.smserror = TempData["smserror"].ToString();
            }
            catch { }

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbDepartamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Departamento/Edit")]
        public ActionResult Edit(string id, [Bind(Include = "dep_Codigo,dep_Nombre,dep_UsuarioCrea,dep_FechaCrea,dep_UsuarioModifica,dep_FechaModifica")] tbDepartamento tbDepartamento)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<object> list = null;
                string MsjError = "";
                {
                    try
                    {
                        list = db.UDP_Gral_tbDepartamento_Update(tbDepartamento.dep_Codigo, tbDepartamento.dep_Nombre, tbDepartamento.dep_UsuarioCrea, tbDepartamento.dep_FechaCrea, Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Gral_tbDepartamento_Update_Result dep in list)
                            MsjError = dep.MensajeError;
                        if (MsjError.StartsWith("-1"))
                        {
                            Function.InsertBitacoraErrores("Departamento/Edit", MsjError, "Edit");
                            ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                            return View(tbDepartamento);
                        }
                        else
                        {
                            return RedirectToAction("Edit/" + tbDepartamento.dep_Codigo);
                        }
                    }
                    catch (Exception Ex)
                    {
                        Function.InsertBitacoraErrores("Departamento/Create", Ex.Message.ToString(), "Create");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbDepartamento);
                    }
                }
            }
            return View(tbDepartamento);
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
        public JsonResult AnadirMunicipio(tbMunicipio Municipio)
        {
            List<tbMunicipio> sessionMunicipio = new List<tbMunicipio>();
            var list = (List<tbMunicipio>)Session["tbMunicipio"];
            if (list == null)
            {
                sessionMunicipio.Add(Municipio);
                Session["tbMunicipio"] = sessionMunicipio;
            }
            else
            {
                list.Add(Municipio);
                Session["tbMunicipio"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult RemoveMunicipios(tbMunicipio Municipios)
        {
            var list = (List<tbMunicipio>)Session["tbMunicipio"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.mun_UsuarioCrea == Municipios.mun_UsuarioCrea);
                list.Remove(itemToRemove);
                Session["tbMunicipio"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ActualizarMunicipio(tbMunicipio Municipio)
        {
            string MsjError = "";
            string depCodigo = Municipio.mun_Codigo.Substring(0, 2);
            try
            {
                IEnumerable<object> list = null;
                tbMunicipio munl = db.tbMunicipio.Find(Municipio.mun_Codigo);
                list = db.UDP_Gral_tbMunicipio_Update(Municipio.mun_Codigo, 
                    depCodigo,
                    Municipio.mun_Nombre,
                    munl.mun_UsuarioCrea,
                    munl.mun_FechaCrea,
                    Function.GetUser(),
                    Function.DatetimeNow());
                foreach (UDP_Gral_tbMunicipio_Update_Result mun in list)
                    MsjError = (mun.MensajeError);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
            }
            return Json(MsjError, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMun(tbMunicipio GuardarMunicipios)
        {
            {
                string MsjError = "";
                try
                {
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbMunicipio_Insert(GuardarMunicipios.mun_Codigo, GuardarMunicipios.dep_Codigo, GuardarMunicipios.mun_Nombre, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Gral_tbMunicipio_Insert_Result mun in list)
                        MsjError = (mun.MensajeError);

                    if (MsjError.Substring(0, 1) == "-1")
                    {
                        ModelState.AddModelError("", "No se Guardo el Registro");
                    }
                    else
                    {
                        return Json("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro");
                }
                return Json("Index");
            }
        }


        [HttpPost]
        public JsonResult GuardarMunicipioModal([Bind(Include = "dep_Codigo,dep_Nombre,dep_UsuarioCrea,dep_FechaCrea,dep_UsuarioModifica,dep_FechaModifica")] tbMunicipio tbMunicipio)
        {
            string Msj = "";
            try
            {
                IEnumerable<object> list = null;
                list = db.UDP_Gral_tbMunicipio_Insert(tbMunicipio.mun_Codigo, tbMunicipio.dep_Codigo, tbMunicipio.mun_Nombre, Function.GetUser(), Function.DatetimeNow());

                foreach (UDP_Gral_tbMunicipio_Insert_Result Municipio in list)
                    Msj = Municipio.MensajeError;

                if (Msj.Substring(0, 2) == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");


                }
                else
                {
                    return Json(Msj);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
            }
            return Json("Index");
        }


        public ActionResult EliminarMunicipio(string id, string dep_Codigo)
        {
            try
            {
                tbMunicipio obj = db.tbMunicipio.Find(id);
                IEnumerable<object> list = null;
                var MsjError = ""; list = db.UDP_Gral_tbMunicipio_Delete(id);
                foreach (UDP_Gral_tbMunicipio_Delete_Result mun in list)
                    MsjError = mun.MensajeError;
                if (MsjError.StartsWith("-1The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    TempData["smserror"] = "No se puede eliminar el registro porque posee dependencias, favor contacte al administrador.";
                    ViewBag.smserror = TempData["smserror"];
                    ModelState.AddModelError("", "No se puede borrar el registro");
                    return RedirectToAction("Edit/" + dep_Codigo);
                }
                else
                {
                    return RedirectToAction("Edit/" + dep_Codigo);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + dep_Codigo);
            }
        }

        [HttpPost]
        public JsonResult GuardarMuninicipio(string depCodigo)
        {
            string MsjError = "";
            var listMunicipios = (List<tbMunicipio>)Session["tbMunicipio"];
            IEnumerable<object> list = null;
            try
            {
                if(listMunicipios.Count>0 && listMunicipios != null)
                {
                    foreach (tbMunicipio mun in listMunicipios)
                    {
                        list = db.UDP_Gral_tbMunicipio_Insert(mun.mun_Codigo, depCodigo, mun.mun_Nombre, Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Gral_tbMunicipio_Insert_Result municipios in list)
                        {
                            MsjError = municipios.MensajeError;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MsjError = "-1";
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Guardo el registro");
            }
            return Json(MsjError, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult getMunicipio(string munCodigo)
        {
            IEnumerable<object> list = null;
            try
            {
                list = db.SDP_tbMunicipio_Select(munCodigo).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}

