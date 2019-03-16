using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(DetalleEntradaMetaData))]
    public partial class tbDetalleEntrada
    {

    }
    public class DetalleEntradaMetaData
    {
        [Display(Name = "Código Entrada Detalle")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int entd_Id { get; set; }

        [Display(Name = "Código Entrada")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int ent_Id { get; set; }

        [Display(Name = "Código Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Codigo { get; set; }

        [Display(Name = "Cantidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<decimal> entd_Cantidad { get; set; }

        [Display(Name = "Creado Por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int entd_UsuarioCrea { get; set; }

        [Display(Name = "Creado El")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime entd_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public Nullable<int> entd_UsuarioModifica { get; set; }

        [Display(Name = "Modificado El")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> entd_FechaModifica { get; set; }


        [Display(Name = "Código Entrada")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public virtual tbEntrada tbEntrada { get; set; }
        [Display(Name = "Código Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public virtual tbProducto tbProducto { get; set; }
    }
}