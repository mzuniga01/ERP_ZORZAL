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
    public class ProveedorController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Proveedor/
        public ActionResult Index()
        {
            return View(db.tbProveedor.ToList());
        }

        // GET: /Proveedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbProveedor.prov_UsuarioCrea).usu_NombreUsuario;
            var UsuarioModfica = tbProveedor.prov_UsuarioModifica;
            if (UsuarioModfica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModfica).usu_NombreUsuario;
            };
            if (tbProveedor == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbProveedor);
        }

        // GET: /Proveedor/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.smserror = TempData["smserror"].ToString();
            }
            catch { }
            ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
            return View();
        }


        [HttpPost]
        public JsonResult GetActividadEconomica(short? acte_Id)
        {
            var list = db.spGetActividadEconomica(acte_Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        // GET: /Proveedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
         
          
            if (tbProveedor == null)
            {
              
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.Actividad = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion", tbProveedor.acte_Id);
            return View(tbProveedor);
        }





        [HttpPost]
        public JsonResult GuardarProveedor(string prov_RTN, string prov_Nombre, string prov_NombreContacto, string prov_Direccion, string prov_Email, string prov_Telefono,short? acte_Id)
        {
            var MsjError = "";
            if (ModelState.IsValid)
            {
                //db.tbUnidadMedida.Add(tbProveedor);
                //db.SaveChanges();
                try
                {
                    IEnumerable<object> List = null;
                   


                    List = db.UDP_Inv_tbProveedor_Insert(prov_Nombre, prov_NombreContacto, prov_Direccion, prov_Email, prov_Telefono, prov_RTN, acte_Id);
                    foreach (UDP_Inv_tbProveedor_Insert_Result Proveedor in List)
                        MsjError = Proveedor.MensajeError;

                    if (MsjError == "-1")
                    {

                        ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");
                     
                    }

                    else if (MsjError == "-2")
                    {

                        ModelState.AddModelError("prov_RTN", "No se guardo el registro, Contacte al Administrador");
                       
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
        // POST: /Proveedor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public JsonResult ActualizarProveedor( int? prov_Id, string prov_RTN, string prov_Nombre, string prov_NombreContacto, string prov_Direccion, string prov_Email, string prov_Telefono, short? acte_Id)
        {
            var MsjError = "";
            tbProveedor tbProveedor = db.tbProveedor.Find(prov_Id);
            if (ModelState.IsValid)
            {
                //db.tbUnidadMedida.Add(tbProveedor);
                //db.SaveChanges();
                try
                {
                    IEnumerable<object> List = null;



                    List = db.UDP_Inv_tbProveedor_Update(prov_Id,prov_Nombre, prov_NombreContacto, prov_Direccion, prov_Email, prov_Telefono, prov_RTN, acte_Id);
                    foreach (UDP_Inv_tbProveedor_Update_Result Proveedor in List)
                        MsjError = Proveedor.MensajeError;

                    if (MsjError == "-1")
                    {
                      
                        ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");

                    }

                    else if (MsjError == "-2")
                    {

                        ModelState.AddModelError("prov_RTN", "No se guardo el registro, Contacte al Administrador");

                    }


                }
                catch (Exception Ex)
                {
                    MsjError = "-1";
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro, Contacte al Administrador");
                }
                ViewBag.Actividad = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion", tbProveedor.acte_Id);
            }

            return Json(MsjError, JsonRequestBehavior.AllowGet);

        }
        // GET: /Proveedor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbProveedor);
        }

        // POST: /Proveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            db.tbProveedor.Remove(tbProveedor);
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
