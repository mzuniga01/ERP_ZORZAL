$(function () {
    console.log('devd_CantidadProducto');
    $("#devd_CantidadProducto,#PrecioUnitario").keyup(function (e) {

        var Cantidad = $("#devd_CantidadProducto").val(),
            Precio = $("#PrecioUnitario").val(),
            result = "";

        if (Cantidad.length && Precio.length > 0) {
            result += Cantidad * Precio;
        }

        $("#devd_Monto").val(result);

    });

});