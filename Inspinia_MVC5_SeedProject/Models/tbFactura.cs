//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inspinia_MVC5_SeedProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbFactura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbFactura()
        {
            this.tbDevolucion = new HashSet<tbDevolucion>();
            this.tbDevolucion1 = new HashSet<tbDevolucion>();
            this.tbPago = new HashSet<tbPago>();
        }
    
        public string fact_Codigo { get; set; }
        public System.DateTime fact_Fecha { get; set; }
        public byte esfac_Id { get; set; }
        public int pago_Id { get; set; }
        public string cja_Codigo { get; set; }
        public string sucur_Codigo { get; set; }
        public int clte_id { get; set; }
        public string peCodigo { get; set; }
        public string fact_UsuarioCrea { get; set; }
        public System.DateTime fact__FechaCrea { get; set; }
        public string fact__UsuarioModifica { get; set; }
        public Nullable<System.DateTime> fact_FechaModica { get; set; }
    
        public virtual tbCaja tbCaja { get; set; }
        public virtual tbCliente tbCliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbDevolucion> tbDevolucion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbDevolucion> tbDevolucion1 { get; set; }
        public virtual tbEstadoFactura tbEstadoFactura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPago> tbPago { get; set; }
        public virtual tbPago tbPago1 { get; set; }
        public virtual tbSucursal tbSucursal { get; set; }
    }
}