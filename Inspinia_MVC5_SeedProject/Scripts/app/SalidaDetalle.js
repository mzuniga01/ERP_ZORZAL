var contador = 0;

function validateMyForm() {
    var Fecha = $('#sal_FechaElaboracion').val();
    var TipoSalida = $('#tsal_Id').val()

    var BodDestino = $("#sal_BodDestino").val()
    var validacionFactura = $("#CodigoError").text()
    var sal_RazonDevolucion = $("#sal_RazonDevolucion").val()

    var tblSalidaDetalle = $('#tblSalidaDetalle >tbody >tr').length;
    console.log();
    if (Fecha == '' || Fecha == null) {
        $('#sal_FechaElaboracionError').text('');
        $('#validationsal_FechaElaboracion').after('<ul id="sal_FechaElaboracionError" class="validation-summary-errors text-danger">Fecha Requerida</ul>');
        vFecha = false;
    }
    else {
        vFecha = true
    }
    if (TipoSalida == '') {
        $('#NombreError').text('');
        $('#validationtsal_Id').after('<ul id="NombreError" class="validation-summary-errors text-danger">Seleccione un tipo de salida</ul>');
        vTipoSalida = false;
    }
    else {
        vTipoSalida = true;
        if (TipoSalida == 1) {
            if (BodDestino == 'Seleccione una Bodega de Destino') {
                $('#sal_BodDestinoError').text('');
                $('#validationsal_BodDestino').after('<ul id="sal_BodDestinoError" class="validation-summary-errors text-danger">Seleccione una bodega de Destino</ul>');
                vBodDestino = false;
                $('#sal_BodDestino').focus()

                vFactura = true;
                vDevolucion = true;
            }
            else {
                vBodDestino = true;

                vFactura = true;
                vDevolucion = true;
            }
        }
        else {
            if (TipoSalida == 2) {
                var validacionFactura = $("#CodigoError").text()
                if (validacionFactura != '') {
                    vFactura = false;
                    $('#fact_Codigo').focus()

                    vBodDestino = true;
                    vDevolucion = true;
                    //$('#FacturaError').text('');
                    //$('#validationFactura').after('<ul id="FacturaError" class="validation-summary-errors text-danger">Factura</ul>');
                }
                else {
                    vFactura = true;

                    vBodDestino = true;
                    vDevolucion = true;
                }
            }
            else {
                if (TipoSalida == 3) {
                    if (validacionFactura != '') {
                        vDFactura = false;
                        $('#fact_Codigo').focus()
                        //$('#FacturaError').text('');
                        //$('#validationFactura').after('<ul id="FacturaError" class="validation-summary-errors text-danger">Factura</ul>');
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
    if (tblSalidaDetalle < 1) {
        vSalidaDetalle = false;
        $('#exampleModalCenter').modal('show');
        $("#exampleModalCenter").on('hidden.bs.modal', function () {
            $('#prod_CodigoBarras').focus()
        });
        console.log(vSalidaDetalle + ' 0')
    }
    else {
        vSalidaDetalle = true;
        console.log(vSalidaDetalle + ' 1')
    }
    if (!vFecha || !vTipoSalida || !vBodDestino || !vFactura || !vDevolucion || !vSalidaDetalle) {
        return false;
    }
    else {
        return true
    }
}


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

$(document).on("click", "#Table_BuscarProductoD tbody tr td button#seleccionar", function () {
    var bod_Id = $('#bod_Id').val()
    var prod_CodigoBarrasItem = $(this).closest('tr').data('cod_barra');
    Producto(bod_Id, prod_CodigoBarrasItem)
});



function ProductoCantidad(bod_Id, prod_Codigo) {
    $("#prod_CodigoBarras").val();
    var cod_Barras = $("#prod_CodigoBarras").val();
    $.ajax({
        url: "/Salida/Cantidad",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            bod_Id: bod_Id,
            prod_Codigo: prod_Codigo
        }),
    }).done(function (data) {
        $('#CantidaExistenteProd').text('');
        $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Existente en Bodega: ' + data + '</ul>');


        //$('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="alert alert-success">Cantidad Existente en Bodega: ' + data + '</ul>')

        //console.log(elemento)
        //if (elemento != '') {
        //    console.log(elemento)
        //    //('#CantidaExistente').removeChild(div.lastChild);class="validation-summary-errors text-danger"

        //    var elemento = document.getElementById('CantidaExistenteProd');
        //    elemento.removeAttribute("class");
        //    console.log(elemento)


        //}

    })
};

function Producto(bod_Id, prod_CodigoBarrasItem) {
    
    $("#prod_CodigoBarras").val();
    var cod_Barras = $("#prod_Codigo").val();
    $.ajax({
        url: "/Salida/GetProdCodBar",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            bod_Id: bod_Id,
            prod_CodigoBarras: prod_CodigoBarrasItem
        }),
    }).done(function (data) {
        if (data.length > 0) {
            $.each(data, function (key, value) {
                $("#prod_CodigoBarras").val(value.prod_CodigoBarras);
                $('#prod_Codigo').val(value.prod_Codigo);
                $('#prod_Descripcion').val(value.prod_Descripcion);
                $("#uni_Id").val(value.uni_Descripcion);
                $("#pscat_Id").val(value.pscat_Descripcion);
                $("#pcat_Id").val(value.pcat_Nombre);
                ProductoCantidad(bod_Id, value.prod_Codigo);
            })
            $("#ModalAgregarProducto").on('hidden.bs.modal', function () {
                $('#prod_CodigoBarras').text('');
                $('#sald_Cantidad').text('');
                $("#sald_Cantidad").focus();
            });
        }
        else {
            $('#Error_Barras').text('');
            $('#validationprod_CodigoBarras').after('<ul id="Error_Barras" class="validation-summary-errors text-danger">*Producto no existe</ul>');

            idItem = $(this).closest('tr').data('id');
            contentItem = $(this).closest('tr').data('content');
            uni_IdtItem = $(this).closest('tr').data('keyboard');
            psubctItem = $(this).closest('tr').data('container');
            pcatItem = $(this).closest('tr').data('pcat');
            prod_CodigoBarrasItem = $(this).closest('tr').data('cod_barras');
            $("#prod_Codigo").val(idItem);
            $("#prod_Descripcion").val(contentItem);
            $("#uni_Id").val(uni_IdtItem);
            $("#pscat_Id").val(psubctItem);
            $("#pcat_Id").val(pcatItem);
            $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
            $("#sald_Cantidad").focus();
        }
    });
    return false;
}

//changeState(this, document.getElementById('nombre'))

//function changeState(check, element, valor) {
//    if (!check.checked) {
//        element.setAttribute('max', valor); //atributo y valor
//    } else {
//        element.removeAttribute('max'); //el atributo
//    }
//}

//Agregar detalle por medio de codigo de Barra
//$('#prod_CodigoBarras').focus(function () {
//    ListaProductos()
//})

var contador = 0;

$(document).keypress(function (e) {
    console.log('Hola', e.target.id);
    var IDInput = e.target.id;
    if (e.which == 13) {
        if (IDInput == 'prod_CodigoBarras') {
            var bod_Id = $('#bod_Id').val()
            var prod_CodigoBarras = $('#prod_CodigoBarras').val()
            Producto(bod_Id, prod_CodigoBarras);
            return false;
        }
        else
            return false;
    }
});

function GetSalidaDetalle() {
    var SalidaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        sald_Cantidad: $('#sald_Cantidad').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}

$('#AgregarSalidaDetalle').click(function () {
    var bodd_Id = $('#bodd_Id').val();
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Unidad_Medida = $('#pscat_Id').val();
    var Cantidad = $('#sald_Cantidad').val();
    var data_producto = $("#prod_Codigo").val();
    if (Producto == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationCodigoCreate').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    else if (Cantidad == '' || Cantidad < 1) {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#sald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
    }
    else {
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
                        var idcontador = $(this).closest('tr').data('id');
                        var cantfisica_anterior = $(this).closest("tr").find("td:eq(3)").text();
                        var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);
                        console.log(sumacantidades);
                        $(this).closest('tr').remove();
                        copiar = "<tr data-id=" + idcontador + ">";
                        copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
                        copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
                        copiar += "<td id = 'Unidad_Medida'>" + $('#uni_Id').val() + "</td>";
                        copiar += "<td id = 'Cantidad'>" + sumacantidades + "</td>";
                        copiar += "<td>" + '<button id="removeSalidaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                        copiar += "</tr>";
                        $('#tblSalidaDetalle').append(copiar);
                    }
                });
            } else {
                copiar = "<tr data-id=" + contador + ">";
                copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
                copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
                copiar += "<td id = 'Unidad_Medida'>" + $('#pscat_Id').val() + "</td>";
                copiar += "<td id = 'Cantidad'>" + $('#sald_Cantidad').val() + "</td>";
                copiar += "<td>" + '<button id="removeSalidaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                copiar += "</tr>";
                $('#tblSalidaDetalle').append(copiar);
            }
        }).done(function (data) {
            $('#prod_Codigo').val('');
            $('#prod_Descripcion').val('');
            $('#pscat_Id').val('');
            $('#uni_Id').val('');
            $('#pcat_Id').val('');

            $("#prod_CodigoBarras").val('');
            $('#sald_Cantidad').val('0.00');
            $('#Error_Barras').text('');
            $('#NombreError').text('');
            console.log('Hola');
        });
    }
});

