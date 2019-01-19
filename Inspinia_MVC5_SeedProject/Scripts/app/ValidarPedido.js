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
      console.log(CodPedido)
      if (CodPedido != 0) {
          console.log("Traigo registros")
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
                            $.each(data, function (key, val) {
                                Descuento=0.00
                                Subtotal = (val.pedd_Cantidad * val.lispd_PrecioMayorista)+((val.pscat_ISV/100)*val.lispd_PrecioMayorista);
                                contador = contador + 1;
                                copiar = "<tr data-id=" + contador + ">";
                                copiar += "<td id = 'prod_CodigoCreate'>" + val.prod_Codigo + "</td>";
                                copiar += "<td id = 'tbProducto_prod_DescripcionCreate'>" + val.prod_Descripcion + "</td>";
                                copiar += "<td id = 'factd_CantidadCreate' align='right'>" + val.pedd_Cantidad + "</td>";
                                copiar += "<td id = 'Precio_UnitarioCreate' align='right'>" + val.lispd_PrecioMayorista + "</td>";
                                copiar += "<td id = 'ImpuestoCreate' align='right'>" + val.pscat_ISV + "</td>";
                                copiar += "<td id = 'factd_MontoDescuentoCreate' align='right'>" + Descuento + "</td>";
                                copiar += "<td id = 'TotalProductoCreate' align='right'>" + Subtotal + "</td>";
                                copiar += "<td>" + '<button id="removeFacturaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                                copiar += "</tr>";
                                $('#tblDetalleFactura').append(copiar);
                                //Descuento 
                                //var Descuento = $(this).parents("tr").find("td")[5].innerHTML;
                                //console.log(Descuento)
                                //var TotalDescuento = parseFloat(document.getElementById("TotalDescuento").innerHTML);

                                //if (document.getElementById("TotalDescuento").innerHTML == '') {
                                //    totalProducto = $('#factd_MontoDescuento').val();
                                //    document.getElementById("TotalDescuento").innerHTML = parseFloat(totalProducto);
                                //}
                                //else {
                                //    document.getElementById("TotalDescuento").innerHTML = parseFloat(TotalDescuento) + parseFloat(Descuento);
                                //}

                                ////Subtotal 
                                ////var totalProducto = $(this).parents("tr").find("td")[6].innerHTML;
                                //document.getElementById('Subtotal').value = document.getElementById('row').cells[6].innerHTML;
                                //console.log(totalProducto)
                                //var subtotal = parseFloat(document.getElementById("Subtotal").innerHTML);

                                //if (document.getElementById("Subtotal").innerHTML == '') {
                                //    document.getElementById('Subtotal').value = document.getElementById('row' + id).cells[6].innerHTML;
                                //    document.getElementById("Subtotal").innerHTML = parseFloat(totalProducto);
                                //}
                                //else {
                                //    document.getElementById("Subtotal").innerHTML = parseFloat(subtotal) + parseFloat(totalProducto);
                                //}
                                ////Impuesto
                                //var totalProducto = document.getElementById("TotalProducto").value;
                                //var impuesto = parseFloat(document.getElementById("factd_Impuesto").value.replace(',', '.'));
                                //var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
                                //var porcentaje = parseFloat(impuesto / 100);
                                //var impuestos = (totalProducto * porcentaje);

                                //if (document.getElementById("isv").innerHTML == '') {
                                //    impuesto = document.getElementById("factd_Impuesto").value;
                                //    document.getElementById("isv").innerHTML = parseFloat(impuestos);
                                //}
                                //else {
                                //    document.getElementById("isv").innerHTML = parseFloat(impuestotal) + parseFloat(impuestos);
                                //}

                                ////Grantotal
                                // document.getElementById("total").innerHTML = parseFloat(subtotal) + parseFloat(totalProducto) + parseFloat(impuestotal) + parseFloat(impuestos);
                                


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



                            });
                        })
          }

      }
      else {
          console.log("No traigo absolutamente nada")
      }

      //    if (data.length > 0) {
      //        $('#mun_Codigo').empty();
      //        $('#mun_Codigo').append("<option value=''>Seleccione Municipio</option>");
      //        $.each(data, function (key, val) {
      //            $('#mun_Codigo').append("<option value=" + val.mun_Codigo + ">" + val.mun_Nombre + "</option>");
      //        });
      //        console.log(mun_Codigo)
      //        $('#mun_Codigo').trigger("chosen:updated");
      //    }
      //    else {
      //        $('#mun_Codigo').empty();
      //        $('#mun_Codigo').append("<option value=''>Seleccione Municipio</option>");
      //    }
      //});

  })
}



