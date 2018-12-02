(document).ready(function () {

    $('#subscribe').on('click', function () {
        var c = document.getElementById('subscribe').checked;
        if (c) {

            $("#apellido").hide();

        }

        else {
            $("#apellido").show();

        }


    });

});



//$(document).ready(function () {
//    $('#suscribe').on('change', function () {
//        if (this.checked) {
//            $("#nombrecomercial").hide();
//            $("#razonsocial").hide();
//            $("#contactonombre").hide();
//            $("#contactoemail").hide();
//            $("#contactotel").hide();
//            $("#fechaconstitucion").hide();
//        }
//        else {
//            $("#nombre").show();
//            $("#apellido").show();
//            $("#fechanacimiento").show();
//            $("#nacionalidad").show();
//            $("#sexo").show();
//            $("#telefono").show();
//            $("#municipio").show();
//            $("#direccion").show();
//            $("#correoelectronico").show();
//            $("#activo").show();
//            $("#clientecredito").show();
//            $("#minorista").show();
//            $("#observaciones").show();

//        }
//    })
//});