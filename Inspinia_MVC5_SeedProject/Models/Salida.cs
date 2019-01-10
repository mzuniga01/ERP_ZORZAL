using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(SalidaMetaData))]
    public partial class tbSalida
    {
       
    }

    public class SalidaMetaData
    {
        [Display(Name = "ID Salida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int sal_Id { get; set; }

        [Display(Name = "Bodega")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int bod_Id { get; set; }

        [Display(Name = "N# de Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public long fact_Id { get; set; }

        [Display(Name = "Fecha de Salida")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime sal_FechaElaboracion { get; set; }

        [Display(Name = "Estado Movimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte estm_Id { get; set; }

        //[Display(Name = "Caja")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //public string box_Codigo { get; set; }

        [Display(Name = "Tipo de Salida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte tsal_Id { get; set; }

        [Display(Name = "Razon de Devolucion")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string sal_RazonDevolucion { get; set; }

        [Display(Name = "Creado por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int sal_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime sal_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<int> sal_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificaion")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<System.DateTime> sal_FechaModifica { get; set; }
   


    }

}