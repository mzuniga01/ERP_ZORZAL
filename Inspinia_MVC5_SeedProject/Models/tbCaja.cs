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
    
    public partial class tbCaja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbCaja()
        {
            this.tbDevolucion = new HashSet<tbDevolucion>();
            this.tbFactura = new HashSet<tbFactura>();
            this.tbMovimientoCaja = new HashSet<tbMovimientoCaja>();
            this.tbNotaCredito = new HashSet<tbNotaCredito>();
        }
    
        public short cja_Id { get; set; }
        public string cja_Descripcion { get; set; }
        public int suc_Id { get; set; }
        public int cja_UsuarioCrea { get; set; }
        public System.DateTime cja_FechaCrea { get; set; }
        public Nullable<int> cja_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> cja_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbDevolucion> tbDevolucion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbFactura> tbFactura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbMovimientoCaja> tbMovimientoCaja { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbNotaCredito> tbNotaCredito { get; set; }
        public virtual tbSucursal tbSucursal { get; set; }
    }
}
