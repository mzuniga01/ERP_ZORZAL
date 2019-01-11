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

namespace ERP_GMEDINA.Controllers
{
    public class UsuarioController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Usuario/
        public ActionResult Index()
        {
            return View(db.tbUsuario.ToList());
        }

        public ActionResult ModificarPass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            ViewBag.User_ID = id;
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuario);

            //ViewBag.UserID = id;
            //return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarPass([Bind(Include = "usu_Id,usu_NombreUsuario,usu_Password,usu_Nombres,usu_Apellidos,usu_Correo")] tbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tbUsuario).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
                try
                {
                    IEnumerable<object> List = null;
                    var MsjError = "0";
                    List = db.UDP_Acce_tbUsuario_PasswordUpdate(tbUsuario.usu_Id, tbUsuario.usu_Password);
                    foreach (UDP_Acce_tbUsuario_PasswordUpdate_Result Usuario in List)
                        MsjError = Usuario.MensajeError;

                    if (MsjError == "-1")
                    {

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
                }
            }
            return View(tbUsuario);
        }

        // GET: /Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuario);
        }

        // GET: /Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "usu_NombreUsuario,usu_Password,usu_Nombres,usu_Apellidos,usu_Correo,usu_ConfirmarPassword")] tbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                //db.tbUsuario.Add(tbUsuario);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                try
                {
                    IEnumerable<object> List = null;
                    var MsjError = "0";
                    List = db.UDP_Acce_tbUsuario_Insert(tbUsuario.usu_NombreUsuario, tbUsuario.usu_Password, tbUsuario.usu_Nombres, tbUsuario.usu_Apellidos, tbUsuario.usu_Correo);
                    foreach (UDP_Acce_tbUsuario_Insert_Result Usuario in List)
                        MsjError = Usuario.MensajeError;

                    if (MsjError == "-1")
                    {

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
                }
            }

            return View(tbUsuario);
        }

        // GET: /Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            ViewBag.User_ID = id;
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuario);
        }

        // POST: /Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "usu_Id,usu_NombreUsuario,usu_Nombres,usu_Apellidos,usu_Correo,usu_EsActivo,usu_RazonInactivo,usu_EsAdministrador,usu_Password")] tbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tbUsuario).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
                try
                {
                    IEnumerable<object> List = null;
                    var MsjError = "0";
                    List = db.UDP_Acce_tbUsuario_Update(tbUsuario.usu_Id, tbUsuario.usu_NombreUsuario, tbUsuario.usu_Nombres, tbUsuario.usu_Apellidos, tbUsuario.usu_Correo, tbUsuario.usu_EsActivo, tbUsuario.usu_RazonInactivo, tbUsuario.usu_EsAdministrador,tbUsuario.usu_SesionesValidas);
                    foreach (UDP_Acce_tbUsuario_Update_Result Usuario in List)
                        MsjError = Usuario.MensajeError;

                    if (MsjError == "-1")
                    {

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
                }
            }
            return View(tbUsuario);
        }

        public ActionResult RestaurarPassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RestaurarPassword(tbUsuario usuarioRecuperar)
        {
            if (ModelState.IsValidField("usuario"))
            {
                var usuario = db.tbUsuario.Where(item => item.usu_NombreUsuario == usuarioRecuperar.usu_NombreUsuario).FirstOrDefault();
                if (usuario != null)
                {
                    string emailsalida = "erpzorzal@gmail.com";
                    string passwordsalida = "sistemadeinventari0";
                    string emaildestino = usuario.usu_Correo;

                    //PasswordGroup.Special,

                    string passwordnueva = RandomPassword.Generate(8, PasswordGroup.Uppercase, PasswordGroup.Lowercase, PasswordGroup.Numeric);
                    db.Entry(usuario).State = EntityState.Modified;
                     var PassUser = Convert.FromBase64String(passwordnueva);
                    try
                    {
                        IEnumerable<object> List = null;
                        var MsjError = "0";
                        List = db.UDP_Acce_tbUsuario_PasswordUpdate(usuario.usu_Id, PassUser);
                        foreach (UDP_Acce_tbUsuario_PasswordUpdate_Result Usuario in List)
                            MsjError = Usuario.MensajeError;

                        Email(emailsalida, passwordsalida, emaildestino, passwordnueva);

                        if (MsjError == "-1")
                        {

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
                    }
                    //usuario.usu_Password = PassUser;
                    //db.SaveChanges();

                  

                }
            } else
            {
                ModelState.AddModelError("", "El usuario ingresado no existe");
            }
            return View(usuarioRecuperar);
        }

        public String getString(byte[] text)
        {
            System.Text.ASCIIEncoding codificador = new System.Text.ASCIIEncoding();
            return codificador.GetString(text);
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



        // GET: /Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuario);
        }

        // POST: /Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            db.tbUsuario.Remove(tbUsuario);
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
    }
}
