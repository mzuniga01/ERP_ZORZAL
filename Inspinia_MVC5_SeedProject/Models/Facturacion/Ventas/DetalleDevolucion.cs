﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(DetalleDevolucionMetaData))]
    public partial class tbDevolucionDetalle
    {
    }
    public class DetalleDevolucionMetaData
    {
        [Display(Name = "Código Detalle Devolución")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public int devd_Id { get; set; }

        [Display(Name = "Código Devolución")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public int dev_Id { get; set; }

        [Display(Name = "Código Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string prod_Codigo { get; set; }

        [Display(Name = "Cantidad Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public decimal devd_CantidadProducto { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string devd_Descripcion { get; set; }

        public Nullable<int> devd_UsuarioCrea { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> devd_FechaCrea { get; set; }
        public Nullable<int> devd_UsuarioModifica { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> devd_FechaModifica { get; set; }
    }
}