using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    [MetadataType(typeof(cExoneracionMetaData))]
     public partial class tbExoneracion
    {
    }
    public class cExoneracionMetaData
    {
        [Display(Name = "Codigo Exoneración")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string exo_Codigo { get; set; }

        [Display(Name = "Documento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string exo_Documento { get; set; }

        [Display(Name = "Exoneración Activa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string exo_ExoneracionActiva { get; set; }

        [Display(Name = "Fecha Inicial Vigencia")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime exo_FechaInicialVigencia { get; set; }

        [Display(Name = "Fecha Final Vigencia")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime exo_FechaIFinalVigencia { get; set; }

        [Display(Name = "Cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int clte_Id { get; set; }

        public string exo_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> exo_FechaCrea { get; set; }
        public string exo_UsuarioModifa { get; set; }
        public Nullable<System.DateTime> exo_FechaModifica { get; set; }
        
    }
}