using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(SolicitudCreditosMetaData))]

    public partial class tbSolicitudCredito
    {

    }

    public class SolicitudCreditosMetaData
    {
        [Display(Name ="Número")]
        [Required]
        public int cred_Id { get; set; }
        [Display(Name = "Número Cliente")]
        [Required]
        public int clte_Id { get; set; }
        [Display(Name = "Número Estado")]
        [Required]
        public byte escre_Id { get; set; }
        [Display(Name = "Fecha Solicitud")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}", HtmlEncode = false)]
        public System.DateTime cred_FechaSolicitud { get; set; }
        [Display(Name = "Fecha Aprobación")]
  
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:yyyy-MM-dd}", HtmlEncode = false)]
        public System.DateTime cred_FechaAprobacion { get; set; }
        [Display(Name = "Monto Solicitado")]
        [Required]
        public decimal cred_MontoSolicitado { get; set; }
        [Display(Name = "Monto Aprobado")]
        [Required]
        public decimal cred_MontoAprobado { get; set; }
        [Display(Name = "Días Solicitados")]
        [Required]
        public int cred_DiasSolicitado { get; set; }
        [Display(Name = "Días Aprobados")]
        [Required]
        public int cred_DiasAprobado { get; set; }
        [Display(Name = "Usuario Creación")]
        [Required]
        public int cred_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creación")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}", HtmlEncode = false)]
        public System.DateTime cred_FechaCrea { get; set; }
        [Display(Name = "Usuario Modificación")]
        [Required]
        public Nullable<int> cred_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modificación")]
        [Required]
        public Nullable<System.DateTime> cred_FechaModifica { get; set; }
    }
}