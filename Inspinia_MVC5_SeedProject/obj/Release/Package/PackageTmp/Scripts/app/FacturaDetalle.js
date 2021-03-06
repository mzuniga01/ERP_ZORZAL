﻿var contador = 0;

$('#AgregarDetalleFactura').click(function () {
    var CodigoProducto = $('#prod_Codigo').val();
    var PorcentajeDescuento = $('#factd_PorcentajeDescuento').val();
    var MontoDescuento = $('#factd_MontoDescuento').val();
    var DescripcionProducto = $('#tbProducto_prod_Descripcion').val();
    var CantidadProducto = $('#factd_Cantidad').val();
    var Subtotal = $('#SubtotalProducto').val();
    var PrecioUnitario = $('#factd_PrecioUnitario').val();
    var Impuesto = $('#factd_Impuesto').val();
    var Total = $('#TotalProducto').val();
    
    if (CodigoProducto == '' )
    {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationCodigoProductoCreate').after('<ul id="ErrorCodigoProductoCreate" class="validation-summary-errors text-danger">Campo Código Producto requerido</ul>');
    }
    else if (MontoDescuento == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationMontoDescuentoCreate').after('<ul id="ErrorMontoDescuentoCreate" class="validation-summary-errors text-danger">Campo Monto Descuento requerido</ul>');
    }
    else if (CantidadProducto == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationCantidadProductoCreate').after('<ul id="ErrorCantidadCreate" class="validation-summary-errors text-danger">Campo Cantidad requerido</ul>');
    }
    else if (Impuesto == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationImpuestoProductoCreate').after('<ul id="ErrorImpuestoCreate" class="validation-summary-errors text-danger">Campo Impuesto requerido</ul>');
    }
    else {
        //ajax para el controlador
        var FacturaDetalle = GetFacturaDetalle();
        $.ajax({
            url: "/Factura/SaveFacturaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ FacturaDetalleC: FacturaDetalle, data_producto: CodigoProducto })
        })
            .done(function(datos) {
                if (datos == CodigoProducto) {
                //alert('Es Igual.')
                var cantfisica_nueva = $('#factd_Cantidad').val();
                $("#tblDetalleFactura td").each(function () {
                    var prueba = $(this).text()
                    if (prueba == CodigoProducto) {
                        var idcontador = $(this).closest('tr').data('id');
                        var cantfisica_anterior = $(this).closest("tr").find("td:eq(2)").text();
                        var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);
                        $(this).closest('tr').remove();
                        copiar = "<tr data-id=" + idcontador + ">";
                        copiar += "<td id = 'prod_CodigoCreate'>" + CodigoProducto + "</td>";
                        copiar += "<td id = 'tbProducto_prod_DescripcionCreate'>" + DescripcionProducto + "</td>";
                        copiar += "<td id = 'factd_CantidadCreate' align='right'>" + sumacantidades + "</td>";
                        copiar += "<td id = 'Precio_UnitarioCreate' align='right'>" + PrecioUnitario + "</td>";
                        copiar += "<td id = 'ImpuestoCreate' align='right'>" + Impuesto + "</td>";
                        copiar += "<td id = 'factd_MontoDescuentoCreate' align='right'>" + MontoDescuento + "</td>";
                        copiar += "<td id = 'TotalProductoCreate' align='right'>" + Total + "</td>";
                        copiar += "<td>" + '<button id="removeFacturaDetalle" class="btn btn-danger glyphicon glyphicon-trash btn-xs eliminar" type="button"></button>' + "</td>";
                        copiar += "</tr>";
                        $('#tblDetalleFactura').append(copiar);
                    }
                });
            }else {
                //alert('NO ES IGUAL')
                //Rellenar la tabla 
                contador = contador + 1;
                copiar = "<tr data-id=" + contador + ">";
                copiar += "<td id = 'prod_CodigoCreate'>" + CodigoProducto + "</td>";
                copiar += "<td id = 'tbProducto_prod_DescripcionCreate'>" + DescripcionProducto + "</td>";
                copiar += "<td id = 'factd_CantidadCreate' align='right'>" + CantidadProducto + "</td>";
                copiar += "<td id = 'Precio_UnitarioCreate' align='right'>" + PrecioUnitario + "</td>";
                copiar += "<td id = 'ImpuestoCreate' align='right'>" + Impuesto + "</td>";
                copiar += "<td id = 'factd_MontoDescuentoCreate' align='right'>" + MontoDescuento + "</td>";
                copiar += "<td id = 'TotalProductoCreate' align='right'>" + Total + "</td>";
                copiar += "<td>" + '<button id="removeFacturaDetalle" class="btn btn-danger glyphicon glyphicon-trash btn-xs eliminar" type="button"></button>' + "</td>";
                copiar += "</tr>";
                $('#tblDetalleFactura').append(copiar);
            }    

        //Descuento 
        var Descuento = $('#factd_MontoDescuento').val();
        var TotalDescuento = parseFloat(document.getElementById("TotalDescuento").innerHTML);

        if (document.getElementById("TotalDescuento").innerHTML == '') {
            totalProducto = $('#factd_MontoDescuento').val();
            document.getElementById("TotalDescuento").innerHTML = parseFloat(totalProducto);
        }
        else {
            document.getElementById("TotalDescuento").innerHTML = parseFloat(TotalDescuento) + parseFloat(Descuento);
        }

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
        //Impuesto
        var Cantidad = CantidadProducto
        var Precio = PrecioUnitario
        var impuesto = parseFloat(document.getElementById("factd_Impuesto").value.replace(',', '.'));
        var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
        var porcentaje = parseFloat(impuesto / 100);
        var impuestos = (Cantidad * Precio) * porcentaje;
        if (document.getElementById("isv").innerHTML == '') {
            impuesto = document.getElementById("factd_Impuesto").value;
            document.getElementById("isv").innerHTML = parseFloat(impuestos);
        }
        else {
            document.getElementById("isv").innerHTML = parseFloat(impuestotal) + parseFloat(impuestos);
        }

        //Grantotal
        if (document.getElementById("total").innerHTML == '') {
            var TotalEncabezado = document.getElementById("total").innerHTML = parseFloat(totalProducto) + parseFloat(impuestos) - parseFloat(Descuento);
            $("#TotalProductoEncabezado").val(TotalEncabezado);
        }
        else {
            var TotalEncabezado = document.getElementById("total").innerHTML = parseFloat(subtotal) + parseFloat(totalProducto) + parseFloat(impuestotal) + parseFloat(impuestos)- parseFloat(TotalDescuento) - parseFloat(Descuento);
            $("#TotalProductoEncabezado").val(TotalEncabezado);
        }        
        })
        .done(function (data) {
            $('#ErrorCodigoProductoCreate').text('');
            $('#ErrorMontoDescuentoCreate').text('');
            $('#ErrorCantidadCreate').text('');
            $('#ErrorImpuestoCreate').text('');
            //Input
            $('#prod_CodigoBarras').val('');
            $('#prod_Codigo').val('');
            $('#factd_MontoDescuento').val('');
            $('#Impuesto').val('');
            $('#tbProducto_prod_Descripcion').val('');
            $('#factd_Cantidad').val('');
            $('#SubtotalProducto').val('');
            $('#factd_PrecioUnitario').val('');
            $('#factd_Impuesto').val('');
            $('#TotalProducto').val('');
        })
    }
})

