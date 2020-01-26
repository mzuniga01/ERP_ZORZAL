using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class Test
    {
        public static List<TipoPagos> TPList()
        {
            List<TipoPagos> list = new List<TipoPagos>();

            list.Add(new TipoPagos()
            {
                ID_TP = 1,
                DESCRIPCION_TP = "Efectivo"
            });
            list.Add(new TipoPagos()
            {
                ID_TP = 2,
                DESCRIPCION_TP = "Tarjeta Crédito/Débito"
            });
            return list;
        }
    }
}