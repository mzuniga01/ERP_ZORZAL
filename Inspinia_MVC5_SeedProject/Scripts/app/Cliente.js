//Factura Buscar Cliente
$(document).ready(function () {
    var $rows = $('#ClienteTbody tr');
    $("#searchCliente").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
        console.log('val', val.length);
        if (val.length >= 3) {
            $rows.show().filter(function () {
                var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                return !~text.indexOf(val);
            }).hide();
        }
        else if (val.length >= 1) {
            $rows.show().filter(function () {
            }).hide();
        }

    })
});

//Factura Seleccionar Cliente
$(document).on("click", "#tbCliente tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    NombreCliente = $(this).closest('tr').data('name');
    $("#clte_Identificacion").val(idItem);
    $("#clte_Nombres").val(NombreCliente);
    $('#ModalAgregarCliente').modal('hide');
    //CargarAsignaciones();
});

//Facturar RowSeleccionar Cliente
$(document).ready(function () {
    var table = $('#tbCliente').DataTable();

    $('#tbCliente tbody').on('dblclick', 'tr', function () {
        idItem = $(this).closest('tr').data('id');
        NombreCliente = $(this).closest('tr').data('name');
        $("#clte_Identificacion").val(idItem);
        $("#clte_Nombres").val(NombreCliente);
        $('#ModalAgregarCliente').modal('hide');
    });
});