function SeleccionProducto() {
    var bodd_Id = $('#bodd_Id').val();
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var CodigoBarras = $('#prod_CodigoBarras').val();
    var Cantidad = $('#sald_Cantidad').val();
    var data_producto = $("#prod_Codigo").val();

    if (CodigoBarras == '') {
        $('#prod_CodigoBarrasError').text('');
        $('#validationprod_CodigoBarras').after('<ul id="prod_CodigoBarrasError" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
        vProducto = false;
    }
    else {
        vProducto = true;
    }
    if (Cantidad == '' || Cantidad < 0.01) {
        $('#sald_CantidadError').text('');
        $('#sald_Cantidad').after('<ul id="sald_CantidadError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
        vCantidad = false;
    }
    else {
        vCantidad = true;
    }
    if (vProducto || vCantidad) {
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
                        var idcontador = $(this).closest('tr').data('id');
                        var cantfisica_anterior = $(this).closest("tr").find("td:eq(3)").text();
                        var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);
                        console.log(sumacantidades);
                        $(this).closest('tr').remove();
                        copiar = "<tr data-id=" + idcontador + ">";
                        copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
                        copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
                        copiar += "<td id = 'Unidad_Medida'>" + $('#uni_Id').val() + "</td>";
                        copiar += "<td id = 'Cantidad'>" + sumacantidades + "</td>";
                        copiar += "<td>" + '<button id="removeSalidaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                        copiar += "</tr>";
                        $('#tblSalidaDetalle').append(copiar);
                    }
                });
            } else {
                copiar = "<tr data-id=" + contador + ">";
                copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
                copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
                copiar += "<td id = 'Unidad_Medida'>" + $('#pscat_Id').val() + "</td>";
                copiar += "<td id = 'Cantidad'>" + $('#sald_Cantidad').val() + "</td>";
                copiar += "<td>" + '<button id="removeSalidaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                copiar += "</tr>";
                $('#tblSalidaDetalle').append(copiar);
            }
        }).done(function (data) {
            $('#prod_Codigo').val('');
            $('#prod_Descripcion').val('');
            $('#pscat_Id').val('');
            $('#uni_Id').val('');
            $('#pcat_Id').val('');
            $("#prod_CodigoBarras").val('');
            $('#sald_Cantidad').val('0.00');
            $('#Error_Barras').text('');
            $('#NombreError').text('');
            console.log('Hola');
        });
    }
    else {
    }
};

