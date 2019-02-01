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
    FactIDItem = $(this).closest('tr').data('factid');
    ProductoDescripcionItem = $(this).closest('tr').data('productodescripcion');
    CodigoFacturaItem = $(this).closest('tr').data('codigofactura');
    PorcentajeDescuentoItem = $(this).closest('tr').data('porcentajedescuento')
    CantidadDevItem = $(this).closest('tr').data('cantidaddev')
    $("#tbProducto_prod_Descripcion").val(ProductoDescripcionItem);
    $("#tbDevolucion_tbFactura_fact_Codigo").val(CodigoFacturaItem);
    $("#tbDevolucion_tbFactura_fact_PorcentajeDescuento").val(PorcentajeDescuentoItem);
    $("#devd_CantidadProducto").val(CantidadDevItem);
    GetIDFactura(FactIDItem);
});


//Añadir productos en modal editar producto----------------------------------------------------------------------------
var FacturaID = $('#factID').val();
console.log('facturaid', FacturaID)
function GetIDFactura(FacturaID, FactIDItem) {
    $.ajax({
        url: "/Devolucion/FiltrarModalProducto",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ FacturaID: FacturaID }),

        error: function () {
            alert("No se puede añadir");
        },
        success: function (list) {
            $.each(list, function (key, val) {
                $('#CantidadFacturada').val(val.CantidadFacturada);
                $('#PrecioUnitario').val(val.PrecioUnitario);
                $('#Descuento').val(val.PorcentajeDesc);
                $('#Impuesto').val(val.PorcentajeImpu);
            });
            console.log(list);
        }

    });
}

//Validacion de numeros//
function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
}

$("#devd_CantidadProducto")[0].maxLength = 10;

////Devolucion Seleccionar Producto
//$(document).on("click", "#DataTable1 tbody tr td button#Agregar", function () {
//    idItem = $(this).closest('tr').data('id');
//    DescItem = $(this).closest('tr').data('desc');
//    DescValor = $(this).closest('tr').data('valor');
//    PorcentajeItem = $(this).closest('tr').data('porcentaje');
//    CantidadItem = $(this).closest('tr').data('cantfacturada');
//    ImpuestoItem = $(this).closest('tr').data('impuesto');
//    $("#prod_Codigo").val(idItem);
//    $("#DescripcionProducto").val(DescItem);
//    $("#PrecioUnit").val(DescValor);
//    $("#PorDescuento").val(PorcentajeItem);
//    $("#CantidadFacturada").val(CantidadItem);
//    $("#ImpuestoD").val(ImpuestoItem);
//    $('#ModalAgregarProducto').modal('hide');
//});