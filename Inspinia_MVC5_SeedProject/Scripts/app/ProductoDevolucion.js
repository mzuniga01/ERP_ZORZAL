//Devolucion Seleccionar Producto
$(document).on("click", "#DataTable1 tbody tr td button#Agregar", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    DescValor = $(this).closest('tr').data('valor');
    DescuentoItem = $(this).closest('tr').data('descuento');
    PorcentajeItem = $(this).closest('tr').data('porcentaje');
    CantidadItem = $(this).closest('tr').data('cantfacturada');
    //console.log(CantidadItem)
    ImpuestoItem = $(this).closest('tr').data('impuesto');
    $("#prod_Codigo").val(idItem);
    $("#tbProducto_prod_Descripcion").val(DescItem);
    $("#PrecioUnitario").val(DescValor);
    $("#MontoDescuento").val(DescuentoItem);
    $("#Descuento").val(PorcentajeItem);
    $("#CantidadFacturada").val(CantidadItem);
    $("#Impuesto").val(ImpuestoItem);
    $('#ModalBuscarProducto').modal('hide');
});
$("#devd_CantidadProducto")[0].maxLength = 10;

//Validacion de numeros//
function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
}

//Validacion de cantidad de producto devuelto
$("#devd_CantidadProducto").blur(function () {
    valido = document.getElementById('smsCantidad');
    var CantFacturada = $('#CantidadFacturada').val();
    var CantDevolucion = $('#devd_CantidadProducto').val();
    
    if (parseFloat(CantFacturada) < parseFloat(CantDevolucion)) {
        console.log("facturada",CantFacturada)
        console.log(CantDevolucion)
        console.log("if")
        valido.innerText = "El valor debe ser menor a la cantidad facturada";
    }
    else {
        console.log("else")
        valido.innerText = "";
    }
});

//Devolucion Agregar Producto
$(document).on("click", "#EditarDetalle tbody tr td button#Producto", function () {
    ProductoDescripcionItem = $(this).closest('tr').data('productodescripcion');
    CodigoFacturaItem = $(this).closest('tr').data('codigofactura');
    PorcentajeDescuentoItem = $(this).closest('tr').data('porcentajedescuento')
    PrecioUnitarioItem = $(this).closest('tr').data('preciounitario')
    $("#tbProducto_prod_Descripcion").val(ProductoDescripcionItem);
    $("#tbDevolucion_tbFactura_fact_Codigo").val(CodigoFacturaItem);
    $("#tbDevolucion_tbFactura_fact_PorcentajeDescuento").val(PorcentajeDescuentoItem);
    $("#test_factd_PrecioUnitario").val(PrecioUnitarioItem);
   
});

