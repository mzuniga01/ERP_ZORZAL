$('#Inactivar').click(function () {
    var prod_Codigo = $('#prod_Codigo').val();
    var Activo = 0
    var Razon_Inactivacion = $('#razonInac').val();
    console.log(prod_Codigo)
    console.log(Activo)
    console.log(Razon_Inactivacion)
    console.log(prod_UsuarioModifica)
    if (Razon_Inactivacion == "") {
        valido = document.getElementById('Mensaje');
        valido.innerText = "La razón inactivación es requerida";
    }
    else {
        $.ajax({
            url: "/Producto/EstadoInactivar",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ prod_Codigo: prod_Codigo, Activo: Activo, Razon_Inactivacion: Razon_Inactivacion}),

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
    var prod_Codigo = $('#prod_Codigo').val();
    var Activo = 1
    var Razon_Inactivacion = null;
    $.ajax({
        url: "/Producto/Estadoactivar",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ prod_Codigo: prod_Codigo, Activo: Activo, Razon_Inactivacion: Razon_Inactivacion }),

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

function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}


