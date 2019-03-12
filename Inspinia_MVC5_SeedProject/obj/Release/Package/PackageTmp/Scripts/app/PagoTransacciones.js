
$(document).ready(function () {
    $("#pago_TotalPago")[0].maxLength = 12;

})

$(function () {

    $("#pago_TotalPago").change(function (e) {
        valido = document.getElementById('msgMontoPagar');
        var totalpagar = $("#pago_TotalPago").val();
        var saldoAnterior = $("#SaldoAnterior").val();
        var saldoActual = $("#pago_SaldoAnterior").val();
        var pagado = $("#TotalPagado").val();
        var montofactura = $("#MontoFactura").val();


        var monto = (parseFloat(montofactura));

        result = 0.00;

        if (totalpagar == '') {

            $("#pago_SaldoAnterior").val(saldoAnterior);
            if (isNaN($("#TotalPagado").val(pagado))) {
                $("#TotalPagado").val(0.00);
            }
            else if ($("#TotalPagado").val(pagado) > 0) {
                $("#TotalPagado").val(parseFloat(pagado) - parseFloat(pagoActual));
            }
            $('#pago_TotalPago').val('').focus();
            valido.innerText = "El moto pago no debe ir vacio";
        }
        else if (parseFloat(totalpagar) > parseFloat(saldoAnterior)) {



            $("#pago_SaldoAnterior").val(saldoActual);
            $("#SaldoAnterior").val(saldoAnterior);
            if (isNaN($("#TotalPagado").val(pagado))) {
                $("#TotalPagado").val(0.00);
            }
            else if ($("#TotalPagado").val(pagado) > 0) {

                $("#TotalPagado").val(pagado - totalpagar);
            }
            $('#pago_TotalPago').val('').focus();
            valido.innerText = "El monto pago no debe superar el saldo o el monto de la factura";

        }
             

        else {
            var saldoActualizado = (parseFloat(saldoAnterior) - parseFloat(totalpagar));
            var pagoActual = (parseFloat(pagado) + parseFloat(totalpagar));
            $("#pago_SaldoAnterior").val(saldoActualizado);
            $("#TotalPagado").val(pagoActual);
            valido.innerText = "";
        }




    });
});

$(function () {

    $("#efectivo").change(function (e) {
        valido = document.getElementById('smspsfectivo');
        var efectivo = $("#efectivo").val();
        var cambio = $("#cambio").val();
        var totalpagar = $("#pago_TotalPago").val();


        result = 0;

        if (totalpagar == '' || parseFloat(totalpagar) == 0) {
            $('#efectivo').val('').focus();
            valido.innerText = "El monto efectivo no debe ir vacio";


        }
        else if (parseFloat(efectivo) >= parseFloat(totalpagar)) {
            var cambioefectivo = (parseFloat(efectivo) - parseFloat(totalpagar));
            $("#efectivo").val(efectivo);
            $("#cambio").val(cambioefectivo);
            valido.innerText = "";
        }
        else {
            $("#cambio").val(result);
            $('#efectivo').val('').focus();
            valido.innerText = "El moto efectivo no debe ser mayor o igual al moto pago";
        }

    });
});

// validaciones con cupon descuento

$(function () {

    $("#MontoDesc").change(function (e) {
        valido = document.getElementById('smsFechaVencimiento');
        var MontoDescuento = $("#MontoDesc").val();
        var PorcentDescuento = $("#descuento").val();
        var FechaVencimiento = $("#pago_FechaVencimiento").val();
        var codigo = $("#nocre_Codigo_cdto_Id").val();
        var hoy = new Date();
        console.log(hoy);
        var MontoMaximo = $("#montomax").val();
        var CantidadMinimma = $("#cantmin").val();
        var Redimido = $("#redimido").val();
        var totalpagar = $("#pago_TotalPago").val();
        var montofactura = $("#MontoFactura").val();


        result = 0;

        if (FechaVencimiento <= hoy) {
            if (MontoDesc > 0) {
                $("#pago_TotalPago").val(parseFloat(MontoDescuento));
                valido.innerText = "";
            }
            else {
                var CalculoMontoConPorcent = (parseFloat(montofactura) * (parseFloat(PorcentDescuento) / 100));
                //var saldo = (parseFloat(montofactura) - parseFloat(CalculoMontoConPorcent));
                if (CalculoMontoConPorcent <= parseFloat(MontoMaximo)) {
                    $("#pago_TotalPago").val(parseFloat(CalculoMontoConPorcent));
                    valido.innerText = "";
                }
                else {
                    $("#MontoDesc").val('');
                    $("#descuento").val('');
                    $("#pago_FechaVencimiento").val('');
                    $("#nocre_Codigo_cdto_Id").val('').focus();
                    valido.innerText = "Monto de descuento excede el monto establecido,  con otro cupón.";
                }

            }
        }
        else {
            $("#MontoDesc").val('');
            $("#descuento").val('');
            $("#pago_FechaVencimiento").val('');
            $("#nocre_Codigo_cdto_Id").val('').focus();
            valido.innerText = "Cupón vencido, intente con otro.";
        }







    });
});
