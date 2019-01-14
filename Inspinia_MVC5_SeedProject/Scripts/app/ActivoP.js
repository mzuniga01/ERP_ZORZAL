
//$(document).ready(function () {
//    var checkbox = document.getElementById('checkbox')
//    if (checkbox.checked) {
//        $('#RazonInactivo').hide();
//    }
//    else {
//        $('#RazonInactivo').show();
//    }

//});

//$(document).change(function () {
//    $('#prod_EsActivo').on('change', function () {
//        if (this.checked) {
//            $("#RazonInactivo").show();
//        } else {
//            $("#RazonInactivo").hide();
//        }
//    })
//});
$(document).ready(function () {

    $('#RazonInactivo').hide();
    
    
});

$("#prod_EsActivo").change(function () {
    if (this.checked) {
        $('#RazonInactivo').hide();
        $(prod_Razon_Inactivacion).val('');
    }
    else {
        $('#RazonInactivo').show();
    }
});
        
