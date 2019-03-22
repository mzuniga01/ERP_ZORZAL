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

                "searching": true,
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

                "searching": true,
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

    $('#Table_BuscarEmpleado').DataTable(
        {
            "searching": false,
            "lengthChange": false,

            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",

            }
        });

    var $rows = $('#Table_BuscarEmpleado tr');
    $("#search").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
    });
});

$(document).on('keyup', '#usu_Password, #confirmar-pass', function () {
    var usu_Password = $('#usu_Password').val().trim();
    var bar = $('#confirmar-pass').val().trim();
    valido = document.getElementById('Password');
    if (usu_Password != '' && bar != '') {
        if (usu_Password.length < 8) {
            valido.innerText = "Longitud debe ser mínimo de 8 caracteres";
            $('#AgregarRol').prop('disabled', false);
            $('#QuitarRol').prop('disabled', false);
            $('#btnGuardarUsuario').prop('disabled', true);
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

//$(document).on('blur', '#usu_NombreUsuario', function () {
//    valido = document.getElementById('NombreUsuario');
//    $.ajax({
//        url: "/Usuario/GetUserExist",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ user: $('#usu_NombreUsuario').val() }),
//    })
//    .done(function (data) {
//        if (data.length >= 1) {
//            valido.innerText = "Usuario ya existe";
//        }
//        else {
//            valido.innerText = "";
//        }
//    })
//});

$(document).on('blur', '#usu_Password', function () {
    var usu_Password = $('#usu_Password').val().trim();
    valido = document.getElementById('Password');
    if (usu_Password.length < 8) {
        valido.innerText = "Longitud debe ser de 8 caracteres.";
        //$('#usu_Password').focus();
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
    }
});

$('#AgregarRol').click(function () {
    $('#NoAsignados> tbody > tr').each(function () {
        idItem = $(this).data('id');
        if ($('#check' + idItem).is(':checked')) {
            active = $(this);
            $('#NoAsignados tbody').append(active)
            $('#check' + idItem).prop('checked', false);
            $(this).remove();
            $('#Asignados tbody').append(active)
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
            $('#Asignados tbody').append(active)
            $('#check' + idItem).prop('checked', false);
            $(this).remove();
            $('#NoAsignados tbody').append(active)
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
                    var NoAsignados = $('#NoAsignados').length;
                    $('#Asignados tbody').append(active)
                    $('#check' + idItem).prop('checked', false);
                    $(this).remove();
                    $('#NoAsignados tbody').append(active);
                }
            })
        }
    })
})

//$('#QuitarRol').click(function () {
//    $('#Asignados> tbody > tr').each(function () {
//        idItem = $(this).data('id');
//        if ($('#check' + idItem).is(':checked')) {
//            active = $(this);
//            $('#check' + idItem).prop('checked', false);
//            $(this).remove();
//            $('#NoAsignados tbody').append(active)
//            };

//        })
//    })

//////////////////////////////////////////////////////////
function Seleccionar(emp_Id)
{
    $.ajax({
        url: "/Usuario/getEmpleado",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ EmpleadoID: emp_Id }),
    })
    .done(function (data) {
        if(data.length>0)
        {
            $.each(data, function (i, item) {
                $('#usu_Nombres').val(item.emp_Nombres);
                $('#usu_Apellidos').val(item.emp_Apellidos);
                $('#usu_Correo').val(item.emp_Correoelectronico);
                $('#emp_Id').val(item.emp_Id);
                separador = " ", // un espacio en blanco
                cadena = $('#usu_Nombres').val();
                cadena2 = $('#usu_Apellidos').val();
                arregloDeSubCadenas = Reemplazar(cadena);
                arregloDeSubCadenas2 = Reemplazar(cadena2);
                arregloDeSubCadenas = arregloDeSubCadenas.split(separador);
                arregloDeSubCadenas2 = arregloDeSubCadenas2.split(separador);
                $('#usu_NombreUsuario').val(arregloDeSubCadenas + "." + arregloDeSubCadenas2);
                $("#ModalAgregarEmpleado").modal('hide');
            })
        }
    })
}

function Reemplazar(s) {
    var r = s.toLowerCase();
    r = r.replace(new RegExp("\\s", 'g'), "");
    r = r.replace(new RegExp("[àáâãäå]", 'g'), "a");
    r = r.replace(new RegExp("æ", 'g'), "ae");
    r = r.replace(new RegExp("ç", 'g'), "c");
    r = r.replace(new RegExp("[èéêë]", 'g'), "e");
    r = r.replace(new RegExp("[ìíîï]", 'g'), "i");
    r = r.replace(new RegExp("ñ", 'g'), "n");
    r = r.replace(new RegExp("[òóôõö]", 'g'), "o");
    r = r.replace(new RegExp("œ", 'g'), "oe");
    r = r.replace(new RegExp("[ùúûü]", 'g'), "u");
    r = r.replace(new RegExp("[ýÿ]", 'g'), "y");
    r = r.replace(new RegExp("\\W", 'g'), "");
    return r;
};

function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9a-zA-Z-_.#*]+$/.test(tecla);
}

function nombreusuario(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-Z-.]+$/.test(tecla);
}


