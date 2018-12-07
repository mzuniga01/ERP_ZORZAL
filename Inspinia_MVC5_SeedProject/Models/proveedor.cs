using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(proveedor))]

    public partial class tbProveedor
    {

    }
    public class proveedor
    {
        [Display(Name = "Número Proveedor")]
        public int prov_Id { get; set; }
        [Display(Name = "Nombre")]
        public string prov_Nombre { get; set; }
        [Display(Name = "Nombre Contacto")]
        public string prov_NombreContacto { get; set; }
        [Display(Name = "Dirección")]
        public string prov_Direccion { get; set; }
        [Display(Name = "Email")]
        public string prov_Email { get; set; }
        [Display(Name = "Teléfono")]
        public string prov_Telefono { get; set; }
        [Display(Name = "Creado Por")]
        public int prov_UsuarioCrea { get; set; }
        [Display(Name = "Creado El")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime prov_FechaCrea { get; set; }
        [Display(Name = "Modificado Por")]
        public Nullable<int> prov_UsuarioModifica { get; set; }
        [Display(Name = "Modificado El")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> prov_FechaModifica { get; set; }

    }
}