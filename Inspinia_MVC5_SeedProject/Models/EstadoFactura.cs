using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(_EstadoFacturaMetaData))]
    public partial class tbEstadoFactura
    {

    }
    public class _EstadoFacturaMetaData
    {
        [Display(Name = "ID Estado Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte esfac_Id { get; set; }
        [Display(Name = "Descripión")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string esfac_Descripcion { get; set; }
        public string esfac_UsuarioCrea { get; set; }
        public string esfac_UsuarioModifico { get; set; }
        public System.DateTime esfac_FechaAgrego { get; set; }
        public Nullable<System.DateTime> esfac_FechaModifico { get; set; }
    }

}