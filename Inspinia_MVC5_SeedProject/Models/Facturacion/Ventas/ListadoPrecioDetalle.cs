using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(ListadoPrecioDetalleMetaData))]
    public partial class tbListadoPrecioDetalle
    {

    }




    public class ListadoPrecioDetalleMetaData
    {
        [Display(Name = "ID Listado Precio Detalle")]
        public int lispd_Id { get; set; }


        [Display(Name = "ID Listado Precio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int listp_Id { get; set; }

        [Display(Name = "Producto Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Codigo { get; set; }

        [Display(Name = "Precio Mayorista")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal lispd_PrecioMayorista { get; set; }

        [Display(Name = "Precio Minorista")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal lispd_PrecioMinorista { get; set; }

        [Display(Name = "Descuento Caja")]
        public Nullable<decimal> lispd_DescCaja { get; set; }

        [Display(Name = "Descuento Gerente")]
        public Nullable<decimal> lispd_DescGerente { get; set; }


        [Display(Name = "Usuario Creación")]
        public int lispd_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime lispd_FechaCrea { get; set; }


        [Display(Name = "Usuario Modificación")]
        public Nullable<int> lispd_UsuarioModifica { get; set; }


        [Display(Name = "Fecha Modificó")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> lispd_FechaModifica { get; set; }

    }
}