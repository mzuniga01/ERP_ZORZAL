/// <reference path="DataTableBusquedaGenerica.js" />
$(document).ready(function(){
    Validar()
})

function Validar() {
    $.ajax({
        url: "/Factura/Validar",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({}),
    })
  .done(function (CodPedido) {
      if (CodPedido != 0) {
          GetDetallePedido()
          function GetDetallePedido() {
              $.ajax({
                  url: "/Factura/GetDetallePedido",
                  method: "POST",
                  dataType: 'json',
                  contentType: "application/json; charset=utf-8",
                  data: JSON.stringify({ CodPedido: CodPedido }),
              })
                        .done(function (data) {
                            $("#ped_Id").val(CodPedido);
                            $.each(data, function (key, val) {
                                Descuento = 0;
                                ImpuestoTotal = ((val.pscat_ISV / 100) * val.lispd_PrecioMayorista) * val.pedd_Cantidad;
                                Subtotal = ((val.pedd_Cantidad * val.lispd_PrecioMayorista) + ImpuestoTotal);
                                GranSubtotal = (val.pedd_Cantidad * val.lispd_PrecioMayorista);
                                Total = (GranSubtotal + ImpuestoTotal) - Descuento;
                                var table = $('#tblDetalleFactura').DataTable();
                                table.row.add([
                                val.prod_Codigo,
                                val.prod_Descripcion,
                                val.pedd_Cantidad,
                                val.lispd_PrecioMayorista,
                                val.pscat_ISV,
                                Descuento,
                                Subtotal,
                                '<button id = "removeFacturaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">-</button>'
                                ]).draw(false);

                                total_col1 = 0
                                SubtotalD = 0;
                                GranImpuesto = 0;
                                GranTotal = 0;
                                $("#tblDetalleFactura tbody tr").each(function (index) {
                                    DescuentoDD = $(this).children("td:eq(5)").html();
                                    Cantidad = $(this).children("td:eq(2)").html();
                                    ImpuestoD = $(this).children("td:eq(4)").html();
                                    ValorUnitario = $(this).children("td:eq(3)").html();
                                    PorcentajeImpuesto = parseFloat(ImpuestoD / 100);
                                    if (ValorUnitario != '') {
                                        total_col1 += parseFloat($(this).find('td').eq(5).text());
                                        ValorUnitario = parseFloat(ValorUnitario);
                                        SubtotalD += Cantidad * ValorUnitario;
                                        GranImpuesto += (Cantidad * ValorUnitario) * PorcentajeImpuesto;
                                        GranTotal += Cantidad * ValorUnitario + (Cantidad * ValorUnitario) * PorcentajeImpuesto;
                                    }
                                });
                                document.getElementById("TotalDescuento").innerHTML = parseFloat(total_col1);
                                document.getElementById("Subtotal").innerHTML = parseFloat(SubtotalD);
                                document.getElementById("isv").innerHTML = parseFloat(GranImpuesto);
                                document.getElementById("total").innerHTML = parseFloat(GranTotal);
                                $("#TotalProductoEncabezado").val(GranTotal)
                               


                                var FacturaDetalle = GetFacturaDetalle();
                                $.ajax({
                                    url: "/Factura/SaveFacturaDetalle",
                                    method: "POST",
                                    dataType: 'json',
                                    contentType: "application/json; charset=utf-8",
                                    data: JSON.stringify({ FacturaDetalleC: FacturaDetalle }),
                                })
                                .done(function (data) {
                                    $('#ErrorCodigoProductoCreate').text('');
                                    $('#ErrorMontoDescuentoCreate').text('');
                                    $('#ErrorCantidadCreate').text('');
                                    $('#ErrorImpuestoCreate').text('');
                                    //Input
                                    $('#prod_Codigo').val('');
                                    $('#factd_MontoDescuento').val('');
                                    $('#Impuesto').val('');
                                    $('#tbProducto_prod_Descripcion').val('');
                                    $('#factd_Cantidad').val('');
                                    $('#SubtotalProducto').val('');
                                    $('#factd_PrecioUnitario').val('');
                                    $('#factd_Impuesto').val('');
                                    $('#TotalProducto').val('');
                                }); 

                                function GetFacturaDetalle() {

                                    var FacturaDetalle = {
                                        prod_Codigo: val.prod_Codigo,
                                        factd_PorcentajeDescuento: Descuento,
                                        factd_MontoDescuento: Descuento,
                                        tbProducto_prod_Descripcion: val.prod_Descripcion,
                                        factd_Cantidad: val.pedd_Cantidad,
                                        SubtotalProducto: Subtotal,
                                        factd_PrecioUnitario: val.lispd_PrecioMayorista,
                                        factd_Impuesto: ImpuestoTotal,
                                        TotalProducto: Subtotal,
                                        factd_Id: contador
                                    }
                                    return FacturaDetalle
                                };

                            });
                        })
          }

      }
      else {
      }


  })
}



