
$(document).ready(function () {
    $("#soled_CantidadSolicitada")[0].maxLength = 7;
    $("#ValorDeno")[0].maxLength = 12;
})
 
$(function () {
    $("#soled_CantidadSolicitada,#idinput").keyup(function (e) {
        var Cantidad = $("#soled_CantidadSolicitada").val(),
            valores = $("#ValorDeno").val(),
            result = "";
        if (Cantidad.length && valores.length > 0) {
            result += Cantidad * valores;
        }
        $("#MontoSolicitado").val(result);
    });
});
