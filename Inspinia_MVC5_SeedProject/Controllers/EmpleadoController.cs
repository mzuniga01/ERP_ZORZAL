using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class EmpleadoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: /Empleado/
        [SessionManager("Empleado/Index")]
        public ActionResult Index()
        {
            var tbempleado = db.tbEmpleado.Include(t => t.tbUsuario).Include(t => t.tbTipoIdentificacion);
            return View(tbempleado.ToList());
        }

        // GET: /Empleado/Details/5
        [SessionManager("Empleado/Details")]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbEmpleado);
        }


        // GET: /Empleado/Create
        [SessionManager("Empleado/Create")]
        public ActionResult Create()
        {
            ViewBag.tpi_Id = new SelectList(db.tbEmpleado, "emp_Id", "tpi_Id");
            ViewBag.TipoIList = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");
            return View();
        }

        // POST: /Empleado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Empleado/Create")]
        public ActionResult Create([Bind(Include= "emp_Id,emp_Nombres,emp_Apellidos,emp_Sexo,emp_FechaNacimiento,tpi_Id,emp_Identificacion,emp_Telefono,emp_Correoelectronico,emp_TipoSangre,emp_Puesto,emp_FechaIngreso,emp_Direccion,emp_RazonInactivacion,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica,emp_Estado,emp_RazonSalida,emp_FechaDeSalida")] tbEmpleado tbEmpleado)
        {
            if (ModelState.IsValid)
            {
                if (db.tbEmpleado.Any(a => a.emp_Identificacion == tbEmpleado.emp_Identificacion))
                {
                    ViewBag.tpi_Id = new SelectList(db.tbEmpleado, "emp_Id", "tpi_Id", tbEmpleado.tpi_Id);
                    ViewBag.TipoIList = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");
                    ModelState.AddModelError("", "Ya existe un empleado con el mismo numero de identidad");
                    return View(tbEmpleado);
                }
                try
                {
                    IEnumerable<object> list = null;
                    string MsjError = "";
                    list = db.UDP_Gral_tbEmpleados_Insert(tbEmpleado.emp_Nombres, 
                        tbEmpleado.emp_Apellidos, 
                        tbEmpleado.emp_Sexo, 
                        tbEmpleado.emp_FechaNacimiento, 
                        tbEmpleado.tpi_Id, 
                        tbEmpleado.emp_Identificacion,
                        tbEmpleado.emp_Telefono, 
                        tbEmpleado.emp_Correoelectronico, 
                        tbEmpleado.emp_TipoSangre, 
                        tbEmpleado.emp_Puesto, 
                        tbEmpleado.emp_FechaIngreso, 
                        tbEmpleado.emp_Direccion,
                        Function.GetUser(),
                        DateTime.Now                        
                        );
                    foreach (UDP_Gral_tbEmpleados_Insert_Result empleados in list)
                        MsjError = empleados.MensajeError;

                    if (MsjError.StartsWith("-1"))
                    {
                        ViewBag.tpi_Id = new SelectList(db.tbEmpleado, "emp_Id", "tpi_Id", tbEmpleado.tpi_Id);
                        ViewBag.TipoIList = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");
                        Function.InsertBitacoraErrores("Empleado/Create", MsjError, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbEmpleado);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex) {
                    ViewBag.tpi_Id = new SelectList(db.tbEmpleado, "emp_Id", "tpi_Id", tbEmpleado.tpi_Id);
                    ViewBag.TipoIList = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");
                    Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbEmpleado);
                }
                
            }
            ViewBag.tpi_Id = new SelectList(db.tbEmpleado, "emp_Id", "tpi_Id", tbEmpleado.tpi_Id);
            ViewBag.TipoIList = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");

            return View(tbEmpleado);
        }

        // GET: /Empleado/Edit/5
        [SessionManager("Empleado/Edit")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbEmpleado.tpi_Id);
            return View(tbEmpleado);
        }

        // POST: /Empleado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Empleado/Edit")]
        public ActionResult Edit([Bind(Include= "emp_Id,emp_Nombres,emp_Apellidos,emp_Sexo,emp_FechaNacimiento,tpi_Id,emp_Identificacion,emp_Telefono,emp_Correoelectronico,emp_TipoSangre,emp_Puesto,emp_FechaIngreso,emp_Direccion,emp_RazonInactivacion,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica")] tbEmpleado tbEmpleado)
        {
            if (ModelState.IsValid)
            {
              
                try
                {
                    IEnumerable<object> list = null;
                    string MsjError = "";
                    list = db.UDP_Gral_tbEmpleados_Update(tbEmpleado.emp_Id
                                                        , tbEmpleado.emp_Nombres
                                                        , tbEmpleado.emp_Apellidos
                                                        , tbEmpleado.emp_Sexo
                                                        , tbEmpleado.emp_FechaNacimiento
                                                        , tbEmpleado.tpi_Id
                                                        , tbEmpleado.emp_Identificacion
                                                        , tbEmpleado.emp_Telefono
                                                        , tbEmpleado.emp_Correoelectronico
                                                        , tbEmpleado.emp_TipoSangre
                                                        , tbEmpleado.emp_Puesto
                                                        , tbEmpleado.emp_FechaIngreso
                                                        , tbEmpleado.emp_Direccion
                                                        , tbEmpleado.emp_RazonInactivacion
                                                        , tbEmpleado.emp_UsuarioCrea
                                                        , tbEmpleado.emp_FechaCrea,
                                                        tbEmpleado.emp_RazonSalida,
                                                        tbEmpleado.emp_FechaDeSalida,
                                                        Function.GetUser(),
                                                        DateTime.Now);
                    foreach (UDP_Gral_tbEmpleados_Update_Result empleado in list)
                        MsjError = empleado.MensajeError;                    
                    if (MsjError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("Empleado/Edit", MsjError, "Edit");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbEmpleado);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch(Exception Ex)
                {
                    ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbEmpleado.tpi_Id);
                    ViewBag.TipoIList = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");
                    ModelState.AddModelError("", "Error al actualizar el registro" + " " + Ex.Message.ToString());
                    return View(tbEmpleado);
                }                
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbEmpleado.tpi_Id);
            ViewBag.TipoIList = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", "Seleccione");
            return View(tbEmpleado);
        }

        [HttpPost]
        public JsonResult GetEmpleado(int emp_Id)
        {
            var list = db.SDP_tbEmpleado_Consulta(emp_Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult EstadoEmpleadoRazon(tbEmpleado tbEmpleado)
        {
            string Msj = "";
            try
            {
                IEnumerable<object> list = null;


                list = db.UDP_Gral_tbEmpleado_Update_RazonInactivacion(tbEmpleado.emp_Id,
                    tbEmpleado.emp_Estado,
                  
                    tbEmpleado.emp_RazonInactivacion,

                    Function.GetUser(),
                    
                    DateTime.Now                    
                    );
                foreach ( UDP_Gral_tbEmpleado_Update_RazonInactivacion_Result empleado in list)
                    Msj = empleado.MensajeError;

                if (Msj.StartsWith("-1"))
                {
                    Msj = "-1";
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Guardo el registro");
                Msj = "-1";
            }
            return Json(Msj, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult GetEmpleadoRazon(int emp_Id)
        {
            var list = db.SDP_tbEmpleado_Consulta(emp_Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RazonSalida(tbEmpleado tbEmpleado)
        {
            string Msj = "";
            try
            {
                IEnumerable<object> list = null;


                list = db.UDP_Gral_tbEmpleado_Update_RazonSalida(tbEmpleado.emp_Id,
                    tbEmpleado.emp_Estado,
                    tbEmpleado.emp_RazonSalida,
                    Function.GetUser(),
                    DateTime.Now
                    );
                foreach (UDP_Gral_tbEmpleado_Update_RazonSalida_Result empleado in list)
                    Msj = empleado.MensajeError;

                if (Msj.StartsWith("-1"))
                {
                    Msj = "-1";
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Guardo el registro");
                Msj = "-1";
            }
            return Json(Msj, JsonRequestBehavior.AllowGet);

        }
        
        public ActionResult EstadoActivar(int? id)
        {
            try
            {
                tbEmpleado obj = db.tbEmpleado.Find(id);
                tbEmpleado empleado = new tbEmpleado();
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Gral_tbEmpleado_Update_Estado(id, Helpers.EmpleadoActivo,Function.GetUser(),DateTime.Now);
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
