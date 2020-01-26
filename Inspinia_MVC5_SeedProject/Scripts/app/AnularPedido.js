function AnularPedido() {
    var CodPedido = $('#ped_Id').val();
    var NoAnulado = 1
    var RazonAnulado = $('#razonInac').val();

    if (RazonAnulado == "") {
        var valido = document.getElementById('Mensaje');
        valido.innerText = "La razón anulación es requerida";
    }
    else {
        $.ajax({
            url: "/Pedido/AnularPedido",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodPedido: CodPedido, NoAnulado: NoAnulado, RazonAnulado: RazonAnulado }),

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
}