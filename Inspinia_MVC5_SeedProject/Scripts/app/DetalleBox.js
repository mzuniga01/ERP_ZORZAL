﻿var contador = 0;
$(document).ready(function () {
    $('#Table_BuscarProducto').DataTable(
        {
            "searching": false,
            "lengthChange": false,

            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",
            }
        });

    var $rows = $('#Table_BuscarProducto tr');
    $("#search").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
        //$rows.show().filter(function () {
        //    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
        //    return !~text.indexOf(val);
        //}).hide();
    });
});

    $(document).ready(function () {
        $('#tblBoxDetalle').DataTable({

            "searching": true,
            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sLengthMenu": "Mostrar _MENU_ Registros por página",
                "sInfo": "Mostrando _START_ a _END_ Entradas",
            },

        });
    });

    $(document).on("click", "#Table_BuscarProducto tbody tr td button#seleccionar", function () {
        var currentRowDT = $('Table_BuscarProducto').DataTable();
    var currentRow = $(this).closest("tr");
    prod_CodigoBarrasItem = currentRow.find("td:eq(0)").text();
    idItem = currentRow.find("td:eq(1)").text();
    contentItem = currentRow.find("td:eq(2)").text();
    pcatItem = currentRow.find("td:eq(3)").text();
    psubctItem = currentRow.find("td:eq(4)").text();
    uni_IdtItem = currentRowDT.rows().data()[4];
    //uni_IdtItem = currentRow.find("td:eq(4)").text();

    //prod_CodigoBarrasItem = $(this).closest('tr').data('html');
    //idItem = $(this).closest('tr').data('id');
    //contentItem = $(this).closest('tr').data('content');
    //uni_IdtItem = $(this).closest('tr').data('keyboard');
    //psubctItem = $(this).closest('tr').data('container');
    //pcatItem = $(this).closest('tr').data('interval');
    $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
    $("#prod_Codigo").val(idItem);
    $("#prod_Descripcion").val(contentItem);
    $("#uni_Id").val(uni_IdtItem);
    $("#pscat_Id").val(psubctItem);
    $("#pcat_Id").val(pcatItem);
    $("#boxd_Cantidad").focus();
    //$("#cod").val(idItem);
});

//prueba de enter

$(function () {
    $('#prod_CodigoBarras').keydown(function (e) {
        if (e.keyCode == 13) {
            $("#seleccionar").focus().click();

            $(document).on("click", "#Table_BuscarProducto tbody tr td button#seleccionar", function () {
                var currentRow = $(this).closest("tr");
                prod_CodigoBarrasItem = currentRow.find("td:eq(0)").text();
                idItem = currentRow.find("td:eq(1)").text();

                //prod_CodigoBarrasItem = $(this).closest('tr').data('html');
                //idItem = $(this).closest('tr').data('id');
                contentItem = $(this).closest('tr').data('content');
                uni_IdtItem = $(this).closest('tr').data('keyboard');
                psubctItem = $(this).closest('tr').data('container');
                pcatItem = $(this).closest('tr').data('interval');
                $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
                $("#prod_Codigo").val(idItem);
                $("#prod_Descripcion").val(contentItem);
                $("#uni_Id").val(uni_IdtItem);
                $("#pscat_Id").val(psubctItem);
                $("#pcat_Id").val(pcatItem);
                $("#boxd_Cantidad").focus();
                //$("#cod").val(idItem);
            });
            console.log('prueba');
            $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
            $("#prod_Codigo").val(idItem);
            $("#prod_Descripcion").val(contentItem);
            $("#uni_Id").val(uni_IdtItem);
            $("#pscat_Id").val(psubctItem);
            $("#pcat_Id").val(pcatItem);
            $("#boxd_Cantidad").focus();
            return false;
        }
    });
});

function GetBoxDetalle() {
    var BoxDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        boxd_Cantidad: $('#boxd_Cantidad').val(),
        boxd_UsuarioCrea: contador
    };
    return BoxDetalle;
}
//SALIDA DETALLE

