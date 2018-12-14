$(document).on("click", "#tbFacturaDevolucion tbody tr td button#AgregarFactura", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    $("#fact_Codigo").val(idItem);
    $("#clte_Identificacion").val(DescItem);
    $("#clte_Nombres").val(DescItem);
    $('#ModalAgregarFactura').modal('hide');
    //CargarAsignaciones();
});
