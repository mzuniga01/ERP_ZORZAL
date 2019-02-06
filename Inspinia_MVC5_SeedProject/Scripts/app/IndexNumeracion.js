$(document).ready(function () {
    //Current Date
    var d = new Date();
    $("#PuntoEmision tbody tr").each(function () {
        //Id PuntoEmisionDetalle
        var ID = $(this).children("td:eq(0)").text();

        var RangoFinal = $(this).children("td:eq(5)").text();
        var DivisionesRangoFinal = RangoFinal.split("-", 4);
        var UltimoNumero = DivisionesRangoFinal[3]
        var RangoFinalCasteado = parseInt(UltimoNumero)

        var NumeroActual = $(this).children("td:eq(6)").text();
        var NumeroActualCasteado = parseInt(NumeroActual)

        //FechaLimiteEmision
        var FechaLimiteEmsion = $(this).children("td:eq(7)").text();
        var i = new Date(FechaLimiteEmsion);

        if (i <= d) {
            $("#btnModalEditarEdit_" + parseInt(ID)).prop("disabled", true);
            $("#Circulos_" + parseInt(ID)).removeClass().addClass("red");
        }
        else if (NumeroActualCasteado == RangoFinalCasteado) {
            $("#btnModalEditarEdit_" + parseInt(ID)).prop("disabled", true);
            $("#Circulos_" + parseInt(ID)).removeClass().addClass("red");
        }
        else {
            $("#btnModalEditarEdit_" + parseInt(ID)).prop("disabled", false);
            $("#Circulos_" + parseInt(ID)).removeClass().addClass("green");
        }
    });
});