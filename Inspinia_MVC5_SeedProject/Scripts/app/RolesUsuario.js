$(document).ready(function () {
    $('#AgregarRol').prop('disabled', true);
    $('#QuitarRol').prop('disabled', true);
    $('#btnGuardarUsuario').prop('disabled', true);

    $.ajax({
        url: "/Usuario/GetRoles",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(),
    })
    .done(function (data) {
        if (data.length < 1) {
        }
        else {
            $.each(data, function (i, item) {
                newTr = '';
                newTr += '<tr data-id="' + item.rol_Id + '">'
                newTr += '<td id="objpantalla' + item.rol_Id + '">' + item.rol_Descripcion + '</td>'
                newTr += '<td><input name="id02" style="background-color:#1ab394" type="checkbox" id="check' + item.rol_Id + '" /></td>'
                newTr += '</tr>'
                $('#NoAsignados tbody').append(newTr)
            })
            $('#NoAsignados').DataTable({

                "searching": false,
                "scrollY": "300px",
                "scrollCollapse": true,
                "paging": false,
                "info": false,
                "oLanguage": {
                    "oPaginate": {
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior",
                    },
                    "sSearch": "Buscar",
                    "sLengthMenu": "Mostrar _MENU_ Registros Por Página",
                    "sInfo": "Mostrando _START_ a _END_ Entradas"

                },

            });

            $('#Asignados').DataTable({

                "searching": false,
                "scrollY": "300px",
                "scrollCollapse": true,
                "paging": false,
                "info": false,
                "oLanguage": {
                    "oPaginate": {
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior",
                    },
                    "sSearch": "Buscar",
                    "sLengthMenu": "Mostrar _MENU_ Registros Por Página",
                    "sInfo": "Mostrando _START_ a _END_ Entradas"

                },

            });
            $('#Asignados> tbody > tr').each(function () {
                $(this).remove();
            })
        }
    })
});

$(document).on('keyup', '#usu_Password, #confirmar-pass', function () {
    var usu_Password = $('#usu_Password').val().trim();
    var bar = $('#confirmar-pass').val().trim();
    valido = document.getElementById('Password');
    if (usu_Password != '' && bar != '') {
        if (usu_Password.length < 8) {
            valido.innerText = "Longitud debe ser de 8 caracteres";
            $('#AgregarRol').prop('disabled', false);
            $('#QuitarRol').prop('disabled', false);
            $('#btnGuardarUsuario').prop('disabled', false);
        }
        else {
            if (!usu_Password || !bar || usu_Password == '' || bar == '') {
                valido.innerText = "Las contraseñas no coinciden";
            }
            else {
                if (usu_Password !== bar) {
                    $('#AgregarRol').prop('disabled', true);
                    $('#QuitarRol').prop('disabled', true);
                    $('#btnGuardarUsuario').prop('disabled', true);
                    valido.innerText = "Las contraseñas no coinciden";
                }

                else {
                    $('#AgregarRol').prop('disabled', false);
                    $('#QuitarRol').prop('disabled', false);
                    $('#btnGuardarUsuario').prop('disabled', false);
                    valido.innerText = "";
                }
            }
        }
    }
});

$(document).on('blur', '#usu_NombreUsuario', function () {
    valido = document.getElementById('NombreUsuario');
    $.ajax({
        url: "/Usuario/GetUserExist",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ user: $('#usu_NombreUsuario').val() }),
    })
    .done(function (data) {
        if (data.length >= 1) {
            valido.innerText = "Usuario ya existe";
            $('#usu_NombreUsuario').focus();
        }
        else {
            valido.innerText = "";
        }
    })
});

$(document).on('blur', '#usu_Password', function () {
    var usu_Password = $('#usu_Password').val().trim();
    valido = document.getElementById('Password');
    if (usu_Password.length < 8) {
        //$('#msg').removeClass('text-success').addClass('text-danger').text('Longitud debe ser de 8 caracteres.');
        valido.innerText = "Longitud debe ser de 8 caracteres.";
        $('#usu_Password').focus();
    }
});

$("#usu_Correo").blur(function () {
    campo = event.target;
    valido = document.getElementById('emailOK');

    var reg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    var regOficial = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (reg.test(campo.value) && regOficial.test(campo.value)) {
        valido.innerText = "";
    } else if (reg.test(campo.value)) {
        valido.innerText = "";

    } else {
        valido.innerText = "Direccion de Correo Electronico Incorrecta";
        $("#usu_Correo").focus();
    }
});

$('#AgregarRol').click(function () {
    $('#NoAsignados> tbody > tr').each(function () {
        idItem = $(this).data('id');
        if ($('#check' + idItem).is(':checked')) {
            active = $(this);
            var RolesUsuario = {
                rol_Id: idItem,
            };

            $.ajax({
                url: "/Usuario/saveRol",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ Roles: RolesUsuario }),
            })
            .done(function (data) {
                if (data == '') {
                    $('#validationDescripcionRol').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se pudo ingresar el registro</ul>');
                }
                else {
                    var Asignados = $('#Asignados').length;
                    $('#NoAsignados tbody').append(active)
                    $('#check' + idItem).prop('checked', false);
                    $(this).remove();
                    $('#Asignados tbody').append(active);
                }
            })
        }
    })
})

$('#QuitarRol').click(function () {
    $('#Asignados> tbody > tr').each(function () {
        idItem = $(this).data('id');
        if ($('#check' + idItem).is(':checked')) {
            active = $(this);
            var RolesUsuario = {
                rol_Id: idItem,
            };

            $.ajax({
                url: "/Usuario/removeRol",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ Roles: RolesUsuario }),
            })
            .done(function (data) {
                if (data == '') {
                    $('#validationDescripcionRol').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se pudo ingresar el registro</ul>');
                }
                else {
                    $('#check' + idItem).prop('checked', false);
                    $(this).remove();
                    $('#NoAsignados tbody').append(active);
                }
            })
        }
    })
})
