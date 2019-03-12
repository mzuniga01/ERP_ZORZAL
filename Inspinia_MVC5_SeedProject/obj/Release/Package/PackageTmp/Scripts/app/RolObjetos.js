$(document).ready(function () {
    $("#rol_Descripcion")[0].maxLength = 100;
    $.ajax({
        url: "/Rol/GetObjetos",
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
                "searching": true,
                //"scrollY": "300px",
                //"scrollCollapse": true,
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
                        "sZeroRecords": "No se encontraron resultados",
                        "sInfoFiltered": "(Filtrado de _MAX_ total entradas)",
                    },

            });
            
            
                $('#Asignados').DataTable({
                    "searching": true,
                    "scrollY": "300px",
                    "order": false,
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
                        "sZeroRecords": "No se encontraron resultados",
                        "sInfoFiltered": "(Filtrado de _MAX_ total entradas)",
                    },

                });
                $('#Asignados> tbody > tr').each(function () {
                        $(this).remove();
                })


                             
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
            $('#NoAsignados tbody').append(active)
        }
    })
})

$('#rol_Descripcion').on('input', function (e) {
    if (!/^[ a-z-áéíóúüñ]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^ a-z-áéíóúüñ]+/ig, "");
    }
});

$('#btnGuardarRol').click(function () {
    var DescripcionRol = $("#rol_Descripcion").val();
    if (DescripcionRol == '') {
        $('#DescripcionRol').text('');
        $('#errorDescripcionRol').text('');
        $('#validationDescripcionRol').after('<ul id="errorDescripcionRol" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
    else if (DescripcionRol.substring(0, 1) == " ") {
        $('#DescripcionRol').text('');
        $('#errorDescripcionRol').text('');
        $('#validationDescripcionRol').after('<ul id="errorDescripcionRol" class="validation-summary-errors text-danger">El primer caracter no puede un espacio en blanco.</ul>');
    }
    else {

        var TableLeght = $("#Asignados tr").length;
        var DescripcionRol = $('#rol_Descripcion').val()
        var AccesoRol = []

        if (TableLeght > 1) {

            $('#Asignados> tbody > tr').each(function () {
                var idItem = $(this).attr('data-id')
                console.log(idItem);

                var item = {
                    obj_Id: idItem,
                }
                AccesoRol.push(item)
                console.log(item);

            })
        }
        console.log(AccesoRol);

        $.ajax({
            url: "/Rol/InsertRol",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ DescripcionRol: DescripcionRol, AccesoRol: AccesoRol }),
        })
                .done(function (data) {
                    if (data == '-2') {
                        $('#validationDescripcionRol').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">Ya existe un rol con el mismo nombre</ul>');
                    }
                    else if (data == '-1') {
                        $('#validationDescripcionRol').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se pudo ingresar el registro</ul>');
                    }
                    else {
                        //swal("El registro se creo exitosamente!", "Será devuelto al Listado", "success");
                        //setTimeout(
                        //function () {
                            window.location.href = '/Rol/Index';
                        //}, 3000);
                    }
                })
    }
})
$("#rol_Descripcion").change(function () {
    var txtDescripcion = $("#rol_Descripcion").val();
    if (txtDescripcion == "") {
        $('#Add').attr("disabled", true);
        $('#Remove').attr("disabled", true);
    }
    else {
        $('#Add').attr("disabled", false);
        $('#Remove').attr("disabled", false);
    }
});




