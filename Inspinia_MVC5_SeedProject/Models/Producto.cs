using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(ProductoMetaData))]
    public partial class tbProducto
    {
    }
    public class ProductoMetaData
    {
        [Display(Name = "Codigo Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Codigo { get; set; }
        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Descripcion { get; set; }
        [Display(Name = "Marca")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Marca { get; set; }
        [Display(Name = "Modelo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Modelo { get; set; }
        [Display(Name = "Talla")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Talla { get; set; }
        [Display(Name = "Color")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Color { get; set; }
        [Display(Name = "Categoria")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pscat_Id { get; set; }
        [Display(Name = "Unidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int uni_Id { get; set; }
        public int prod_UsuarioCrea { get; set; }
        public System.DateTime prod_FechaCrea { get; set; }
        public Nullable<int> prod_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> prod_FechaModifica { get; set; }
    }
}