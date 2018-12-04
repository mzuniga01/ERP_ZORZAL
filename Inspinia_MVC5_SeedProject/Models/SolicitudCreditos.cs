using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_Zorzal.Models
{

    [MetadataType(typeof(SolicitudCreditosMetaData))]
    public partial class tbSolicitudCredito
    {

    }


    public class SolicitudCreditosMetaData
    {
        [Display(Name = "Código Crédito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int cred_Id { get; set; }

        [Display(Name = "Código Cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int clte_Id { get; set; }

        [Display(Name = "Estado Crédito")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte escre_Id { get; set; }

        [Display(Name = "Fecha de Solicitud")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime cred_FechaSolicitud { get; set; }

        [Display(Name = "Fecha de Aprobación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime cred_FechaAprobacion { get; set; }

        [Display(Name = "Monto Solicitado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal cred_MontoSolicitado { get; set; }

        [Display(Name = "Monto Aprobado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal cred_MontoAprobado { get; set; }

        [Display(Name = "Dias Solicitado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int cred_DiasSolicitado { get; set; }

        [Display(Name = "Dias Aprobado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int cred_DiasAprobado { get; set; }

        [Display(Name = "Usuario Creó")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int cred_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creó")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime cred_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]

        public Nullable<int> cred_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]

        public Nullable<System.DateTime> cred_FechaModifica { get; set; }

        //public virtual tbUsuario tbUsuario { get; set; }
        //public virtual tbUsuario tbUsuario1 { get; set; }

        //[Display(Name = "Identificación")]
        //public virtual tbCliente tbCliente { get; set; }

        //[Display(Name = "Estado")]
        //public virtual tbEstadoSolicitudCredito tbEstadoSolicitudCredito { get; set; }
    }
}