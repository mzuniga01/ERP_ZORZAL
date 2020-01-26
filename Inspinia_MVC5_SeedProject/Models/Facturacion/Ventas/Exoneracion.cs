﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(ExoneracionMetaData))]
    public partial class tbExoneracion
    {
     
    }
    public class ExoneracionMetaData
    {
        [Display(Name = "Número")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int exo_Id { get; set; }

        [Display(Name = "Documento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string exo_Documento { get; set; }

        [Display(Name = "Exoneración Activa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public bool exo_ExoneracionActiva { get; set; }

        [Display(Name = "Fecha Inicio")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime exo_FechaInicialVigencia { get; set; }

        [Display(Name = "Fecha Vencimiento")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime exo_FechaIFinalVigencia { get; set; }

        [Display(Name = "Cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int clte_Id { get; set; }

        [Display(Name = "Usuario Crea")]
        public int exo_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public Nullable<System.DateTime> exo_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> exo_UsuarioModifa { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> exo_FechaModifica { get; set; }
    }
}