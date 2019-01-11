using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(PagosArqueosMetaData))]
    public partial class tbPagosArqueo
    {

    }


    public class PagosArqueosMetaData
    {
        [Display(Name = "Número Pago Arqueado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int arqpg_Id { get; set; }


        [Display(Name = "Movimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int mocja_Id { get; set; }



        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public short tpa_Id { get; set; }



        [Display(Name = "Pagos del Sistema")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal arqpg_PagosSistema { get; set; }



        [Display(Name = "Conteo de Pagos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal arqpg_PagosConteo { get; set; }


        [Display(Name = "Usuario Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int arqpg_UsuarioCrea { get; set; }


        [Display(Name = "Fecha Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime arqpg_FechaCrea { get; set; }



        [Display(Name = "Usuario Modifica")]
        public Nullable<int> arqpg_UsuarioModifica { get; set; }


        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> arqpg_FechaModifica { get; set; }
    }
}