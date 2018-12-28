//Máximo de caracteres
$(document).ready(function () {
    $("#pemi_NumeroCAI")[0].maxLength = 40;
});

//Conversión a mayúscula
$("#pemi_NumeroCAI").change(function () {
    var str = $("#pemi_NumeroCAI").val();
    var res = str.toUpperCase();
    $("#pemi_NumeroCAI").val(res);
});

$(pemi_NumeroCAI).on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})

//Datatables
$(document).ready(function () {
    $('#PuntoEmision').DataTable(
    {
        "searching": true,

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