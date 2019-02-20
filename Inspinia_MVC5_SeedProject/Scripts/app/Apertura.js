var total = 0;
var MontoInicial = 0;
var Monto = 0;
////Funcion denominacion
$('#mnda_Id').on("change", function () {
    total = 0;
    MontoInicial = 0;
    Monto = 0;
    GetDenominacion();
     $(function () {
         var $tabla = $('#DenominacionDetalle');
         $('#mnda_Id').change(function () {
             var value = $(this).val();
             if (this.value != "") {
                 $('tbody tr.' + value, $tabla).show();
                 $('tbody tr:not(.' + value + ')', $tabla).hide();
                 $('#SuntotalCreate').val('');
                 $('#MontoInicial').val('');
                 document.getElementById('Total').innerHTML = '';
             }
             else {
                 // Se ha seleccionado todo
                 $('#DenominacionDetalle > tbody').empty();
                 $('tbody tr', $tabla).show();
             }
         });
     });
});

function GetDenominacion() {
    var CodMoneda = $('#mnda_Id').val();
    if (CodMoneda != "") {
        $.ajax({
            url: "/MovimientoCaja/GetDenominacion",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodMoneda: CodMoneda }),
        })
        .done(function (data) {
            $("#DenominacionDetalle").append('');
            if (data.length > 0) {
                var contador = 0;
                $.each(data, function (key, val) {
                    contador = contador + 1;
                    copiar = "<tr data-id=" + contador + ">";
                    copiar += "<td id = 'DenominacionCreate'>"  + val.deno_Descripcion + "</td>";
                    copiar += "<td>" + '<input type="number" min="0" id="name" name="name" class="form-control" size="3">' + "</td>";
                    copiar += "<td id = 'ValorCreate'>" + val.deno_valor + "</td>";
                    copiar += "<td id = 'SuntotalCreate'></td>";
                    copiar += "<td id = 'deno_Id' hidden>" + val.deno_Id + "</td>";
                    copiar += "<td id = 'mnda_Id' hidden>" + val.mnda_Id + "</td>";                 
                    copiar += "</tr>";
                    $('#DenominacionDetalle').append(copiar);

                });
            }
            else {

            }
        });
    }
    else {


    }
}


//////Calculos
$(document).on("change", "#DenominacionDetalle tbody tr td input#name", function () {
    var row = $(this).closest("tr");
    var Cantidad = $(this).val();
    var ValorDenominacion = $(this).parents("tr").find("td")[2].innerHTML;
    var Subtotal = parseFloat(Cantidad * ValorDenominacion);
    $(this).parents("tr").find("td")[3].innerHTML = Subtotal;
    if (Subtotal != 0) {
        MontoInicial += Subtotal;
    }
    else
    {
        MontoInicial = 0;
        $("#DenominacionDetalle tbody tr").each(function (index) {
            Monto = $(this).children("td:eq(3)").html();
            if (Monto != '')
            {
                Monto = parseFloat(Monto);
                MontoInicial += Monto;
            }
        })
    }
    var totalfinal = document.getElementById("Total").innerHTML = parseFloat(MontoInicial);
    $("#Monto").val(parseFloat(MontoInicial));
    console.log('MontoInicial', MontoInicial);
    console.log('Subtotal', Subtotal);
    console.log('Total', Total);
    console.log('totalfinal', totalfinal);
   
});

