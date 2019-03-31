$(document).ready(function () {
    $.ajax({
        url: "/Usuario/GetRolesDisponibles",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ rolId: $('#usu_Id').val() }),
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
                $('#NoAsignadosEdit tbody').append(newTr)
            })
            $('#NoAsignadosEdit').DataTable({
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
                    "sInfo": "Mostrando _START_ a _END_ Entradas",

                },

            });
        }
    })
    $.ajax({
        url: "/Usuario/GetRolesAsignados",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ rolId: $('#usu_Id').val() }),
    })
    .done(function (data) {
        if (data.length < 1) {
            $('#AsignadosEdit').DataTable({
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
            $('#AsignadosEdit> tbody > tr').each(function () {
                $(this).remove();
            })
        }
        else {
            $.each(data, function (i, item) {

                newTr = '';
                newTr += '<tr data-id="' + item.rol_Id + '">'
                newTr += '<td id="objpantalla' + item.rol_Id + '">' + item.rol_Descripcion + '</td>'
                newTr += '<td><input name="id03" style="background-color:#1ab394" type="checkbox" id="check' + item.rol_Id + '" /></td>'
                newTr += '</tr>'
                $('#AsignadosEdit tbody').append(newTr)
            })
            $('#AsignadosEdit').DataTable({
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
        }
    })


});
$('#Add').click(function () {
    var idRol = $('#usu_Id').val()
    var RolUsuario = []
    $('#NoAsignadosEdit> tbody > tr').each(function () {
        idItem = $(this).data('id');
        var rolusuario;
        if ($('#check' + idItem).is(':checked')) {
            active = $(this)
            var Asignados = $('#AsignadosEdit').length
            $('#NoAsignadosEdit tbody').append(active)
            $('#check' + idItem).prop('checked', false);
            $(this).remove();
            $('#AsignadosEdit tbody').append(active)

            var item = {
                rol_Id: idItem,
            }
            RolUsuario.push(item)

        }
    })
    $.ajax({
        url: "/Usuario/AgregarRol",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ idRol: idRol, RolUsuario: RolUsuario }),
        success: function (json) {
            //Recargar();
        },
        error: function () {
            $('#validationDescripcionRol').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se pudo añadir la pantalla, contacte con el administrador</ul>');
        }
    })

                .done(function (data) {
                    if (data == '') {
                        console.log("Guardado Exito");
                        var TableLegth = $("#NoAsignadosEdit tr").length;
                        //if (TableLegth > 1) {
                        //    $("#NoAsignadosEdit tbody").empty();
                        //};
                    }
                    else {
                        console.log("Guardado Fallido");
                    }
                });
})

$('#Remove').click(function () {
    var usu_Id = $('#usu_Id').val()
    var RolUsuario = []
    $('#AsignadosEdit> tbody > tr').each(function () {
        idItem = $(this).data('id');
        var rolusuario;

        if ($('#check' + idItem).is(':checked')) {
            active = $(this)
            $('#check' + idItem).prop('checked', false);
            $(this).remove();
            $('#NoAsignadosEdit tbody').append(active)
            var item = {
                rol_Id: idItem,
            }
            RolUsuario.push(item)
        }
    })
    $.ajax({
        url: "/Usuario/QuitarRol",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ usu_Id: usu_Id, RolUsuario: RolUsuario }),
    })
                .done(function (data) {
                    if (data == '') {
                        console.log("Quitar Exito");

                    }
                    else {
                        console.log("Quitar Fallido");
                    }
                })
    var TableLeght = $("#NoAsignadosEdit tr").length;



})

function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}

function Caracteres_Email(e) {

    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ1234567890@._-]+$/.test(tecla);

}

function CorreoElectronico(string) {//Algunos caracteres especiales para el correo
    var out = '';
    //Se añaden las letras validas
    var filtro = 'abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890@ .-_';//Caracteres validos

    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) != -1)
            out += string.charAt(i);

    return out;
}
$('#usu_Nombres').on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50)

});

$('#usu_Apellidos').on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50)

});

$('#Inactivar').click(function () {
    var usu_id = $('#usu_Id').val();
    var Activo = 0
    var Razon_Inactivacion = $('#razonInac').val();
    console.log(usu_id)
    console.log(Activo)
    console.log(Razon_Inactivacion)
    if (Razon_Inactivacion == "") {
        valido = document.getElementById('Mensaje');
        valido.innerText = "La razón inactivación es requerida";
    }

    else {
        $.ajax({
            url: "/Usuario/EstadoInactivar",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ usu_id: usu_id, Activo: Activo, Razon_Inactivacion: Razon_Inactivacion }),

        })
    .done(function (data) {
        if (data.length > 0) {
            var url = $("#RedirectTo").val();
            location.href = url;
        }
        else {
            alert("Registro No Actualizado");
        }
    });
    }

})

$('#Activar').click(function () {
    var usu_id = $('#usu_Id').val();
    var Activo = 1
    var Razon_Inactivacion = null;
    $.ajax({
        url: "/Usuario/Estadoactivar",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ usu_id: usu_id, Activo: Activo, Razon_Inactivacion: Razon_Inactivacion }),

    })
    .done(function (data) {
        if (data.length > 0) {
            var url = $("#RedirectTo").val();
            location.href = url;
        }
        else {
            alert("Registro No Actualizado");
        }
    });
})

////Validacion De solo letras 
function CaracteresNombre(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);

}
function NumText(string) {//solo letras y numeros
    var out = '';
    //Se añaden las letras validas
    var filtro = 'abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890áéíóúÁÉÍÓÚ ,.';//Caracteres validos

    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) != -1)
            out += string.charAt(i);

    return out;
}