//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDP_Vent_SolicituEfectivo_Select
    {
        public int IdSolicitud { get; set; }
        public System.DateTime FechaSolicitud { get; set; }
        public string Sucursal { get; set; }
        public string Caja { get; set; }
        public string Moneda { get; set; }
        public Nullable<decimal> MontoSolicitado { get; set; }
        public Nullable<decimal> MontoEntregado { get; set; }
        public bool Apertura { get; set; }
        public bool Anulada { get; set; }
    }
}
