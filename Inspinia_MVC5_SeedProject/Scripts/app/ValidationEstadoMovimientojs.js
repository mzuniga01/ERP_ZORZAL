$('#btnGuardar').click(function () {
  
    var Descripcion = $("#estm_Descripcion").val();
   
    if (Descripcion == '') {
        $('#Descripcion').text('');
        $('#errorDescripcion').text('');
        $('#validationDescripcion').after('<ul id="errorDescripcion" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }


    else {


        $.ajax({
            url: "/EstadoMovimiento/GuardarEstado",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ estm_Descripcion: Descripcion }),
        })
            .done(function (data) {
                if (data == "-1") {
                    $('#estm_Descripcion').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se Puede Guardar</ul>');
                }

                else if (data == "-2"){
                    $('#estm_Descripcion').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">Ya Existe Un Estado con el mismo Nombre</ul>');
                }

                else {
                    window.location.href = "Index/EstadoMovimiento";
                }
                console.log(data);
            })
    }
});


function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ]+$/.test(tecla);
}
$('#btnActualizar').click(function () {
    console.log('hola')
    var Id = $("#estm_Id").val();
    var Descripcion = $("#estm_Descripcion").val();

    if (Descripcion == '') {
        $('#Descripcion').text('');
        $('#errorDescripcion').text('');
        $('#validationDescripcion').after('<ul id="errorDescripcion" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }


    else {


        $.ajax({
            url: "/EstadoMovimiento/ActualizarEstado",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ estm_Id: Id,estm_Descripcion: Descripcion }),
        })
            .done(function (data) {
                if (data == "-1") {
                    $('#estm_Descripcion').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se Puede Guardar</ul>');
                }

                else if (data == "-2") {
                    $('#estm_Descripcion').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">Ya Existe Un Estado con el mismo Nombre</ul>');
                }

                else {
                    window.location.href = "/EstadoMovimiento/Index";
                }
                console.log(data);
            })
    }
});