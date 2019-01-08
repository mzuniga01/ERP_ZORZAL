$(document).ready(function () {
    var Anular = $('#solef_EsAnulada').val();
    document.getElementById("solef_EsAnulada").disabled = false;

    if (Anular == 1) {
        $('#bottonAnular').hide();
       
    }
});

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
            var url = $("#RedirectTo").val();
            location.href = url;
        }
        else {
            alert("Registro No Actualizado");
        }
    });
}
