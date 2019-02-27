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


$(ban_TelefonoContacto).on("keypress keyup blur", function (event) {
    $(this).val($(this).val().replace(/[^0-9]/g, ""));
    if ((event.which != 46 || $(this).val().indexOf('') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});





