var contador = 0;

$('#AgregarDetalleDevolucion').click(function () {
    var date = $('#fechaCreate').val();
    if ($('#prod_Codigo').val() == '') {
        $('#MessageError').text('');
        $('#ErrorFecha').text('');
        $('#ErrorDescripcion').text('');
        $('#validationDescripcion').after('<ul id="ErrorDescripcion" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    if ($('#devd_Descripcion').val() == '') {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorFecha').text('');
        $('#validationFecha').after('<ul id="ErrorFecha" class="validation-summary-errors text-danger">Campo´Descripìon Requerido</ul>');

    if ($('#devd_Descripcion').val() == '') {
            $('#MessageError').text('');
            $('#ErrorDescripcion').text('');
            $('#ErrorFecha').text('');
            $('#validationFecha').after('<ul id="ErrorFecha" class="validation-summary-errors text-danger">Campo´Descripìon Requerido</ul>');
   
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'prod_Codigo'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'devd_Descripcion'>" + $('#devd_Descripcion').val() + "</td>";
        copiar += "<td id = 'devd_CantidadProducto'>" + $('#devd_CantidadProducto').val() + "</td>";
        copiar += "<td>" + '<button id="removeFacturaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        //console.log('CodTipoCasoExito', $('#CodTipoCasoExito option:selected').text());
        $('#tbDetalleDevolucion').append(copiar);

        var CasoExito = GetCasosExito();
        $.ajax({
            url: "/Instructor/SaveCasoExito",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CasoExito: CasoExito }),
        })
        .done(function (data) {
            $('#prod_Codigo').val('');
            $('#tdevd_Descripcion').val('');
            $('#MessageError').text('');
            $('#ErrorDescripcion').text('');
            $('#ErrorFecha').text('');
        });
    }
});