using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(cDetalleDevolucionMetaData))]
    public partial class tbDevolucionDetalle
    {

    }
    public class cDetalleDevolucionMetaData
    {
        [Display(Name = "Codigo Detalle Devolución")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string ddv_Codigo { get; set; }
        [Display(Name = "Codigo Devolución")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string dev_Codigo { get; set; }
        [Display(Name = "Codigo Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Codigo { get; set; }
        [Display(Name = "Cantidad Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int ddv_CantidadProducto { get; set; }
        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string ddv_Descripcion { get; set; }
        public string ddv_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> ddv_FechaCrea { get; set; }
        public string ddv_UsuarioModifa { get; set; }
        public Nullable<System.DateTime> ddv_FechaModifa { get; set; }
    }

   
}