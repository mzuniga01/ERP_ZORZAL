using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(TiposIdenticacionMetaData))]
    public partial class tbTipoIdentificacion
    {
       
    }
    public class TiposIdenticacionMetaData
    {
        [Display(Name = "Número")]

        public byte tpi_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        [StringLength(25)]
        public string tpi_Descripcion { get; set; }

        [Display(Name = "Usuario Crea")]
        public int tpi_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime tpi_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tpi_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> tpi_FechaModifica { get; set; }
    }
}