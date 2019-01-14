var contador = 0;

//para busqueda de productos
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
$(document).on("click", "#Table_BuscarProducto tbody tr td button#seleccionar", function () {
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

//para eliminar
$(document).on("click", "#tbentrada tbody tr td button#Eliminardetalleentrada", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');

    var Eliminar = {
        ent_Id: idItem,
    };
    $.ajax({
        url: "/Entrada/Eliminardetalleentrada",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ EntradaDetalle: Eliminar }),
    });
});
//para añadir codigo ala tabla temporal(Create)
$('#AgregarDetalleEntrada').click(function () {
    var entrada = $("#ent_Id").val();
    var codigoproducto = $("#prod_Codigo").val();
    var cantidad = $("#entd_Cantidad").val();
    var unimedida = $("#uni_Id").val();

    if (entrada == '') {
        //$('#Errorentrada').text('');
        //$('#Errorcodigoproducto').text('');
        //$('#Errorcantidad').text('');
        console.log("entrada");
    //}
    //else
    //if (codigoproducto == '') {
    //    $('#Mensajecodigo').text('');
    //    $('#Mensajecantidad').text('');
    //    $('#validationcodigoproducto').after('<ul id="Mensajecodigo" class="validation-summary-errors text-danger">Campo codigo producto Requerido</ul>');
    //    console.log("codigoproducto");
    //}
    //else if(cantidad == '') {
    //    $('#Mensajecodigo').text('');
    //    $('#Mensajecantidad').text('');
    //    $('#validationcantidad').after('<ul id="Mensajecantidad" class="validation-summary-errors text-danger">Campo cantidad Requerido</ul>');
    //    console.log("cantidad");

    //}
    //else if (unimedida == '') {
    //    $('#Errorentrada').text('');
    //    $('#Errorcodigoproducto').text('');
    //    $('#validationcodigoproducto').after('<ul id="validationunimedida" class="validation-summary-errors text-danger">Campo codigo producto Requerido</ul>');
    //    console.log("codigoproducto");
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        //copiar += "<td>" + $('#ent_Id option:selected').text() + "</td>";
        //copiar += "<td hidden id='ent_Id'>" + $('#ent_Id option:selected').val() + "</td>";

        copiar += "<td id = 'codigoproducto'>" + $('#prod_Codigo').val() + "</td>";

        copiar += "<td>" + $('#uni_Id option:selected').text() + "</td>";
        copiar += "<td hidden id='uni_Id'>" + $('#unimedida option:selected').val() + "</td>";

        //copiar += "<td id = 'unimedida'>" + $('#uni_Id').val() + "</td>";


        copiar += "<td id = 'cantidad'>" + $('#entd_Cantidad').val() + "</td>";

        copiar += "<td>" + '<button id="Eliminardetalleentrada" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tbentrada').append(copiar);

        var EntradaDetalle = GetEntradaDetalle();
        $.ajax({
            url: "/Entrada/Guardardetalleentrada",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ entradadetalle: EntradaDetalle }),
        })
        .done(function (data) {
            $('#prod_Codigo').val('');
            $("#uni_Id").val('');
            $('#entd_Cantidad').val('');
             //
            $('#prod_Descripcion').val('');
            $('#pscat_Id').val('');

            $('#Mensajecodigo').text('');
            $('#Mensajecantidad').text('');

        });
    }
})
function GetEntradaDetalle() {
    var EntradaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        uni_Id: $('#uni_Id').val(),
        entd_Cantidad: $('#entd_Cantidad').val(),
        ent_UsuarioCrea: contador,
        ent_Id: contador,
        //Fecha: new Date($('#fechaCreate').val()),
        //Fecha: $('#fechaCreate').val(),
    };
    return EntradaDetalle;
}


$(document).on("click", "#Table_BuscarProducto tbody tr td button#seleccionar", function () {
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


//actualizar Detalle Entrada
function btnActualizarentrada(entd_Id) {
    console.log(entd_Id);
    var coProductoEdit = $("#coProductoEdit").val();
    var cantidadEdit = $("#cantidadEdit").val();
    var unidadEdit = $("#unidadEdit").val();
    console.log(coProductoEdit);
    console.log(cantidadEdit);
    console.log(unidadEdit);

    var tbEntradaDetalle = Getentradadetalle_actualizar();

    $.ajax({
        url: "/Entrada/entradadetalle_actualizar",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ actualizarEntradaDetalle: tbEntradaDetalle }),
    }).done(function (data) {
        if (data == '') {
            location.reload();
        }
        else if (data == '-1') {
            $('#MensajeError' + entd_Id).text('');
            $('#ValidationMessageFor' + entd_Id).after('<ul id="MensajeError' + entd_Id + '" class="validation-summary-errors text-danger">No se ha podido Actualizar el registro.</ul>');
        }
        else {
            $('#MensajeError' + entd_Id).text('');
            $('#ValidationMessageFor' + entd_Id).after('<ul id="MensajeError' + entd_Id + '" class="validation-summary-errors text-danger">Campo Requerido</ul>');
        }
    });
}
function Getentradadetalle_actualizar() {

    var actualizarEntradaDetalle = {
        ent_Id: $('#ent_Id').val(),
        entd_Id: $('#entd_Id').val(),
        prod_Codigo: $('#coProductoEdit').val(),
        entd_Cantidad: $("#cantidadEdit").val(),
        entd_UsuarioCrea: $('#entd_UsuarioCrea').val(),
        entd_UsuarioModifica: $('#entd_UsuarioModifica').val(),
        entd_FechaCrea: $('#entd_FechaCrea').val(),
        entd_FechaModifica: $('#entd_FechaModifica').val(),
        uni_Id: $('#unidadEdit').val(),
    };
    return actualizarEntradaDetalle;
}

//para inprimir
$('#btnImprimir').click(function () {
    // Function available at https://gist.github.com/sixlive/55b9630cc105676f842c  
    $.fn.printDiv = function () {
        var printContents = $(this).html();
        var originalContents = $('body').html();
        $('body').html(printContents);
        $('body').addClass('js-print');
        window.print();
        $('body').html(originalContents);
        $('body').removeClass('js-print');
    };

    // Print
    $('[data-print]').click(function () {
        $('[data-print-content]').printDiv();
    });
});











//codigo para guardar detalle en la vista de editar
//$('#GuardarDetalleEntrada').click(function () {

//        var EntradaDetalle = GetEntradaDetalle();
//        $.ajax({
//            url: "/Entrada/GuardardetalleentradaEditar",
//            method: "POST",
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify({ entradadetalle: EntradaDetalle }),
//        })
//        .done(function (data) {
//            $('#prod_Codigo').val('');
//            $("#uni_Id").val('');
//            $('#entd_Cantidad').val('');
//            //
//            $('#prod_Descripcion').val('');
//            $('#pscat_Id').val('');

//            $('#Mensajecodigo').text('');
//            $('#Mensajecantidad').text('');

//        });
//})
