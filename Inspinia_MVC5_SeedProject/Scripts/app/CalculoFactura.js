$(document).ready(function () {
    $("#factd_Cantidad")[0].maxLength = 7;
    $("#factd_MontoDescuento")[0].maxLength = 12;
})

$(function () {

    $("#factd_Cantidad,#factd_PrecioUnitario").keyup(function (e) {

        var Cantidad = $("#factd_Cantidad").val(),
            Precio = $("#factd_PrecioUnitario").val(),
            result = "";

        if (Cantidad && Precio > 0) {
            result += Cantidad * Precio;
        }

        $("#SubtotalProducto").val(result);

    });

});

$(function () {

    $("#factd_PorcentajeDescuento").keyup(function (e) {
        var Descuento = $("#factd_PorcentajeDescuento").val();
        var Subtotal = $("#SubtotalProducto").val();
        var impuesto = $("#factd_Impuesto").val();        
        var PorcentajeDescuento = (parseFloat(Descuento) / 100);
        var PorcentajeImpuesto = (parseFloat(impuesto) / 100);
        var DescuentoTotal = (parseFloat(Subtotal) * parseFloat(PorcentajeDescuento));
        var impuestotal = (Subtotal * PorcentajeImpuesto);
        result = "";

        if (PorcentajeDescuento == '') {
            result += (parseFloat(Subtotal) + parseFloat(impuestotal));
        }
        else if (DescuentoTotal == 0) {
            result += (parseFloat(Subtotal) + parseFloat(impuestotal));
        }
        else {
            result += (parseFloat(Subtotal) - parseFloat(DescuentoTotal) + parseFloat(impuestotal));
        }
        if (Descuento == '')
        {
            $("#factd_MontoDescuento").val('');
        }
        else {
            $("#factd_MontoDescuento").val(DescuentoTotal);
        }
        if (Descuento == '')
        {
            $("#TotalProducto").val('');
        }
        else {
            $("#TotalProducto").val(result);
        }

        if ($("#factd_Cantidad").val(), $("#factd_PrecioUnitario").val() == '') {
            $("#factd_MontoDescuento").val('');
            $("#TotalProducto").val('');
        }        
    });
});



