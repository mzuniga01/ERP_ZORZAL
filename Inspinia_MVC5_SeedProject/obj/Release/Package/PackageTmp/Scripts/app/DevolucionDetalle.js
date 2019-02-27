var contador = 0;

$('#AgregarDetalleDevolucion').click(function () {
    var CodigoProducto = $('#prod_Codigo').val();
    var Producto = $('#tbProducto_prod_Descripcion').val();
    var Cantidad = $('#devd_CantidadProducto').val();
    var Comentario = $('#devd_Descripcion').val();

    if ($('#prod_Codigo').val() == '') {
        $('#MessageError').text('');
        $('#ErrorFecha').text('');
        $('#ErrorDescripcion').text('');
        $('#validationCodigoProductoCreate').after('<ul id="ErrorCodigoProductoCreate" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }

    else if ($('#tbProducto_prod_Descripcion').val() == '') {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorFecha').text('');
        $('#validationDescripcionCreate').after('<ul id="validationDescripcionCreate" class="validation-summary-errors text-danger">Campo Descripción Producto Requerido</ul>');
    }
     else if ($('#devd_CantidadProducto').val() == '') {
                $('#MessageError').text('');
                $('#ErrorDescripcion').text('');
                $('#ErrorFecha').text('');
                $('#validationCantidadCreate').after('<ul id="validationCantidadCreate" class="validation-summary-errors text-danger">Campo Cantidad Requerido</ul>');

     }
     else if ($('#devd_Descripcion').val() == '') {
         $('#MessageError').text('');
         $('#ErrorDescripcion').text('');
         $('#ErrorFecha').text('');
         $('#validationComentariosCreate').after('<ul id="ErrorProductoComentarioCreate" class="validation-summary-errors text-danger">Campo Observaciones Requerido</ul>');
     }
     else if ($('#devd_Monto').val() == '') {
         $('#MessageError').text('');
         $('#ErrorDescripcion').text('');
         $('#ErrorFecha').text('');
         $('#ErrorMontoCreate').after('<ul id="ErrorMontoCreate" class="validation-summary-errors text-danger">Campo Monto Requerido</ul>');
     }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'prod_Codigo'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'tbProducto_prod_Descripcion'>" + $('#tbProducto_prod_Descripcion').val() + "</td>";
        copiar += "<td id = 'devd_CantidadProducto'>" + $('#devd_CantidadProducto').val() + "</td>";
        copiar += "<td id = 'devd_Descripcion'>" + $('#devd_Descripcion').val() + "</td>";
        copiar += "<td id = 'devd_Monto'>" + $('#devd_Monto').val() + "</td>";
        copiar += "<td>" + '<button id="removeDevolucionDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tbDetalleDevolucion').append(copiar);


        var DevolucionDetalle = GetDevolucionDetalle();

        $.ajax({
            url: "/Devolucion/InsertDevolucion",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ DetalleDevolucioncont: DevolucionDetalle }),
        })
        .done(function (data) {
            $('#MessageError').text('');
            $('#ErrorFecha').text('');
            $('#ErrorCodigoProductoCreate').val('');
            $('#ErrorProductoDescripcionCreate').val('');
            $('#ErrorDescripcionCreate').val('');
            $('#ErrorProductoComentarioCreate').val('');
            //Input
            $('#prod_Codigo').val('');
            $('#tbProducto_prod_Descripcion').val('');
            $('#devd_CantidadProducto').val('');
            $('#devd_Descripcion').val('');
            $('#PrecioUnitario').val('');
            $('#devd_Monto').val('');
            $('#tbDevolucion_tbFactura_fact_PorcentajeDescuento').val('');
            $('#test_factd_MontoDescuento').val('');
            $('#Subtotal').val('');
            $('#CantidadFacturada').val('');
            $('#Impuesto').val('');
            $('#ValorImpuesto').val('');
            $('#Descuento').val('');
        });
    }
});


function GetDevolucionDetalle() {

    var DevolucionDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        devd_CantidadProducto: $('#devd_CantidadProducto').val(),
        devd_Descripcion: $('#devd_Descripcion').val(),
        devd_Monto: $('#devd_Monto').val(),
        devd_Id: contador
    }
    return DevolucionDetalle
};

$(document).on("click", "#tbDetalleDevolucion tbody tr td button#removeDevolucionDetalle", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');     
    var DevolucionDetalle = {
        devd_Id: idItem,
    };
    $.ajax({
        url: "/Devolucion/RemoveDevolucionDetalle",
        method: "POST",         
        dataType: 'json',         
        contentType: "application/json; charset=utf-8",         
        data: JSON.stringify({ DetalleDevolucioncont: DevolucionDetalle }),
    }); 
    });

