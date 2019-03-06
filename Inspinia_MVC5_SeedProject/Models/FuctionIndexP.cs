using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class FuctionIndexP
    {
        ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        public List<tbBodegaDetalle> getProductoInformation(string Producto)
        {
            List<tbBodegaDetalle> ProductoList = new List<tbBodegaDetalle>();
            try
            {
                if (Producto != null)
                {
                    ProductoList = db.tbBodegaDetalle.Where(p => p.prod_Codigo == Producto).ToList();
                }

                return ProductoList;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return ProductoList;
            }
        }
        public List<tbPedidoDetalle> getProductoInformation2(string Producto)
        {

            List<tbPedidoDetalle> PedidiDetalleList = new List<tbPedidoDetalle>();
            try
            {
                if (Producto != null)
                {

                    PedidiDetalleList = db.tbPedidoDetalle.Where(p => p.prod_Codigo == Producto).ToList();
                }

                return PedidiDetalleList;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return PedidiDetalleList;
            }
        }
        public List<tbListadoPrecioDetalle> getProductoInformation3(string Producto)
        {


            List<tbListadoPrecioDetalle> ListaPrecioDetalle = new List<tbListadoPrecioDetalle>();
            try
            {
                if (Producto != null)
                {
                    ListaPrecioDetalle = db.tbListadoPrecioDetalle.Where(p => p.prod_Codigo == Producto).ToList();
                }

                return ListaPrecioDetalle;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return ListaPrecioDetalle;
            }
        }

        public List<tbInventarioFisicoDetalle> getProductoInformation4(string Producto)
        {


            List<tbInventarioFisicoDetalle> ListaInventarioFisicoDetalle = new List<tbInventarioFisicoDetalle>();
            try
            {
                if (Producto != null)
                {
                    ListaInventarioFisicoDetalle = db.tbInventarioFisicoDetalle.Where(p => p.prod_Codigo == Producto).ToList();
                }

                return ListaInventarioFisicoDetalle;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return ListaInventarioFisicoDetalle;
            }
        }
    }
}