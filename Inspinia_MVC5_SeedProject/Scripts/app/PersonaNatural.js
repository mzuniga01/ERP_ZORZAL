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
    var depto = $('#dep_Codigo').val();
    if (depto === '') {
        document.getElementById("mun_Codigo").disabled = true;
    }
    else {
        
    }

});

$("#dep_Codigo").change(function () {
    var depto = $('#dep_Codigo').val();
    if (depto != '') {
        document.getElementById("mun_Codigo").disabled = false;
    }
    else {

    }
});


$("#tpi_Id").change(function () {
    var d = $("#tpi_Id").val();

    if (d == 4) {
        document.getElementById("clte_Identificacion").maxLength = "13";
    }
    else if (d == 1) {
        document.getElementById("clte_Identificacion").maxLength = "25";
    }
    else if (d == 3) {
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
    if (this.checked) {
        var NombreC = $("#clte_NombreComercial").val();
        var RazonS = $("#clte_RazonSocial").val();
        var ContactoN = $("#clte_ContactoNombre").val();
        var Email = $("#clte_ContactoEmail").val();
        var ContactoTel = $("#clte_ContactoTelefono").val();
        if (NombreC == '' && RazonS == '' && ContactoN == '' && Email == '' && ContactoTel == '') {

        }
        else {
            var r = confirm("¿Esta seguro de borrar los datos ya Ingresados?");
            if (r == true) {
            }
            else {
                return false
            }
        }
    }
    else {
        var Nombres = $("#clte_Nombres").val();
        var Apellidos = $("#clte_Apellidos").val();
        var Sexo = $("#clte_Sexo").val();
        console.log(Sexo)
        var Telefono = $("#clte_Telefono").val();
        var Correo = $("#clte_CorreoElectronico").val();
        if (Nombres == '' && Apellidos == '' && Sexo == null && Telefono == '' && Correo == '') {

        }
        else {
            var r = confirm("¿Esta seguro de borrar los datos ya Ingresados?");
            if (r == true) {
            }
            else {
                return false
            }
        }
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

$("#clte_ContactoEmail").blur(function () {   
        campo = event.target;
        valido = document.getElementById('emailOK');

        var reg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        var regOficial = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;
        //Se muestra un texto a modo de ejemplo, luego va a ser un icono
        if (reg.test(campo.value) && regOficial.test(campo.value)) {
            valido.innerText = "";
        } else if (reg.test(campo.value)) {
            valido.innerText = "";

        } else {
            valido.innerText = "Direccion de Correo Electronico Incorrecta";

        }
});

$("#clte_CorreoElectronico").blur(function () {
    campo = event.target;
    valido = document.getElementById('emailOK1');

    var reg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    var regOficial = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (reg.test(campo.value) && regOficial.test(campo.value)) {
        valido.innerText = "";
    } else if (reg.test(campo.value)) {
        valido.innerText = "";

    } else {
        valido.innerText = "Direccion de Correo Electronico Incorrecta";

    }
});
