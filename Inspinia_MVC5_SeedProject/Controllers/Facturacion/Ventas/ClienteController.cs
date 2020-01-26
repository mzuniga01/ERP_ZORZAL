﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Data.SqlClient;
using System.Data.Common;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class ClienteController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        // GET: /Cliente/
        [SessionManager("Cliente/Index")]
        public ActionResult Index()
        {
            var tbcliente = db.UDV_Vent_Busqueda_Clientes;
            return View(tbcliente.ToList());
        }

        [HttpPost]
        [SessionManager("Cliente/Index")]
        public ActionResult Index(string identificacion, string nombre, string telefono)
        {
            try
            {
                //var resultado = 0;
                List<UDV_Vent_Busqueda_Clientes> list = new List<UDV_Vent_Busqueda_Clientes>();
                using (var db = new ERP_ZORZALEntities())
                {
                    using (var oCmd = db.Database.Connection.CreateCommand())
                    {
                        db.Database.Connection.Open();
                        oCmd.CommandText = "Vent.GetBusquedaClient";
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Add(new SqlParameter("@identificacion", identificacion));
                        oCmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        oCmd.Parameters.Add(new SqlParameter("@telefono", telefono));

                        DbDataReader reader = oCmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UDV_Vent_Busqueda_Clientes tbclientes = new UDV_Vent_Busqueda_Clientes();
                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Id")))
                                tbclientes.clte_Id = Convert.ToInt16(reader["clte_Id"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("tpi_Id")))
                                tbclientes.tpi_Id = Convert.ToByte(reader["tpi_Id"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Identificacion")))
                                tbclientes.clte_Identificacion = Convert.ToString(reader["clte_Identificacion"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_EsPersonaNatural")))
                                tbclientes.clte_EsPersonaNatural = Convert.ToBoolean(reader["clte_EsPersonaNatural"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Nombres")))
                                tbclientes.clte_Nombres = Convert.ToString(reader["clte_Nombres"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Apellidos")))
                                tbclientes.clte_Apellidos = Convert.ToString(reader["clte_Apellidos"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_FechaNacimiento")))
                                tbclientes.clte_FechaNacimiento = Convert.ToDateTime(reader["clte_FechaNacimiento"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Nacionalidad")))
                                tbclientes.clte_Nacionalidad = Convert.ToString(reader["clte_Nacionalidad"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Sexo")))
                                tbclientes.clte_Sexo = Convert.ToString(reader["clte_Sexo"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Telefono")))
                                tbclientes.clte_Telefono = Convert.ToString(reader["clte_Telefono"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_NombreComercial")))
                                tbclientes.clte_NombreComercial = Convert.ToString(reader["clte_NombreComercial"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_RazonSocial")))
                                tbclientes.clte_RazonSocial = Convert.ToString(reader["clte_RazonSocial"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_ContactoNombre")))
                                tbclientes.clte_ContactoNombre = Convert.ToString(reader["clte_ContactoNombre"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_ContactoTelefono")))
                                tbclientes.clte_ContactoTelefono = Convert.ToString(reader["clte_ContactoTelefono"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_ContactoEmail")))
                                tbclientes.clte_ContactoEmail = Convert.ToString(reader["clte_ContactoEmail"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_FechaConstitucion")))
                                tbclientes.clte_FechaConstitucion = Convert.ToDateTime(reader["clte_FechaConstitucion"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("mun_Codigo")))
                                tbclientes.mun_Codigo = Convert.ToString(reader["mun_Codigo"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Direccion")))
                                tbclientes.clte_Direccion = Convert.ToString(reader["clte_Direccion"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_CorreoElectronico")))
                                tbclientes.clte_CorreoElectronico = Convert.ToString(reader["clte_CorreoElectronico"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_CorreoElectronico")))
                                tbclientes.clte_CorreoElectronico = Convert.ToString(reader["clte_CorreoElectronico"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_EsActivo")))
                                tbclientes.clte_EsActivo = Convert.ToBoolean(reader["clte_EsActivo"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_RazonInactivo")))
                                tbclientes.clte_RazonInactivo = Convert.ToString(reader["clte_RazonInactivo"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_ConCredito")))
                                tbclientes.clte_ConCredito = Convert.ToBoolean(reader["clte_ConCredito"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_EsMinorista")))
                                tbclientes.clte_EsMinorista = Convert.ToBoolean(reader["clte_EsMinorista"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Observaciones")))
                                tbclientes.clte_Observaciones = Convert.ToString(reader["clte_Observaciones"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_UsuarioCrea")))
                                tbclientes.clte_UsuarioCrea = Convert.ToInt16(reader["clte_UsuarioCrea"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_FechaCrea")))
                                tbclientes.clte_FechaCrea = Convert.ToDateTime(reader["clte_FechaCrea"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_UsuarioModifica")))
                                tbclientes.clte_UsuarioModifica = Convert.ToInt16(reader["clte_UsuarioModifica"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_FechaModifica")))
                                tbclientes.clte_FechaModifica = Convert.ToDateTime(reader["clte_FechaModifica"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("tpi_Descripcion")))
                                tbclientes.tpi_Descripcion = Convert.ToString(reader["tpi_Descripcion"]);

                            list.Add(tbclientes);
                        }
                    }
                }
                ViewBag.Ident = identificacion;
                ViewBag.Nombres = nombre;
                ViewBag.Tel = telefono;

                return View(list);
            }
            catch (Exception Ex)
            {
                ViewBag.Ident = identificacion;
                ViewBag.Nombres = nombre;
                ViewBag.Tel = telefono;
                var tbcliente = db.UDV_Vent_Busqueda_Clientes;
                Function.InsertBitacoraErrores("Cliente/Create", Ex.Message.ToString(), "Create");
                return View(tbcliente.ToList());
            }
        }

        // GET: /Cliente/Details/5
        [SessionManager("Cliente/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbCliente);
        }

        // GET: /Cliente/Create
        [SessionManager("Cliente/Create")]
        public ActionResult Create()
        {
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            var TipoIdentificacion = db.tbTipoIdentificacion.Select(s => new
            {
                tpi_Id = s.tpi_Id,
                tpi_Descripcion = s.tpi_Descripcion
            }).Where(x => x.tpi_Id == Helpers.RTN).ToList();
            ViewBag.tpi_Id = new SelectList(TipoIdentificacion, "tpi_Id", "tpi_Descripcion");
            return View();

        }

        // POST: /Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Cliente/Create")]
        public ActionResult Create([Bind(Include = "clte_Id,tpi_Id,clte_Identificacion,clte_EsPersonaNatural,clte_Nombres,clte_Apellidos,clte_FechaNacimiento,clte_Nacionalidad,clte_Sexo,clte_Telefono,clte_NombreComercial,clte_RazonSocial,clte_ContactoNombre,clte_ContactoEmail,clte_ContactoTelefono,clte_FechaConstitucion,mun_Codigo,clte_Direccion,clte_CorreoElectronico,clte_EsActivo,clte_RazonInactivo,clte_ConCredito,clte_EsMinorista,clte_Observaciones,clte_UsuarioCrea,clte_FechaCrea,clte_UsuarioModifica,clte_FechaModifica,clte_MontoCredito,clte_DiasCredito")] tbCliente tbCliente, string dep_Codigo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbCliente_Insert(tbCliente.tpi_Id,
                                                        tbCliente.clte_Identificacion,
                                                        tbCliente.clte_EsPersonaNatural,
                                                        tbCliente.clte_Nombres,
                                                        tbCliente.clte_Apellidos,
                                                        tbCliente.clte_FechaNacimiento,
                                                        tbCliente.clte_Nacionalidad,
                                                        tbCliente.clte_Sexo,
                                                        tbCliente.clte_Telefono,
                                                        tbCliente.clte_NombreComercial,
                                                        tbCliente.clte_RazonSocial,
                                                        tbCliente.clte_ContactoNombre,
                                                        tbCliente.clte_ContactoEmail,
                                                        tbCliente.clte_ContactoTelefono,
                                                        tbCliente.clte_FechaConstitucion,
                                                        tbCliente.mun_Codigo,
                                                        tbCliente.clte_Direccion,
                                                        tbCliente.clte_CorreoElectronico,
                                                        Helpers.ClienteActivo,
                                                        tbCliente.clte_RazonInactivo,
                                                        Helpers.ClienteCredito,
                                                        tbCliente.clte_EsMinorista,
                                                        tbCliente.clte_Observaciones,
                                                        tbCliente.clte_MontoCredito,
                                                        tbCliente.clte_DiasCredito,
                                                        Helpers.ClienteExonerado,
                                                        Function.GetUser(),
                                                        Function.DatetimeNow());
                    foreach (UDP_Vent_tbCliente_Insert_Result cliente in list)
                        MensajeError = cliente.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("Cliente/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbCliente);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("Cliente/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", dep_Codigo);
                    ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
                    ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
                    return View(tbCliente);
                }
            }
            tbCliente Cliente = new tbCliente();
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", dep_Codigo);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
            return View(tbCliente);
        }

        // GET: /Cliente/Edit/5
        [SessionManager("Cliente/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewData["Razon"] = tbCliente.clte_RazonInactivo;
            ViewData["Monto"] = tbCliente.clte_MontoCredito;
            ViewData["Dias"] = tbCliente.clte_DiasCredito;
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbCliente.tbMunicipio.tbDepartamento.dep_Codigo);
            var Departamento = tbCliente.tbMunicipio.tbDepartamento.dep_Codigo; ;
            var Municipio = db.tbMunicipio.Select(s => new
            {
                mun_Codigo = s.mun_Codigo,
                mun_Nombre = s.mun_Nombre,
                dep_Codigo = s.dep_Codigo
            }).Where(x => x.dep_Codigo == Departamento).ToList();
            ViewBag.mun_Codigo = new SelectList(Municipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
            if (tbCliente.clte_EsPersonaNatural)
                ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
            else
            {
                var TipoIdentificacion = db.tbTipoIdentificacion.Select(s => new
                {
                    tpi_Id = s.tpi_Id,
                    tpi_Descripcion = s.tpi_Descripcion
                }).Where(x => x.tpi_Id == Helpers.RTN).ToList();
                ViewBag.tpi_Id = new SelectList(TipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
            }

            var Lista = Function.GeneroList();
            ViewBag.GeneroList = new SelectList(Lista, "ID_GENERO", "DESCRIPCION", tbCliente.clte_Sexo);
            return View(tbCliente);
        }

        // POST: /Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Cliente/Edit")]
        public ActionResult Edit([Bind(Include = "clte_Id,tpi_Id,clte_Identificacion,clte_EsPersonaNatural,clte_Nombres,clte_Apellidos,clte_FechaNacimiento,clte_Nacionalidad,clte_Sexo,clte_Telefono,clte_NombreComercial,clte_RazonSocial,clte_ContactoNombre,clte_ContactoEmail,clte_ContactoTelefono,clte_FechaConstitucion,mun_Codigo,clte_Direccion,clte_CorreoElectronico,clte_EsActivo,clte_RazonInactivo,clte_ConCredito,clte_EsMinorista,clte_Observaciones,clte_UsuarioCrea,clte_FechaCrea,clte_UsuarioModifica,clte_FechaModifica,clte_MontoCredito,clte_DiasCredito")] tbCliente tbCliente, string dep_Codigo)
        {
            var Lista = Function.GeneroList();
            if (ModelState.IsValid)
            {
                try
                {
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbCliente_Update(tbCliente.clte_Id,
                                                        tbCliente.tpi_Id,
                                                        tbCliente.clte_Identificacion,
                                                        tbCliente.clte_EsPersonaNatural,
                                                        tbCliente.clte_Nombres,
                                                        tbCliente.clte_Apellidos,
                                                        tbCliente.clte_FechaNacimiento,
                                                        tbCliente.clte_Nacionalidad,
                                                        tbCliente.clte_Sexo,
                                                        tbCliente.clte_Telefono,
                                                        tbCliente.clte_NombreComercial,
                                                        tbCliente.clte_RazonSocial,
                                                        tbCliente.clte_ContactoNombre,
                                                        tbCliente.clte_ContactoEmail,
                                                        tbCliente.clte_ContactoTelefono,
                                                        tbCliente.clte_FechaConstitucion,
                                                        tbCliente.mun_Codigo,
                                                        tbCliente.clte_Direccion,
                                                        tbCliente.clte_CorreoElectronico,
                                                        tbCliente.clte_EsActivo,
                                                        tbCliente.clte_RazonInactivo,
                                                        tbCliente.clte_ConCredito,
                                                        tbCliente.clte_EsMinorista,
                                                        tbCliente.clte_Observaciones,
                                                        tbCliente.clte_UsuarioCrea,
                                                        tbCliente.clte_FechaCrea,
                                                        tbCliente.clte_MontoCredito,
                                                        tbCliente.clte_DiasCredito,
                                                        tbCliente.clte_Exonerado,
                                                        Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Vent_tbCliente_Update_Result cliente in list)
                        MensajeError = cliente.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("Cliente/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbCliente);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("Cliente/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", dep_Codigo);
                    ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
                    ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
                    ViewBag.GeneroList = new SelectList(Lista, "ID_GENERO", "DESCRIPCION", tbCliente.clte_Sexo);
                    Lista = Function.GeneroList();
                    return View(tbCliente);
                }
            }
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", dep_Codigo);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
            ViewBag.GeneroList = new SelectList(Lista, "ID_GENERO", "DESCRIPCION", tbCliente.clte_Sexo);
            Lista = Function.GeneroList();
            return View(tbCliente);
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
        public JsonResult GetMunicipios(string CodDepartamento)
        {
            var list = db.spGetMunicipios1(CodDepartamento).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetIdentificacion(bool CodIdentificacion)
        {
            IEnumerable<object> TipoIdentificacion = null;
            if (CodIdentificacion)
            {
                TipoIdentificacion = db.tbTipoIdentificacion.Select(s => new
                {
                    tpi_Id = s.tpi_Id,
                    tpi_Descripcion = s.tpi_Descripcion
                }).ToList();
            }
            else
            {
                TipoIdentificacion = db.tbTipoIdentificacion.Select(s => new
                {
                    tpi_Id = s.tpi_Id,
                    tpi_Descripcion = s.tpi_Descripcion
                }).Where(x => x.tpi_Id == Helpers.RTN).ToList();
            }
            return Json(TipoIdentificacion, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InactivarCliente(int CodCliente, bool Activo, string RazonInactivo)
        {
            var list = db.UDP_Vent_tbCliente_Estado(CodCliente, Helpers.ClienteInactivo, RazonInactivo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActivarCliente(int CodCliente, bool Activo, string RazonInactivo)
        {
            var list = db.UDP_Vent_tbCliente_Estado(CodCliente, Helpers.ClienteActivo, RazonInactivo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNacionalidades(string term)
        {
            var Nacionalidades = db.tbCliente.Where(s => s.clte_Nacionalidad.Contains(term)).Select(x => new { value = x.clte_Nacionalidad }).Distinct().ToList();
            return Json(Nacionalidades, JsonRequestBehavior.AllowGet);
        }
    }
}
