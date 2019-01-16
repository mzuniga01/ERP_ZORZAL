using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(PedidoDetalleMetaData))]
    public partial class tbPedidoDetalle
    {
    }


    public partial class PedidoDetalleMetaData
    {
        [Display(Name = "Número Pedido Detalle")]
        public int pedd_Id { get; set; }
        [Display(Name = "Número Pedido")]
        public int ped_Id { get; set; }
        [Display(Name = "Código Producto")]
        public string prod_Codigo { get; set; }
        [Display(Name = "Cantidad")]
        public decimal pedd_Cantidad { get; set; }
        [Display(Name = "Cantidad Facturada ")]
        public decimal pedd_CantidadFacturada { get; set; }
   


        public int pedd_UsuarioCrea { get; set; }
        public System.DateTime pedd_FechaCrea { get; set; }
        public Nullable<int> pedd_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pedd_FechaModifica { get; set; }
    }

}