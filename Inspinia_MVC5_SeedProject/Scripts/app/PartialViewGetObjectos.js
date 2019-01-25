$(document).ready(function () {

    var rolId = $("#dd").text();
    console.log("Hola Desde tiempos asestrales");


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
                        "sSearch": "Buscar",
                        "sLengthMenu": "Mostrar _MENU_ Registros Por Página",
                        "sInfo": "Mostrando _START_ a _END_ Entradas"

                    },

                });
                $('#DataTable> tbody > tr').each(function () {
                    $(this).remove();
                })
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
                        "sSearch": "Buscar",
                        "sLengthMenu": "Mostrar _MENU_ Registros Por Página",
                        "sInfo": "Mostrando _START_ a _END_ Entradas"

                    },

                });
            }
        })
});
