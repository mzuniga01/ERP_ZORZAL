var contador = 0;
$('#AgregarPuntoEmisionDetalle').click(function () {

    var DocumentoFiscal = $('#dfisc_Id').val();
    var RangoInicio = $('#pemid_RangoInicio').val();
    var RangoFinal = $('#pemid_RangoFinal').val();
    var NumeroActual = $('#pemid_NumeroActual').val();
    var FechaLimite = $('#pemid_FechaLimite').val();

    //Split Rango Inicial
    var divisiones = RangoInicio.split("-", 4);
    var ultimo = divisiones[3]
    var rango = parseInt(ultimo)

    //Split Rango Final
    var divisiones1 = RangoFinal.split("-", 4);
    var ultimo1 = divisiones1[3]
    var rango1 = parseInt(ultimo1)

    //Longitud de la cadena
    var Length = 19;
    var RangoInicioLength = $('#pemid_RangoInicio').val().length;
    var RangoFinalLength = $('#pemid_RangoFinal').val().length;

    //ValidaciónFecha < GetCurrentDate
    var p = new Date(FechaLimite);

    ////Current date
    var GetCurrentDate = new Date();
    
    if (DocumentoFiscal == '') {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionDocumentoFiscalCreate').after('<p id="ErrorDocumentoFiscalCreate" style="color:red">Campo Documento Fiscal requerido</p>');
    }
    else if (RangoInicio == '')
    {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionRangoInicioCreate').after('<p id="ErrorRangoInicioCreate" style="color:red">Campo Rango Inicio requerido</p>');
    }
    else if (RangoInicioLength < Length) {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionRangoInicioCreate').after('<p id="ErrorRangoInicioLengthCreate" style="color:red">Campo Rango Inicio debe  tener 19 caracteres</p>');
    }
    else if (RangoFinal == '')
    {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionRangoFinalCreate').after('<p id="ErrorRangoFinalCreate" style="color:red">Campo Rango Final requerido</p>');
    }
    else if (rango1 <= rango) {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionRangoFinalCreate').after('<p id="ErrorRangoFinalSplitCreate" style="color:red">El Rango Final debe ser mayor al Rango Inicial</p>');
    }
    else if (RangoFinalLength < Length) {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionRangoFinalCreate').after('<p id="ErrorRangoFinalLengthCreate" style="color:red">El Rango Final debe tener el mismo formato de Rango Inicial</p>');
    }
    else if (FechaLimite == '') {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionFechaLimiteCreate').after('<p id="ErrorFechaLimiteCreate" style="color:red">Campo Fecha Limite requerido</p>');
    }
    else if (p < GetCurrentDate) {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionFechaLimiteCreate').after('<p id="ErrorFechaLimiteMenorCreate" style="color:red">El campo Fecha Límite debe ser mayor a la actual</p>');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td>" + $('#dfisc_Id option:selected').text() + "</td>";
        copiar += "<td hidden id='dfisc_IdCreate'>" + $('#dfisc_Id option:selected').val() + "</td>";
        copiar += "<td id = 'pemid_RangoInicioCreate'>" + $('#pemid_RangoInicio').val() + "</td>";
        copiar += "<td id = 'pemid_RangoFinalCreate'>" + $('#pemid_RangoFinal').val() + "</td>";
        copiar += "<td id = 'pemid_NumeroActualCreate'>" + $('#pemid_NumeroActual').val() + "</td>";
        copiar += "<td id = 'pemid_FechaLimiteCreate'>" + $('#pemid_FechaLimite').val() + "</td>";
        copiar += "<td>" + '<button id="removePuntoEmisionDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblPuntoEmisionDetalle').append(copiar);

        var PuntoEmisionDetalle = GetPuntoEmisionDetalle();
        $.ajax({
            url: "/PuntoEmision/SavePuntoEmisionDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ PuntoEmisionDet: PuntoEmisionDetalle }),
        })
        .done(function (data) {
            //Limpiar mensajes
            $('#ErrorDocumentoFiscalCreate').text('');
            $('#ErrorRangoInicioCreate').text('');
            $('#ErrorRangoInicioLengthCreate').text('');
            $('#ErrorRangoFinalCreate').text('');
            $('#ErrorRangoFinalSplitCreate').text('');
            $('#ErrorRangoFinalLengthCreate').text('');
            $('#ErrorFechaLimiteCreate').text('');
            $('#ErrorFechaLimiteMenorCreate').text('');
            //Limpiar input
            $('#dfisc_Id').val('');
            $('#pemid_RangoInicio').val('');
            $('#pemid_RangoFinal').val('');
            $('#pemid_NumeroActual').val('');
            $('#pemid_FechaLimite').val('');

        });
    }
    
});

