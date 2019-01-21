$(document).ready(function () {
    $("#factd_Cantidad")[0].maxLength = 7;
    $("#factd_MontoDescuento")[0].maxLength = 12;
})

$(function () {

    $("#factd_Cantidad").keyup(function (e) {

        var Cantidad = $("#factd_Cantidad").val(),
            Precio = $("#factd_PrecioUnitario").val(),
            Impuesto = $("#factd_Impuesto").val(),
            Subtotal = $("#SubtotalProducto").val(),
            result = "";
            result1 = "";

        if (Cantidad && Precio > 0) {
            result += Cantidad * Precio ;
        }

        $("#SubtotalProducto").val(result);
        PorcentajeImpuesto = ((parseFloat(Impuesto) / 100) * Subtotal);

         result1 += PorcentajeImpuesto;

         $("#Impuesto").val(result1);
         var Descuento = $("#factd_PorcentajeDescuento").val();
         var Subtotal = $("#SubtotalProducto").val();
         var impuesto = $("#factd_Impuesto").val();
         var PorcentajeDescuento = (parseFloat(Descuento) / 100);
         var PorcentajeImpuesto = (parseFloat(impuesto) / 100);
         var DescuentoTotal = (parseFloat(Subtotal) * parseFloat(PorcentajeDescuento));
         var impuestotal = (Subtotal * PorcentajeImpuesto);
         result = "";

         if (PorcentajeDescuento && Cantidad == '') {
             result += (parseFloat(Subtotal) + parseFloat(impuestotal));
         }
         else if (DescuentoTotal && Cantidad == 0) {
             result += (parseFloat(Subtotal) + parseFloat(impuestotal));
         }
         else {
             result += (parseFloat(Subtotal) - parseFloat(DescuentoTotal) + parseFloat(impuestotal));
         }
         if (Descuento == '') {
             $("#factd_PorcentajeDescuento").val(0);
         }
         if (Cantidad == '') {
             $("#factd_MontoDescuento").val('');
         }
         else {
             $("#factd_MontoDescuento").val(DescuentoTotal);
         }
         if (Descuento && Cantidad == '') {
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




