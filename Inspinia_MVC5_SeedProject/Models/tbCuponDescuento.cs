//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP_ZORZAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbCuponDescuento
    {
        public int cd_IDCuponDescuento { get; set; }
        public string sucur_Codigo { get; set; }
        public System.DateTime cd_FechaEmision { get; set; }
        public System.DateTime cd_FechaVencimiento { get; set; }
        public Nullable<decimal> cd_PorcentajeDescuento { get; set; }
        public Nullable<decimal> cd_MontoDescuento { get; set; }
        public string cd_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> cd_FechaCrea { get; set; }
        public string cd_UsuarioModifa { get; set; }
        public Nullable<System.DateTime> cd_FechaModifa { get; set; }
    
        public virtual tbSucursal tbSucursal { get; set; }
    }
}
