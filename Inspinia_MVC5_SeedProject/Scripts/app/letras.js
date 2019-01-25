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