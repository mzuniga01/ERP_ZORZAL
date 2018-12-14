//var contador = 0;
//$('#deno_Id').click(function () {
//    //var Denominacion = $('#deno_Id').val();

//    //if (Denominacion == '') {
//    //    $('#ErrorDenimnacionCreate').text('');
//    //    $('#validacionDenominacionCreate').after('<ul id="ErrorDenominacionCreate" class="validation-summary-errors text-danger">Indique una demonación requerido</ul>');

//    //}
//    //else {
//    //    contador = contador + 1;
//    //    copiar = "<tr data-id=" + contador + ">";
//    //    copiar += "<td>" + $('#deno_Id option:selected').text() + "</td>";
//    //    copiar += "<td hidden id='deno_IdCreate'>" + $('#deno_Id option:selected').val() + "</td>";
//    //    copiar += "</tr>";
//    //    $('#DenominacionDetalle').append(copiar);

//    //}
//    console.log("Hola María Alejandra, Feliz Navidad");
//});

$("#deno_Id").change(function () {
    console.log("Hola María Alejandra, Feliz Navidad");
    var Denominacion = $('#deno_Id').val();
    console.log("Denominacion", Denominacion);
    if (Denominacion != "") {
        $.ajax({
            url: "/Instructor/GetDenominacionArqueo",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodDepartamento: CodDepartamento }),
        })


    }
    else
    {
        //$('#DenominacionDetalle').empty();
    }
});
