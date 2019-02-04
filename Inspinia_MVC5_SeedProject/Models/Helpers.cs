using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class Helpers
    {
        public const bool AnuladoFactura = true;
        public const int RTN = 3;
        public const int ID = 2;

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

        ///ESTADO ENTRADA
        public const int EntradaAnulada = 1;
        public const int EntradaEmitida = 2;
        public const int EntradaInactivada = 67;
        public const int EntradaAplicada = 1;
                //estado movimiento
        public const int EntradaEstadoAnulada = 24;

        ///ESTADO OBJETO
        public const bool ObjetoActivo = true;
        public const bool ObjetoInactivo = false;

        //Estado Rol
        public const bool RolActivo = true;//1
        public const bool RolInactivo = false;//0

        //Inventario Físico
        public const int InvFisicoActivo = 1;
        public const int InvFisicoConciliado = 2;

        //BODEGA
        public const int BodegaActivo = 1;
        public const int BodegaInactivo = 0;

        //Empleado
        public const bool EmpleadoActivo = true;//1
        public const bool EmpleadoInactivo = false;//0

        //Estados Producto
        public const bool ProductoActivo = true;
        public const bool ProductoInactivo = false;

    }
}