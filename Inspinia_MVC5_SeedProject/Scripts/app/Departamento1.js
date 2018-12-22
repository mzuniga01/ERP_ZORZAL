$(document).ready(function () {
    GetDepartamento();
});
function GetDepartamento() {
    var CodMunicipio = $('#mun_Codigo').val();
    $.ajax({
        url: "/Cliente/GetDepartamento",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodMunicipio: CodMunicipio }),
    })
        .done(function (data) {
            if (data.length > 0) {
                $('#dep_Codigo').empty();
                $.each(data, function (key, val) {
                    $('#dep_Codigo').append("<option value=" + val.dep_Codigo + ">" + val.dep_Nombre + "</option>");
                });
                console.log(mun_Codigo)
                console.log(CodMunicipio)

                $('#dep_Codigo').trigger("chosen:updated");
            }
            else {
                $('#dep_Codigo').empty();
                $('#dep_Codigo').append("<option value=''>Seleccione Departamento</option>");

            }
        });

}
