//var contador = 0;
//$('#deno_Id').click(function () {
//    //var Denominacion = $('#deno_Id').val();

//    //if (Denominacion == '') {
//    //    $('#ErrorDenimnacionCreate').text('');
//    //    $('#validacionDenominacionCreate').after('<ul id="ErrorDenominacionCreate" class="validation-summary-errors text-danger">Indique una demonación requerido</ul>');

//    //}
//    //else {


//    //}
//    console.log("Hola María Alejandra, Feliz Navidad");
//});
$('#deno_Id').on("change", function () {
    GetDenominacion();
});
function GetDenominacion() {
    var IdDenominacion = $('#deno_Id').val();
    if (IdDenominacion != "") {
        $.ajax({
            url: "/MovimientoCaja/GetDenominacion",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ IdDenominacion: IdDenominacion }),
        })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (key, val) {
                    var Cantidad = $('#Cantidad').val();
                    $("#Valor").val(data);
                    var Subtotal = Cantidad * data;
                    $("#Subtotal").val(Subtotal);

                });
            }
            else {
               
            }
        });
    }
    else
    {
        $("#Valor").val('');
        $("#Cantidad").val('');
        $("#Subtotal").val('');
    }
}


$('#Cantidad').on("keypress keyup blur", function (event) {
    var Cantidad = $('#Cantidad').val();
    var Valor = $('#Valor').val();
    var Subtotal = Cantidad * Valor;
    $("#Subtotal").val(Subtotal);
});

$("#Cantidad").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});