$('#AgregarBoxDetalleEdit').click(function () {
    SeleccionProducto()
});
function SeleccionProducto() {
    var bodd_Id = $('#bodd_Id').val();
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Unidad_Medida = $('#pscat_Id').val();
    var Cantidad = $('#boxd_Cantidad').val();
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
        $('#boxd_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
    }
    else {
        var tbBoxDetalle = GetBoxDetalle();
        $.ajax({
            url: "/Box/SaveBoxDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ BoxDetalle: tbBoxDetalle, data_producto: data_producto }),
        }).done(function (datos) {
            if (datos == data_producto) {
                //alert('Es Igual.')
                console.log('Repetido');
                var cantfisica_nueva = $('#boxd_Cantidad').val();
                $("#tblBoxDetalle td").each(function () {
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
                        copiar += "<td>" + '<button id="removesBoxDetalle" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
                        copiar += "</tr>";
                        $('#tblBoxDetalle').append(copiar);
                    }
                });
            } else {
                //alert('NO ES IGUAL')
                //Rellenar la tabla
                //contador = contador + 1;
                copiar = "<tr data-id=" + contador + ">";
                //copiar += "<td>" + $('#CodTipoCasoExitoCreate option:selected').text() + "</td>";
                copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
                copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
                copiar += "<td id = 'Unidad_Medida'>" + $('#pscat_Id').val() + "</td>";
                copiar += "<td id = 'Cantidad'>" + $('#boxd_Cantidad').val() + "</td>";
                copiar += "<td>" + '<button id="removesBoxDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                copiar += "</tr>";
                $('#tblBoxDetalle').append(copiar);
            }
        }).done(function (data) {
            $('#prod_Codigo').val('');
            $('#prod_Descripcion').val('');
            $('#pscat_Id').val('');
            $('#uni_Id').val('');
            $('#pcat_Id').val('');

            $("#prod_CodigoBarras").val('');
            $('#boxd_Cantidad').val('0.00');
            $('#Error_Barras').text('');
            $('#NombreError').text('');
            console.log('Hola');
        });
    }
};

