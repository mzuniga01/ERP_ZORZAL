using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(EmpleadoMetaData))]
    public partial class tbEmpleado
    {

    }

    public class EmpleadoMetaData
    {
        [Display(Name = "Número")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short emp_Id { get; set; }
        [Display(Name = "Nombres ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string emp_Nombres { get; set; }
        [Display(Name = "Apellidos ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string emp_Apellidos { get; set; }
        [Display(Name = "Sexo ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string emp_Sexo { get; set; }
        [Display(Name = "Fecha Nacimiento")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime emp_FechaNacimiento { get; set; }

        [Display(Name = "Tipo Identificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte tpi_Id { get; set; }
        [Display(Name = "Numero Identidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string emp_Identificacion { get; set; }
        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string emp_Telefono { get; set; }
        [Display(Name = "Correo Electrónico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string emp_Correoelectronico { get; set; }
        [Display(Name = "Tipo Sangre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string emp_TipoSangre { get; set; }
        [Display(Name = "Puesto Desempeña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string emp_Puesto { get; set; }
        [Display(Name = "Fecha Ingreso")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime emp_FechaIngreso { get; set; }
        [Display(Name = "Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string emp_Direccion { get; set; }
        [Display(Name = "Observaciones")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string emp_Observaciones { get; set; }
        [Display(Name = "Creado Por")]
        public int emp_UsuarioCrea { get; set; }
        [Display(Name = "Creado El")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime emp_FechaCrea { get; set; }
        [Display(Name = "Modificado Por")]
        public Nullable<int> emp_UsuarioModifica { get; set; }
        [Display(Name = "Modificado El")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> emp_FechaModifica { get; set; }
    }
}