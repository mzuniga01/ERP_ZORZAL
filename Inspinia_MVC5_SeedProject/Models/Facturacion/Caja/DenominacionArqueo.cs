using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(DenominacionArqueoMetaData))]
    public partial class tbDenominacionArqueo
    {
        
    }


    public class DenominacionArqueoMetaData
    {
        [Display(Name = "Número")]
        public int arqde_Id { get; set; }
        [Display(Name = "Movimiento Caja")]
        public int mocja_Id { get; set; }
        [Display(Name = "Denominación")]
        public short deno_Id { get; set; }
        [Display(Name = "Cantidad Denominación")]
        public short arqde_CantidadDenominacion { get; set; }
        [Display(Name = "Monto Denominación")]
        public decimal arqde_MontoDenominacion { get; set; }
        [Display(Name = "Usuario Crea")]
        public Nullable<int> arqde_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime arqde_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> arqde_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifiica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime arqde_FechaModifica { get; set; }
    }
}