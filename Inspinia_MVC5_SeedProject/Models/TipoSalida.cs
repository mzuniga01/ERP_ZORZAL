using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(TipoSalidaMetaData))]
    public partial class tbTipoSalida
    {
       

    }
    public class TipoSalidaMetaData
    {
        [Display(Name = "Id de Tipo de Salida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte tsal_Id { get; set; }

        [Display(Name = "Tipo de Salida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string tsal_Descripcion { get; set; }

        [Display(Name = "Creado por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int tsal_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime tsal_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<int> tsal_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<System.DateTime> tsal_FechaModifica { get; set; }
    }
}