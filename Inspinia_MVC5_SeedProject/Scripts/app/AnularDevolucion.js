$(document).ready(function () {
    var Estado = $('#esfac_Id').val();
    document.getElementById("esfac_Id").disabled = true;

    if (Estado == 1) {
        $('#bottonAnular').hide();
        document.getElementById("esfac_Id").disabled = true;
        document.getElementById("fechafacturaEdit").disabled = true;
        document.getElementById("clte_Identificacion").disabled = true;
        document.getElementById("fact_AlCredito").disabled = true;
        document.getElementById("clte_Nombres").disabled = true;
        document.getElementById("fact_AutorizarDescuento").disabled = true;
        document.getElementById("fact_Vendedor").disabled = true;
        document.getElementById("AddCliente").disabled = true;
        document.getElementById("btnSave").disabled = true;
        document.getElementById("AddProducto").disabled = true;
    }
});

function AnularDevolucion() {
    var CodDevolucion = $('#dev_Id').val();
    var Estado = 1
    $.ajax({
        url: "/Factura/AnularFactura",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodDevolucion: CodDevolucion, Estado: Estado }),

    })
    .done(function (data) {
        if (data.length > 0) {
            var url = $("#RedirectTo").val();
            location.href = url;
        }
        else {
            alert("Registro No Actualizado");
        }
    });
}
