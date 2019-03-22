//contador para la tabla detalle
var contador = 0;

//Funcion para validar datos
$('#AgregarInvFisicoDetalle').click(function () {
    var data_producto = $("#prod_Codigo").val();
    var barras = $("#prod_CodigoBarras").val();
    var Descripcion = $("#prod_Descripcion").val();
    var UnidadMedida = $("#uni_Id").val();
    var uni = $('#uni_Ids').val();
    var cantidadfisica = $("#invfd_Cantidad").val();
    var cantidadsistema = $("#invfd_CantidadSistema").val();

    if (barras == '') {
        $('#MessageError').text('');
        $('#errorproducto').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationproducto').after('<ul id="errorproducto" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    else if(cantidadfisica == '')
    {
        $('#MessageError').text('');
        $('#errorproducto').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationCantidadFisica').after('<ul id="errorcantidadfisica" class="validation-summary-errors text-danger">Campo Cantidad Requerido</ul>');
    }
    else if (cantidadfisica == 0) {
        $('#Mensajecodigo').text('');
        $('#Mensajecantidad').text('');
        $('#validationCantidadFisica').after('<ul id="errorcantidadfisica" class="validation-summary-errors text-danger">La cantidad No debe ser 0.</ul>');
    }
    else if (cantidadfisica == 0.00) {
        $('#Mensajecodigo').text('');
        $('#Mensajecantidad').text('');
        $('#validationCantidadFisica').after('<ul id="errorcantidadfisica" class="validation-summary-errors text-danger">La cantidad No debe ser 0.</ul>');
    }
    else if (cantidadsistema == '') {
        $('#MessageError').text('');
        $('#errorproducto').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationCantidadSistema').after('<ul id="errorcantidadsistema" class="validation-summary-errors text-danger">Campo Cantidad Sistema Requerido</ul>');

    } else {
        //ajax para el controlador
        var InventarioFisicoDetalle = GetInventarioFisicoDetalle();
        $.ajax({
            url: "/InventarioFisico/GuardarInventarioDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ invfd: InventarioFisicoDetalle, data_producto: data_producto })
        })
        .done(function (datos) {
            if (datos == data_producto) {
                //alert('Es Igual.')
                console.log('Repetido');
                var cantfisica_nueva = $('#invfd_Cantidad').val();
                $("#detalle td").each(function () {
                    var prueba = $(this).text()
                    if (prueba == data_producto) {
                        var idcontador = $(this).closest('tr').data('id');
                        var cantfisica_anterior = $(this).closest("tr").find("td:eq(4)").text();
                        var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);
                        $(this).closest('tr').remove();
                        copiar = "<tr data-id=" + idcontador + ">";
                        copiar += "<td id = 'data_producto' hidden='hidden'>" + $('#prod_Codigo').val() + "</td>";
                        copiar += "<td id = 'barras'>" + $('#prod_CodigoBarras').val() + "</td>";
                        copiar += "<td id = 'Descripcion'>" + $('#prod_Descripcion').val() + "</td>";
                        copiar += "<td id = 'UnidadMedida'>" + $('#uni_Id').val() + "</td>";
                        copiar += "<td id = 'cantidadfisicas'>" + sumacantidades + "</td>";
                        copiar += "<td id = 'cantidadsistema'>" + $('#invfd_CantidadSistema').val() + "</td>";
                        copiar += "<td id = 'uni' hidden='hidden'>" + $('#uni_Ids').val() + "</td>";
                        copiar += "<td>" + '<button id="removerInventarioFisicoDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                        copiar += "</tr>";
                        $('#detalle').append(copiar);
                        $("#invfd_Cantidad").val('1');
                    }
                });
            }
            else {
                //alert('NO ES IGUAL')
                //Rellenar la tabla 
                contador = contador + 1;
                copiar = "<tr data-id=" + contador + ">";
                copiar += "<td id = 'data_producto' hidden='hidden'>" + $('#prod_Codigo').val() + "</td>";
                copiar += "<td id = 'barras'>" + $('#prod_CodigoBarras').val() + "</td>";
                copiar += "<td id = 'Descripcion'>" + $('#prod_Descripcion').val() + "</td>";
                copiar += "<td id = 'UnidadMedida'>" + $('#uni_Id').val() + "</td>";
                copiar += "<td id = 'cantidadfisicas'>" + $('#invfd_Cantidad').val() + "</td>";
                copiar += "<td id = 'cantidadsistema'>" + $('#invfd_CantidadSistema').val() + "</td>";
                copiar += "<td id = 'uni' hidden='hidden'>" + $('#uni_Ids').val() + "</td>";
                copiar += "<td>" + '<button id="removerInventarioFisicoDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                copiar += "</tr>";
                $('#detalle').append(copiar);
                $("#invfd_Cantidad").val('1');
            }
        }).done(function (data) {

            $('#prod_CodigoBarras').val('');
            $('#invfd_CantidadSistema').val('');
            $('#prod_Descripcion').val('');
            $('#uni_Id').val('');

            $('#MessageError').text('');
            $('#errorproducto').text('');
            $('#errorcantidadfisica').text('');
            $('#errorcantidadsistema').text('');

        })

    }
})

