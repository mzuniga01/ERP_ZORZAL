$(document).ready(function () {
    if (fact_AlCredito.checked) {
        $('#Credito').show();
    } else {
        $('#Credito').hide();
    }
});

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
}

function pierdeFoco(e) {
    var valor = e.value.replace(/^0*/, '');
    e.value = valor;
}

//Show Tabla Detalle Factura
$(document).ready(function () {
    GetDetalle()
    $('#prod_CodigoBarras').focus();
    $('#Alcredito').hide();
    $('#factd_Cantidad').prop('disabled', true);
    $('#tblDetalleFactura').DataTable({
        "searching": false,
        "lengthChange": false,
        "bPaginate": true,
        "bInfo": true,
        "responsive": true,
        "oLanguage": {
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior",
            },
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "No hay registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoEmpty": "Mostrando 0 de 0 Entradas",
            "sSearch": "Buscar",
            "sLengthMenu": "Mostrar _MENU_ registros por página",
            "sInfo": "Mostrando _START_ a _END_ Entradas"
        }
    });

    $('#factd_Impuesto').val((0).toFixed(2));
    $('#factd_PrecioUnitario').val((0).toFixed(2));
    $('#factd_Cantidad').val((0).toFixed(2));
    $('#factd_PorcentajeDescuento').val((0).toFixed(2));
    $('#factd_MontoDescuento').val((0).toFixed(2));
    $('#Impuesto').val((0).toFixed(2));
    $('#SubtotalProducto').val((0).toFixed(2));
    $('#TotalProducto').val((0).toFixed(2));
});
var contador = 0;
function GetDetalle() {
    var factID = $("#fact_Id").val();
    $.ajax({
        url: "/Factura/GetFacturaDetalleD",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ factID: factID }),
    })
      .done(function (data) {
          $.each(data, function (key, val) {
              Descuento = (val.factd_MontoDescuento * val.factd_Cantidad);
              ImpuestoTotal = ((val.factd_Impuesto / 100) * val.factd_PrecioUnitario) * val.factd_Cantidad;
              Subtotal = ((val.factd_Cantidad * val.factd_PrecioUnitario) - Descuento);
              Total = (Subtotal + ImpuestoTotal);
              StudentId = val.factd_Id;
              var table = $('#tblDetalleFactura').DataTable();
              $(this).closest('tr').remove();
              table.row($(this).parents('tr')).remove().draw();
              table.row.add([
              val.prod_Codigo,
              val.prod_Descripcion,
              parseFloat(val.factd_Cantidad).toFixed(2),
              val.factd_PrecioUnitario,
              val.factd_Impuesto,
              val.factd_MontoDescuento,
              parseFloat(Subtotal).toFixed(2),
              "<a href='#' onclick='EditStudentRecord(" + StudentId + ")' ><span class='btn btn-warning glyphicon glyphicon-edit btn-xs'></span></a>" + "</td>"
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
                      SubtotalD += Cantidad * ValorUnitario - DescuentoDD;
                      GranImpuesto += (Cantidad * ValorUnitario) * PorcentajeImpuesto;
                      GranTotal += Cantidad * ValorUnitario + (Cantidad * ValorUnitario) * PorcentajeImpuesto;
                  }
              });
              document.getElementById("TotalDescuento").innerHTML = parseFloat(total_col1).toFixed(2);
              document.getElementById("Subtotal").innerHTML = parseFloat(SubtotalD).toFixed(2);
              document.getElementById("isv").innerHTML = parseFloat(GranImpuesto).toFixed(2);
              document.getElementById("total").innerHTML = parseFloat(GranTotal - DescuentoDD).toFixed(2);
          });

      })

}
//Show Modal Detalle Factura
function EditStudentRecord(StudentId) {
    var url = "/Factura/GetDetalleEdit?StudentId=" + StudentId;
    $("#FacturaDetalleEdit").modal();
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            $.each(data, function (key, arn) {

                var fechaString = arn.factd_FechaCrea.substr(6);
                var fechaActual = new Date(parseInt(fechaString));
                var mes = fechaActual.getMonth() + 1;
                var dia = fechaActual.getDate();
                var anio = fechaActual.getFullYear();
                var FechaCrea = dia + "/" + mes + "/" + anio;

                $("#factdd").val(arn.factd_Id);
                $("#IDProducto").val(arn.prod_Codigo);
                $("#DescProducto").val(arn.prod_Descripcion);
                $("#MontoDescuentoEdit").val(arn.factd_MontoDescuento);
                $("#CantidadEdit").val(arn.factd_Cantidad);
                $("#ImpuestoEdit").val(arn.factd_Impuesto);
                $("#PrecioUnitarioEdit").val(arn.factd_PrecioUnitario);
                $("#UsuCrea").val(arn.factd_UsuarioCrea);
                $("#FechaCrea").val(FechaCrea);
                var Cantidad = document.getElementById("CantidadEdit").value;
                var Precio = document.getElementById("PrecioUnitarioEdit").value;
                var Descuento = document.getElementById("MontoDescuentoEdit").value;
                var Impuesto = document.getElementById("ImpuestoEdit").value;
                var PorcentajeImpuesto = (Impuesto / 100);
                var ImpuestoTotal = (Cantidad * Precio) * PorcentajeImpuesto;
                result = "";
                result1 = "";
                if (Cantidad && Precio > 0) {
                    result += Cantidad * Precio - Descuento;
                    result1 += ((Cantidad * Precio) + ImpuestoTotal)
                }
                $("#SubtotalEdit").val(result);
                $("#TotalEdit").val(result1);
            })
        }
    })
}
//Editar Show Detalle Factura
$("#EditFacturaDetalle").click(function () {
    var factd_Ids = $('#factd_Id').val();
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "POST",
        url: "/Factura/UpdateFacturaDetalle",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
        }
    });
    location.reload(true);
})
//Anular Factura
$('#Anular').click(function () {
    var CodFactura = $('#fact_Id').val();
    var FacturaAnulado = 1
    var RazonAnulado = $('#razonAnular').val();
    if (RazonAnulado == "") {
        valido = document.getElementById('Mensaje');
        valido.innerText = "La razón Anular es requerida";
    } else {
        $.ajax({
            url: "/Factura/AnularFactura",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodFactura: CodFactura, FacturaAnulado: FacturaAnulado, RazonAnulado: RazonAnulado }),

        })
            .done(function (data) {
                if (data.length > 0) {
                    var url = $("#RedirectTo").val();
                    location.href = url;
                }
                else {
                    alert("Registro No Actualizado");
                }
            });
    }

})
//Factura Buscar Producto
$(document).ready(function () {
    var $rows = $('#ProductoTbody tr');
    $("#search").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
        if (val.length >= 3) {
            $rows.show().filter(function () {
                var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                return !~text.indexOf(val);
            }).hide();
        }
        else if (val.length >= 1) {
            $rows.show().filter(function () {
            }).hide();
        }

    })
});
// Factura Seleccionar Producto
$(document).on("click", "#tbProductoFactura tbody tr td button#seleccionar", function () {
    var currentRow = $(this).closest("tr");
    var prod_CodigoBarrasItem = currentRow.find("td:eq(2)").text();
    var prod_DescripcionItem = currentRow.find("td:eq(1)").text();
    var prod_CodigoItem = currentRow.find("td:eq(0)").text();
    var bod_Id = $('#bod_Id').val();
    $('#prod_CodigoBarras').val(prod_CodigoBarrasItem);
    document.getElementById("prod_CodigoBarras").focus();
    $('#tbProducto_prod_Descripcion').val(prod_DescripcionItem);
    $('#prod_Codigo').val(prod_CodigoItem);
    $('#ModalAgregarProducto').modal('hide');
    SeleccionProducto();
});
$(function () {
    $("#CantidadEdit").keyup(function (e) {
        var Cantidad = document.getElementById("CantidadEdit").value;
        var Precio = document.getElementById("PrecioUnitarioEdit").value;
        var Descuento = document.getElementById("MontoDescuentoEdit").value;
        var Impuesto = document.getElementById("ImpuestoEdit").value;
        var PorcentajeImpuesto = (Impuesto / 100);
        var ImpuestoTotal = (Cantidad * Precio) * PorcentajeImpuesto;
        result = "";
        result1 = "";
        if (Cantidad && Precio > 0) {
            result += Cantidad * Precio;
            result1 += ((Cantidad * Precio) + ImpuestoTotal - Descuento)
        }
        $("#SubtotalEdit").val(result);
        $("#TotalEdit").val(result1);
    });
});

