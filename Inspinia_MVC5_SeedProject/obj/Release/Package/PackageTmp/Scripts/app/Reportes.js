$("#obj_Id").click(function () {
    var expression = parseInt($("#obj_Id").val());
    switch (expression) {
        case 195:
            $("#FiltrosReporteCajaFechas").hide();
            $("#FiltrosReporteFacturasPendientes").hide();
            $("#FiltrosReporteNotaCredito").hide();
            $("#FiltrosReporteVentasEntreFechas").show();
            break;
        case 196:
            $("#FiltrosReporteVentasEntreFechas").hide();
            $("#FiltrosReporteFacturasPendientes").hide();
            $("#FiltrosReporteNotaCredito").hide();
            $("#FiltrosReporteCajaFechas").show();
            break;
        case 197:
            $("#FiltrosReporteCajaFechas").hide();
            $("#FiltrosReporteVentasEntreFechas").hide();
            $("#FiltrosReporteNotaCredito").hide();
            $("#FiltrosReporteFacturasPendientes").show();
            break;
        case 198:
            $("#FiltrosReporteCajaFechas").hide();
            $("#FiltrosReporteVentasEntreFechas").hide();
            $("#FiltrosReporteFacturasPendientes").hide();
            $("#FiltrosReporteNotaCredito").hide();
            break;
        case 199:
            $("#FiltrosReporteCajaFechas").hide();
            $("#FiltrosReporteVentasEntreFechas").hide();
            $("#FiltrosReporteFacturasPendientes").hide();
            $("#FiltrosReporteNotaCredito").show();
            break;
        case 200:
            $("#FiltrosReporteCajaFechas").hide();
            $("#FiltrosReporteVentasEntreFechas").hide();
            $("#FiltrosReporteFacturasPendientes").show();
            $("#FiltrosReporteNotaCredito").hide();
            break;
        case 202:
            $("#FiltrosReporteCajaFechas").hide();
            $("#FiltrosReporteVentasEntreFechas").hide();
            $("#FiltrosReporteFacturasPendientes").hide();
            $("#FiltrosReporteNotaCredito").show();
            break;
        case 205:
            $("#FiltrosReporteCajaFechas").hide();
            $("#FiltrosReporteVentasEntreFechas").hide();
            $("#FiltrosReporteFacturasPendientes").hide();
            $("#FiltrosReporteNotaCredito").hide();
            break;
        default:
            // code block
    }
});

$("#suc_Id").change(function () {
    var suc_Id = $('#suc_Id').val();
    if (suc_Id != 0 && suc_Id != null)
    {
        $.ajax({
            url: "/Reportes/GetCajas",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ suc_Id: suc_Id }),
        })
     .done(function (data) {
         if (data.length > 0) {
             $('#cja_Id').empty();
             $('#cja_Id').append("<option value=''>Seleccione la Caja</option>");
             $.each(data, function (key, val) {
                 $('#cja_Id').append("<option value=" + val.cja_Id + ">" + val.cja_Descripcion + "</option>");
             });
             $('#cja_Id').trigger("chosen:updated");
         }
     });
    }
    else
    {
        $('#cja_Id').empty();
        $('#cja_Id').append("<option value=''>Seleccione la Caja</option>");
    }
});



