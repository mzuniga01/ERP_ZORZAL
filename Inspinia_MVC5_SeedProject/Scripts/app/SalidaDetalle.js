var contador = 0;

var CantidadExit = 0.00;

$(function () {
    $("#sal_FechaElaboracion").datepicker({
        dateFormat: 'mm-dd-yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
    }).datepicker("setDate", new Date());
});

function validateMyForm() {
    //Bodega
    var Bod = document.getElementById("bod_Id");
    var Bodega = Bod.options[Bod.selectedIndex].text;

    /////////
    //TipoSalida
    var TSal = document.getElementById("tsal_Id");
    var TipoSalida = TSal.options[TSal.selectedIndex].text;
    /////////
    var Fecha = $('#sal_FechaElaboracion').val();

    var BodDestino = $("#sal_BodDestino").val()
    var validacionFactura = $("#CodigoError").text()
    var sal_RazonDevolucion = $("#sal_RazonDevolucion").val()

    var currentRow = $("#tblSalidaDetalle tbody tr");
    var Tabla = currentRow.find("td:eq(0)").text();

    if (Fecha == '' || Fecha == null) {
        $('#sal_FechaElaboracionError').text('');
        $('#validationsal_FechaElaboracion').after('<ul id="sal_FechaElaboracionError" class="validation-summary-errors text-danger">Fecha Requerida</ul>');
        vFecha = false;
    }
    else {
        $('#sal_FechaElaboracionError').text('');
        vFecha = true
    }
    if (Bodega.startsWith("Seleccione")) {
        $('#bod_IdError').text('');
        $('#validationbod_Id').after('<ul id="bod_IdError" class="validation-summary-errors text-danger">Seleccione una Bodega</ul>');
        vBodega = false;
    }
    else {
        $('#bod_IdError').text('');

        vBodega = true
    }
    if (TipoSalida.startsWith("Seleccionar")) {
        $('#NombreError').text('');
        $('#validationtsal_Id').after('<ul id="NombreError" class="validation-summary-errors text-danger">Seleccione un tipo de salida</ul>');
        vTipoSalida = false;
    }
    else {
        $('#NombreError').text('');
        vTipoSalida = true;
        if (TipoSalida == "Prestamo") {
            if (BodDestino == 'Seleccione una Bodega de Destino') {
                $('#sal_BodDestinoError').text('');
                $('#validationsal_BodDestino').after('<ul id="sal_BodDestinoError" class="validation-summary-errors text-danger">Seleccione una bodega de Destino</ul>');
                vBodDestino = false;
                $('#sal_BodDestino').focus()

                vFactura = true;
                vDevolucion = true;
            }
            else {
                $('#sal_BodDestinoError').text('');

                vBodDestino = true;

                vFactura = true;
                vDevolucion = true;
            }
        }
        else {
            if (TipoSalida == "Venta") {
                var validacionFactura = $("#CodigoError").text()
                if (validacionFactura != '') {
                    vFactura = false;
                    $('#fact_Codigo').focus()

                    vBodDestino = true;
                    vDevolucion = true;
                }
                else {
                    $('#validationFactura').text('');
                    vFactura = true;

                    vBodDestino = true;
                    vDevolucion = true;
                }
            }
            else {
                if (TipoSalida == "DEVOLUCION") {
                    if (validacionFactura != 'Factura Disponible') {
                        $('#validationFactura').text('');
                        vDFactura = false;
                        $('#fact_Codigo').focus()
                    }
                    else {
                        vDFactura = true;
                    }
                    if (sal_RazonDevolucion == '') {
                        $('#sal_RazonDevolucionError').text('');
                        $('#validationsal_RazonDevolucion').after('<ul id="sal_RazonDevolucionError" class="validation-summary-errors text-danger">Campo Requerido</ul>');
                        $('#sal_RazonDevolucion').focus()
                        vRazonDevolucion = false;
                    }
                    else {
                        $('#sal_RazonDevolucionError').text('');
                        vRazonDevolucion = true;
                    }
                    if (!vDFactura || !vRazonDevolucion) {
                        vDevolucion = false;

                        vFactura = true;
                        vBodDestino = true;
                    }
                    else {
                        vDevolucion = true;

                        vFactura = true;
                        vBodDestino = true;
                    }
                }
            }
        }
    }
    if (Tabla == "No hay registros") {
        vSalidaDetalle = false;

        $('#alert_message').focus();

        $('#alert_message').html('<div class="alert alert-danger">No hay Detalle</div>');
        setInterval(function () {
            $('#alert_message').html('');
            $('#prod_CodigoBarras').focus()
        }, 5000);
    }
    else {
        vSalidaDetalle = true;
    }
    if (!vFecha || !vBodega || !vTipoSalida || !vBodDestino || !vFactura || !vDevolucion || !vSalidaDetalle) {
        return false;
    }
    else {
        return true
    }
}