//Modal Producto
$("#Producto").click(function () {
    ListaProductos();
})

function ListaProductos() {
    url = "/Factura/ListaProductos";
    var table = $('#tbProductoFactura').dataTable({
        destroy: true,
        resposive: true,
        ajax: {
            method: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            "dataSrc": ""
        },
        "columns": [
            { "data": "prod_Codigo" },
            { "data": "prod_Descripcion" },
            { "data": "prod_CodigoBarras" },
            { "defaultContent": "<button class='btn btn-primary btn-xs'  id='seleccionar' data-dismiss='modal'>Seleccionar</button>" }
        ],
        "searching": false,
        "lengthChange": false,
        "oLanguage": {
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior",
            },
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sEmptyTable": "No hay registros",
            "sInfoEmpty": "Mostrando 0 de 0 Entradas",
            "sSearch": "Buscar",
            "sInfo": "Mostrando _START_ a _END_ Entradas",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
        }
    });
    $('#ModalAgregarProducto').modal('show');
}

function SeleccionProducto() {
    var CodBarra = $('#prod_CodigoBarras').val();
    var DescFactura = $('#Descuento').val();
    var PorcentajeDescuentoDet = $('#factd_PorcentajeDescuento').val();
    var IDSucursal = $('#suc_Id').val();
    var IDCliente = $('#clte_Id').val();
    $.ajax({
        type: 'POST',
        url: '/Factura/BuscarCodigoBarras',
        data: JSON.stringify({ IDSucursal: IDSucursal, CodBarra: CodBarra, IDCliente: IDCliente }),
        contentType: 'application/json;',
        dataType: 'json',
        success: function (data) {
            if (data.length > 0) {
                $.each(data, function (key, val) {
                    if (val.EXISTE) {
                        data_producto = val.CODIGOPRODUCTO;
                        data_descripcion = val.DESCRIPCIONPRODUCTO;
                        data_impuesto = val.IMPUESTOPRODUCTO;
                        data_precio = val.PRECIOUNITARIO;
                        descuento = val.DESCUENTOCAJERO;
                        if (data_precio == null) {
                            $('#factd_Impuesto').val((0).toFixed(2));
                            $('#factd_PrecioUnitario').val((0).toFixed(2));
                            $('#factd_Cantidad').val(0);
                            $('#factd_PorcentajeDescuento').val((0).toFixed(2));
                            $('#factd_MontoDescuento').val((0).toFixed(2));
                            $('#Impuesto').val((0).toFixed(2));
                            $('#SubtotalProducto').val((0).toFixed(2));
                            $('#TotalProducto').val((0).toFixed(2));
                            $('#factd_Cantidad').val(0);
                            $('#factd_Cantidad').prop('disabled', true);
                            $('#msjSinPrecio').show();
                            $('#alerta').hide();
                            $('#SinCantidad').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            $('#CantidadDistinta').hide();
                        }
                        else {
                            $('#factd_Cantidad').prop('disabled', false);
                            $('#prod_Codigo').val(data_producto);
                            $('#tbProducto_prod_Descripcion').val(data_descripcion);
                            $('#factd_Impuesto').val(data_impuesto.toFixed(2));
                            $('#factd_PrecioUnitario').val(data_precio.toFixed(2));
                            $('#factd_Cantidad').val(1);
                            $('#factd_PorcentajeDescuento').val(descuento.toFixed(2));
                            $('#factd_Cantidad').prop('disabled', false);
                            $('#factd_Cantidad').focus();
                            Cantidad = $('#factd_Cantidad').val();
                            PrecioSubtotal = (parseFloat(data_precio) * parseFloat(Cantidad));
                            $('#SubtotalProducto').val((PrecioSubtotal).toFixed(2));
                            MontoDescuento = (PrecioSubtotal * (parseFloat(descuento) / 100));
                            $('#factd_MontoDescuento').val(MontoDescuento.toFixed(2));
                            Impuesto = ((PrecioSubtotal - MontoDescuento) * (parseFloat(data_impuesto) / 100));
                            $('#Impuesto').val(Impuesto.toFixed(2));
                            total = parseFloat((PrecioSubtotal - MontoDescuento) + Impuesto);
                            $('#TotalProducto').val((total).toFixed(2));
                            $('#msjSinPrecio').hide();
                            $('#alerta').hide();
                            $('#SinCantidad').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            $('#CantidadDistinta').hide();
                            GetCantidad(1);
                        }
                    }
                })
            }
        }
    });
}
//Enter Codigo de barras
$("#prod_CodigoBarras").keyup(function (e) {
    var prodCodigoBarrar = $("#prod_CodigoBarras").val();
    var code = e.which;
    if (code == 13) e.preventDefault();
    if (code == 32 || code == 13 || code == 188 || code == 186) {
        if (prodCodigoBarrar == '') {
            $('#factd_Cantidad').prop('disabled', true);
        }
        else {
            BuscarCodigoBarras();
        }
    }
})
//Enter cantidad
$("#factd_Cantidad").keyup(function (e) {
    GetCantidad(1);//Validar que cuando no haya cantidad no permita ingresar el detalle.

    var code = e.which;
    if (code == 13) e.preventDefault();
    if (code == 32 || code == 13 || code == 188 || code == 186) {
        if ($("#factd_Cantidad").val() != '' && parseFloat($("#factd_Cantidad").val()) != 0) {
            var cantfisica_nueva = $('#factd_Cantidad').val();
            addtable(cantfisica_nueva);
        }
        else {
            $('#msjSinPrecio').hide();
            $('#alerta').hide();
            $('#SinCantidad').hide();
            $('#CantidadNoDisponible').hide();
            $('#NoExiste').hide();
            $('#CantidadDistinta').show();
        }
    }
});

