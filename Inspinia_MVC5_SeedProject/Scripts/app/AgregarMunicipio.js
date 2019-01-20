var contador = 0;
var contadorEdit = 0;
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
            url: "/Departamento/AnadirMunicipio",
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

//GUARDAR MUNICIPIOS
$('#btnGuardar').click(function () {
    var munCodigo = $('#mun_Codigo').val();
    var munNombre = $('#mun_Nombre').val();

    if (munNombre == '') {
        $('#mun_Nombre').text('');
        $('#errorCodigo').text('');
        $('#errorNombre').text('');
        $('#ValidationNombre').after('<ul id="errorNombre" class="validation-summary-errors text-danger">Campo Municipio Requerido</ul>');

    }

    else if (munCodigo == '') {
        $('#mun_Nombre').text('');
        $('#errorCodigo').text('');
        $('#errorNombre').text('');
        $('#ValidationCodigoUpdate').after('<ul id="errorCodigo" class="validation-summary-errors text-danger">Campo Codigo Municipio Requerido</ul>');

    }

    else {
        var tbMunicipio = GetMunicipio();
        $.ajax({
            url: "/Departamento/GuardarMun",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ tbMunicipio: tbMunicipio }),

        })
        .done(function (data) {
            if (data == '') {
                $('#ValidationNombreUpdate').after('<ul id="ValidationNombreUpdate" class="validation-summary-errors text-danger">No se pudo actualizar el registro, contacte con el administrador</ul>');
            }
            else {
                window.location.href = '/Departamento/Index';
            }
        })
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

//Agreagar Municipios en Edit Departamento(Esto lo agregó Mágdaly)
$('#AgregarMunicipiosEdit').click(function () {
    var MunCodigo = $('#mun_CodigoEdit').val();
    var MunNombre = $('#mun_NombreEdit').val();

    if (MunCodigo == '') {
        $('#ValidationCreate').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidacionMunCodigoEdit').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Campo Requerido</ul>');
    }
    else if (MunNombre == '') {
        $('#ValidationCreate').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidacionMunNombreEdit').after('<ul id="NombreError" class="validation-summary-errors text-danger">Campo Requerido</ul>');
    }
    else {
        contadorEdit = contadorEdit + 1;
        copiar = "<tr data-id=" + contadorEdit + ">";
        copiar += "<td id = 'MunCodigo'>" + $('#mun_CodigoEdit').val() + "</td>";
        copiar += "<td id = 'MunNombre'>" + $('#mun_NombreEdit').val() + "</td>";
        copiar += "<td>" + '<button id="removeMunicipiosEdit" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblMunicipiosEdit').append(copiar);

        var Municipio = {
            mun_Codigo: $('#mun_CodigoEdit').val(),
            mun_Nombre: $('#mun_NombreEdit').val(),
            mun_UsuarioCrea: contadorEdit,
        };
        $.ajax({
            url: "/Departamento/AnadirMunicipio",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Municipio: Municipio }),
        })
        .done(function (data) {
            $('#ValidationCreate').text('');
            $('#mun_CodigoEdit').val('');
            $('#mun_NombreEdit').val('');
            $('#CodigoError').text('');
            $('#NombreError').text('');
        });
    }
});

$(document).ready(function () {
    $("#mun_CodigoEdit")[0].maxLength = 4;
})

$("#mun_NombreEdit").change(function () {
    var str = $("#mun_NombreEdit").val();
    var res = str.toUpperCase();
    $("#mun_NombreEdit").val(res);
});

$("#mun_NombreEdit").on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})

$('#btnNuevoEdit').click(function () {
    var depCodigo = $('#dep_Codigo').val();
    $.ajax({
        url: "/Departamento/GuardarMuninicipio",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ depCodigo: depCodigo }),
    })
    .done(function (data) {
        if (data == '-1') {
            $('#ValidationCreate').text('');
            $('#ValidationCreate_').after('<ul id="ValidationCreate" class="validation-summary-errors text-danger">No se pudo guardar el registro, contacte al administrador</ul>');
        }
        else {
            window.location.href = '/Departamento/Edit/' + depCodigo;
        }
    })
});

$(document).on("click", "#tblMunicipiosEdit tbody tr td button#removeMunicipiosEdit", function () {
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
/////////////////////////////////////////

//Edit Municipios en Edit Departamento (Esto lo agregó Mágdaly)
function EditMunicipioRecord(mun_Codigo)
{
    $.ajax({
        url: "/Departamento/getMunicipio",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ munCodigo: mun_Codigo }),

    })
    .done(function (data) {
        if (data.length > 0) {
            $.each(data, function (i, item) {
                $("#mun_CodigoEdit_").val(item.mun_Codigo);
                $("#mun_NombreEdit_").val(item.mun_Nombre);
                $("#EditMunicipioModal").modal();
            })
        }
    })
}

$("#BtnsubmitMunicipio").click(function () {
    var depCodigo = $('#dep_Codigo').val();
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "Post",
        url: "/Departamento/ActualizarMunicipio",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
            else
                window.location.href = '/Departamento/Edit/' + depCodigo;
        }
    });
})

$("#mun_NombreEdit_").change(function () {
    var str = $("#mun_NombreEdit").val();
    var res = str.toUpperCase();
    $("#mun_NombreEdit").val(res);
});

$("#mun_NombreEdit_").on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})
/////////////////////////////////////////