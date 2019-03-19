
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




$(document).ready(function () {
 
    /////GET CAJA DE DEVOLUCION 
    var CodUsuario = $("#usu_Id").val();
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

    ///GET USUARIO APERTURA --------------------
    var CodUsuario = $("#usu_Id").val();
    $.ajax({
        url: "/MovimientoCaja/UsuarioApertura",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodUsuario: CodUsuario }),
    })
    .done(function (list) {
        if (list.length > 0) {
            $.each(list, function (key, val) {

                var fechaString = val.mocja_FechaApertura.substr(6);
                var fechaActual = new Date(parseInt(fechaString));
                var mes = fechaActual.getMonth();
                var dia = fechaActual.getDate();
                var anio = fechaActual.getFullYear();
                var FechaApertura = dia + "/" + mes + "/" + anio;

                $("#mocja_UsuarioApertura").val(val.mocja_UsuarioApertura);
                $("#UsuarioApertura").val(val.Nombres);
                $("#mocja_FechaApertura").val(val.mocja_FechaApertura);
                $("#mocja_UsuarioAceptacion").val(val.Nombres);
            });
        }
    });
});
