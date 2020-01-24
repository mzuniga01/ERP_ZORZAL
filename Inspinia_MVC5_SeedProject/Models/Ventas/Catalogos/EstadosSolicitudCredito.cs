using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(EstadosSolicitudCreditosMetaData))]

    public partial class tbEstadoSolicitudCredito
    {

    }
    public class EstadosSolicitudCreditosMetaData
    {

        [Display(Name = "Número")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte escre_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El campo {0} debe de tener como máximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "El campo {0} no permite números y caracteres especiales")]
        public string escre_Descripcion { get; set; }


        [Display(Name = "Usuario Crea")]
        public int escre_UsuarioCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> escre_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Agrego ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime escre_FechaAgrego { get; set; }

        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> escre_FechaModifica { get; set; }

    }



   

}