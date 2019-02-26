function NumText(string) {//solo letras y numeros
    var out = '';
    //Se añaden las letras validas
    var filtro = 'abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890áéíóúÁÉÍÓÚ ,.';//Caracteres validos

    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) != -1)
            out += string.charAt(i);

    return out;
}
function NumText1(string) {//solo letras y numeros
    var out = '';
    //Se añaden las letras validas
    var filtro = 'abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890áéíóúÁÉÍÓÚ ,.#';//Caracteres validos

    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) != -1)
            out += string.charAt(i);

    return out;
}
//NombreDeProveedor
function CaracteresNombre(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ1234567890 ]+$/.test(tecla);

}
//////////////////////////////////////////////////////////
$("#prov_Telefono").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
///SoloNumerosy+Telefono
$("#prov_Telefono").on("keypress keyup blur", function (event) {
    var Telefono = $(this).val();
    this.value = this.value.replace(/[a-záéíóúüñ#/=]+/ig, "");
});
///SoloNumerosRTN
$("#prov_RTN").on("keypress keyup blur", function (event) {
  
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
////Accion Guardar
$('#btnGuardar').click(function () {    
    $("#ErrorValidacionGeneral").remove();
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

    if (RTN == '') {
        $('#RTN').text('');
        $('#errorRTN').text('');
        $('#validationRTN').after('<ul id="errorRTN" class="validation-summary-errors text-danger">Campo RTN Requerido</ul>');
    }

    if ($.trim(Nombre) == '') {
        $('#validationRTN').text('');
        $("errorRTN").removeClass("error");
        $('#Nombre').text('');
        $('#errorNombre').text('');
        $('#validationNombre').after('<ul id="errorNombre" class="validation-summary-errors text-danger">Campo Nombre Requerido</ul>');
    }

    if ($.trim(Contacto) == '') {
        $('#Contacto').text('');
        $('#errorContacto').text('');
        $('#validationContacto').after('<ul id="errorContacto" class="validation-summary-errors text-danger">Campo Contacto Requerido</ul>');
    }

    if ($.trim(Direccion) == '') {
        $('#Direccion').text('');
        $('#errorDireccion').text('');
        $('#validationDireccion').after('<ul id="errorDireccion" class="validation-summary-errors text-danger">Campo Direccion Requerido</ul>');
    }

    if ($.trim(Email) == '') {
        $('#Email').text('');
        $('#errorEmail').text('');
        $('#validationEmail').after('<ul id="errorEmail" class="validation-summary-errors text-danger">Campo Email Requerido</ul>');
    }

    if ($.trim(Telefono) == '') {
        $('#Telefono').text('');
        $('#errorTelefono').text('');
        $('#validationTelefono').after('<ul id="errorTelefono" class="validation-summary-errors text-danger">Campo Telefono Requerido</ul>');
    }

    if ($.trim(Actividad) == '') {
        $('#Actividad').text('');
        $('#errorActividad').text('');
        $('#validationActividad').after('<ul id="errorActividad" class="validation-summary-errors text-danger">Campo Actividad Economica Requerido</ul>');
    }
    
    var length = $("#prov_RTN").val().length;
    if (length < 14) {
        $('#errorRTN').text('');
        $('#validationRTN').after('<ul id="errorRTN" class="validation-summary-errors text-danger">RTN debe tener 14 dígitos</ul>');
        $("#prov_RTN").focus();
    }

    if (RTN != '' && $.trim(Nombre) != '' && $.trim(Contacto) != '' && $.trim(Direccion) != '' && $.trim(Email) != '' && $.trim(Telefono) != '' && $.trim(Actividad) != '' && length == 14) {
        $.ajax({
            url: "/Proveedor/GuardarProveedor",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ prov_RTN: RTN, prov_Nombre: Nombre, prov_NombreContacto: Contacto, prov_Direccion: Direccion, prov_Email: Email, prov_Telefono: Telefono, acte_Id: Actividad}),
        })
        .done(function (data) {
            if (data == "-1") {
                $('#prov_RTN').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se Puede Guardar Contacte Al Administrador</ul>');
            }
            else if (data == "-2") {
                $('#RTN').text('');
                $('#ErrorValidacionGeneral').text('');
                $('#prov_RTN').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">Este RTN Ya existe</ul>');
            }
            else {
                window.location.href = "/Proveedor/Index";
            }
        })
    }
});
////Accion Actualizar
$('#btnActualizar').click(function () {

    $("#ErrorValidacionGeneral").remove();
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

    if ($.trim(Nombre) == '') {
        $('#Nombre').text('');
        $('#errorNombre').text('');
        $('#validationNombre').after('<ul id="errorNombre" class="validation-summary-errors text-danger">Campo Nombre Requerido</ul>');
    }

    if ($.trim(Contacto) == '') {
        $('#Contacto').text('');
        $('#errorContacto').text('');
        $('#validationContacto').after('<ul id="errorContacto" class="validation-summary-errors text-danger">Campo Nombre Contacto Requerido</ul>');
    }

    if ($.trim(Direccion) == '') {
        $('#Direccion').text('');
        $('#errorDireccion').text('');
        $('#validationDireccion').after('<ul id="errorDireccion" class="validation-summary-errors text-danger">Campo Dirección Requerido</ul>');
    }

    if ($.trim(Email) == '') {
        $('#Email').text('');
        $('#errorEmail').text('');
        $('#validationEmail').after('<ul id="errorEmail" class="validation-summary-errors text-danger">Campo Email Requerido</ul>');
    }

    if ($.trim(Telefono) == '') {
        $('#Telefono').text('');
        $('#errorTelefono').text('');
        $('#validationTelefono').after('<ul id="errorTelefono" class="validation-summary-errors text-danger">Campo Teléfono Requerido</ul>');
    }

    if ($.trim(Actividad) == '') {
        $('#Actividad').text('');
        $('#errorActividad').text('');
        $('#validationActividad').after('<ul id="errorActividad" class="validation-summary-errors text-danger">Campo Actividad Económica Requerido</ul>');
    }

    var length = $("#prov_RTN").val().length;
    if (length < 14) {
        $('#errorRTN').text('');
        $('#validationRTN').after('<ul id="errorRTN" class="validation-summary-errors text-danger">RTN debe tener 14 dígitos</ul>');
        $("#prov_RTN").focus();
    }

    if (RTN != '' && $.trim(Nombre) != '' && $.trim(Contacto) != '' && $.trim(Direccion) != '' && $.trim(Email) != '' && $.trim(Telefono) != '' && $.trim(Actividad) != '' && length == 14) {
        $.ajax({
            url: "/Proveedor/ActualizarProveedor",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ prov_Id: Id, prov_RTN: RTN, prov_Nombre: Nombre, prov_NombreContacto: Contacto, prov_Direccion: Direccion, prov_Email: Email, prov_Telefono: Telefono, acte_Id: Actividad }),
        })
        .done(function (data) {
            if (data == "-1") {
                $('#RTN').text('');
                $('#ErrorValidacionGeneral').text('');
                $('#validationAdmi').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se actualizó el registro, contacte al Administrador.</ul>');
            }
            else if (data == "-2") {
                $('#RTN').text('');
                $('#ErrorValidacionGeneral').text('');
                $('#validationAdmi').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">Este RTN Ya existe</ul>');
            }
            else {
                window.location.href = "/Proveedor/Index";
            }
        })
    }
});


