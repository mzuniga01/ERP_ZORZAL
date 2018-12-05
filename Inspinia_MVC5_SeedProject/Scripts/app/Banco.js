//$("#ban_Nombre").change(function () {
//    var str = $("#ban_Nombre").val();
//    var res = str.toUpperCase();
//    $("#ban_Nombre").val(res);

//    document.getElementById("ban_Nombre").value = document.getElementById("ban_Nombre").value.toUpperCase();


//});


$(document).ready(function () {
    $(ban_Nombre).on("keypress", function () {
        $input = $(this);
        setTimeout(function () {
            $input.val($input.val().toUpperCase());
        }, 50);
    })
})


