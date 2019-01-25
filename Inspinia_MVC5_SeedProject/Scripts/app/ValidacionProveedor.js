$('#prov_Email').change(function (e) {
    var emailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var EmailId = this.value;
    if (emailRegex.test(EmailId))
        this.style.backgroundColor = "";
    else
        this.style.backgroundColor = "LightPink";
    
});
$("#prov_Telefono").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
$('#btnGuardar').click(function () {
    console.log('hola')
    var RTN = $("#prov_RTN").val();
    var Nombre = $("#prov_Nombre").val();
    var Contacto = $("#prov_NombreContacto").val();
    var Direccion = $("#prov_Direccion").val();
    var Email = $("#prov_Email").val();
    var Telefono = $("#prov_Telefono").val();
    if (RTN == '') {
        $('#RTN').text('');
        $('#errorRTN').text('');
        $('#validationRTN').after('<ul id="errorRTN" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }

    else if (Nombre == '') {
        $('#Nombre').text('');
        $('#errorNombre').text('');
        $('#validationNombre').after('<ul id="errorNombre" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }

    else if (Contacto == '') {
        $('#Contacto').text('');
        $('#errorContacto').text('');
        $('#validationContacto').after('<ul id="errorContacto" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }

    else if (Direccion == '') {
        $('#Direccion').text('');
        $('#errorDireccion').text('');
        $('#validationDireccion').after('<ul id="errorDireccion" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
    else if (Email == '') {
        $('#Email').text('');
        $('#errorEmail').text('');
        $('#validationEmail').after('<ul id="errorEmail" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
    else if (Telefono == '') {
        $('#Telefono').text('');
        $('#errorTelefono').text('');
        $('#validationTelefono').after('<ul id="errorTelefono" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }


    else {


        $.ajax({
            url: "/Proveedor/GuardarProveedor",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ prov_RTN:RTN, prov_Nombre: Nombre, prov_NombreContacto: Contacto, prov_Direccion: Direccion, prov_Email: Email, prov_Telefono: Telefono }),
        })
                .done(function (data) {
                    if (data == "") {
                        $('#prov_RTN').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">RTN Ya Existe!!!</ul>');
                    }
                    else {
                        window.location.href = "Index/Proveedor";
                    }
                    console.log(data);
                })
    }
});