var contador = 0;

$('#AgregarDetalleFactura').click(function () {
    var CodigoProducto = $('#prod_Codigo').val();
    var MontoDescuento = $('#factd_MontoDescuento').val();
    var DescripcionProducto = $('#tbProducto_prod_Descripcion').val();
    var CantidadProducto = $('#factd_Cantidad').val();
    var Subtotal = $('#SubtotalProducto').val();
    var PrecioUnitario = $('#PrecioUnitario').val();
    var Impuesto = $('#factd_Impuesto').val();
    var Total = $('#TotalProducto').val();
    
    if (CodigoProducto == '')
    {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationCodigoProductoCreate').after('<ul id="ErrorCodigoProductoCreate" class="validation-summary-errors text-danger">Campo Código Producto requerido</ul>');
    }
    else if (MontoDescuento == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationMontoDescuentoCreate').after('<ul id="ErrorMontoDescuentoCreate" class="validation-summary-errors text-danger">Campo Monto Descuento requerido</ul>');
    }
    else if (CantidadProducto == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationCantidadProductoCreate').after('<ul id="ErrorCantidadCreate" class="validation-summary-errors text-danger">Campo Cantidad requerido</ul>');
    }
    else if (Impuesto == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationImpuestoProductoCreate').after('<ul id="ErrorImpuestoCreate" class="validation-summary-errors text-danger">Campo Impuesto requerido</ul>');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'prod_CodigoCreate'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'tbProducto_prod_DescripcionCreate'>" + $('#tbProducto_prod_Descripcion').val() + "</td>";
        copiar += "<td id = 'factd_CantidadCreate'>" + $('#factd_Cantidad').val() + "</td>";
        copiar += "<td id = 'PrecioUnitarioCreate'>" + $('#PrecioUnitario').val() + "</td>";
        copiar += "<td id = 'factd_MontoDescuentoCreate'>" + $('#factd_MontoDescuento').val() + "</td>";
        copiar += "<td id = 'TotalProductoCreate'>" + $('#TotalProducto').val() + "</td>";
        copiar += "<td>" + '<button id="removeFacturaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblDetalleFactura').append(copiar);

        var FacturaDetalle = GetFacturaDetalle();
        $.ajax({
            url: "/Factura/SavePuntoEmisionDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ PuntoEmisionDet: PuntoEmisionDetalle }),
        })
        .done(function (data) {
            $('#ErrorCodigoProductoCreate').text('');
            $('#ErrorMontoDescuentoCreate').text('');
            $('#ErrorCantidadCreate').text('');
            $('#ErrorImpuestoCreate').text('');

        });
    }

});

function GetFacturaDetalle() {

    var FacturaDetalle = {
        dfisc_Id: $('#dfisc_Id').val(),
        pemid_RangoInicio: $('#pemid_RangoInicio').val(),
        pemid_RangoFinal: $('#pemid_RangoFinal').val(),
        pemid_FechaLimite: new Date($('#pemid_FechaLimite').val()),
        pemid_Id: contador
    }
    return FacturaDetalle
};


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