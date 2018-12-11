using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(ProductosSubCategoriasMetada))]

    public partial class tbProductoSubcategoria
    {

    }

    public class ProductosSubCategoriasMetada
    {
        [Display(Name = "Número SubCategoria")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pscat_Id { get; set; }

        [Display(Name = "SubCategoría")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string pscat_Descripcion { get; set; }

        [Display(Name = "Número Categoria")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pcat_Id { get; set; }

        [Display(Name = "Estado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte pscat_EsActiva { get; set; }

        //[Display(Name = "Creado Por")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //public int pscat_UsuarioCrea { get; set; }

        //[Display(Name = "Creado El")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //public System.DateTime pscat_FechaCrea { get; set; }

        //[Display(Name = "Modificado El")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //public Nullable<int> pscat_UsuarioModifica { get; set; }

        //[Display(Name = "Modificado El")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]

        public virtual tbEstadoMovimiento tbEstadoMovimiento { get; set; }
        public virtual tbProductoCategoria tbProductoCategoria { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbProducto> tbProducto { get; set; }
    }
}