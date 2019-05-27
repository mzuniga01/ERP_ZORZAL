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

        ///ESTADO ENTRADA
        public const int EntradaAnulada = 1;

        public const int EntradaEmitida = 2;
        public const int EntradaInactivada = 4;
        public const int EntradaAplicada = 1;

        //estado movimiento
        public const int EntradaEstadoAnulada = 3;

        //Salida
        public const int sal_Aplicada = 1;

        public const int sal_Emitida = 2;
        public const int sal_Anulada = 3;
        public const int sal_Inactiva = 4;
        public const int sal_Activa = 5;

        public const int sal_Impresa = 6;
        public const bool sal_EsAnulada = true;
        public const int sal_Prestamo = 1;

        public const int sal_Venta = 2;
        public const int sal_Devolucion = 3;
        public const int esfac_Pagada = 3;
        public const int esfac_PagoPendiente = 4;
        public const bool fact_EsAnulada = true;
        public const string sal_Estado = "Emitida";


        ///ESTADO OBJETO
        public const bool ObjetoActivo = true;
        public const bool ObjetoInactivo = false;

        //Estado Rol
        public const bool RolActivo = true;//1
        public const bool RolInactivo = false;//0

        //Inventario Físico
        public const int InvFisicoActivo = 1;
        public const int InvFisicoConciliado = 2;
        public const int InvFisicoReconteo = 3;

        //BODEGA
        public const int BodegaActivo = 1;
        public const int BodegaInactivo = 0;

        //Empleado
        public const bool EmpleadoActivo = true;//1
        public const bool EmpleadoInactivo = false;//0

        //Estados Producto
        public const bool ProductoActivo = true;
        public const bool ProductoInactivo = false;

        //Estado Categoria
        public const int CategoriaActivo = 1;
        public const int CategoriaInactivo = 2;

        //Estado Subcategoria
        public const int SubcategoriaActivo = 1;
        public const int SubcategoriaInactivo = 2;

        //Estado Box
        public const int vbox_Abrierta = 1;
        public const int vbox_Cerrada = 2;
        public const string box_Abrierta = "Abrierta";
        public const string box_Cerrada = "Cerrada";
    }
}