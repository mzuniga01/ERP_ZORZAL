$(function () {

    $("#factd_Cantidad,#PrecioUnitario").keyup(function (e) {

        var val1 = $("#factd_Cantidad").val(),
            val2 = $("#PrecioUnitario").val(),
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

        var val1 = $("#factd_MontoDescuento").val(),
            val2 = $("#SubtotalProducto").val(),
            descuento = parseInt(val1 / 100);
        result = "";
        if (val1.length > 0) {
        }
        if (val2.length > 0) {
            result += (val2 * descuento);
        }
        $("#TotalProducto").val(result);

    });

});

