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
    
    public partial class tbEstadoSolicitudCredito
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbEstadoSolicitudCredito()
        {
            this.tbSolicitudCredito = new HashSet<tbSolicitudCredito>();
        }
    
        public byte escre_Id { get; set; }
        public string escre_Descripcion { get; set; }
        public int escre_UsuarioCrea { get; set; }
        public Nullable<int> escre_UsuarioModifico { get; set; }
        public System.DateTime escre_FechaAgrego { get; set; }
        public Nullable<System.DateTime> escre_FechaModifico { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSolicitudCredito> tbSolicitudCredito { get; set; }
    }
}
