function GuardarEditNumeracion(IdPuntoEmisionDetalle) {
    var IdPuntoEmisionDetalle = $('#pemid_Id_' + IdPuntoEmisionDetalle).val();
    var IdPuntoEmision = $('#pemi_Id').val();

    var RangoInicial = $('#RangoInicial_' + IdPuntoEmisionDetalle).val();
    var RangoFinal = $('#RangoFinal_' + IdPuntoEmisionDetalle).val();
    var NumeroActual = $('#NumeroActual_' + IdPuntoEmisionDetalle).val();
    var FechaLimite = $('#FechaLimite_' + IdPuntoEmisionDetalle).val();

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

    if (RangoInicial == '') {
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionRangoInicioEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorRangoInicioEdit_'+ IdPuntoEmisionDetalle+'" style="color:red">Campo Rango Inicial requerido</p>');
    }
    else if (RangoInicioLength < Length) {
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionRangoInicioEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle + '" style="color:red">Campo Rango Inicio debe  tener 19 caracteres</p>');
    }
    else if (RangoFinal == '')
    {
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionRangoFinalEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle + '" style="color:red">Campo Rango Final requerido</p>');
    }
    else if (rangof <= rangoi) {
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionRangoFinalEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle + '" style="color:red">El Rango Final debe ser mayor al Rango Inicial</p>');
    }
    else if (RangoFinalLength < Length) {
        $('#ErrorRangoInicioEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoInicioLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalSplitEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle).text('');
        $('#ValidacionRangoFinalEdit_' + IdPuntoEmisionDetalle).after('<p id="ErrorRangoFinalLengthEdit_' + IdPuntoEmisionDetalle + '" style="color:red">El Rango Final debe tener el mismo formato de Rango Inicial</p>');
    }
    else {
        var GetPuntoEmisionDetalleEdit = {
            pemid_Id: IdPuntoEmisionDetalle,
            pemi_Id: IdPuntoEmision,
            pemid_RangoInicio: RangoInicial,
            pemid_RangoFinal: RangoFinal,
            pemid_NumeroActual: NumeroActual,
            pemid_FechaLimite:  new Date(FechaLimite).val()
        };

        $.ajax({
            url: "/PuntoEmision/SaveEditNumeracion",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ EditPuntoEmisionDetalle: GetPuntoEmisionDetalleEdit }),
        })
        .done(function (data) {
            if (data == 'El registro se guardó exitosamente') {
                location.reload();
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

