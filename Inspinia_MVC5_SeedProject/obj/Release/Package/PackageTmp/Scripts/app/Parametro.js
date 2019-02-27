function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgpreview').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#CargarFoto").change(function () {
    readURL(this);
});


//Solo numeros
$('#par_TelefonoEmpresa').keypress(function (e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9\-]+$/.test(tecla);

});


$('#par_SucursalPrincipal').keypress(function (e) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if ((keynum == 48) || (keynum == 57))
        return true;
    return /\d/.test(String.fromCharCode(keynum));

});

$('#par_PorcentajeDescuentoTE').keypress(function (e) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if ((keynum == 48) || (keynum == 57))
        return true;
    return /\d/.test(String.fromCharCode(keynum));

});