//funcion para el controlador
function GetInventarioFisicoDetalle() {
    var invfd = {
        prod_Codigo: $('#prod_Codigo').val(),
        uni_Id: $('#uni_Ids').val(),
        invfd_Cantidad: $('#invfd_Cantidad').val(),
        invfd_CantidadSistema: $('#invfd_CantidadSistema').val(),
        invfd_id: contador,
        invfd_UsuarioCrea: contador
        //Fecha: $('#fechaCreate').val(),
    };
    return invfd;
}

//eliminar datos agregados a la tabla
$(document).on("click", "#detalle tbody tr td button#removerInventarioFisicoDetalle", function () {
    $(this).closest('tr').remove();
    var currentRow = $(this).closest("tr");
    var prod_Codigo = currentRow.find("td:eq(0)").text(); // get current row 1st TD value

    var detalle = {
        prod_Codigo: prod_Codigo,
    };
    $.ajax({
        url: "/InventarioFisico/removeInvFisicoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ detalle: detalle }),
    });
});

//Busqueda Generica de Productos
$(document).ready(function () {
    $('#BuscarProducto').DataTable(
        {
            "searching": true,
            "lengthChange": true,

            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sEmptyTable": "No hay registros",
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",

            }
        });
});
$(document).on("click", "#BuscarProducto tbody tr td button#seleccionar", function () {
    id = $(this).closest('tr').data('id');
    descripcion = $(this).closest('tr').data('content');
    barras = $(this).closest('tr').data('delay');
    uni = $(this).closest('tr').data('keyboard');
    uniid = $(this).closest('tr').data('container');
    $("#prod_Codigo").val(id);
    $("#prod_Descripcion").val(descripcion);
    $("#prod_CodigoBarras").val(barras);
    $("#uni_Id").val(uni);
    $("#uni_Ids").val(uniid);
    seleccionar(id);
});


//funcion para el controlador
function GetInventarioFisicoDetalle() {
    var guardar_detalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        uni_Id: $('#uni_Ids').val(),
        invfd_Cantidad: $('#invfd_Cantidad').val(),
        invfd_CantidadSistema: $('#invfd_CantidadSistema').val(),
        invf_Id: $('#invf_Id').val(),
        invfd_Id: $('#invfd_Id').val(),
        invfd_UsuarioCrea: $('#invfd_UsuarioCrea').val(),
        invfd_UsuarioModifica: $('#invfd_UsuarioModifica').val(),
        invfd_FechaCrea: $('#invfd_FechaCrea').val(),
        invfd_FechaModifica: $('#invfd_FechaModifica').val()
    };
    return guardar_detalle;
}


//Actualizar detalle
function EditarDetalle(invfd_Id) {
    $("#MsjError").text("");

    $.ajax({
        url: "/InventarioFisico/GetInventarioDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ invfd_Id }),
    })
    .done(function (data) {
        $.each(data, function (i, item) {
            $("#invfd_Id").val(item.invfd_Id);
            $("#invf_Id_edit").val(item.invf_Id);
            $("#prod_Codigo_edit").val(item.prod_Codigo);
            $("#invfd_Cantidad_edit").val(item.invfd_Cantidad);
            $("#invfd_CantidadSistema_edit").val(item.invfd_CantidadSistema);
            $("#uni_Id_edit").val(item.uni_Id);
            $("#Modaleditar").modal();
        })
    })
    .fail( function( jqXHR, textStatus, errorThrown ) {
        console.log('jqXHR', jqXHR);
        console.log('textStatus', textStatus);
        console.log('errorThrown', errorThrown);
    })
}

