using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class Helpers
    {
        public const bool AnuladoFactura = true;
        public const bool EsImpreso = true;
        public const int EstadoImpreso = 2;
        public const int RTN = 3;
        public const int ID = 2;
        public const int rol_Id = 2;

        //Estados Cliente 
        public const bool ClienteActivo = true;
        public const bool ClienteCredito = false;
        public const bool ClienteInactivo = false;
        public const bool ClienteExonerado = false;

        //Lista Precios
        public const bool ListaPrecioActivo = true;
        public const bool ListaPrecioInactivo = false;


        //Estados Pedido
        public const int Pendiente = 1;
        public const int Facturado = 2;


        //Estado Solicitud Credito
        public const int SolicitudPendiente = 1;
        public const int SolicitudAprobado = 2;
        public const int SolicitudDenegado = 3;

        //Estados Exoneración 
        public const bool ExoneracionActiva = true;
        public const bool ExoneracionInactiva = false;

        //Reportes
        public const int rptVentasFechas = 195;
        public const int rptVentasCajaFechas = 196;
        public const int rptFacturasPendientesPago = 197;
        public const int rptVentasConsumidorFinal = 198;
        public const int rptNotasCreditoEntreFechas = 199;
        public const int rptAnalisisMora = 200;
        public const int rptSolicitudesCreditoAprobar = 202;
        public const int rptCuponesDescuentoFechas = 205;
        
    }
}