function ProductoCantidad(bod_Id, prod_Codigo) {
    $("#prod_CodigoBarras").val();
    $("#CantidaExistenteProd").text('');
    var cod_Barras = $("#prod_CodigoBarras").val();
    return $.ajax({
        url: "/Box/Cantidad",
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
            var currentRow = $("#tblBoxDetalle tbody tr");
            //var DataTable = $("#tblBoxDetalle >tbody >tr").DataTable();
            //$("#tblBoxDetalle td").each(function () {
            var prod_CodigoTabla = currentRow.find("td:eq(0)").text();
            var idcontador = $(this).closest('tr').data('id');
            var cantfisica_anterior = $(this).closest("tr").find("td:eq(7)").text();
            console.log(prod_CodigoTabla)
            if (prod_CodigoTabla !== "No hay registros") {
                currentRow.each(function () {
                    var prod_CodigoTablaeach = $(this).closest("tr").find("td:eq(0)").text();

                    if (prod_CodigoCampo == prod_CodigoTablaeach) {
                        var CantidadTabla = $(this).closest("tr").find("td:eq(7)").text();
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
                    else {
                        $("#CantidaExistenteProd").text('');
                        $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Existente en Bodega: ' + CantidadAceptada + '</ul>');
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


$('#btnCreateBoxDetalle').click(function () {
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Cantidad = $('#boxd_Cantidad').val();
    console.log('Funca');
    var tbBoxDetalle = GetNewBoxDetalle();
    $.ajax({
        url: "/Box/SaveNewDetail",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ BoxDetalle: tbBoxDetalle }),
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

$('#AgregarBoxDetalle').click(function () {
    var table = $('#tblBoxDetalle').DataTable();
    var counter = 0;
    var bod_Id = $('#bod_Id').val();
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Unidad_Medida = $('#pscat_Id').val();
    var Cantidad = $('#boxd_Cantidad').val();
    var data_producto = $("#prod_Codigo").val();
    var CodBarra_Producto = $('#prod_CodigoBarras').val();
    $('#boxd_CantidadError').text();
    var vCodBarra_Producto = true;
    var vCantidad = true;

    if (CodBarra_Producto == "") {
        $('#ValidationCodigoBarrasCreateError').text('');
        $('#validationprod_CodigoBarras').after('<ul id="ValidationCodigoBarrasCreateError" class="validation-summary-errors text-danger">Campo de Barras Requerido</ul>');
        vCodBarra_Producto = false;
    }
    if (Cantidad == "") {
        $('#MessageError').text('');
        $('#boxd_CantidadExedError').text('');
        $('#boxd_CantidadError').text('');
        $('#boxd_Cantidad').after('<ul id="boxd_CantidadError" class="validation-summary-errors text-danger">Cantidad Requerida</ul>');
        vCantidad = false;
    }
    if (!vCantidad || !vCodBarra_Producto) {
        return false;
    }
    else {
        $('#ValidationCodigoBarrasCreateError').text('');
        ProductoCantidad(bod_Id, Cod_Producto).done(function (data) {
            var CantidadAceptada = data.CantidadAceptada
            var CantidadMinima = data.CantidadMinima
            if ($('#tblBoxDetalle >tbody >tr').length > 0) {
                $('#tblBoxDetalle >tbody >tr').each(function () {
                    var prod_CodigoTabla = $(this).find("td:eq(1)").text()
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
                $('#boxd_CantidadExedError').text('');
                $('#boxd_CantidadError').text('');
                $('#boxd_Cantidad').after('<ul id="sald_CantidadError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
            }
            else if (Cantidad > CantidadExit) {
                $('#MessageError').text('');
                $('#boxd_CantidadError').text('');
                $('#boxd_CantidadExedError').text('');
                $('#boxd_Cantidad').after('<ul id="sald_CantidadExedError" class="validation-summary-errors text-danger">Cantidad Superada</ul>');
            }
            else {
                $('#ValidationCodigoCreateError').text('');
                $('#boxd_CantidadError').text('');
                $('#boxd_CantidadExedError').text('');
                $('#CantidaExistenteProd').text('');
                var tbBoxDetalle = GetBoxDetalle();
                $.ajax({
                    url: "/Box/SaveBoxDetalle",
                    method: "POST",
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ BoxDetalle: tbBoxDetalle, data_producto: data_producto }),
                }).done(function (datos) {
                    $("#prod_CodigoBarras").removeAttr("readonly");
                    if (datos == data_producto) {
                        console.log('Repetido');
                        var cantfisica_nueva = $('#boxd_Cantidad').val();
                        $("#tblBoxDetalle td").each(function () {
                            var prueba = $(this).text()
                            if (prueba == data_producto) {
                                table.row($(this).parents('tr')).remove().draw();
                                var idcontador = $(this).closest('tr').data('id');
                                var cantfisica_anterior = $(this).closest("tr").find("td:eq(7)").text();
                                var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);

                                table.row.add([
                                    $('#prod_Codigo').val(),
                                    $('#prod_Descripcion').val(),
                                    $('#uni_Id').val(),
                                    sumacantidades,
                                    '<button id = "removeBoxDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">-</button>'

                                ]).draw(false);
                            }
                        });
                    } else {
                        table.row.add([
                            $('#prod_Codigo').val(),
                            $('#prod_Descripcion').val(),
                            $('#uni_Id').val(),
                            $('#boxd_Cantidad').val(),
                            '<button id = "removesBoxDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">-</button>'

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
                    $('#boxd_CantidadError').text('');
                    $('#CantidaExistenteProd').text('');
                    $('#prod_CodigoBarras').focus();
                    console.log('Hola');
                });
            }
        });
    }
});

function GetBoxDetalle() {
    var BoxDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        boxd_Cantidad: $('#boxd_Cantidad').val(),
        boxd_UsuarioCrea: contador
    };
    return BoxDetalle;
}

$('#AgregarDetalleSalida').click(function () {
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Subcategoria = $('#pscat_Id').val();
    var Categoria = $('#pcat_Id').val();
    var Unidad_Medida = $('#uni_Id').val();
    var Cantidad = $('#boxd_Cantidad').val();
    if (Cantidad == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#validationcantidad').text('Cantidad Requerido');
    }
    else if (Producto == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#validationproducto').text('Producto Requerido');
    }
    else if (Producto == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#validationproducto').text('Producto Requerido');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        //copiar += "<td>" + $('#CodTipoCasoExitoCreate option:selected').text() + "</td>";
        //copiar += "<td hidden id='MunCodigo'>" + $('#mun_Codigo option:selected').val() + "</td>";
        copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
        copiar += "<td id = 'Unidad_Medida'>" + $('#pscat_Id').val() + "</td>";
        copiar += "<td id = 'Cantidad'>" + $('#boxd_Cantidad').val() + "</td>";
        copiar += "<td>" + '<button id="removesBoxDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblBoxDetalle').append(copiar);

        //Para obtener el valor y mandarlo al controlador

        var tbBoxDetalle = GetBoxDetalle();
        $.ajax({
            url: "/Box/SaveBoxDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ BoxDetalle: tbBoxDetalle }),
        })
            .done(function (data) {
                $('#prod_Codigo').val('');
                $('#prod_Descripcion').val('');
                $('#pscat_Id').val('');
                $('#boxd_Cantidad').val('');
                $('#MessageError').text('');
                $('#NombreError').text('');
                $('#validationcantidad').text('');
                $('#validationproducto').text('');
            });
    }
});

//Remover el detalle
$(document).on("click", "#tblBoxDetalle tbody tr td button#removesBoxDetalle", function () {
    var currentRow = $(this).closest("tr");
    prod_Codigo = currentRow.find("td:eq(0)").text();
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var BoxDetalle = {
        prod_Codigo: prod_Codigo,
    };
    $.ajax({
        url: "/Box/RemovesBoxDetalles",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ BoxDetalle: BoxDetalle }),
    });
});

//Detalle

function EditBoxDetalles(boxd_Id) {
    $.ajax({
        url: "/Box/getBoxDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ boxd_Id: boxd_Id }),
    })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#boxd_Id_SD").val(item.boxd_Id);
                    $("#prod_Codigo_SD").val(item.prod_Codigo);
                    $("#boxd_Cantidad_SD").val(item.boxd_Cantidad);
                    $("#prod_Descripcion_SD").val(item.prod_Descripcion);
                    $("#pcat_Nombre_SD").val(item.pcat_Nombre);
                    $("#pscat_Descripcion_SD").val(item.pscat_Descripcion);
                    $("#uni_Descripcion_SD").val(item.uni_Descripcion);
                    $("#box_Codigo_SD").val(item.box_Codigo);
                    console.log(item);
                    $("#EditBoxDetalle").modal("show");
                })
            }
        })
}

function DeleteBoxDetalles(boxd_Id) {
    $.ajax({
        url: "/Box/DeleteBoxDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ boxd_Id: boxd_Id }),
    })
        .done(function (data) {
            console.log(data);
            if (data == "Exito") {
                location.reload();
            }
        })
}
$("#BtnsubmitEdit").click(function () {
    var boxd_id = $('#boxd_Id').val();
    var box = $('#box_Codigo').val();
    var data = $("#SubmitForm").serializeArray();
    console.log(data);
    $.ajax({
        type: "Post",
        url: "/Box/EditBoxDetalle",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
            else
                window.location.href = '/Box/Edit/' + box;
        }
    });
})

function GetNewBoxDetalle() {
    var BoxDetalle = {
        box_Codigo: $('#txtbox_Codigo').val(),
        boxd_Id: $('#boxd_Id').val(),
        prod_Codigo: $('#prod_Codigo').val(),
        boxd_Cantidad: $('#boxd_Cantidad').val(),
        boxd_UsuarioCrea: contador
    };
    return BoxDetalle;
}

//VALIDAR SOLO NUMEROS
$(function () {
    $("#boxd_Cantidad").keydown(function (event) {
        //alert(event.keyCode);
        if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) && event.keyCode !== 190 && event.keyCode !== 110 && event.keyCode !== 8 && event.keyCode !== 9) {
            return false;
        }
    });
});

//VALIDAR CARACTERES
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
}

