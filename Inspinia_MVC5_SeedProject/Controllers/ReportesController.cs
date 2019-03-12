using CrystalDecisions.CrystalReports.Engine;
using ERP_GMEDINA.Dataset;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_GMEDINA.Controllers
{
    public class ReportesController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: Reportes
        public ActionResult Index()
        {
            llenarListas();
            return View();
        }

        [HttpPost]
        public ActionResult Index(tbObjeto Objeto, string TipoFactura, string clte_Identificacion, string FechaHasta, string FechaDesde, short? cja_Id, int? fact_UsuarioCrea, int? suc_Id,
            string clte_Identificacion_, bool? clte_EsPersonaNatural, string clte_Identificacion_1)
        {
            if (clte_EsPersonaNatural == null)
                clte_EsPersonaNatural = false;
            int iTipoReporte = Objeto.obj_Id;
            Stream stream = null;
            string fileName = "";

            try
            {
                int Usuario = Function.GetUser();
                var UsuarioName = db.tbUsuario.Where(x => x.usu_Id == Usuario).Select(i => new { i.usu_Nombres, i.usu_Apellidos, i.suc_Id }).FirstOrDefault();
                if (iTipoReporte == Helpers.rptVentasFechas)
                {
                    Ventas_entre_Fechas SalidaRV = new Ventas_entre_Fechas();
                    Reportes SalidaDST = new Reportes();
                    var SalidaTableAdapter = new UDV_Vent_Factura_VentasporFechaTableAdapter();
                    SalidaTableAdapter.FillFiltros(SalidaDST.UDV_Vent_Factura_VentasporFecha, clte_Identificacion, TipoFactura, FechaDesde, FechaHasta);
                    SalidaRV.SetDataSource(SalidaDST);
                    SalidaRV.SetParameterValue("usuario", UsuarioName.usu_Nombres + " " + UsuarioName.usu_Apellidos);
                    SalidaRV.SetParameterValue("Fecha_Desde", FechaDesde);
                    SalidaRV.SetParameterValue("Fecha_Hasta", FechaHasta);
                    stream = SalidaRV.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);

                    SalidaRV.Close();
                    SalidaRV.Dispose();
                    fileName = "VentasEntreFechas.pdf";
                }
                else if (iTipoReporte == Helpers.rptVentasCajaFechas)
                {
                    rptVentasPorCajaEntreFecha VentasCaja = new rptVentasPorCajaEntreFecha();
                    Reportes DataSet = new Reportes();
                    var VentasTableAdapter = new UDV_Vent_VentasPorCaja_EntreFechasTableAdapter();
                    VentasTableAdapter.FillFiltros(DataSet.UDV_Vent_VentasPorCaja_EntreFechas, suc_Id, cja_Id, fact_UsuarioCrea, FechaDesde, FechaHasta);
                    VentasCaja.SetDataSource(DataSet);
                    VentasCaja.SetParameterValue("usuario", UsuarioName.usu_Nombres + " " + UsuarioName.usu_Apellidos);
                    stream = VentasCaja.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);

                    VentasCaja.Close();
                    VentasCaja.Dispose();
                    fileName = "VentasPorCajaEntreFecha.pdf";
                }
                else if (iTipoReporte == Helpers.rptFacturasPendientesPago)
                {
                    FacturasPendienteDePago FacturasPendientes = new FacturasPendienteDePago();
                    Reportes DataSet = new Reportes();
                    var TableAdapter = new UDV_Vent_FacturasPendientesDePagoTableAdapter();
                    TableAdapter.FillFiltros(DataSet.UDV_Vent_FacturasPendientesDePago, FechaDesde, FechaHasta, clte_Identificacion_);
                    FacturasPendientes.SetDataSource(DataSet);
                    stream = FacturasPendientes.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);

                    FacturasPendientes.Close();
                    FacturasPendientes.Dispose();
                    fileName = "FacturasPendientesPago.pdf";
                }
                else if (iTipoReporte == Helpers.rptVentasConsumidorFinal)
                {
                    VentasConsumidorFinal Reporte = new VentasConsumidorFinal();
                    Reportes DataSet = new Reportes();
                    var TableAdapter = new UDV_Vent_VentasConsumidorFinalTableAdapter();
                    TableAdapter.FillFiltros(DataSet.UDV_Vent_VentasConsumidorFinal, FechaDesde, FechaHasta);
                    Reporte.SetDataSource(DataSet);
                    Reporte.SetParameterValue("usuario", UsuarioName.usu_Nombres + " " + UsuarioName.usu_Apellidos);
                    stream = Reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);

                    Reporte.Close();
                    Reporte.Dispose();
                    fileName = "VentasConsumidorFinal.pdf";
                }
                else if (iTipoReporte == Helpers.rptNotasCreditoEntreFechas)
                {
                    NotaCredito Reporte = new NotaCredito();
                    Reportes DataSet = new Reportes();
                    var TableAdapter = new UDV_Vent_NotaCreditoPorFechaTableAdapter();
                    TableAdapter.FillFiltros(DataSet.UDV_Vent_NotaCreditoPorFecha, clte_Identificacion_1, FechaDesde, FechaHasta, clte_EsPersonaNatural);
                    Reporte.SetDataSource(DataSet);
                    Reporte.SetParameterValue("USUARIO", UsuarioName.usu_Nombres + " " + UsuarioName.usu_Apellidos);
                    stream = Reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);

                    Reporte.Close();
                    Reporte.Dispose();
                    fileName = "NotasCreditoEntreFechas.pdf";
                }
                else if (iTipoReporte == Helpers.rptAnalisisMora)
                {
                    AnalisisDeMora Reporte = new AnalisisDeMora();
                    Reportes DataSet = new Reportes();
                    var TableAdapter = new UDV_Vent_AnalisisDeMoraTableAdapter();
                    TableAdapter.FillFiltros(DataSet.UDV_Vent_AnalisisDeMora, clte_Identificacion_);
                    Reporte.SetDataSource(DataSet);
                    Reporte.SetParameterValue("usuario", UsuarioName.usu_Nombres + " " + UsuarioName.usu_Apellidos);
                    stream = Reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);

                    Reporte.Close();
                    Reporte.Dispose();
                    fileName = "AnalisisDeMora.pdf";
                }
                else if (iTipoReporte == Helpers.rptSolicitudesCreditoAprobar)
                {
                    SolicitudesCreditosPorAprobar Reporte = new SolicitudesCreditosPorAprobar();
                    Reportes DataSet = new Reportes();
                    var TableAdapter = new UDV_Vent_SolicitudCredito_SolicitudesPorAprobarReporteTableAdapter();
                    TableAdapter.FillFiltros(DataSet.UDV_Vent_SolicitudCredito_SolicitudesPorAprobarReporte, clte_Identificacion_1, FechaDesde, FechaHasta);
                    Reporte.SetDataSource(DataSet);
                    stream = Reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);

                    Reporte.Close();
                    Reporte.Dispose();
                    fileName = "SolicitudesCreditoAprobar.pdf";
                }
                llenarListas();
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                return File(stream, "application/pdf");
            }
            catch (Exception Ex)
            {
                llenarListas();
                Function.InsertBitacoraErrores("Reportes/Index", Ex.Message.ToString(), "Index");
                ModelState.AddModelError("", "No se pudo imprimir, contacte al administrador.");
                return View(Objeto);
            }
        }

        private void llenarListas()
        {
            var list = db.SDP_Acce_GetReportes().ToList();
            var listCliente = db.tbCliente.Select(s => new
            {
                clte_Identificacion = s.clte_Identificacion,
                clte_Nombres = s.clte_Nombres + " " + s.clte_Apellidos,
                clte_EsActivo = s.clte_EsActivo
            }).Where(x => x.clte_EsActivo == true).ToList();
            var listSucursal = db.tbSucursal.ToList();
            var listCajeros = db.tbUsuario.Select(s => new
            {
                usu_Id = s.usu_Id,
                usu_Nombres = s.usu_Nombres + " " + s.usu_Apellidos
            }).ToList();
            ViewBag.obj_Id = new SelectList(list, "obj_Id", "obj_Pantalla");
            ViewBag.clte_Identificacion = new SelectList(listCliente, "clte_Identificacion", "clte_Nombres");
            ViewBag.suc_Id = new SelectList(listSucursal, "suc_Id", "suc_Descripcion");
            ViewBag.fact_UsuarioCrea = new SelectList(listCajeros, "usu_Id", "usu_Nombres");
            ViewBag.clte_Identificacion_ = new SelectList(listCliente, "clte_Identificacion", "clte_Nombres");
            ViewBag.clte_Identificacion_1 = new SelectList(listCliente, "clte_Identificacion", "clte_Nombres");
        }

        [HttpPost]
        public JsonResult GetCajas(int suc_Id)
        {
            var list = db.tbCaja.Where(s=> s.suc_Id== suc_Id).ToList();
            var listCliente = db.tbCaja.Select(s => new
            {
                cja_Id = s.cja_Id,
                cja_Descripcion = s.cja_Descripcion,
                suc_Id = s.suc_Id
            }).Where(x => x.suc_Id == suc_Id).ToList();
            return Json(listCliente, JsonRequestBehavior.AllowGet);
        }
    }
}