$(document).on("click", "#tblSalidaDetalle tbody tr td button#removeSalidaDetalle", function () {
    contador = contador - 1;
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    console.log(idItem);
    var SalidaDetalle = {
        sald_UsuarioCrea: idItem,
    };
    var vprod_Codigo = $(this).closest("tr").find("td:eq(0)").text();
    var tbSalidaDetalle = {
        prod_Codigo: vprod_Codigo,
        sald_UsuarioCrea: vprod_Codigo
    };
    $.ajax({
        url: "/Salida/RemoveSalidaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle }),
    });
});

function DeleteSalidaDetalle(sald_Id) {
    $.ajax({
        url: "/Salida/DeleteSalidaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ sald_Id: sald_Id }),
    })
        .done(function (data) {
            console.log(data);
            location.reload();
        })
}

function EditSalidaDetalles(sald_Id) {
    $.ajax({
        url: "/Salida/getSalidaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ sald_Id: sald_Id }),
    })
        .done(function (data) {
            console.log(data);
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    console.log(item);
                    $("#sal_Id_SD").val(item.sal_Id);
                    $("#sald_Id_SD").val(item.sald_Id);
                    $("#prod_Codigo_SD").val(item.prod_Codigo);
                    $("#sald_Cantidad_SD").val(item.sald_Cantidad);
                    $("#prod_Descripcion_SD").val(item.prod_Descripcion);
                    $("#pcat_Nombre_SD").val(item.pcat_Nombre);
                    $("#pscat_Descripcion_SD").val(item.pscat_Descripcion);
                    $("#uni_Descripcion_SD").val(item.uni_Descripcion);
                    $("#EditSalidaDetalle").modal();
                })
            }
        })
}

