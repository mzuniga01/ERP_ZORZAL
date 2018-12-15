var contador = 0;
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
    var cantidad = $("#cantidad").val();


    if (entrada  == '')
    {
        $('#MessageError').text('');
        $('#entrada').text('');
        $('#Errorentrada').text('');
        $('#Errorcodigoproducto').text('');
        $('#Errorcantidad').text('');
        $('#validationentrada').after('<ul id="Errorentrada" class="validation-summary-errors text-danger">Campo entrada Requerido</ul>');
    }
    else if ( codigoproducto== '')
    {
        $('#MessageError').text('');
        $('#codigoproducto').text('');
        $('#Errorentrada').text('');
        $('#Errorcodigoproducto').text('');
        $('#Errorcantidad').text('');
        $('#validationcodigoproducto').after('<ul id="Errorcodigoproducto" class="validation-summary-errors text-danger">Campo codigo producto Requerido</ul>');
    }
    else if (cantidad == '')
    {
        $('#MessageError').text('');
        $('#cantidad').text('');
        $('#Errorentrada').text('');
        $('#Errorcodigoproducto').text('');
        $('#Errorcantidad').text('');
        $('#validationcantidad').after('<ul id="Errorcantidad" class="validation-summary-errors text-danger">Campo cantidad Requerido</ul>');
    } else
    {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td>" + $('#ent_Id option:selected').text() + "</td>";
        copiar += "<td hidden id='ent_Id'>" + $('#entrada option:selected').val() + "</td>";

        copiar += "<td>" + $('#prod_Codigo option:selected').text() + "</td>";
        copiar += "<td hidden id='prod_Codigo'>" + $('#codigoproducto option:selected').val() + "</td>";
       
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
            $('#DescripcionCreate').val('');
            $('#MessageError').text('');
            $('#ErrorDescripcion').text('');
            $('#ErrorFecha').text('');
        });
    }
})
function GetEntradaDetalle() {
    var EntradaDetalle = {
        prod_Codigo: $('#codigoproducto').val(),
        entd_Cantidad: $('#cantidad').val(),
        ent_Id: contador,
        //Fecha: new Date($('#fechaCreate').val()),
        //Fecha: $('#fechaCreate').val(),
    };
    return EntradaDetalle;
}