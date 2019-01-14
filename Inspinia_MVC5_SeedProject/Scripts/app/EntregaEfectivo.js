$(document).on("change", "#Entrega tbody tr td input#CantidadE", function () {
    var Cantidad = $(this).val();
    var ValorDenominacion = $(this).parents("tr").find("td")[3].innerHTML;
    var MontoEntregado = Cantidad * ValorDenominacion;

    //Monto Entregado
    $(this).parents("tr").find("td")[6].innerHTML = MontoEntregado;
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

$(document).on("keypress", "#Entrega tbody tr td input#CantidadE", function () {
    var Cantidad = $(this).val();
    var ValorDenominacion = $(this).parents("tr").find("td")[3].innerHTML;
    var MontoEntregado = Cantidad * ValorDenominacion;

    //Monto Entregado
    $(this).parents("tr").find("td")[6].innerHTML = MontoEntregado;
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

