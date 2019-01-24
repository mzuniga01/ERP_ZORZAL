$(document).on("change", "#DenominacionDetalle tbody tr td input#name", function () {

    var Cantidad = $(this).val();
    //var denoID = $('#deno_Id').text();
    var denoID = $(this).parents("tr").find("td")[4].innerHTML;
    console.log('denoID', denoID);
    var cantidadsolicitada = $(this).val();
    var cantidadentregada = $(this).val();
    var Total = $(this).parents("tr").find("td")[3].innerHTML;
    console.log('cantidadsolicitada', cantidadsolicitada);
    console.log('cantidadentregada', cantidadentregada);
    console.log('Total', Total);
    console.log('----------------------------');


    if (Cantidad == 0) {
        $('#ErrorDocumentoFiscalCreate').text('');   
    }
    else {
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
            $('#DenominacionCreate').val();
            $('#name').val();
            $('#SuntotalCreate').val();
        });


        function GetSolicitudEfectivo() {
            var solicitudefectivodetalle = {
                deno_Id: denoID,
                soled_CantidadSolicitada: cantidadsolicitada,
                soled_CantidadEntregada: cantidadentregada,
                soled_MontoEntregado: Total,
                soled_Id: contador
            }
            return solicitudefectivodetalle
        };

    }
 
});