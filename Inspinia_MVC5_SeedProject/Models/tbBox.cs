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
    
    public partial class tbBox
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbBox()
        {
            this.tbSalidaDetalle = new HashSet<tbSalidaDetalle>();
        }
    
        public string box_Codigo { get; set; }
        public string box_Descripcion { get; set; }
        public bool box_Estado { get; set; }
        public int box_UsuarioCrea { get; set; }
        public System.DateTime box_FechaCrea { get; set; }
        public Nullable<int> box_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> box_FechaModifica { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSalidaDetalle> tbSalidaDetalle { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
    }
}
