$(document).ready(function () {
    var $rows = $('#DevFactura tr');
    $("#searchFactura").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
    })
});



$(document).on("click", "#DevFactura tbody tr td button#AgregarFactura", function () {
    idItem = $(this).closest('tr').data('id');
    CodigoItem = $(this).closest('tr').data('codigo');
    DescItem = $(this).closest('tr').data('desc');
    ClienteItem = $(this).closest('tr').data('cliente');
    console.log('Cliente', CodigoItem)
    $("#tbFactura_fact_Codigo").val(idItem);
    $("#fact_Id").val(CodigoItem);
    $("#tbFactura_clte_Identificacion").val(DescItem);
    $("#tbFactura_clte_Nombres").val(ClienteItem);
    $('#ModalAgregarFactura').modal('hide');
    //CargarAsignaciones();
   
});
