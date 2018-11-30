using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(EstadoSolicitudCreditoscs))]

    public partial class tbEstadoSolicitudCredito
    {

    }
    public class EstadoSolicitudCreditoscs
    {
        [Display(Name = "Estado")]
        [Required]
        public byte escre_Id { get; set; }
        [Display(Name = "Crédito")]
        [Required]
        public string escre_Descripcion { get; set; }
        [Display(Name = "Usuario Crea")]
        [Required]
        public int escre_UsuarioCrea { get; set; }
        [Display(Name = "Usuario Modifico")]
        [Required]
        public Nullable<int> escre_UsuarioModifico { get; set; }
        [Display(Name = "Fecha Agrego")]
        [Required]
        public System.DateTime escre_FechaAgrego { get; set; }
        [Display(Name = "Fecha Modifico")]
        [Required]
        public Nullable<System.DateTime> escre_FechaModifico { get; set; }
        [Display(Name = "Solicitu Credito ")]
        [Required]
        public virtual ICollection<tbSolicitudCredito> tbSolicitudCredito { get; set; }

    }
}