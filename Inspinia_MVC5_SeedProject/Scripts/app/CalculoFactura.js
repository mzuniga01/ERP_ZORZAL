$(document).ready(function () {

    $("#factd_Cantidad")[0].maxLength = 7;
    $("#factd_MontoDescuento")[0].maxLength = 12;
})

$(function () {

    $("#factd_Cantidad,#factd_PrecioUnitario").keyup(function (e) {

        var val1 = $("#factd_Cantidad").val(),
            val2 = $("#factd_PrecioUnitario").val(),
            result = "";

        if (val1.length > 0) {
        }
        if (val2.length > 0) {
            result += val1 * val2;
        }
        $("#SubtotalProducto").val(result);

    });

});

$(function () {

    $("#factd_MontoDescuento").keyup(function (e) {
        var Descuento = document.getElementById("factd_MontoDescuento").value;
        var Subtotal = document.getElementById("SubtotalProducto").value;
        var Porcentaje = (parseFloat(Descuento) / 100);
        var VarDescuento = (parseFloat(Subtotal) * parseFloat(Porcentaje));
        result = "";

        result += (Subtotal - VarDescuento);

        console.log("Hola")
       $("#TotalProducto").val(result);

    });

});

