using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(ProductosCategoriasmetadata))]
    public partial class tbProductoCategoria
    {
    }

    public class ProductosCategoriasmetadata
    {
        [Display(Name = "ID Categoría")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pcat_Id { get; set; }

        [Display(Name = "Nombre Categoría")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string pcat_Nombre { get; set; }

        [Display(Name = "Creado Por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pcat_UsuarioCrea { get; set; }

        [Display(Name = "Creado En")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime pcat_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<int> pcat_UsuarioModifica { get; set; }

        [Display(Name = "Modificado En")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<System.DateTime> pcat_FechaModifica { get; set; }

        public virtual ICollection<tbProductoSubcategoria> tbProductoSubcategoria { get; set; }
    }
}