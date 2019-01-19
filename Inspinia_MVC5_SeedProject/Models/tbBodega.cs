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
    
    public partial class tbBodega
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbBodega()
        {
            this.tbBodegaDetalle = new HashSet<tbBodegaDetalle>();
            this.tbEntrada = new HashSet<tbEntrada>();
            this.tbEntrada1 = new HashSet<tbEntrada>();
            this.tbInventarioFisico = new HashSet<tbInventarioFisico>();
            this.tbSalida = new HashSet<tbSalida>();
            this.tbSalida1 = new HashSet<tbSalida>();
            this.tbSucursal = new HashSet<tbSucursal>();
        }
    
        public int bod_Id { get; set; }
        public string bod_Nombre { get; set; }
        public short bod_ResponsableBodega { get; set; }
        public string bod_Direccion { get; set; }
        public string bod_Correo { get; set; }
        public string bod_Telefono { get; set; }
        public string mun_Codigo { get; set; }
        public byte bod_EsActiva { get; set; }
        public int bod_UsuarioCrea { get; set; }
        public System.DateTime bod_FechaCrea { get; set; }
        public Nullable<int> bod_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> bod_FechaModifica { get; set; }
    
        public virtual tbEmpleado tbEmpleado { get; set; }
        public virtual tbMunicipio tbMunicipio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbBodegaDetalle> tbBodegaDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEntrada> tbEntrada { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEntrada> tbEntrada1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbInventarioFisico> tbInventarioFisico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSalida> tbSalida { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSalida> tbSalida1 { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSucursal> tbSucursal { get; set; }
    }
}
