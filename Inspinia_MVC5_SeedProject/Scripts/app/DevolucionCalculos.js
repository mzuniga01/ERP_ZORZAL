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

$(document).ready(function () {
    var Cliente = $('#tbFactura_clte_Identificacion').val();
if (Cliente === '') {
    document.getElementById("Factura").disabled = true;
    document.getElementById("tbFactura_fact_Codigo").disabled = true;
}
});

