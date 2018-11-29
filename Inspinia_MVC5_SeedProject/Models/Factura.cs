using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    [MetadataType(typeof(_FacturaMetaData))]
    public partial class tbFactura
    {
        
    }
    public class _FacturaMetaData
    {
        [Display(Name = "Código Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string fact_Codigo { get; set; }
        [Display(Name = "Fecha Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime fact_Fecha { get; set; }
    }
}