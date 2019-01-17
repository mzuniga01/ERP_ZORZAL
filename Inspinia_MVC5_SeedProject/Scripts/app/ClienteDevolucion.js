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
            alert("No se puede filtr");
        },
        success: function (list) {
        //    document.getElementById("CodigoFactura").innerHTML = list[1].FactCodigo;
        //    document.getElementById("FechaF").innerHTML = list[0].FactFecha;
        //    document.getElementById("ClienteRTN").innerHTML = list[0].CtleRTN;
        //    document.getElementById("NombreCliente").innerHTML = list[0].Nombre;

            console.log(list);
            response = list;

            //$('#DevFactura > tbody > tr').each(function () {
            //    FactCodigo = $(this).attr('data-id')
            //    FactFecha = $(this).attr('data-fecha')
            //    CtleRTN = $(this).attr('data-desc')
            //    Nombre = $(this).attr('data-cliente')
            //    console.log(FactCodigo);
            //    console.log(FactFecha);
            //    console.log(CtleRTN);
            //    console.log(Nombre);
                //idItem = FactCodigo
                //FechaItem = FactFecha
                //RTNItem = CtleRTN
                //clienteItem = Nombre

                //console.log('idItem', CtleRTN);
            //});
        }
    });


//var CodCliente = $('#tbFactura_clte_Identificacion').val();
//    console.log(CodCliente)
//    function GetIDCliente(CodCliente, idItem) {
//        $.ajax({
//            url: "/Devolucion/FiltrarModal",
//            method: "POST",
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify({ CodCliente: CodCliente }),

//        })
//        .done(function (data) {
//            console.log("si entrafiltrar1");
//            var list = data.d;
//            $.each(list, function (index, item) {
//                console.log("si entrafiltrar");
//            });
//        });

//var CodCliente = $('#tbFactura_clte_Identificacion').val();
//        $.ajax({
//            url: '@Url.Action("FiltrarModal")',
//            type: 'POST',
//            async: false,
//            data: "{'clte_Identificacion': " + CodCliente + "}",
//            dataType: 'json',
//            contentType: 'application/json; charset=utf-8',
//            error: function () {
//                alert("Server access failure!");
//            },
//            success: function (result) {
//                response = result;
//            }
//        });

        //.done(function (data) {
        //    console.log('Si entra done')
        //});
    };