﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(accesorolMetaData))]
    public partial class tbAccesoRol
    {

    }
    public class accesorolMetaData
    {
        [Display(Name = "Código")]
        public int acrol_Id { get; set; }
        [Display(Name = "Código Rol")]
        public int rol_Id { get; set; }
        [Display(Name = "Código Objeto")]
        public int obj_Id { get; set; }
        [Display(Name = "Descripción Acceso Rol")]
        public string acrol_Descripcion { get; set; }
        [Display(Name = "Creado por")]
        public int acrol_UsuarioCrea { get; set; }
        [Display(Name = "Creado el")]
        public System.DateTime acrol_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> acrol_UsuarioModifica { get; set; }
        [Display(Name = "Modificado el")]
        public Nullable<System.DateTime> acrol_FechaModifica { get; set; }
    }
}