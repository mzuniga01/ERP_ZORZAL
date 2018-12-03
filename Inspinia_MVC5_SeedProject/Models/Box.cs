using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(BoxMetaData))]

    public partial class tbBox
    {

       
    }

    public class BoxMetaData
    {
        [Display(Name = "Codigo de Caja")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string box_Codigo { get; set; }

        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string box_Descripcion { get; set; }

        [Display(Name = "Creada Por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int box_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creaciom")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime box_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<int> box_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<System.DateTime> box_FechaModifica { get; set; }

    }
}