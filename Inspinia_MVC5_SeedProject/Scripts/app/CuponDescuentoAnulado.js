
    function AnularCuponDescuento() {
        var cdtoId = $('#cdto_ID').val();
        var Anulada = 1
    $.ajax({
        url: "/CuponDescuento/AnularCuponDescuento",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ cdtoId: cdtoId, Anulada: Anulada }),
    })

    .done(function (data) {
        if (data.length > 0) {
            var url = $("#RedirectTo").val();
            location.href = url;
        }
        else {
            alert("Registro No Anulado");
        }
    });
}