////Validacion De solo letras 
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}
/////Validacion de RTN<14
$('#prov_RTN').change(function (e) {
    var RTN = $.trim(this.value);
    if (RTN != '') {
        $('#RTN').text('');
        $('#errorRTN').text('');
    }
    $('#errorRTN').text('');
    var RTN = $("#prov_RTN").val();
    $("#errorRTN").remove();
    var length = $("#prov_RTN").val().length;
    if (length < 14) {
        $('#errorRTN').text('');
        $('#validationRTN').after('<ul id="errorRTN" class="validation-summary-errors text-danger">RTN debe tener 14 dígitos</ul>');
        $("#prov_RTN").focus();
    }
    else
        $('#errorRTN').text('');
});

/////Validacion De Space
$('#prov_Nombre').blur(function () {
    if ($.trim($('#prov_Nombre').val()) == 0) {
        $('#errorNombre').text('');
        $('#validationNombre').after('<ul id="errorNombre" class="validation-summary-errors text-danger">Campo Nombre Requerido</ul>');
    }
});

$('#prov_NombreContacto').blur(function () {
    if ($.trim($('#prov_NombreContacto').val()) == 0) {
        $('#errorContacto').text('');
        $('#validationContacto').after('<ul id="errorContacto" class="validation-summary-errors text-danger">Campo Nombre Contacto Requerido</ul>');
    }
});

$('#prov_Direccion').blur(function () {
    if ($.trim($('#prov_Direccion').val()) == 0) {
        $('#errorDireccion').text('');
        $('#validationDireccion').after('<ul id="errorDireccion" class="validation-summary-errors text-danger">Campo Dirección Requerido</ul>');
    }
});

$('#prov_Email').blur(function () {
    if ($.trim($('#prov_Email').val()) == 0) {
        $('#errorEmail').text('');
        $('#validationEmail').after('<ul id="errorEmail" class="validation-summary-errors text-danger">Campo Email Requerido</ul>');
    }
});
/////ValidacionDireccion
function controlCaracteres(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ1234567890# ,.]+$/.test(tecla);
}

$('#prov_Nombre').change(function (e) {
    var Nombre = $.trim(this.value);
    if (Nombre != '') {
        $('#Nombre').text('');
        $('#errorNombre').text('');
    }
});

$('#prov_NombreContacto').change(function (e) {
    var Contacto = $.trim(this.value);
    if (Contacto != '') {
        $('#Contacto').text('');
        $('#errorContacto').text('');
    }
});

$('#prov_Direccion').change(function (e) {
    var Direccion = $.trim(this.value);
    if (Direccion != '') {
        $('#Direccion').text('');
        $('#errorDireccion').text('');
    }
});

$('#prov_Email').change(function (e) {
    var Email= $.trim(this.value);
    if (Email != '') {
        $('#Email').text('');
        $('#errorEmail').text('');
    }

    var emailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var Correo = this.value;
    if (emailRegex.test(Correo)) { //si el input es cero
        $('#ErrorCorreo').text('');
    }
    else { //si el input es cero

        $('#ErrorCorreo').text('');
        $('#MessageForCorreo').after('<ul id="ErrorCorreo" class="validation-summary-errors text-danger">Correo Electronico Es Incorrecto </ul>');
        $("#prov_Email").focus();
        $('#btnGuardar').attr('disabled', 'disabled');
        $('#btnActualizar').attr('disabled', 'disabled');
    }
});

$('#prov_Telefono').change(function (e) {
    var Telefono = this.value;
    if (Telefono != '') {
        $('#Telefono').text('');
        $('#errorTelefono').text('');
    }
});

$('#acte_Id').change(function (e) {
    var Actividad = $.trim(this.value);
    if (Actividad != '') {
        $('#Actividad').text('');
        $('#errorActividad').text('');
    }
});
    
