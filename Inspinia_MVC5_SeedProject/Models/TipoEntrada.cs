using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(TipoEntradaMetaData))]

    public partial class tbTipoEntrada
    {
    }


    public class TipoEntradaMetaData
    {
        [Display(Name = "Número")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte tent_Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string tent_Descripcion { get; set; }

        //[Display(Name = "Usuario Crea")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //public int tent_UsuarioCrea { get; set; }

        //[Display(Name = "Fecha Crea")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public System.DateTime tent_FechaCrea { get; set; }

        //[Display(Name = "Usuario Modifica")]
        //public Nullable<int> tent_UsuarioModifica { get; set; }

        //[Display(Name = "Fecha Modifica")]
        //public Nullable<System.DateTime> tent_FechaModifica { get; set; }
        
    }
}