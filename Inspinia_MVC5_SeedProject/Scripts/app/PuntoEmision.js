$(document).ready(function () {
    $("#pemi_NumeroCAI")[0].maxLength = 37;

    $('#PuntoEmision').DataTable(
    {
        "searching": true,
        "responsive": true,
        "oLanguage": {
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior",
            },
            "sZeroRecords": "No se encontraron resultados",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sEmptyTable": "No hay registros",
            "sInfoEmpty": "Mostrando 0 de 0 Entradas",
            "sSearch": "Buscar",
            "sLengthMenu": "Mostrar _MENU_ registros por página",
            "sInfo": "Mostrando _START_ a _END_ Entradas",
        }
    });
});

//Limpiar mensajes a tiempo real
$("#pemi_NumeroCAI").keyup(function () {
    $('#MessageNumeroCAICreate').text('');
});