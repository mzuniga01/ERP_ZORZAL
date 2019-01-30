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
        [Display(Name = "Número ")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public short emp_Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string emp_Nombres { get; set; }

        [Display(Name = "Apellidos ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string emp_Apellidos { get; set; }

        [Display(Name = "Sexo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string emp_Sexo { get; set; }

        [Display(Name = "Fecha Nacimiento")] 
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public DateTime emp_FechaNacimiento { get; set; }

        [Display(Name = "Tipo Identificación ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public byte tpi_Id { get; set; }

        [Display(Name = "Número de Identificación ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string emp_Identificacion { get; set; }


        [Display(Name = "Teléfono ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string emp_Telefono { get; set; }


        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*Se requiere un correo electrónico")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "No es un Correo Electronico")]
        public string emp_Correoelectronico { get; set; }


        [Display(Name = "Tipo Sangre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string emp_TipoSangre { get; set; }

        [Display(Name = "Puesto Desempeña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string emp_Puesto { get; set; }


        [Display(Name = "Fecha Ingreso")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime emp_FechaIngreso { get; set; }


        [Display(Name = "Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string emp_Direccion { get; set; }

        [Display(Name = "Razón de Inactivación")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string emp_RazonInactivacion { get; set; }

        //[Display(Name = "Usuario Crea")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        //public int emp_UsuarioCrea { get; set; }

        //[Display(Name = "Fecha Crea ")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        //public System.DateTime emp_FechaCrea { get; set; }

        //[Display(Name = "Usuario Modifica ")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        //public Nullable<int> emp_UsuarioModifica { get; set; }

        //[Display(Name = "Fecha Modifica ")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        //public Nullable<System.DateTime> emp_FechaModifica { get; set; }

        [Display(Name = "Estado ")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public bool emp_Estado { get; set; }

        [Display(Name = "Razón de la Salida ")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string emp_RazonSalida { get; set; }

        [Display(Name = "Fecha Salida ")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public Nullable<System.DateTime> emp_FechaDeSalida { get; set; }

    }
}