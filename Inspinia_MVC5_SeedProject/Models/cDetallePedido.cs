using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{

    [MetadataType(typeof(tbPedidoDetalleMetaData))]
    public partial class DetallesPedidos
    {
    }


    public  class tbPedidoDetalleMetaData
    {
        [Display(Name = "Código Detalle Pedido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int pdet_Id { get; set; }

        [Display(Name = "Código Pedido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int ped_Codigo { get; set; }

        [Display(Name = "Código Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string prod_Codigo { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string ped_Descripcion { get; set; }

        [Display(Name = "Cantidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int pdet_Cantidad { get; set; }

        [Display(Name = "Cantidad Entregada")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string pdet_CantidadEntregada { get; set; }


        public string pdet_UsuarioCrea { get; set; }
        public System.DateTime pdet_FechaCrea { get; set; }
        public string pdet_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pdet_FechaModifica { get; set; }

        public virtual tbProducto tbProducto { get; set; }


    }






}