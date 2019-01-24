$(document).ready(function () {
    $('#Cambiarpass').prop('disabled', true);


    $(function () {

        $(document).on('keyup', '#usu_Password, #confirmar-pass', function () {
            var usu_Password = $('#usu_Password').val().trim();
            var bar = $('#confirmar-pass').val().trim();
            if (!usu_Password || !bar || usu_Password == '' || bar == '') {
                $('#msg').removeClass('text-success').addClass('text-danger').text('Las contraseñas no coinciden');
                $('#Cambiarpass').prop('disabled', true);
            }

            else {
                if (bar !== usu_Password) {
                    bar
                    $('#msg').removeClass('text-success').addClass('text-danger').text('Las contraseñas no coinciden');
                    $('#Cambiarpass').prop('disabled', true);
                }
                else
                {
                    $('#Cambiarpass').prop('disabled', false);
                }


            }
        });
    });
});