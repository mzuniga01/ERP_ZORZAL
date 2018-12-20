using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(ProductosCategoriasmetadata))]
    public partial class tbProductoCategoria
    {
    }

    public class ProductosCategoriasmetadata
    {
        [Display(Name = "Número Categoría")]
        public int pcat_Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string pcat_Nombre { get; set; }

        [Display(Name = "Estado")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public bool pcat_Estado { get; set; }

    }
}