$(document).ready(function () {
    var Estado = $('#esfac_Id').val();
    if (Estado == 4) {
        $('#bottonAnular').hide();
        document.getElementById("esfac_Id").disabled = true;
    }
});

function AnularFactura() {
    var CodFactura = $('#fact_Id').val();
    var Estado = 4
    $.ajax({
        url: "/Factura/AnularFactura",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodFactura: CodFactura, Estado: Estado }),

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



