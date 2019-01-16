$(function () {
    console.log('devd_CantidadProducto');
    $("#devd_CantidadProducto,#PrecioUnitario").keyup(function (e) {

        var Cantidad = $("#devd_CantidadProducto").val(),
            Precio = $("#PrecioUnitario").val(),
            MontoDescuento = $("#MontoDescuento").val(),
            Subtotal = $("#Subtotal").val(),
            Impuesto = $("#Impuesto").val(),
            valorSubtotal = "";
            MontoTotal = "";
            Impuesto = parseFloat(Impuesto / 100)
          
        if (Cantidad.length && Precio.length > 0) {
            valorSubtotal += Cantidad * Precio;
            ValorImpuesto += parseFloat(valorSubtotal * Impuesto)
            MontoTotal += (parseFloat(valorSubtotal) + parseFloat(ValorImpuesto)) - parseFloat(MontoDescuento);

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

