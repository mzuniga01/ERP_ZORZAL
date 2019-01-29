$(document).on("change", "#EntregaEfectivo tbody tr td input#CantidadE", function () {
    var IdSolicitud = $(this).parents("tr").find("td")[0].innerHTML;
    console.log("IdSolicitud: ", IdSolicitud);

    var IdSolicitudDetalle = $(this).parents("tr").find("td")[1].innerHTML;
    console.log("IdSolicitudDetalle:", IdSolicitudDetalle);

    var CantidadSolicitada = $(this).parents("tr").find("td")[4].innerHTML;
    console.log("CantidadSolicitada:", CantidadSolicitada);

    var CantidadEntregada = $(this).val();
    console.log('CantidadEntregada: ', CantidadEntregada);

    var Monto = $(this).parents("tr").find("td")[6].innerHTML;
    console.log('Monto Entregado:', Monto);

    var denoID = $(this).parents("tr").find("td")[7].innerHTML;
    console.log('denoID: ', denoID);

    var UsuarioCrea = $(this).parents("tr").find("td")[8].innerHTML;
    console.log('UsuarioCrea: ', UsuarioCrea);

    var FechaCrea = $(this).parents("tr").find("td")[9].innerHTML;
    console.log('FechaCrea: ', FechaCrea);

    console.log('----------------------------');

    var SolicitudEfectivoDetalle = GetEntregaEfectivoDetalle();

    $.ajax({
        url: "/SolicitudEfectivo/SaveEditSolicitudEfectivoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ tbSolicitudEfectivoDetalle: SolicitudEfectivoDetalle }),
        success: function (data) {
        }
    })
    .done(function (data) {
        if (data == 'El registro se guardó exitosamente') {
            swal("El registro se editó exitosamente!", "", "success");
        }
        else {
            swal("El registro  no se editó!", "", "error");
        }
    });

    function GetEntregaEfectivoDetalle() {
        var EntregaEfectivoDetalle = {
            soled_Id: IdSolicitudDetalle,
            deno_Id: denoID,
            soled_CantidadSolicitada: CantidadSolicitada,
            soled_CantidadEntregada: CantidadEntregada,
            soled_MontoEntregado: Monto,
            soled_UsuarioCrea: UsuarioCrea,
            soled_FechaCrea: FechaCrea

        }
        return EntregaEfectivoDetalle
    };

});