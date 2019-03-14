using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Net.Mail;
using SimpleCrypto;
using System.Transactions;
using Microsoft.Owin.Security;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class UsuarioController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /Usuario/
        [SessionManager("Usuario/Index")]
        public ActionResult Index()
        {
            return View(db.tbUsuario.ToList());
        }

        [SessionManager("Usuario/ModificarPass")]
        public ActionResult ModificarPass(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            ViewBag.User_ID = id;
            ViewBag.ConfirmarPassword = "Password";
            if (tbUsuario == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbUsuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Usuario/ModificarPass")]
        public ActionResult ModificarPass([Bind(Include = "usu_Id,usu_NombreUsuario,usu_Nombres,usu_Apellidos,usu_Correo,ConfirmarPassword,suc_Id")] tbUsuario tbUsuario, string usu_Password, string txtPassword)
        {
            ModelState.Remove("usu_Password");
            if (ModelState.IsValid)
            {
                try
                {

                    IEnumerable<object> List = null;
                    var MsjError = "0";
                    var credenciales = db.UDP_Acce_Login(tbUsuario.usu_NombreUsuario, txtPassword).ToList();
                    if (credenciales.Count > 0)
                    {
                        List = db.UDP_Acce_tbUsuario_PasswordUpdate(tbUsuario.usu_Id, usu_Password);

                        foreach (UDP_Acce_tbUsuario_PasswordUpdate_Result Usuario in List)
                            MsjError = Usuario.MensajeError;

                        if (MsjError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                            return View(tbUsuario);
                        }
                        else
                        {
                            Session.Clear();
                            Session.Abandon();
                            Response.Buffer = true;
                            Response.ExpiresAbsolute = Function.DatetimeNow().AddDays(-1D);
                            Response.Expires = -1500;
                            Response.CacheControl = "no-cache";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            AuthenticationManager.SignOut();
                            Session["UserLogin"] = null;
                            Session["UserLoginRols"] = null;
                            Session["UserLoginEsAdmin"] = null;
                            return RedirectToAction("Index", "Login");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("usu_NombreUsuario", "Contraseña incorrecta");
                        return View(tbUsuario);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                    return View(tbUsuario);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(tbUsuario);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: /Usuario/Details/5
        [SessionManager("Usuario/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            if (tbUsuario == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbUsuario);
        }

        // GET: /Usuario/Create
        [SessionManager("Usuario/Create")]
        public ActionResult Create()
        {
            Session["tbRolesUsuario"] = null;
            ViewBag.Empleado = db.SDP_tbEmpleado_Select().ToList();
            ViewBag.Sucursal = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Usuario/Create")]
        public ActionResult Create([Bind(Include = "usu_NombreUsuario,usu_Nombres,usu_Apellidos,usu_Correo,ConfirmarPassword,suc_Id, emp_Id")] tbUsuario tbUsuario, string usu_Password)
        {
            if (db.tbUsuario.Any(a => a.usu_NombreUsuario == tbUsuario.usu_NombreUsuario))
            {
                ModelState.AddModelError("", "Ya existe un Usuario con ese nombre de usuario");
            }

            IEnumerable<object> List = null;
            IEnumerable<object> Roles = null;
            var listRoles = (List<tbRolesUsuario>)Session["tbRolesUsuario"];
            var MsjError = "0";
            var MsjErrorRoles = "0";
            ModelState.Remove("usu_Password");
            if (ModelState.IsValid)
            {
                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {
                        List = db.UDP_Acce_tbUsuario_Insert(tbUsuario.usu_NombreUsuario, usu_Password, tbUsuario.usu_Nombres, tbUsuario.usu_Apellidos, tbUsuario.usu_Correo, tbUsuario.suc_Id, tbUsuario.emp_Id);
                        foreach (UDP_Acce_tbUsuario_Insert_Result Usuario in List)
                            MsjError = Usuario.MensajeError;
                        if (MsjError.StartsWith("-1"))
                        {
                            ViewBag.Empleado = db.SDP_tbEmpleado_Select().ToList();
                            ViewBag.Sucursal = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
                            ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                            Session["tbRolesUsuario"] = null;
                            return View(tbUsuario);
                        }
                        else
                        {
                            if (listRoles != null)
                            {
                                if (listRoles.Count > 0)
                                {
                                    foreach (tbRolesUsuario URoles in listRoles)
                                    {
                                        Roles = db.UDP_Acce_tbRolesUsuario_Insert(Convert.ToInt32(MsjError), URoles.rol_Id, Function.GetUser()
                                                        , Function.DatetimeNow());
                                        foreach (UDP_Acce_tbRolesUsuario_Insert_Result Resultado in Roles)
                                            MsjErrorRoles = Resultado.MensajeError;
                                        if (MsjError.StartsWith("-1"))
                                        {
                                            ViewBag.Empleado = db.SDP_tbEmpleado_Select().ToList();
                                            ViewBag.Sucursal = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
                                            ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                                            Session["tbRolesUsuario"] = null;
                                            return View(tbUsuario);
                                        }
                                    }
                                }
                            }
                        }
                        _Tran.Complete();

                        return RedirectToAction("Index");
                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ViewBag.Empleado = db.SDP_tbEmpleado_Select().ToList();
                        ViewBag.Sucursal = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
                        ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                        Session["tbRolesUsuario"] = null;
                        return View(tbUsuario);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("ConfirmarPassword", "El campo Password es requerido");
                ModelState.AddModelError("usu_Password", "El campo Password es requerido");
                ViewBag.Empleado = db.SDP_tbEmpleado_Select().ToList();
                ViewBag.Sucursal = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
            }
            Session["tbRolesUsuario"] = null;
            return View(tbUsuario);
        }

        [SessionManager("Usuario/ModificarCuenta")]
        public ActionResult ModificarCuenta(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            ViewBag.User_ID = id;
            ViewBag.ConfirmarPassword = "Password";
            if (tbUsuario == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbUsuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Usuario/ModificarCuenta")]
        public ActionResult ModificarCuenta([Bind(Include = "usu_Id,usu_NombreUsuario,usu_Nombres,usu_Apellidos,usu_Correo,usu_EsActivo,usu_RazonInactivo,usu_EsAdministrador,usu_Password,ConfirmarPassword,suc_Id,emp_Id")] tbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usu_EsActivo = true;
                    IEnumerable<object> List = null;
                    var MsjError = "0";
                    List = db.UDP_Acce_tbUsuario_Update(tbUsuario.usu_Id, tbUsuario.usu_NombreUsuario, tbUsuario.usu_Nombres, tbUsuario.usu_Apellidos, tbUsuario.usu_Correo, usu_EsActivo, tbUsuario.usu_RazonInactivo, tbUsuario.usu_EsAdministrador,
                        tbUsuario.suc_Id, tbUsuario.emp_Id);
                    foreach (UDP_Acce_tbUsuario_Update_Result Usuario in List)
                        MsjError = Usuario.MensajeError;

                    if (MsjError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                        return View(tbUsuario);
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
                    return View(tbUsuario);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(tbUsuario);
        }

        // GET: /Usuario/Edit/5
        [SessionManager("Usuario/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            ViewBag.User_ID = id;
            ViewBag.ConfirmarPassword = "Password";
            ViewBag.Sucursal = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
            if (tbUsuario == null)
            {
                return RedirectToAction("NotFound", "Login");
            }

            ViewData["Razon"] = tbUsuario.usu_RazonInactivo;
            return View(tbUsuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Usuario/Edit")]
        public ActionResult Edit([Bind(Include = "usu_Id,usu_NombreUsuario,usu_Nombres,usu_Apellidos,usu_Correo,usu_EsActivo,usu_RazonInactivo,usu_EsAdministrador,usu_Password,ConfirmarPassword,suc_Id,emp_Id")] tbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usu_EsActivo = true;
                    IEnumerable<object> List = null;
                    var MsjError = "0";
                    List = db.UDP_Acce_tbUsuario_Update(tbUsuario.usu_Id, tbUsuario.usu_NombreUsuario, tbUsuario.usu_Nombres, tbUsuario.usu_Apellidos, tbUsuario.usu_Correo, usu_EsActivo, tbUsuario.usu_RazonInactivo, tbUsuario.usu_EsAdministrador,
                        tbUsuario.suc_Id, tbUsuario.emp_Id);
                    foreach (UDP_Acce_tbUsuario_Update_Result Usuario in List)
                        MsjError = Usuario.MensajeError;

                    if (MsjError.StartsWith("-1"))
                    {
                        ViewBag.ConfirmarPassword = "Password";
                        ViewBag.Sucursal = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
                        ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                        return View(tbUsuario);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }

                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ViewBag.ConfirmarPassword = "Password";
                    ViewBag.Sucursal = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
                    ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                    return View(tbUsuario);
                }
            }
            else
            {
                ViewBag.Sucursal = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(tbUsuario);
        }

        [SessionManager("Usuario/RestaurarPassword")]
        public ActionResult RestaurarPassword(int? id)
        {
            if (ModelState.IsValidField("usuario"))
            {
                tbUsuario tbUsuario = db.tbUsuario.Find(id);
                if (tbUsuario != null)
                {
                    string emailsalida = "erpzorzal@gmail.com";
                    string passwordsalida = "sistemadeinventari0";
                    string emaildestino = tbUsuario.usu_Correo;
                    string passwordnueva = RandomPassword.Generate(8, PasswordGroup.Uppercase, PasswordGroup.Lowercase, PasswordGroup.Numeric);
                    db.Entry(tbUsuario).State = EntityState.Modified;
                    try
                    {
                        IEnumerable<object> List = null;
                        var MsjError = "0";
                        List = db.UDP_Acce_tbUsuario_PasswordRestore(tbUsuario.usu_Id, passwordnueva);
                        foreach (UDP_Acce_tbUsuario_PasswordRestore_Result Usuario in List)
                            MsjError = Usuario.MensajeError;

                        Email(emailsalida, passwordsalida, emaildestino, passwordnueva);

                        if (MsjError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                            return View(tbUsuario);
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
                        return View(tbUsuario);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "El usuario ingresado no existe");
            }
            return RedirectToAction("Index");
        }
        
        public void Email(string emailsalida, string passwordsalida, string emaildestino, string passwordnueva)
        {
            string asunto = "Recuperación de contraseña";
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(emaildestino);
            msg.From = new MailAddress(emailsalida, "ERPZORZAL", System.Text.Encoding.UTF8);
            msg.Subject = asunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = string.Format("Esta es su nueva contraseña: {0}", passwordnueva);
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = System.Net.Mail.MailPriority.High;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(emailsalida, passwordsalida);
            client.Port = 25;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true; //Esto es para que se vaya a través de SSL que es obligatorio con Gmail
            try
            {
                client.Send(msg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

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
        public JsonResult GetRolesDisponibles(int rolId)
        {
            var list1 = db.SDP_Acce_GetRolesDisponibles(rolId).ToList();
            return Json(list1, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRoles()
        {
            var list = db.SDP_Acce_GetRoles().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUserExist(string user)
        {
            var list = db.tbUsuario.Where(s=> s.usu_NombreUsuario == user).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveRol(tbRolesUsuario Roles)
        {
            List<tbRolesUsuario> sessionRolesUsuario = new List<tbRolesUsuario>();
            var list = (List<tbRolesUsuario>)Session["tbRolesUsuario"];
            if (list == null)
            {
                sessionRolesUsuario.Add(Roles);
                Session["tbRolesUsuario"] = sessionRolesUsuario;
            }
            else
            {
                list.Add(Roles);
                Session["tbRolesUsuario"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

       

        [HttpPost]
        public JsonResult removeRol(tbRolesUsuario Roles)
        {
            var list = (List<tbRolesUsuario>)Session["tbRolesUsuario"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.rol_Id == Roles.rol_Id);
                list.Remove(itemToRemove);
                Session["tbRolesUsuario"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRolesAsignados(int rolId)
        {
            var list = db.SDP_Acce_GetRolesAsignados(rolId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EstadoInactivar(string prod_Codigo, bool Activo, string Razon_Inactivacion)
        {
            var list = db.UDP_Acce_tbUsuario_Estado(prod_Codigo, Activo, Razon_Inactivacion).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Estadoactivar(string prod_Codigo, bool Activo, string Razon_Inactivacion)
        {
            var list = db.UDP_Acce_tbUsuario_Estado(prod_Codigo, Activo, Razon_Inactivacion).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AgregarRol(int idRol, ICollection<tbRolesUsuario> RolUsuario)
        {
            var Msj = "";
            IEnumerable<Object> Rol = null;
            try
            {
                if (RolUsuario != null)
                {
                    if (RolUsuario.Count > 0)
                    {
                        foreach (tbRolesUsuario vRolUsuario in RolUsuario)
                        {
                            Rol = db.UDP_Acce_tbRolesUsuario_Insert(idRol, vRolUsuario.rol_Id, Function.GetUser()
                                                        , Function.DatetimeNow());
                            foreach (UDP_Acce_tbRolesUsuario_Insert_Result item in Rol)
                            {
                                Msj = Convert.ToString(item.MensajeError);
                            }
                        }
                        var Listado = db.SDP_Acce_GetUserRols(Function.GetUser(), "").ToList();
                        Session["UserLoginRols"] = Listado;
                    }
                }
            }
            catch (Exception)
            {
                Msj = "-1";
            }
            return Json(Msj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QuitarRol(int usu_Id, ICollection<tbRolesUsuario> RolUsuario)
        {
            var Msj = "";
            try
            {
                if (RolUsuario != null)
                {
                    if (RolUsuario.Count > 0)
                    {
                        foreach (tbRolesUsuario vRolUsuario in RolUsuario)
                        {
                            db.UDP_Acce_tbRolesUsuario_Delete(usu_Id, vRolUsuario.rol_Id);
                        }
                        var Listado = db.SDP_Acce_GetUserRols(Function.GetUser(), "").ToList();
                        Session["UserLoginRols"] = Listado;
                    }
                }
            }
            catch (Exception)
            {
                Msj = "-1";
            }
            return Json(Msj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getEmpleado(int EmpleadoID)
        {
            var EmpleadoList = db.SDP_Gral_tbEmpleado_Select((short)EmpleadoID).ToList();
            return Json(EmpleadoList, JsonRequestBehavior.AllowGet);
        }
    }
}
