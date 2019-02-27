$(document).ready(function () {
    $("#tpa_Descripcion")[0].maxLength = 50;
})

$("#tpa_Descripcion").change(function () {
    var str = $("#tpa_Descripcion").val();
    var res = str.toUpperCase();
    $("#tpa_Descripcion").val(res);
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


