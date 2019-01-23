var MontoEntregado = 0;
var Monto = 0;
$(document).on("change", "#EntregaEfectivo tbody tr td input#CantidadE", function () {
    var row = $(this).closest("tr");
    var Cantidad = $(this).val();
    var ValorDenominacion = $(this).parents("tr").find("td")[3].innerHTML;
    var Subtotal = parseFloat(Cantidad * ValorDenominacion);
    //Monto Entregado
    $(this).parents("tr").find("td")[6].innerHTML = Subtotal;
    if (Subtotal != 0) {
        MontoEntregado += parseFloat(Subtotal);
    }
    else {
         MontoEntregado = 0;
         $("#EntregaEfectivo tbody tr").each(function (index) {
             Monto = $(this).children("td:eq(6)").html();
             console.log('Monto', Monto);
             if (Monto != '') {
                 Monto = parseFloat(Monto);
                 MontoEntregado += Monto;
             }
         });
    }
    var TotalFinal = document.getElementById("Total").innerHTML = parseFloat(MontoEntregado);
    $("#detalle_soled_MontoEntregado").val(MontoEntregado)
    //document.getElementById("soled_MontoEntregado").innerHTML = parseFloat(MontoEntregado);

    //document.getElementById("Monto").innerHTML = parseFloat(MontoEntregado);
    //$("#detalle_soled_MontoEntregado").val(Total);
    //document.getElementById("detalle_soled_MontoEntregado").innerHTML = Total
});

//$(document).on("keypress", "#Entrega tbody tr td input#CantidadE", function () {
//    var Cantidad = $(this).val();
//    var ValorDenominacion = $(this).parents("tr").find("td")[3].innerHTML;
//    var MontoEntregado = parseFloat(Cantidad * ValorDenominacion).toFixed(2).replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");

//    //Monto Entregado

//    $(this).parents("tr").find("td")[6].innerHTML = MontoEntregado;
//    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
//    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
//        event.preventDefault();
//    }
//    document.getElementById("#soled_MontoEntregado").innerHTML = parseFloat(MontoEntregado);
//});

