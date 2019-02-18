
    function CuponEsImpreso() {
        var cdtoId = $('#cdto_ID').val();
        var EsImpreso = 1
    $.ajax({
        url: "/CuponDescuento/CuponEsImpreso",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ cdtoId: cdtoId, EsImpreso: EsImpreso }),
    })

    .done(function (data) {
        if (data.length > 0) {
            var url = $("#RedirectTo").val();
            console.log(url)
            location.href = url;
        }
        else {
            alert("No Impreso");
        }
    });
}
