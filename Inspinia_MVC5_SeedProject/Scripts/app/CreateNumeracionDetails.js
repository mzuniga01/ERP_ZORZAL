$('#btnCreateNumeracionDetalle').click(function () {
    var DocumentoFiscal = $('#dfisc_Id').val();
    var RangoInicio = $('#txtRangoInicial').val();
    var RangoFinal = $('#txtRangoFinal').val();
    var FechaLimite = $('#txtFechalimite').val();

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
    var RangoInicioLength = RangoInicio.length;
    var RangoFinalLength = RangoFinal.length;

    //var length = RangoInicio.length;
    //var RangoInicio = RangoInicio.substring(11);
    //var RangoInicio = parseInt(RangoInicio);

    //var length = RangoFinal.length;
    //var RangoFinal = RangoFinal.substring(11);
    //var RangoFinal = parseInt(RangoFinal);

    if (DocumentoFiscal == '')
    {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#validacionDocumentoFiscalCreate').after('<p id="ErrorDocumentoFiscalCreate" style="color:red">Campo Documento Fiscal requerido</p>');
    }
    else if (RangoInicio == '') {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#validacionRangoInicioCreate').after('<p id="ErrorRangoInicioCreate" style="color:red">Campo Rango Inicial requerido</p>');
    }
    else if (RangoInicioLength < Length) {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#validacionRangoInicioCreate').after('<p id="ErrorRangoInicioLengthCreate" style="color:red">Campo Rango Inicio debe  tener 19 caracteres</p>');
    }
    else if (RangoFinal == '') {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#validacionRangoFinalCreate').after('<p id="ErrorRangoFinalCreate" style="color:red">Campo Rango Final requerido</p>');
    }
    else if (rango1 <= rango) {
        console.log("1","Split");
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#validacionRangoFinalCreate').after('<p id="ErrorRangoFinalSplitCreate" style="color:red">El Rango Final debe ser mayor al Rango Inicial</p>');
    }
    else if (RangoFinalLength < Length) {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#validacionRangoFinalCreate').after('<p id="ErrorRangoFinalLengthCreate" style="color:red">El Rango Final debe tener el mismo formato de Rango Inicial</p>');
    }
    else if (FechaLimite == '') {
        $('#ErrorDocumentoFiscalCreate').text('');
        $('#ErrorRangoInicioCreate').text('');
        $('#ErrorRangoFinalCreate').text('');
        $('#ErrorRangoInicioLengthCreate').text('');
        $('#ErrorRangoFinalSplitCreate').text('');
        $('#ErrorRangoFinalLengthCreate').text('');
        $('#ErrorFechaLimiteCreate').text('');
        $('#validacionFechaLimiteCreate').after('<p id="ErrorFechaLimiteCreate" style="color:red">Campo Fecha Limite requerido</p>');
    }
    else {
        console.log("2");
        var PuntoEmisionDetalle = GetPuntoEmisionDetalle();
        $.ajax({
            url: "/PuntoEmision/SaveCreateNumeracion",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CreatePuntoEmisionDetalle: PuntoEmisionDetalle }),
        })
        .done(function (data) {
            if (data == 'El registro se guardo exitosamente') {
                location.reload();
            }
        });
    }
    

    function GetPuntoEmisionDetalle() {
        var PuntoEmisionDetalle = {
            pemi_Id: $('#txtpemi_Id').val(),
            dfisc_Id: $('#dfisc_Id').val(),
            pemid_RangoInicio: $('#txtRangoInicial').val(),
            pemid_RangoFinal: $('#txtRangoFinal').val(),
            pemid_FechaLimite: new Date($('#txtFechalimite').val())
        }
        return PuntoEmisionDetalle
    };
});


//Máximo de caracteres_Rangos
$(document).ready(function () {
    $("#txtRangoInicial")[0].maxLength = 20;
    $("#txtRangoFinal")[0].maxLength = 20;
});

//Limpiar los mensajes de error
$("#dfisc_Id").change(function () {
    var dfisc_Id = $("#dfisc_Id").val();
    if (dfisc_Id != '') {
        valido = document.getElementById('ErrorDocumentoFiscalCreate');
        valido.innerText = "";
    }
    else {
        $('#validacionDocumentoFiscalCreate').after('<p id="ErrorDocumentoFiscalCreate" style="color:red">Campo Documento Fiscal requerido</p>');
    }
});