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


//$("#clte_EsPersonaNatural").ready(function () {
//    if (this.checked) {
//        //Do stuff
//        console.log("Hola");
//        $('#natural').show();
//        $('#juridica').hide();
//    }
//    else {
//        $('#natural').hide();
//        $('#juridica').show();
//    }
//});

$(document).ready(function () {
    if ("#clte_EsPersonaNatural".checked) {
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


//$("#clte_EsPersonaNatural").change(function () {
//    if (this.checked) {
//        //Do stuff
//        console.log("Hola");
//        $('#natural1').show();
//        $('#juridica1').hide();
//    }
//    else {
//        $('#natural1').hide();
//        $('#juridica1').show();
//    }
//});

//$(document).ready(function () {
//    if (this.checked) {
//        //Do stuff
//        console.log("Hola");
//        $('#natural1').show();
//        $('#juridica1').hide();
//    }
//    else {
//        $('#natural1').hide();
//        $('#juridica1').show();
//    }
//});


$(document).ready(function () {
    $('#clte_EsPersonaNatural').click(function () {
        if ($(this).is(':checked')) {
            $('#natural1').show();
            $('#juridica1').hide();
        } else {
            $('#juridica1').show();
            $('#natural1').hide();
        }
    });
});