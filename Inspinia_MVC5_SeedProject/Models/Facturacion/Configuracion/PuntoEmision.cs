﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(PuntoEmisionMetaData))]
    public partial class tbPuntoEmision
    {
        
    }
    public class PuntoEmisionMetaData
    {
        [Display(Name ="Código")]
        public int pemi_Id { get; set; }

        [Display(Name ="Número CAI")]
        public string pemi_NumeroCAI { get; set; }

        [Display(Name = "Usuario Creación")]
        public int pemi_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime pemi_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificación")]
        public Nullable<int> pemi_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> pemi_FechaModifica { get; set; }

    }
}