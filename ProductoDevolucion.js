//Devolucion Seleccionar Producto
$(document).on("click", "#DataTable1 tbody tr td button#Agregar", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    DescValor = $(this).closest('tr').data('valor');
    $("#prod_Codigo").val(idItem);
    $("#tbProducto_prod_Descripcion").val(DescItem);
    $("#PrecioUnitario").val(DescValor);
    $('#ModalBuscarProducto').modal('hide');
});
