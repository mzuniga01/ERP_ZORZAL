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
    
    public partial class UDP_Login_Result
    {
        public int usu_Id { get; set; }
        public string usu_NombreUsuario { get; set; }
        public byte[] usu_Password { get; set; }
        public string usu_Nombres { get; set; }
        public string usu_Apellidos { get; set; }
        public string usu_Correo { get; set; }
        public bool usu_EsActivo { get; set; }
        public string usu_RazonInactivo { get; set; }
        public bool usu_EsAdministrador { get; set; }
        public byte usu_SesionesValidas { get; set; }
    }
}
