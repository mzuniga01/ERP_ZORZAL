//$(document).ready(function () {
//    if (this.checked) {
//        $('#RazonInactivo').hide();
//    }
//    else {
//        $('#RazonInactivo').show();
//    }

//});



//(document).ready(function () {

//    $('#prod_EsActivo').on('click', function () {
//        var c = document.getElementById('Activo').checked;
//        if (c) {

//            $("#RazonInactivo").hide();

//        }

//        else {
//            $("#email").show();

//        }


//    });

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


$("#prod_EsActivo").change(function () {
    if (this.checked) {
        $('#RazonInactivo').hide();
    }
    else {
        $('#RazonInactivo').show();
    }
});
        
