$("#search").click(function () {
    var $rows = $('#DevolucionTbody tr');
    $('#post').each(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
    });
});

$(document).ready(function () {
    $('#tblSalidaDetalle').DataTable(
        {
            "searching": true,
            "lengthChange": true,
            "columnDefs": [{ "targets": [9], "visible": false, "searchable": false }],
             "order": [[9, 'desc']],
            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            }
        });
});

$(document).ready(function () {
    $('#tblUsuario').DataTable(
        {
            "searching": true,
            "lengthChange": true,

            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            }
        });
});

$(document).ready(function () {
    $('#Datatable').DataTable({
        "searching": true,
        "lengthChange": true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },
    });
});