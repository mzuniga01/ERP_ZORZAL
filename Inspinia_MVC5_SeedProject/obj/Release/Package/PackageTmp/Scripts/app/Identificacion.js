$(document).on("change", '#clte_EsPersonaNatural', function () {
    GetIdentificacion();
});

function GetIdentificacion() {
    if (clte_EsPersonaNatural.checked) {
        var CodIdentificacion = 1;
    } else {
        var CodIdentificacion = 0;
    }
    $.ajax({
        url: "/Cliente/GetIdentificacion",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodIdentificacion: CodIdentificacion }),
    })
    .done(function (data) {
        if (data.length > 0) {
            $('#tpi_Id').empty();
            $('#tpi_Id').append("<option value=''>Seleccione Tipo Identificacion</option>");
            $.each(data, function (key, val) {
                $('#tpi_Id').append("<option value=" + val.tpi_Id + ">" + val.tpi_Descripcion + "</option>");
            });
            $('#tpi_Id').trigger("chosen:updated");
        }
    });

}