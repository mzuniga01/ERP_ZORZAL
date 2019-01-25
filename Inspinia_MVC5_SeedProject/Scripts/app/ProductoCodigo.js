$(document).on("change", "#pcat_Id", "#pscat_Id", function () {
    GetValue();
});

function GetValue() {
    var pcat_Id = $('#pcat_Id').val();
    var pscat_Id = $('#pscat_Id').val();
    $.ajax({
        url: "/Producto/GetValue",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ pcat_Id: pcat_Id, pscat_Id: pscat_Id }),       
    })
    .done(function (data) {
        if (data.length > 0) {
            $('#prod_Codigo').empty();           
            $.each(data, function (key, val) {
                $('#prod_Codigo').append("<option value=" + val.prod_Codigo + ">" + val.prod_Codigo + "</option>");
            });         
            $('#prod_Codigo').trigger("chosen:updated");
        }
        else {
            $('#prod_Codigo').empty();
            $('#prod_Codigo').append("<option value=''>error/option>");
        }
    });
}

