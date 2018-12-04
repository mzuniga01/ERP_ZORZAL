using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(PedidosMetaData))]


    public partial class tbPedidos
    {
    }

    public class PedidosMetaData
    {
        [Display (Name = "Id Pedido")]
        [Required]
        public int ped_Id { get; set; }
        [Display(Name = "Fecha Elaboración")]
        [Required]
        public System.DateTime ped_FechaElaboracion { get; set; }
        [Display(Name = "Fecha Entrega")]
        [Required]
        public System.DateTime ped_FechaEntrega { get; set; }
        [Display(Name = "Id Cliente")]
        [Required]
        public int clte_Id { get; set; }
        [Display(Name = "Id Sucursal")]
        [Required]
        public short suc_Id { get; set; }
        [Display(Name = "Id Factura")]
        [Required]
        public long fact_Id { get; set; }
        [Display(Name = "Usuario Crea")]
        [Required]
        public int ped_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        [Required]
        public System.DateTime ped_FechaCrea { get; set; }
        [Display(Name = "Usuarui Modifica")]
        [Required]
        public Nullable<int> ped_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        [Required]
        public Nullable<System.DateTime> ped_FechaModifica { get; set; }
    }
}