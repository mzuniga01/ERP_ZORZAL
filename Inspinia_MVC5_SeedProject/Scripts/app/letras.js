$(document).ready(function ()
{
    $("#dep_Nombre")[0].maxLength = 50;

})

$("#dep_Nombre").change(function () {
    var str = $("#dep_Nombre").val();
    var res = str.toUpperCase();
    $("#dep_Nombre").val(res);
});

$(dep_Nombre).on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})


$(document).ready(function () {
    $("#dep_Nombre")[0].maxLength = 50;

})

$("#mun_Nombre").change(function () {
    var str = $("#mun_Nombre").val();
    var res = str.toUpperCase();
    $("#mun_Nombre").val(res);
});

$(mun_Nombre).on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})