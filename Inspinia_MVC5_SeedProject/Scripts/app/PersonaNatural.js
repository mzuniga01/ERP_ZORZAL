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


$("#clte_EsPersonaNatural").ready(function () {
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
