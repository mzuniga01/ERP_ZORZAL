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
$(document).on("change", "deno_Id", function () {
    GetDenominacion();
    console.log("Entre")
});

function GetDenominacion() {
    var Denominacion = $('#deno_Id').val();
    console.log("Denominacion", Denominacion);
    if (Denominacion != "") {
        $.ajax({
            url: "/MovimientoCaja/GetDenominacion",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Denominacion: Denominacion }),
        })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (key, val) {
                    //document.getElementById("Valor").val = ;
                    $("#Valor").val(deno_valor);
                    console.log(data)
                });
            }
            else {
                $('#deno_Id').empty();
                $("#Valor").val('');
            }
        });
    }
    else
    {
                contador = contador + 1;
                copiar = "<tr data-id=" + contador + ">";
                copiar += "<td>" + $('#deno_Id option:selected').text() + "</td>";
                copiar += "<td hidden id='deno_IdCreate'>" + $('#deno_Id option:selected').val() + "</td>";
                copiar += "</tr>";
                $('#DenominacionDetalle').append(copiar);
    }
}