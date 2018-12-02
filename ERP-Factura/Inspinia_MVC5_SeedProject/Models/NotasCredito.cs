using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(NotasCreditoMeta))]

    public partial class tbNotaCredito
    {

    }
    public class NotasCreditoMeta
    {
        [Display(Name ="Id Crédito")]
        [Required]
        public short nocre_Id { get; set; }
        [Display(Name = "Código Nota Crédito")]
        [Required]
        public string nocre_Codigo { get; set; }
        [Display(Name = "Id Devolución")]
        [Required]
        public int dev_Id { get; set; }
        [Display(Name = "Id Cliente")]
        [Required]
        public int clte_Id { get; set; }
        [Display(Name = "Fecha Emisión")]
        [Required]
        public System.DateTime nocre_FechaEmision { get; set; }
        [Display(Name = "Motivo Emisión")]
        [Required]
        public string nocre_MotivoEmision { get; set; }
        [Display(Name = "Monto Crédito")]
        [Required]
        public Nullable<decimal> nocre_Monto { get; set; }
        public int nocre_UsuarioCrea { get; set; }
        public System.DateTime nocre_FechaCrea { get; set; }
        public Nullable<int> nocre_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> nocre_FechaModifica { get; set; }

        //public virtual tbCliente tbCliente { get; set; }
        //public virtual tbDevolucion tbDevolucion { get; set; }
    }
}