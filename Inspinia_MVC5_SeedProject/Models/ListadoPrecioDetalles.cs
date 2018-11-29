using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{

    [MetadataType(typeof(ListadoPrecioDetallesMetaData))]


    public partial class tbListadoPrecioDetalle
    {

    }

    public class ListadoPrecioDetallesMetaData
    {
        [Display(Name = "ID Listado Precios")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int listp_Id { get; set; }

        [Display(Name = "Código Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string prod_Codigo { get; set; }

        [Display(Name = "Precio Mayoristas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal lispd_PrecioMayorista { get; set; }

        [Display(Name = "Precio Minoristas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal lispd_Preciominorista { get; set; }

        [Display(Name = "Fecha Inicio Vigencia")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public System.DateTime lispd_Fechainiciovigencia { get; set; }

        [Display(Name = "Fecha Final Vigencia")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public System.DateTime lispd_Fechaifinalvigencia { get; set; }

        [Display(Name = "Descripción Caja")]
        public Nullable<decimal> lispd_DescCaja { get; set; }

        [Display(Name = "Descripción Gerente")]
        public Nullable<decimal> lispd_DescGerente { get; set; }

        [Display(Name = "Usuario Crea")]
        public string lispd_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime lispd_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public string lispd_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> lispd_FechaModifica { get; set; }


    }

}