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
        $('#ErrorRangoInicioCorrelativo').text('');
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
        $('#ErrorRangoInicioCorrelativo').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionRangoInicioCreate').after('<p id="ErrorRangoInicioCreate" style="color:red">Campo Rango Inicial requerido</p>');
    }
    else if (RangoInicioLength < Length) {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoInicioCorrelativo').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionRangoInicioCreate').after('<p id="ErrorRangoInicioLengthCreate" style="color:red">Campo Rango Inicial debe tener 19 caracteres</p>');
    }
    else if (rango == 0) {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoInicioCorrelativo').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionRangoInicioCreate').after('<p id="ErrorRangoInicioCorrelativo" style="color:red">El número correlativo debe ser mayor a 0</p>');
    }
    else if (RangoFinal == '')
    {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoInicioCorrelativo').text('');
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
        $('#ErrorRangoInicioCorrelativo').text('');
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
        $('#ErrorRangoInicioCorrelativo').text('');
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
        $('#ErrorRangoInicioCorrelativo').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionFechaLimiteCreate').after('<p id="ErrorFechaLimiteCreate" style="color:red">Campo Fecha Límite Emisión requerido</p>');
    }
    else if (p < GetCurrentDate) {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoInicioCorrelativo').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#ErrorFechaLimiteMenorCreate').text('');
        $('#validacionFechaLimiteCreate').after('<p id="ErrorFechaLimiteMenorCreate" style="color:red">El campo Fecha Límite Emisión debe ser mayor a la actual</p>');
    }
    else {
        var PuntoEmisionDetalle = GetPuntoEmisionDetalle();
        $.ajax({
            url: "/PuntoEmision/SavePuntoEmisionDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ PuntoEmisionDet: PuntoEmisionDetalle }),
            success: function (data) {
            }
        })
        .done(function (data) {
            if (data == 'Ya existe este Rango Inicial') {
                $('#MensajeNumeracionRangoInicial').text('');
                $('#validacionRangoInicioCreate').after('<p id="MensajeNumeracionRangoInicial" style="color:red">Ya existe este Rango Inicial</p>');
            }
            else if (data == 'Ya existe este Rango Final') {
                 $('#MensajeNumeracionRangoFinal').text('');
                 $('#validacionRangoInicioCreate').after('<p id="MensajeNumeracionRangoFinal" style="color:red">Ya existe este Rango Final</p>');
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

                //Limpiar mensajes
                $('#ErrorDocumentoFiscalCreate').text('');
                $('#ErrorRangoInicioCreate').text('');
                $('#ErrorRangoInicioLengthCreate').text('');
                $('#ErrorRangoFinalCreate').text('');
                $('#ErrorRangoFinalSplitCreate').text('');
                $('#ErrorRangoFinalLengthCreate').text('');
                $('#ErrorFechaLimiteCreate').text('');
                $('#ErrorFechaLimiteMenorCreate').text('');
                $('#MensajeNumeracionRangoInicial').text('');
                $('#MensajeNumeracionRangoFinal').text('');
                //Limpiar input
                $('#dfisc_Id').val('');
                $('#pemid_RangoInicio').val('');
                $('#pemid_RangoFinal').val('');
                $('#pemid_NumeroActual').val('');
                $('#pemid_FechaLimite').val('');
            }
                
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
    $("#pemid_RangoInicio")[0].maxLength = 19;
    $("#pemid_RangoFinal")[0].maxLength = 19;
});

//Número Actual
$("#pemid_RangoInicio").keyup(function () {
    var RangoInicio = $('#pemid_RangoInicio').val();
    var divisiones = RangoInicio.split("-", 4);
    var ultimo = divisiones[3]
    var NumeroActual = $('#pemid_NumeroActual').val(ultimo);
});

//Mostrar Mensaje:Campo requerido en tiempo real
$("#dfisc_Id").change(function () {
    var dfisc_Id = $("#dfisc_Id").val();
    if (dfisc_Id != '') {
        $('#ErrorDocumentoFiscalCreate').text('');
    }
    else {
        $('#validacionDocumentoFiscalCreate').after('<p id="ErrorDocumentoFiscalCreate" style="color:red">Campo Documento Fiscal requerido</p>');
    }
});

//borrar mensajes en tiempo real
$("#pemid_RangoInicio").keyup(function () {
    $('#ErrorRangoInicioCreate').text('');
    $('#ErrorRangoInicioLengthCreate').text('');
    $('#ErrorRangoInicioCorrelativo').text('');
});

$("#pemid_RangoFinal").keyup(function () {
    $('#ErrorRangoFinalCreate').text('');
    $('#ErrorRangoFinalSplitCreate').text('');
    $('#ErrorRangoFinalLengthCreate').text('');
});

$("#pemid_FechaLimite").keyup(function () {
    $('#ErrorFechaLimiteCreate').text('');
    $('#ErrorFechaLimiteMenorCreate').text('');
});