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
    
    public partial class tbFactura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbFactura()
        {
            this.tbSalida = new HashSet<tbSalida>();
            this.tbDevolucion = new HashSet<tbDevolucion>();
            this.tbFacturaHistorica = new HashSet<tbFacturaHistorica>();
            this.tbPago = new HashSet<tbPago>();
            this.tbPedido = new HashSet<tbPedido>();
            this.tbFacturaDetalle = new HashSet<tbFacturaDetalle>();
        }
    
        public long fact_Id { get; set; }
        public string fact_Codigo { get; set; }
        public System.DateTime fact_Fecha { get; set; }
        public byte esfac_Id { get; set; }
        public short cja_Id { get; set; }
        public int suc_Id { get; set; }
        public int clte_Id { get; set; }
        public string pemi_NumeroCAI { get; set; }
        public bool fact_AlCredito { get; set; }
        public int fact_DiasCredito { get; set; }
        public decimal fact_PorcentajeDescuento { get; set; }
        public string fact_Vendedor { get; set; }
        public string clte_Identificacion { get; set; }
        public string clte_Nombres { get; set; }
        public string fact_IdentidadTE { get; set; }
        public string fact_NombresTE { get; set; }
        public Nullable<System.DateTime> fact_FechaNacimientoTE { get; set; }
        public Nullable<int> fact_UsuarioAutoriza { get; set; }
        public Nullable<System.DateTime> fact_FechaAutoriza { get; set; }
        public bool fact_EsAnulada { get; set; }
        public int fact_UsuarioCrea { get; set; }
        public System.DateTime fact_FechaCrea { get; set; }
        public Nullable<int> fact_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> fact_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbUsuario tbUsuario2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSalida> tbSalida { get; set; }
        public virtual tbCaja tbCaja { get; set; }
        public virtual tbCliente tbCliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbDevolucion> tbDevolucion { get; set; }
        public virtual tbEstadoFactura tbEstadoFactura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbFacturaHistorica> tbFacturaHistorica { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPago> tbPago { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPedido> tbPedido { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbFacturaDetalle> tbFacturaDetalle { get; set; }
        public virtual tbSucursal tbSucursal { get; set; }
    }
}
