$(document).ready(function () {
    $('#DataTable1').DataTable(
    {
        "searching": false,

        "oLanguage": {
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior",
            },
            "sEmptyTable": "No hay registros",
            "sInfoEmpty": "Mostrando 0 de 0 Entradas",
            "sSearch": "Buscar",
            "sLengthMenu": "Mostrar _MENU_ registros por página",
            "sInfo": "Mostrando _START_ a _END_ Entradas",
        }
    });
});

$("#fact_NombresTE").change(function () {
    var str = $("#fact_NombresTE").val();
    var res = str.toUpperCase();
    $("#fact_NombresTE").val(res);
});

$("#fact_NombresTE").on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})


$('#fact_IdentidadTE').on('input', function (e) {
    if (!/^[ a-z0-9]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^ a-z0-9áéíóúüñ]+/ig, "");
    }
});


//Validacion de numeros//
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
        }, 50);
    })
}

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
            $('#fact_PorcentajeDescuento').val('');
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


