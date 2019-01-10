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
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int sald_Id { get; set; }
        [Display(Name = "ID Salida")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int sal_Id { get; set; }
        [Display(Name = "N# Producto")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Codigo { get; set; }
        [Display(Name = "Cantidad")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal sal_Cantidad { get; set; }
        [Display(Name = "Caja")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string box_Codigo { get; set; }
        [Display(Name = "Creado Por")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int sald_UsuarioCrea { get; set; }
        [Display(Name = "Creado el")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime sald_FechaCrea { get; set; }
        [Display(Name = "Modificado Por")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<int> sald_UsuarioModifica { get; set; }
        [Display(Name = "Modificado el")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<System.DateTime> sald_FechaModifica { get; set; }
        public virtual tbProducto tbProducto { get; set; }
    }
}