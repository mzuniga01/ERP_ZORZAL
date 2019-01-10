var contador = 0;

//js Todas las tablas
$(document).ready(function () {
    $('#tbBodega').DataTable({

        "searching": true,
        "oLanguage": {
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior",
            },
            "sSearch": "Buscar",
            "sLengthMenu": "Mostrar _MENU_Registros Por Página",
            "sInfo": "Mostrando _START_ a _END_ Entradas"

        },

    });

});
//Fin

///Busqueda products
$(document).ready(function () {
    $('#Table_BuscarProductoBodega').DataTable(
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

    var $rows = $('#Table_BuscarProductoBodega tr');
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
$(document).on("click", "#Table_BuscarProductoBodega tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    contentItem = $(this).closest('tr').data('content');
    uni_IdtItem = $(this).closest('tr').data('keyboard');
    psubctItem = $(this).closest('tr').data('container');
    pcatItem = $(this).closest('tr').data('interval');
    $("#prod_Codigo").val(idItem);
    $("#prod_Descripcion").val(contentItem);
    $("#uni_Id").val(uni_IdtItem);
    $("#pscat_Id").val(psubctItem);
    $("#pcat_Id").val(pcatItem);
    //$("#cod").val(idItem);
});
///Fin


//Agregar Detalle a Bodega
$('#AgregarBodegaDetalle').click(function () {
    var Producto = $('#prod_Codigo').val();
    var Preorden = $('#bodd_PuntoReorden').val();
    var Cminima = $('#bodd_CantidadMinima').val();
    var Cmaxima = $('#bodd_CantidadMaxima').val();
    var Costo = $('#bodd_Costo').val();
    var Cpromedio = $('#bodd_CostoPromedio').val();


    if (Producto == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorProducto_Create').after('<ul id="Error_Producto" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    else if (Preorden == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorPuntoReorden_Create').after('<ul id="Error_PuntoReorden" class="validation-summary-errors text-danger">Campo Punto Reorden Requerido</ul>');
    }
    else if (Cminima == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCantidadMinima_Create').after('<ul id="Error_CantidadMinima" class="validation-summary-errors text-danger">Campo Canidad Mínima Requerido</ul>');
    }
    else if (Cmaxima == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCantidadMaxima_Create').after('<ul id="Error_CantidadMaxima" class="validation-summary-errors text-danger">Campo Cantidad Máxima Requerido</ul>');
    }
    else if (Costo == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCosto_Create').after('<ul id="Error_Costo" class="validation-summary-errors text-danger">Campo Costo Requerido</ul>');
    }
    else if (Cpromedio == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCostoPromedio_Create').after('<ul id="Error_CostoPromedioo" class="validation-summary-errors text-danger">Campo Costo Promedio Requerido</ul>');
    }

    else {
        //Aqui importa el orden
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'Producto'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'Preorden'>" + $('#bodd_PuntoReorden').val() + "</td>";// aqui va el campo y luego se llena con el id del mismo, que ya ha capturado el valor
        copiar += "<td id = 'Cminima'>" + $('#bodd_CantidadMinima').val() + "</td>";
        copiar += "<td id = 'Cmaxima'>" + $('#bodd_CantidadMaxima').val() + "</td>";
        copiar += "<td id = 'Costo'>" + $('#bodd_Costo').val() + "</td>";
        copiar += "<td id = 'Cpromedio'>" + $('#bodd_CostoPromedio').val() + "</td>";
        copiar += "<td>" + '<button id="removeBodegaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblBodegadetalle_Create').append(copiar);

        var tbBodegaDetalle = Getbodegadetalle();
        $.ajax({
            url: "/Bodega/SaveBodegaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ BODEGADETALLE: tbBodegaDetalle }),
        })
            .done(function (data) {
                $('#prod_Codigo').val('');
                $('#prod_Descripcion').val('');
                $('#pcat_Id').val('');
                $('#pscat_Id').val('');
                $('#uni_Id').val('');
                $('#bodd_PuntoReorden').val('');
                $('#bodd_CantidadMinima').val('');
                $('#bodd_CantidadMaxima').val('');
                $('#bodd_Costo').val('');
                $('#bodd_CostoPromedio').val(''); 

                $('#MessageError').text('');
                $('#Error_Producto').text('');
                $('#Error_PuntoReorden').text('');
                $('#Error_CantidadMinima').text('');
                $('#Error_CantidadMaxima').text('');
                $('#Error_Costo').text('');
                $('#Error_CostoPromedioo').text('');
            });



    }
});
function Getbodegadetalle() {
    var BODEGADETALLE = {
        prod_Codigo: $('#prod_Codigo').val(),
        bodd_puntoReorden: $('#bodd_PuntoReorden').val(),
        bodd_cantidadMinima: $('#bodd_CantidadMinima').val(),
        bodd_cantidadMaxima: $('#bodd_CantidadMaxima').val(), 
        bodd_costo: $('#bodd_Costo').val(),
        bodd_costoPromedio: $('#bodd_CostoPromedio').val(),
        bodd_UsuarioCrea: contador,
        bodd_Id : contador,
        //Fecha: $('#fechaCreate').val(),
    };
    return BODEGADETALLE;
}
//Fin

//Remover Detalle
$(document).on("click", "#tblBodegadetalle_Create tbody tr td button#removeBodegaDetalle", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var BorrarItems = {
        bodd_Id : idItem,
    };
    $.ajax({
        url: "/Bodega/removeBodegaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ BorrarItem: BorrarItems }),
    });
});
//Fin


//Actualizar Detalle Bodega
function btnActualizarBodegaDetalle(bodd_Id) {

    var Producto = $('#prod_Codigo').val();
    var Preorden = $('#bodd_PuntoReorden').val();
    var Cminima = $('#bodd_CantidadMinima').val();
    var Cmaxima = $('#bodd_CantidadMaxima').val();
    var Costo = $('#bodd_Costo').val();
    var Cpromedio = $('#bodd_CostoPromedio').val();


    if (Producto == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorProducto_Create').after('<ul id="Error_Producto" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    else if (Preorden == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorPuntoReorden_Create').after('<ul id="Error_PuntoReorden" class="validation-summary-errors text-danger">Campo Punto Reorden Requerido</ul>');
    }
    else if (Cminima == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCantidadMinima_Create').after('<ul id="Error_CantidadMinima" class="validation-summary-errors text-danger">Campo Canidad Mínima Requerido</ul>');
    }
    else if (Cmaxima == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCantidadMaxima_Create').after('<ul id="Error_CantidadMaxima" class="validation-summary-errors text-danger">Campo Cantidad Máxima Requerido</ul>');
    }
    else if (Costo == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCosto_Create').after('<ul id="Error_Costo" class="validation-summary-errors text-danger">Campo Costo Requerido</ul>');
    }
    else if (Cpromedio == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCostoPromedio_Create').after('<ul id="Error_CostoPromedioo" class="validation-summary-errors text-danger">Campo Costo Promedio Requerido</ul>');
    }

    else {

        var tbBodegaDetalle = Getbodegadetalle_UPDATE();

        $.ajax({
            url: "/Bodega/UpdateBodegaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ACTUALIZAR_tbBodegaDetalle: tbBodegaDetalle }),
        }).done(function (data) {
            if (data == '') {
                location.reload();
            }
            else if (data == '-1') {
                $('#MensajeError' + bodd_Id).text('');
                $('#ValidationMessageFor' + bodd_Id).after('<ul id="MensajeError' + bodd_Id + '" class="validation-summary-errors text-danger">No se ha podido Actualizar el registro.</ul>');
            }
            else {
                $('#MensajeError' + bodd_Id).text('');
                $('#ValidationMessageFor' + bodd_Id).after('<ul id="MensajeError' + bodd_Id + '" class="validation-summary-errors text-danger">Campo Requerido</ul>');
            }
        });
    }
}
function Getbodegadetalle_UPDATE() {
    var ACTUALIZAR_tbBodegaDetalle = {
        bod_Id: $('#bod_Id').val(),
        bodd_Id: $('#bodd_Id').val(),
        prod_Codigo: $('#prod_Codigo').val(),
        bodd_puntoReorden: $('#bodd_PuntoReorden').val(),
        bodd_cantidadMinima: $('#bodd_CantidadMinima').val(),
        bodd_cantidadMaxima: $('#bodd_CantidadMaxima').val(),
        bodd_UsuarioCrea: $('#bodd_UsuarioCrea').val(),
        bodd_FechaCrea: $('#bodd_FechaCrea').val(),
        bodd_UsuarioModifica: $('#bodd_UsuarioModifica').val(),
        bodd_FechaModifica: $('#bodd_FechaModifica').val(),
        bodd_costo: $('#bodd_Costo').val(),
        bodd_costoPromedio: $('#bodd_CostoPromedio').val(),
        
        //Fecha: $('#fechaCreate').val(),
    };
    return ACTUALIZAR_tbBodegaDetalle;
}
//Fin


//Crear Nuevo Detalle Bodega Atraves de Modal
$('#btnGuardarNuevoDetalle').click(function () {
    var Producto = $('#prod_Codigo').val();
    var Preorden = $('#bodd_PuntoReorden').val();
    var Cminima = $('#bodd_CantidadMinima').val();
    var Cmaxima = $('#bodd_CantidadMaxima').val();
    var Costo = $('#bodd_Costo').val();
    var Cpromedio = $('#bodd_CostoPromedio').val();


 

        var tbBodegaDetalle = Getbodegadetalle();
        $.ajax({
            url: "/Bodega/SaveNuevoDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ GUARDAR_NUEVO_DETALLE: tbBodegaDetalle }),
                })
            .done(function (data) {
                $('#prod_Codigo').val('');
                $('#bodd_PuntoReorden').val('');
                $('#bodd_CantidadMinima').val('');
                $('#bodd_CantidadMaxima').val('');
                $('#bodd_Costo').val('');
                $('#bodd_CostoPromedio').val('');

                $('#MessageError').text('');
                $('#Error_Producto').text('');
                $('#Error_PuntoReorden').text('');
                $('#Error_CantidadMinima').text('');
                $('#Error_CantidadMaxima').text('');
                $('#Error_Costo').text('');
                $('#Error_CostoPromedioo').text('');
            });
});
function Getbodegadetalle() {
    var GUARDAR_NUEVO_DETALLE = {
        bod_Id: $('#bod_Id').val(),
        bodd_Id: $('#bodd_Id').val(),
        prod_Codigo: $('#prod_Codigo').val(),
        bodd_puntoReorden: $('#bodd_PuntoReorden').val(),
        bodd_cantidadMinima: $('#bodd_CantidadMinima').val(),
        bodd_cantidadMaxima: $('#bodd_CantidadMaxima').val(),
        bodd_UsuarioCrea: $('#bodd_UsuarioCrea').val(),
        bodd_FechaCrea: $('#bodd_FechaCrea').val(),
        bodd_UsuarioModifica: $('#bodd_UsuarioModifica').val(),
        bodd_FechaModifica: $('#bodd_FechaModifica').val(),
        bodd_costo: $('#bodd_Costo').val(),
        bodd_costoPromedio: $('#bodd_CostoPromedio').val(),
    };
    return GUARDAR_NUEVO_DETALLE;
}
//Fin


















//Busqueda Producto AutoComplete
//$(function () {
//    $("#prod_Codigo").autocomplete({
//        source: "/Bodega/BuscarProductos"
//    });
//});
//Fin

//2da Opcion
//$(function () {
//    $("#prod_Codigo").autocomplete({
//        source: "/Bodega/BuscarProductos"
//    });
//});
//$('#RegistrarNuevoDetalle').modal('show');
//$("#prod_Codigo").autocomplete({
//    source: "/Bodega/BuscarProductos"
//});
//Fin

//3ra Opcion
//$("#prod_Codigo").autocomplete({
//    source: function (request, response) {
//        var results = $.ui.autocomplete.filter(prod_CodigoArray, request.term);
//        response(results.slice(0, 10));
//    },
//    minLength: 3
//});

//Fin



//$('#prod_Codigo').mdb_autocomplete({
//    data: states
//});