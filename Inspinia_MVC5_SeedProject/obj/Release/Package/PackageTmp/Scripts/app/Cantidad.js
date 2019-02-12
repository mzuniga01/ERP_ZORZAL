////Get Municipio
//$(document).on("change", "#factd_Cantidad", function () {
//    GetCantidad();
//});
//$("#factd_Cantidad").blur(function () {
//    GetCantidad();

//});
$("#factd_Cantidad").on("blur", function (event) {
    GetCantidad();
});

function GetCantidad() {
    var CodSucursal = $('#suc_Id').val();
    console.log(CodSucursal)
    var CodProducto = $('#prod_Codigo').val();
    console.log(CodProducto)
    var CantidadIngresada = $('#factd_Cantidad').val();
    console.log(CantidadIngresada)


    $.ajax({
        url: "/Factura/GetCantidad",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodSucursal: CodSucursal, CodProducto: CodProducto}),
    })
    .done(function (data) {

        if (data.length > 0) {
           $.each(data, function (key, val) {
            var MENSAJE = data[0]['MENSAJE'];
            console.log(MENSAJE)

               if (MENSAJE) {
                   var can = data[0]['CANTIDAD'];
                   var CANTIDAD = parseFloat(can)
                   console.log(CANTIDAD)
                   if (CANTIDAD < CantidadIngresada) {
                       alert('La cantidad de productos no esta disponible, Cantidad disponible: ' + CANTIDAD)
                       $('#factd_Impuesto').val(0.00);
                       $('#factd_Cantidad').val(''),
                       $("#SubtotalProducto").val(0.00),
                       $("#factd_PorcentajeDescuento").val(0.00),
                       $("#TotalProducto").val(0.00)
                       $('#Impuesto').val(0.00);
                       $('#factd_Cantidad').val('');
                   }
                   else if (CANTIDAD == 10) {
                       alert('Pocos productos en exitencia, cantidad existente: ' + CANTIDAD)
                   } else {
                       document.getElementById("AgregarDetalleFactura").disabled = false;
                   }

               }
               else {
                   var can = data[0]['CANTIDAD'];
                   var CANTIDAD = parseFloat(can)
                   alert('No hay productos en existencia')
                   $('#factd_Cantidad').val('');
                   $('#factd_MontoDescuento').val('');
                   $('#factd_Cantidad').val('');
                   $('#SubtotalProducto').val('');
                   $('#Impuesto').val('');
                   $('#TotalProducto').val('');                   
               }   
           });
        }
        else {

        }
    });
}

//Fin