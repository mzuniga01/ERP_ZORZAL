using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class EstadoMovimientoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /EstadoMovimiento/
        public ActionResult Index()
        {
           
try { ViewBag.smserror = TempData["smserror"].ToString();
    } catch { }
            return View(db.tbEstadoMovimiento.ToList());
        }
        

        // GET: /EstadoMovimiento/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
                return HttpNotFound();
            }
            return View(tbEstadoMovimiento);
        }

        // GET: /EstadoMovimiento/Create
        public ActionResult Create()
        {
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
                        List = db.UDP_Inv_tbEstadoMovimiento_Insert(tbEstadoMovimiento.estm_Descripcion);
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
                return HttpNotFound();
            }
            return View(tbEstadoMovimiento);
        }

        // POST: /EstadoMovimiento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(byte? id, [Bind(Include="estm_Id,estm_Descripcion,estm_UsuarioCrea,estm_FechaCrea")] tbEstadoMovimiento tbEstadoMovimiento)
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
                    List = db.UDP_Inv_tbEstadoMovimiento_Update(tbEstadoMovimiento.estm_Id, tbEstadoMovimiento.estm_Descripcion, tbEstadoMovimiento.estm_UsuarioCrea, tbEstadoMovimiento.estm_FechaCrea);
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoMovimiento tbEstadoMovimiento = db.tbEstadoMovimiento.Find(id);
            if (tbEstadoMovimiento == null)
            {
                return HttpNotFound();
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

                //if(MsjError.StartsWith("-1The DELETE statement conflicted with the REFERENCE constraint"))
                //{
                //    ViewBag.Error = "Registro Tiene Dependencia";
                //   return RedirectToAction("Index");
                //}


                if (MsjError.StartsWith("-1The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    TempData["smserror"] = " No se puede eliminar el dato porque tiene dependencia.";
                    ViewBag.smserror = TempData["smserror"];

                    ModelState.AddModelError("", "No se puede borrar el registro");
                    return RedirectToAction("Index");
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

    }
}
