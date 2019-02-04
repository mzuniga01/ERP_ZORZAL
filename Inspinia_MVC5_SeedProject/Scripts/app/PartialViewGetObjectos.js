$(document).ready(function () {

    var rolId = $("#dd").text();
    $.ajax({
        url: "/Rol/GetObjetosAsignados",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ rolId: rolId }),
    })
        .done(function (data) {
            if (data.length < 1) {
                $('#DataTable').DataTable({

                    "searching": false,
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
            else {
                $.each(data, function (i, model) {
                    newTr = '';
                    newTr += '<tr data-id="' + model.obj_Id + '">'
                    newTr += '<td id="">' + model.obj_Id + '</td>'
                    newTr += '<td id="">' + model.obj_Pantalla + '</td>'
                    newTr += '</tr>'
                    $('#DataTable tbody').append(newTr)
                })
                $('#DataTable').DataTable({

                    "searching": false,
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
