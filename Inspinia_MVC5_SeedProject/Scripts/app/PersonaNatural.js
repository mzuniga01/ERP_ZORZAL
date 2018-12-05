$("#clte_EsPersonaNatural").change(function () {
    if (this.checked) {
        //Do stuff
        console.log("Hola");
        $('#natural').show();
        $('#juridica').hide();
    }
    else {
        $('#natural').hide();
        $('#juridica').show();
    }
});

$(document).ready(function () {
    if (clte_EsPersonaNatural.checked) {
        $('#natural').show();
        $('#juridica').hide();
    } else {
        $('#juridica').show();
        $('#natural').hide();
    }
});


$("#tpi_Id").change(function () {
    var d = $("#tpi_Id").val();

    if (d == 1) {
        document.getElementById("clte_RTN_Identidad_Pasaporte").maxLength = "13";
    }
    else if (d == 2) {
        document.getElementById("clte_RTN_Identidad_Pasaporte").maxLength = "14";
    }
    else if (d == 3) {
        document.getElementById("clte_RTN_Identidad_Pasaporte").maxLength = "25";
    }
});


