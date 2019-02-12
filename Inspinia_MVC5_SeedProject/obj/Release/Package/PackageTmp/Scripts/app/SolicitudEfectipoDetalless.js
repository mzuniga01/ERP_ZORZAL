var contador = 0;

$('#AgregarDetalle').click(function () {

    //alert('csacsacsac');
    //var select = $("#deno_Id option:selected").text();
    //var valor = $("#deno_Id").val();
    //$("#selector").val(select)
    //$("#valor").val(valor)

    //$("#deno_Id option:selected").text();
    var moneda = $("#mnda_Id").val();
   
    var IdDenos = $("#deno_Id option:selected").text();    
    var IdDeno = $("#deno_Id").val();
    var SDCantidadSolicitada = $('#soled_CantidadSolicitada').val();
    var SDMontoSolicitado = $('#MontoSolicitado').val();
    var SDCantidadEntregada = $('#soled_CantidadEntregada').val();
    var SDMontoEntregado = $('#soled_MontoEntregado').val();

    if (moneda == 0) {
        $('#ErrornMonedaCreate').text('');
        $('#ErrorIdDenoCreate').text('');
        $('#ErrorSDCantidadSolicitadaCreate').text('');
        $('#ErrorSDCantidadEntregadaCreate').text('');
        $('#ErrorSDMontoEntregadoCreate').text('');
        $('#validationMonedaCreate').after('<ul id="ErrorIdDenoCreate" class="validation-summary-errors text-danger">Campo Moneda es requerido</ul>');
    }

    else if (IdDeno == 0) {
        $('#ErrornMonedaCreate').text('');
        $('#ErrorIdDenoCreate').text('');
        $('#ErrorSDCantidadSolicitadaCreate').text('');
        $('#ErrorSDCantidadEntregadaCreate').text('');
        $('#ErrorSDMontoEntregadoCreate').text('');
        $('#validationDenoIdCreate').after('<ul id="ErrorIdDenoCreate" class="validation-summary-errors text-danger">Campo Denominación es requerido</ul>');
    }
    //else if (IdDeno == '0') {
    //    $('#ErrorIdDenoCreate').text('');
    //    $('#ErrorSDCantidadSolicitadaCreate').text('');
    //    $('#ErrorSDCantidadEntregadaCreate').text('');
    //    $('#ErrorSDMontoEntregadoCreate').text('');
    //    $('#validationDenoIdCreate').after('<ul id="ErrorIdDenoCreate" class="validation-summary-errors text-danger">Campo Denominación es requerido</ul>');
    //}


    else if (SDCantidadSolicitada == '') {
        $('#ErrornMonedaCreate').text('');
        $('#ErrorIdDenoCreate').text('');
        $('#ErrorSDCantidadSolicitadaCreate').text('');
        $('#ErrorSDCantidadEntregadaCreate').text('');
        $('#ErrorSDMontoEntregadoCreate').text('');
        $('#validationCantidadSolicitadaCreate').after('<ul id="ErrorSDCantidadSolicitadaCreate" class="validation-summary-errors text-danger">Campo Cantidad Solicitada es requerido</ul>');
    }
    else if (SDCantidadSolicitada == '0') {
        $('#ErrornMonedaCreate').text('');
        $('#ErrorIdDenoCreate').text('');
        $('#ErrorSDCantidadSolicitadaCreate').text('');
        $('#ErrorSDCantidadEntregadaCreate').text('');
        $('#ErrorSDMontoEntregadoCreate').text('');
        $('#validationCantidadSolicitadaCreate').after('<ul id="ErrorSDCantidadSolicitadaCreate" class="validation-summary-errors text-danger">Campo Cantidad Solicitada no puede ser Cero.</ul>');
    }

        //else if (SDMontoSolicitado == '') {
        //    $('#ErrorIdDenoCreate').text('');
        //    $('#ErrorSDCantidadSolicitadaCreate').text('');
        //    $('#ErrorSDCantidadEntregadaCreate').text('');
        //    $('#ErrorSDMontoEntregadoCreate').text('');
        //    $('#validationMontosSolicitadoCreate').after('<ul id="ErrorSDMontoSolicitadoCreate" class="validation-summary-errors text-danger">Campo Monto Solicitado es requerido</ul>');
        //}

    else if (SDCantidadEntregada == '') {
        $('#ErrornMonedaCreate').text('');
        $('#ErrorIdDenoCreate').text('');
        $('#ErrorSDCantidadSolicitadaCreate').text('');
        $('#ErrorSDCantidadEntregadaCreate').text('');
        $('#ErrorSDMontoEntregadoCreate').text('');
        $('#validationCantidadEntregadaCreate').after('<ul id="ErrorSDCantidadEntregadaCreate" class="validation-summary-errors text-danger">Campo Cantidad Entregada es requerido</ul>');
    }
    else if (SDMontoEntregado == '') {
        $('#ErrornMonedaCreate').text('');
        $('#ErrorIdDenoCreate').text('');
        $('#ErrorSDCantidadSolicitadaCreate').text('');
        $('#ErrorSDCantidadEntregadaCreate').text('');
        $('#ErrorSDMontoEntregadoCreate').text('');
        $('#validationMontoEntregadoCreate').after('<ul id="ErrorSDMontoEntregadoCreate" class="validation-summary-errors text-danger">Campo Monto Entregado es requerido</ul>');
    }

    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'IdDenoCreate'>" + IdDenos + "</td>";
        copiar += "<td id = 'SDCantidadSolicitadaCreate'>" + SDCantidadSolicitada + "</td>";
        copiar += "<td id = 'SDMontoSolicitadoCreate'>" + SDMontoSolicitado + "</td>";
        copiar += "<td id = 'SDCantidadEntregadaCreate'>" + SDCantidadEntregada + "</td>";
        copiar += "<td id = 'SDMontoEntregadoCreate'>" + SDMontoEntregado + "</td>";
        copiar += "<td>" + '<button id="removeSolicitudEfectivoDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblSolicitudEfectivoDetalle').append(copiar);

        var SolicitudDetalle = GetSolicitudDetalle();

        $.ajax({
            url: "/SolicitudEfectivo/SaveSolicitudEfectivoDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ SolicitudEfeDetalleC: SolicitudDetalle }),
        })
        .done(function (data) {
            $('#ErrorIdDenoCreate').text('');
            $('#ErrorSDCantidadSolicitadaCreate').text('');
            $('#ErrorSDMontoSolicitadoCreate').text('');
            $('#ErrorSDCantidadEntregadaCreate').text('');
            $('#ErrorSDMontoEntregadoCreate').text('');

            //Input
            $('#deno_Id').val('0');
          
            $('#soled_CantidadSolicitada').val('');
            $('#MontoSolicitado').val('');
            $('#ValorDeno').val('');
            $('#soled_CantidadSolicitada').val('');
           
           

        });
    }
});


function GetSolicitudDetalle() {

    var SolicitudDetalle = {
        deno_Id: $('#deno_Id').val(),
        soled_CantidadSolicitada: $('#soled_CantidadSolicitada').val(),
        MontoSolicitado: $('#MontoSolicitado').val(),
        soled_CantidadEntregada: $('#soled_CantidadEntregada').val(),
        soled_MontoEntregado: $('#soled_MontoEntregado').val(),

        soled_Id: contador
    }
    return SolicitudDetalle
};

$(document).on("click", "#tblSolicitudEfectivoDetalle tbody tr td button#removeSolicitudEfectivoDetalle", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var SolicitudDetalle = {
        soled_Id: idItem,
    };
    $.ajax({
        url: "/SolicitudEfectivo/RemoveSolicitudEfectivo",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ SolicitudEfeDetalleC: SolicitudDetalle }),
    });
});