//Validar Solo caracteres
function soloLetrasYNumeros(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-z0-9A-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}

function controlCaracteres(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ1234567890# ,.]+$/.test(tecla);
}

function NumText(string) {//solo letras y numeros
    var out = '';
    //Se añaden las letras validas
    var filtro = 'abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890áéíóúÁÉÍÓÚ# ,.';//Caracteres validos

    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) != -1)
            out += string.charAt(i);

    return out;
}

$("#boxd_Cantidad").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

$('#Productos').click(function () {
    var bod_Id = $("bod_Id").val();
    if (bod_Id = "") {
        $("bod_Id").focus();
    }
    else {
        $('#ModalAgregarProducto').modal('show');
        ListaProductos()
    }
})

function ListaProductos() {
    var bod_Id = $('#bod_Id').val()
    url = "/Box/GetProducto?bod_Id=" + bod_Id;
    $('#ModalAgregarProducto').modal('show');

    var table = $('#Table_BuscarProducto').dataTable({
        destroy: true,
        resposive: true,
        "columnDefs": [
            { "visible": false, "targets": 4 }
        ],
        ajax: {
            method: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            "dataSrc": ""
        },
        "columns": [
            { "data": "prod_CodigoBarras" },
            { "data": "prod_Codigo" },
            //{ "data": "prod_Descripcion" },
            { "data": "prod_Marca" },
            { "data": "prod_Modelo" },
            { "data": "uni_Descripcion"},

            { "defaultContent": "<button class='btn btn-primary btn-xs'  id='seleccionar' data-dismiss='modal'>Seleccionar</button>" }
        ],
        "oLanguage": {
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior",
            },
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "No hay registros",
            "sInfoEmpty": "Mostrando 0 de 0 Entradas",
            "sSearch": "Buscar",
            "sInfo": "Mostrando _START_ a _END_ Entradas",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
        }
    })
    //$('#tblBusquedaGenerica tbody').on('click', 'button', function () {
    //    var data = table.row($(this).parents('tr')).data();
    //    alert(data[0]);
    //});
}

