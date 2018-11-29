using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(_PuntoEmisionMetaData))]
    public partial class tbPuntoEmision
    {

    }
    public class _PuntoEmisionMetaData
    {
        [Display(Name = "Codigo Pedido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string pe_Codigo { get; set; }
        [Display(Name = "Codigo Sucursal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string sucur_Codigo { get; set; }
        [Display(Name = "Numero CAI")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string pe_NumeroCAI { get; set; }
        [Display(Name = "Usuario Creacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string pe_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public Nullable<System.DateTime> pe_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string pe_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public Nullable<System.DateTime> pe_FechaModifica { get; set; }
    }
}