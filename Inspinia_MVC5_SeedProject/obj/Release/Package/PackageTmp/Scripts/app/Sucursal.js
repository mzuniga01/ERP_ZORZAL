$("#suc_Telefono").on("keypress keyup blur", function (event) {
    var Telefono = $(this).val();

    if (Telefono == '') {
        $(this).val('+');
    }
    this.value = this.value.replace(/[a-záéíóúüñ#/=]+/ig, "");
});

$(document).ready(function () {
    $('#Sucursal').DataTable(
    {
        "searching": true,

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


////Get Municipio
$(document).on("change", "#dep_Codigo", function () {
    GetMunicipios();
});

function GetMunicipios() {
    var CodDepartamento = $('#dep_Codigo').val();
    $.ajax({
        url: "/Sucursal/GetMunicipios",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodDepartamento: CodDepartamento }),
    })
    .done(function (data) {
        if (data.length > 0) {
            $('#mun_Codigo').empty();
            $('#mun_Codigo').append("<option value=''>Seleccione Municipio</option>");
            $.each(data, function (key, val) {
                $('#mun_Codigo').append("<option value=" + val.mun_Codigo + ">" + val.mun_Nombre + "</option>");
            });
            console.log(mun_Codigo)
            $('#mun_Codigo').trigger("chosen:updated");
        }
        else {
            $('#mun_Codigo').empty();
            $('#mun_Codigo').append("<option value=''>Seleccione Municipio</option>");
        }
    });
}

//Fin


    
$(document).ready(function () {

    var depto = $('#dep_Codigo').val();
    if (depto === '') {
        document.getElementById("mun_Codigo").disabled = true;
    }
    else {

    }
    var x = document.getElementById("mun_Codigo").disabled;
    if (x == true) {
        document.getElementById("mun_Codigo").title = "Seleccione primero un departamento";
    }
    else {
    }
});

$("#dep_Codigo").change(function () {


    var depto = $('#dep_Codigo').val();
    if (depto === '') {
        document.getElementById("mun_Codigo").disabled = true;
    }
    else {
        document.getElementById("mun_Codigo").disabled = false;
    }
    var x = document.getElementById("mun_Codigo").disabled;
    if (x == true) {

    }
    else {
        document.getElementById("mun_Codigo").title = "";
    }
});



$('#suc_Correo').change(function (e) {
    var emailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var EmailId = this.value;
    if (emailRegex.test(EmailId)) {
        $('#ErrorCorreo').text('');
        this.style.backgroundColor = "";
    }
    else {

        $('#ErrorCorreo').text('');
        $('#MessageForCorreo').after('<ul id="ErrorCorreo" class="validation-summary-errors text-danger">Correo Electronico Es Incorrecto </ul>');
        $("#suc_Correo").focus();
    }
});

function controlCaracteres(e) {

    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ1234567890# ,.]+$/.test(tecla);

}