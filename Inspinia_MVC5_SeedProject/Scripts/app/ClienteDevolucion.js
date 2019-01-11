//Devolucion Seleccionar Cliente
$(document).on("click", "#DataTable tbody tr td button#AgregarCliente", function () {
    idItem = $(this).closest('tr').data('id');
    descItem = $(this).closest('tr').data('desc');
    $("#tbFactura_clte_Identificacion").val(idItem);
    $("#tbFactura_clte_Nombres").val(descItem);
    $('#ModalAgregarCliente').modal('hide');
    $(document).ready(function () {
    if (descItem != '') {
        document.getElementById("Factura").disabled = false;
        document.getElementById("tbFactura_fact_Codigo").disabled = false;
    }
    else {

    }
    });
});

//function FiltrarModal() {
//    var CodFactura = $('#tbFactura_clte_Identificacion').val();
//    console.log('modal')
//    $.ajax({
//        url: "/Devolucion/Create",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ CodFactura: CodFactura }),

//    })
//    .done(function (data) {
//        if (data.length > 0) {
//            alert("Registro No Actualizado");
//        }
//    });
//}