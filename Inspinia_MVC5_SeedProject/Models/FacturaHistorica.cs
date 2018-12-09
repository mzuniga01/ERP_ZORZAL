using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(FacturaHistoricaMetaData))]
    public partial class tbFacturaHistorica
    {
    }
    public class FacturaHistoricaMetaData
    {
        [Display(Name = "Factura Historica")]
        public byte facth_Id { get; set; }
        [Display(Name = "Factura")]

        public long fact_Id { get; set; }
        [Display(Name = "Estado Factura")]

        public byte esfac_Id { get; set; }
        [Display(Name = "Fecha Factura")]

        public Nullable<System.DateTime> facth_Fecha { get; set; }
    }


}