function GetCantidad(Cantidad1) {
    var CodSucursal = $('#suc_Id').val();
    var CodProducto = $('#prod_Codigo').val();
    var CantidadIngresada = $('#factd_Cantidad').val();

    if (CantidadIngresada == '') {
        $('#factd_MontoDescuento').val((0).toFixed(2));
        $('#Impuesto').val((0).toFixed(2));
        $('#SubtotalProducto').val((0).toFixed(2));
        $('#TotalProducto').val((0).toFixed(2));
    }
    else {
        $.ajax({
            url: "/Factura/GetCantidad",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodSucursal: CodSucursal, CodProducto: CodProducto }),
        })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (key, val) {
                    var MENSAJE = data[0]['MENSAJE'];
                    if (MENSAJE) {
                        var can = data[0]['CANTIDAD'];
                        var CANTIDAD = parseFloat(can);
                        if (CANTIDAD < Cantidad1) {
                            $('#SinCantidad').show();
                            $('#msjSinPrecio').hide();
                            $('#alerta').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            $('#CantidadDistinta').hide();
                            $('#factd_Cantidad').prop('disabled', false);
                        }
                        if (CANTIDAD < CantidadIngresada) {
                            $('#SinCantidad').show();
                            $('#msjSinPrecio').hide();
                            $('#alerta').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            $('#CantidadDistinta').hide();
                            $('#factd_Cantidad').prop('disabled', false);
                        }
                        else {
                            $('#msjSinPrecio').hide();
                            $('#alerta').hide();
                            $('#SinCantidad').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            $('#CantidadDistinta').hide();
                            //Calcular
                            Cantidad = $('#factd_Cantidad').val();
                            PrecioSubtotal = (parseFloat(data_precio) * parseFloat(Cantidad));
                            $('#SubtotalProducto').val((PrecioSubtotal).toFixed(2));
                            MontoDescuento = (PrecioSubtotal * (parseFloat(descuento) / 100));
                            PrecioSubtotal = PrecioSubtotal - MontoDescuento;
                            Impuesto = ((PrecioSubtotal) * (parseFloat(data_impuesto) / 100));
                            $('#factd_MontoDescuento').val(MontoDescuento.toFixed(2));
                            $('#Impuesto').val(Impuesto.toFixed(2));
                            total = parseFloat((PrecioSubtotal) + Impuesto);
                            $('#TotalProducto').val((total).toFixed(2));
                            $('#factd_Cantidad').prop('disabled', false);
                        }
                    }
                    else {
                        $('#CantidadNoDisponible').show();
                        $('#SinCantidad').show();
                        $('#msjSinPrecio').hide();
                        $('#alerta').hide();
                        $('#NoExiste').hide();
                        $('#CantidadDistinta').hide();
                        $('#factd_Cantidad').prop('disabled', true);
                    }
                });
            }
        });
    }
}

