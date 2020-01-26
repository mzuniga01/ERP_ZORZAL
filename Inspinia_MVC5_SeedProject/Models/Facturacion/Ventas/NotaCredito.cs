﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(NotaCreditoMetaData))]
    public partial class tbNotaCredito
    {
    }
    public class NotaCreditoMetaData
    {
        [Display(Name = "Id Nota Crédito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public short nocre_Id { get; set; }

        [Display(Name = "Nota Crédito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string nocre_Codigo { get; set; }

        [Display(Name = "Devolución")]
        public int dev_Id { get; set; }

        [Display(Name = "Cliente")]
        public int clte_Id { get; set; }

        [Display(Name = "Sucursal")]
        public int suc_Id { get; set; }

        [Display(Name = "Caja")]
        public short cja_Id { get; set; }

        [Display(Name = "Anulada")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public bool nocre_Anulado { get; set; }

        [Display(Name = "Razón Anulada")]
        public string nocre_RazonAnulado { get; set; }
        
        [Display(Name = "Fecha Emisión")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}", HtmlEncode = false)]
        public System.DateTime nocre_FechaEmision { get; set; }

        [Display(Name = "Motivo Emisión")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string nocre_MotivoEmision { get; set; }

        [Display(Name = "Monto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public Nullable<decimal> nocre_Monto { get; set; }

        [Display(Name = "Redimido")]
        public bool nocre_Redimido { get; set; }

        [Display(Name = "Fecha Redención")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}", HtmlEncode = false)]
        public Nullable<DateTime> nocre_FechaRedimido { get; set; }

        [Display(Name = "Impreso")]
       public bool nocre_EsImpreso { get; set; }

        [Display(Name = "Usuario Crea")]
        public int nocre_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime nocre_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> nocre_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> nocre_FechaModifica { get; set; }

       
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCliente tbCliente { get; set; }
        public virtual tbDevolucion tbDevolucion { get; set; }
        public virtual tbSucursal tbSucursal { get; set; }
    }
}