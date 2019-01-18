$('#cred_MontoAprobado').on('change', function () {
    var MontoSolicitado = $('#cred_MontoSolicitado').val();
    var MontoAprobobado = $('#cred_MontoAprobado').val();
    var MontoSolicitadoC = parseInt(MontoSolicitado);
    var MontoAprobobadoC = parseInt(MontoAprobobado);

    if (MontoAprobobadoC > MontoSolicitadoC) {
        valido = document.getElementById('AcepSolicitud2');
        valido.innerText = "El monto Aprobado no puede ser mayor al solicitado";
        $('#cred_MontoAprobado').val('');

    }
    else {
        valido = document.getElementById('AcepSolicitud2');
        valido.innerText = "";
    }
});
$('#cred_DiasAprobado').on('change', function () {
    var MontoSolicitado = $('#cred_DiasSolicitado').val();
    var MontoAprobobado = $('#cred_DiasAprobado').val();
    var MontoSolicitadoA = parseFloat(MontoSolicitado);
    var MontoAprobobadoB = parseFloat(MontoAprobobado);

    if (MontoAprobobadoB > MontoSolicitadoA) {
        valido = document.getElementById('AcepSolicitud');
        valido.innerText = "Los Dias Solicitados no puede ser mayor al solicitado";
        $('#cred_DiasAprobado').val('');
    }
    else {
        valido = document.getElementById('AcepSolicitud');
        valido.innerText = "";
    }
});

//validacion no vacios ni iguales a cero
$(function () {
    $("#cred_FechaAprobacion").datepicker({ dateFormat: 'yy-mm-dd' });
    $("#cred_FechaSolicitud").datepicker({ dateFormat: 'dd/mm/yy' }).datepicker("destroy");
});
/////////////VALIDACIONES
$('#AceptarAprobacion').click(function () {
    var MontoAprobado = $('#cred_MontoAprobado').val();
    var MSF = parseInt(MontoAprobado);
    var MSstring = MontoAprobado;


    if (MSF <= 0) {
       // alert('El valor Monto Aprobado no puede ser cero');
        document.getElementById('AceptarAprobacion').disabled = true;
        valido = document.getElementById('AcepSolicitud2');
        valido.innerText = "El valor Monto Aprobado no puede ser cero";
        $('#cred_MontoAprobado').val(MontoAprobado);
        
        //if (document.getElementById('AceptarAprobacion').disabled = true /*&& MSF > 0*/)
        //{
        //    document.getElementById('AceptarAprobacion').disabled = false;
        //    $('#cred_MontoAprobado').val(MontoAprobado);
        //}
        
    }    else if (MSstring == '') {
        {
            // alert('El valor Monto Aprobado no puede ser cero');
           document.getElementById('AceptarAprobacion').disabled = true
            valido = document.getElementById('AcepSolicitud2');
            valido.innerText = "El valor Monto Aprobado no puede ser vacio";
        }
    }

});



$("#cred_MontoAprobado").blur(function () {
    var MontoAprobado = $('#cred_MontoAprobado').val();
    var MSF = parseInt(MontoAprobado);
    var MSstring = MontoAprobado;


    if (MSF <= 0) {
        // alert('El valor Monto Aprobado no puede ser cero');
        document.getElementById('AceptarAprobacion').disabled = true;
        valido = document.getElementById('AcepSolicitud2');
        valido.innerText = "El valor Monto Aprobado no puede ser cero";
        $('#cred_MontoAprobado').val(MontoAprobado);

        //if (document.getElementById('AceptarAprobacion').disabled = true /*&& MSF > 0*/)
        //{
        //    document.getElementById('AceptarAprobacion').disabled = false;
        //    $('#cred_MontoAprobado').val(MontoAprobado);
        //}

    } else {
        document.getElementById('AceptarAprobacion').disabled =false;
    }
    if (MSstring == '') {
        {
            // alert('El valor Monto Aprobado no puede ser cero');
            document.getElementById('AceptarAprobacion').disabled = true
            valido = document.getElementById('AcepSolicitud2');
            valido.innerText = "El valor Monto Aprobado no puede ser vacio";
        }
    }
    else {
        document.getElementById('AceptarAprobacion').disabled = false;
    }
});



//Validacion de numeros//
function soloNumeros(e) {
    if ((car < '0' || car > '9') && (car < ',' || car > '.')) evt.consume();
}