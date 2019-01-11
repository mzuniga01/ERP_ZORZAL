//$(document).ready(function () {
//    var Estado = $('#esfac_Id').val();
//    document.getElementById("esfac_Id").disabled = true;

//    if (Estado == 1) {
//        $('#bottonAnular').hide();
//        document.getElementById("esfac_Id").disabled = true;
//        document.getElementById("fechafacturaEdit").disabled = true;
//        document.getElementById("clte_Identificacion").disabled = true;
//        document.getElementById("fact_AlCredito").disabled = true;
//        document.getElementById("clte_Nombres").disabled = true;
//        document.getElementById("fact_AutorizarDescuento").disabled = true;
//        document.getElementById("fact_Vendedor").disabled = true;
//        document.getElementById("AddCliente").disabled = true;
//        document.getElementById("btnSave").disabled = true;
//        document.getElementById("AddProducto").disabled = true;
//    }
//});

function AnularDevolucion() {
   
    var CodDevolucion = $('#dev_Id').val();
    var Estado = 1
    $.ajax({
        url: "/Devolucion/AnularDevolucion",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodDevolucion: CodDevolucion, Estado: Estado }),
        
    })
    .done(function (data) {
        if (data.length > 0) {
            alert("Registro No Actualizado");
        }
        else {
            var url = $("#Redireccionar").val();
            location.href = url;
        }
    });
}
$(document).ready(function () {
    var dev_Estado = ("item_dev_Estado")
    if (dev_Estado === true) {
        document.getElementById("btnEditar").disabled = true;
    }
});

$(document).ready(function () {
    EstadoItem = $(this).closest('tr').data('estado');
    //$("#tbFactura_clte_Identifcacion").val(EstadoItem);
    var dev_Estado = (EstadoItem);
    console.log(dev_Estado)
   if (dev_Estado === true) {
        document.getElementById("btnEditar").disabled = true;
    }

    var EstadoDetalle = $('#Anulado').val();
    if (EstadoDetalle === 'Anulado') {
        document.getElementById("btnDetalleEditar").disabled = true;
        document.getElementById("bottonNotaCredito").disabled = true;
        document.getElementById("bottonAnular").disabled = true;
    }

    var DevEstado = $('#dev_Estado').val();
    console.log(DevEstado)
    if (DevEstado === 'Anulado') {
        document.getElementById("bottonAnular").disabled = true;
        document.getElementById("Detalle").disabled = true;
        document.getElementById("bottonNotaCredito").disabled = true;
        document.getElementById("Producto").disabled = true;
        document.getElementById("Guardar").disabled = true;
    }

   
});