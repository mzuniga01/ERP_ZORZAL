$(document).ready(function ()
{
    $("#ban_Nombre")[0].maxLength = 50;
    $("#ban_NombreContacto")[0].maxLength = 50;
    $("#ban_TelefonoContacto")[0].maxLength = 25;
    $("#ban_Nombre").attr("autocomplete", "randomString");
    $("#ban_NombreContacto").attr("autocomplete", "randomString");
})
