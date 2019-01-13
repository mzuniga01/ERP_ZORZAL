
function AnularNotaCredito() {
    var nocreId = $('#nocre_Id').val();
    var Anulada = 1
    console.log('hota');
    $.ajax({
        url: "/NotaCredito/AnularNotaCredito",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ nocreId: nocreId, Anulada: Anulada }),
       
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
