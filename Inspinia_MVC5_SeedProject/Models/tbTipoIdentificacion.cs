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
    
    public partial class tbTipoIdentificacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTipoIdentificacion()
        {
            this.tbEmpleado = new HashSet<tbEmpleado>();
            this.tbIdentificacionCliente = new HashSet<tbIdentificacionCliente>();
            this.tbCliente = new HashSet<tbCliente>();
        }
    
        public byte tpi_Id { get; set; }
        public string tpi_Descripcion { get; set; }
        public int tpi_UsuarioCrea { get; set; }
        public System.DateTime tpi_FechaCrea { get; set; }
        public Nullable<int> tpi_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tpi_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEmpleado> tbEmpleado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbIdentificacionCliente> tbIdentificacionCliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente> tbCliente { get; set; }
    }
}
