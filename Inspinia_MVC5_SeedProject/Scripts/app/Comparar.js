$(function () {

    $(document).on('keyup', '#usu_Password, #confirmar-pass', function () {
        var usu_Password = $('#usu_Password').val().trim();
        var bar = $('#confirmar-pass').val().trim();
        if (!usu_Password || !bar || usu_Password == '' || bar == '') {
            $('#msg').removeClass('text-success').addClass('text-danger').text('Las contraseñas no coinciden');
        }

        else {
            if (usu_Password !== bar) {
                $('#msg').removeClass('text-success').addClass('text-danger').text('Las contraseñas no coinciden');
            }

            else {
                $('#msg').removeClass('text-danger').addClass('text-success').text('Las contraseñas si coinciden');
            }
        }
    });
});