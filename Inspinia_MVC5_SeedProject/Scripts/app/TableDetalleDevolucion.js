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
              copiar += "<td>" + '<button id="EditDevolucionDetalle" class="btn btn-warning glyphicon glyphicon-edit btn-xs  eliminar" type="button">-</button>' + "</td>";
              copiar += "</tr>";
              $('#tblDetalleDevolucion').append(copiar);

              $("#tblDetalleDevolucion tbody tr").each(function (index) {
             
              });
           
          });

      })
}