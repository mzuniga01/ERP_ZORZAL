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
    
    public partial class tbUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbUsuario()
        {
            this.tbBodega = new HashSet<tbBodega>();
            this.tbRolesUsuario1 = new HashSet<tbRolesUsuario>();
        }
    
        public int usu_Id { get; set; }
        public string usu_NombreUsuario { get; set; }
        public int rolusu_Id { get; set; }
        public string usu_Password { get; set; }
        public string usu_Nombre { get; set; }
        public string usu_Apellido { get; set; }
        public string usu_Correo { get; set; }
        public bool edo_IdEstado { get; set; }
        public string usu_RazonEstado { get; set; }
        public bool usu_EsAdministrador { get; set; }
    
        public virtual tbRolesUsuario tbRolesUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbBodega> tbBodega { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbRolesUsuario> tbRolesUsuario1 { get; set; }
    }
}
