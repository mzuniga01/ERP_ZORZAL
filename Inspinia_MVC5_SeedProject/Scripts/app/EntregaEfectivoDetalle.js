//$('#btnGuardarEntregaEfectivo').click(function () {
//    var SolicitudEfectivoEntrega = $('#solef_Id').val();
//    var CantidadEntregada = $('#soled_CantidadEntregada').val();
//    var MontoEntregado = $('#soled_MontoEntregado').val();

//    console.log("SolicitudEfectivoEntrega:", SolicitudEfectivoEntrega);
//    console.log("CantidadEntregada:", CantidadEntregada);
//    console.log("MontoEntregado:", MontoEntregado);
//});


//$(document).ready(function () {
//    $("#EntregaEfectivo tbody tr").each(function () {
//        //Id Entrega Efectivo
//        var ID = $(this).children("td:eq(1)").text();
//        var CantidadEntregada = $(this).children("td:eq(5)").text();
//        console.log("Cantidad:", CantidadEntregada);
//        console.log("ID:",ID);
//    });
//});


$(document).on("change", "#EntregaEfectivo tbody tr td input#CantidadE", function () {
    var row = $(this).closest("tr");
    var Cantidad = $(this).val();
    console.log("Cantidad:", Cantidad);
    var Subtotal = $(this).parents("tr").find("td")[6].innerHTML;
    console.log("Subtotal:", Subtotal);
    $("#EntregaEfectivo tbody tr").each(function (index) {
        var ID = $(this).children("td:eq(1)").text();
        console.log("ID:", ID);
            Monto = $(this).children("td:eq(6)").html();
            console.log('Monto', Monto);
           
        });
});