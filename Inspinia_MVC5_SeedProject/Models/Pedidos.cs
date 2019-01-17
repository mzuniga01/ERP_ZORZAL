using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(PedidosMetaData))]

    public partial class tbPedido
    {
    }

    public class PedidosMetaData
    {
        [Display(Name ="Número Pedido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int ped_Id { get; set; }

        [Display(Name ="Estado")]
        public byte esped_Id { get; set; }

        [Display(Name = "Fecha Elaboración")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}", HtmlEncode = false)]
        public System.DateTime ped_FechaElaboracion { get; set; }

        [Display(Name = "Fecha Entrega")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}", HtmlEncode = false)]
        public System.DateTime ped_FechaEntrega { get; set; }

        [Display(Name = "Número Cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int clte_Id { get; set; }

        [Display(Name = "Sucursal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int suc_Id { get; set; }

        [Display(Name = "Número Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public long fact_Id { get; set; }



        [Display(Name = "¿Es Anulado?")]
       // [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public bool ped_EsAnulado { get; set; }

        [Display(Name = "Razon Anulado")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string ped_RazonAnulado { get; set; }


        [Display(Name = "Usuario Creación")]
        public int ped_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creación")]
        public System.DateTime ped_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificación")]
        public Nullable<int> ped_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificación")]
        public Nullable<System.DateTime> ped_FechaModifica { get; set; }

    }
}