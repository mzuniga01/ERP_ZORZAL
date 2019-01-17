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
        GetIDCliente(idItem);
    }
    });
});

var CodCliente = $('#tbFactura_clte_Identificacion').val();
console.log(CodCliente)
function GetIDCliente(CodCliente, idItem) {
    $.ajax({
        url: "/Devolucion/FiltrarModal",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodCliente: CodCliente }),

        error: function () {
            console.log("si entrafiltrar1");
            alert("No se puede filtrar");
        },
        success: function (list) {
            $('#BodyFactura').empty();
            $.each(list, function (key, val) {
                contador = contador + 1;
                copiar = "<tr data-id=" + contador + ">";
                copiar += "<td id = 'idItem'>" + val.FactCodigo + "</td>";
                copiar += "<td id = 'b'>" + val.FactFecha + "</td>";
                copiar += "<td id = 'c'>" + val.CtleRTN + "</td>";
                copiar += "<td id = 'd'>" + val.Nombre + "</td>";
                copiar += "<td>" + '<button id="AgregarFactura" class="btn btn-primary btn-xs" type="button">Añadir</button>' + "</td>";
                copiar += "</tr>";
                $('#BodyFactura').append(copiar);
            });
            console.log(list);
        }

    });

}

//$(document).on("click", "#DevFactura tbody tr td button#AgregarFactura", function () {
//    idItem = $(this).closest('tr').data('id');
//    CodigoItem = $(this).closest('tr').data('codigo');
//    DescItem = $(this).closest('tr').data('desc');
//    ClienteItem = $(this).closest('tr').data('cliente');
//    console.log('Cliente', CodigoItem)
//    $("#tbFactura_fact_Codigo").val(idItem);
//    $("#fact_Id").val(CodigoItem);
//    $("#tbFactura_clte_Identificacion").val(DescItem);
//    $("#tbFactura_clte_Nombres").val(ClienteItem);
//    $('#ModalAgregarFactura').modal('hide');
//    //CargarAsignaciones();

//});
