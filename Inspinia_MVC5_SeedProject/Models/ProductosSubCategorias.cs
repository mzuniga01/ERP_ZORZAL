using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(ProductosSubCategoriasMetada))]

    public partial class tbProductoSubcategoria
    {

    }

    public class ProductosSubCategoriasMetada
    {
        [Display(Name = "ID SubCategoria")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pscat_Id { get; set; }

        [Display(Name = "Descripción SubCategoría")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string pscat_Descripcion { get; set; }

        [Display(Name = "ID Categoria")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pcat_Id { get; set; }

        [Display(Name = "Estado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte pscat_EsActiva { get; set; }

        [Display(Name = "Creado Por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pscat_UsuarioCrea { get; set; }

        [Display(Name = "Creado En")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime pscat_FechaCrea { get; set; }

        [Display(Name = "Modificado En")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<int> pscat_UsuarioModifica { get; set; }

        [Display(Name = "Modificado El")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<System.DateTime> pscat_FechaModifica { get; set; }
    }
}