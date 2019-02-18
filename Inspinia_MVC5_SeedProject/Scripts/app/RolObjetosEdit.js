$(document).ready(function () {
    $.ajax({
        url: "/Rol/GetObjetosDisponibles",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ rolId: $('#rol_Id').val() }),
    })
    .done(function (data) {
        if (data.length < 1) {
        }
        else {
            $.each(data, function (i, item) {

                newTr = '';
                newTr += '<tr data-id="' + item.obj_Id + '">'
                newTr += '<td id="objpantalla' + item.obj_Id + '">' + item.obj_Pantalla + '</td>'
                newTr += '<td><input name="id02" style="background-color:#1ab394" type="checkbox" id="check' + item.obj_Id + '" /></td>'
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
                    "sEmptyTable": "No hay registros",
                    "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                    "sSearch": "Buscar",
                    "sLengthMenu": "Mostrar _MENU_ registros por página",
                    "sInfo": "Mostrando _START_ a _END_ Entradas",

                },

            });
        }
    })
    $.ajax({
        url: "/Rol/GetObjetosAsignados",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ rolId: $('#rol_Id').val() }),
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
                    "sEmptyTable": "No hay registros",
                    "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                    "sSearch": "Buscar",
                    "sLengthMenu": "Mostrar _MENU_ registros por página",
                    "sInfo": "Mostrando _START_ a _END_ Entradas",

                },

            });
            $('#AsignadosEdit> tbody > tr').each(function () {
                $(this).remove();
            })
        }
        else {
            $.each(data, function (i, item) {
                
                newTr = '';
                newTr += '<tr data-id="' + item.obj_Id + '">'
                newTr += '<td id="objpantalla' + item.obj_Id + '">' + item.obj_Pantalla + '</td>'
                newTr += '<td><input name="id02" style="background-color:#1ab394" type="checkbox" id="check' + item.obj_Id + '" /></td>'
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
                    "sEmptyTable": "No hay registros",
                    "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                    "sSearch": "Buscar",
                    "sLengthMenu": "Mostrar _MENU_ registros por página",
                    "sInfo": "Mostrando _START_ a _END_ Entradas",

                },

            });
        }
    })
});
$('#Add').click(function () {
        var idRol = $('#rol_Id').val()
        var RolAcceso = []
        $('#NoAsignadosEdit> tbody > tr').each(function () {
            idItem = $(this).data('id');
            var objpantalla;
            if ($('#check' + idItem).is(':checked')) {
                active = $(this)
                var Asignados = $('#AsignadosEdit').length
                $('#NoAsignadosEdit tbody').append(active)
                $('#check' + idItem).prop('checked', false);
                $(this).remove();
                $('#AsignadosEdit tbody').append(active)

                var item = {
                    obj_Id: idItem,
                }
                RolAcceso.push(item)

                
            }
        })
        $.ajax({
            url: "/Rol/AgregarObjeto",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ idRol: idRol, RolAcceso: RolAcceso }),
            success: function (json) {
            },
            error: function () {
                $('#validationDescripcionRol').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se pudo añadir la pantalla, contacte con el administrador</ul>');
            }
        })

                    .done(function (data) {
                        if (data == '') {
                            var TableLegth = $("#NoAsignadosEdit tr").length;
                            }
                        else {
                        }
                    });
})

    $('#Remove').click(function () {
        var idRol = $('#rol_Id').val()
        var RolAcceso = []
        $('#AsignadosEdit> tbody > tr').each(function () {
            idItem = $(this).data('id');
            var objpantalla;

            if ($('#check' + idItem).is(':checked')) {
                active = $(this)
                $('#check' + idItem).prop('checked', false);
                $(this).remove();
                $('#NoAsignadosEdit tbody').append(active)
                var item = {
                    obj_Id: idItem,
                }
                RolAcceso.push(item)
            }
        })
        $.ajax({
            url: "/Rol/QuitarObjeto",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ idRol: idRol, RolAcceso: RolAcceso }),
        })
                    .done(function (data) {
                        if (data == '') {

                        }
                        else {
                        }
                    })
        var TableLeght = $("#NoAsignadosEdit tr").length;
        
        

    })

    $('#rol_Descripcion').on('input', function (e) {
        if (!/^[ a-z-áéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ a-z-áéíóúüñ]+/ig, "");
        }
    });

    $('#btnActualizarRol').click(function () {
        var rolId = $("#rol_Id").val();
        var Descripcion = $("#rol_Descripcion").val();
        if (Descripcion == '') {
            $('#DescripcionRol').text('');
            $('#errorDescripcionRol').text('');
            $('#validationDescripcionRol').after('<ul id="errorDescripcionRol" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
        }
        else if (Descripcion.substring(0, 1) == " ") {
            $('#DescripcionRol').text('');
            $('#errorDescripcionRol').text('');
            $('#validationDescripcionRol').after('<ul id="errorDescripcionRol" class="validation-summary-errors text-danger">El primer caracter no puede un espacio en blanco.</ul>');
        }
        else {
            $.ajax({
                url: "/Rol/UpdateRol",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ rolId: rolId, Descripcion: Descripcion }),
            })
                        .done(function (data) {
                            if (data == '') {
                                $('#validationDescripcionRol').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se pudo actualizar el registro, contacte con el administrador</ul>');
                            }
                            else {
                                //swal("El registro se editó exitosamente!", "Será devuelto al Listado", "success");
                                //setTimeout(
                                //function () {
                                    window.location.href = '/Rol/Index';
                                //}, 3000);
                                
                            }
                        })
        }
    });