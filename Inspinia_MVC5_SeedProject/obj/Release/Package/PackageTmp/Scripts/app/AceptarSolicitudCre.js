function AceptarSolicitud(CodSolicitud, estado) {
    $.ajax({
        url: "/SolicitudCredito/AceptarSolicitud",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodSolicitud: CodSolicitud, estado: estado }),

    })
    .done(function (data) {
        if (data.length > 0) {
            location.reload(true);
        }
        else {
            alert("Registro No Actualizado");
        }
    });
}
