using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(UnidadMedidaMetaData))]


    public partial class tbUnidadMedida
    {

    }

    public class UnidadMedidaMetaData
    {
        [Display(Name = "Número")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int uni_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string uni_Descripcion { get; set; }

        [Display(Name = "Abreviatura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string uni_Abreviatura { get; set; }

        [Display(Name = "Creado Por")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string uni_UsuarioCrea { get; set; }

        [Display(Name = "Creado El")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime uni_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
       
        public string uni_UsuarioModifica { get; set; }

        [Display(Name = "Modificado El")]
       
        public Nullable<System.DateTime> uni_FechaModifica { get; set; }
    }
   
}