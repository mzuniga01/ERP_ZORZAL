var contador = 0;

//Validar Los campos numericos
function format(input) {
    var num = input.value.replace(/\,/g, '');
    if (!isNaN(num)) {
        input.value = num;
    }
    else {
        //alert('Solo se permiten numeros');
        input.value = input.value.replace(/[^\d\.]*/g, '');
    }
}
//fin


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
       
    }
    else if (Preorden == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
       
    }
  
    else if (Cminima == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        
    }
   
    else if (Cmaxima == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        
    }
   
    else if (Costo == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        
    }
    else if (Cpromedio == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        
    }

    else {
        //Aqui importa el orden
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'Producto'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'Descripcion_P'>" + $('#prod_Descripcion').val() + "</td>";
        copiar += "<td id = 'Costo'>" + $('#bodd_Costo').val() + "</td>";
        copiar += "<td id = 'Cpromedio'>" + $('#bodd_CostoPromedio').val() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'Preorden' hidden >" + $('#bodd_PuntoReorden').val() + "</td>";// aqui va el campo y luego se llena con el id del mismo, que ya ha capturado el valor
        copiar += "<td id = 'Cminima' hidden>" + $('#bodd_CantidadMinima').val() + "</td>";
        copiar += "<td id = 'Cmaxima' hidden >" + $('#bodd_CantidadMaxima').val() + "</td>";
        copiar += "<td>" + '<button id="removeBodegaDetalle" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        copiar += "</tr>";
        $('#tbBodega').append(copiar);

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
        bodd_Id: contador,
        //Fecha: $('#fechaCreate').val(),
    };
    return BODEGADETALLE;
}
//Fin

//Remover Detalle
$(document).on("click", "#tbBodega tbody tr td button#removeBodegaDetalle", function () {
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
    console.log(bodd_Id);

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

//Validar Cantidades Bodega Detalle
$(document).on('blur', '#bodd_PuntoReorden', function () {
    var Mn =  $('#bodd_CantidadMinima').val();
    var Pr = $('#bodd_PuntoReorden').val();
    console.log(Mn)
    console.log(Pr)
    if (Mn)


        if (Pr != '' && Mn != '') {

            if (parseFloat(Mn) > parseFloat(Pr)) {

                $('#Error_PuntoReorden').text('');
                $('#ErrorPuntoReorden_Create').after('<ul id="Error_PuntoReorden" class="validation-summary-errors text-danger">Punto Reorden debe ser Mayor de Cantidad Minima</ul>');
                console.log('1')
            }
            else {
                $('#Error_PuntoReorden').text('');
                console.log('2')
            }
        }
        

})

$(document).on('blur', '#bodd_CantidadMinima', function () {
    var Mn = $('#bodd_CantidadMinima').val();
    var Pr = $('#bodd_PuntoReorden').val();
    console.log(Mn)
    console.log(Pr)
    if (Mn)


        if (Pr != '' && Mn != '') {

            if (parseFloat(Mn) > parseFloat(Pr)) {

                $('#Error_CantidadMinima').text('');
                $('#ErrorCantidadMinima_Create').after('<ul id="Error_CantidadMinima" class="validation-summary-errors text-danger">Cantidad Minima debe ser Menor que Punto Reorden</ul>');
                console.log('1')
            }
            else {
                $('#Error_CantidadMinima').text('');
                console.log('2')
            }
        }


})

$(document).on('blur', '#bodd_CantidadMaxima', function () {
    var Mx = $('#bodd_CantidadMaxima').val();
    var Pr = $('#bodd_PuntoReorden').val();
    console.log(Mx)
    console.log(Pr)
    if (Mx)


        if (Pr != '' && Mx != '') {

            if (parseFloat(Mx) < parseFloat(Pr)) {

                $('#Error_CantidadMaxima').text('');
                $('#ErrorCantidadMaxima_Create').after('<ul id="Error_CantidadMaxima" class="validation-summary-errors text-danger">Cantidad Maxima debe ser Mayor que Punto Reorden</ul>');
                console.log('1')
            }
            else {
                $('#Error_CantidadMaxima').text('');
                console.log('2')
            }
        }


})









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