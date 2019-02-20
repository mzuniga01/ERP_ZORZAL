$('#AutorizarDescuento').click(function () {
    var User = $('#Username').val();
    var Pass = $('#txtPassword').val();
    var Descuento = $('#PorcentajeDescuento').val();
    if (User == '') {
        $('#ErrorUsernameCreate').text('');
        $('#ErrortxtPasswordCreate').text('');
        $('#ErrorPorcentajeDescuentoCreate').text('');
        $('#validationUsernameCreate').after('<ul id="ErrorUsernameCreate" class="validation-summary-errors text-danger">Campo Código Producto requerido</ul>');
    }
    else if (Pass == '') {
        $('#ErrorUsernameCreate').text('');
        $('#ErrortxtPasswordCreate').text('');
        $('#ErrorPorcentajeDescuentoCreate').text('');
        $('#validationtxtPasswordCreate').after('<ul id="ErrortxtPasswordCreate" class="validation-summary-errors text-danger">Campo Monto Descuento requerido</ul>');
    }
    else if (Descuento == '') {
        $('#ErrorUsernameCreate').text('');
        $('#ErrortxtPasswordCreate').text('');
        $('#ErrorPorcentajeDescuentoCreate').text('');
        $('#validationPorcentajeDescuentoCreate').after('<ul id="ErrorPorcentajeDescuentoCreate" class="validation-summary-errors text-danger">Campo Monto Descuento requerido</ul>');
    }
    else {
       var GetLogin = GetData();
        $.ajax({
            url: "/Factura/SaveFacturaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ FacturaDetalleC: GetLogin }),
        })
        .done(function (data) {
            $('#ErrorUsernameCreate').text('');
            $('#ErrortxtPasswordCreate').text('');
            $('#ErrorPorcentajeDescuentoCreate').text('');
            //Input
            $('#Username').val('');
            $('#txtPassword').val('');
            $('#PorcentajeDescuento').val('');
        });
    }
});

function GetData() {

    var LoginGet = {
        Username: $('#Username').val(),
        txtPassword: $('#txtPassword').val(),
        PorcentajeDescuento: $('#PorcentajeDescuento').val(),
    }
    return LoginGet
};