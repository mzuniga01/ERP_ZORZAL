using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(EstadoFacturaMetaData))]
    public partial class tbEstadoFactura
    {

    }

    public class EstadoFacturaMetaData
    {

        public byte esfac_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="El campo {0} es requerido")]
        public string esfac_Descripcion { get; set; }

        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int esfac_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime esfac_FechaCrea { get; set; }

        public Nullable<int> esfac_UsuarioModifica { get; set; }

        public Nullable<System.DateTime> esfac_FechaModifica { get; set; }
    }

   
}