$("#BtnsubmitEdit").click(function () {
    var Cantidad = $('#sald_Cantidad_SD').val();
    if (Cantidad == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#validationsald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
    }
    else if (Cantidad < '0.05') {
        $('#NombreError').text('');
        $('#validationsald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad muy Pequeña</ul>');
    }
    else {
        var sal_id = $('#sald_Id').val();
        var data = $("#SubmitForm").serializeArray();
        $.ajax({
            type: "Post",
            url: "/Salida/EditSalidaDetalle",
            data: data,
            success: function (result) {
                if (result == '-1')
                    $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
                else
                    console.log(result);
                window.location.href = '/Salida/Edit/' + result;
            }
        });
    }
})

//////////////////////////////////////////////////////////////////////////////////////
//Edit

function GetSalidaDetalle() {
    var SalidaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        sald_Cantidad: $('#sald_Cantidad').val(),
        sal_Id: $('#sal_Id').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}

$('#AgregarSalidaDetalleEdit').click(function () {
    SeleccionProducto()
});

function GetNewSalidaDetalle() {
    var SalidaDetalle = {
        sal_Id: $('#sal_Id').val(),
        prod_Codigo: $('#prod_Codigo').val(),
        sald_Cantidad: $('#sald_Cantidad').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}

$('#btnCreateSalidaDetalle').click(function () {
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Cantidad = $('#sald_Cantidad').val();
    console.log('Funca');
    var tbSalidaDetalle = GetNewSalidaDetalle();
    $.ajax({
        url: "/Salida/SaveNewDatail",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle }),
    })
        .done(function (data) {
            if (data == 'El registro se guardo exitosamente') {
                location.reload();
                swal("El registro se almacenó exitosamente!", "", "success");
            }
            else {
                location.reload();
                swal("El registro  no se almacenó!", "", "error");
            }
        });
})

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




//var inputs = document.getElementById('sald_Cantidad')

////changeState(this, document.getElementById('nombre'))
//changeState(this, inputs, data)
//function changeState(check, element, valor) {
//    if (!check.checked) {
//        element.setAttribute('max', valor); //atributo y valor
//    } else {
//        element.removeAttribute('max'); //el atributo
//    }
//}




