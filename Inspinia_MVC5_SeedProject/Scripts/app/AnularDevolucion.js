function AnularDevolucion() {
   
    var CodDevolucion = $('#dev_Id').val();
    var Estado = 1
    $.ajax({
        url: "/Devolucion/AnularDevolucion",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodDevolucion: CodDevolucion, Estado: Estado }),
        
    })
    .done(function (data) {
        console.log('anularEntra')
        if (data.length > 0) {
            var url = $("#Redireccionar").val();
            console.log(url)
            location.href = url;
            //alert("Registro No Anulado");
        }
        else {
            console.log('Redireccionar1')
            var url = $("#Redireccionar").val();
            console.log(url)
            location.href = url;
        }
    });
}

$(document).ready(function () {
    var DevEstado = $('#dev_Estado').val();
    console.log(DevEstado)
    if (DevEstado === 'Anulado') {
        document.getElementById("bottonAnular").disabled = true;
        document.getElementById("Detalle").disabled = true;
        document.getElementById("bottonNotaCredito").disabled = true;
        document.getElementById("Producto").disabled = true;
        document.getElementById("Guardar").disabled = true;
    }

   
});