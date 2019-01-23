var contador = 0;
$('#mnda_Id').change(function () {
    var moneda = $('#mnda_Id').val();
    console.log('moneda', moneda);

    var SolicitudEfectivoMoneda = GetSolicitudEfectivoMoneda();
    $.ajax({
        url: "/MovimientoCaja/SaveSolicitudEfectivoMoneda",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ SolicitudEfectivoMon: SolicitudEfectivoMoneda }),
    })
    .done(function (data) {
        ////Limpiar mensajes
        //$('#Errorcajacreate').text('');

        //Limpiar 
        //$('#mnda_Id').val('');
    });
});



///////////////////////////////////////////////////

function GetSolicitudEfectivoMoneda() {
    var solicitudefectivo = {
        mnda_Id: $('#mnda_Id').val(),
        solef_Id: contador
    }
    return solicitudefectivo
};

