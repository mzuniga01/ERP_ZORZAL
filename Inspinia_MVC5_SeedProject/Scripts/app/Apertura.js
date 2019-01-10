////Funcion denominacion
$('#mnda_Id').on("change", function () {
    GetDenominacion();
     if(this.value != "")
    {
        var data = this.value.split(" ");
        var deno = $("#fbody").find("tr");
          if (this.value == "" || this.value=="#DenominacionDetalle") {
              deno.show();
            return;
        }
          deno.hide();
          deno.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                if ($t.is(":contains('" + data[d] + "')")) {
                    return true;
                }
            }
            return false;
        })
        .show();
     }          
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
        $("#DenominacionDetalle").val('');

    }
}


$(document).on("change", "#DenominacionDetalle tbody tr td input#name", function () {
    var Cantidad = $(this).val();
    var total = parseFloat(document.getElementById("Total").innerHTML);
    var ValorDenominacion = $(this).parents("tr").find("td")[2].innerHTML;
    var Subtotal = Cantidad * ValorDenominacion;
    console.log(Cantidad)
    console.log(ValorDenominacion)
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
        document.getElementById("Total").innerHTML = parseFloat(total) + parseFloat(Subtotal);
    }

});

$(document).on("keypress", "#DenominacionDetalle tbody tr td input#name", function () {
    var Cantidad = $(this).val();
    var total = parseFloat(document.getElementById("Total").innerHTML);
    var ValorDenominacion = $(this).parents("tr").find("td")[2].innerHTML;
    var Subtotal = Cantidad * ValorDenominacion;

    console.log(Cantidad)
    console.log(ValorDenominacion)
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
        document.getElementById("Total").innerHTML = parseFloat(total) + parseFloat(Subtotal);
    }

});


