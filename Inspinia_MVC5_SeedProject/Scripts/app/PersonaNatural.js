$("#clte_EsPersonaNatural").change(function () {
    $('#tpi_Id').val('');
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
        $("#clte_Sexo").val('');
        $("#clte_Telefono").val('');
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
        $("#clte_CorreoElectronico").val('');
        $("#clte_NombreComercial").val('');
        $("#clte_RazonSocial").val('');
        $("#clte_ContactoNombre").val('');
        $("#clte_ContactoEmail").val('');
        $("#clte_ContactoTelefono").val('');
        $('#juridica').show();
    }

    var campo = $('#tpi_Id').val();
    if (campo === '') {
        $("#clte_Identificacion").val('')
        $('#identificacion').hide();
    }
});

$(document).ready(function () {
    $("#clte_Identificacion")[0].maxLength = 26;
    $("#clte_Nombres")[0].maxLength = 50;
    $("#clte_Apellidos")[0].maxLength = 50;
    $("#clte_FechaNacimiento")[0].maxLength = 10;
    $("#clte_Nacionalidad")[0].maxLength = 30;
    $("#clte_Telefono")[0].maxLength = 25;
    $("#clte_NombreComercial")[0].maxLength = 50;
    $("#clte_RazonSocial")[0].maxLength = 50;
    $("#clte_ContactoNombre")[0].maxLength = 100;
    $("#clte_ContactoEmail")[0].maxLength = 50;
    $("#clte_ContactoTelefono")[0].maxLength = 25;
    $("#clte_FechaConstitucion")[0].maxLength = 10;
    $("#clte_Direccion")[0].maxLength = 100;
    $("#clte_CorreoElectronico")[0].maxLength = 50;
    $("#clte_RazonInactivo")[0].maxLength = 50;
    $("#clte_Observaciones")[0].maxLength = 250;
    $('#identificacion').hide();
    $('#consumidorfinal').hide();
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
        $('#natural').hide();
    }
    var campo = $('#tpi_Id').val();
    if (campo === '') {
        $("#clte_Identificacion").val('')
        $('#identificacion').hide();
    }
    else {
        $('#identificacion').show();
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
    var campo = $('#tpi_Id').val();
    if (campo === '') {
        $("#clte_Identificacion").val('')
        $('#identificacion').hide();
    }
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
$("#clte_Telefono").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
