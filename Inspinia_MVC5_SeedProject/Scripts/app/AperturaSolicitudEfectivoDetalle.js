var contador = 0;
$('#guardar').click(function () {
    //var cajaid = $('#cja_Id').val();
    //////////////////////////////////////
    var denoID = $('#deno_Id').text();
    var cantidadsolicitada = $('#name').val();
    var cantidadentregada = $('#name').val();
    var Total = $('#SuntotalCreate').text();
    console.log('cantidadsolicitada', cantidadsolicitada);
    console.log('cantidadentregada', cantidadentregada);
    console.log('Total', Total);
    console.log('denoID', denoID);


        var SolicitudEfectivo = GetSolicitudEfectivo();
        $.ajax({
            url: "/MovimientoCaja/SaveSolicitudEfectivoDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ SolicitudEfectivoDet: SolicitudEfectivo }),
        })
        .done(function (data) {
            //Limpiar mensajes
            $('#Errorcajacreate').text('');

            //Limpiar input
            //$('#cja_Id').val('');
            ////////////////////////////
            $('#DenominacionCreate').val();
            $('#name').val();
            $('#SuntotalCreate').val();
        });
    });


///////////////////////////////////////////////////

function GetSolicitudEfectivo() {
    var solicitudefectivodetalle = {
        deno_Id: $(this).parents("tr").find("td")[4].innerHTML,
        soled_CantidadSolicitada: $('#name').val(),
        soled_CantidadEntregada: $('#name').val(),
        soled_MontoEntregado: $('#Monto').val(),
        soled_Id: contador
    }
    return solicitudefectivodetalle
};



