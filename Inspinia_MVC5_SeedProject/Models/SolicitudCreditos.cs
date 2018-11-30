using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{

    [MetadataType(typeof(SolicitudCreditoMetaData))]

    public partial class tbSolicitudCredito
    {

    }
    public class SolicitudCreditoMetaData
    {
        [Display(Name = "Crédito")]
        [Required]
        public int cred_Id { get; set; }
        [Display(Name = "Cliente")]
        [Required]
        public int clte_Id { get; set; }
        [Display(Name = "Estado Crédito")]
        [Required]
        public byte escre_Id { get; set; }
        [Display(Name = "Fecha de Solicitud")]
        [Required]
        public System.DateTime cred_FechaSolicitud { get; set; }
        [Display(Name = "Fecha de Aprobación")]
        [Required]
        public System.DateTime cred_FechaAprobacion { get; set; }
        [Display(Name = "Monto Solicitado")]
        [Required]
        public decimal cred_MontoSolicitado { get; set; }
        [Display(Name = "Monto Aprobado ")]
        [Required]
        public decimal cred_MontoAprobado { get; set; }
        [Display(Name = "Solicitado a los dias ")]
        [Required]
        public int cred_DiasSolicitado { get; set; }
        [Display(Name = "Aprobado a los dias ")]
        [Required]
        public int cred_DiasAprobado { get; set; }
        [Display(Name = "Usuaria Crea")]
        [Required]
        public int cred_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        [Required]
        public System.DateTime cred_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifico")]
        [Required]
        public Nullable<int> cred_UsuarioModicacion { get; set; }
        [Display(Name = "Fecha Modifico")]
        [Required]
        public Nullable<System.DateTime> cred_FechaModifica { get; set; }
        [Display(Name = "Cliente")]
        [Required]

        public virtual tbCliente tbCliente { get; set; }
        [Display(Name = "Estado")]
        [Required]
        public virtual tbEstadoSolicitudCredito tbEstadoSolicitudCredito { get; set; }
    }
}



