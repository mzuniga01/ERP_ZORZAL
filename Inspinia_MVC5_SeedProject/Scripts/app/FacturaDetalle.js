var contador = 0;

$(document).on("click", "#tblDetalleFactura tbody tr td button#removeFacturaDetalle", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var CasoExitos = {
        CodInstructorCasoExito: idItem,
    };
    $.ajax({
        url: "/Instructor/RemoveCasoExito",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CasoExito: CasoExitos }),
    });
});

$('#AgregarDetalleFactura').click(function () {
    var date = $('#fechaCreate').val();
    if ($('#prod_Codigo').val() == '') {
        $('#MessageError').text('');
        $('#ErrorFecha').text('');
        $('#ErrorDescripcion').text('');
        $('#validationDescripcion').after('<ul id="ErrorDescripcion" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    else if ($('#tbProducto_prod_Descripcion').val() == '') {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorFecha').text('');
        $('#validationFecha').after('<ul id="ErrorFecha" class="validation-summary-errors text-danger">Campo´Descripìon Requerido</ul>');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'prod_Codigo'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'tbProducto_prod_Descripcion'>" + $('#tbProducto_prod_Descripcion').val() + "</td>";
        copiar += "<td id = 'factd_Cantidad'>" + $('#factd_Cantidad').val() + "</td>";
        copiar += "<td>" + '<button id="removeFacturaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        //console.log('CodTipoCasoExito', $('#CodTipoCasoExito option:selected').text());
        $('#tblDetalleFactura').append(copiar);

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
            $('#tbProducto_prod_Descripcion').val('');
            $('#MessageError').text('');
            $('#ErrorDescripcion').text('');
            $('#ErrorFecha').text('');
        });
    }
});