using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
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
        [Display(Name = "Usuario Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int listp_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime listp_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<int> listp_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<System.DateTime> listp_FechaModifica { get; set; }
        //[Display(Name = "Código")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //public virtual tbUsuario tbUsuario { get; set; }
        //public virtual tbUsuario tbUsuario1 { get; set; }
        //public virtual tbListadoPrecioDetalle tbListadoPrecioDetalle { get; set; }


    }
}