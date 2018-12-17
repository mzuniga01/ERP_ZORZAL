var contador = 0;

$(document).on("click", "#tblMunicipios tbody tr td button#removeMunicipios", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var Municipios = {
        mun_UsuarioCrea: idItem,
    };
    $.ajax({
        url: "/Departamento/RemoveMunicipios",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Municipios: Municipios }),
    });
});



$('#AgregarMunicipios').click(function () {
    var MunCodigo = $('#mun_Codigo').val();
    var MunNombre = $('#mun_Nombre').val();

    if( MunCodigo =='')
    {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationCodigoCreate').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Campo Codigo Municipio Requerido</ul>');
    }
    else if (MunNombre == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationNombreCreate').after('<ul id="NombreError" class="validation-summary-errors text-danger">Campo Municipio Requerido</ul>');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        //copiar += "<td>" + $('#CodTipoCasoExitoCreate option:selected').text() + "</td>";
        //copiar += "<td hidden id='MunCodigo'>" + $('#mun_Codigo option:selected').val() + "</td>";
        copiar += "<td id = 'MunCodigo'>" + $('#mun_Codigo').val() + "</td>";
        copiar += "<td id = 'MunNombre'>" + $('#mun_Nombre').val() + "</td>";
        copiar += "<td>" + '<button id="removeMunicipios" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblMunicipios').append(copiar);

        
        var tbMunicipio = GetMunicipio();
        $.ajax({
            url: "/Departamento/SaveMunicipio",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Municipio: tbMunicipio }), 
        })
                .done(function (data) {
                    $('#mun_Codigo').val('');
                    $('#mun_Nombre').val('');
                    $('#MessageError').text('');
                    $('#NombreError').text('');
                    console.log('Hola');
                });



    }

});


function GetMunicipio()
{
    var Municipio = {
        mun_Codigo: $('#mun_Codigo').val(),
        mun_Nombre: $('#mun_Nombre').val(),
        mun_UsuarioCrea: contador
    };
    return Municipio;
}