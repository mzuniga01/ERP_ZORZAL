using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(SucursalMetaData))]
    public partial class tbSucursal
    {


    }
    public class SucursalMetaData
    {
        [Display(Name ="Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int suc_Id { get; set; }

        [Display(Name ="Municipio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string mun_Codigo { get; set; }

        [Display(Name = "Bodega")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int bod_Id { get; set; }

        [Display(Name = "Punto Emisión")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pemi_Id { get; set; }

        [Display(Name = "Sucursal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string suc_Descripcion { get; set; }

        [Display(Name = "Correo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string suc_Correo { get; set; }

        [Display(Name = "Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string suc_Direccion { get; set; }

        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string suc_Telefono { get; set; }

        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int suc_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime suc_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificación")]
        public Nullable<int> suc_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> suc_FechaModifica { get; set; }
    }
}