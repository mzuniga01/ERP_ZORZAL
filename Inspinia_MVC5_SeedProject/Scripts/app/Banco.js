$("#ban_Nombre").change(function () {
    var str = $("#ban_Nombre").val();
    var res = str.toUpperCase();
    $("#ban_Nombre").val(res);
});

$(ban_Nombre).on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})

//$(document).ready(function () {
//    $('#ban_TelefonoContacto').mask('(00) 0000-0000');
//});