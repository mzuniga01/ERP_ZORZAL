$(document).ready(function () {

    $('#usu_RazonInactivo').hide();

});



$("#usu_EsActivo").change(function () {
    if (this.checked) {
        $('#usu_RazonInactivo').hide();
    }
    else {
        $('#usu_RazonInactivo').show();
    }
});