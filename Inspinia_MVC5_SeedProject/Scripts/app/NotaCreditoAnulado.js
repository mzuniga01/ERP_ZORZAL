function AnularNotaCredito() {
    var RazonAnular = $('#nocre_RazonAnulado').val();
    var nocreId = $('#nocre_Id').val();
    var Anulado = 1
    if (RazonAnular == "") {
        valido = document.getElementById('smsRazonAnular');
        valido.innerText = "La razón anulación es requerida";
    } else {
        $.ajax({
            url: "/NotaCredito/AnularNotaCredito",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ nocreId: nocreId, Anulado: Anulado, RazonAnular: RazonAnular }),
        })

    .done(function (data) {
        if (data.length > 0) {
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
