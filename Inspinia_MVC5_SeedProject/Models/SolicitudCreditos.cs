using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{

    [MetadataType(typeof(SolicitudCreditosMetaData))]

    public partial class tbSolicitudCredito
    {

    }

    public class SolicitudCreditosMetaData
    {
        [Display(Name ="Id Crédito")]
        [Required]
        public int cred_Id { get; set; }
        [Display(Name = "Id Cliente")]
        [Required]
        public int clte_Id { get; set; }

        [Display(Name = "Id Estado Crédito")]
        [Required]
        public byte escre_Id { get; set; }
        [Display(Name = "Fecha Solicitud")]
        [Required]
        public System.DateTime cred_FechaSolicitud { get; set; }
        [Display(Name = "Fecha Aprobacion")]
        [Required]
        public System.DateTime cred_FechaAprobacion { get; set; }
        [Display(Name = "Monto Solicitado")]
        [Required]
        public decimal cred_MontoSolicitud { get; set; }
        [Display(Name = "Monto Aprobado")]
        [Required]
        public decimal cred_MontoAprobacion { get; set; }
        [Display(Name = "Dias Solicitud")]
        [Required]
        public int cred_DiasSolicitud { get; set; }
        [Display(Name = "Dias Aprobacion")]
        [Required]
        public int cred_DiasAprobacion { get; set; }

        public string cred_UsuarioCrea { get; set; }
        public System.DateTime cred_FechaCrea { get; set; }
        public string cred_UsuarioModicacion { get; set; }
        public System.DateTime cred_FechaModifica { get; set; }

        //public virtual tbCliente tbCliente { get; set; }
        //public virtual tbEstadoSolicitudCredito tbEstadoSolicitudCredito { get; set; }
    }


   


}