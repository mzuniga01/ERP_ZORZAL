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
    
    public partial class tbTiposSalida
    {
        public byte tsal_Id { get; set; }
        public string tsal_Descripcion { get; set; }
        public int tsal_UsuarioCrea { get; set; }
        public System.DateTime tsal_FechaCrea { get; set; }
        public Nullable<int> tsal_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tsal_FechaModifica { get; set; }
    }
}
