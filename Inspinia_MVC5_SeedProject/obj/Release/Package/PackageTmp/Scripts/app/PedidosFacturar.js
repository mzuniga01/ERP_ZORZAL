$(document).ready(function () {
    var idpedido = '<%= Session["DETALLE"] ?? "" %>';
    console.log(idpedido)
})

    //var pedido = $('#tbFactura_clte_Identificacion').val();
    //console.log(pedido)
    //function GetDetallePedido(pedido) {
    //    $.ajax({
    //        url: "/Pedido/GetDetallePedido",
    //        method: "POST",
    //        dataType: 'json',
    //        contentType: "application/json; charset=utf-8",
    //        data: JSON.stringify({ pedido: pedido }),

    //        error: function () {
            
    //            alert("No se puede filtrar");
    //        },
    //        success: function (list) {
    //            $('#PedFact').empty();
    //            $.each(list, function (key, val) {
    //                contador = contador + 1;
    //                var myDate = "/Date(1547704800000)/";
    //                var jsDate = new Date(parseInt(myDate.replace(/\D/g, '')))

    //                //var date = new Date(parseInt(val.FactFecha.substr(6)));
    //                //val.FactFecha = new Date(parseInt(val.FactFecha.replace("/Date(", "").replace(")/", ""), 10));
    //                copiar = "<tr data-id=" + contador + " data-codigo=" + val.FactCodigo + " data-idfact=" + val.FactId + ">";
    //                copiar += "<td id = 'codigo'>" + val.FactCodigo + "</td>";
    //                copiar += "<td id = 'b'>" + val.jsDate + "</td>";
    //                copiar += "<td id = 'data-DescItem'>" + val.CtleRTN + "</td>";
    //                copiar += "<td id = 'ClienteItem'>" + val.Nombre + "</td>";
    //                copiar += "<td>" + '<button id="AgregarFactura" class="btn btn-primary btn-xs" type="button">Añadir</button>' + "</td>";
    //                copiar += "</tr>";
    //                $('#PedFact').append(copiar);
    //            });
    //            console.log(list);
    //        }


    //    });

   //* }