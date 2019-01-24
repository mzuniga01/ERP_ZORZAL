
function SolicitarProducto(bod_Id, bod_Nombre, suc_Descripcion, prod_Codigo, prod_Descripcion, prod_Marca, bodd_CantidadExistente, bodd_CantidadMinima, BodegaDestino) {
    $('#txt1').val(suc_Descripcion);
    $('#txt2').val(bod_Nombre);
    $('#txt3').val(prod_Descripcion);
    $('#txt4').val(prod_Marca);

    $('#btnGuardarSalida').click(function () {
        $("#ErrorValidacionGeneral").remove();
        $("#errorDescripcionRol").remove();
        
        var CantidadSolicitada = $('#txt5').val();
        var CantidadDisponible = bodd_CantidadExistente - CantidadSolicitada
        if (CantidadSolicitada == '') {
            $("#ErrorValidacionGeneral").remove();
            $('#validationCantidad').text('');
            $('#errorCantidad').text('');
            $('#validationCantidad').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">Favor de Enviar una cantidad</ul>');
        }
        else if (CantidadDisponible < bodd_CantidadMinima) {
            $('#validationCantidad').text('');
            $('#errorCantidad').text('');
            $('#validationCantidad').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No puede pedir esa Cantidad, porque sobrepasa la cantidad minima, Intente con una cantidad menor.</ul>');
        }
        else {
            $.ajax({
                url: "/ConsultarExistenciaProductos/ValidacionPorBodega",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ IDBodega: bod_Id, IDProducto: prod_Codigo })
            })
            .done(function (data) {
                if (data == '-1') {
                    $("#ErrorValidacionGeneral").remove();
                    $('#validationCantidad').text('');
                    $('#errorCantidad').text('');
                    $('#validationCantidad').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se pudo hacer el pedido, Esta bodega ya tiene demasiadas solicitudes.</ul>');
                }
                else {
                    $.ajax({
                        url: "/ConsultarExistenciaProductos/InsertPedido",
                        method: "POST",
                        dataType: 'json',
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ IDBodega: bod_Id, IDProducto: prod_Codigo, CantidadSolicitada: CantidadSolicitada, BodegaDestino: BodegaDestino, CantidadNoDisponible: data }),
                        success: function (json) {
                        },
                        error: function () {
                            $('#validationDescripcionRol').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se pudo añadir la pantalla, contacte con el administrador</ul>');
                        }
                    })
            .done(function (data) {
                if (data == '-1') {
                    $("#dialog").dialog({
                        modal: true,
                        title: "Fallo",
                        buttons: [
                          {
                              text: "No se pudo hacer el pedido, Contacte con el administración"
                          }
                        ]
                    }).prev(".ui-widget-header").css("background", "#FF3232");
                }
                else if (data == '-2') {
                    $("#ErrorValidacionGeneral").remove();
                    $('#validationCantidad').text('');
                    $('#errorCantidad').text('');
                    $('#validationCantidad').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">Esta bodega ya tiene pedidos pendientes porlo cual la cantidad que solicito sobrepasa la cantidad minima del producto que esta bodega de tener.</ul>');
                }
                else {
                    $("#dialog").dialog({
                        modal: true,
                        title: "Exito",
                        buttons: [
                          {
                              text: "El Pedido ha sido enviado correctamente. Codigo de referencia de la salida Generada es: " + data
                          }
                        ]
                    }).prev(".ui-widget-header").css("background", "#64FF00");
                    $("#ErrorValidacionGeneral").remove();
                    $("#errorDescripcionRol").remove();
                }
            })
                    $('#SolicitarProductomodal').modal('hide');
                    $('#txt5').val("");
                }
            })
                }
            })
    $('#btnCerrar').click(function () {
        $("#ErrorValidacionGeneral").remove();
        $("#errorDescripcionRol").remove();
        $('#txt5').val("");
    })
    $('#CerrarX').click(function () {
        $("#ErrorValidacionGeneral").remove();
        $("#errorDescripcionRol").remove();
        $('#txt5').val("");
    })
    
}
