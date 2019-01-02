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
        var Descuento = document.getElementById("factd_PorcentajeDescuento").value;
        var Subtotal = document.getElementById("SubtotalProducto").value;
        var impuesto = document.getElementById("factd_Impuesto").value;
        var Porcentaje = (parseFloat(Descuento) / 100);
        var porcentaje = (impuesto / 100);
        var VarDescuento = (parseFloat(Subtotal) * parseFloat(Porcentaje));
        var impuesto = (Subtotal * porcentaje);
        result = "";
        if (Descuento.length == 0) {
            result += (Subtotal + impuesto);
        } else {
            result += (Subtotal - VarDescuento + impuesto);
        }
        $("#factd_MontoDescuento").val(VarDescuento);
        $("#TotalProducto").val(result);
    });
});



