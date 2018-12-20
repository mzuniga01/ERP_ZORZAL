$(document).ready(function () {
    $.ajax({
        url: "/Rol/GetObjetosDisponibles",
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
                newTr += '<tr data-id="' + item.obj_Id + '">'
                newTr += '<td id="objpantalla' + item.obj_Id + '">' + item.obj_Pantalla + '</td>'
                newTr += '<td><input name="id02" style="background-color:#1ab394" type="checkbox" id="check' + item.obj_Id + '" /></td>'
                newTr += '</tr>'
                $('#NoAsignados tbody').append(newTr)
            })
            $('#NoAsignados').DataTable({

                "searching": false,
                "oLanguage": {
                    "oPaginate": {
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior",
                    },
                    "sSearch": "Buscar",
                    "sLengthMenu": "Mostrar _MENU_ Registros Por Página",
                    "sInfo": "Mostrando _START_ a _END_ Entradas"

                },
            })

            $('#Asignados> tbody > tr').each(function () {
                $(this).remove();
            })
        }
    })
    $.ajax({
        url: "/Rol/GetObjetosAsignados",
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
                newTr += '<tr data-id="' + item.obj_Id + '">'
                newTr += '<td id="objpantalla' + item.obj_Id + '">' + item.obj_Pantalla + '</td>'
                newTr += '<td><input name="id02" style="background-color:#1ab394" type="checkbox" id="check' + item.obj_Id + '" /></td>'
                newTr += '</tr>'
                $('#Asignados tbody').append(newTr)
            })
            $('#Asignados').DataTable({

                "searching": false,
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
    $('#NoAsignados> tbody > tr').each(function () {
        idItem = $(this).data('id');
        var objpantalla;

        if ($('#check' + idItem).is(':checked')) {
            active = $(this)
            var Asignados = $('#Asignados').length
            $('#NoAsignados tbody').append(active)
            $('#check' + idItem).prop('checked', false);
            $(this).remove();
            $('#Asignados tbody').append(active)

        }
    })
})

$('#Remove').click(function () {
    $('#Asignados> tbody > tr').each(function () {
        idItem = $(this).data('id');
        var objpantalla;

        if ($('#check' + idItem).is(':checked')) {
            active = $(this)
            $('#check' + idItem).prop('checked', false);
            $(this).remove();
        }
    })
})

$('#btnActualizarRol').click(function () {
    var DescripcionRol = $("#rol_Descripcion").val();
    if (DescripcionRol == '') {
        $('#DescripcionRol').text('');
        $('#errorDescripcionRol').text('');
        $('#validationDescripcionRol').after('<ul id="errorDescripcionRol" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
//    else {

//        var TableLeght = $("#Asignados tr").length;
//        var DescripcionRol = $('#rol_Descripcion').val()
//        var AccesoRol = []

//        if (TableLeght > 1) {

//            $('#Asignados> tbody > tr').each(function () {
//                var idItem = $(this).attr('data-id')
//                console.log(idItem);

//                var item = {
//                    obj_Id: idItem,
//                }
//                AccesoRol.push(item)
//                console.log(item);

//            })
//        }
//        console.log(AccesoRol);

//        $.ajax({
//            url: "/Rol/InsertRol",
//            method: "POST",
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify({ DescripcionRol: DescripcionRol, AccesoRol: AccesoRol }),
//        })
//                .done(function (data) {
//                    if (data == '') {
//                        $('#validationDescripcionRol').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se pudo ingresar el registro</ul>');
//                    }
//                    else {
//                        window.location.href = "Index/Rol";
//                    }
//                    console.log(data);
//                })
//    }
//});

    


