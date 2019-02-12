$(document).ready(function () {

    $('#RazonInactivo').hide();

});



$("#clte_EsActivo").change(function () {
    if (this.checked) {
        $('#RazonInactivo').hide();
    }
    else {
        $('#RazonInactivo').show();
    }
});
        
