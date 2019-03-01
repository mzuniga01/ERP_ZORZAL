
////ORIGINAL

$(document).ready(function ()
{
    $("#ban_Nombre")[0].maxLength = 50;
    $("#ban_NombreContacto")[0].maxLength = 50;
    $("#ban_TelefonoContacto")[0].maxLength = 25;
})

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


$("#esfac_Descripcion").change(function () {
    var str = $("#esfac_Descripcion").val();
    var res = str.toUpperCase();
    $("#esfac_Descripcion").val(res);
});

$(esfac_Descripcion).on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})

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






