
$(document).ready(function () {
    $("#pago_TotalPago")[0].maxLength = 12;

})

$(function () {

    $("#pago_TotalPago").change(function (e) {
        var totalpagar = $("#pago_TotalPago").val();
        var saldoAnterior = $("#SaldoAnterior").val();
        var saldoActual = $("#pago_SaldoAnterior").val();
        var pagado = $("#TotalPagado").val();
        var montofactura = $("#MontoFactura").val();

       
        var monto = (parseFloat(montofactura));

        result = 0.00;

        if (totalpagar == '') {
            alert('Monto Incorrecto2');
            $("#pago_SaldoAnterior").val(saldoAnterior);
            if (isNaN($("#TotalPagado").val(pagado))) {
                $("#TotalPagado").val(0.00);
            }
            else if ($("#TotalPagado").val(pagado) > 0) {
                $("#TotalPagado").val(parseFloat(pagado) - parseFloat(pagoActual));
            }
        }
       else if (totalpagar < 0) {
           alert('Monto menor a cero');
           $("#pago_TotalPago").val('');
            $("#pago_SaldoAnterior").val(saldoAnterior);
            if (isNaN($("#TotalPagado").val(pagado))) {
                $("#TotalPagado").val(0.00);
            }
            else if ($("#TotalPagado").val(pagado) > 0) {
                $("#TotalPagado").val(parseFloat(pagado) - parseFloat(pagoActual));
            }
        }
      

        else if ( totalpagar > saldoAnterior && pagado > montofactura) {
           
           
            alert('Monto Incorrecto');
            $("#pago_SaldoAnterior").val(saldoAnterior);
            $("#TotalPagado").val(pagado - pagoActual);

        }
   

       

        else {
            var saldoActualizado = (parseFloat(saldoAnterior) - parseFloat(totalpagar));
            var pagoActual = (parseFloat(pagado) + parseFloat(totalpagar));
            $("#pago_SaldoAnterior").val(saldoActualizado);
            $("#TotalPagado").val(pagoActual);     
        }
       
     
    
   
    });
});

$(function () {

    $("#efectivo").change(function (e) {
        var efectivo = $("#efectivo").val();
        var cambio = $("#cambio").val();
        var totalpagar = $("#pago_TotalPago").val();

        var cambioefectivo = (parseFloat(efectivo) - parseFloat(totalpagar));
        result = 0;

        if (totalpagar == '' || totalpagar == 0) {

            $("#efectivo").val(result);
            $("#cambio").val(result);

        }
        else {
            $("#efectivo").val(efectivo);
            $("#cambio").val(cambioefectivo);

        }

    });
});


