//Factura Buscar Producto
$(document).ready(function () {
    var $rows = $('#ProductoTbody tr');
    $("#search").keyup(function () {
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

// Factura Seleccionar Producto
$(document).on("click", "#tbProductoFactura tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    ISVItem = $(this).closest('tr').data('isv');
    $("#prod_Codigo").val(idItem);
    $("#tbProducto_prod_Descripcion").val(DescItem);
    $("#factd_Impuesto").val(ISVItem);
    $('#ModalAgregarProducto').modal('hide');
    //CargarAsignaciones();
});

//Facturar RowSeleccionar Producto
$(document).ready(function () {
    var table = $('#tbProductoFactura').DataTable();

    $('#tbProductoFactura tbody').on('click', 'tr', function () {
        idItem = $(this).closest('tr').data('id');
        DescItem = $(this).closest('tr').data('desc');
        ISVItem = $(this).closest('tr').data('isv');
        $("#prod_Codigo").val(idItem);
        $("#tbProducto_prod_Descripcion").val(DescItem);
        $("#factd_Impuesto").val(ISVItem);
        $('#ModalAgregarProducto').modal('hide');
    });
});


//Devolucion Seleccionar Producto
$(document).on("click", "#DataTable1 tbody tr td button#Agregar", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    DescValor = $(this).closest('tr').data('valor');
    $("#prod_Codigo").val(idItem);
    $("#tbProducto_prod_Descripcion").val(DescItem);
    $("#PrecioUnitario").val(DescValor);
    $('#ModalBuscarProducto').modal('hide');
    //CargarAsignaciones();
});



