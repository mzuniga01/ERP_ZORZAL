using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(UnidadesMedidaMetaData))]


    public partial class tbUnidadesMedida
    {

    }

    public class UnidadesMedidaMetaData
    {
        [Display(Name = "ID Unidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int uni_Id { get; set; }

        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string uni_Descripcion { get; set; }

        [Display(Name = "Abreviacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string uni_Abreviacion { get; set; }

        [Display(Name = "Creado por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string uni_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime uni_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string uni_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<System.DateTime> uni_FechaModifica { get; set; }
    }

}