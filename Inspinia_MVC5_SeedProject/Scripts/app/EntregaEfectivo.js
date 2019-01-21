var total = 0;
var MontoEntregado = 0;
var Monto = 0;
$(document).on("change", "#Entrega tbody tr td input#CantidadE", function () {
    var row = $(this).closest("tr");
    var Cantidad = $(this).val();
    var ValorDenominacion = $(this).parents("tr").find("td")[3].innerHTML;
    var Subtotal = parseFloat(Cantidad * ValorDenominacion);

    if (Subtotal != 0) {
        MontoEntregado += Subtotal;
    }
    else {
        MontoEntregado = 0;
        $("#Entrega tbody tr").each(function (index) {
            Monto = $(this).children("td:eq(3)").html();
            console.log('Monto', Monto);
            if (Monto != '') {
                Monto = parseFloat(Monto);
                MontoEntregado += Monto;
            }
        })
    }

    //Monto Entregado
    $(this).parents("tr").find("td")[6].innerHTML = Subtotal;
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }

    var total = document.getElementById("total").innerHTML = parseFloat(MontoEntregado);
    $("#detalle_soled_MontoEntregado").val(total);
    console.log(total);
  
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

