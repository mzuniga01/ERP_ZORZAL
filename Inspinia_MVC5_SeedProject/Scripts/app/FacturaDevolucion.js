$(document).ready(function () {
    var $rows = $('#DataTable tr');
    $("#searchFactura").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
    })
});



$(document).on("click", "#DataTable tbody tr td button#AgregarFactura", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    ClienteItem = $(this).closest('tr').data('cliente');
    console.log('Cliente', ClienteItem)
    $("#fact_Codigo").val(idItem);
    $("#tbFactura_clte_Identificacion").val(DescItem);
    $("#tbFactura_clte_Nombres").val(ClienteItem);
    $('#ModalAgregarFactura').modal('hide');
    //CargarAsignaciones();
});
