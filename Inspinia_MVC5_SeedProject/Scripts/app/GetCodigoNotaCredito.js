$(document).ready(function () {
    var Suc = $("#suc_Id").val();
    GetCodigoNotaCredito(Suc, 4)
    $("#cja_Id").val(4);
})
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