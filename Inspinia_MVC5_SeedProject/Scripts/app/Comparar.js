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
            $('#msg').removeClass('text-success').addClass('text-danger').text('Longitud mínima es de 8 caracteres.');
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

$(document).on('change', '#usu_NombreUsuario', function () {
    $.ajax({
        url: "/Usuario/GetUserExist",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ user: $('#usu_NombreUsuario').val() }),
    })
    .done(function (data) {
        console.log(data.length);
        console.log(data);
        console.log('Hola');
        if (data.length < 1) {
            $('#errorUser').text('');
            $('#validationDescripcionRol').after('<ul id="errorUser" class="validation-summary-errors text-danger">Nombre de Usuario ya existe.</ul>');
            $('#usu_Nombres').focus();
        }
        else {
            $('#errorUser').text('');
        }
    })
    .fail(function (xhr, err) {
        var responseTitle = $(xhr.responseText).filter('title').get(0);
        console.log($(responseTitle).text() + "\n" + formatErrorMessage(xhr, err));
    })
});

