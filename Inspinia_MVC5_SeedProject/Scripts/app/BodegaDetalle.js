﻿var contador = 0;
$("#bod_Nombre").change(function () {
    var str = $("#bod_Nombre").val();
    var res = str.toUpperCase();
    $("#bod_Nombre").val(res);
});
//Telefono Formato Correcto
//Validar Teléfono
function validartel(e) {
    campo = event.target;
    $(campo).on("input", function (event) {
        var Telefono = this.value.match(/[0-9\s]+/);

        if (Telefono != null) {
            this.value = '+' + ((Telefono).toString().replace(/[^ 0-9a-záéíóúñ@._-\s]\d +/ig, ""));
        }
        else {
            this.value = null;
        }
    });
}
//Validar Correo Electronico
$('#bod_Correo').change(function (e) {
    var emailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var EmailId = this.value;
        correo = $("#bod_Correo").val();
  
    if (emailRegex.test(EmailId)) {
        $('#correo_error').text('');
        this.style.backgroundColor = "";
    }
       
    else
    {
        if (correo != "") {
            valido = document.getElementById('correo_error');
            valido.innerText = "Dirección de Correo Electrónico Incorrecto";
            return false
            
        }
    }
    

});
//Limpriar Validation Erros

//
//Validar Los campos string
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}
//Validar Los campos numericos
function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
    
}
function validaFloat(numero) {
    if (!/^([0-9])*[.]?[0-9]*$/.test(numero)) {
        alert("El valor " + numero + "  un NUMERO");
    }
    else {
        alert("El valor " + numero + " no es una LETRA");
    }
       
}
$(function () {
    $(".validar").keydown(function (event) {
        //alert(event.keyCode);
        if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) && event.keyCode !== 190 && event.keyCode !== 110 && event.keyCode !== 8 && event.keyCode !== 9) {
            return false;
        }
    });
});
function onKeyDecimal(e, thix) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if (document.getElementById(thix.id).value.indexOf('.') != -1 && keynum == 46)
        return false;
    if ((keynum == 8 || keynum == 48 || keynum == 46))
        return true;
    if (keynum <= 47 || keynum >= 58) return false;
    return /\d/.test(String.fromCharCode(keynum));
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
            "sZeroRecords": "No se encontraron resultados",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sEmptyTable": "No hay registros",
            "sInfoEmpty": "Mostrando 0 de 0 Entradas",
            "sSearch": "Buscar",
            "sLengthMenu": "Mostrar _MENU_Registros Por Página",
            "sInfo": "Mostrando _START_ a _END_ Entradas"

        },

    });

});
//Fin
///Busqueda products
$(document).ready(function () {
    $('#Table_BuscarProductoBodega').DataTable({

        "searching": true,
        "oLanguage": {
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior",
            },
            "sZeroRecords": "No se encontraron resultados",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sEmptyTable": "No hay registros",
            "sInfoEmpty": "Mostrando 0 de 0 Entradas",
            "sSearch": "Buscar",
            "sLengthMenu": "Mostrar _MENU_Registros Por Página",
            "sInfo": "Mostrando _START_ a _END_ Entradas"
        },

    });

    //var $rows = $('#Table_BuscarProductoBodega tr');
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
    });
$(document).on("click", "#Table_BuscarProductoBodega tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('animation');
    contentItem = $(this).closest('tr').data('content');
    uni_IdtItem = $(this).closest('tr').data('keyboard');
    psubctItem = $(this).closest('tr').data('container');
    pcatItem = $(this).closest('tr').data('interval');
    pBarras = $(this).closest('tr').data('id');
    $("#prod_CodigoBarras").val(pBarras);
    $("#prod_Codigo").val(idItem);
    $("#prod_Descripcion").val(contentItem);
    $("#uni_Id").val(uni_IdtItem);
    $("#pscat_Id").val(psubctItem);
    $("#pcat_Id").val(pcatItem);
    $('#Error_Barras').text('');
    $('#bodd_CantidadMinima').focus();
    
});
$("#ModalAgregarProducto").ready('hidden.bs.modal', function () {
    $('#bodd_CantidadMinima').focus();
});
///Fin

function formateo(input) {
    $(input).change(function () {
        var str = $(input).val();
        var res = str.toLowerCase();
        $(input).val(res);
    });
    $(input).on("keypress", function () {
        $input = $(this);
        setTimeout(function () {
            $input.val($input.val().toLowerCase());
        }, 50);
    })
}

