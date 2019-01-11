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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
                return HttpNotFound();
            }
            return View(tbProveedor);
        }

        // GET: /Proveedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Proveedor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="prov_Nombre,prov_NombreContacto,prov_Direccion,prov_Email,prov_Telefono,prov_UsuarioCrea,prov_FechaCrea")] tbProveedor tbProveedor)
        {
            if (ModelState.IsValid)
            {
                //db.tbUnidadMedida.Add(tbProveedor);
                //db.SaveChanges();
                try
                {
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Inv_tbProveedor_Insert(tbProveedor.prov_Nombre,tbProveedor.prov_NombreContacto,tbProveedor.prov_Direccion,tbProveedor.prov_Email,tbProveedor.prov_Telefono);
                    foreach (UDP_Inv_tbProveedor_Insert_Result Proveedor in List)
                        MsjError = Proveedor.MensajeError;

                    if (MsjError == "-1")
                    {
                        ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }


                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro, Contacte al Administrador");
                }
                return RedirectToAction("Index");
            }

            return View(tbProveedor);
        }

        // GET: /Proveedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
                return HttpNotFound();
            }
            return View(tbProveedor);
        }

        // POST: /Proveedor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(byte? id,[Bind(Include="prov_Id,prov_Nombre,prov_NombreContacto,prov_Direccion,prov_Email,prov_Telefono,prov_UsuarioCrea,prov_FechaCrea,prov_UsuarioModifica,prov_FechaModifica")] tbProveedor tbProveedor)
        {
           
            if (ModelState.IsValid)
            {
              
                try
                {
                    tbProveedor vtbProveedor = db.tbProveedor.Find(id);
                    /*:ssTZD*/
                    IEnumerable<object> List = null;
                    var MsjError ="";
                    List = db.UDP_Inv_tbProveedor_Update(tbProveedor.prov_Id,
                                                         tbProveedor.prov_Nombre,
                                                         tbProveedor.prov_NombreContacto,
                                                         tbProveedor.prov_Direccion,
                                                         tbProveedor.prov_Email,
                                                         tbProveedor.prov_Telefono, 
                                                         tbProveedor.prov_UsuarioCrea, 
                                                         tbProveedor.prov_FechaCrea);
                    foreach (UDP_Inv_tbProveedor_Update_Result Proveedor in List)
                        MsjError = Proveedor.MensajeError;

                    if (MsjError== "-1")
                    {
                        ModelState.AddModelError("", "No se Actualizo el registro");
                        return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            return View(tbProveedor);
        }

    

    // GET: /Proveedor/Delete/5
    public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return HttpNotFound();
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
