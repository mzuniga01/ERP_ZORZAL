using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    [MetadataType(typeof(PedidosMetaData))]
    public partial class tbPedido
    {
     
    }
    public class PedidosMetaData
    {

        [Display(Name = "Código Pedido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int ped_Codigo { get; set; }

        [Display(Name = "Fecha Pedido")]
        public System.DateTime ped_Fecha { get; set; }

        [Display(Name = "Fecha Entrega")]
        public System.DateTime ped_FechaEntrega { get; set; }

        [Display(Name = "Cliente ID")]
        public int clte_id { get; set; }

        [Display(Name = "Código Sucursal")]
        public string sucur_Codigo { get; set; }

        [Display(Name = "Usuario Crea")]
        public string ped_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime ped_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public string ped_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> ped_FechaModica { get; set; }

        
    }
}