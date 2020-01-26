$('#BtnGuardarDetalle').click(function () {
    var ProductoCodigo = $('#prod_Codigo').val();
    var Cantidad = $('#pedd_Cantidad').val();

    if (ProductoCodigo == '') {
        $('#ErrorProductoCodigoCreate').text('');
        $('#validacionProductoCodigoCreate').after('<p id="ErrorProductoCodigoCreate" style="color:red">Campo Producto requerido</p>');
    }
    else if (Cantidad == '') {
        $('#ErrorCantidadCreate').text('');
        $('#validacionCantidadCreate').after('<p id="ErrorCantidadCreate" style="color:red">Campo Cantidad requerido</p>');
    }

    else {
        var PedidoDetalle = GetPedidoDetalles();
        $.ajax({
            url: "/Pedido/GuardarPedidoDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ PedidoDetalles: PedidoDetalle }),
            success: function (data) {
            }
        })
        .done(function (data) {
            if (data == 'El registro se guardo exitosamente') {
                location.reload();
                swal("El registro se guardó exitosamente!", "", "success");
            }
            else {
                location.reload();
                swal("El registro  no se guardó!", "", "error");
            }
        });
    }


    function GetPedidoDetalles() {
        var PedidoDetalle = {
            ped_Id: $('#ped_Id').val(),
            prod_Codigo: $('#prod_Codigo').val(),
            pedd_Cantidad: $('#pedd_Cantidad').val(),

        }
        return PedidoDetalle
    };
});