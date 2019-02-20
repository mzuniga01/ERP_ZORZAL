
$("#guardar").click(function FacturaPedido() {
    var CodFactura = $("#fact_Id").val();
    var CodPedido = $("#ped_Id").val();
    if (CodPedido == "") {

    }
    else {
        $.ajax({
            url: "/Factura/FacturaPedido",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodFactura: CodFactura, CodPedido: CodPedido }),
        })
    }
   

})