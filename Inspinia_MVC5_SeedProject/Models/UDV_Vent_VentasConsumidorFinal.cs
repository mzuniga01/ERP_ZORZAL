//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Vent_VentasConsumidorFinal
    {
        public int clte_Id { get; set; }
        public System.DateTime fact_Fecha { get; set; }
        public string fact_Codigo { get; set; }
        public string Sucursal { get; set; }
        public short cja_Id { get; set; }
        public string TipoDescuento { get; set; }
        public Nullable<decimal> MontoFactura { get; set; }
        public Nullable<decimal> MontoDescuento { get; set; }
        public Nullable<decimal> MontoImpuesto { get; set; }
        public Nullable<decimal> Total { get; set; }
    }
}
