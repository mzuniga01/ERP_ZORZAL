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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Los datos de Cliente son requeridos")]
        public int clte_Id { get; set; }
        [Display(Name = "Número Estado")]
        public byte escre_Id { get; set; }
        [Display(Name = "Fecha Solicitud")]
 //       [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:dd/MM/yyyy}",
            HtmlEncode = false)]
        public System.DateTime cred_FechaSolicitud { get; set; }
        /// <summary>
        /// //////////fecha denegacion
        /// </summary>
        /// 
        [Display(Name = "Fecha Denegación")]
        public Nullable<System.DateTime>cred_FechaDenegacion { get; set; }


        [Display(Name = "Fecha Aprobación")]
        //      [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:dd/MM/yyyy}",
            HtmlEncode = false)]
        public System.DateTime cred_FechaAprobacion { get; set; }

        //create
        [Display(Name = "Monto Solicitado")]
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "El campo {0} permite números mayores que cero")]
        // [RegularExpression("^\\d+$", ErrorMessage = "El campo {0} permite números mayores que cero")]
         //[RegularExpression(@"^(((\d{1,3})(,\d{3})*)|(\d+))(.\d+)?$", ErrorMessage = "El campo {0} permite números mayores que cero")]
        public decimal cred_MontoSolicitado { get; set; }


        [Display(Name = "Monto Aprobado")]
      //  [RegularExpression("^\\d+$", ErrorMessage = "El campo {0} permite números iguales o mayores que cero")]
        public decimal cred_MontoAprobado { get; set; }

        //create
        [Display(Name = "Días Solicitados")]
        [Required]
       [Range(1, int.MaxValue, ErrorMessage = "El campo {0} permite números mayores que cero")]
        [RegularExpression("^\\d+$", ErrorMessage = "El campo {0} permite números iguales o mayores que cero")]
        public int cred_DiasSolicitado { get; set; }


        [Display(Name = "Días Aprobados")]
        //[Required]
        [RegularExpression("^\\d+$", ErrorMessage = "El campo {0} permite números iguales o mayores que cero")]
        public int cred_DiasAprobado { get; set; }
        [Display(Name = "Usuario Crea")]

        public int cred_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime cred_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> cred_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> cred_FechaModifica { get; set; }
    }
}