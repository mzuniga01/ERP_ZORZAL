$(function () {
    console.log('devd_CantidadProducto');
    $("#devd_CantidadProducto,#PrecioUnitario").keyup(function (e) {

        var Cantidad = $("#devd_CantidadProducto").val(),
            Precio = $("#PrecioUnitario").val(),
            MontoDescuento = $("#test_factd_MontoDescuento").val(),
            Subtotal = $("#Subtotal").val(),
            valorSubtotal = "";
        MontoTotal = "";

        if (Cantidad.length && Precio.length > 0) {
            valorSubtotal += Cantidad * Precio;
            MontoTotal += parseFloat(valorSubtotal) - parseFloat(MontoDescuento)  ;

        }
        $("#Subtotal").val(valorSubtotal);
        $("#devd_Monto").val(MontoTotal);
    });

});

$(document).ready(function () {
    var Cliente = $('#tbFactura_clte_Identificacion').val();
if (Cliente === '') {
    document.getElementById("Factura").disabled = true;
    document.getElementById("tbFactura_fact_Codigo").disabled = true;
}
});

