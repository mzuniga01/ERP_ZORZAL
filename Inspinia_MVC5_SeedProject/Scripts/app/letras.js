$(document).ready(function ()
{
    $("#dep_Nombre")[0].maxLength = 30;
    $("#dep_Nombre")[0].minLength = 30;
    $("#dep_Codigo")[0].maxLength = 2;
    $("#dep_Codigo")[0].minLength = 2;
})

$("#dep_Nombre").change(function () {
    var str = $("#dep_Nombre").val();
    var res = str.toUpperCase();
    $("#dep_Nombre").val(res);
});

$("#dep_Nombre").on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})

$("#mun_Nombre").change(function () {
    var str = $("#mun_Nombre").val();
    var res = str.toUpperCase();
    $("#mun_Nombre").val(res);
});

$("#mun_Nombre").on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})
var codigo = $("#mun_Codigo").val();

//MODAL
$("#MunNombre_" + codigo).change(function () {
    console.log('Hola8');
    var str = $("#MunNombre_" + codigo).val();
    var res = str.toUpperCase();
    $("#MunNombre_" + codigo).val(res);
});

$("#MunNombre").on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})

function MunicipioUpper(Parametro)
{
    console.log("Parametro", Parametro);
    //var str = $("#MunNombre_" + Parametro).val();
    //console.log("str", str);
    //var res = str.toUpperCase();
    //$("#MunNombre_" + Parametro).val(res);
}

$(document).ready(function () {
    //QUE SOLO ACEPTE 4 NUMEROS
    $("#mun_Codigo")[0].maxLength = 4;
    $("#mun_Codigo")[0].minLength = 4;
    $("#mun_Nombre")[0].maxLength = 30;
    $("#mun_Nombre")[0].minLength = 30;


    //VALIDAR SOLO NUMEROS
    $('#mun_Codigo').bind('keypress', function (event) {
        var regex = new RegExp("^[0-9]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    $('#dep_Codigo').bind('keypress', function (event) {
        var regex = new RegExp("^[0-9]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    //VALIDAR SOLO LETRAS
    $('#mun_Nombre').on('input', function (e) {
        if (!/^[ a-záéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ a-záéíóúüñ]+/ig, "");
        }
    });
    $('#dep_Nombre').on('input', function (e) {
        if (!/^[ a-záéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ a-záéíóúüñ]+/ig, "");
        }
    });
    //Copiar y Pegar//
    $(document).ready(function () {
        $('#dep_Codigo').bind("cut copy paste", function (e) {
            e.preventDefault();
        });
    });
    $(document).ready(function () {
        $('#mun_Codigo').bind("cut copy paste", function (e) {
            e.preventDefault();
        });
    });
})

