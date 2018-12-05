$("#ban_Nombre").change(function () {
    var str = $("#ban_Nombre").val();
    var res = str.toUpperCase();
    $("#ban_Nombre").val(res);
});