//$('#AgregarBoxDetalleEdit').click(function () {
//    var Cod_Producto = $('#prod_Codigo').val();
//    var Producto = $('#prod_Descripcion').val();
//    var Subcategoria = $('#pscat_Id').val();
//    var Categoria = $('#pcat_Id').val();
//    var Unidad_Medida = $('#uni_Id').val();
//    var Cantidad = $('#boxd_Cantidad').val();
//    if (Cantidad == '') {
//        $('#MessageError').text('');
//        $('#CodigoError').text('');
//        $('#NombreError').text('');
//        $('#validationcantidad').text('Cantidad Requerido');
//    }
//    else if (Producto == '') {
//        $('#MessageError').text('');
//        $('#CodigoError').text('');
//        $('#NombreError').text('');
//        $('#validationproducto').text('Producto Requerido');
//    }
//    else if (Producto == '') {
//        $('#MessageError').text('');
//        $('#CodigoError').text('');
//        $('#NombreError').text('');
//        $('#validationproducto').text('Producto Requerido');
//    }
//    else {
//        contador = contador + 1;
//        copiar = "<tr data-id=" + contador + ">";
//        //copiar += "<td>" + $('#CodTipoCasoExitoCreate option:selected').text() + "</td>";
//        //copiar += "<td hidden id='MunCodigo'>" + $('#mun_Codigo option:selected').val() + "</td>";
//        copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
//        copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
//        copiar += "<td id = 'Unidad_Medida'>" + $('#pscat_Id').val() + "</td>";
//        copiar += "<td id = 'Cantidad'>" + $('#boxd_Cantidad').val() + "</td>";
//        copiar += "<td>" + '<button id="removeBoxDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
//        copiar += "</tr>";
//        $('#tblBoxDetalle').append(copiar);

//        //Para obtener el valor y mandarlo al controlador