function fBodega() {
    var e = document.getElementById("bod_Id");
    var vBodega = e.options[e.selectedIndex].text;
    return vBodega
};

function TipoSalida() {
    var e = document.getElementById("tsal_Id");
    var vTipoSalida = e.options[e.selectedIndex].text;
    return false;
    return vTipoSalida
};

$(document).ready(function () {
    $('#tbSalidaDetalle').DataTable(
        {
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
});

function ProductoCantidad(bod_Id, prod_Codigo) {
    $("#prod_CodigoBarras").val();
    var cod_Barras = $("#prod_CodigoBarras").val();
    return $.ajax({
        url: "/Salida/Cantidad",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            bod_Id: bod_Id,
            prod_Codigo: prod_Codigo
        }),
        success: function (data) {
            var CantidadAceptada = data.CantidadAceptada
            var CantidadMinima = data.CantidadMinima
            var prod_CodigoCampo = $('#prod_Codigo').val();
            var currentRow = $("#tblSalidaDetalle tbody tr");
            var prod_CodigoTabla = currentRow.find("td:eq(0)").text();
            if (prod_CodigoTabla != "No hay registros") {
                $("#tblSalidaDetalle >tbody >tr").each(function () {
                    console.log(prod_CodigoTabla)

                    if (prod_CodigoCampo == prod_CodigoTabla) {
                        var CantidadTabla = currentRow.find("td:eq(7)").text();

                        var CantidatTotal = parseFloat(CantidadAceptada) + parseFloat(CantidadMinima)
                        var CantidadRestante = parseFloat(CantidadAceptada) - parseFloat(CantidadTabla)
                        if (CantidadTabla >= CantidadAceptada && CantidadRestante > 0) {
                            $("#CantidaExistenteProd").text('');
                            $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Minima Alcanzada solo hay en existencia: ' + CantidadRestante + ' de este producto</ul>');
                        }
                        else {
                            if (CantidadTabla < CantidadAceptada) {
                                CantidadAceptada = CantidadAceptada - CantidadTabla
                                $("#CantidaExistenteProd").text('');
                                $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Existente en Bodega: ' + CantidadAceptada + '</ul>');
                            }
                            else {
                                if (CantidadTabla == CantidadAceptada) {
                                    $("#CantidaExistenteProd").text('');
                                    $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Sin Existencias de este producto</ul>');
                                }
                            }
                        }
                    }
                })
            }
            else {
                $("#CantidaExistenteProd").text('');
                Aceptada = CantidadAceptada
                if (CantidadAceptada == 0 && CantidadMinima > 0) {
                    $("#CantidaExistenteProd").text('');
                    $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Minima Alcanzada solo hay en existencia: ' + CantidadMinima + ' de este producto</ul>');
                }
                else {
                    $("#CantidaExistenteProd").text('');
                    $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Existente en Bodega: ' + Aceptada + '</ul>');
                }
            }
        }
    })
};

function GetSalidaDetalle() {
    var SalidaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        sald_Cantidad: $('#sald_Cantidad').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}

function CantidadExiste(Cod_Producto, CantidadAceptada) {
    if ($('#tblSalidaDetalle >tbody >tr').length > 0) {
        $('#tblSalidaDetalle >tbody >tr').each(function () {
            var prod_CodigoTabla = $(this).find("td:eq(0)").text()
            if (Cod_Producto == prod_CodigoTabla) {
                var CantidadTabla = $(this).find("td:eq(7)").text();
                var CantidadExit = parseFloat(CantidadAceptada) - parseFloat(CantidadTabla);

                return CantidadExit
            }
            else {
            }
        })
    }
}
$('#sal_BodDestino').click(function () {
    var Bod = document.getElementById("bod_Id");
    var Bodega = Bod.options[Bod.selectedIndex].text;
    if (Bodega.startsWith("Seleccione")) {
        $('#bod_IdError').text('');
        $('#validationbod_Id').after('<ul id="bod_IdError" class="validation-summary-errors text-danger">Seleccione una Bodega</ul>');
    }
    else {
    }
})

