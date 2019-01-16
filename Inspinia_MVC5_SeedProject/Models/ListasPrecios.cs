using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(ListasPreciosMetaData))]
    public partial class tbListaPrecio
    {
    }

   public class ListasPreciosMetaData
    {
        [Display(Name = "Numero de Lista")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int listp_Id { get; set; }


        [Display(Name = "Nombre de Lista")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string listp_Nombre { get; set; }


        [Display(Name = "¿Es Activo?")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<bool> listp_EsActivo { get; set; }

        [Display(Name = "Prioridad")]
        [Range(01, 99, ErrorMessage = "El Campo {0} debe ser un numero entre 01 y {2}")]
        //[StringLength(04, MinimumLength = 03, ErrorMessage = "El campo {0} debe tener el mismo formato de Rango Inicial")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<short> listp_Prioridad { get; set; }


        [Display(Name = "Usuario Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int listp_UsuarioCrea { get; set; }


        [Display(Name = "Fecha Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt }", ApplyFormatInEditMode = true)]
        public System.DateTime listp_FechaCrea { get; set; }


        [Display(Name = "Usuario Modifico")]
    
        public Nullable<int> listp_UsuarioModifica { get; set; }


        [Display(Name = "Fecha Modifico")]

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> listp_FechaModifica { get; set; }
        //[Display(Name = "Código")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha Inicio Vigencia")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> listp_FechaInicioVigencia { get; set; }

        [Display(Name = "Fecha Final Vigencia")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> listp_FechaFinalVigencia { get; set; }

        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        //public virtual tbListadoPrecioDetalle tbListadoPrecioDetalle { get; set; }


    }
}