function addtable(cantfisica_nueva) {
    var IDSucursal = $('#suc_Id').val();
    var IDCliente = $('#clte_Id').val();
    var table = $('#tblDetalleFactura').DataTable();
    var FacturaDetalle = GetFacturaDetalle();
    $.ajax({
        url: "/Factura/SaveFacturaDetalleEdit",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ FacturaDetalleEdit: FacturaDetalle, data_producto: data_producto })
    })
    .done(function (datos) {
        if (datos == data_producto) {
            $("#tblDetalleFactura td").each(function () {
                var prueba = $(this).text();
                if (prueba == data_producto) {
                    var Cantidad = $('#factd_Cantidad').val();
                    var CantidadSum = $(this).parents("tr").find("td")[2].innerHTML;
                    CantidadSum = parseFloat(CantidadSum) + parseFloat(Cantidad);
                    var PrecioSum = $(this).parents("tr").find("td")[3].innerHTML;
                    var Descuento = $(this).closest("tr").find("td:eq(5)").text();
                    var impuesto = parseFloat(document.getElementById("factd_Impuesto").value.replace(',', '.'));
                    var SubTotal = (parseFloat(PrecioSum) * parseFloat(CantidadSum));
                    var MontoDescuento = (SubTotal * (parseFloat(Descuento) / 100));
                    SubTotal = SubTotal - MontoDescuento;
                    var Impuesto = ((SubTotal) * (parseFloat(data_impuesto) / 100));
                    var Total = parseFloat((SubTotal) + Impuesto);
                    $(this).closest('tr').remove();
                    table.row($(this).parents('tr')).remove().draw();
                    table.row.add([
                    data_producto,
                    data_descripcion,
                    parseFloat(CantidadSum).toFixed(2),
                    data_precio,
                    data_impuesto,
                    Descuento,
                    parseFloat(Total).toFixed(2),
                    '<button id = "removeFacturaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">-</button>'
                    ]).draw(false);
                    CalculoDetalle();
                    LimpiarControles();
                }
            });
        }
        else {
            var CodigoProducto = $('#prod_Codigo').val();
            var DescripcionProducto = $('#tbProducto_prod_Descripcion').val();
            var Cantidad = $('#factd_Cantidad').val();
            var Total = $('#TotalProducto').val();
            table.row.add([
                        data_producto,
                        data_descripcion,
                        parseFloat(Cantidad).toFixed(2),
                        parseFloat(data_precio).toFixed(2),
                        parseFloat(data_impuesto).toFixed(2),
                        parseFloat(descuento).toFixed(2),
                        parseFloat(Total).toFixed(2),
                        '<button id = "removeFacturaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">-</button>'
            ]).draw(false);
            CalculoDetalle();
            LimpiarControles();
        }
    })
}

