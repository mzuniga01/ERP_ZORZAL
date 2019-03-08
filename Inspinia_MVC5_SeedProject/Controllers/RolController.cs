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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using ERP_GMEDINA.Dataset;

namespace ERP_GMEDINA.Controllers
{
    public class RolController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /Rol/
        [SessionManager("Rol/Index")]
        public ActionResult Index()
        {
            return View(db.tbRol.ToList());
        }
        public ActionResult _IndexAccesoRol()
        {
            return View();
        }
        // GET: /Rol/Details/5
        [SessionManager("Rol/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbRol tbRol = db.tbRol.Find(id);
            if (tbRol == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbRol);
        }
        // GET: /Rol/Create
        [SessionManager("Rol/Create")]
        public ActionResult Create()
        {
            ViewBag.obj_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla");
            return View();
        }

        // GET: /Rol/Edit/5
        [SessionManager("Rol/Edit")]
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
            ViewBag.obj_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla");
            tbRol tbRol = db.tbRol.Find(id);
            if (tbRol == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbRol);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult EstadoRolInactivo(int id)
        {
            try
            {
                IEnumerable<Object> List = null;
                var Msj = "";
                tbRol tbRol = db.tbRol.Find(id);
                List = db.UDP_Acce_tbRolEstado_Update(id, Helpers.RolInactivo, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Acce_tbRolEstado_Update_Result Rol in List)
                    Msj = Rol.MensajeError;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el Estado , Contacte al Administrador");
            }
            return RedirectToAction("Edit/" + id);
        }
        public ActionResult EstadoRolActivo(int id)
        {
            try
            {
                IEnumerable<Object> List = null;
                var Msj = "";
                tbRol tbRol = db.tbRol.Find(id);
                List = db.UDP_Acce_tbRolEstado_Update(id, Helpers.RolActivo, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Acce_tbRolEstado_Update_Result Rol in List)
                    Msj = Rol.MensajeError;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el Estado , Contacte al Administrador");
            }
            return RedirectToAction("Edit/" + id);
        }
        public ActionResult Inactivar(int id)
        {
            try
            {
                IEnumerable<Object> List = null;
                var Msj = "";
                tbRol tbRol = db.tbRol.Find(id);
                List = db.UDP_Acce_tbRol_Inactivar(id, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Acce_tbRol_Inactivar_Result Rol in List)
                    Msj = Rol.MensajeError;
                if (Msj.StartsWith("-1"))
                {
                    TempData["smserror"] = " No se puede inactivar el rol porque ya hay usuarios asignados con este rol.";
                    ViewBag.smserror = TempData["smserror"];

                    ModelState.AddModelError("", "No se puede inactivar el rol");
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el Estado , Contacte al Administrador");
            }
            return RedirectToAction("Edit/" + id);
        }
        [HttpPost]
        public JsonResult AgregarObjeto(int idRol, ICollection<tbAccesoRol> RolAcceso)
        {
            var Msj = "";
            IEnumerable<Object> Acceso = null;
            using (TransactionScope Tran = new TransactionScope())
            {
                try
                {
                    if (RolAcceso != null)
                    {
                        if (RolAcceso.Count > 0)
                        {
                            foreach (tbAccesoRol vAccesoRol in RolAcceso)
                            {
                                Acceso = db.UDP_Acce_tbAccesoRol_Insert(idRol, vAccesoRol.obj_Id, Function.GetUser(), Function.DatetimeNow());
                                foreach (UDP_Acce_tbAccesoRol_Insert_Result item in Acceso)
                                {
                                    Msj = Convert.ToString(item.MensajeError);
                                }
                            }
                            var Listado = db.SDP_Acce_GetUserRols(Function.GetUser(), "").ToList();
                            Session["UserLoginRols"] = Listado;
                        }
                    }
                    Tran.Complete();
                }
                catch (Exception)
                {
                    Msj = "-1";
                }
                return Json(Msj, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [SessionManager("Rol/Create")]
        public JsonResult InsertRol(string DescripcionRol, ICollection<tbAccesoRol> AccesoRol)
        {
            int idUser = 0;
            GeneralFunctions Login = new GeneralFunctions();
            List<tbUsuario> User = Login.getUserInformation();
            foreach (tbUsuario Usuario in User)
            {
                idUser = Convert.ToInt32(Usuario.usu_Id);
            }
            IEnumerable<Object> Rol = null;
            IEnumerable<Object> RolAcceso = null;
            int idRol = 0;
            string Msj1 = "-1";
            if (db.tbRol.Any(a => a.rol_Descripcion == DescripcionRol))
            {
                ModelState.AddModelError("", "Ya existe un rol con el mismo nombre");
                Msj1 = "-2";
            }
            else
            {
                using (TransactionScope Tran = new TransactionScope())
                {
                    try
                    {
                        if (DescripcionRol != "")
                        {
                            Rol = db.UDP_Acce_tbRol_Insert(DescripcionRol, Helpers.RolActivo, Function.GetUser(), Function.DatetimeNow());
                            foreach (UDP_Acce_tbRol_Insert_Result vRol in Rol)
                                Msj1 = vRol.MensajeError;
                            if (!Msj1.StartsWith("-1"))
                            {
                                if (AccesoRol != null)
                                {
                                    if (AccesoRol.Count > 0)
                                    {
                                        idRol = Convert.ToInt32(Msj1);
                                        foreach (tbAccesoRol vAccesoRol in AccesoRol)
                                        {
                                            RolAcceso = db.UDP_Acce_tbAccesoRol_Insert(idRol, vAccesoRol.obj_Id, Function.GetUser(), Function.DatetimeNow());
                                            foreach (UDP_Acce_tbAccesoRol_Insert_Result item in RolAcceso)
                                            {
                                                Msj1 = Convert.ToString(item.MensajeError);
                                                if (Msj1.StartsWith("-1"))
                                                {
                                                    Function.InsertBitacoraErrores("Rol/Create", Msj1, "Create");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Function.InsertBitacoraErrores("Rol/Create", Msj1, "Create");
                                //Msj1 = "-1";
                            }
                            Tran.Complete();
                        }
                    }
                    catch (Exception Ex)
                    {
                        Function.InsertBitacoraErrores("Rol/Create", Ex.Message.ToString(), "Create");
                        Msj1 = "-1";
                    }
                }
            }
            return Json(Msj1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetObjetosDisponibles(int rolId)
        {
            var list = db.SDP_Acce_GetObjetosDisponibles(rolId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetObjetosAsignados(int rolId)
        {
            var list = db.SDP_Acce_GetObjetosAsignados(rolId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetObjetos()
        {
            var list = db.SDP_Acce_GetObjetos().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [SessionManager("Rol/Edit")]
        public JsonResult UpdateRol(int rolId, string Descripcion)
        {
            string Msj = "-1";
            IEnumerable<Object> Rol = null;
            if (db.tbRol.Any(a => a.rol_Descripcion == Descripcion && a.rol_Id != rolId))
            {
                ModelState.AddModelError("", "Ya existe un rol con el mismo nombre");
                Msj = "-2";
            }
            else
            {
                try
                {
                    if (Descripcion != null)
                    {
                        Rol = db.UDP_Acce_tbRol_Update(rolId, Descripcion, Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Acce_tbRol_Update_Result item in Rol)
                        {
                            Msj = Convert.ToString(item.MensajeError);
                            if (Msj.StartsWith("-1"))
                            {
                                Function.InsertBitacoraErrores("Rol/Edit", Msj, "Edit");
                            }
                            else
                                Msj = "1";
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("Rol/Edit", Ex.Message.ToString(), "Edit");
                    Msj = "-1";
                }
            }
            return Json(Msj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult QuitarObjeto(int idRol, ICollection<tbAccesoRol> RolAcceso)
        {
            var Msj = "";
            using (TransactionScope Tran = new TransactionScope())
            {
                try
                {
                    if (RolAcceso != null)
                    {
                        if (RolAcceso.Count > 0)
                        {
                            foreach (tbAccesoRol vAccesoRol in RolAcceso)
                            {
                               db.UDP_Acce_tbAccesoRol_Delete(idRol, vAccesoRol.obj_Id);
                                
                            }
                        }
                        var Listado = db.SDP_Acce_GetUserRols(Function.GetUser(), "").ToList();
                        Session["UserLoginRols"] = Listado;
                    }
                    Tran.Complete();
                }
                catch (Exception)
                {
                    Msj = "-1";
                }
                return Json(Msj, JsonRequestBehavior.AllowGet);

            }
        }
        //public ActionResult ExportReport()
        //{
        //    ProductoSolicitadoPorEntregar ds = new ProductoSolicitadoPorEntregar();
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ProductoSolicitadosPorEntregar.rpt"));
        //    rd.SetDataSource(ds.UDV_Inv_ProductosSolicitadosPorEntregar.ToList());
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    try
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        string fileName = "ProductoSolicitadosPorEntregar.pdf";
        //        Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
        //        return File(stream, "application/pdf");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}