$("#submit").click(function () {
    var data = $("#form").serializeArray();
    var cantidadfisicaedit = $("#invfd_Cantidad_edit").val();
    $.ajax({
        type: "Post",
        url: "/InventarioFisico/UpdateInvFisicoDetalle",
        data: data,
        success: function (result) {
            if (cantidadfisicaedit == '') {
                $('#validationcantidadedit').after('<ul id="errorcantidadfisica" class="validation-summary-errors text-danger">Campo Cantidad Requerido</ul>');
            }
            else if (cantidadfisicaedit == 0) {
                $('#validationcantidadedit').after('<ul id="errorcantidadfisica" class="validation-summary-errors text-danger">La cantidad No debe ser 0.</ul>');
            }
            else if (cantidadfisicaedit == 0.00) {
                $('#validationcantidadedit').after('<ul id="errorcantidadfisica" class="validation-summary-errors text-danger">La cantidad No debe ser 0.</ul>');
            }
            else if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
            else

            location.reload();
        }
    });
})

//Encargado de Bodega
$("#bod_Id").change(function () {
    GetResponsableBodega();
    $("#prod_CodigoBarras").val('');
    $("#prod_Descripcion").val('');
    $("#uni_Id").val('');
    $('#uni_Ids').val('');
    $("#invfd_CantidadSistema").val('');
    $("#invfd_Cantidad").val('1');
});


function GetResponsableBodega() {
    var invf_responsable = $('#bod_Id').val();
    $.ajax({
        url: "/InventarioFisico/GetResponsableBodega",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ invf_responsable: invf_responsable }),
    })
    .done(function (data) {
        if (data.length > 0) {
            $('#invf_ResponsableBodega').empty();
            $.each(data, function (key, val) {
                $('#invf_ResponsableBodega').val(val.emp_Nombres + " " + val.emp_Apellidos);
            });
            $('#invf_ResponsableBodega').trigger("chosen:updated");
        }
        else {
            $('#invf_ResponsableBodega').empty();
            $('#invf_ResponsableBodega').val(val.emp_Nombres + " " + val.emp_Apellidos);
        }
    });
}


//Cantidad del Sistema
function seleccionar(prod_Codigo) {
    var bodega = $("#bod_Id").val();
    var guardar_cantidad = {
        bod_Id: bodega,
        prod_Codigo: prod_Codigo,
    };
    $.ajax({
        url: "/InventarioFisico/CantidadExistencias",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CantidadExistencias: guardar_cantidad }),
    }).done(function (data) {
        if (data.length > 0) {
            $('#invfd_CantidadSistema').empty();
            $.each(data, function (key, val) {
                if (data.length > 0) {
                    $('#invfd_CantidadSistema').val(val.bodd_CantidadExistente);
                } else {
                    $('#prod_CodigoBarras').val('');
                    $('#invfd_CantidadSistema').val('');
                    $('#prod_Descripcion').val('');
                    $('#uni_Id').val('');
                    $('#invfd_Cantidad').val('1');
                }
            });
            $('#invfd_CantidadSistema').trigger("chosen:updated");
        }
        else {
            $('#invfd_CantidadSistema').empty();
            if (data.length > 0) {
                $('#invfd_CantidadSistema').val(val.bodd_CantidadExistente);
            } else {
       
                $('#invfd_CantidadSistema').val('0');
            }
       
        }
    });
}

function GetCantidadExistente() {
    var guardar_cantidad = {
        bod_Id: $('#bod_Id').val(),
        prod_Codigo: $('#prod_Codigo').val()
    };
    return guardar_cantidad;
}


//Productos Por Bodega
$("#bod_Id").change(function () {
    $('#detalle tbody').empty();
    cambiobodega();
    $("#invfd_Cantidad").val('1');
});


