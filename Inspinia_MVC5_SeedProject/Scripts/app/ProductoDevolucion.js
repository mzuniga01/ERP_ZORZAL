//Busqueda de Producto en Devolucion-----------
$(document).ready(function () {
    var $rows = $('#BodyProducto tr');
    $("#searchProducto").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
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

//Devolucion Agregar Producto en el editar devolucion
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


//Validacion de numeros//
function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
}

////Validacion de cantidad de producto devuelto
//$("#devd_CantidadProducto").blur(function () {
//    valido = document.getElementById('smsCantidad');
//    var CantFacturada = $('#CantidadFacturada').val();
//    var CantDevolucion = $('#devd_CantidadProducto').val();
//    var CodigoProducto = $('#prod_Codigo').val();
    
//    if (parseFloat(CantFacturada) < parseFloat(CantDevolucion)) {
//        console.log("facturada",CantFacturada)
//        console.log(CantDevolucion)
//        console.log("if")
//        valido.innerText = "El valor debe ser menor a la cantidad facturada";
//    }
//    else {
//        console.log("else")
//        valido.innerText = "";
//    }

//    if (CodigoProducto != CantDevolucion) {
//        valido.innerText = "Codigo de Producto Incorrecto";
//    }
//    else {
//        valido.innerText = "";
//    }
//});

$("#devd_CantidadProducto")[0].maxLength = 10;

