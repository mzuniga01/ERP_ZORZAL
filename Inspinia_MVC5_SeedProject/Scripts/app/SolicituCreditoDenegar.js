function AnularSolictud() {
    var credID = $('#cred_Id').val();
    var Denegado = 3
    $.ajax({
        url: "/SolicitudCredito/DenegarSolCredito",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ credID: credID, Denegado: Denegado }),

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