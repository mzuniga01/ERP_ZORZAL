using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(proveedor))]

    public partial class tbProveedor
    {

    }
    public class proveedor
    {
       
        [Display(Name = "Número")]
        public int prov_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "Máximo {1} caracteres")]
        public string prov_Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Contacto")]
        [StringLength(75, ErrorMessage = "Máximo {1} caracteres")]
        public string prov_NombreContacto { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Dirección")]
        [StringLength(50, ErrorMessage = "Máximo {1} caracteres")]
        public string prov_Direccion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Es un Correo Electronico")]
        [StringLength(50, ErrorMessage = "Máximo {1} caracteres")]
        public string prov_Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Teléfono")]
        [Phone]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "* Solo se permiten números.")]

        [StringLength(50, ErrorMessage = "Máximo {1} caracteres")]
        public string prov_Telefono { get; set; }

        //[Display(Name = "Creado Por")]
        //public int prov_UsuarioCrea { get; set; }
        //[Display(Name = "Creado El")]
        ////[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //public System.DateTime prov_FechaCrea { get; set; }
        //[Display(Name = "Modificado Por")]
        //public Nullable<int> prov_UsuarioModifica { get; set; }
        //[Display(Name = "Modificado El")]
        ////[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //public Nullable<System.DateTime> prov_FechaModifica { get; set; }

    }
}