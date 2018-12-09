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
        public string escre_Descripcion { get; set; }


        [Display(Name = "Usuario Crea")]
       
        public int escre_UsuarioCrea { get; set; }

        [Display(Name = "Usuario Modifica")]

        public Nullable<int> escre_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Agrego ")]

        public System.DateTime escre_FechaAgrego { get; set; }

        [Display(Name = "Fecha Modifica")]

        public Nullable<System.DateTime> escre_FechaModifica { get; set; }

    }



   

}