function GetFacturaDetalle() {

    var FacturaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        factd_PorcentajeDescuento: parseFloat($('#factd_PorcentajeDescuento').val()),
        factd_MontoDescuento: parseFloat($('#factd_MontoDescuento').val()),
        tbProducto_prod_Descripcion: $('#tbProducto_prod_Descripcion').val(),
        factd_Cantidad: parseFloat($('#factd_Cantidad').val()),
        SubtotalProducto: parseFloat($('#SubtotalProducto').val()),
        factd_PrecioUnitario: parseFloat($('#factd_PrecioUnitario').val()),
        factd_Impuesto: parseFloat($('#factd_Impuesto').val()),
        TotalProducto: $('#TotalProducto').val()
    }
    return FacturaDetalle
};

function CalculoDetalle() {
    //Subtotal 
    var totalProducto = $('#SubtotalProducto').val();
    var subtotal = parseFloat(document.getElementById("Subtotal").innerHTML);

    if (document.getElementById("Subtotal").innerHTML == '') {
        totalProducto = $('#SubtotalProducto').val();
        document.getElementById("Subtotal").innerHTML = parseFloat(totalProducto);
    }
    else {
        document.getElementById("Subtotal").innerHTML = parseFloat(subtotal) + parseFloat(totalProducto);
    }

    //Descuento 
    var Descuento = $('#factd_MontoDescuento').val();
    var TotalDescuento = parseFloat(document.getElementById("TotalDescuento").innerHTML);
    if (document.getElementById("TotalDescuento").innerHTML == '') {
        document.getElementById("TotalDescuento").innerHTML = parseFloat(Descuento).toFixed(2);

    }
    else {
        document.getElementById("TotalDescuento").innerHTML = (parseFloat(TotalDescuento) + parseFloat(Descuento)).toFixed(2);
    }
    //Impuesto
    var Cantidad = $('#factd_Cantidad').val();
    var Precio = $('#factd_PrecioUnitario').val();
    var impuesto = parseFloat(document.getElementById("factd_Impuesto").value.replace(',', '.'));
    var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
    var porcentaje = parseFloat(impuesto / 100);
    var impuestos = ((Cantidad * Precio) - Descuento) * porcentaje;
    if (document.getElementById("isv").innerHTML == '') {
        impuesto = document.getElementById("factd_Impuesto").value;
        document.getElementById("isv").innerHTML = (parseFloat(impuestos)).toFixed(2);
    }
    else {
        document.getElementById("isv").innerHTML = (parseFloat(impuestotal) + parseFloat(impuestos)).toFixed(2);
    }

    //Grantotal
    if (document.getElementById("total").innerHTML == '') {
        var TotalEncabezado = document.getElementById("total").innerHTML = (parseFloat(totalProducto) + parseFloat(impuestos) - parseFloat(Descuento)).toFixed(2);
        $("#TotalProductoEncabezado").val(TotalEncabezado);
    }
    else {
        var TotalEncabezado = document.getElementById("total").innerHTML = (parseFloat(subtotal) + parseFloat(totalProducto) + parseFloat(impuestotal) + parseFloat(impuestos) - parseFloat(TotalDescuento) - parseFloat(Descuento)).toFixed(2);
        $("#TotalProductoEncabezado").val(TotalEncabezado);
    }
};

