function EditListadoPrecioDetalle(lispd_Id) {
    $.ajax({
        url: "/ListaPrecios/getListadoPrecioDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ lispd_Id: lispd_Id }),

    })
    .done(function (data) {
        if (data.length > 0) {
            $.each(data, function (i, item) {
                $("#lispd_Id_lisd").val(item.lispd_Id);
                $("#prod_Codigo_lisd").val(item.prod_Codigo);
                $("#lispd_PrecioMayorista_lisd").val(item.lispd_PrecioMayorista);
                $("#lispd_PrecioMinorista_lisd").val(item.lispd_PrecioMinorista);
                $("#lispd_DescCaja_lisd").val(item.lispd_DescCaja);
                $("#lispd_DescGerente_lisd").val(item.lispd_DescGerente);

                $("#lispd_UsuarioCrea_lisd").val(item.lispd_UsuarioCrea);
                $("#lispd_FechaCrea_lisd").val(item.lispd_FechaCrea);
                $("#EditPedidoDetalle").modal();
            })
        }
    })
}


$("#BtnsubmitMunicipio").click(function () {
    var lispd_Ids = $('#lispd_Id').val();
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "Post",
        url: "/ListaPrecios/UpdateListadoPrecioDetalle",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
            else
                window.location.href = '/ListaPrecios/Edit/' + lispd_Ids;
        }
    });

    location.reload(true);
})

