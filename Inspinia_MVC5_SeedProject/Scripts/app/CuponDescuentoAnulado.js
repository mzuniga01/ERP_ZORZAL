function AnularCuponDescuento() {
    var RazonAnular = $('#cdto_RazonAnulado').val();
    var cdtoId = $('#cdto_ID').val();
    var Anulada = 1
    if (RazonAnular == "") {
        valido = document.getElementById('smsRazonAnular1');
        valido.innerText = "La razón anulación es requerida";
    }
    else {
        $.ajax({
            url: "/CuponDescuento/AnularCuponDescuento",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ cdtoId: cdtoId, Anulada: Anulada, RazonAnular: RazonAnular }),
        })
        .done(function (data) {
            if (data = true) {
                var url = $("#RedirectTo").val();
                location.href = url;
                valido.innerText = "";
            }
            else {
                alert("Registro No Anulado");
            }
        });
    }
}