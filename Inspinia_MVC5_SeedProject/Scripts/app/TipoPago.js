$(document).ready(function () {

    $('#Efectivo').hide();
    $('#TCD').hide();

});

$(document).ready(function () {
    $("#TipoPago").change(function (evt) {
        if ($("#TipoPago").val() == 1 )
        {
            $('#Efectivo').show();
            $('#TCD').hide();
        }
        else if ($("#TipoPago").val() == 2) {
            $('#Efectivo').hide();
            $('#TCD').show();
        }
        else {
            $('#Efectivo').hide();
            $('#TCD').hide();
        }
       
    });
});