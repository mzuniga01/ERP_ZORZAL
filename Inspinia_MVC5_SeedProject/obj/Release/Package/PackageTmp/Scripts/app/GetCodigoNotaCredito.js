$(document).ready(function () {
    GetCodigoNotaCredito(7, 8)
    $("#suc_Id").val(7);
    $("#cja_Id").val(8);
})
function GetCodigoNotaCredito(CodSucursal, CodCaja) {
    //var CodSucursal = $('#suc_Id').val();
    //var CodCaja = $('#cja_Id').val();   

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
                alert("El Numero CAI no tiene numeracion")
                var url = $("#RedirectTo").val();
                location.href = url;
            }
            else {
                $('#nocre_Codigo').val(data[0]['CODNOTACREDITO']);
                console.log('#nocre_Codigo')
            }
            //$.each(data, function (key, val) {

            //});
            //console.log(data)
        }
        else {

        }
    });
}