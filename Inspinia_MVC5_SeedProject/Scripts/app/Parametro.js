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