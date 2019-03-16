﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(EntradaMetaData))]
    public partial class tbEntrada
    {

    }
    public class EntradaMetaData
    {
        [Display(Name = "Código Entrada")]
        public int ent_Id { get; set; }

        [Display(Name = "Número Formato")]
        public string ent_NumeroFormato { get; set; }

        [Display(Name = "Fecha Elaboración")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public System.DateTime ent_FechaElaboracion { get; set; }

        [Display(Name = "Nombre Bodega")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int bod_Id { get; set; }

        [Display(Name = "Estado")]
        public byte estm_Id { get; set; }

        [Display(Name = "Nombre Proveedor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int prov_Id { get; set; }

        [Display(Name = "Factura Compra")]
        public string ent_FacturaCompra { get; set; }

        [Display(Name = "Fecha Compra")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public System.DateTime ent_FechaCompra { get; set; }

        [Display(Name = "Código Factura")]
        public string fact_Id { get; set; }

        [Display(Name = "Razón Devolución")]
        public string ent_RazonDevolucion { get; set; }

        [Display(Name = "Bodega Destino")]
        public int ent_BodegaDestino { get; set; }

        [Display(Name = "Tipo Entrada")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte tent_Id { get; set; }

        [Display(Name = "Creado Por")]
        public int ent_UsuarioCrea { get; set; }

        [Display(Name = "Creado El")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime ent_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public Nullable<int> ent_UsuarioModifica { get; set; }

        [Display(Name = "Modificado El")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ent_FechaModifica { get; set; }


        [Display(Name = "Nombre Bodega")]
        public virtual tbBodega tbBodega { get; set; }

        [Display(Name = "Estado")]
        public virtual tbEstadoMovimiento tbEstadoMovimiento { get; set; }

        [Display(Name = "Nombre Proveedor")]
        public virtual tbProveedor tbProveedor { get; set; }

        [Display(Name = "Tipo Entrada")]
        public virtual tbTipoEntrada tbTipoEntrada { get; set; }

    }
}