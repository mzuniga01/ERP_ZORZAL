using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class Estados
    {

        public byte estif_Id { get; set; }
        public string estif_Descripcion { get; set; }

        public static List<Estados> EstadoList()
        {
            List<Estados> listEstados = new List<Estados>();

            listEstados.Add(new Estados()
            {
                estif_Id = 0,
                estif_Descripcion = "Activo"
            });

            listEstados.Add(new Estados()
            {
                estif_Id = 1,
                estif_Descripcion = "Inactivo"
            });

            return listEstados;
        }
    }
}