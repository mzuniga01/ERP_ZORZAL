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

namespace ERP_ZORZAL.Controllers
{
    public class RolController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Rol/
        public ActionResult Index()
        {
            return View(db.tbRol.ToList());
        }

        // GET: /Rol/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRol tbRol = db.tbRol.Find(id);

            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbRol.rol_UsuarioCrea).usu_Nombres;
            var UsuarioModfica = tbRol.rol_UsuarioModifica;
            if (UsuarioModfica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModfica).usu_Nombres;
            };
            if (tbRol == null)
            {
                return HttpNotFound();
            }
            return View(tbRol);
        }

        // GET: /Rol/Create
        public ActionResult Create()
        {
            ViewBag.obj_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla");
            return View();
        }

        // POST: /Rol/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="rol_Descripcion,rol_Estado")] tbRol tbRol)
        {
            if (ModelState.IsValid)
            {
                //db.tbRol.Add(tbRol);
                //db.SaveChanges();
                try
                {
                    IEnumerable<Object> List = null;
                    var Msj = "";
                    List = db.UDP_Acce_tbRol_Insert(tbRol.rol_Descripcion, Helpers.Activo);
                    foreach (UDP_Acce_tbRol_Insert_Result Rol in List)
                        Msj = Rol.MensajeError;
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                }
                return RedirectToAction("Index");
            }

            return View(tbRol);
        }

        // GET: /Rol/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.obj_Id = new SelectList(db.tbObjeto, "obj_Id", "obj_Pantalla");
            ViewBag.ListEstado = ListEstado();
            
            tbRol tbRol = db.tbRol.Find(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbRol.rol_UsuarioCrea).usu_Nombres;
            var UsuarioModfica = tbRol.rol_UsuarioModifica;
            if (UsuarioModfica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModfica).usu_Nombres;
            };
            if (tbRol == null)
            {
                return HttpNotFound();
            }
            return View(tbRol);
        }

        // POST: /Rol/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(byte? id,[Bind(Include="rol_Id,rol_Descripcion,rol_UsuarioCrea,rol_FechaCrea,rol_Estado")] tbRol tbRol)
        {
            if (ModelState.IsValid)
            {
                 
                //db.Entry(tbRol).State = EntityState.Modified;
                //db.SaveChanges();
                try
                {
                    tbRol vRol = db.tbRol.Find(id);
                    IEnumerable<Object> List = null;
                    var Msj = "";
                    List = db.UDP_Acce_tbRol_Update(tbRol.rol_Id, tbRol.rol_Descripcion, vRol.rol_UsuarioCrea, vRol.rol_FechaCrea, vRol.rol_Estado);
                    foreach (UDP_Acce_tbRol_Update_Result Rol in List)
                        Msj = Rol.MensajeError;
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                }
                return RedirectToAction("Index");
            }
            return View(tbRol);
        }

        // GET: /Rol/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRol tbRol = db.tbRol.Find(id);
            if (tbRol == null)
            {
                return HttpNotFound();
            }
            return View(tbRol);
        }

        // POST: /Rol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbRol tbRol = db.tbRol.Find(id);
            db.tbRol.Remove(tbRol);
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

        public ActionResult EstadoRolInactivo(int id)
        {
            try
            {
                IEnumerable<Object> List = null;
                var Msj = "";
                tbRol tbRol = db.tbRol.Find(id);
                List = db.UDP_Acce_tbRolEstado_Update(id, Helpers.Inactivo);
                foreach (UDP_Acce_tbRolEstado_Update_Result Rol in List)
                    Msj = Rol.MensajeError;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
            }
            return RedirectToAction("Index");
        }

        public ActionResult EstadoRolActivo(int id)
        {
            try
            {
                IEnumerable<Object> List = null;
                var Msj = "";
                tbRol tbRol = db.tbRol.Find(id);
                List = db.UDP_Acce_tbRolEstado_Update(id, Helpers.Activo);
                foreach (UDP_Acce_tbRolEstado_Update_Result Rol in List)
                    Msj = Rol.MensajeError;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
            }
            return RedirectToAction("Index");
        }

        public List<SelectListItem> ListEstado()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Activo",
                    Value = "1"
                },
                new SelectListItem()
                {
                    Text = "Inactivo",
                    Value = "0"
                }
            };
         }

        [HttpPost]
        public JsonResult GetObjetosDisponibles(int rolId)
        {
            var list1 = db.SDP_Acce_GetObjetosDisponibles(rolId).ToList();
            return Json(list1, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetObjetosAsignados(int rolId)
        {
            var list2 = db.SDP_Acce_GetObjetosAsignados(rolId).ToList();
            return Json(list2, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetObjetos()
        {
            var list = db.SDP_Acce_GetObjetos().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertRol(string DescripcionRol, ICollection<tbAccesoRol> AccesoRol)
        {
            IEnumerable<Object> Rol = null;
            IEnumerable<Object> RolAcceso = null;
            int idRol = 0;
            var Msj1 = "";
            var Msj2 = "";
            using (TransactionScope Tran = new TransactionScope())
            {
                
                try
                {
                    if (DescripcionRol != "")
                    {
                        Rol = db.UDP_Acce_tbRol_Insert(DescripcionRol, Helpers.Activo);
                        foreach (UDP_Acce_tbRol_Insert_Result vRol in Rol)
                            Msj1 = vRol.MensajeError;
                        if (Msj1.Substring(0, 1) != "-")
                        {
                            if (AccesoRol != null)
                            {
                                if (AccesoRol.Count > 0)
                                {
                                    idRol = Convert.ToInt32(Msj1);
                                    foreach (tbAccesoRol vAccesoRol in AccesoRol)
                                    {
                                        RolAcceso = db.UDP_Acce_tbAccesoRol_Insert(idRol, vAccesoRol.obj_Id);
                                        foreach (UDP_Acce_tbAccesoRol_Insert_Result item in RolAcceso)
                                        {
                                            Msj2 = Convert.ToString(item.MensajeError);
                                        }
                                    }
                                }
                            }

                        }
                        Tran.Complete();
                    }
                }
                catch (Exception)
                {
                    Msj1 = "-1";
                }
                
            }
            return Json(Msj1, JsonRequestBehavior.AllowGet);
        }
    }
}
