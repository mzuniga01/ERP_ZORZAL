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
$('#mnda_Id').on("change", function () {
    GetDenominacion();
});
function GetDenominacion() {
    var CodMoneda = $('#mnda_Id').val();
    console.log(CodMoneda)
    if (CodMoneda != "") {
        $.ajax({
            url: "/MovimientoCaja/GetDenominacion",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodMoneda: CodMoneda }),
        })
        .done(function (data) {
            if (data.length > 0) {
                var contador = 0;
                $.each(data, function (key, val) {
                    //$('#DenominacionDetalle').append("<option value=" + val.mun_Codigo + ">" + val.mun_Nombre + "</option>");
                    //$('#DenominacionDetalle').append("<tr><td>" + val.deno_Descripcion + "</td>");
                    contador = contador + 1;
                    copiar = "<tr data-id=" + contador + ">";
                    copiar += "<td id = 'DenominacionCreate'>" + val.deno_Descripcion + "</td>";
                    copiar += "<td id = 'CantidadCreate'>" + "<input type='text' id='name' name='name'>" + "</td>";
                    copiar += "<td id = 'ValorCreate'>" + val.deno_valor + "</td>";
                    copiar += "<td id = 'SuntotalCreate'>" + "<label for='name'></label>" + "</td>";
                    copiar += "<td>" + '<button id="removeDenominacion" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                    copiar += "</tr>";
                    $('#DenominacionDetalle').append(copiar);
                    console.log(copiar)
                    
                });
            }
            else {
               
            }
        });
    }
    else
    {
        //$("#Valor").val('');
        //$("#Cantidad").val('');
        //$("#Subtotal").val('');
    }
}

////var totalDenominacion = $(this).parents("tr").find("td")[3].innerHTML;
$('#name').on("keypress keyup blur", function (event) {
    var Cantidad = $('#name').val();
    console.log(Cantidad)
    var Valor = $(this).parents("tr").find("td")[2].innerHTML;
    console.log(valor)
    var Subtotal = Cantidad * Valor;
    $("#SuntotalCreate").val(Subtotal);
});

$("#Cantidad").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

$("#deno_Id").change(function () {
    var deno_Id = $("#deno_Id").val();
    if (deno_Id != '') {
        valido = document.getElementById('MensajeErrorDenominacion');
        valido.innerText = "";
    }
});

$("#Cantidad").change(function () {
    var Cantidad = $("#Cantidad").val();
    if (Cantidad != '') {
        valido = document.getElementById('MensajeErrorCantidad');
        valido.innerText = "";
    }
});