$(document).ready(function () {
    GetDetalle()
});
var contador = 0;
function GetDetalle() {
    var factID = $("#fact_Id").val();
    $.ajax({
        url: "/Factura/GetFacturaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ factID:factID }),
    })
      .done(function (data) {
          $.each(data, function (key, val) {
              Descuento = (val.factd_MontoDescuento * val.factd_Cantidad);
              ImpuestoTotal = ((val.factd_Impuesto / 100) * val.factd_PrecioUnitario) * val.factd_Cantidad;
              Subtotal = ((val.factd_Cantidad * val.factd_PrecioUnitario) + ImpuestoTotal);
              GranSubtotal = (val.factd_Cantidad * val.factd_PrecioUnitario);
              Total = (GranSubtotal + ImpuestoTotal) - Descuento;
              contador = contador + 1;
              copiar = "<tr data-id=" + contador + ">";
              copiar += "<td id = 'prod_CodigoCreate'>" + val.prod_Codigo + "</td>";
              copiar += "<td id = 'tbProducto_prod_DescripcionCreate'>" + val.prod_Descripcion + "</td>";
              copiar += "<td id = 'factd_CantidadCreate' align='right'>" + val.factd_Cantidad + "</td>";
              copiar += "<td id = 'Precio_UnitarioCreate' align='right'>" + val.factd_PrecioUnitario + "</td>";
              copiar += "<td id = 'ImpuestoCreate' align='right'>" + val.factd_Impuesto + "</td>";
              copiar += "<td id = 'factd_MontoDescuentoCreate' align='right'>" + val.factd_MontoDescuento + "</td>";
              copiar += "<td id = 'TotalProductoCreate' align='right'>" + Subtotal + "</td>";
              copiar += "</tr>";
              $('#tblDetalleFactura').append(copiar);
              document.getElementById("TotalDescuento").innerHTML = parseFloat(Descuento) ;
              document.getElementById("Subtotal").innerHTML = parseFloat(GranSubtotal);
              document.getElementById("isv").innerHTML = parseFloat(ImpuestoTotal);
              document.getElementById("total").innerHTML = parseFloat(Total);
          });         

  })
}
