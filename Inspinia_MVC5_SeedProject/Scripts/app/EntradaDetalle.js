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

$('#AgregarDetalleEntrada').click(function () {
    var entrada = $("#ent_Id").val();
    var codigoproducto = $("#prod_Codigo").val();
    var cantidad = $("#entd_Cantidad").val();
    var unimedida = $("#uni_Id").val();

    //if (entrada == '') {
    //    $('#Errorentrada').text('');
    //    $('#Errorcodigoproducto').text('');
    //    $('#Errorcantidad').text('');
    //    console.log("entrada");
    //}
    //else
    if (codigoproducto == '') {
        $('#Mensajecodigo').text('');
        $('#Mensajecantidad').text('');
        $('#validationcodigoproducto').after('<ul id="Mensajecodigo" class="validation-summary-errors text-danger">Campo codigo producto Requerido</ul>');
        console.log("codigoproducto");
    }
    else if(cantidad == '') {
        $('#Mensajecodigo').text('');
        $('#Mensajecantidad').text('');
        $('#validationcantidad').after('<ul id="Mensajecantidad" class="validation-summary-errors text-danger">Campo cantidad Requerido</ul>');
        console.log("cantidad");

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

        copiar += "<td id = 'unimedida'>" + $('#uni_Id').val() + "</td>";

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
            $('#prod_Codigo').text('');
            $("#uni_Id").val();
            $('#entd_Cantidad').text('');
            

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

//$('#GuardarEntrada').click(function () {
//    TableLenght = $("#tbentrada tr").length;
//    var IdEntrada = $("#ent_Id").val();
//    if (TableLenght == 1) {
//        $('#MessageError').text('');
//        $('#ErrorDescripcion').text('');
//        $('#ErrorFecha').text('');
//        $('#validationSummary').after('<ul id="MessageError" class="validation-summary-errors text-danger">No se encontró ningún Detalle, favor agregue uno</ul>');
//    }
//    else {
//        $.ajax({
//            url: "/Entrada/GuardarEntrada",
//            method: "POST",
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify({ ent_Id: IdEntrada }),
//        }).done(function (data) {
//            if (data == 'Exito') {
//                location.reload();
//            }
//            else if (data == 'Error-01') {
//                $('#MessageError').text('');
//                $('#ErrorDescripcion').text('');
//                $('#ErrorFecha').text('');
//                $('#validationSummary').after('<ul id="MessageError" class="validation-summary-errors text-danger">No se ha reconocido el usuario, favor inicie sesión.</ul>');
//            }
//            else {
//                $('#MessageError').text('');
//                $('#ErrorDescripcion').text('');
//                $('#ErrorFecha').text('');
//                $('#validationSummary').after('<ul id="MessageError" class="validation-summary-errors text-danger">Se produjo un error, no se guardaron los registros.</ul>');
//            }
//        });
//    }
//});