function BlockChars(event) {
    if (event.keyCode < 65 && event.keyCode > 90)//los numeros son codigos ASCII
        event.keyCode = 0;
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
function camposDecimales(e) {

    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[1234567890]+$/.test(tecla);

}
function controlCaracteres(e) {
  
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ1234567890# ,.]+$/.test(tecla);

}

function Caracteres_Email(e) {

    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ1234567890@.-_]+$/.test(tecla);

}

function CorreoElectronico(string) {//Algunos caracteres especiales para el correo
    var out = '';
    //Se añaden las letras validas
    var filtro = 'abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890@ .-_';//Caracteres validos

    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) != -1)
            out += string.charAt(i);

    return out;
}

function CaracteresTelefono(e) {

    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[1234567890+-]+$/.test(tecla);

}
function CaracteresNombre(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ1234567890 ]+$/.test(tecla);

}
//function CaracteresTelefono_borrar(string) {//solo letras y numeros
//    var out = '';
//    //Se añaden las letras validas
//    var filtro = '1234567890-+';//Caracteres validos

//    for (var i = 0; i < string.length; i++)
//        if (filtro.indexOf(string.charAt(i)) != -1)
//            out += string.charAt(i);

//    return out;
//}
//}

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

//Pantalla Edit
$('#AgregarBodegaDetalle').click(function () {
    var Producto = $('#prod_Codigo').val();
    var Cminima = $('#bodd_CantidadMinima').val();
    var Preorden = $('#bodd_PuntoReorden').val();
    var Cmaxima = $('#bodd_CantidadMaxima').val();
    var Costo = $('#bodd_Costo').val();
    var Cpromedio = $('#bodd_CostoPromedio').val();
    var Cbarras = $('#prod_CodigoBarras').val();

    if (Producto == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorProducto_Create').after('<ul id="Error_Producto" class="validation-summary-errors text-danger">*Codigo Producto Requerido</ul>');

    }
    else if (Cbarras == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#Error_Barras').text('');
        $('#ErrorBarras_Create').after('<ul id="Error_Barras" class="validation-summary-errors text-danger">*Codigo De Barras Requerido</ul>');
    }
    else if (Cminima == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCantidadMinima_Create').after('<ul id="Error_CantidadMinima" class="validation-summary-errors text-danger">*Cantidad Miníma Requerido</ul>');
    }

    else if (Preorden == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorPuntoReorden_Create').after('<ul id="Error_PuntoReorden" class="validation-summary-errors text-danger">*Campo Punto Reorden Requerido</ul>');
    }


    else if (Cmaxima == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCantidadMaxima_Create').after('<ul id="Error_CantidadMaxima" class="validation-summary-errors text-danger">*Cantidad Máxima Requerido</ul>');

    }

    else if (Costo == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCosto_Create').after('<ul id="Error_Costo" class="validation-summary-errors text-danger">*Campo Costo Requerido</ul>');

    }
    else if (Cpromedio == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCostoPromedio_Create').after('<ul id="Error_CostoPromedioo" class="validation-summary-errors text-danger">*Campo Costo Promedio Requerido</ul>');
    }

    else {
        //Aqui importa el orden
        console.log(Cbarras, 'prueba');
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'Cbarras'>" + Cbarras + "</td>";
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
                $('#prod_CodigoBarras').val('');
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
    };
    return BODEGADETALLE;
}

//Remover Detalle Ventana Edit
$(document).on("click", "#tbBodega tbody tr td button#removeBodegaDetalle", function () {
    //$(this).closest('tr').remove();
    //idItem = $(this).closest('tr').data('id');
    //var prod_Codigo = currentRow.find("td:eq(0)").text();
    idItem = $(this).closest('tr').data('id');
    var vprod_Codigo = $(this).closest("tr").find("td:eq(0)").text();
    var BorrarItems = {
        prod_Codigo: vprod_Codigo,
    };
    var table = $('#tbBodega').DataTable();
    table.row($(this).parents('tr'))
        .remove()
        .draw();
    $.ajax({
        url: "/Bodega/removeBodegaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ BorrarItem: BorrarItems }),
    });
});
////Fin

///Agregar detalle pantalla create (clik)
$('#AgregarBodegaDetalle_Prueba').click(function () {
    console.log('boton');
    var table = $('#tbBodega').DataTable();
    var Producto = $('#prod_Codigo').val();
    var Cminima = $('#bodd_CantidadMinima').val();
    var Preorden = $('#bodd_PuntoReorden').val();
    var Cmaxima = $('#bodd_CantidadMaxima').val();
    var Costo = $('#bodd_Costo').val();
    var Cpromedio = $('#bodd_CostoPromedio').val();
    var Cbarras = $('#prod_CodigoBarras').val();

    if (Producto == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorProducto_Create').after('<ul id="Error_Producto" class="validation-summary-errors text-danger">*Codigo De Barra Requerido</ul>');

    }
    else if (Cbarras == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#Error_Barras').text('');
        $('#ErrorBarras_Create').after('<ul id="Error_Barras" class="validation-summary-errors text-danger">*Codigo De Barras Requerido</ul>');
    }
    else if (Cminima == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCantidadMinima_Create').after('<ul id="Error_CantidadMinima" class="validation-summary-errors text-danger">*Cantidad Miníma Requerido</ul>');
    }

    else if (Preorden == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorPuntoReorden_Create').after('<ul id="Error_PuntoReorden" class="validation-summary-errors text-danger">*Campo Punto Reorden Requerido</ul>');
    }


    else if (Cmaxima == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCantidadMaxima_Create').after('<ul id="Error_CantidadMaxima" class="validation-summary-errors text-danger">*Cantidad Máxima Requerido</ul>');

    }

    else if (Costo == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCosto_Create').after('<ul id="Error_Costo" class="validation-summary-errors text-danger">*Campo Costo Requerido</ul>');

    }
    else if (Cpromedio == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCostoPromedio_Create').after('<ul id="Error_CostoPromedioo" class="validation-summary-errors text-danger">*Campo Costo Promedio Requerido</ul>');
    }

    else {
        //Aqui importa el orden
        //contador = contador + 1;
        //copiar = "<tr data-id=" + contador + ">";
        //copiar += "<td id = 'Cbarras'>" + $('#prod_CodigoBarras').val() + "</td>";
        //copiar += "<td id = 'Producto'>" + $('#prod_Codigo').val() + "</td>";
        //copiar += "<td id = 'Descripcion_P'>" + $('#prod_Descripcion').val() + "</td>";
        //copiar += "<td id = 'Costo'>" + $('#bodd_Costo').val() + "</td>";
        //copiar += "<td id = 'Cpromedio'>" + $('#bodd_CostoPromedio').val() + "</td>";
        //copiar += "<td id = 'Preorden'  >" + $('#bodd_PuntoReorden').val() + "</td>";// aqui va el campo y luego se llena con el id del mismo, que ya ha capturado el valor
        //copiar += "<td id = 'Cminima' >" + $('#bodd_CantidadMinima').val() + "</td>";
        //copiar += "<td id = 'Cmaxima'  >" + $('#bodd_CantidadMaxima').val() + "</td>";
        //copiar += "<td>" + '<button id="removeBodegaDetalle" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        //copiar += "</tr>";
        //$('#tbBodega').append(copiar);

        var tbBodegaDetalle = Getbodegadetalle();
        $.ajax({
            url: "/Bodega/SaveBodegaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ BODEGADETALLE: tbBodegaDetalle }),
        }).done(function (data) {
            $("#prod_CodigoBarras").removeAttr("readonly");
            if (data == prod_Codigo) {
                $("#tbBodega td").each(function () {
                    var prueba = $(this).text()
                    if (prueba == prod_Codigo) {
                        alert(prod_Codigo)
                        table.row($(this).parents('tr')).remove().draw();
                        var trPro = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);
                        table.row.add([
                            $('#prod_CodigoBarras').val(),
                            $('#prod_Codigo').val(),
                            $('#prod_Descripcion').val(),
                            $('#bodd_CantidadMinima').val(),
                            $('#bodd_PuntoReorden').val(),
                            $('#bodd_CantidadMaxima').val(),
                            $('#bodd_Costo').val(),
                            $('#bodd_CostoPromedio').val(),
                            '<button id = "removeBodegaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Quitar</button>'

                        ]).draw(false);
                    }
                });

            }
            else {
                table.row.add([
                    $('#prod_CodigoBarras').val(),
                    $('#prod_Codigo').val(),
                    $('#prod_Descripcion').val(),
                    $('#bodd_CantidadMinima').val(),
                    $('#bodd_PuntoReorden').val(),
                    $('#bodd_CantidadMaxima').val(),
                    $('#bodd_Costo').val(),
                    $('#bodd_CostoPromedio').val(),
                    '<button id = "removeBodegaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Quitar</button>'

                ]).draw(false);
            }

        }).done(function (data) {
            $('#prod_Codigo').val('');
            $('#prod_CodigoBarras').val('');
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
            $('#Error_Barras').text('');
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