
//$(document).ready(function () {
//    $("#soled_CantidadSolicitada")[0].maxLength = 7;
//    $("#ValorDeno")[0].maxLength = 12;
//})
 
//$(function () {
//    $("#soled_CantidadSolicitada,#idinput").keyup(function (e) {
//        var Cantidad = $("#soled_CantidadSolicitada").val(),
//            valores = $("#ValorDeno").val(),
//            result = "";
//        if (Cantidad.length && valores.length > 0) {
//            result += Cantidad * valores;
//        }
//        $("#MontoSolicitado").val(result);
//    });
//});


/////GET CAJA DE DEVOLUCION 

$(document).ready(function () {
    var CodUsuario = $("#usu_Id").val();
    console.log(CodUsuario, 'consolelog')
    $.ajax({
        url: "/MovimientoCaja/GetCaja",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodUsuario: CodUsuario }),
    })
    .done(function (data) {
        if (data.length > 0) {
            $.each(data, function (key, val) {
                $("#cja_Id").val(val.cja_Id);
                $("#tbCaja_cja_Descripcion").val(val.cja_Descripcion);
            });
        }
    });
});

