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
    
    public partial class UDV_Vent_VentasExoneradas
    {
        public Nullable<System.DateTime> Fecha_factura { get; set; }
        public string suc_Descripcion { get; set; }
        public string prod_Codigo { get; set; }
        public string clte_Identificacion { get; set; }
        public string clte_NombreComercial { get; set; }
        public string fact_Codigo { get; set; }
        public string prod_Descripcion { get; set; }
        public Nullable<decimal> Monto_Facturado { get; set; }
        public Nullable<decimal> Impuesto_Exonerado { get; set; }
    }
}
