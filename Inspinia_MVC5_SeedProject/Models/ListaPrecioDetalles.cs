using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{



    public partial class tbListadoPrecioDetalle
    {
      
    }
    public class ListaPrecioDetallesMetaData
    {
        [Display (Name ="Id Lista Precio Detalle")]
        [Required]
        public int listp_Id { get; set; }
        [Display(Name = "Código Producto")]
        [Required]
        public string prod_Codigo { get; set; }
        [Display(Name = "Precio Mayorista")]
        [Required]
        public decimal lispd_PrecioMayorista { get; set; }
        [Display(Name = "Precio Minorista")]
        [Required]
        public decimal lispd_Preciominorista { get; set; }
        [Display(Name = "Fecha Inicio Vigencia")]
        [Required]
        public System.DateTime lispd_Fechainiciovigencia { get; set; }
        [Display(Name = "Fecha Final Vigencia")]
        [Required]
        public System.DateTime lispd_Fechaifinalvigencia { get; set; }
        public Nullable<decimal> lispd_DescCaja { get; set; }
        [Display(Name = "Descripcion Gerente")]
        [Required]
        public Nullable<decimal> lispd_DescGerente { get; set; }
        public string lispd_UsuarioCrea { get; set; }
        public System.DateTime lispd_FechaCrea { get; set; }
        public string lispd_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> lispd_FechaModifica { get; set; }

        //public virtual tbProducto tbProducto { get; set; }
        //public virtual tbListaPrecio tbListaPrecio { get; set; }

    }
}