//$(document).on("click", "#Table_BuscarProductoD tbody tr td button#seleccionar", function () {
//    var bod_Id = $('#bod_Id').val()
//    var prod_CodigoBarrasItem = $(this).closest('tr').data('cod_barra');
//    Producto()
//});
////

//function GetSalidaDetalles() {
//    var table = '#Table_BuscarProductoD tbody tr td button#seleccionar'
//    var SalidaDetalle = {
//        prod_Codigo: $('#prod_Codigo').val(),
//        bod_Id: $('#bod_Id').val(),
//        prod_CodigoBarras: $(table).closest('tr').data('cod_barra'),
//        sald_Cantidad: $('#sald_Cantidad').val(),
//        sald_UsuarioCrea: contador
//    };
//    return SalidaDetalle;
//}

//function Producto(SalidaDetalle) {
//    var SalidaDetalles = GetSalidaDetalles
//    $("#prod_CodigoBarras").val();
//    console.log(SalidaDetalles().prod_CodigoBarras);
//    var cod_Barras = $("#prod_CodigoBarras").val();
//    $.ajax({
//        url: "/Salida/GetProdCodBar",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({
//            bod_Id: SalidaDetalle.bod_Id,
//            prod_CodigoBarras: Producto
//        }),
//    }).done(function (data) {
//        console.log(data);
//        if (data.length > 0) {
//            $.each(data, function (key, value) {
//                $("#prod_CodigoBarras").val(value.prod_CodigoBarras);
//                $('#prod_Codigo').val(value.prod_Codigo);
//                $('#prod_Descripcion').val(value.prod_Descripcion);
//                $("#uni_Id").val(value.uni_Descripcion);
//                $("#pscat_Id").val(value.pscat_Descripcion);
//                $("#pcat_Id").val(value.pcat_Nombre);
//            })
//            $('#prod_CodigoBarras').text('');
//            $('#sald_Cantidad').text('');
//            $("#sald_Cantidad").focus();
//        }
//        else {
//            $('#prod_CodigoBarras').text('');
//            $('#validationprod_CodigoBarras').after('<ul id="Error_Barras" class="validation-summary-errors text-danger">*Producto no existe</ul>');

//            idItem = $(this).closest('tr').data('id');
//            contentItem = $(this).closest('tr').data('content');
//            uni_IdtItem = $(this).closest('tr').data('keyboard');
//            psubctItem = $(this).closest('tr').data('container');
//            pcatItem = $(this).closest('tr').data('pcat');
//            prod_CodigoBarrasItem = $(this).closest('tr').data('cod_barras');
//            $("#prod_Codigo").val(idItem);
//            $("#prod_Descripcion").val(contentItem);
//            $("#uni_Id").val(uni_IdtItem);
//            $("#pscat_Id").val(psubctItem);
//            $("#pcat_Id").val(pcatItem);
//            $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
//            $("#sald_Cantidad").focus();
//        }
//    });
//    return false;
//}

//Tabla del Detalle
//$(document).ready(function () {
//    $('#tblSalidaDetalle1').DataTable(
//        {
//            "searching": true,
//            "lengthChange": false,
//            "oLanguage": {
//                "oPaginate": {
//                    "sNext": "Siguiente",
//                    "sPrevious": "Anterior",
//                },
//                "sEmptyTable": "Agregue un Producdo",
//                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
//                "sSearch": "Buscar",
//                "sInfo": "Mostrando _START_ a _END_ Entradas",
//            }
//        });

//    var $rows = $('#tblSalidaDetalle1 tr');
//    $("#search").keyup(function () {
//        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

//        $rows.show().filter(function () {
//            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
//            return !~text.indexOf(val);
//        }).hide();
//    });
//});

//$(document).ready(function () {
//    var Name = '#tbSalidaDetalle';

//    Datatable(Name)
//});

//function Datatable(Name) {
//    $(Name).DataTable(
//        {
//            "searching": false,
//            "lengthChange": false,

