using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(BodegaDetalleMetaData))]
    public partial class tbBodegaDetalle
    {
    }


    public class BodegaDetalleMetaData
    {
        [Display(Name = "Número Detalle")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int bodd_Id { get; set; }

        [Display(Name = "Producto")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Codigo { get; set; }

        [Display(Name = "Número Bodega")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int bod_Id { get; set; }

        [Display(Name = "Cant. Mínima")]
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal bodd_CantidadMinima { get; set; }

        [Display(Name = "Cant. Máxima")]
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal bodd_CantidadMaxima { get; set; }

        [Display(Name = "Cant. Existente")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal bodd_CantidadExistente { get; set; }

        [Display(Name = "Punto Reorden")]
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal bodd_PuntoReorden { get; set; }
        
        [Display(Name = "Costo")]
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal bodd_Costo { get; set; }

        [Display(Name = " Cost. Promedio ")]
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal bodd_CostoPromedio { get; set; }

        [Display(Name = "Bodega")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public virtual tbBodega tbBodega { get; set; }

        [Display(Name = "Producto")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public virtual tbProducto tbProducto { get; set; }
    }
}