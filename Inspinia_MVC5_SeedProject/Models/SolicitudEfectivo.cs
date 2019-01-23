using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(SolicitudEfectivoMetaData))]
    public partial class tbSolicitudEfectivo
    {
       
    }

    public class SolicitudEfectivoMetaData
    {

        [Display(Name = "Número Solicitud")]
        
        public int solef_Id { get; set; }
        [Display(Name = "Movimiento Caja")]
        
        public int mocja_Id { get; set; }
        [Display(Name = "¿Es Apertura?")]
       
        public bool solef_EsApertura { get; set; }
        [Display(Name = "Fecha de Entrega ")]
        
        public Nullable<System.DateTime> solef_FechaEntrega { get; set; }
        [Display(Name = "Usuario Entrega")]
        
        public Nullable<int> solef_UsuarioEntrega { get; set; }
        [Display(Name = "Moneda")]
      
        public short mnda_Id { get; set; }
        [Display(Name = "¿Es Anulada?")]
        
        public bool solef_EsAnulada { get; set; }
        [Display(Name = "Usuario Creación")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]

        public string RazonAnulado { get; set; }
        [Display(Name = "Razon Anulada")]

        public int solef_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creación")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime solef_FechaCrea { get; set; }
        [Display(Name = "Usuario Modificación")]
        public Nullable<int> solef_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modificación")]
        public Nullable<System.DateTime> solef_FechaModifica { get; set; }

        //[Display(Name = "Usuario")]
        //public virtual tbUsuario tbUsuario { get; set; }
        //[Display(Name = "Usuario1")]
        //public virtual tbUsuario tbUsuario1 { get; set; }
        //[Display(Name = "Usuario3")]
        //public virtual tbUsuario tbUsuario2 { get; set; }
        //public virtual tbMoneda tbMoneda { get; set; }
        //public virtual tbMovimientoCaja tbMovimientoCaja { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tbSolicitudEfectivoDetalle> tbSolicitudEfectivoDetalle { get; set; }
    }
}