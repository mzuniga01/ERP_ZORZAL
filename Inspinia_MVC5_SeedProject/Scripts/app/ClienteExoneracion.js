//Exoneracion RowSeleccionar Cliente
$(document).on("click", "#tbCliente tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    rtnItem = $(this).closest('tr').data('rtn');
    NombreCliente = $(this).closest('tr').data('name');
    $("#clte_Id").val(idItem);
    $("#tbCliente_clte_Identificacion").val(rtnItem);
    $("#tbCliente_clte_Nombres").val(NombreCliente);
    $('#ModalAgregarClientes').modal('hide');
});

$(document).on("click", "#tbClienteSC tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    rtnItem = $(this).closest('tr').data('rtn');
    nombreItem = $(this).closest('tr').data('nombre');
    $("#clte_Id").val(idItem);
    $("#tbCliente_clte_Identificacion").val(rtnItem);
    $("#tbCliente_clte_Nombres").val(nombreItem);
    $('#ModalAgregarClientes').modal('hide');
});



