var contador = 0;
$('#AgregarDetalleDenominacion').click(function () {
    var Denominacion = $("#deno_Id option:selected").text()
    var Cantidad = $('#Cantidad').val();    
    var Valor = $('#Valor').val();
    var Subtotal = $('#Subtotal').val();

    if (Denominacion == "" || Denominacion == "Seleccione Denominación")
    {
        $('#MensajeErrorCantidad').text('');
        $('#MensajeErrorDenominacion').text('El campo denominacion es requerido');
    }
    else if (Cantidad == "") {
        $('#MensajeErrorDenominacion').text('');
        $('#MensajeErrorCantidad').text('El campo denominacion es requerido');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'DenominacionCreate'>" + Denominacion + "</td>";
        copiar += "<td id = 'CantidadCreate'>" + Cantidad + "</td>";
        copiar += "<td id = 'ValorCreate'>" + Valor + "</td>";
        copiar += "<td id = 'SuntotalCreate'>" + Subtotal + "</td>";
        copiar += "<td>" + '<button id="removeDenominacion" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#DenominacionDetalle').append(copiar);       
    }

    //Total 
    var totalDenominacion = $('#Subtotal').val();
    var sutotal = parseFloat(document.getElementById("Total").innerHTML);

    if (document.getElementById("Total").innerHTML == '') {
        totalDenominacion = $('#Subtotal').val();
        document.getElementById("Total").innerHTML = parseFloat(totalDenominacion);
        $('#deno_Id').val('');
        $('#Cantidad').val('');
        $('#Valor').val('');
        $('#Subtotal').val('');
    }
    else {
        document.getElementById("Total").innerHTML = parseFloat(sutotal) + parseFloat(totalDenominacion);
        $('#deno_Id').val('');
        $('#Cantidad').val('');
        $('#Valor').val('');
        $('#Subtotal').val('');
    }
});

$(document).on("click", "#DenominacionDetalle tbody tr td button#removeDenominacion", function () {
    var totalDenominacion = $(this).parents("tr").find("td")[3].innerHTML;
    console.log(totalDenominacion)
    var subtotal = parseFloat(document.getElementById("Total").innerHTML);
    console.log(subtotal)
    document.getElementById("Total").innerHTML = parseFloat(subtotal) - parseFloat(totalDenominacion);
    $(this).closest('tr').remove();
    contador = $(this).closest('tr').data('id');
});