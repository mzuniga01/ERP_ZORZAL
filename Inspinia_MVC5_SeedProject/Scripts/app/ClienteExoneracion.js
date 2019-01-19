$(function () {
var from = $("#exo_FechaInicialVigencia").val();
var to = $("#exo_FechaIFinalVigencia").val();

if (Date.parse(from) > Date.parse(to)) {
    valido = document.getElementById('cc');
    valido.innerText = "Fecha inicial mayor a la fecha vencimiento";
}
});




$(document).on("click", "#tbCliente tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    rtnItem = $(this).closest('tr').data('rtn');
    NombreCliente = $(this).closest('tr').data('name');
    $("#clte_Id").val(idItem);
    $("#tbCliente_clte_Identificacion").val(rtnItem);
    $('#ModalAgregarClientes').modal('hide');
    $("#tbCliente_clte_Nombres").val(NombreCliente)
    $("#tbCliente_clte_NombreComercial").val(NombreCliente);
});