$('#prov_Email').change(function (e) {
    var emailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var EmailId = this.value;
    if (emailRegex.test(EmailId)) {
        $('#ErrorCorreo').text('');
        this.style.backgroundColor = "";
    }

    else {
       
        $('#ErrorCorreo').text('');
        $('#MessageForCorreo').after('<ul id="ErrorCorreo" class="validation-summary-errors text-danger">Correo Electronico Es Incorrecto </ul>');
        $("#prov_Email").focus();
    }


});


function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-z0-9A-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}


$("#prov_Telefono").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});


$("#prov_Telefono").on("keypress keyup blur", function (event) {
    var Telefono = $(this).val();
  
    if (Telefono == '') {
        $(this).val('+');
    }
    this.value = this.value.replace(/[a-záéíóúüñ#/=]+/ig, "");
});


$("#prov_RTN").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

$('#prov_RTN').change(function (e) {
 
    var RTN = $("#prov_RTN").val();
    $("#ErrorCorreo").remove();
    if (RTN < 14) {
        $('#ErrorCorreo').text('');
        $('#validationRTN').after('<ul id="ErrorCorreo" class="validation-summary-errors text-danger">RTN debe tener 14 dígitos</ul>');
    }


});


$('#btnGuardar').click(function () {


    $("#errorRTN").remove();
    $("#errorNombre").remove();
    $("#errorContacto").remove();
    $("#errorDireccion").remove();

    $("#errorEmail").remove();
    $("#errorTelefono").remove();
    $("#errorActividad").remove();
    var RTN = $("#prov_RTN").val();
    var Nombre = $("#prov_Nombre").val();
    var Contacto = $("#prov_NombreContacto").val();
    var Direccion = $("#prov_Direccion").val();
    var Email = $("#prov_Email").val();
    var Telefono = $("#prov_Telefono").val();
    var Actividad = $("#acte_Id").find('option:selected').val();
    //console.log(Actividad);
    if (RTN == '') {
        $('#RTN').text('');
        $('#errorRTN').text('');
        $('#validationRTN').after('<ul id="errorRTN" class="validation-summary-errors text-danger">Campo RTN Requerido</ul>');
    }

    if (Nombre == '') {
        $('#Nombre').text('');
        $('#errorNombre').text('');
        $('#validationNombre').after('<ul id="errorNombre" class="validation-summary-errors text-danger">Campo Nombre Requerido</ul>');
    }

    if (Contacto == '') {
        $('#Contacto').text('');
        $('#errorContacto').text('');
        $('#validationContacto').after('<ul id="errorContacto" class="validation-summary-errors text-danger">Campo Contacto Requerido</ul>');
    }

    if (Direccion == '') {
        $('#Direccion').text('');
        $('#errorDireccion').text('');
        $('#validationDireccion').after('<ul id="errorDireccion" class="validation-summary-errors text-danger">Campo Direccion Requerido</ul>');
    }
    if (Email == '') {
        $('#Email').text('');
        $('#errorEmail').text('');
        $('#validationEmail').after('<ul id="errorEmail" class="validation-summary-errors text-danger">Campo Email Requerido</ul>');
    }
    if (Telefono == '') {
        $('#Telefono').text('');
        $('#errorTelefono').text('');
        $('#validationTelefono').after('<ul id="errorTelefono" class="validation-summary-errors text-danger">Campo Telefono Requerido</ul>');
    }
    if (Actividad == '') {
        $('#Actividad').text('');
        $('#errorActividad').text('');
        $('#validationActividad').after('<ul id="errorActividad" class="validation-summary-errors text-danger">Campo Actividad Economica Requerido</ul>');
    }
  
    else {


        $.ajax({
            url: "/Proveedor/GuardarProveedor",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ prov_RTN: RTN, prov_Nombre: Nombre, prov_NombreContacto: Contacto, prov_Direccion: Direccion, prov_Email: Email, prov_Telefono: Telefono, acte_Id: Actividad}),
        })
                .done(function (data) {
                    if (data == "-1") {
                        $('#prov_RTN').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se Puede Guardar</ul>');
                    }
                    else if (data == "-2") {
                        $('#prov_RTN').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">RTN Ya Existe!!!</ul>');
                    }

                    else {
                        window.location.href = "Index/Proveedor";
                    }
                    console.log(data);
            })
    }
});
$('#btnActualizar').click(function () {
    $("#errorRTN").remove();
    $("#errorNombre").remove();
    $("#errorContacto").remove();
    $("#errorDireccion").remove();

    $("#errorEmail").remove();
    $("#errorTelefono").remove();
    $("#errorActividad").remove();

    var Id = $("#prov_Id").val();
    var RTN = $("#prov_RTN").val();
    var Nombre = $("#prov_Nombre").val();
    var Contacto = $("#prov_NombreContacto").val();
    var Direccion = $("#prov_Direccion").val();
    var Email = $("#prov_Email").val();
    var Telefono = $("#prov_Telefono").val();
    var Actividad = $("#acte_Id").val();
    if (RTN == '') {
        $('#RTN').text('');
        $('#errorRTN').text('');
        $('#validationRTN').after('<ul id="errorRTN" class="validation-summary-errors text-danger">Campo RTN Requerido</ul>');
    }

    if (Nombre == '') {
        $('#Nombre').text('');
        $('#errorNombre').text('');
        $('#validationNombre').after('<ul id="errorNombre" class="validation-summary-errors text-danger">Campo Nombre Requerido</ul>');
    }

    if (Contacto == '') {
        $('#Contacto').text('');
        $('#errorContacto').text('');
        $('#validationContacto').after('<ul id="errorContacto" class="validation-summary-errors text-danger">Campo Contacto Requerido</ul>');
    }

    if (Direccion == '') {
        $('#Direccion').text('');
        $('#errorDireccion').text('');
        $('#validationDireccion').after('<ul id="errorDireccion" class="validation-summary-errors text-danger">Campo Direccion Requerido</ul>');
    }
    if (Email == '') {
        $('#Email').text('');
        $('#errorEmail').text('');
        $('#validationEmail').after('<ul id="errorEmail" class="validation-summary-errors text-danger">Campo Email Requerido</ul>');
    }
    if (Telefono == '') {
        $('#Telefono').text('');
        $('#errorTelefono').text('');
        $('#validationTelefono').after('<ul id="errorTelefono" class="validation-summary-errors text-danger">Campo Telefono Requerido</ul>');
    }
    if (Actividad == '') {
        $('#Actividad').text('');
        $('#errorActividad').text('');
        $('#validationActividad').after('<ul id="errorActividad" class="validation-summary-errors text-danger">Campo Actividad Economica Requerido</ul>');
    }
    else {


        $.ajax({
            url: "/Proveedor/ActualizarProveedor",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ prov_Id: Id, prov_RTN: RTN, prov_Nombre: Nombre, prov_NombreContacto: Contacto, prov_Direccion: Direccion, prov_Email: Email, prov_Telefono: Telefono, acte_Id: Actividad }),
        })
            .done(function (data) {
                if (data == "-1") {
                    $('#prov_RTN').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se Puede Guardar</ul>');
                }
                else if (data == "-2") {
                    $('#prov_Nombre').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">Ya Existe Un Proveedor Con El Mismo Nombre!!!</ul>');
                }

                else {
                    window.location.href = "/Proveedor/Index";
                }
                console.log(data);
            })
    }
});