//Agregar Detalle
$('#AgregarNuevoDetalle').click(function () {
    var data_producto = $("#prod_Codigo").val();
    var barras = $("#prod_CodigoBarras").val();
    var Descripcion = $("#prod_Descripcion").val();
    var UnidadMedida = $("#uni_Id").val();
    var uni = $('#uni_Ids').val();
    var cantidadfisica = $("#invfd_Cantidad").val();
    var cantidadsistema = $("#invfd_CantidadSistema").val();

    if (barras == '') {
        $('#MessageError').text('');
        $('#errorproducto').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationproducto').after('<ul id="errorproducto" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    else if (cantidadfisica == '') {
        $('#MessageError').text('');
        $('#errorproducto').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationCantidadFisica').after('<ul id="errorcantidadfisica" class="validation-summary-errors text-danger">Campo Cantidad Requerido</ul>');
    }
    else if (cantidadfisica == 0) {
        $('#Mensajecodigo').text('');
        $('#Mensajecantidad').text('');
        $('#validationCantidadFisica').after('<ul id="errorcantidadfisica" class="validation-summary-errors text-danger">La cantidad No debe ser 0.</ul>');
    }
    else if (cantidadfisica == 0.00) {
        $('#Mensajecodigo').text('');
        $('#Mensajecantidad').text('');
        $('#validationCantidadFisica').after('<ul id="errorcantidadfisica" class="validation-summary-errors text-danger">La cantidad No debe ser 0.</ul>');
    }
    else if (cantidadsistema == '') {
        $('#MessageError').text('');
        $('#errorproducto').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationCantidadSistema').after('<ul id="errorcantidadsistema" class="validation-summary-errors text-danger">Campo Cantidad Sistema Requerido</ul>');

    } else {
        //ajax para el controlador
            var InventarioFisicoDetalle = GetInventarioFisicoDetalle();
            $.ajax({
                url: "/InventarioFisico/GuardarInventarioDetalle",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ invfd: InventarioFisicoDetalle, data_producto: data_producto })
            })
            .done(function (datos) {
                if (datos == data_producto) {
                    //alert('Es Igual.')
                    console.log('Repetido');
                    var cantfisica_nueva = $('#invfd_Cantidad').val();
                    $("#InvDetalle td").each(function () {
                        var prueba = $(this).text()
                        if (prueba == data_producto) {
                            var idcontador = $(this).closest('tr').data('id');
                            var cantfisica_anterior = $(this).closest("tr").find("td:eq(5)").text();
                            var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);
                            console.log(sumacantidades);
                            $(this).closest('tr').remove();
                            copiar = "<tr data-id=" + idcontador + ">";
                            copiar += "<td id = 'data_producto' hidden='hidden'>" + $('#prod_Codigo').val() + "</td>";
                            copiar += "<td id = 'barras'>" + $('#prod_CodigoBarras').val() + "</td>";
                            copiar += "<td id = 'Descripcion'>" + $('#prod_Descripcion').val() + "</td>";
                            copiar += "<td id = 'UnidadMedida'>" + $('#uni_Id').val() + "</td>";
                            copiar += "<td id = 'cantidadsistema'>" + $('#invfd_CantidadSistema').val() + "</td>";
                            copiar += "<td id = 'cantidadfisicas'>" + sumacantidades + "</td>";
                            copiar += "<td id = 'uni' hidden='hidden'>" + $('#uni_Ids').val() + "</td>";
                            copiar += "<td>" + '<button id="removerInvFisicoDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                            copiar += "</tr>";
                            $('#InvDetalle').append(copiar);
                            $("#invfd_Cantidad").val('1');
                        }
                    });
                }
                else {
                    //alert('NO ES IGUAL')
                    //Rellenar la tabla 
                    contador = contador + 1;
                    copiar = "<tr data-id=" + contador + ">";
                    copiar += "<td id = 'data_producto' hidden='hidden'>" + $('#prod_Codigo').val() + "</td>";
                    copiar += "<td id = 'barras'>" + $('#prod_CodigoBarras').val() + "</td>";
                    copiar += "<td id = 'Descripcion'>" + $('#prod_Descripcion').val() + "</td>";
                    copiar += "<td id = 'UnidadMedida'>" + $('#uni_Id').val() + "</td>";
                    copiar += "<td id = 'cantidadsistema'>" + $('#invfd_CantidadSistema').val() + "</td>";
                    copiar += "<td id = 'cantidadfisicas'>" + $('#invfd_Cantidad').val() + "</td>";
                    copiar += "<td id = 'uni' hidden='hidden'>" + $('#uni_Ids').val() + "</td>";
                    copiar += "<td>" + '<button id="removerInvFisicoDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                    copiar += "</tr>";
                    $('#InvDetalle').append(copiar);
                    $("#invfd_Cantidad").val('1');
                }
            }).done(function (data) {

                $('#prod_CodigoBarras').val('');
                $('#invfd_CantidadSistema').val('');
                $('#prod_Descripcion').val('');
                $('#uni_Id').val('');

                $('#MessageError').text('');
                $('#errorproducto').text('');
                $('#errorcantidadfisica').text('');
                $('#errorcantidadsistema').text('');

            })

        }
})

//eliminar datos agregados a la tabla detalle editar
$(document).on("click", "#InvDetalle tbody tr td button#removerInvFisicoDetalle", function () {
    $(this).closest('tr').remove();
    var currentRow = $(this).closest("tr");
    var prod_Codigo = currentRow.find("td:eq(0)").text(); // get current row 1st TD value
    var detalle = {
        prod_Codigo: prod_Codigo,
    };
    $.ajax({
        url: "/InventarioFisico/removeInvFisicoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ detalle: detalle }),
    });
});

//Solo numeros
function justNumbers(e) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if ((keynum == 48) || (keynum == 57))
        return true;
    return /\d/.test(String.fromCharCode(keynum));
}

