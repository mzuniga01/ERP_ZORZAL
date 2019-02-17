function btnActualizarBodegaDetalle(bodd_Id) {
     var bodega_id = $('#bod_Id').val();
    var bodegadetalle_id = $('#bodd_Id').val();
    var producto_codigo = $('#producto_Edit_' + bodd_Id ).val();
    var puntoreorden = $('#puntoreorden_Edit_' + bodd_Id ).val();
    var cantidadminima = $('#cantidadminima_Edit_' + bodd_Id ).val();
    var cantidadmaxima = $('#cantidadmaxima_Edit_' + bodd_Id ).val();
    var usuariocrea= $('#bodd_UsuarioCrea').val();
    var fechacrea= $('#bodd_FechaCrea').val();
    var usuariomodifica= $('#bodd_UsuarioModifica').val();
    var fechamodifica =$('#bodd_FechaModifica').val();
    var costo = $('#costo_Edit_' + bodd_Id ).val();
    var costoPromedio = $('#costopromedio_Edit_' + bodd_Id ).val();

    //Cantidad Minima debe ser menor a Punto reorden y Cantidad Maxima
    var min

    if (puntoreorden == '') {
        $('#ErrorCantidadMinimaEdit_' + bodd_Id).text('');
        $('#ErrorCantidadMaximaEdit_' + bodd_Id).text('');
        $('#ErrorPuntoReordenEdit_' + bodd_Id).text('');
        $('#ErrorCostoEdit_' + bodd_Id).text('');
        $('#ErrorCostoPromedioEdit_' + bodd_Id).text('');
        $('#ValidacionPuntoReordenEdit_' + bodd_Id).after('<p id="ErrorPuntoReordenEdit_' + bodd_Id + '" style="color:red">El Campo Punto Reorden es requerido</p>');
    }
    else if (cantidadminima == '') {
        $('#ErrorCantidadMinimaEdit_' + bodd_Id).text('');
        $('#ErrorCantidadMaximaEdit_' + bodd_Id).text('');
        $('#ErrorPuntoReordenEdit_' + bodd_Id).text('');
        $('#ErrorCostoEdit_' + bodd_Id).text('');
        $('#ErrorCostoPromedioEdit_' + bodd_Id).text('');
        $('#ValidacionCantidadMinimaEdit_' + bodd_Id).after('<p id="ErrorCantidadMinimaEdit_' + bodd_Id + '" style="color:red">El Campo Cantidad Mínima es requerido</p>');
    }
    else if (cantidadmaxima == '') {
        $('#ErrorCantidadMinimaEdit_' + bodd_Id).text('');
        $('#ErrorCantidadMaximaEdit_' + bodd_Id).text('');
        $('#ErrorPuntoReordenEdit_' + bodd_Id).text('');
        $('#ErrorCostoEdit_' + bodd_Id).text('');
        $('#ErrorCostoPromedioEdit_' + bodd_Id).text('');
        $('#ValidacionCantidadmaximaEdit_' + bodd_Id).after('<p id="ErrorCantidadMaximaEdit_' + bodd_Id + '" style="color:red">El Campo Cantidad Máxima es requerido</p>');
    }
    else if (costo == '') {
        $('#ErrorCantidadMinimaEdit_' + bodd_Id).text('');
        $('#ErrorCantidadMaximaEdit_' + bodd_Id).text('');
        $('#ErrorPuntoReordenEdit_' + bodd_Id).text('');
        $('#ErrorCostoEdit_' + bodd_Id).text('');
        $('#ErrorCostoPromedioEdit_' + bodd_Id).text('');
        $('#ValidacionCostoEdit_' + bodd_Id).after('<p id="ErrorCostoEdit_' + bodd_Id + '" style="color:red">El Campo Costo es requerido</p>');
    }
    else if (costoPromedio == '') {
        $('#ErrorCantidadMinimaEdit_' + bodd_Id).text('');
        $('#ErrorCantidadMaximaEdit_' + bodd_Id).text('');
        $('#ErrorPuntoReordenEdit_' + bodd_Id).text('');
        $('#ErrorCostoEdit_' + bodd_Id).text('');
        $('#ErrorCostoPromedioEdit_' + bodd_Id).text('');
        $('#ValidacionCostoPromedioEdit_' + bodd_Id).after('<p id="ErrorCostoPromedioEdit_' + bodd_Id + '" style="color:red">El campo Costo Promedio es requerido</p>');
    }
    else {
        var tbBodegaDetalle = {
            bod_Id: $('#bod_Id').val(),
            bodd_Id: $('#bodd_Id').val(),
            prod_Codigo: producto_codigo,
            bodd_PuntoReorden: puntoreorden,
            bodd_CantidadMinima: cantidadminima,
            bodd_CantidadMaxima: cantidadmaxima,
            bodd_UsuarioCrea: $('#bodd_UsuarioCrea').val(),
            bodd_FechaCrea: $('#bodd_FechaCrea').val(),
            bodd_UsuarioModifica: $('#bodd_UsuarioModifica').val(),
            bodd_FechaModifica: $('#bodd_FechaModifica').val(),
            bodd_Costo: costo,
            bodd_CostoPromedio: costoPromedio,

        };
        $.ajax({
            url: "/Bodega/UpdateBodegaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Editardetalle: tbBodegaDetalle }),
            success: function (data) {
            }
        }).done(function (data) {
            if (data == 'Exito.') {
                location.reload();
            }
            else {
                $('#MessageGuardar').text('');
                $('#ValidationSummary').after('<p id="MessageGuardar" style="color:red">No se pudo agregar el registro.</p>');
            }
        });
    }
}



