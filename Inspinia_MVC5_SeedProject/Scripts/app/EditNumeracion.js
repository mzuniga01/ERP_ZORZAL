function GuardarEditNumeracion(IdPuntoEmisionDetalle) {
   
    var IdPuntoEmisionDetalle = $('#pemid_Id_' + IdPuntoEmisionDetalle).val();
    var IdPuntoEmision = $('#pemi_Id').val();
    var DocumentoFiscal = $('#dfisc_IdEdit_' + IdPuntoEmisionDetalle).val();
    var RangoInicial = $('#RangoInicial_' + IdPuntoEmisionDetalle).val();
    var RangoFinal = $('#RangoFinal_' + IdPuntoEmisionDetalle).val();
    var NumeroActual = $('#NumeroActual_' + IdPuntoEmisionDetalle).val();
    var FechaLimite = $('#FechaLimite_' + IdPuntoEmisionDetalle).val();
    var UsuarioCrea = $('#pemid_UsuarioCreaEdit_' + IdPuntoEmisionDetalle).val();
    var FechaCrea = $('#pemid_FechaCreaEdit_' + IdPuntoEmisionDetalle).val();

    //Split Rango Inicial
    var divisiones = RangoInicial.split("-", 4);
    var ultimo = divisiones[3]
    var rangoi = parseInt(ultimo)

    //Split Rango Final
    var divisiones1 = RangoFinal.split("-", 4);
    var ultimo1 = divisiones1[3]
    var rangof = parseInt(ultimo1)
   
    //Longitud de la cadena
    var Length = 19;
    var RangoInicioLength = RangoInicial.length;
    var RangoFinalLength = RangoFinal.length;

    //ValidaciónFecha < GetCurrentDate
    var pemid_FechaLimiteEmision = $('#FechaLimite_' + IdPuntoEmisionDetalle).val();
    var p = new Date(pemid_FechaLimiteEmision);

    ////Current date
    var GetCurrentDate = new Date();

    if (DocumentoFiscal == '')
    {
        $('#ErrorDocumentoFiscalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteVacioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteMenorEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionDocumentoFiscalEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorDocumentoFiscalEdit_' + IdPuntoEmisionDetalle + '" style="color:red">Campo Documento Fiscal requerido</p>');
    }
    else if (RangoInicial == '')
    {
        $('#ErrorDocumentoFiscalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteVacioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteMenorEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionRangoInicioEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorRangoInicioEdit_'+ IdPuntoEmisionDetalle+'" style="color:red">Campo Rango Inicial requerido</p>');
    }
    else if (RangoInicioLength < Length)
    {
        $('#ErrorDocumentoFiscalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteVacioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteMenorEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionRangoInicioEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle + '" style="color:red">Campo Rango Inicio debe  tener 19 caracteres</p>');
    }
    else if (RangoFinal == '')
    {
        $('#ErrorDocumentoFiscalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteVacioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteMenorEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionRangoFinalEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle + '" style="color:red">Campo Rango Final requerido</p>');
    }
    else if (rangof <= rangoi)
    {
        $('#ErrorDocumentoFiscalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteVacioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteMenorEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionRangoFinalEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle + '" style="color:red">El Rango Final debe ser mayor al Rango Inicial</p>');
    }
    else if (RangoFinalLength < Length)
    {
        $('#ErrorDocumentoFiscalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteVacioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteMenorEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionRangoFinalEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle + '" style="color:red">El Rango Final debe tener el mismo formato de Rango Inicial</p>');
    }
    else if (FechaLimite == '')
    {
        $('#ErrorDocumentoFiscalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteVacioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteMenorEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionFechaLimiteEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorFechaLimiteVacioEdit_' + IdPuntoEmisionDetalle + '" style="color:red">El campo Fecha Límite es requerido</p>');
    }
    else if (p < GetCurrentDate) {
        $('#ErrorDocumentoFiscalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteVacioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorFechaLimiteMenorEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionFechaLimiteEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorFechaLimiteMenorEdit_' + IdPuntoEmisionDetalle + '" style="color:red">El campo Fecha Límite debe ser mayor a la actual</p>');
    }
    else {
        var GetPuntoEmisionDetalleEdit = {
            pemid_Id: IdPuntoEmisionDetalle,
            pemi_Id: IdPuntoEmision,
            dfisc_Id: DocumentoFiscal,
            pemid_RangoInicio: RangoInicial,
            pemid_RangoFinal: RangoFinal,
            pemid_NumeroActual: NumeroActual,
            pemid_FechaLimite: new Date(FechaLimite),
            pemid_UsuarioCrea: UsuarioCrea,
            pemid_FechaCrea: FechaCrea
        };

        $.ajax({
            url: "/PuntoEmision/SaveEditNumeracion",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ EditPuntoEmisionDetalle: GetPuntoEmisionDetalleEdit }),
            success: function (data) {
            }
        })
        .done(function (data) {
            if (data == 'El registro se guardó exitosamente') {
                location.reload();
                swal("El registro se editó exitosamente!", "", "success"); 
            }
            else {
                location.reload();
                swal("El registro  no se editó!", "", "error");
            }
        });
    }
}


//Datepicker
function FechaLimite(IdPuntoEmisionDetalle) {
    $("#FechaLimite_" + IdPuntoEmisionDetalle).datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        minDate: new Date(),
        maxDate: '+3Y',
        prevText: 'Ant',
        nextText: 'Sig',
        changeMonth: true,
        changeYear: true,
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
    });
}

