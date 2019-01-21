
$(document).ready(function () {
    $("#pago_TotalPago")[0].maxLength = 12;

})

$(function () {

    $("#pago_TotalPago").change(function (e) {
        var totalpagar = $("#pago_TotalPago").val();
        var saldoAnterior = $("#SaldoAnterior").val();
        var saldoActual = $("#pago_SaldoAnterior").val();
        var pagado = $("#TotalPagado").val();
        var montofactura = $("#MontoFactura").val();

       
        var monto = (parseFloat(montofactura));

        result = 0.00;

        if (totalpagar == '') {
            alert('Monto Incorrecto2');
            $("#pago_SaldoAnterior").val(saldoAnterior);
            if (isNaN($("#TotalPagado").val(pagado))) {
                $("#TotalPagado").val(0.00);
            }
            else if ($("#TotalPagado").val(pagado) > 0) {
                $("#TotalPagado").val(parseFloat(pagado) - parseFloat(pagoActual));
            }
        }
       else if (totalpagar < 0) {
           alert('Monto menor a cero');
           $("#pago_TotalPago").val('');
            $("#pago_SaldoAnterior").val(saldoAnterior);
            if (isNaN($("#TotalPagado").val(pagado))) {
                $("#TotalPagado").val(0.00);
            }
            else if ($("#TotalPagado").val(pagado) > 0) {
                $("#TotalPagado").val(parseFloat(pagado) - parseFloat(pagoActual));
            }
        }
      

        else if ( totalpagar > saldoAnterior && pagado > montofactura) {
           
           
            alert('Monto Incorrecto');
            $("#pago_SaldoAnterior").val(saldoAnterior);
            $("#TotalPagado").val(pagado - pagoActual);

        }
   

       

        else {
            var saldoActualizado = (parseFloat(saldoAnterior) - parseFloat(totalpagar));
            var pagoActual = (parseFloat(pagado) + parseFloat(totalpagar));
            $("#pago_SaldoAnterior").val(saldoActualizado);
            $("#TotalPagado").val(pagoActual);     
        }
       
     
    
   
    });
});

$(function () {

    $("#efectivo").change(function (e) {
        var efectivo = $("#efectivo").val();
        var cambio = $("#cambio").val();
        var totalpagar = $("#pago_TotalPago").val();

        var cambioefectivo = (parseFloat(efectivo) - parseFloat(totalpagar));
        result = 0;

        if (totalpagar == '' || totalpagar == 0) {

            $("#efectivo").val(result);
            $("#cambio").val(result);

        }
        else {
            $("#efectivo").val(efectivo);
            $("#cambio").val(cambioefectivo);

        }

    });
});


//Filtro de Modal Factura----------------------------------------------------------------------------

var CodCliente = $('#tbFactura_clte_Id').val();
console.log(CodCliente)
function GetIDCliente(CodCliente, idItem) {
    $.ajax({
        url: "/Pago/FiltrarModal",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodCliente: CodCliente }),

        error: function () {
            console.log("si entrafiltrar1");
            alert("No se puede filtrar");
        },
        success: function (list) {
            $('#FacturaPagoTbody').empty();
            $.each(list, function (key, val) {
                contador = contador + 1;
                var myDate = "/Date(1547704800000)/";
                var jsDate = new Date(parseInt(myDate.replace(/\D/g, '')))

                //var date = new Date(parseInt(val.FactFecha.substr(6)));
                //val.FactFecha = new Date(parseInt(val.FactFecha.replace("/Date(", "").replace(")/", ""), 10));
                copiar = "<tr data-id=" + contador + " data-codigo=" + val.Factura_Codigo + " data-id=" + val.Factura_Id + ">";
                copiar += "<td id = 'codigo'>" + val.Factura_Codigo + "</td>";
                copiar += "<td id = 'b'>" + val.jsDate + "</td>";
                copiar += "<td id = 'data-c_id'>" + val.clte_Id + "</td>";
                copiar += "<td id = 'data-monto'>" + val.Factura_Monto + "</td>";
                copiar += "<td id = 'data-pago'>" + val.Factura_Pagado + "</td>";
                copiar += "<td id = 'data-saldo'>" + val.Factura_Saldo + "</td>";
                copiar += "<td>" + '<button id="Seleccionar" class="btn btn-primary btn-xs" type="button">Añadir</button>' + "</td>";
                copiar += "</tr>";
                $('#BodyFactura').append(copiar);
            });
            console.log(list);
        }


    });

}