function btnCerrar() {

                location.reload();
            
}

btnCerrar.onclick = function () { location.reload(); };

$(document).on('blur', '#puntoreorden_Edit_', function () {
    var Pr = $('#puntoreorden_Edit_' + bodd_Id).val();
    var Mn = $('#cantidadminima_Edit_' + bodd_Id).val();
    alert(Pr)
    if (Mn)


        if (Pr != '' && Mn != '') {

            if (parseFloat(Mn) > parseFloat(Pr)) {


                $('#ErrorCantidadMinimaEdit_' + bodd_Id).text('');
                $('#ErrorCantidadMaximaEdit_' + bodd_Id).text('');
                $('#ErrorPuntoReordenEdit_' + bodd_Id).text('');
                $('#ValidacionPuntoReordenEdit_' + bodd_Id).after('<p id="ErrorPuntoReordenEdit_' + bodd_Id + '" style="color:red">Punto Reorden</p>');
            }
            else {
                $('#ErrorPuntoReordenEdit_' + bodd_Id).text('');
            }
        }


})

$(document).on('blur', '#bodd_CantidadMinima', function () {
    var Pr = $('#puntoreorden_Edit_' + bodd_Id).val();
    var Mn = $('#cantidadminima_Edit_' + bodd_Id).val();
    if (Mn)


        if (Pr != '' && Mn != '') {

            if (parseFloat(Mn) > parseFloat(Pr)) {


                $('#ErrorCantidadMinimaEdit_' + bodd_Id).text('');
                $('#ErrorCantidadMaximaEdit_' + bodd_Id).text('');
                $('#ErrorPuntoReordenEdit_' + bodd_Id).text('');
                $('#ValidacionCantidadMinimaEdit_' + bodd_Id).after('<p id="ErrorCantidadMinimaEdit_' + bodd_Id + '" style="color:red">Cantidad Minima</p>');
                console.log('1')
            }
            else {
                $('#ErrorCantidadMinimaEdit_' + bodd_Id).text('');
            }
        }


})

$(document).on('blur', '#bodd_CantidadMaxima', function () {
    var Pr = $('#puntoreorden_Edit_' + bodd_Id).val();
    var Mx = $('#cantidadmaxima_Edit_' + bodd_Id).val();
    if (Mx)


        if (Pr != '' && Mx != '') {

            if (parseFloat(Mx) < parseFloat(Pr)) {

                $('#Error_CantidadMaxima').text('');
                $('#ErrorCantidadMaxima_Create').after('<ul id="Error_CantidadMaxima" class="validation-summary-errors text-danger">Cantidad Maxima debe ser Mayor que Punto Reorden</ul>');
                console.log('1')
            }
            else {
                $('#Error_CantidadMaxima').text('');
                console.log('2')
            }
        }


})

