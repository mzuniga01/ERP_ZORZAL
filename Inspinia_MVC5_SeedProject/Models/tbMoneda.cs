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
    
    public partial class tbMoneda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbMoneda()
        {
            this.tbCuentasBanco = new HashSet<tbCuentasBanco>();
            this.tbDenominacion = new HashSet<tbDenominacion>();
            this.tbParametro = new HashSet<tbParametro>();
            this.tbSolicitudEfectivo = new HashSet<tbSolicitudEfectivo>();
        }
    
        public short mnda_Id { get; set; }
        public string mnda_Abreviatura { get; set; }
        public string mnda_Nombre { get; set; }
        public int mnda_UsuarioCrea { get; set; }
        public System.DateTime mnda_FechaCrea { get; set; }
        public Nullable<int> mnda_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> mnda_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCuentasBanco> tbCuentasBanco { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbDenominacion> tbDenominacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbParametro> tbParametro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSolicitudEfectivo> tbSolicitudEfectivo { get; set; }
    }
}
