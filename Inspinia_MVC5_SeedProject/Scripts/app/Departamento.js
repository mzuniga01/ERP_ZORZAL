////Get Municipio
$(document).on("change", "#dep_Codigo", function () {
    GetMunicipios();
});

function GetMunicipios() {
    var CodDepartamento = $('#dep_Codigo').val();
    $.ajax({
        url: "/Cliente/GetMunicipios",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodDepartamento: CodDepartamento }),
    })
    .done(function (data) {
        if (data.length > 0) {
            $('#mun_Codigo').empty();
            $('#mun_Codigo').append("<option value=''>Seleccione Municipio</option>");
            $.each(data, function (key, val) {
                $('#mun_Codigo').append("<option value=" + val.mun_Codigo + ">" + val.mun_Nombre + "</option>");
            });
            console.log(mun_Codigo)
            $('#mun_Codigo').trigger("chosen:updated");
        }
        else {
            $('#mun_Codigo').empty();
            $('#mun_Codigo').append("<option value=''>Seleccione Municipio</option>");
        }
    });
}

//Fin