//Enter
var contador = 0;
$(document).keypress(function (e) {
    var IDInput = e.target.id;
    if (e.which == 13) {
        if (IDInput == 'prod_CodigoBarras') {
            /////
            $(function () {
                var cod_Barras = $("#prod_CodigoBarras").val();
                $.ajax({
                    url: "/InventarioFisico/ProductosEnter",
                    method: "POST",
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({
                        cod_Barras: cod_Barras
                    }),
                }).done(function (data) {
                    if (data.length > 0) {
                        $.each(data, function (key, val) {
                            data_producto = val.prod_Codigo;
                            data_cantidadfisica = val.invfd_Cantidad;
                            data_uniId = val.uni_Id;
                            data_unidad = val.uni_Descripcion;
                            data_Descripcion = val.prod_Descripcion;
                            seleccionar(data_producto);
                            $('#prod_Codigo').val(data_producto);
                            $('#invfd_CantidadSistema').val('0');
                            $('#invfd_Cantidad').val('1');
                            $('#uni_Ids').val(data_uniId);
                            $('#uni_Id').val(data_unidad);
                            $('#prod_Descripcion').val(data_Descripcion);
                        })
                        $('#Error_Barras').text('');
                        $("#bodd_CantidadExistente").focus();
                
                    }
                    else {
                        $('#prod_Codigo').text();
                        $('#invfd_CantidadSistema').text();
                        $('#invfd_Cantidad').text('1');
                        $('#uni_Id').text();
                        $('#prod_Descripcion').text('');
                        $('#Error_Barras').text('');
                        $('#ErrorBarras_Create').after('<ul id="Error_Barras" class="validation-summary-errors text-danger">*Producto no existe</ul>');
                    }
                });
                return false;
            });
            return false;
        }
        else
            return false;
    }
});



//Imprimir
$('#Imprimir').click(function () {
    var a = document.createElement("a");
    a.target = "_blank";
    a.href = url;
    a.click();
})
var url = "";

//Cambio de bodega
function cambiobodega() {
    var bod_Id = $("#bod_Id").val();
    var data_producto = $("#prod_Codigo").val();
    $.ajax({
        url: "/InventarioFisico/cambiobodega",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ bod_Id: bod_Id }),
    })

}

$("#invfd_Cantidad_edit").on('keyup', function () {
    $('#errorcantidadfisica').hide();
}).keyup();

$("#invfd_Cantidad").on('keyup', function () {
    $('#errorcantidadfisica').hide();
}).keyup();

//Validar campos Especiales
$('#invf_Descripcion').keypress(function (e) {
    if (e.key.match(/[a-z0-9ñçáéíóú\s]/i) === null) {

        // Si la tecla pulsada no es la correcta, eliminado la pulsación
        e.preventDefault();
    }
})

//solo letras y numeros
function NumText(string) {
    var out = '';
    //Se añaden las letras validas
    var filtro = 'abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890';//Caracteres validos

    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) != -1)
            out += string.charAt(i);
    return out;
}


//reconteo
$('#aceptar_reconteo').click(function () {
    $('#errorol').hide();
    var id = $('#invf_Id').val();
    var User_NombreUsuario = $('#User_NombreUsuario').val();
    var User_Password = $('#User_Password').val();
    if (User_NombreUsuario == '') {
        $('#error_nombreusuario').after('<ul id="errorusuario" class="validation-summary-errors text-danger">Campo Usuario Requerido</ul>');
    }
    else if (User_Password == '') {
        $('#error_password').after('<ul id="errorcontra" class="validation-summary-errors text-danger">Campo Contraseña Requerido</ul>');
    } else {
        $.ajax({
            url: "/InventarioFisico/Reconteo",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: id, User_NombreUsuario: User_NombreUsuario, User_Password: User_Password }),
        }).done(function (incorrecto) {
            if (incorrecto == "incorrecto")
            {
                $('#error_rol').after('<ul id="errorol" class="validation-summary-errors text-danger">Usuario incorrecto o roles sin acceso.</ul>');
        }
        });
    }
})

$("#User_NombreUsuario").on('keyup', function () {
    $('#errorusuario').hide();
}).keyup();

$("#error_password").on('keyup', function () {
    $('#errorcontra').hide();
}).keyup();



//teclas rapidas 

$(document).keydown(function (e) {

    if ((e.key == 'g' || e.key == 'G') && (e.ctrlKey || e.metaKey)) {

        e.preventDefault();

        $("form").submit();

        return false;

    }

    return true;

});

//limpiar
$('#reconteo').click(function () {
    $('#User_NombreUsuario').val("");
    $('#User_Password').val("");
})