$("#btnGuardarEntregaEfectivo").click(function () {
    var IdSolicitud = $('#solef_Id').val();
    console.log("IdSolicitud:", IdSolicitud);

    $("#EntregaEfectivo tbody tr ").each(function (index) {
        var IdSolicitudDetalle = $(this).children("td:eq(1)").text();
        console.log("IdSolicitudDetalle:", IdSolicitudDetalle);

        //var cantidad = $(this).parents("tr").find('#CantidadE').val();
        //var cantidad = $("input[name*='CantidadE']").val();
        //var cantidad = $('.CantidadE').val();
        //console.log("Cantidad:", cantidad);

        var Monto = $(this).children("td:eq(6)").html();
        console.log('Monto:', Monto);
    });

    $("#EntregaEfectivo tbody tr td input#CantidadE").each(function (index) {
        //$("#EntregaEfectivo tbody tr ").find(':input').each(function () {
        var Cantidad = $(this).val();
        console.log("Cantidad:", Cantidad);
    });

    var GetEfectivoDetalle = {
        solef_Id: IdSolicitud,
        soled_Id: IdSolicitudDetalle,
        soled_CantidadEntregada: Cantidad,
        soled_MontoEntregado: Monto
    };

    $.ajax({
        url: "/SolicitudEfectivo/SaveEditSolicitudEfectivoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ EdiEntregaEfectivoDetalle: GetEfectivoDetalle }),
        success: function (data) {
        }
    })
    .done(function (data) {
        if (data == 'El registro se guardó exitosamente') {
            location.reload();
            swal("El registro se editó exitosamente!", "", "success");
        }
        else {
            location.reload();
            swal("El registro  no se editó!", "", "error");
        }
    });
});