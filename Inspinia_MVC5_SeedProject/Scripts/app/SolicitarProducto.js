$(document).ready(function () {
    if (localStorage.getItem("MensajeExito")) {
        $('#msj_success').show();
        //console.log("Hola");
        var serialCode = localStorage.getItem("MensajeExito")
        var BodegaOrigen = localStorage.getItem("BodegaOrigen")
        $('#MensajeExito').text("El Pedido ha sido enviado correctamente, A la Bodega con código: " + BodegaOrigen + ". Codigo de referencia de la salida Generada es: " + serialCode);
        //console.log("serialCode", serialCode);
        localStorage.clear();
    }
    else {
        $('#msj_success').hide();
    }
    if (localStorage.getItem("MensajeError")) {
        $('#msj_failed').show();
        //console.log("Hola");
        var InfoMSJ = localStorage.getItem("MensajeError")
        $('#MensajeFallo').text(InfoMSJ);
        //console.log("serialCode", serialCode);
        localStorage.clear();
    }
    else {
        $('#msj_failed').hide();
    }
});
    $("#txt5").on("keypress keyup blur", function (event) {
        //this.value = this.value.replace(/[^0-9\.]/g,'');
        $(this).val($(this).val().replace(/[^0-9.\.]/g, ''));
        if ((event.which != 46 || $(this).val().indexOf('') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

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
            else if (CantidadSolicitada <= 0) {
                $("#ErrorValidacionGeneral").remove();
                $('#validationCantidad').text('');
                $('#errorCantidad').text('');
                $('#validationCantidad').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">Favor de Enviar una cantidad mayor a 0</ul>');
            }
            else if (CantidadDisponible < bodd_CantidadMinima) {
                $("#ErrorValidacionGeneral").remove();
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
                                data: JSON.stringify({ IDBodega: bod_Id, IDProducto: prod_Codigo, CantidadSolicitada: CantidadSolicitada, BodegaDestino: BodegaDestino, CantidadDisponible: data }),
                                success: function () {
                                },
                                error: function () {
                                    $('#validationDescripcionRol').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se pudo haacer el pedido, contacte con el administrador</ul>');
                                }
                            })
                                .done(function (data) {
                                    if (data == '-1') {
                                        localStorage.setItem("MensajeError", "No se pudo hacer el pedido, Contacte con el administración");
                                        location.reload('/ConsultarExistenciaProductos/Index');
                                    }
                                    else if (data == '-2') {
                                        $("#ErrorValidacionGeneral").remove();
                                        $('#validationCantidad').text('');
                                        $('#errorCantidad').text('');
                                        $('#validationCantidad').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">Esta bodega ya tiene pedidos pendientes por lo cuál la cantidad que solicito sobrepasa la cantidad minima del producto que esta bodega debe tener.</ul>');
                                    }
                                    else {
                                        localStorage.setItem("MensajeExito", data);
                                        localStorage.setItem("BodegaOrigen", bod_Id);
                                        location.reload('/ConsultarExistenciaProductos/Index');
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