function GetFacturaDetalle() {

    var FacturaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        factd_PorcentajeDescuento: $('#factd_PorcentajeDescuento').val(),
        factd_MontoDescuento: $('#factd_MontoDescuento').val(),
        tbProducto_prod_Descripcion: $('#tbProducto_prod_Descripcion').val(),
        factd_Cantidad: $('#factd_Cantidad').val(),
        SubtotalProducto: $('#SubtotalProducto').val(),
        factd_PrecioUnitario: $('#factd_PrecioUnitario').val(),
        factd_Impuesto: $('#factd_Impuesto').val(),
        TotalProducto: $('#TotalProducto').val(),
        factd_Id: contador
    }
    return FacturaDetalle
};


$(document).on("click", "#tblDetalleFactura tbody tr td button#removeFacturaDetalle", function () {

    //Descuento
    var Descuento = $(this).parents("tr").find("td")[5].innerHTML;
    var TotalDescuento = parseFloat(document.getElementById("TotalDescuento").innerHTML);
    document.getElementById("TotalDescuento").innerHTML = parseFloat(TotalDescuento) - parseFloat(Descuento);

    //Subtotal
    var Cantidad = $(this).parents("tr").find("td")[2].innerHTML;
    var Precio = $(this).parents("tr").find("td")[3].innerHTML;
    var SubtotalProducto = Cantidad * Precio;
    var subtotal = parseFloat(document.getElementById("Subtotal").innerHTML);
    document.getElementById("Subtotal").innerHTML = parseFloat(subtotal) - parseFloat(SubtotalProducto);

    //Impuesto
    var impuesto = $(this).parents("tr").find("td")[4].innerHTML;
    var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
    var porcentaje = parseFloat(impuesto.replace(',', '.') / 100);
    var impuestos = (SubtotalProducto * porcentaje);
    document.getElementById("isv").innerHTML = parseFloat(impuestotal) - parseFloat(impuestos);

    //GranTotal
    var TotalEncabezado = document.getElementById("total").innerHTML = (parseFloat(subtotal) - parseFloat(SubtotalProducto)) + (parseFloat(impuestotal) - parseFloat(impuestos));
    $("#TotalProductoEncabezado").val(TotalEncabezado);

    var table = $('#tblDetalleFactura').DataTable();
    table.row($(this).parents('tr'))
        .remove()
        .draw();

    //$(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var FacturaDetalle = {
        factd_Id: idItem,
    };

    $.ajax({
        url: "/Factura/RemoveFacturaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ FacturaDetalleC: FacturaDetalle }),
    });
});

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
}

function pierdeFoco(e) {
    var valor = e.value.replace(/^0*/, '');
    e.value = valor;
}

function ponerdecimales(numero) {
    if (numero.indexOf(".") == -1) { numero += ".00" } else {
        if (numero.indexOf(".") == numero.length - 2) { numero += "0" }
    }
    return numero;
}

function validar(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-z0-9A-Z\-]+$/.test(tecla);
}

$("#Producto").click(function()
{
    ListaProductos();
})
function ListaProductos() {
    url = "/Factura/ListaProductos";
    $('#ModalAgregarProducto').modal('show');
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
    })
}