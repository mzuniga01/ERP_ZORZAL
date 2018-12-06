using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(PedidosMetaData))]

    public partial class tbPedido
    {
    }

    public class PedidosMetaData
    {
        [Display(Name ="Número Pedido")]
        [Required]
        public int ped_Id { get; set; }
        [Display(Name = "Fecha Elaboración")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}", HtmlEncode = false)]
        public System.DateTime ped_FechaElaboracion { get; set; }
        [Display(Name = "Fecha Entrega")]
        [Required]
        public System.DateTime ped_FechaEntrega { get; set; }
        [Display(Name = "Número Cliente")]
        [Required]
        public int clte_Id { get; set; }
        [Display(Name = "Sucursal")]
        [Required]
        public short suc_Id { get; set; }
        [Display(Name = "Número Factura")]
        [Required]
        public long fact_Id { get; set; }
        [Display(Name = "Usuario Creación")]
        [Required]
        public int ped_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creación")]
        [Required]
        public System.DateTime ped_FechaCrea { get; set; }
        [Display(Name = "Usuario Modificación")]
        [Required]
        public Nullable<int> ped_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modificación")]
        [Required]
        public Nullable<System.DateTime> ped_FechaModifica { get; set; }

    }
}