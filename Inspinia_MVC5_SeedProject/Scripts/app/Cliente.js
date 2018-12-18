$(document).ready(function () {
    var $rows = $('#tbCliente tr');
    $("#searchCliente").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
    })
});

$(document).on("click", "#tbCliente tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    $("#clte_Identificacion").val(idItem);
    $('#ModalAgregarCliente').modal('hide');
    //CargarAsignaciones();
});

$(document).on("click", "#tbClientes tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    nombreItem = $(this).closest('tr').data('nombre');

    $("#tbCliente_clte_Nombres").val(nombreItem);
    $("#tbCliente_clte_Identificacion").val(idItem);
    $('#ModalAgregarCliente').modal('hide');
    //CargarAsignaciones();
});