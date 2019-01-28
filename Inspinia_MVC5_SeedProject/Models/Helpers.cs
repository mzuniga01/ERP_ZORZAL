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

        //Estado Bodega
        public const int BodegaActivo = 1;
        public const int BodegaInactivo = 0;

        //Estado Empleado
        public const bool EmpleadoActivo = true;
        public const bool EmpleadoInactivo = false;

        //Estado Entrada
        public const int EntradaEmitida = 2;
        public const int EntradaInactivada = 36;
        public const int EntradaAplicada = 1;
        public const int EntradaAnulada = 1;

        //Estado Objeto
        public const bool ObjetoActivo = true;
        public const bool ObjetoInactivo = false;

        //Estado Rol
        public const bool RolActivo = true;//1
        public const bool RolInactivo = false;//0

        //Estado Subcategoria
        public const int SubcategoriaActivo = 1;
        public const int SubcategoriaInactivo = 2;

        //Inventario Físico
        public const int InvFisicoActivo = 1;
        public const int InvFisicoConciliado = 2;

        //Estado Categoria
        public const int CategoriaActivo = 1;
        public const int CategoriaInactivo = 2;
    }
}