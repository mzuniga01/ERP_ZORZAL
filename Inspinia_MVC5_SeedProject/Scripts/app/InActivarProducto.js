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

//$('#Activar').click(function () {
//    var prod_Codigo = $('#prod_Codigo').val();
//    var Activo = 1
//    var Razon_Inactivacion = null;
//    $.ajax({
//        url: "/Producto/Estadoactivar",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ prod_Codigo: prod_Codigo, Activo: Activo, Razon_Inactivacion: Razon_Inactivacion }),

//    })
//    .done(function (data) {
//        if (data.length > 0) {
//            var url = $("#RedirectTo").val();
//            location.href = url;
//        }
//        else {
//            alert("Registro No Actualizado");
//        }
//    });
//})

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

function EditStudentRecord (prod_Codigo) {


    $("#MsjError").text("");

    $.ajax({
        url: "/Producto/GetProducto",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ prod_Codigo }),
    })
    .done(function (data) {
        $.each(data, function (m, Model) {
            $("#prod_Codigo_Edit").val(Model.prod_Codigo);
            $("prod_EsActivo_Edit").val(Model.prod_EsActivo);
            $("#prod_Razon_Inactivacion_Edit").val(Model.prod_Razon_Inactivacion);

            $("#MyModal").modal();

        })
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        console.log('jqXHR', jqXHR);
        console.log('textStatus', textStatus);
        console.log('errorThrown', errorThrown);
    })
}
$('#Btnsubmit').click(function () {
    //var data = $("#SubmitForm").serializeArray();
    var prod_Codigo = $('#prod_Codigo').val();
    var prod_EsActivo = 0
    var prod_Razon_Inactivacion = $('#razonInac').val();
    var prod_UsuarioModifica = $('#prod_UsuarioModifica_Edit').val();
    var prod_FechaModifica = $('#prod_FechaModifica_Edit').val();
    console.log(prod_Codigo)
    console.log(prod_EsActivo)
    console.log(prod_Razon_Inactivacion)
    console.log(prod_UsuarioModifica)
    if (prod_Razon_Inactivacion == "") {
        valido = document.getElementById('Mensaje');
        valido.innerText = "La razón inactivación es requerida";
    }
    else {
        $.ajax({
            url: "/Producto/EstadoInactivar",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ prod_Codigo: prod_Codigo, prod_EsActivo: prod_EsActivo, prod_Razon_Inactivacion: prod_Razon_Inactivacion, prod_UsuarioModifica: prod_UsuarioModifica, prod_FechaModifica: prod_FechaModifica }),

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


function RazonSalida(prod_Codigo) {


    $("#MsjError").text("");

    $.ajax({
        url: "/Producto/GetEmpleadoRazon",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ prod_Codigo }),
    })
    .done(function (data) {
        $.each(data, function (m, Model) {
            $("#prod_Codigo_Edit_Razon").val(Model.prod_Codigo);
            $("prod_EsActivo_Edit_Razon").val(Model.prod_EsActivo);
            $("#prod_Razon_Inactivacion_Edit_Razon").val(Model.prod_Razon_Inactivacion);

            $("#Editarmodales").modal();

        })
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        console.log('jqXHR', jqXHR);
        console.log('textStatus', textStatus);
        console.log('errorThrown', errorThrown);
    })
}

$('#BtnRazon').click(function () {
    //var data = $("#SubmitForm").serializeArray();
    var prod_Codigo = $('#prod_Codigo_Edit_Razon').val();
    var prod_EsActivo = 0
    var prod_RazonInactivacion = $('#prod_Razon_Inactivacion_Edit_Razon').val();
    var prod_UsuarioModifica = $('#prod_UsuarioModifica_Edit').val();
    console.log(prod_Codigo)
    console.log(prod_Estado)
    console.log(prod_RazonSalida)
    if (prod_RazonSalida == "") {
        valido = document.getElementById('ErrorMessage');
        valido.innerText = "La razón Inactivacion es requerida";
    }
    else {
        $.ajax({
            url: "/Producto/RazonSalida",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ prod_Codigo: prod_Codigo, prod_EsActivo: prod_EsActivo, prod_RazonInactivacion: prod_RazonInactivacion }),

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

