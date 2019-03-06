using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(SalidaMetaData))]
    public partial class tbSalida
    {
        [NotMapped]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Codigo de Factura")]
        public string fact_Codigo { get; set; }
    }

    public class SalidaMetaData
    {
        [Display(Name = "ID Salida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int sal_Id { get; set; }

        [Display(Name = "Bodega")]
        public int bod_Id { get; set; }

        [Display(Name = "N# de Factura")]
        public long fact_Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Fecha de Salida")]
        public System.DateTime sal_FechaElaboracion { get; set; }

        [Display(Name = "Estado Movimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte estm_Id { get; set; }

        [Display(Name = "Tipo de Salida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte tsal_Id { get; set; }

        [Display(Name = "Razon de Devolucion")]
        public string sal_RazonDevolucion { get; set; }

        [Display(Name = "Bodega de Destino")]
        public int sal_BodDestino { get; set; }

        [Display(Name = "EsAnulado?")]
        public bool sal_EsAnulada { get; set; }

        [Display(Name = "Razon de Anulacion")]
        public string sal_RazonAnulada { get; set; }

        [Display(Name = "Creado por")]
        public int sal_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public System.DateTime sal_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> sal_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificaion")]
        public Nullable<System.DateTime> sal_FechaModifica { get; set; }
    }
}