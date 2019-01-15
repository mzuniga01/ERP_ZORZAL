
$(function () {

    $("#pago_TotalPago").keyup(function (e) {

        var totalpagar = $("#pago_TotalPago").val(),
            pagado = $("#TotalPagado").val(),
            saldo = $("#pago_SaldoAnterior").val(),

            result = 0;

        if (isNaN(parseFloat(totalpagar) > 0)) {
            result += parseFloat(saldo) - parseFloat(totalpagar);
        }

        $("#pago_SaldoAnterior").val(result);

    });

});



$(function () {

    $("#pago_TotalPago").keyup(function (e) {
        var totalpagar = $("#pago_TotalPago").val();
        var saldo = $("#pago_SaldoAnterior").val();
        var pagado = $("#TotalPagado").val();
        var montofactura = $("#MontoFactura").val();

        var monto = (parseFloat(montofactura));

        var saldoActual = (parseFloat(saldo)- parseFloat(totalpagar));
        var pagoActual = (parseFloat(pagado) + parseFloat(totalpagar));
        result = 0.00;

        if (totalpagar ==='') {
            $("#pago_SaldoAnterior").val(saldo);
            $("#TotalPagado").val(pagado);       
        }
        else {
            $("#pago_SaldoAnterior").val(saldoActual);
            $("#TotalPagado").val(pagoActual);
        }
   
    });
});

$(function () {

    $("#efectivo").keyup(function (e) {
        var efectivo = $("#efectivo").val();
        var cambio = $("#cambio").val();
        var totalpagar = $("#pago_TotalPago").val();
        var saldo = $("#pago_SaldoAnterior").val();
        var pagado = $("#TotalPagado").val();
        var montofactura = $("#MontoFactura").val();

        var monto = (parseFloat(montofactura));

        var cambioefectivo = (parseFloat(efectivo) - parseFloat(totalpagar));
        var saldoActual = (parseFloat(saldo) - parseFloat(totalpagar))/-1;
        var pagoActual = (parseFloat(pagado) + parseFloat(totalpagar));
        result = 0.00;

        if (totalpagar === '') {
            $("#pago_SaldoAnterior").val(saldo);
            $("#TotalPagado").val(pagado);
            $("#efectivo").val(efectivo);
            $("#cambio").val(cambio);

        }
        else {
            $("#efectivo").val(efectivo);
            $("#cambio").val(cambioefectivo);

        }

    });
});


