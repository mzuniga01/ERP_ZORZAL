using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(MovimientoCajasMetaData))]
    public partial class tbMovimientoCaja
    {

    }

    public class MovimientoCajasMetaData
    {   

        [Display(Name = "Número Movimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int mocja_Id { get; set; }


        [Display(Name = "Caja")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public short cja_Id { get; set; }


        [Display(Name = "Fecha Apertura")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime mocja_FechaApertura { get; set; }


        [Display(Name = "Usuario Apertura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public Nullable<int> mocja_UsuarioApertura { get; set; }


        [Display(Name = "Fecha Arqueo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime mocja_FechaArqueo { get; set; }


        [Display(Name = "Usuario Arquea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public Nullable<int> mocja_UsuarioArquea { get; set; }


        [Display(Name = "Fecha Aceptacion")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime mocja_FechaAceptacion { get; set; }


        [Display(Name = "Usuario Aceptacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public Nullable<int> mocja_UsuarioAceptacion { get; set; }


        [Display(Name = "Usuario Crea")]
        public Nullable<int> mocja_UsuarioCrea { get; set; }


        [Display(Name = "Fecha Crea")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime mocja_FechaCrea { get; set; }


        [Display(Name = "Usuario Modifica")]
        public Nullable<int> mocja_UsuarioModifica { get; set; }


        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime mocja_FechaModifica { get; set; }
    }
}