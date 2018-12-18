using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(SolicitudEfectivoDetalleMetaData))]
    public partial class tbSolicitudEfectivoDetalle
    {
        
    }
    public class SolicitudEfectivoDetalleMetaData
    {
        [Display(Name = "Número")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int soled_Id { get; set; }
        [Display(Name = "Número Solictud")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int solef_Id { get; set; }
        [Display(Name = "Denominacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short deno_Id { get; set; }
        [Display(Name = "Cantidad Solicitada")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short soled_CantidadSolicitada { get; set; }
        [Display(Name = "Cantidad Entregada")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short soled_CantidadEntregada { get; set; }
        [Display(Name = "Monto Entregado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal soled_MontoEntregado { get; set; }
        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int soled_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime soled_FechaCrea { get; set; }
        [Display(Name = "Usuario Modificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<int> soled_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<System.DateTime> soled_FechaModifica { get; set; }

       
    
}
}