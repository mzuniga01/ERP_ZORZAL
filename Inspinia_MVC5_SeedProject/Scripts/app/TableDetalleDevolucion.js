$(document).ready(function () {
    GetDetalle()
    console.log("si entra")
});
var contador = 0;
function GetDetalle() {
    var devolucionId = $("#dev_Id").val();
    console.log(devolucionId)
    $.ajax({
        url: "/Devolucion/GetDevolucionDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ devolucionId: devolucionId }),
    })
      .done(function (data) {
          $.each(data, function (key, val) {
              contador = contador + 1;
              copiar = "<tr data-id=" + contador + ">";
              copiar += "<td id = 'a'>" + val.prod_Codigo + "</td>";
              copiar += "<td id = 'b'>" + val.prod_Descripcion + "</td>";
              copiar += "<td id = 'c'>" + val.devd_CantidadProducto + "</td>";
              copiar += "<td id = 'd' align='right'>" + val.devd_Descripcion + "</td>";
              copiar += "<td id = 'g' align='right'>" + val.devd_Monto + "</td>";
              copiar += "<td>" + "<a href='#' onclick='GetIDFactura(" + contador + ")' ><span class='btn btn-warning glyphicon glyphicon-edit btn-xs'></span></a>" + "</td>";
              copiar += "</tr>";
              $('#tbDetalleDevolucion').append(copiar);

              $("#tbDetalleDevolucion tbody tr").each(function (index) {
             
              });
           
          });

      })
}

//Añadir productos en modal editar producto----------------------------------------------------------------------------
function GetIDFactura(contador) {
    var url = "/Devolucion/GetDetalleEdit?StudentId=" + StudentId;
    $("#ModalTitle").html("Update Student Record");
    $("#FacturaDetalleEdit").modal();

    $.ajax({
            type: "GET",
            url: url,
        success: function (list) {
            $.each(list, function (key, val) {
                $('#CantidadFacturada').val(val.CantidadFacturada);
                $('#PrecioUnitario').val(val.PrecioUnitario);
                $('#Descuento').val(val.PorcentajeDesc);
                $('#Impuesto').val(val.PorcentajeImpu);
            });
            console.log(list);
        }

    });
}