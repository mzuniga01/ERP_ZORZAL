var contador = 0;
//para eliminar
$(document).on("click", "#tblSalidaDetalle tbody tr td button#Eliminardetallesalida", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');

    var Eliminar = {
        ent_Id: idItem,
    };
    $.ajax({
        url: "/Salida/Eliminardetallesalida",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ SalidaDetalle: Eliminar }),
    });
});



$('#AgregarSalidaDetalle').click(function () {
    var CodigoSalida = $('#sal_Id').val()
    var Producto = $('#prod_Codigo').val()
    var Cantidad = $('#sal_Cantidad').val()

    if (CodigoSalida == '') {
        $('#MessageError').text('');
        $('#CodigoSalida').text('');
        $('#ErrorSalidaVacio').text('');
        $('#ErrorCantidadVacio').text('');
        $('#ErrorProductoVacio').text('');
        $('#validationCodigoSalida').after('<ul id="ErrorSalidaVacio" class="validation-summary-errors text-danger">Campo Código Salida Requerido</ul>');
    }
   else if (Producto == '') {
       $('#MessageError').text('');
       $('#Producto').text('');
       $('#ErrorProductoVacio').text('');
       $('#ErrorCantidadVacio').text('');
       $('#ErrorSalidaVacio').text('');
        $('#validationprod_CodigoCreate').after('<ul id="ErrorProductoVacio" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }

else if (Cantidad == '')
    {
        $('#MessageError').text('');
        $('#Cantidad').text('');
        $('#ErrorCantidadVacio').text('');
        $('#ErrorProductoVacio').text('');
        $('#ErrorSalidaVacio').text('');
        $('#validationCantidadCreate').after('<ul id="ErrorCantidadVacio" class="validation-summary-errors text-danger">Campo Cantidad Requerido</ul>');
    }
    else
    {
        contador = +1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td>" + $('#sal_Id option:selected').text() + "</td>";
        copiar += "<td hidden id='CodigoSalida'>" + $('#sal_Id option:selected').val() + "</td>";

        copiar += "<td id = 'prod_Codigo'>" + $('#prod_Codigo').val() + "</td>";

        copiar += "<td id = 'CantidadCreate'>" + $('#sal_Cantidad').val() + "</td>";
        copiar += "<td>" + '<button id="Eliminardetallesalida" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblSalidaDetalle').append(copiar);
    }
     
    var SalidaDetalle = GetSalidaDetalle();
    $.ajax({
        url: "/SalidaController/SaveSalidaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ salidadetalle: SalidaDetalle }),
    })
        .done(function (data) {
            //$('#DescripcionCreate').val('');
            $('#MessageError').text('');
            $('#ErrorDescripcion').text('');
            $('#ErrorFecha').text('');
        });

})

function GetSalidaDetalle() {
    var CasosExito = {
        sal_Id: $('#sal_Id').val(),
        prod_Codigo: $('#prod_Codigo').val(),
        sal_Cantidad: $('#sal_Cantidad').val(),
       sald_Id: contador,
        //Fecha: new Date($('#fechaCreate').val()),
        //Fecha: $('#fechaCreate').val(),
    };
    return CasosExito;
}