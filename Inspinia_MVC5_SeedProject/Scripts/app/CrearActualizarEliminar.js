//Actualizar Municipios

function btnActualizar(mun_Codigo) {
   
    var Municipio = mun_Codigo;
    var Depatamento = $('#dep_Codigo').val();
    var NombreMunicipio = $("#MunNombre_" + mun_Codigo).val();
    
     
    console.log(Municipio);
    console.log(NombreMunicipio);
    console.log(Depatamento);

    $.ajax({
        url: "/Departamento/ActualizarMunicipio",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ mun_Codigo: Municipio, dep_Codigo: Depatamento, mun_Nombre: NombreMunicipio }),
    }).done(function (data) {
        if (data == '') {
            location.reload();
        }
        else if (data == '-1') {
            $('#MensajeError' + mun_Codigo).text('');
            $('#ValidationMessageFor' + mun_Codigo).after('<ul id="MensajeError' + mun_Codigo + '" class="validation-summary-errors text-danger">No se ha podido Actualizar el registro.</ul>');
        }
        else {
            $('#MensajeError' + mun_Codigo).text('');
            $('#ValidationMessageFor' + mun_Codigo).after('<ul id="MensajeError' + mun_Codigo + '" class="validation-summary-errors text-danger">Campo Requerido</ul>');
        }
    });
}


//Guardar Municipio Modales
$('#btnNuevo').click(function () {

    var CodigoMun = $('#mun_Codigo').val();
    var Depatamento = $('#dep_Codigo').val();
    var NombreMun = $('#mun_Nombre').val();

    console.log(CodigoMun)
    console.log(NombreMun)
        //var munCodigo = $('#mun_Codigo').val();
        //var munNombre = $('#mun_Nombre').val();


    if (CodigoMun == '') {
            $('#mun_Codigo').text('');
            $('#errorCodigo').text('');
            $('#errorNombre').text('');
            $('#ValidationNombre').after('<ul id="errorNombre" class="validation-summary-errors text-danger">Campo Municipio Requerido</ul>');
            console.log('HOLAAAA')
        }

    else if (NombreMun == '') {
            $('#mun_Nombre').text('');
            $('#errorCodigo').text('');
            $('#errorNombre').text('');
            $('#ValidationCodigoUpdate').after('<ul id="errorCodigo" class="validation-summary-errors text-danger">Campo Codigo Municipio Requerido</ul>');
            console.log('ADIOS')
        }

        else {
            $.ajax({
                url: "/Departamento/GuardarMunicipioModal",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ mun_Codigo: CodigoMun, dep_Codigo: Depatamento, mun_Nombre: NombreMun }),

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
