$(document).ready(function () {

    $('#RazonInactivo').hide();

});



$("#exo_ExoneracionActiva").change(function () {
    if (this.checked) {
        $('#RazonInactivo').hide();
    }
    else {
        $('#RazonInactivo').show();
    }
});
        
