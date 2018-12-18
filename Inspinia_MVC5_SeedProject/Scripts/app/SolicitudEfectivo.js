$(document).ready(function () {
    $('#SolicitudEfectivo').DataTable(
    {
        "searching": true,

        "oLanguage": {
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior",
            },
            "sEmptyTable": "No hay registros",
            "sInfoEmpty": "Mostrando 0 de 0 Entradas",
            "sSearch": "Buscar SolicitudEfectivo",
            "sLengthMenu": "Mostrar _MENU_ registros por página",
            "sInfo": "Mostrando _START_ a _END_ Entradas",
        }
    });
});


$(document).ready(function () {
    var $rows = $('#TbdenominacionTBody tr');
    $("#searchDenominacion").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
    })
});

$(document).on("click", "#TbdenominacionTBody tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    DescItem = $(this).closest('tr').data('desc1');
    $("#tbDenominacion_deno_Descripcion").val(idItem);
    $("#tbDenominacion_deno_Tipo").val(DescItem);
    $('#ModalAgregarSolicitudEfectivoDetalleDenominacion').modal('hide');
    //CargarAsignaciones();
});

$(document).on("click", "#Denominacion tbody tr td button#AgregarDetalleSolicitudEfectico", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    DescItem = $(this).closest('tr').data('desc1');
    $("#deno_Descripcion").val(idItem);
    $("#deno_Tipo").val(DescItem);
    $('#ModalAgregarSolicitudEfectivoDetalleDenominacion').modal('hide');
    //CargarAsignaciones();
});
