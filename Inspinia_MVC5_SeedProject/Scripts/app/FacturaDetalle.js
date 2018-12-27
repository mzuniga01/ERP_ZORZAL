var contador = 0;

$('#AgregarDetalleFactura').click(function () {
    var CodigoProducto = $('#prod_Codigo').val();
    console.log(CodigoProducto);
    var PorcentajeDescuento = $('#factd_PorcentajeDescuento').val();
    console.log(PorcentajeDescuento);
    var MontoDescuento = $('#factd_MontoDescuento').val();
    console.log(MontoDescuento);
    var DescripcionProducto = $('#tbProducto_prod_Descripcion').val();
    console.log(DescripcionProducto);
    var CantidadProducto = $('#factd_Cantidad').val();
    console.log(CantidadProducto);
    var Subtotal = $('#SubtotalProducto').val();
    console.log(Subtotal);
    var PrecioUnitario = $('#factd_PrecioUnitario').val();
    console.log(PrecioUnitario);
    var Impuesto = $('#factd_Impuesto').val();
    console.log(Impuesto);
    var Total = $('#TotalProducto').val();
    console.log(Total);
    
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
        copiar += "<td id = 'Precio_UnitarioCreate'>" + $('#factd_PrecioUnitario').val() + "</td>";
        copiar += "<td id = 'factd_MontoDescuentoCreate'>" + $('#factd_MontoDescuento').val() + "</td>";
        copiar += "<td id = 'TotalProductoCreate'>" + $('#TotalProducto').val() + "</td>";
        copiar += "<td>" + '<button id="removeFacturaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblDetalleFactura').append(copiar);

        var FacturaDetalle = GetFacturaDetalle();
        $.ajax({
            url: "/Factura/SaveFacturaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ FacturaDetalleC: FacturaDetalle }),
        })
        .done(function (data) {
            $('#ErrorCodigoProductoCreate').text('');
            $('#ErrorMontoDescuentoCreate').text('');
            $('#ErrorCantidadCreate').text('');
            $('#ErrorImpuestoCreate').text('');
            //Input
            $('#prod_Codigo').val('');
            $('#factd_PorcentajeDescuento').val('');
            $('#factd_MontoDescuento').val('');
            $('#tbProducto_prod_Descripcion').val('');
            $('#factd_Cantidad').val('');
            $('#SubtotalProducto').val('');
            $('#factd_PrecioUnitario').val('');
            $('#factd_Impuesto').val('');
            $('#TotalProducto').val('');
        });
    }
});

function GetFacturaDetalle() {

    var FacturaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        factd_PorcentajeDescuento: $('#factd_PorcentajeDescuento').val(),
        factd_MontoDescuento: $('#factd_MontoDescuento').val(),
        tbProducto_prod_Descripcion: $('#tbProducto_prod_Descripcion').val(),
        factd_Cantidad: $('#factd_Cantidad').val(),
        SubtotalProducto: $('#SubtotalProducto').val(),
        factd_PrecioUnitario: $('#factd_PrecioUnitario').val(),
        factd_Impuesto: $('#factd_Impuesto').val(),
        TotalProducto: $('#TotalProducto').val(),
        factd_Id: contador
    }
    return FacturaDetalle
};

$(document).on("click", "#tblDetalleFactura tbody tr td button#removeFacturaDetalle", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var FacturaDetalle = {
        factd_Id: idItem,
    };
    $.ajax({
        url: "/Factura/RemoveFacturaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ FacturaDetalleC: FacturaDetalle }),
    });
});