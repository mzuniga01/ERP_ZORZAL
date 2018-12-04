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




