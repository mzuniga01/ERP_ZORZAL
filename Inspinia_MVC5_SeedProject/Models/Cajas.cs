using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(CajasMetaData))]
    public partial class tbCaja
    {
    }


    public class CajasMetaData
    {
            [Display(Name = "Número")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
            public short cja_Id { get; set; }



            [Display(Name = "Descripción")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
            public string cja_Descripcion { get; set; }


            [Display(Name = "Sucursal")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
            public int suc_Id { get; set; }



            [Display(Name = "Usuario Crea")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
            public int cja_UsuarioCrea { get; set; }


            [Display(Name = "Fecha Crea")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
            public System.DateTime cja_FechaCrea { get; set; }


            [Display(Name = "Usuario Modifica")]
            public Nullable<int> cja_UsuarioModifica { get; set; }


            [Display(Name = "Fecha Modifica")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> cja_FechaModifica { get; set; }
    }

    

}