function LimpiarControles() {
    $('#prod_CodigoBarras').val('');
    $('#prod_Codigo').val('');
    $('#tbProducto_prod_Descripcion').val('');
    $('#factd_Impuesto').val((0).toFixed(2));
    $('#factd_PrecioUnitario').val((0).toFixed(2));
    $('#factd_Cantidad').val((0).toFixed(2));
    $('#factd_PorcentajeDescuento').val((0).toFixed(2));
    $('#factd_MontoDescuento').val((0).toFixed(2));
    $('#Impuesto').val((0).toFixed(2));
    $('#SubtotalProducto').val((0).toFixed(2));
    $('#TotalProducto').val((0).toFixed(2));
    $('#prod_CodigoBarras').focus();
    $('#factd_Cantidad').prop('disabled', true);
}

function BuscarCodigoBarras() {
    var CodBarra = $('#prod_CodigoBarras').val();
    var DescFactura = $('#Descuento').val();
    var PorcentajeDescuentoDet = $('#factd_PorcentajeDescuento').val();
    var IDSucursal = $('#suc_Id').val();
    var IDCliente = $('#clte_Id').val();
    $.ajax({
        type: 'POST',
        url: '/Factura/BuscarCodigoBarras',
        data: JSON.stringify({ IDSucursal: IDSucursal, CodBarra: CodBarra, IDCliente: IDCliente }),
        contentType: 'application/json;',
        dataType: 'json',
        success: function (data) {
            if (data.length > 0) {
                $.each(data, function (key, val) {
                    if (val.EXISTE) {
                        data_producto = val.CODIGOPRODUCTO;
                        data_descripcion = val.DESCRIPCIONPRODUCTO;
                        data_impuesto = val.IMPUESTOPRODUCTO;
                        data_precio = val.PRECIOUNITARIO;
                        descuento = val.DESCUENTOCAJERO;
                        if (data_precio == null) {
                            $('#factd_Impuesto').val((0).toFixed(2));
                            $('#factd_PrecioUnitario').val((0).toFixed(2));
                            $('#factd_Cantidad').val(0);
                            $('#factd_PorcentajeDescuento').val((0).toFixed(2));
                            $('#factd_MontoDescuento').val((0).toFixed(2));
                            $('#Impuesto').val((0).toFixed(2));
                            $('#SubtotalProducto').val((0).toFixed(2));
                            $('#TotalProducto').val((0).toFixed(2));
                            $('#factd_Cantidad').val(0);
                            $('#factd_Cantidad').prop('disabled', true);
                            $('#msjSinPrecio').show();
                            $('#alerta').hide();
                            $('#SinCantidad').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                        }
                        else {
                            $('#factd_Cantidad').prop('disabled', false);
                            $('#prod_Codigo').val(data_producto);
                            $('#tbProducto_prod_Descripcion').val(data_descripcion);
                            $('#factd_Impuesto').val(data_impuesto.toFixed(2));
                            $('#factd_PrecioUnitario').val(data_precio.toFixed(2));
                            $('#factd_Cantidad').val(1);
                            $('#factd_PorcentajeDescuento').val(descuento.toFixed(2));
                            $('#factd_Cantidad').prop('disabled', false);
                            $('#factd_Cantidad').focus();
                            Cantidad = $('#factd_Cantidad').val();
                            PrecioSubtotal = (parseFloat(data_precio) * parseFloat(Cantidad));
                            $('#SubtotalProducto').val((PrecioSubtotal).toFixed(2));
                            MontoDescuento = (PrecioSubtotal * (parseFloat(descuento) / 100));
                            PrecioSubtotal = PrecioSubtotal - MontoDescuento;
                            Impuesto = ((PrecioSubtotal) * (parseFloat(data_impuesto) / 100));
                            $('#factd_MontoDescuento').val(MontoDescuento.toFixed(2));
                            $('#Impuesto').val(Impuesto.toFixed(2));
                            total = parseFloat((PrecioSubtotal) + Impuesto);
                            $('#TotalProducto').val((total).toFixed(2));
                            $('#msjSinPrecio').hide();
                            $('#alerta').hide();
                            $('#SinCantidad').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            GetCantidad(1);
                        }
                    }
                    else {
                        $('#factd_Cantidad').prop('disabled', true);
                        $('#tbProducto_prod_Descripcion').val('');
                        $('#prod_Codigo').val('');
                        $('#factd_Impuesto').val((0).toFixed(2));
                        $('#factd_PrecioUnitario').val((0).toFixed(2));
                        $('#factd_Cantidad').val((0).toFixed(2));
                        $('#factd_PorcentajeDescuento').val((0).toFixed(2));
                        $('#factd_MontoDescuento').val((0).toFixed(2));
                        $('#Impuesto').val((0).toFixed(2));
                        $('#SubtotalProducto').val((0).toFixed(2));
                        $('#TotalProducto').val((0).toFixed(2));
                        $('#msjSinPrecio').hide();
                        $('#alerta').hide();
                        $('#SinCantidad').hide();
                        $('#CantidadNoDisponible').hide();
                        $('#NoExiste').show();
                    }
                })
            }
        }
    });
}

