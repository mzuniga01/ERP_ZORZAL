$(document).ready(function () {
    GetDetalle()
});
var contador = 0;
function GetDetalle() {
    var factID = $("#fact_Id").val();
    $.ajax({
        url: "/Factura/GetFacturaDetalleD",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ factID: factID }),
    })
      .done(function (data) {
          $.each(data, function (key, val) {
              Descuento = (val.factd_MontoDescuento * val.factd_Cantidad);
              ImpuestoTotal = ((val.factd_Impuesto / 100) * val.factd_PrecioUnitario) * val.factd_Cantidad;
              Subtotal = ((val.factd_Cantidad * val.factd_PrecioUnitario) + ImpuestoTotal);              
              GranSubtotal = (val.factd_Cantidad * val.factd_PrecioUnitario);
              Total = (GranSubtotal + ImpuestoTotal) - Descuento;
              StudentId = val.factd_Id;
              contador = contador + 1;
              copiar = "<tr data-id=" + StudentId + ">";
              copiar += "<td id = 'prod_CodigoCreate'>" + val.prod_Codigo + "</td>";
              copiar += "<td id = 'tbProducto_prod_DescripcionCreate'>" + val.prod_Descripcion + "</td>";
              copiar += "<td id = 'factd_CantidadCreate' align='right'>" + val.factd_Cantidad + "</td>";
              copiar += "<td id = 'Precio_UnitarioCreate' align='right'>" + val.factd_PrecioUnitario + "</td>";
              copiar += "<td id = 'ImpuestoCreate' align='right'>" + val.factd_Impuesto + "</td>";
              copiar += "<td id = 'factd_MontoDescuentoCreate' align='right'>" + val.factd_MontoDescuento + "</td>";
              copiar += "<td id = 'TotalProductoCreate' align='right'>" + Subtotal + "</td>";
              copiar += "<td>" + "<a href='#' onclick='EditStudentRecord(" + StudentId + ")' ><span class='btn btn-warning glyphicon glyphicon-edit btn-xs'></span></a>" + "</td>";
              copiar += "</tr>";
              $('#tblDetalleFactura').append(copiar);
              total_col1 = 0
              SubtotalD = 0;
              GranImpuesto = 0;
              GranTotal = 0;
              $("#tblDetalleFactura tbody tr").each(function (index) {
                  DescuentoDD = $(this).children("td:eq(5)").html();
                  Cantidad = $(this).children("td:eq(2)").html();
                  ImpuestoD = $(this).children("td:eq(4)").html();
                  ValorUnitario = $(this).children("td:eq(3)").html();
                  PorcentajeImpuesto = parseFloat(ImpuestoD / 100);
                  if (ValorUnitario != '') {
                      total_col1 += parseFloat($(this).find('td').eq(5).text());
                      ValorUnitario = parseFloat(ValorUnitario);
                      SubtotalD += Cantidad * ValorUnitario;
                      GranImpuesto += (Cantidad * ValorUnitario) * PorcentajeImpuesto;
                      GranTotal += Cantidad * ValorUnitario + (Cantidad * ValorUnitario) * PorcentajeImpuesto - DescuentoDD;
                  }
              });
              document.getElementById("TotalDescuento").innerHTML = parseFloat(total_col1);
              document.getElementById("Subtotal").innerHTML = parseFloat(SubtotalD);
              document.getElementById("isv").innerHTML = parseFloat(GranImpuesto);
              document.getElementById("total").innerHTML = parseFloat(GranTotal);
          });

      })
   
}
//Show The Popup Modal For Edit Student Record
function EditStudentRecord(StudentId) {
    var url = "/Factura/GetDetalleEdit?StudentId=" + StudentId;
    $("#ModalTitle").html("Update Student Record");
    $("#FacturaDetalleEdit").modal();
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            $.each(data, function (key, val) {
                $("#factdd").val(val.factd_Id);
                $("#IDProducto").val(val.prod_Codigo);
                $("#DescProducto").val(val.prod_Descripcion);
                $("#MontoDescuentoEdit").val(val.factd_MontoDescuento);
                $("#CantidadEdit").val(val.factd_Cantidad);
                $("#ImpuestoEdit").val(val.factd_Impuesto);
                $("#PrecioUnitarioEdit").val(val.factd_PrecioUnitario);
                $("#UsuCrea").val(val.factd_UsuarioCrea);
                $("#FechaCrea").val(val.factd_FechaCrea);
                var Cantidad = document.getElementById("CantidadEdit").value;
                var Precio = document.getElementById("PrecioUnitarioEdit").value;
                var Descuento = document.getElementById("MontoDescuentoEdit").value;
                var Impuesto = document.getElementById("ImpuestoEdit").value;
                var PorcentajeImpuesto = (Impuesto / 100);
                var ImpuestoTotal = (Cantidad * Precio) * PorcentajeImpuesto;
                result = "";
                result1 = "";
                if (Cantidad && Precio > 0) {
                    result += Cantidad * Precio;
                    result1 += ((Cantidad * Precio) + ImpuestoTotal - Descuento)
                }
                $("#SubtotalEdit").val(result);
                $("#TotalEdit").val(result1);
            })
        }
    })
}


$("#EditFacturaDetalle").click(function () {
    var factd_Ids = $('#factd_Id').val();
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "POST",
        url: "/Factura/UpdateFacturaDetalle",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");           
        }
    });

    location.reload(true);
})
