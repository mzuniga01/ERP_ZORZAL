$('#Inactivar').click(function () {
    var usu_id = $('#usu_id').val();
    var Activo = 0
    var Razon_Inactivacion = $('#razonInac').val();
    console.log(usu_id)
    console.log(Activo)
    console.log(Razon_Inactivacion)
    if (Razon_Inactivacion == "") {
        valido = document.getElementById('Mensaje');
        valido.innerText = "La razón inactivación es requerida";
    }

    else {
        $.ajax({
            url: "/Usuario/EstadoInactivar",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ usu_id: usu_id, Activo: Activo, Razon_Inactivacion: Razon_Inactivacion }),

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

$('#Activar').click(function () {
    var usu_id = $('#usu_id').val();
    var Activo = 1
    var Razon_Inactivacion = null;
    $.ajax({
        url: "/Usuario/EstadoInactivar",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ usu_id: usu_id, Activo: Activo, Razon_Inactivacion: Razon_Inactivacion }),

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