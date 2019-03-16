using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(BoxMetaData))]

    public partial class tbBox
    {

       
    }

    public class BoxMetaData
    {
        [Display(Name = "Código de Caja")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string box_Codigo { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string box_Descripcion { get; set; }

        [Display(Name = "Creada Por")]
        public int box_UsuarioCrea { get; set; }

        [Display(Name = "Creado El")]
        public System.DateTime box_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public Nullable<int> box_UsuarioModifica { get; set; }

        [Display(Name = "Modificado El")]
        public Nullable<System.DateTime> box_FechaModifica { get; set; }

    }
}