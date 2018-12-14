$(document).ready(function () {
    var $rows = $('#tbProductoFactura tr');
    $("#search").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);}).hide();
    })
});

$(document).on("click", "#tbProductoFactura tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    $("#prod_Codigo").val(idItem);
    $("#tbProducto_prod_Descripcion").val(DescItem);
    $('#ModalAgregarProducto').modal('hide');
    //CargarAsignaciones();
});

$(document).on("click", "#tbProductoDevolucion tbody tr td button#Agregar", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    $("#prod_Codigo").val(idItem);
    $("#tbProducto_prod_Descripcion").val(DescItem);
    $('#ModalBuscarProducto').modal('hide');
    //CargarAsignaciones();
});

