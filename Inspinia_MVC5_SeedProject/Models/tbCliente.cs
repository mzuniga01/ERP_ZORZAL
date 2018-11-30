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
    
    public partial class tbCliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbCliente()
        {
            this.tbPedido = new HashSet<tbPedido>();
            this.tbNotaCredito = new HashSet<tbNotaCredito>();
            this.tbSolicitudCredito = new HashSet<tbSolicitudCredito>();
        }
    
        public int clte_Id { get; set; }
        public string clte_RTN_Identidad_Pasaporte { get; set; }
        public bool clte_EsPersonaNatural { get; set; }
        public string clte_Nombres { get; set; }
        public string clte_Apellidos { get; set; }
        public System.DateTime clte_FechaNacimiento { get; set; }
        public string clte_Nacionalidad { get; set; }
        public string clte_Sexo { get; set; }
        public string clte_Telefono { get; set; }
        public string clte_NombreComercial { get; set; }
        public string clte_RazonSocial { get; set; }
        public string clte_ContactoNombre { get; set; }
        public string clte_ContactoEmail { get; set; }
        public string clte_ContactoTelefono { get; set; }
        public Nullable<System.DateTime> clte_FechaConstitucion { get; set; }
        public string mun_Codigo { get; set; }
        public string clte_Direccion { get; set; }
        public string clte_CorreoElectronico { get; set; }
        public string clte_EsActivo { get; set; }
        public string clte_RazonInactivo { get; set; }
        public bool clte_ConCredito { get; set; }
        public bool clte_EsMinorista { get; set; }
        public string clte_Observaciones_ { get; set; }
        public int clte_UsuarioCrea { get; set; }
        public System.DateTime clte_FechaCrea { get; set; }
        public Nullable<int> clte_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> clte_FechaModifica { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPedido> tbPedido { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbNotaCredito> tbNotaCredito { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSolicitudCredito> tbSolicitudCredito { get; set; }
    }
}