//        var tbBoxDetalle = GetBoxDetalle();
//        $.ajax({
//            url: "/Box/SaveBoxDetalle",
//            method: "POST",
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify({ BoxDetalle: tbBoxDetalle }),
//        })
//            .done(function (data) {
//                $('#prod_Codigo').val('');
//                $('#prod_Descripcion').val('');
//                $('#pscat_Id').val('');
//                $('#boxd_Cantidad').val('');
//                $('#MessageError').text('');
//                $('#NombreError').text('');
//                $('#validationcantidad').text('');
//                $('#validationproducto').text('');
//            });
//    }
//});

////////////////////////////////////////////////////////////////////////////

//$('#AgregarBoxDetalle').click(function () {
//    var bodd_Id = $('#bodd_Id').val();
//    var Cod_Producto = $('#prod_Codigo').val();
//    var Producto = $('#prod_Descripcion').val();
//    var Unidad_Medida = $('#pscat_Id').val();
//    var Cantidad = $('#boxd_Cantidad').val();
//    var data_producto = $("#prod_Codigo").val();
//    if (Producto == '') {
//        $('#MessageError').text('');
//        $('#CodigoError').text('');
//        $('#NombreError').text('');
//        $('#ValidationCodigoCreate').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
//    }
//    else if (Cantidad == '' || Cantidad < 1) {
//        $('#MessageError').text('');
//        $('#CodigoError').text('');
//        $('#NombreError').text('');
//        $('#boxd_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
//    }
//    else {
//        var tbBoxDetalle = GetBoxDetalle();
//        $.ajax({
//            url: "/Box/SaveBoxDetalle",
//            method: "POST",
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify({ BoxDetalle: tbBoxDetalle, data_producto: data_producto }),
//        }).done(function (datos) {
//            if (datos == data_producto) {
//                //alert('Es Igual.')
//                console.log('Repetido');
//                var cantfisica_nueva = $('#boxd_Cantidad').val();
//                $("#tblBoxDetalle td").each(function () {
//                    var prueba = $(this).text()
//                    if (prueba == data_producto) {
//                        var idcontador = $(this).closest('tr').data('id');
//                        var cantfisica_anterior = $(this).closest("tr").find("td:eq(3)").text();
//                        var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);
//                        console.log(sumacantidades);
//                        $(this).closest('tr').remove();
//                        copiar = "<tr data-id=" + idcontador + ">";
//                        copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
//                        copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
//                        copiar += "<td id = 'Unidad_Medida'>" + $('#uni_Id').val() + "</td>";
//                        copiar += "<td id = 'Cantidad'>" + sumacantidades + "</td>";
//                        copiar += "<td>" + '<button id="removeBoxDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
//                        copiar += "</tr>";
//                        $('#tblBoxDetalle').append(copiar);
//                    }
//                });
//            } else {
//                //alert('NO ES IGUAL')
//                //Rellenar la tabla
//                //contador = contador + 1;
//                copiar = "<tr data-id=" + contador + ">";
//                //copiar += "<td>" + $('#CodTipoCasoExitoCreate option:selected').text() + "</td>";
//                copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
//                copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
//                copiar += "<td id = 'Unidad_Medida'>" + $('#pscat_Id').val() + "</td>";
//                copiar += "<td id = 'Cantidad'>" + $('#boxd_Cantidad').val() + "</td>";
//                copiar += "<td>" + '<button id="removeBoxDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
//                copiar += "</tr>";
//                $('#tblBoxDetalle').append(copiar);
//            }
//        }).done(function (data) {
//            $('#prod_Codigo').val('');
//            $('#prod_Descripcion').val('');
//            $('#pscat_Id').val('');
//            $('#uni_Id').val('');
//            $('#pcat_Id').val('');

//            $("#prod_CodigoBarras").val('');
//            $('#boxd_Cantidad').val('0.00');
//            $('#Error_Barras').text('');
//            $('#NombreError').text('');
//            console.log('Hola');
//        });
//    }
//});

/////////////////////////////////////////////////////////////////


