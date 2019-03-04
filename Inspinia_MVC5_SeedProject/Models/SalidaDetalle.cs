using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(SalidaDetalleMetaData))]
    public partial class tbSalidaDetalle
    {

    }
    public class SalidaDetalleMetaData
    {
        [Display(Name = "ID Salida Detalle")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int sald_Id { get; set; }
        [Display(Name = "ID Salida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int sal_Id { get; set; }
        [Display(Name = " N# Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Codigo { get; set; }
        [Display(Name = "Cantidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal sald_Cantidad { get; set; }
        [Display(Name = "Creado Por")]
        public int sald_UsuarioCrea { get; set; }
        [Display(Name = "Creado el")]
        public System.DateTime sald_FechaCrea { get; set; }


        [Display(Name = "Modificado Por")]
        public Nullable<int> sald_UsuarioModifica { get; set; }
        [Display(Name = "Modificado el")]
        public Nullable<System.DateTime> sald_FechaModifica { get; set; }

    }
}