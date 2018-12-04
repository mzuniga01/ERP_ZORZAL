$(document).ready(function () {

    $('#RazonInactivo').hide();
    console.log("Hola");
});

$("#clte_EsActivo").change(function () {
    if (this.checked) {
        $('#RazonInactivo').show();
    }
    else {
        $('#RazonInactivo').hide();
    }
});
        
