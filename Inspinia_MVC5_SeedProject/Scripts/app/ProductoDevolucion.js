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


//Devolucion Agregar Producto
$(document).on("click", "#DataTable1 tbody tr td button#Producto", function () {
    ProductoDescripcionItem = $(this).closest('tr').data('productodescripcion');
    CodigoFacturaItem = $(this).closest('tr').data('codigofactura');
    PorcentajeDescuentoItem = $(this).closest('tr').data('porcentajedescuento')
    PrecioUnitarioItem = $(this).closest('tr').data('preciounitario')
    $("#tbProducto_prod_Descripcion").val(ProductoDescripcionItem);
    $("#tbDevolucion_tbFactura_fact_Codigo").val(CodigoFacturaItem);
    $("#tbDevolucion_tbFactura_fact_PorcentajeDescuento").val(PorcentajeDescuentoItem);
    $("#test_factd_PrecioUnitario").val(PrecioUnitarioItem);
});
