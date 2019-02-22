
//NombreDeProveedor
function CaracteresNombre(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ1234567890 ]+$/.test(tecla);

}
//////////////////////////////////////////////////////////
$("#prov_Telefono").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\+.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
///SoloNumerosy+Telefono
$("#prov_Telefono").on("keypress keyup blur", function (event) {
    var Telefono = $(this).val();
  
    if (Telefono == '') {
        $(this).val('+');
    }
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
                        $('#prov_RTN').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se Puede Guardar Contacte Al Administrador</ul>');
                    }
                    else if (data == "-2") {
                        $('#RTN').text('');
                        $('#ErrorValidacionGeneral').text('');
                        $('#prov_RTN').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">Este RTN Ya existe</ul>');
                    }

                    else {
                        window.location.href = "Index/Proveedor";
                    }
                    console.log(data);
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

    if (Nombre.substring(0, 1) == "") {
        $('#Nombre').text('');
        $('#errorNombre').text('');
        $('#validationNombre').after('<ul id="errorNombre" class="validation-summary-errors text-danger">El primer caracter no puede un espacio en blanco.</ul>');
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
                    $('#prov_RTN').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se Guardo el registro, Contacte al Administrador.</ul>');
                }
                else if (data == "-2") {
                    $('#RTN').text('');
                    $('#ErrorValidacionGeneral').text('');
                    $('#prov_RTN').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">Este RTN Ya existe</ul>');
                }
                else {
                    window.location.href = "/Proveedor/Index";
                }
                console.log(data);
            })
    }
});

////Validacion De Correo Electronico
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
////Validacion De solo letras 
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}
/////Validacion de RTN<14
$('#prov_RTN').change(function (e) {
   
    var RTN = $("#prov_RTN").val();
    $("#ErrorCorreo").remove();
    if (RTN < 9999999999999) {

        $('#ErrorCorreo').text('');
        $('#validationRTN').after('<ul id="ErrorCorreo" class="validation-summary-errors text-danger">RTN debe tener 14 dígitos</ul>');
    }


});
/////Validacion De Space
$('#prov_Nombre,#prov_NombreContacto,#prov_Direccion,#prov_Email').change(function () {
 
    //alert("el input cambio");
    if ($(this).val() == 0) { //si el input es cero
        $('#btnGuardar').attr('disabled', 'disabled');
        $('#btnActualizar').attr('disabled', 'disabled');
       alert("No puede llevar Solo Espacios")
    }
   else if ($(this).val() == 0) { //si el input es cero
       $('#btnGuardar').attr('disabled', 'disabled');
       $('#btnActualizar').attr('disabled', 'disabled');
     
   }
   else if ($(this).val() == 0) { //si el input es cero
       $('#btnGuardar').attr('disabled', 'disabled');
       $('#btnActualizar').attr('disabled', 'disabled');
      
   }
    else { // si tiene un valor diferente a cero
       $('#btnGuardar').removeAttr("disabled");
       $('#btnActualizar').removeAttr("disabled");

    }
});
/////ValidacionDireccion
function controlCaracteres(e) {

    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ1234567890# ,.]+$/.test(tecla);

}