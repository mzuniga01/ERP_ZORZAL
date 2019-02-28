/////GET CAJA DE NOTA CREDITO
$(document).ready(function () {
    GetCaja();
})


function GetCaja() {
    var CodUsuario = $("#usu_Id").val();
    console.log(CodUsuario)
    $.ajax({
        url: "/NotaCredito/GetCaja",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodUsuario: CodUsuario }),
    })
    .done(function (data) {
        if (data.length > 0) {
            $.each(data, function (key, val) {
                $("#cja_Id").val(val.cja_Id);
                $("#cja_Descripcion").val(val.cja_Descripcion);

                var CodSucursal = $("#suc_Id").val();
                var CodCaja = $("#cja_Id").val();
                console.log('CodCaja', CodCaja)
                console.log("holakeyla", CodSucursal, "holakeylaaaaaa", CodCaja, "joshuaaa")
                GetCodigoNotaCredito(CodSucursal, CodCaja)
                GetCodigoNC(CodSucursal, CodCaja)
            });
        }
    });
}




function GetCodigoNotaCredito(CodSucursal, CodCaja) {
    $.ajax({
        url: "/NotaCredito/GetCodigoNotaCredito",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodSucursal: CodSucursal, CodCaja: CodCaja }),
    })
    .done(function (data) {
        if (data.length > 0) {
            var Mensaje = data;
            console.log(Mensaje)

            if (Mensaje == -1) {
                alert("Fecha limite de Emisión Vencida")
                var url = $("#RedirectTo").val();
                location.href = url;
            }else if (Mensaje == -2) {
                alert("El Numero CAI no tiene numeración")
                var url = $("#RedirectTo").val();
                location.href = url;
            }
            else {
                $('#nocre_Codigo').val(data[0]['CODNOTACREDITO']);
                console.log('#nocre_Codigo')
            }
        }
        else {

        }
    });
}

function GetCodigoNC(CodSucursal, CodCaja) {
    $.ajax({
        url: "/Devolucion/GetCodigoNotaCredito",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodSucursal: CodSucursal, CodCaja: CodCaja }),
    })
    .done(function (data) {
        if (data.length > 0) {
            var Mensaje = data;
            console.log(Mensaje)

            if (Mensaje == -1) {
                alert("Fecha limite de Emisión Vencida")
                var url = $("#RedirectTo").val();
                location.href = url;
            } else if (Mensaje == -2) {
                alert("El Numero CAI no tiene numeración")
                var url = $("#RedirectTo").val();
                location.href = url;
            }
            else {
                $('#nocre_Codigo').val(data[0]['CODNOTACREDITO']);
                console.log('#nocre_Codigo')
            }
        }
        else {

        }
    });
}
