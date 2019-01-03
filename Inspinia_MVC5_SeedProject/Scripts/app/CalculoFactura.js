$(document).ready(function () {
    $("#factd_Cantidad")[0].maxLength = 7;
    $("#factd_MontoDescuento")[0].maxLength = 12;
})

$(function () {

    $("#factd_Cantidad,#factd_PrecioUnitario").keyup(function (e) {

        var Cantidad = $("#factd_Cantidad").val(),
            Precio = $("#factd_PrecioUnitario").val(),
            result = "";

        if (Cantidad.length && Precio.length > 0) {
            result += Cantidad * Precio;
        }

        $("#SubtotalProducto").val(result);

    });

});

$(function () {

    $("#factd_PorcentajeDescuento").keyup(function (e) {
        var Descuento = $("#factd_PorcentajeDescuento").val();
        console.log('Descuento',Descuento);
        var Subtotal = $("#SubtotalProducto").val();
        console.log('Subtotal',Subtotal);
        var impuesto = $("#factd_Impuesto").val();        
        console.log('impuesto', impuesto);
        var PorcentajeDescuento = (parseFloat(Descuento) / 100);
        console.log('PorcentajeDescuento', PorcentajeDescuento);
        var PorcentajeImpuesto = (parseFloat(impuesto) / 100);
        console.log('PorcentajeImpuesto',PorcentajeImpuesto);
        var DescuentoTotal = (parseFloat(Subtotal) * parseFloat(PorcentajeDescuento));
        console.log('DescuentoTotal', DescuentoTotal);
        var impuestotal = (Subtotal * PorcentajeImpuesto);
        console.log('impuestotal', impuestotal);
        result = "";

        if (DescuentoTotal == '') {
            result += (parseFloat(Subtotal) + parseFloat(impuestotal));
        }
        else if (DescuentoTotal == 0) {
            result += (parseFloat(Subtotal) + parseFloat(impuestotal));
        }
        else {
            result += (parseFloat(Subtotal) - parseFloat(DescuentoTotal) + parseFloat(impuestotal));
        }
        $("#factd_MontoDescuento").val(DescuentoTotal);
        $("#TotalProducto").val(result);
    });
});



