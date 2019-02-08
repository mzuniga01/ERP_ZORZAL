$(document).ready(function ()
{
    $("#dep_Nombre")[0].maxLength = 50;
    $("#dep_Codigo")[0].maxLength = 2;
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
    //VALIDAR SOLO NUMEROS
    $(function () {
        $("#mun_Codigo").keydown(function (event) {
            //alert(event.keyCode);
            if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) && event.keyCode !== 190 && event.keyCode !== 110 && event.keyCode !== 8 && event.keyCode !== 9) {
                return false;
            }
        });
    });
    $(function () {
        $("#dep_Codigo").keydown(function (event) {
            //alert(event.keyCode);
            if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) && event.keyCode !== 190 && event.keyCode !== 110 && event.keyCode !== 8 && event.keyCode !== 9) {
                return false;
            }
        });
    });
    //VALIDAR SOLO LETRAS
    $('#mun_Nombre').on('input', function (e) {
        if (!/^[ a-z-áéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ a-z-áéíóúüñ]+/ig, "");
        }
    });
    $('#dep_Nombre').on('input', function (e) {
        if (!/^[ a-z-áéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ a-z-áéíóúüñ]+/ig, "");
        }
    });

})

