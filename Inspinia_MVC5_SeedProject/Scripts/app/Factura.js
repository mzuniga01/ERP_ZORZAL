$("#consumidorFinal").change(function () {
    if (this.checked) {
        //Do stuff     
        $("#cliente_Identificacionxx").val('99999999999999');
        $("#cliente_Nombresxx").val('CONSUMIDOR FINAL');
        valido = document.getElementById('label_identificacion');
        valido.innerText = "RTN/Identificación";
        document.getElementById("fact_AlCredito").disabled = true;
        document.getElementById("fact_AutorizarDescuento").disabled = true;
    }
    else {
        $("#cliente_Nombresxx").val('');
        $("#cliente_Identificacionxx").val('');
        $("#clte_Id").val('');
        document.getElementById("MostrarTerceraEdad").disabled = false;
        $("#MostrarTerceraEdad").prop("checked", false);
        $("#fact_AutorizarDescuento").prop("checked", false);
        $('#Cred2').hide();
        ///Aqui tengo que borrar variables de session
        document.getElementById("fact_AlCredito").disabled = false;
        document.getElementById("fact_AutorizarDescuento").disabled = false;
    }
})

$("#consumidorFinal").change(function () {
    if (this.checked) {
        $('#ConsuFinal').modal('show');
    }
    else {

        $('#ConsuFinal').modal('hide');
    }
});



$("#fact_NombresTE").change(function () {
    var str = $("#fact_NombresTE").val();
    var res = str.toUpperCase();
    $("#fact_NombresTE").val(res);
});

function format(input) {
    $(input).change(function () {
        var str = $(input).val();
        var res = str.toUpperCase();
        $(input).val(res);
    });
    $(input).on("keypress", function () {
        $input = $(this);
        setTimeout(function () {
            $input.val($input.val().toUpperCase());
        }, 0);
    })
}


$("#fact_IdentidadTE")[0].maxLength = 13;
//Validacion de numeros//
function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
}


$("#confi_Telefono").on("keypress keyup blur", function (event) {
    var Telefono = $(this).val();
    console.log(Telefono)
    if (Telefono == '') {
        $(this).val('+');
    }
    this.value = this.value.replace(/[a-záéíóúüñ#/=]+/ig, "");
});


$("#confi_Correo").blur(function () {
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


////Tercera Edad
$('#AgregarTerceraEdad').click(function () {
    var IdentidadTE = $('#fact_IdentidadTE').val();
    var Nombre = $('#fact_NombresTE').val();
    var FechaNacimiento = $('#fact_FechaNacimientoTE').val();

    if (IdentidadTE == '') {
        $('#ErrorIdentidadTECreate').text('');
        $('#ErrorNombreCreate').text('');
        $('#ErrorFechaNacimientoCreate').text('');
        $('#validationfact_IdentidadTECreate').after('<ul id="ErrorIdentidadTECreate" class="validation-summary-errors text-danger">Campo requerido</ul>');
    }
    else if (Nombre == '') {
        $('#ErrorIdentidadTECreate').text('');
        $('#ErrorNombreCreate').text('');
        $('#ErrorFechaNacimientoCreate').text('');
        $('#validationNombreTECreate').after('<ul id="ErrorNombreCreate" class="validation-summary-errors text-danger">Campo requerido</ul>');
    }
    else if (FechaNacimiento == '') {
        $('#ErrorIdentidadTECreate').text('');
        $('#ErrorNombreCreate').text('');
        $('#ErrorFechaNacimientoCreate').text('');
        $('#validationFechaNacimientoTECreate').after('<ul id="ErrorFechaNacimientoCreate" class="validation-summary-errors text-danger">Campo requerido</ul>');
    }
    else {
        var TerceraEdad = GetTerceraEdad();
        $.ajax({
            url: "/Factura/SaveTerceraEdad",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ TerceraEdadC: TerceraEdad }),
        })
        .done(function (data) {
            $('#ErrorIdentidadTECreate').text('');
            $('#ErrorNombreCreate').text('');
            $('#ErrorFechaNacimientoCreate').text('');
            //Input
            $('#fact_IdentidadTE').val();
            $('#fact_NombresTE').val();
            $('#fact_FechaNacimientoTE').val();
            $('#DescTerceraEdad').modal('hide');
            $("#MostrarTerceraEdad").prop("checked", true);
            $("#fact_AutorizarDescuento").prop("checked", true);
            $('#Cred2').show();
            GetParametro();
            function GetParametro() {
                $.ajax({
                    url: "/Factura/GetParametro",
                    method: "POST",
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({}),
                })
                .done(function (data) {
                    $.each(data, function (key, val) {
                        $('#fact_PorcentajeDescuento').val(val.par_PorcentajeDescuentoTE);
                        $('#factd_PorcentajeDescuento').val(val.par_PorcentajeDescuentoTE);
                    });

                    console.log(data)
                });
            }
            document.getElementById("MostrarTerceraEdad").disabled = true;
            document.getElementById("fact_AutorizarDescuento").disabled = true;
        });
    }
});
function GetTerceraEdad() {

    var TerceraEdad = {
        fact_IdentidadTE: $('#fact_IdentidadTE').val(),
        fact_NombresTE: $('#fact_NombresTE').val(),
        fact_FechaNacimientoTE: $('#fact_FechaNacimientoTE').val(),
    }
    return TerceraEdad
};


$('#AgregarConsumidorFinal').click(function () {

    var DatoConsumidorFinal = GetConsumidorFinal();
    $.ajax({
        url: "/Factura/ConsumidorFinal",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ConsumidorFinal: DatoConsumidorFinal }),
    })
    .done(function (data) {
        //Input
        $('#confi_Nombres').val();
        $('#confi_Telefono').val();
        $('#confi_Correo').val();
        $('#ConsuFinal').modal('hide');
    });
});
function GetConsumidorFinal() {

    var ConsumidorFinal = {
        fact_Id: $('#fact_Id').val(),
        confi_Nombres: $('#confi_Nombres').val(),
        confi_Telefono: $('#confi_Telefono').val(),
        confi_Correo: $('#confi_Correo').val()

    }
    return ConsumidorFinal
};