//$('#btnCreateBoxDetalle').click(function () {
//    var Cod_Producto = $('#prod_Codigo').val();
//    var Producto = $('#prod_Descripcion').val();
//    var Cantidad = $('#boxd_Cantidad').val();
//    console.log('Funca');
//    var tbBoxDetalle = GetNewBoxDetalle();
//    $.ajax({
//        url: "/Box/SaveNewDetail",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ BoxDetalle: tbBoxDetalle }),
//    })
//        .done(function (data) {
//            if (data == 'El registro se guardo exitosamente') {
//                location.reload();
//                swal("El registro se almacenó exitosamente!", "", "success");
//            }
//            else {
//                location.reload();
//                swal("El registro  no se almacenó!", "", "error");
//            }
//        });
//})

//function ListaProductos() {
//    var vbod_Id = $('#bod_Id').val()
//    var BodegaDetalle = {
//        bod_Id: vbod_Id
//    };
//    $.ajax({
//        url: "/Box/GetProdList",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ tbBodegaDetalle: BodegaDetalle }),
//    }).done(function (data) {
//        $.each(data, function (key, value) {
//            key = "<tr tr data-id =" + value.prod_Codigo + " , tr data-content =" + value.prod_Descripcion + " tr data-container =" + value.pscat_Descripcion + " , tr data-keyboard =" + value.uni_Descripcion + " , tr data-pcat=" + value.pcat_Nombre + " , tr data-cod_Barra=" + value.prod_CodigoBarras + " >";
//            key += "<td id ='prod_Codigo' >" + value.prod_Codigo + "</td>";
//            key += "<td id ='prod_Descripcion' >" + value.prod_Descripcion + "</td>";
//            key += "<td id = 'pcat_Nombre'>" + value.pcat_Nombre + "</td>";
//            key += "<td id = 'pscat_Descripcion'>" + value.pscat_Descripcion + "</td>";
//            key += "<td id = 'uni_Descripcion'>" + value.uni_Descripcion + "</td>";
//            key += "<td id = 'prod_CodigoBarras'>" + value.prod_CodigoBarras + "</td>";
//            key += "<td>" + "<button class='btn btn-primary btn-xs' value=" + value.prod_Codigo + " id='seleccionar' data-dismiss='modal'>Seleccionar</button>" + "</td>"
//            key += "</tr>";
//            $('#Table_BuscarProductoD').append(key);
//        })
//    });
//}
//function LimpiarTable() { $("#Body_BuscarProducto").removeData() }

//Agregar


//$('#btnCreateBoxDetalle').click(function () {
//    var Cod_Producto = $('#prod_Codigo').val();
//    var Producto = $('#prod_Descripcion').val();
//    var Unidad_Medida = $('#pscat_Id').val();
//    var Cantidad = $('#boxd_Cantidad').val();
//    var boxd_Id = $('#boxd_Id').val();

//    if (Producto == '') {
//        $('#MessageError').text('');
//        $('#CodigoError').text('');
//        $('#NombreError').text('');
//        $('#ValidationCodigoCreate').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
//    }
//    else if (Unidad_Medida == '') {
//        $('#MessageError').text('');
//        $('#CodigoError').text('');
//        $('#NombreError').text('');
//        $('#ValidationNombreCreate').after('<ul id="NombreError" class="validation-summary-errors text-danger">Unidad Medida Requerido</ul>');
//    }
//    else if (Cantidad == '') {
//        $('#MessageError').text('');
//        $('#CodigoError').text('');
//        $('#NombreError').text('');
//        $('#boxd_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
//    }

//    else {
//        var tbBoxDetalle = GetNewBoxDetalle();
//        $.ajax({
//            url: "/Box/SaveNewDatail",
//            method: "POST",
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify({ BoxDetalle: tbBoxDetalle }),
//        })
//            .done(function (data) {
//                if (data == 'El registro se guardo exitosamente') {
//                    location.reload();
//                    swal("El registro se almacenó exitosamente!", "", "success");
//                }
//                else {
//                    location.reload();
//                    swal("El registro  no se almacenó!", "", "error");
//                }
//            });

//        var PuntoEmisionDetalle = GetPuntoEmisionDetalle();
//        $.ajax({
//            url: "/PuntoEmision/SaveCreateNumeracion",
//            method: "POST",
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify({ CreatePuntoEmisionDetalle: PuntoEmisionDetalle }),
//            success: function (data) {
//            }
//        })
//    }
//});

