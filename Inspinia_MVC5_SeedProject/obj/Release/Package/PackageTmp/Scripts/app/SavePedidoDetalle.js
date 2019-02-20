var contador = 0;
$('#AñadirPedidoDetalle').click(function () {
    var prod_Codigo = $('#prod_Codigo').val();
    var CodigoBarra = $('#tbProducto_prod_CodigoBarras').val();
    var prod_Descripcion = $('#tbProducto_prod_Descripcion').val();
    var pedd_Descripcion = $('#pedd_Descripcion').val();
    var pedd_Cantidad = $('#pedd_Cantidad').val();
    var pedd_CantidadFacturada = $('#pedd_CantidadFacturada').val();


    if (prod_Codigo == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationCodigoCreate').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Campo Codigo Producto Requerido</ul>');
    }
    else if (prod_Descripcion == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationNombreCreate').after('<ul id="NombreError" class="validation-summary-errors text-danger">Campo Descripcion Producto Requerido</ul>');
    }
    else if (pedd_Descripcion == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationNombreCreate').after('<ul id="NombreError" class="validation-summary-errors text-danger">Campo Descripcion Pedido Requerido</ul>');
    }
    else if (pedd_Cantidad == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationNombreCreate').after('<ul id="NombreError" class="validation-summary-errors text-danger">Campo Cantidad Requerido</ul>');
    }
    else if (pedd_CantidadFacturada == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationNombreCreate').after('<ul id="NombreError" class="validation-summary-errors text-danger">Campo Cantidad Facturada Requerido</ul>');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        //copiar += "<td>" + $('#CodTipoCasoExitoCreate option:selected').text() + "</td>";
        //copiar += "<td hidden id='MunCodigo'>" + $('#mun_Codigo option:selected').val() + "</td>";
        copiar += "<td id = 'prod_Codigo'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'prod_Descripcion'>" + $('#tbProducto_prod_Descripcion').val() + "</td>";
        copiar += "<td id = 'pedd_Descripcion'>" + $('#pedd_Descripcion').val() + "</td>";
        copiar += "<td id = 'pedd_Cantidad'>" + $('#pedd_Cantidad').val() + "</td>";
        copiar += "<td id = 'pedd_CantidadFacturada'>" + $('#pedd_CantidadFacturada').val() + "</td>";

        copiar += "<td>" + '<button id="QuitarDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblPedidoDetalle').append(copiar);


        var tbPedidoDetalle = GetPedidoDetalle();
        $.ajax({
            url: "/Pedido/SavePedidoDetalles",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ PedidoDetalle:tbPedidoDetalle }),
        })
                .done(function (data) {
                    $('#prod_Codigo').val('');
                    $('#tbProducto_prod_CodigoBarras').val('');
                    $('#tbProducto_prod_Descripcion').val('');
                    $('#pedd_Descripcion').val('');
                    $('#pedd_Cantidad').val('');
                    $('#pedd_CantidadFacturada').val('');
                    $('#MessageError').text('');
                    $('#NombreError').text('');
                    console.log('Hola');
                });


    }

});


function GetPedidoDetalle() {
    var PedidoDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        CodigoBarra : $('#tbProducto_prod_CodigoBarras').val(),
         prod_Descripcion : $('#tbProducto_prod_Descripcion').val(),
         pedd_Descripcion : $('#pedd_Descripcion').val(),
         pedd_Cantidad : $('#pedd_Cantidad').val(),
         pedd_CantidadFacturada : $('#pedd_CantidadFacturada').val(),
        mun_UsuarioCrea: contador
    };
    return PedidoDetalle;
}




$(document).on("click", "#tblPedidoDetalle tbody tr td button#QuitarDetalle", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var PedidoDetalle = {
        mun_UsuarioCrea: idItem,
    };
    $.ajax({
        url: "/Pedido/QuitarPedidoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ PedidoDetalle: PedidoDetalle }),
    });
});