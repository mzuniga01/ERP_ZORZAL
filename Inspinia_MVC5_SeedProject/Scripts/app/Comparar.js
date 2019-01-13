$(document).ready(function () {
    $('#AgregarRol').prop('disabled', true);
    $('#QuitarRol').prop('disabled', true);
    $('#btnGuardarUsuario').prop('disabled', true);
});

$(document).on('keyup', '#usu_Password, #confirmar-pass', function () {
    var usu_Password = $('#usu_Password').val().trim();
    var bar = $('#confirmar-pass').val().trim();
    if (usu_Password != '' && bar != '')
    {
        if (usu_Password.length < 8)
        {
            $('#msg').removeClass('text-success').addClass('text-danger').text('Longitud debe ser de 8 caracteres.');
            $('#AgregarRol').prop('disabled', false);
            $('#QuitarRol').prop('disabled', false);
            $('#btnGuardarUsuario').prop('disabled', false);
        }
        else
        {
            if (!usu_Password || !bar || usu_Password == '' || bar == '') {
                $('#msg').removeClass('text-success').addClass('text-danger').text('Las contraseñas no coinciden');
            }
            else {
                if (usu_Password !== bar) {
                    $('#AgregarRol').prop('disabled', true);
                    $('#QuitarRol').prop('disabled', true);
                    $('#btnGuardarUsuario').prop('disabled', true);
                    $('#msg').removeClass('text-success').addClass('text-danger').text('Las contraseñas no coinciden');
                }

                else {
                    $('#AgregarRol').prop('disabled', false);
                    $('#QuitarRol').prop('disabled', false);
                    $('#btnGuardarUsuario').prop('disabled', false);
                    $('#msg').text('');
                }
            }
        }
    }
});

$(document).on('blur', '#usu_NombreUsuario', function () {
    $.ajax({
        url: "/Usuario/GetUserExist",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ user: $('#usu_NombreUsuario').val() }),
    })
    .done(function (data) {
        if (data.length >= 1) {
            $('#msgUsuario').text('');
            $('#msgUsuario').removeClass('text-success').addClass('text-danger').text('Usuario ya existe');
            $('#usu_NombreUsuario').focus();
        }
        else {
            $('#msgUsuario').text('');
        }
    })
});

$(document).on('blur', '#usu_Password', function () {
    var usu_Password = $('#usu_Password').val().trim();
    if (usu_Password.length < 8) {
        $('#msg').removeClass('text-success').addClass('text-danger').text('Longitud debe ser de 8 caracteres.');
        $('#usu_Password').focus();
    }
});




