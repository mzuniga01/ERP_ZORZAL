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
    
    public partial class SDP_Acce_GetRoles_Result
    {
        public int rol_Id { get; set; }
        public string rol_Descripcion { get; set; }
        public int rol_UsuarioCrea { get; set; }
        public System.DateTime rol_FechaCrea { get; set; }
        public Nullable<int> rol_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> rol_FechaModifica { get; set; }
        public Nullable<bool> rol_Estado { get; set; }
    }
}
