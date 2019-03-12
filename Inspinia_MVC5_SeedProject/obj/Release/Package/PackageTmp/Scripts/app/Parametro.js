function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgpreview').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#CargarFoto").change(function () {
    readURL(this);
});


//Solo numeros
$('#par_TelefonoEmpresa').keypress(function (e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9\-]+$/.test(tecla);

});


$('#par_SucursalPrincipal').keypress(function (e) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if ((keynum == 48) || (keynum == 57))
        return true;
    return /\d/.test(String.fromCharCode(keynum));

});

$('#par_PorcentajeDescuentoTE').keypress(function (e) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if ((keynum == 48) || (keynum == 57))
        return true;
    return /\d/.test(String.fromCharCode(keynum));

});

//Validar que se borre mensaje mientras se escribe
$('#par_NombreEmpresa').change(function () {
    $('#errornombreempresa').hide();
});

$('#par_TelefonoEmpresa').change(function () {
    $('#errortelefonoempresa').hide();
    $('#errorformatotelefono').hide();
    
});

$('#par_CorreoEmpresa').change(function () {
    $('#errorcorreoempresa').hide();
});

$('#mnda_Id').change(function () {
    $('#errormoneda').hide();
});

$('#par_RolGerenteTienda').change(function () {
    $('#errorgerentetienda').hide();
});

$('#par_RolCreditoCobranza').change(function () {
    $('#errorcreditocobranza').hide();
});

$('#par_RolSupervisorCaja').change(function () {
    $('#errorsupervisorcaja').hide();
});

$('#par_RolCajero').change(function () {
    $('#errorcajero').hide();
});

$('#par_RolAuditor').change(function () {
    $('#errorauditor').hide();
});

$('#par_SucursalPrincipal').change(function () {
    $('#errorsucursalprincipal').hide();
});

$('#par_PorcentajeDescuentoTE').change(function () {
    $('#errorporcentajedescuento').hide();
});

$('#par_IdConsumidorFinal').change(function () {
    $('#errorconsumidorfinal').hide();
});

$('#CargarFoto').change(function () {
    $('#errorlogo').hide();
});

//Numero de telefono
var selector = "#par_TelefonoEmpresa";
$(selector).mask("+999-99999-9999", { reverse: true });




