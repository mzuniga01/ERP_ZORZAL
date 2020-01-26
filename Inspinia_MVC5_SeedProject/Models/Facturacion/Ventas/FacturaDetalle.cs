using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(FacturaDetalleMetaData))]

    public partial class tbFacturaDetalle
    {
       
    }

    public class FacturaDetalleMetaData
    {
        public long fact_Id { get; set; }
        public short factd_Id { get; set; }
        [Display(Name = "Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string prod_Codigo { get; set; }
        [Display(Name = "Cantidad")]
        [DisplayFormat(DataFormatString = "", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        [RegularExpression("[^0-9]", ErrorMessage = "UPRN must be numeric")]
        public decimal factd_Cantidad { get; set; }
        [Display(Name = "Monto Descuento")]
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        public decimal factd_MontoDescuento { get; set; }
        [Display(Name = "Porcentaje Descuento")]
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal factd_PorcentajeDescuento { get; set; }
        [Display(Name = "Impuesto")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]        
        public decimal factd_Impuesto { get; set; }
        [Display(Name = "Precio Unitario")]
        public decimal factd_PrecioUnitario { get; set; }        
    }
}