$('#AgregarSalidaDetalle').click(function () {
    var table = $('#tblSalidaDetalle').DataTable();
    var counter = 0;
    var bod_Id = $('#bod_Id').val();
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Unidad_Medida = $('#pscat_Id').val();
    var Cantidad = $('#sald_Cantidad').val();
    var data_producto = $("#prod_Codigo").val();
    $('#sald_CantidadError').text();
    ProductoCantidad(bod_Id, Cod_Producto).done(function (data) {
        var CantidadAceptada = data.CantidadAceptada
        var CantidadMinima = data.CantidadMinima

        if ($('#tblSalidaDetalle >tbody >tr').length > 0) {
            $('#tblSalidaDetalle >tbody >tr').each(function () {
                var prod_CodigoTabla = $(this).find("td:eq(0)").text()
                if (Cod_Producto == prod_CodigoTabla) {
                    var CantidadTabla = $(this).find("td:eq(7)").text();
                    CantidadExit = parseFloat(CantidadAceptada) - parseFloat(CantidadTabla);
                }
                else {
                    CantidadExit = CantidadAceptada
                }
            })
        }
        else {
            CantidadExit = CantidadAceptada
        }
        console.log(CantidadExit)
        if (Producto == '') {
            $('#MessageError').text('');
            $('#CodigoError').text('');
            $('#ValidationCodigoCreateError').text('');
            $('#ValidationCodigoCreate').after('<ul id="ValidationCodigoCreateError" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
        }
        else if (Cantidad == '' || Cantidad < 0.25) {
            $('#MessageError').text('');
            $('#sald_CantidadExedError').text('');
            $('#sald_CantidadError').text('');
            $('#sald_Cantidad').after('<ul id="sald_CantidadError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
        }
        else if (Cantidad > CantidadExit) {
            $('#MessageError').text('');
            $('#sald_CantidadError').text('');
            $('#sald_CantidadExedError').text('');
            $('#sald_Cantidad').after('<ul id="sald_CantidadExedError" class="validation-summary-errors text-danger">Cantidad Superada</ul>');
        }
        else {
            $('#ValidationCodigoCreateError').text('');
            $('#sald_CantidadError').text('');
            $('#sald_CantidadExedError').text('');
            $('#CantidaExistenteProd').text('');
            var tbSalidaDetalle = GetSalidaDetalle();
            $.ajax({
                url: "/Salida/SaveSalidaDetalle",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle, data_producto: data_producto }),
            }).done(function (datos) {
                if (datos == data_producto) {
                    console.log('Repetido');
                    var cantfisica_nueva = $('#sald_Cantidad').val();
                    $("#tblSalidaDetalle td").each(function () {
                        var prueba = $(this).text()
                        if (prueba == data_producto) {
                            t.row($(this).parents('tr')).remove().draw();
                            var idcontador = $(this).closest('tr').data('id');
                            var cantfisica_anterior = $(this).closest("tr").find("td:eq(7)").text();
                            var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);

                            table.row.add([
                                $('#prod_Codigo').val(),
                                $('#prod_Descripcion').val(),
                                $('#prod_Marca').val(),
                                $('#prod_Modelo').val(),
                                $('#prod_Talla').val(),
                                $('#pcat_Id').val(),
                                $('#uni_Id').val(),
                                sumacantidades,
                                '<button id = "removeSalidaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">-</button>'

                            ]).draw(false);
                        }
                    });
                } else {
                    table.row.add([
                        $('#prod_Codigo').val(),
                        $('#prod_Descripcion').val(),
                        $('#prod_Marca').val(),
                        $('#prod_Modelo').val(),
                        $('#prod_Talla').val(),
                        $('#pcat_Id').val(),
                        $('#uni_Id').val(),
                        $('#sald_Cantidad').val(),
                        '<button id = "removeSalidaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">-</button>'

                    ]).draw(false);
                }
            }).done(function (data) {
                $('#prod_Codigo').val('');
                $('#prod_Descripcion').val('');
                $('#pscat_Id').val('');
                $('#uni_Id').val('');
                $('#pcat_Id').val('');

                $("#prod_CodigoBarras").val('');
                $('#sald_Cantidad').val('');
                $('#Error_Barras').text('');
                $('#NombreError').text('');
                console.log('Hola');
            });
        }
    });
});
function SeleccionProductoTabla() {
    console.log(cantfisica_anterior)
    $("#tblSalidaDetalle td").each(function () {
        var prueba = $(this).text()
        if (prueba == data_producto) {
            var idcontador = $(this).closest('tr').data('id');
            var cantfisica_anterior = $(this).closest("tr").find("td:eq(7)").text();
            console.log(cantfisica_anterior)
        }
    })
}

$('#prod_Codigo').click(function () {
    SeleccionProductoTabla()
})