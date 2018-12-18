var contador = 0;

$('#AgregarDetalleSolicitudEfectico').click(function () {
    //var SolicitudEfectivoDetalle = $('#soled_Id').val();
    //var SolicitudEfectivo = $('#solef_Id').val();
    var Denominaciondes = $('#tbDenominacion_deno_Descripcion').val();
    var DenominacionTipo = $('#tbDenominacion_deno_Tipo').val();
    var CantidadSolictada = $('#soled_CantidadSolicitada').val();
    
   
    if (Denominaciondes == '') {
        $('#ErrorDenoDescripcionCreate').text('');
        $('#ErrorDenominacionCreate').text('');
        $('#ErrorCantidadSolicitadaCreate').text('');           
        $('#validationSolefIdCreate').after('<ul id="ErrorSolefIdCreate" class="validation-summary-errors text-danger">Campo Denominación es requerido</ul>');
    }
    else if (DenominacionTipo == '') {
        $('#ErrorDenoDescripcionCreate').text('');
        $('#ErrorDenominacionCreate').text('');
        $('#ErrorCantidadSolicitadaCreate').text('');
        $('#validationDenoTipoDenominacionCreate').after('<ul id="ErrorDenominacionCreate" class="validation-summary-errors text-danger">Campo Tipo Denominación es requerido</ul>');
    }

    else if (CantidadSolictada == '') {
        $('#ErrorDenoDescripcionCreate').text('');
        $('#ErrorDenominacionCreate').text('');
        $('#ErrorCantidadSolicitadaCreate').text('');
        $('#validationCantidadSolicitadaCreate').after('<ul id="ErrorCantidadSolicitadaCreate" class="validation-summary-errors text-danger">Campo Cantidad Solicitada es requerido</ul>');
    }   

    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'tbDenominacion_deno_DescripcionCreate'>" + $('#tbDenominacion_deno_Descripcion').val() + "</td>";
        copiar += "<td id = 'tbDenominacion_deno_TipoCreate'>" + $('#tbDenominacion_deno_Tipo').val() + "</td>";
        copiar += "<td id = 'soled_CantidadSolicitadaCreate'>" + $('#soled_CantidadSolicitada').val() + "</td>";       
        copiar += "<td>" + '<button id="removeSolicitudEfectivoDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblSolicitudEfectivoDetalle').append(copiar);

        var FacturaDetalle = GetSolicitudEfectivo();
        $.ajax({
            url: "/SolicitudEfectivo/SaveSolicitudEfectivoDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ SolicitudEfectivoDet: SolicitudEfectivoDetalle }),
        })
        .done(function (data) {
            $('#ErrorDenoDescripcionCreate').text('');
            $('#ErrorDenominacionCreate').text('');
            $('#ErrorCantidadSolicitadaCreate').text('');
           

        });
    }
});


function GetSolicitudEfectivoDenominacionDetalle() {

    var SolicitudEfectivoDenominacionDetalle = {
        deno_Id: $('#deno_Id').val(),
        deno_Descripcion: $('#deno_Descripcion').val(),
        deno_valor: $('#deno_valor').val(),        
        pemid_Id: contador
    }
    return SolicitudEfectivoDenominacionDetalle
};

$(document).on("click", "#tblSolicitudEfectivoDetalle tbody tr td button#removeSolicitudEfectivoDetalle", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var CasoExitos = {
        CodInstructorCasoExito: idItem,
    };
    $.ajax({
        url: "/Instructor/RemoveCasoExito",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CasoExito: CasoExitos }),
    });
});