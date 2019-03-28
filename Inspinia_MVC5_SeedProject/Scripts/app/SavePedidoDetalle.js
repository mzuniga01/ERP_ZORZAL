$(document).on("click", "#PedidoDetalle tbody tr td button#QuitarDetalle", function () {
    var table = $('#PedidoDetalle').DataTable();
    var prod_Codigo = $(this).closest("tr").find("td:eq(0)").text();
    table
        .row($(this).parents('tr'))
        .remove()
        .draw();
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    //prod_codigo = $(this).closest('tr').data('prod_codigo');
    var PedidoDetalle = {
        prod_Codigo: prod_Codigo,
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