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
    idbarraItem = $(this).closest('tr').data('barra');
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    ISVItem = $(this).closest('tr').data('isv');
    $("#prod_CodigoBarras").val(idbarraItem);
    $("#prod_Codigo").val(idItem);
    $("#tbProducto_prod_CodigoBarras").val(idCbItem);
    $("#tbProducto_prod_Descripcion").val(DescItem);
    $("#factd_Impuesto").val(ISVItem);
    $('#ModalAgregarProducto').modal('hide');
});

//Facturar RowSeleccionar Producto
$(document).ready(function () {
    var table = $('#tbProductoFactura').DataTable();

    $('#tbProductoFactura tbody').on('click', 'tr', function () {
        idbarraItem = $(this).closest('tr').data('barra');
        idItem = $(this).closest('tr').data('id');
        DescItem = $(this).closest('tr').data('desc');
        ISVItem = $(this).closest('tr').data('isv');
        $("#prod_CodigoBarras").val(idbarraItem);
        $("#prod_Codigo").val(idItem);
        $("#tbProducto_prod_Descripcion").val(DescItem);
        $("#factd_Impuesto").val(ISVItem);
        $('#ModalAgregarProducto').modal('hide');
        var Cliente = $('#clte_Id').val();
        if (Cliente == '') {
            Cliente = 0;
            GetPrecio(Cliente,idItem);
        }
        else {
            GetPrecio(Cliente, idItem);
        }
       
        function GetPrecio(Cliente, idItem) {
            $.ajax({
                url: "/Factura/GetPrecio",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ Cliente: Cliente,idItem:idItem }),
            })
            .done(function (data) {
                var g = data;
                $("#factd_PrecioUnitario").val(g);
            });
        }
    });
});

//Devolucion Seleccionar Producto
$(document).on("click", "#DataTable1 tbody tr td button#Agregar", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    $("#prod_Codigo").val(idItem);
    $("#tbProducto_prod_Descripcion").val(DescItem);
    $('#ModalBuscarProducto').modal('hide');
    //CargarAsignaciones();
});



