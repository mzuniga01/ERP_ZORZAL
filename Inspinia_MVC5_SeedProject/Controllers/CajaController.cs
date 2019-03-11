using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using ERP_GMEDINA.Reports;
using ERP_GMEDINA.Dataset;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;

namespace ERP_GMEDINA.Controllers
{
    public class CajaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /Caja/

        [HttpPost]
        public ActionResult RVPCEF()
        {
            //int iTipoReporte = Objeto.obj_Id;
            //var list = db.SDP_Acce_GetReportes().ToList();
            ReportDocument rd = new ReportDocument();
            Stream stream = null;
            rptVentasPorCajaEntreFecha SalidaRV = new rptVentasPorCajaEntreFecha();
            Reportes SalidaDST = new Reportes();

            var SalidaTableAdapter = new UDV_Vent_VentasPorCaja_EntreFechasTableAdapter();

            try
            {
                SalidaTableAdapter.FillFiltros(SalidaDST.UDV_Vent_VentasPorCaja_EntreFechas,"CAJA 4", "Brayan Interiano");

                SalidaRV.SetDataSource(SalidaDST);
                stream = SalidaRV.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                SalidaRV.Close();
                SalidaRV.Dispose();

                string fileName = "rptVentasPorCajaEntreFecha.pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                return File(stream, "application/pdf");
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                throw;
            }
        }

        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Caja/Index"))
                    {
                        return View(db.tbCaja.ToList());
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /Caja/Details/5
        public ActionResult Details(short? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Caja/Details"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbCaja tbCaja = db.tbCaja.Find(id);
                        if (tbCaja == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        return View(tbCaja);
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /Caja/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Caja/Create"))
                    {
                        tbCaja caja = new tbCaja();
                        List<tbUsuario> List = Function.getUserInformation();
                        int SucursalId = 0;
                        foreach(tbUsuario User in List)
                        {
                            SucursalId =  (int)User.suc_Id;
                        }
                        var Sucursal = db.tbSucursal.Select(s => new
                        {
                            suc_Id = s.suc_Id,
                            suc_Descripcion = s.suc_Descripcion
                        }).Where(x => x.suc_Id == SucursalId).ToList();
                        ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                        return View(caja);
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // POST: /Caja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="cja_Id,cja_Descripcion,suc_Id,cja_UsuarioCrea,cja_FechaCrea,cja_UsuarioModifica,cja_FechaModifica")] tbCaja tbCaja)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Caja/Create"))
                    {
                        List<tbUsuario> List = Function.getUserInformation();
                        int SucursalId = 0;
                        foreach (tbUsuario User in List)
                        {
                            SucursalId = (int)User.suc_Id;
                        }
                        var Sucursal = db.tbSucursal.Select(s => new
                        {
                            suc_Id = s.suc_Id,
                            suc_Descripcion = s.suc_Descripcion
                        }).Where(x => x.suc_Id == SucursalId).ToList();
                        try
                        {
                            if (ModelState.IsValid)
                            {
                                string MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbCaja_Insert(tbCaja.cja_Descripcion, tbCaja.suc_Id, Function.GetUser(), Function.DatetimeNow());
                                foreach (UDP_Vent_tbCaja_Insert_Result caja in list)
                                    MensajeError = caja.MensajeError.ToString();
                                if (MensajeError.StartsWith("-1"))
                                {
                                    ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                                    Function.InsertBitacoraErrores("Caja/Create", MensajeError, "Create");
                                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                    return View(tbCaja);
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                            }
                            ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                            return View(tbCaja);
                        }
                        catch (Exception Ex)
                        {
                            ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                            Function.InsertBitacoraErrores("Caja/Create", Ex.Message.ToString(), "Create");
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            return View(tbCaja);
                        }
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");            
        }

        // GET: /Caja/Edit/5
        public ActionResult Edit(short? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Caja/Edit"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbCaja tbCaja = db.tbCaja.Find(id);
                        if (tbCaja == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        ViewBag.cja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCaja.cja_UsuarioCrea);
                        ViewBag.cja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCaja.cja_UsuarioModifica);
                        List<tbUsuario> List = Function.getUserInformation();
                        int SucursalId = 0;
                        foreach (tbUsuario User in List)
                        {
                            SucursalId = (int)User.suc_Id;
                        }
                        var Sucursal = db.tbSucursal.Select(s => new
                        {
                            suc_Id = s.suc_Id,
                            suc_Descripcion = s.suc_Descripcion
                        }).Where(x => x.suc_Id == SucursalId).ToList();
                        ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                        return View(tbCaja);
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // POST: /Caja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "cja_Id,cja_Descripcion,suc_Id,cja_UsuarioCrea,cja_FechaCrea,cja_UsuarioModifica,cja_FechaModifica,tbUsuario,tbUsuario1")] tbCaja tbCaja)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Caja/Edit"))
                    {
                        List<tbUsuario> List = Function.getUserInformation();
                        int SucursalId = 0;
                        foreach (tbUsuario User in List)
                        {
                            SucursalId = (int)User.suc_Id;
                        }
                        var Sucursal = db.tbSucursal.Select(s => new
                        {
                            suc_Id = s.suc_Id,
                            suc_Descripcion = s.suc_Descripcion
                        }).Where(x => x.suc_Id == SucursalId).ToList();
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                string MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbCaja_Update(tbCaja.cja_Id, tbCaja.cja_Descripcion, tbCaja.suc_Id, tbCaja.cja_UsuarioCrea, tbCaja.cja_FechaCrea, Function.GetUser(), Function.DatetimeNow());
                                foreach (UDP_Vent_tbCaja_Update_Result caja in list)
                                    MensajeError = caja.MensajeError.ToString();
                                if (MensajeError.StartsWith("-1"))
                                {
                                    ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                                    Function.InsertBitacoraErrores("Caja/Edit", MensajeError, "Edit");
                                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                    return View(tbCaja);
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                            }
                            catch (Exception Ex)
                            {
                                ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                                Function.InsertBitacoraErrores("Caja/Edit", Ex.Message.ToString(), "Edit");
                                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                return View(tbCaja);
                            }
                        }
                        ViewBag.cja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCaja.cja_UsuarioCrea);
                        ViewBag.cja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCaja.cja_UsuarioModifica);
                        ViewBag.suc_Id = new SelectList(Sucursal, "suc_Id", "suc_Descripcion");
                        return View(tbCaja);
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");
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