function GetPuntoEmisionDetalle() {    
    var PuntoEmisionDetalle = {        
        dfisc_Id: $('#dfisc_Id').val(),
        pemid_RangoInicio: $('#pemid_RangoInicio').val(),
        pemid_RangoFinal: $('#pemid_RangoFinal').val(),
        pemid_NumeroActual: $('#pemid_NumeroActual').val(),
        pemid_FechaLimite: new Date($('#pemid_FechaLimite').val()),
        pemid_Id: contador
    }
    return PuntoEmisionDetalle
};

$(document).on("click", "#tblPuntoEmisionDetalle tbody tr td button#removePuntoEmisionDetalle", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var PuntoEmisionDetalle = {
        pemid_Id: idItem,
    };
    $.ajax({
        url: "/PuntoEmision/RemovePuntoEmisionDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ PuntoEmisionDet: PuntoEmisionDetalle }),
    });
});

//Máximo de caracteres_Rangos
$(document).ready(function () {
    $("#pemid_RangoInicio")[0].maxLength = 20;
    $("#pemid_RangoFinal")[0].maxLength = 20;
});


//Mostrar Mensaje:Campo requerido en tiempo real
$("#dfisc_Id").change(function () {
    var dfisc_Id = $("#dfisc_Id").val();
    if (dfisc_Id != '') {
        $('#ErrorDocumentoFiscalCreateEventoChange').text('');
    }
    else {
        $('#validacionDocumentoFiscalCreate').after('<p id="ErrorDocumentoFiscalCreateEventoChange" style="color:red">Campo Documento Fiscal requerido</p>');
    }
});
$("#pemid_RangoInicio").change(function () {
    var pemid_RangoInicio = $("#pemid_RangoInicio").val();
    if (pemid_RangoInicio != '') {
        $('#ErrorRangoInicioCreateEventoChange').text('');
    }
    else {
        $('#validacionRangoInicioCreate').after('<p id="ErrorRangoInicioCreateEventoChange" style="color:red">Campo Rango Inicio requerido</p>');
    }
});

$("#pemid_RangoFinal").change(function () {
    var pemid_RangoFinal = $("#pemid_RangoFinal").val();
    if (pemid_RangoFinal != '') {
        $('#ErrorRangoFinalCreateEventoChange').text('');
    }
    else {
        $('#validacionRangoFinalCreate').after('<p id="ErrorRangoFinalCreateEventoChange" style="color:red">Campo Rango Final requerido</p>');
    }

});

$("#pemid_FechaLimite").change(function () {
    var pemid_FechaLimite = $("#pemid_FechaLimite").val();
    if (pemid_FechaLimite != '') {
        $('#ErrorFechaLimiteCreateEventoChange').text('');
    }
    else {
        $('#validacionFechaLimiteCreate').after('<p id="ErrorFechaLimiteCreateEventoChange" style="color:red">Campo Fecha Limite requerido</p>');
    }
});

$(document).ready(function () {
    $("#pemid_RangoInicio").blur(function () {
        var RangoInicio = $('#pemid_RangoInicio').val();        

        //Split Rango Inicial
        var divisiones = RangoInicio.split("-", 4);
        var ultimo = divisiones[3]

        var NumeroActual = $('#pemid_NumeroActual').val(ultimo);
    });
});