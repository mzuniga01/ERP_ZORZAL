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
    public class EstadoMovimientoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: /EstadoMovimiento/
        public ActionResult Index()
        {
            if (Function.Sesiones("EstadoMovimiento/Index"))
            {

            }
            else
            {
                return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
            }
            return View(db.tbEstadoMovimiento.ToList());
        }


        // GET: /EstadoMovimiento/Details/5
        public ActionResult Details(byte? id)
        {
            if (Function.Sesiones("EstadoMovimiento/Details"))
            {

            }
            else
            {
                return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
            }
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

        // GET: /EstadoMovimiento/Create
        public ActionResult Create()
        {
            if (Function.Sesiones("EstadoMovimiento/Create"))
            {

            }
            else
            {
                return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
            }
            return View();
        }

        // POST: /EstadoMovimiento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "estm_Descripcion")] tbEstadoMovimiento tbEstadoMovimiento)
        {
            {
                if (ModelState.IsValid)
                {
                    //db.tbRol.Add(tbRol);
                    //db.SaveChanges();
                    try
                    {
                        IEnumerable<Object> List = null;
                        var Msj = "";
                        List = db.UDP_Inv_tbEstadoMovimiento_Insert(tbEstadoMovimiento.estm_Id, tbEstadoMovimiento.estm_Descripcion);
                        foreach (UDP_Inv_tbEstadoMovimiento_Insert_Result EstadoMovimientos in List)
                            Msj = EstadoMovimientos.MensajeError;
                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                    }
                    return RedirectToAction("Index");
                }

                return View(tbEstadoMovimiento);
            }
        }
        // GET: /EstadoMovimiento/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (Function.Sesiones("EstadoMovimiento/Edit"))
            {

            }
            else
            {
                return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
            }
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

        // POST: /EstadoMovimiento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(byte? id, [Bind(Include = "estm_Id,estm_Descripcion,estm_UsuarioCrea,estm_FechaCrea")] tbEstadoMovimiento tbEstadoMovimiento)
        {
            
            if (ModelState.IsValid)
            {

                //db.Entry(tbRol).State = EntityState.Modified;
                //db.SaveChanges();
                try
                {
                    tbEstadoMovimiento VtbEstadoMovimiento = db.tbEstadoMovimiento.Find(id);
                    IEnumerable<Object> List = null;
                    var Msj = "";
                    List = db.UDP_Inv_tbEstadoMovimiento_Update(tbEstadoMovimiento.estm_Id, tbEstadoMovimiento.estm_Descripcion);
                    foreach (UDP_Inv_tbEstadoMovimiento_Update_Result EstadoMovimiento in List)
                        Msj = EstadoMovimiento.MensajeError;
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                }
                return RedirectToAction("Index");
            }
            return View(tbEstadoMovimiento);
        }


        // GET: /EstadoMovimiento/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbEstadoMovimiento tbEstadoMovimiento = db.tbEstadoMovimiento.Find(id);
            if (tbEstadoMovimiento == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbEstadoMovimiento);
        }

        // POST: /EstadoMovimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbEstadoMovimiento tbEstadoMovimiento = db.tbEstadoMovimiento.Find(id);
            db.tbEstadoMovimiento.Remove(tbEstadoMovimiento);
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
        public ActionResult Eliminar(byte id)
        {
            try
            {
                tbEstadoMovimiento obj = db.tbEstadoMovimiento.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbEstadoMovimiento_Delete(id);
                foreach (UDP_Inv_tbEstadoMovimiento_Delete_Result Estm in list)
                    MsjError = Estm.MensajeError;



                if (MsjError.StartsWith("-1The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    TempData["smserror"] = " No se puede eliminar el dato porque tiene dependencia.";
                    ViewBag.smserror = TempData["smserror"];

                    ModelState.AddModelError("", "No se puede borrar el registro");
                    return RedirectToAction("Edit/" + id);
                }

                else
                {
                    //ViewBag.smserror = "";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {

                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Index");
            }


            //return RedirectToAction("Index");

        }

        [HttpPost]
        public JsonResult GuardarEstado(byte? estm_Id, string estm_Descripcion)
        {
          
            var MsjError = "";
            if (ModelState.IsValid)
            {
              

                try
                {
                    IEnumerable<Object> List = null;

                    List = db.UDP_Inv_tbEstadoMovimiento_Insert(estm_Id, estm_Descripcion);
                    foreach (UDP_Inv_tbEstadoMovimiento_Insert_Result EstadoMovimientos in List)
                        MsjError = EstadoMovimientos.MensajeError;
                    if (MsjError == "-1")
                    {

                        ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");

                    }
                    if (MsjError == "-2")
                    {

                        ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");

                    }


                }
                catch (Exception Ex)
                {
                    MsjError = "-1";
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro, Contacte al Administrador");
                }

            }
            return Json(MsjError, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ActualizarEstado(byte? estm_Id, string estm_Descripcion)
        {
            var MsjError = "";
            if (ModelState.IsValid)
            {


                try
                {
                    IEnumerable<Object> List = null;

                    List = db.UDP_Inv_tbEstadoMovimiento_Update(estm_Id,estm_Descripcion);
                    foreach (UDP_Inv_tbEstadoMovimiento_Update_Result EstadoMovimientos in List)
                        MsjError = EstadoMovimientos.MensajeError;
                    if (MsjError =="-1")
                    {

                        ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");
                      
                    }
                    if (MsjError == "-2")
                    {

                        ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");
                        
                    }


                }
                catch (Exception Ex)
                {
                   
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro, Contacte al Administrador");
                }

            }
            return Json(MsjError, JsonRequestBehavior.AllowGet);

        }

    }
}