//            "oLanguage": {
//                "oPaginate": {
//                    "sNext": "Siguiente",
//                    "sPrevious": "Anterior",
//                },
//                "sProcessing": "Procesando...",
//                "sLengthMenu": "Mostrar _MENU_ registros",
//                "sZeroRecords": "No se encontraron resultados",
//                "sEmptyTable": "Ningún dato disponible en esta tabla",
//                "sEmptyTable": "No hay registros",
//                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
//                "sSearch": "Buscar",
//                "sInfo": "Mostrando _START_ a _END_ Entradas",
//                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",

//            }
//        });
//};

////Tabla de Busqueda Generica
//$(document).ready(function () {
//    $('#Table_BuscarProducto').DataTable(
//        {
//            "searching": false,
//            "lengthChange": false,

//            "oLanguage": {
//                "oPaginate": {
//                    "sNext": "Siguiente",
//                    "sPrevious": "Anterior",
//                },
//                "sEmptyTable": "No hay registros",
//                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
//                "sSearch": "Buscar",
//                "sInfo": "Mostrando _START_ a _END_ Entradas",

//            }
//        });

//    var $rows = $('#Table_BuscarProducto tr');
//    $("#search").keyup(function () {
//        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

//        $rows.show().filter(function () {
//            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
//            return !~text.indexOf(val);
//        }).hide();
//        //$rows.show().filter(function () {
//        //    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
//        //    return !~text.indexOf(val);
//        //}).hide();

//    });
//});

//function GetProdCodBar() {
//    var bod_Id = $('#bod_Id').val()
//    var prod_CodigoBarras = $('#prod_CodigoBarras').val()
//    $.ajax({
//        url: "/Salida/GetProdCodBar",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ bod_Id: bod_Id, prod_CodigoBarras: prod_CodigoBarras }),
//    })
//        .done(function (data) {
//            console.log(data)
//            $.each(data, function (key, value) {
//                $("#prod_CodigoBarras").val(value.prod_CodigoBarras);
//                $("#prod_Descripcion").val(value.prod_Codigo);
//                $("#sald_Cantidad").focus();
//            })
//        })
//}
//    $('#prod_CodigoBarras').keydown(function (e) {
//        if (e.keyCode == 13) {
//            var bod_Id = $('#bod_Id').val()
//            var prod_CodigoBarras = $('#prod_CodigoBarras').val()
//            $.ajax({
//                url: "/Salida/GetProdCodBar",
//                method: "POST",
//                dataType: 'json',
//                contentType: "application/json; charset=utf-8",
//                data: JSON.stringify({ bod_Id: bod_Id, prod_CodigoBarras: prod_CodigoBarras }),
//            })
//                .done(function (data) {
//                    console.log(data)
//                    $.each(data, function (key, value) {
//                        $("#prod_CodigoBarras").val(data.prod_Codigo);
//                        $("#prod_Codigo").val(data.prod_Descripcion);
//                        $("#prod_Descripcion").val(value.prod_Descripcion);
//                        $("#uni_Id").val(uni_IdtItem);
//                        $("#pscat_Id").val(psubctItem);
//                        $("#pcat_Id").val(pcatItem);
//                        $("#sald_Cantidad").focus();
//                    })
//                })
//                $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
//                $("#prod_Codigo").val(idItem);
//                $("#prod_Descripcion").val(contentItem);
//                $("#uni_Id").val(uni_IdtItem);
//                $("#pscat_Id").val(psubctItem);
//                $("#pcat_Id").val(pcatItem);
//                $("#sald_Cantidad").focus();
//            //$("#cod").val(idItem);
//            return false;
//        }
//            });
//            console.log('prueba');
//            $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
//            $("#prod_Codigo").val(idItem);
//            $("#prod_Descripcion").val(contentItem);
//            $("#uni_Id").val(uni_IdtItem);
//            $("#pscat_Id").val(psubctItem);
//            $("#pcat_Id").val(pcatItem);
//            $("#sald_Cantidad").focus();
//            return false;
//    );

