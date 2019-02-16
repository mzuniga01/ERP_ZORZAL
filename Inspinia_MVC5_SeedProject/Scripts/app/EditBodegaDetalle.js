$("#BtnsubmitMunicipio").click(function () {
    var bodd_Ids = $('#bodd_Id').val();
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "Post",
        url: "/Bodega/UpdatePedidoDetalle",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
            else
                window.location.href = '/Bodega/Edit/' + bodd_Ids;
        }
    });

    location.reload(true);
})


function EditPedidoDetalles(pedd_Id) {
    $.ajax({
        url: "/Pedido/getPedidoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ pedd_Id: pedd_Id }),

    })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#pedd_Id_Ped").val(item.pedd_Id);
                    $("#prod_Codigo_Ped").val(item.prod_Codigo);
                    $("#pedd_Cantidad_Ped").val(item.pedd_Cantidad);


                    $("#pedd_UsuarioCrea_Ped").val(item.pedd_UsuarioCrea);
                    $("#pedd_FechaCrea_Ped").val(item.pedd_FechaCrea);
                    $("#EditPedidoDetalle").modal();
                })
            }
        })
}