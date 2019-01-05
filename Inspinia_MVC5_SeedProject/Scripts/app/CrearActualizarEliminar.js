//Actualizar Municipios
function btnActualizar(mun_Codigo) {
    

    var Municipio = $('#mun_Codigo' + mun_Codigo).val();
    var Depatamento = $('#dep_Codigo' + mun_Codigo).val();
    var NombreMunicipio = $('#mun_Nombre' + mun_Codigo).val();

    var tbMunicipio = GetMunicipioActualizar_UPDATE();
   
   

    $.ajax({
        url: "/Departamento/ActualizarMun",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ActualizarMun: tbMunicipio }),
    }).done(function (data) {
        if (data == 'Exito') {
            location.reload();
        }
        else {
            $('#MessageErrorEdit_' + mun_Codigo).text('');
            $('#validationSummaryEdit_' + mun_Codigo).after('<ul id="MessageErrorEdit_' + mun_Codigo + '" class="validation-summary-errors text-danger">Campo Requerido</ul>');
        }
    });
}
function GetMunicipioActualizar_UPDATE() {
    var ActualizarMun = {
        mun_Codigo: $('#mun_Codigo').val(),
        dep_Codigo: $('#dep_Codigo').val(),
        mun_Nombre: $('#mun_Nombre').val(),
    };
    return ActualizarMun;
}


//Guardar Municipio Modales
function btnGuardarModal(mun_Codigo) {

    var munCodigo = $("#mun_CodigoCreate_" + mun_Codigo).val();
    var depCodigo = $("#dep_CodigoCreate_" + mun_Codigo).val();
    var munNombre = $("#mun_NombreCreate_" + mun_Codigo).val();

    var tbMunicipio = GetMunicipioGuardar();

    $.ajax({
        url: "/Departamento/GuardarMun",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ GuardarMun: tbMunicipio }),
    }).done(function (data) {
        if (data == 'Exito') {
            location.reload();
        }
        else {
            $('#MessageErrorEdit_' + mun_Codigo).text('');
            $('#validationSummaryEdit_' + mun_Codigo).after('<ul id="MessageErrorEdit_' + mun_Codigo + '" class="validation-summary-errors text-danger">Campo Requerido</ul>');
        }
    });
}
function GetMunicipioGuardar() {
    var GuardarMun = {
        mun_Codigo: $('#mun_Codigo').val(),
        dep_Codigo: $('#dep_Codigo').val(),
        mun_Nombre: $('#mun_Nombre').val(),
        mun_FechaCrea: $('#mun_FechaCrea').val(),
        mun_UsuarioModifica: $('#mun_UsuarioModifica').val(),
        mun_FechaModifica: $('#mun_FechaModifica').val(),
        mun_UsuarioModifica: $('#mun_UsuarioModifica').val(),

    };
    return GuardarMun;
}

//EliminarMunicipios

function btnEliminar(EliminarMunicipio) {

    var munCodigo = $("#munCodigoDelete_" + EliminarMunicipio).val();

    var MunicipioE = {
        mun_Codigo: munCodigo

    };
    $.ajax({
        url: "/Departamento/EliminarMunicipio",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ mun_Codigo: MunicipioE }),
    }).done(function (data) {
        if (data == 'Exito') {
            location.reload();
        }

        else {
            $('#MessageErrorM' + EliminarMunicipio).text('');
            $('#validationmunNombreEdit_' + EliminarMunicipio).after('<ul id="MessageErrorM' + EliminarMunicipio + '" class="validation-summary-errors text-danger">Se produjo un error, no se pudo actualizar el registro.</ul>');
        }
    });
}
