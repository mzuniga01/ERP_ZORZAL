using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(TipoDevolucionmetadata))]
    public class TipoDevolucion
    {
    }


    public class TipoDevolucionmetadata
    {
        [Display(Name = "Número")]
        public int tdev_Id { get; set; }

        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} es requerido")]
        public string tdev_Descripcion { get; set; }

    }
}