//function GetSalidaDetalles() {
//    var SalidaDetalle = {
//        prod_Codigo: $('#prod_Codigo').val(),
//        sald_Cantidad: $('#sald_Cantidad').val(),
//        sald_UsuarioCrea: contador
//    };
//    return SalidaDetalle;
//}

//$("#sal_FechaElaboracion").click(function () {
//    var SalidaDetalles2c = GetSalidaDetalles
//    $("#tblSalidaDetalle").length;
//    console.log(SalidaDetalles2c().prod_Codigo);

//})

//function GetProdCodBar() {
//    var bod_Id = $('#bod_Id').val()
//    var prod_CodigoBarras = $('#prod_CodigoBarras').val()
//    $.ajax({
//        url: "/Salida/GetProdCodBar",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ bod_Id: bod_Id, prod_CodigoBarras: prod_CodigoBarras }),
//    })
//        .done(function (data) {
//            $('#CodigoError').text('');
//            $('#validationFactura').after('<ul id="CodigoError" class="validation-summary-errors text-danger">' + data + '</ul>');
//        })
//}
//function test() {
//    $.ajax({
//        method: "POST",
//        url: "/Salida/GetProdList",
//        contentType: "application/json; charset=utf-8",
//        dataType: 'json',
//    }).done(function (info) {
//        console.log(info);
//    });
//}
//function ListaProductos() {
//    var table = $('#Table_BuscarProductoD').dataTable({
//        destroy: true,
//        resposive: true,
//        ajax: {
//                method: "POST",
//                url: "/Salida/GetProdList",
//                contentType: "application/json; charset=utf-8",
//                dataType: 'json',
//                dataSrc: "d"
//        },
//        columns: [
//                     { "d": ".prod_Codigo"}
//                 ]
//    })
//}

//$(document).ready(function () {
//    console.log("ready")
//    ListaProductos()
//});

//$.when(LimpiarTable()).then(function () {
//    $(document).ready(function () {
//        console.log("ready")
//        ListaProductos()
//    });
//});

//$('#submit').attr('value = "SICambio"')

//$(document).ready( function () {
//    var e = document.getElementById("bod_Id");
//    var strUser = e.options[e.selectedIndex].text;
//    console.log(strUser)
//    $("#tbBodega_bod_Nombre").val(strUser)

//});

//$(document).ready(function () {
//    var e = document.getElementById("estm_Id");
//    var strUser = e.options[e.selectedIndex].text;
//    console.log(strUser)
//    $("#tbEstadoMovimiento_estm_Descripcion").val(strUser)

//});

//$(document).ready(function () {
//    var e = document.getElementById("tbFactura_fact_Codigo");
//    var strUser = e.options[e.selectedIndex].text;
//    console.log(strUser)
//    $("#tbFactura_fact_Codigo").val(strUser)

//});
//$(document).on("change", "#dep_Codigo", function () {
//    GetMunicipios();
//});

//function phonenumber(inputtxt) {
//    var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;

//    if (inputtxt.value.match(phoneno)) {
//        return true;

//    }

//    else {
//        alert("message");

//        return false;

//    }

//}

//window.addEventListener("load", function () {
//    Miform.sald_Cantidad.addEventListener("keypress", soloNumeros, false);
//});

////$('#prod_Dsescripcion').mask('0000-0000');
//$(document).on("change", "#sald_Cantidad", function () {
//    var fiel = $("#sald_Cantidad").val();
//    soloNumeros(fiel);
//});

//Solo permite introducir numeros.
//function soloNumeros(e) {
//    var key = window.event ? e.which : e.keyCode;
//    //|| key == 44
//    if (key == 46) {
//    }
//    else {
//        if (key < 48 || key > 57) {
//            e.preventDefault();
//        }
//    }}
    //var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;

    //if (e.value.match(phoneno)) {
    //    return true;

    //}

    //else {
    //    alert("message");

    //    return false;

    //}
