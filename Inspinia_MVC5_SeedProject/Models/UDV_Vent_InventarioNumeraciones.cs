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
    
    public partial class UDV_Vent_InventarioNumeraciones
    {
        public int suc_Id { get; set; }
        public string suc_Descripcion { get; set; }
        public string suc_Telefono { get; set; }
        public string pemi_NumeroCAI { get; set; }
        public string dfisc_Descripcion { get; set; }
        public string pemid_RangoInicio { get; set; }
        public string pemid_RangoFinal { get; set; }
        public string pemid_NumeroActual { get; set; }
        public System.DateTime pemid_FechaLimite { get; set; }
        public string Estado { get; set; }
        public string DiasDisponibles { get; set; }
        public Nullable<int> NumeracionesDisponibles { get; set; }
    }
}