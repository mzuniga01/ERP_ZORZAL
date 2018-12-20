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
    public class EmpleadoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Empleado/
        public ActionResult Index()
        {
            var tbempleado = db.tbEmpleado.Include(t => t.tbUsuario).Include(t => t.tbTipoIdentificacion);
            return View(tbempleado.ToList());
        }

        // GET: /Empleado/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleado);
        }


        // GET: /Empleado/Create
        public ActionResult Create()
        {
            ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.tpi_Id = new SelectList(db.tbEmpleado, "emp_Id", "tpi_Id");
            ViewBag.TipoIList = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");
            //ViewBag.listaIdentidficacion = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");
            return View();
        }

        // POST: /Empleado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="emp_Id,emp_Nombres,emp_Apellidos,emp_Sexo,emp_FechaNacimiento,tpi_Id,emp_Identificacion,emp_Telefono,emp_Correoelectronico,emp_TipoSangre,emp_Puesto,emp_FechaIngreso,emp_Direccion,emp_Observaciones,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica")] tbEmpleado tbEmpleado)
        {
            if (ModelState.IsValid)
            {
               
                try
                {
                    IEnumerable<object> list = null;
                    string MsjError = "";
                    list = db.UDP_Gral_tbEmpleados_Insert(tbEmpleado.emp_Nombres, tbEmpleado.emp_Apellidos, tbEmpleado.emp_Sexo, tbEmpleado.emp_FechaNacimiento, tbEmpleado.tpi_Id, tbEmpleado.emp_Identificacion, tbEmpleado.emp_Telefono, tbEmpleado.emp_Correoelectronico, tbEmpleado.emp_TipoSangre, tbEmpleado.emp_Puesto, tbEmpleado.emp_FechaIngreso, tbEmpleado.emp_Direccion, tbEmpleado.emp_Observaciones);
                    foreach (UDP_Gral_tbEmpleados_Insert_Result empleados in list)
                        MsjError = empleados.MensajeError;

                    if (MsjError.Substring(0, 2) == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo registrar el Dato");
                        return View(tbEmpleado);
                    }
                    else
                    {
                        //db.tbEmpleado.Add(tbEmpleado);
                        //db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex) {
                    //ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleado.emp_UsuarioCrea);
                    //ViewBag.tpi_Id = new SelectList(db.tbEmpleado, "emp_Id", "tpi_Id", tbEmpleado.tpi_Id);
                    //ViewBag.TipoIList = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");
                    ModelState.AddModelError("", "No se pudo ingresar el registro" + " " + Ex);
                    return RedirectToAction("Index");
                }
                
            }
            ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleado.emp_UsuarioCrea);
            ViewBag.tpi_Id = new SelectList(db.tbEmpleado, "emp_Id", "tpi_Id", tbEmpleado.tpi_Id);
            ViewBag.TipoIList = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");

            return View(tbEmpleado);
        }

        // GET: /Empleado/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return HttpNotFound();
            }

            
            ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleado.emp_UsuarioCrea);
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbEmpleado.tpi_Id);
           
            return View(tbEmpleado);
        }

        // POST: /Empleado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="emp_Id,emp_Nombres,emp_Apellidos,emp_Sexo,emp_FechaNacimiento,tpi_Id,emp_Identificacion,emp_Telefono,emp_Correoelectronico,emp_TipoSangre,emp_Puesto,emp_FechaIngreso,emp_Direccion,emp_Observaciones,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica, tbUsuario, tbUsuario1")] tbEmpleado tbEmpleado)
        {
           
            if (ModelState.IsValid)
            {
               
                try
                {
                    IEnumerable<object> list = null;
                    string MsjError = "";                    
                    list = db.UDP_Gral_tbEmpleados_Update(tbEmpleado.emp_Id
                                                        ,tbEmpleado.emp_Nombres
                                                        ,tbEmpleado.emp_Apellidos
                                                        ,tbEmpleado.emp_Sexo
                                                        ,tbEmpleado.emp_FechaNacimiento
                                                        ,tbEmpleado.tpi_Id
                                                        ,tbEmpleado.emp_Identificacion
                                                        ,tbEmpleado.emp_Telefono
                                                        ,tbEmpleado.emp_Correoelectronico
                                                        ,tbEmpleado.emp_TipoSangre
                                                        ,tbEmpleado.emp_Puesto
                                                        ,tbEmpleado.emp_FechaIngreso
                                                        ,tbEmpleado.emp_Direccion
                                                        ,tbEmpleado.emp_Observaciones
                                                        ,tbEmpleado.emp_UsuarioCrea
                                                        ,tbEmpleado.emp_FechaCrea);
                    foreach (UDP_Gral_tbEmpleados_Update_Result empleado in list)
                        MsjError = empleado.MensajeError;                    
                    if (MsjError.Substring(0, 2) == "-1")
                    {
                        ModelState.AddModelError("", "Error al actualizar el registro");
                        return View(tbEmpleado);
                    }
                    else
                    {
                        //db.Entry(tbEmpleado).State = EntityState.Modified;
                        //db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
                catch(Exception Ex)
                {
                    //ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleado.emp_UsuarioCrea);
                    //ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbEmpleado.tpi_Id);
                    //ViewBag.TipoIList = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");                    
                    //ModelState.AddModelError("","Error al actualizar el registro" + " "+ Ex.Message.ToString() );
                    //return View(tbEmpleado);
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se guardo el cambio");
                    return RedirectToAction("Index");

                }                
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleado.emp_UsuarioCrea);
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbEmpleado.tpi_Id);
            ViewBag.TipoIList = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");
            //ViewBag.Macho = "H";
            //ViewBag.Hembra = "M";
            return View(tbEmpleado);
        }

        public ActionResult Estadoactivar(int? id)
        {
            try
            {
                tbObjeto obj = db.tbObjeto.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Gral_tbEmpleado_Update_Estado(id, Helpers.Activo);
                foreach (UDP_Gral_tbEmpleado_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro contacte con el administrador");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro contacte con el administrador");
                return RedirectToAction("Edit/" + id);
            }
            //return RedirectToAction("Index");
        }
        public ActionResult EstadoInactivar(int? id)
        {
            try
            {
                tbObjeto obj = db.tbObjeto.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Gral_tbEmpleado_Update_Estado(id, Helpers.Inactivo);
                foreach (UDP_Gral_tbEmpleado_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro contacte con el administrador");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro contacte con el administrador");
                return RedirectToAction("Edit/" + id);
            }
            //return RedirectToAction("Index");
        }

        // GET: /Empleado/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleado);
        }

        // POST: /Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            db.tbEmpleado.Remove(tbEmpleado);
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