//$(document).ready(function () {
//    var tbSalida = GetAnularSalida();
//    $.ajax({
//        url: "/Salida/Anular",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ Salida: tbSalida }),
//    })
//    .done(function (data) {
//        if (data == 'El registro se guardo exitosamente') {
//            location.reload();
//            swal("El registro se almacenó exitosamente!", "", "success");
//        }
//        else {
//            location.reload();
//            swal("El registro  no se almacenó!", "", "error");
//        }
//    });

////})
//var masked = IMask.createMask({
//    mask: '000-000-00-00000000'
//    // ...and other options
//});

//var maskedValue = masked.resolve('71234567890');

//// mask keeps state after resolving
//console.log(masked.value);  // same as maskedValue
//// get unmasked value
//console.log(masked.unmaskedValue);

//function sald_Cantidad() {
//    //this.value = this.value.replace(/[^0-9\.]/g,'');
//    $(this).val($(this).val().replace(/[^0-9.\.]/g, ''));
//    if ((event.which != 46 || $(this).val().indexOf('') != -1) && (event.which < 48 || event.which > 57)) {
//        event.preventDefault();
//    }
//}

//$("#sald_Cantidad").on("keypress keyup blur", function (event) {
//    sald_Cantidad()
//});

//$(document).ready(function () {
//    $("#fact_Codigo").mask('999-999-99-99999999');
//});

//var dateMask = new IMask(
//    document.getElementById('fact_Codigo'),
//    {
//        mask: Date,
//        min: new Date(1990, 0, 1),
//        max: new Date(2020, 0, 1),
//        lazy: false
//    });

//$(document).ready(function () {
//    var getBrowserInfo = function () {
//        var ua = navigator.userAgent, tem,
//            M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
//        if (/trident/i.test(M[1])) {
//            tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
//            return 'IE ' + (tem[1] || '');
//        }
//        if (M[1] === 'Chrome') {
//            tem = ua.match(/\b(OPR|Edge)\/(\d+)/);
//            if (tem != null) return tem.slice(1).join(' ').replace('OPR', 'Opera');
//        }
//        M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
//        if ((tem = ua.match(/version\/(\d+)/i)) != null) M.splice(1, 1, tem[1]);
//        return M.join(' ');
//    };
//    console.log(getBrowserInfo());

//    console.log(bowser.name, bowser.version);
//});

//OLD SAVE EditSalidaDetalle
//$("#BtnsubmitEdit").click(function () {
//    //var sal_Id = $('#sal_Id_SD').val();
//    //var sald_Id = $('#sald_Id_SD').val();
//    //var prod_Codigo = $('#prod_Codigo_SD').val();
//    //var sald_Cantidad = $('#sald_Cantidad_SD').val();
//    //var sald_UsuarioCrea = $('#sald_UsuarioCrea_SD').val();
//    //var sald_FechaCrea = $('#sald_FechaCrea_SD').val();

//    //var data = {
//    //    sal_Id: sal_Id,
//    //    sald_Id: sald_Id,
//    //    prod_Codigo: prod_Codigo,
//    //    sald_Cantidad: sald_Cantidad
//    //};
//    if (sald_Cantidad == '') {
//        $('#MessageError').text('');
//        $('#CodigoError').text('');
//        $('#NombreError').text('');
//        $('#validationsald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
//    }
//    else if (sald_Cantidad < '0.05') {
//        $('#NombreError').text('');
//        $('#validationsald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad muy Pequeña</ul>');
//    }
//    else {
//        var sal_id = $('#sald_Id').val();
//        var data = $("#SubmitForm").serializeArray();
//        $.ajax({
//            type: "Post",
//            url: "/Salida/EditSalidaDetalle",
//            data: data,
//            success: function (result) {
//                if (result == '-1')
//                    $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
//                else
//                    console.log(result);
//                window.location.href = '/Salida/Edit/' + result;
//            }
//        });
//    }
//})