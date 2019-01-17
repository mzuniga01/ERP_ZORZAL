$(function () {
    console.log('devd_CantidadProducto');
    $("#devd_CantidadProducto,#PrecioUnitario").keyup(function (e) {

        var Cantidad = $("#devd_CantidadProducto").val(),
            Precio = $("#PrecioUnitario").val(),
            porcentajeDesc = $("#Descuento").val(),
            Subtotal = $("#Subtotal").val(),
            Impuesto = $("#Impuesto").val(),

            valorSubtotal = "";
            MontoTotal = "";
            ValorImpuesto = "";
            ValorDescuento = "";
            Impuesto = (parseFloat(Impuesto)/ 100)
            porcentajeDesc1 = (parseFloat(porcentajeDesc) / 100)
            if (Cantidad.length && Precio.length > 0) {
            valorSubtotal += (parseFloat(Cantidad) * parseFloat(Precio));
            ValorImpuesto += (valorSubtotal * parseFloat(Impuesto));
            ValorDescuento += (parseFloat(valorSubtotal) * parseFloat(porcentajeDesc1));
            console.log(ValorDescuento)
            MontoTotal += (parseFloat(valorSubtotal) + parseFloat(ValorImpuesto)) - parseFloat(ValorDescuento);

        }
        $("#Subtotal").val(valorSubtotal);
        $("#devd_Monto").val(MontoTotal);
        $("#Impuesto").val(ValorImpuesto);
        $("#MontoDescuento").val(ValorDescuento);
    });

});

$(document).ready(function () {
    var Cliente = $('#tbFactura_clte_Identificacion').val();
if (Cliente === '') {
    document.getElementById("Factura").disabled = true;
    document.getElementById("tbFactura_fact_Codigo").disabled = true;
}
});

