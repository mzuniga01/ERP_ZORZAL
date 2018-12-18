$("#clte_EsPersonaNatural").change(function () {
    if (this.checked) {
        //Do stuff
        $('#natural').show();
        $("#clte_NombreComercial").val('**');
        $("#clte_RazonSocial").val('**');
        $("#clte_ContactoNombre").val('**');
        $("#clte_ContactoEmail").val('');
        $("#clte_ContactoTelefono").val('**');
        $("#clte_FechaConstitucion").val('');
        $("#clte_Nombres").val('');
        $("#clte_Apellidos").val('');
        $("#clte_FechaNacimiento").val('');
        $("#clte_Sexo").val('');
        $("#clte_Telefono").val('');
        $("#clte_Direccion").val('');
        $("#clte_CorreoElectronico").val('');
        $('#juridica').hide();
    }
    else {
        $('#natural').hide();
        $("#clte_Nombres").val('**');
        $("#clte_Apellidos").val('**');
        $("#clte_FechaNacimiento").val('');
        $("#clte_Sexo").val('**');
        $("#clte_Telefono").val('**');
        $("#clte_Direccion").val('');
        $("#clte_CorreoElectronico").val('');
        $("#clte_NombreComercial").val('');
        $("#clte_RazonSocial").val('');
        $("#clte_ContactoNombre").val('');
        $("#clte_ContactoEmail").val('');
        $("#clte_ContactoTelefono").val('');
        $("#clte_FechaConstitucion").val('');
        $('#juridica').show();
    }
});

$(document).ready(function () {
    $('#identificacion').hide();

    if (clte_EsPersonaNatural.checked) {
        $('#natural').show();
        $('#juridica').hide();
    } else {
        $('#juridica').show();
        $("#clte_Nombres").val('**');
        $("#clte_Apellidos").val('**');
        $("#clte_FechaNacimiento").val('');
        $("#clte_Sexo").val('**');
        $("#clte_Telefono").val('**');
        $("#clte_Direccion").val('');
        $('#natural').hide();
    }
});


$("#tpi_Id").change(function () {
    var d = $("#tpi_Id").val();

    if (d == 8) {
        document.getElementById("clte_Identificacion").maxLength = "13";
    }
    else if (d == 10) {
        document.getElementById("clte_Identificacion").maxLength = "25";
    }
    else if (d == 15) {
        document.getElementById("clte_Identificacion").maxLength = "14";
    }
    else {

    }
});

$("#tpi_Id").on("change", function () {
        $('#identificacion').show();
});

$('#clte_EsPersonaNatural').on('click', function () {
    var x;
    var r = confirm("¿Esta seguro de borrar los datos ya Ingresados?");
    if (r == true) {
    }
    else {
        return false
    }

});

$("#clte_ContactoTelefono").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});