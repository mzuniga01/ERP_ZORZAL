$(document).on("click", "#PedidoDetalle tbody tr td button#QuitarDetalle", function () {
    var table = $('#PedidoDetalle').DataTable();
    table
        .row($(this).parents('tr'))
        .remove()
        .draw();
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    prod_codigo = $(this).closest('tr').data('prod_codigo');
    var PedidoDetalle = {
        prod_Codigo: prod_codigo,
        pedd_UsuarioCrea: idItem,
    };
    $.ajax({
        url: "/Pedido/QuitarPedidoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ PedidoDetalle: PedidoDetalle }),
    });
});