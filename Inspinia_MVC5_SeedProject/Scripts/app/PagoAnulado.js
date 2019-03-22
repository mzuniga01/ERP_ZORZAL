
$('#Anular').click(function () {
    var pago_Id = $('#pago_Id').val();
    var PagoAnulado = 1
    var RazonAnulado = $('#razonAnular').val();
   // var razon =$("#pago_RazonAnulado").val(RazonAnulado);
    if (RazonAnulado == "") {
        valido = document.getElementById('Mensaje');
        valido.innerText = "La razón Anular es requerida";
    } else {
        $.ajax({
            url: "/Pago/AnularPago",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ pago_Id: pago_Id, PagoAnulado: PagoAnulado, RazonAnulado: RazonAnulado }),

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

})
