$('#Inactivar').click(function () {
    var CodLp = $('#listp_Id').val();
    var Activo = 0
   
    console.log(CodLp)
    console.log(Activo)
   
   
    $.ajax({
        url: "/ListaPrecios/InactivarListaPrecio",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodLp: CodLp, Activo: Activo})

        

  
    } )
    .done(function (data) {
        if (data.length > 0) {
            var url = $("#RedirectTo").val();
            location.href = url;
        }
        else {
            alert("Registro No Actualizado");
        }
    });
})


    $('#Activar').click(function () {
        var CodLp = $('#listp_Id').val();
        var Activo = 1
   
        console.log(CodLp)
        console.log(Activo)
  
        $.ajax({
            url: "/ListaPrecios/ActivarListaPrecio",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodLp: CodLp, Activo: Activo })

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

    })








