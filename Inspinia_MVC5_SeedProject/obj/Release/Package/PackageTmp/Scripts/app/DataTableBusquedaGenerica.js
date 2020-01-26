

$(document).ready(function () {
    $('#DataTable1').DataTable(
    {
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
        }
    });
});
