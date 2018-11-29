using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    [MetadataType(typeof(EstadoSolicitudCreditosMetaData))]


    public partial class tbEstadoSolicitudCredito
    {

    }

    public class EstadoSolicitudCreditosMetaData
    {
        [Display(Name = "ID Solicitud Crédito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public byte escre_Id { get; set; }

        [Display(Name = "Descripción Crédito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string escre_Descripcion { get; set; }

        [Display(Name = "Usuario Crea")]
        public string escre_UsuarioCrea { get; set; }

        [Display(Name = "Usuario Modifico")]
        public string escre_UsuarioModifico { get; set; }

        [Display(Name = "Fecha Agrego")]
        public System.DateTime escre_FechaAgrego { get; set; }

        [Display(Name = "Fecha Modifico")]
        public Nullable<System.DateTime> escre_FechaModifico { get; set; }
    }
}