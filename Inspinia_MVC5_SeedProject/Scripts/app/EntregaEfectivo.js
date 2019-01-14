$(document).on("change", "#Entrega tbody tr td input#name", function () {
    var Cantidad = $(this).val();
    var ValorDenominacion = $('#deno_valor').val();
    var soled_MontoEntregado = Cantidad * ValorDenominacion;
    console.log(Cantidad)
    console.log(ValorDenominacion)


    $(this).parents("tr").find("td")[5].innerHTML = soled_MontoEntregado;
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }

});

$(document).on("keypress", "#Entrega tbody tr td input#name", function () {
    var Cantidad = $(this).val();
    var ValorDenominacion = $('#deno_valor').val();
    var soled_MontoEntregado = Cantidad * ValorDenominacion;



    console.log(Cantidad)
    console.log(ValorDenominacion)

    $(this).parents("tr").find("td")[5].innerHTML = soled_MontoEntregado;
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }





});
