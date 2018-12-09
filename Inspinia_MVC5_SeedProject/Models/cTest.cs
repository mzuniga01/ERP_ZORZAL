using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class cTest
    {
        public static List<cTP> TPList()
        {
            List<cTP> list = new List<cTP>();

            list.Add(new cTP()
            {
                ID_TP = 1,
                DESCRIPCION_TP = "Efectivo"
            });
            list.Add(new cTP()
            {
                ID_TP = 2,
                DESCRIPCION_TP = "Tarjeta Crédito/Débito"
            });
            return list;
        }
    }
}