$(document).on("click", "#tblDetalleFactura tbody tr td button#removeFacturaDetalle", function () {

    var Cantidad = $(this).parents("tr").find("td")[2].innerHTML;
    var PrecioUni = $(this).parents("tr").find("td")[3].innerHTML;

    ////Descuento
    var Descuento = $(this).parents("tr").find("td")[5].innerHTML;
    Descuento = (parseFloat(Cantidad) * parseFloat(PrecioUni)) * (parseFloat(Descuento) / 100)
    var TotalDescuento = parseFloat(document.getElementById("TotalDescuento").innerHTML);
    document.getElementById("TotalDescuento").innerHTML = (parseFloat(TotalDescuento) - parseFloat(Descuento)).toFixed(2);

    //Impuesto
    var impuesto = $(this).parents("tr").find("td")[4].innerHTML;
    var porcentaje = parseFloat(impuesto.replace(',', '.') / 100);
    ImpuestoT = ((parseFloat(Cantidad) * parseFloat(PrecioUni) - Descuento) * (parseFloat(porcentaje))).toFixed(2)
    var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
    document.getElementById("isv").innerHTML = (parseFloat(impuestotal) - parseFloat(ImpuestoT)).toFixed(2);

    //Subtotal
    var SubtotalProducto = Cantidad * PrecioUni;
    var subtotal = parseFloat(document.getElementById("Subtotal").innerHTML);
    document.getElementById("Subtotal").innerHTML = (parseFloat(subtotal) - parseFloat(SubtotalProducto)).toFixed(2);

    //GranTotal
    var Total = parseFloat(document.getElementById("total").innerHTML);
    document.getElementById("total").innerHTML = (parseFloat(Total) - parseFloat(SubtotalProducto) + parseFloat(Descuento) - parseFloat(ImpuestoT)).toFixed(2);

    var FacturaDetalle = {
        prod_Codigo: $(this).parents("tr").find("td")[0].innerHTML,
    };
    ////Descuento
    var PorcentajeDescuento = $(this).parents("tr").find("td")[5].innerHTML;
    var TotalDescuento = $(this).parents("tr").find("td")[6].innerHTML;
    var PrecioUnitario = $(this).parents("tr").find("td")[3].innerHTML;
    var Cantidad = $(this).parents("tr").find("td")[2].innerHTML;
    var table = $('#tblDetalleFactura').DataTable();
    table.row($(this).parents('tr'))
                .remove()
                .draw();

    $.ajax({
        url: "/Factura/RemoveFacturaDetalleEdit",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ FacturaDetalleC: FacturaDetalle }),
    })
    .done(function (data) {
    });
});