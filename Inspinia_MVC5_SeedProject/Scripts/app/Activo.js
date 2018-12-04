


$(document).ready(function () {

    $('#clte_RazonInactivo').hide();
    console.log("Hola");
});


        

$(function () {
    $('#clte_EsActivo').change(function () {
        console.log("Adios");
        if ($(this).val() == 1) {
            $('#clte_RazonInactivo').hide();
        } else {
            $('#clte_RazonInactivo').show();
        }
    });
});

