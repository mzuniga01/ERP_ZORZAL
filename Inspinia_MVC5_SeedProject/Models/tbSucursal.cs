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
    
    public partial class tbSucursal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbSucursal()
        {
            this.tbCaja1 = new HashSet<tbCaja1>();
            this.tbCuponDescuento = new HashSet<tbCuponDescuento>();
            this.tbPedido = new HashSet<tbPedido>();
            this.tbPuntoEmision1 = new HashSet<tbPuntoEmision>();
        }
    
        public short suc_Id { get; set; }
        public string mun_Codigo { get; set; }
        public int bod_Id { get; set; }
        public int pemi_Id { get; set; }
        public string suc_Correo { get; set; }
        public string suc_Direccion { get; set; }
        public string suc_Telefono { get; set; }
        public int suc_UsuarioCrea { get; set; }
        public System.DateTime suc_FechaCrea { get; set; }
        public Nullable<int> suc_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> suc_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbMunicipio tbMunicipio { get; set; }
        public virtual tbBodega tbBodega { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCaja1> tbCaja1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCuponDescuento> tbCuponDescuento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPedido> tbPedido { get; set; }
        public virtual tbPuntoEmision tbPuntoEmision { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPuntoEmision> tbPuntoEmision1 { get; set; }
    }
}
