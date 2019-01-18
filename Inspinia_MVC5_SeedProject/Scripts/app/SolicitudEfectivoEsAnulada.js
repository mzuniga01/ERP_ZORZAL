
    function AnularSolictud() {
    var solefId = $('#solef_Id').val();
    var Anulada = 1
    $.ajax({
        url: "/SolicitudEfectivo/AnularSolcitudEfectivo",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ solefId: solefId, Anulada: Anulada }),

    })
    .done(function (data) {
        if (data.length > 0) {
            window.location.href = "/SolicitudEfectivo/Index";
        }
        else {
            alert("Registro No Actualizado");
        }
    });
}
