////Funcion denominacion
$('#mnda_Id').on("change", function () {
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
                 //$('#Total').val('');
                 document.getElementById('Total').innerHTML = '';
             }
             else {
                 // Se ha seleccionado All
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
                    copiar += "<td id = 'DenominacionCreate'>" + val.deno_Descripcion + "</td>";
                    copiar += "<td>" + '<input type="text" id="name" name="name" class="form-control" size="3">' + "</td>";
                    copiar += "<td id = 'ValorCreate'>" + val.deno_valor + "</td>";
                    copiar += "<td id = 'SuntotalCreate'></td>";
                    //copiar += "<td>" + '<button id="removeDenominacion" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
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


$(document).on("change", "#DenominacionDetalle tbody tr td input#name", function () {
    var Cantidad = $(this).val();
    var ValorDenominacion = $(this).parents("tr").find("td")[2].innerHTML;
    var Subtotal = parseFloat(Cantidad * ValorDenominacion).toFixed(2).replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");
    var total = parseFloat(document.getElementById("Total").innerHTML);
    console.log('Cantidad')
    console.log(Cantidad)
    console.log('Valor')
    console.log(ValorDenominacion)
    console.log('SubTotal')
    console.log(Subtotal)
    console.log('Total')
    console.log(Total)

    $(this).parents("tr").find("td")[3].innerHTML = Subtotal;
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
    //Grantotal
    if (document.getElementById("Total").innerHTML == '') {
        document.getElementById("Total").innerHTML = parseFloat(0);
    }
    else {
        var MontoInicial = document.getElementById("Total").innerHTML = (parseFloat(total) + parseFloat(Subtotal)).toFixed(2).replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");
        document.getElementById('MontoInicial').value = MontoInicial;
    }

});

$(document).on("keypress", "#DenominacionDetalle tbody tr td input#name", function () {
    var Cantidad = $(this).val();
    var ValorDenominacion = $(this).parents("tr").find("td")[2].innerHTML;
    var Subtotal = parseFloat(Cantidad * ValorDenominacion).toFixed(2).replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");
    var Cantidad = $(this).val();
    var total = parseFloat(document.getElementById("Total").innerHTML);

    $(this).parents("tr").find("td")[3].innerHTML = Subtotal;
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
    //Grantotal
    if (document.getElementById("Total").innerHTML == '') {
       document.getElementById("Total").innerHTML = parseFloat(0);
    }
    else {
        var MontoInicial = document.getElementById("Total").innerHTML = (parseFloat(total) + parseFloat(Subtotal)).toFixed(2).replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");
        document.getElementById('MontoInicial').value = MontoInicial;
    }
});




////////////////////////////////////////////////////////////////////////////////




