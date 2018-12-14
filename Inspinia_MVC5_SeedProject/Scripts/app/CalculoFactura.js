$(function () {

    $("#factd_Cantidad,#PrecioUnitario").keyup(function (e) {

        var val1 = $("#factd_Cantidad").val(),
            val2 = $("#PrecioUnitario").val(),
            result = "";

        if (val1.length > 0) {
        }
        if (val2.length > 0) {
            result += val1 * val2;;
        }
        $("#Subtotal").val(result);

    });

});

