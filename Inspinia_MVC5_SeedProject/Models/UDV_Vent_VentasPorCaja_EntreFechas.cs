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
    
    public partial class UDV_Vent_VentasPorCaja_EntreFechas
    {
        public System.DateTime fact_Fecha { get; set; }
        public string suc_Descripcion { get; set; }
        public string cja_Descripcion { get; set; }
        public string Cajero { get; set; }
        public Nullable<decimal> Total_facturado { get; set; }
        public decimal pago_TotalPago { get; set; }
        public short cja_Id { get; set; }
        public int fact_UsuarioCrea { get; set; }
        public int suc_Id { get; set; }
    }
}
