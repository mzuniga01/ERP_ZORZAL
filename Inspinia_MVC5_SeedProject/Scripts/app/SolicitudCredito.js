$('#cred_MontoAprobado').on('change', function () {
    var MontoSolicitado = $('#cred_MontoSolicitado').val();
    var MontoAprobobado = $('#cred_MontoAprobado').val();
    var MontoSolicitadoC = parseInt(MontoSolicitado);
    var MontoAprobobadoC = parseInt(MontoAprobobado);

    if (MontoAprobobadoC > MontoSolicitadoC) {
        valido = document.getElementById('AcepSolicitud');
        valido.innerText = "El monto Aprobado no puede ser mayor al solicitado";
        $('#cred_MontoAprobado').val('');
    }
    else {
        valido = document.getElementById('AcepSolicitud');
        valido.innerText = "";
    }
});

