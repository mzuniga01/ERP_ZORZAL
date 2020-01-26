$(document).ready(function () {
    GetDetalle()
});
var contador = 0;
function GetDetalle() {
    var listp_Id = $("#listp_Id").val();
    $.ajax({
        url: "/ListaPrecios/GetListadoDetalleEdit",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ listp_Id: listp_Id }),
    })
      .done(function (data) {
          $.each(data, function (key, val) {
              contador = contador + 1;
              copiar = "<tr data-id=" + contador + ">";
              copiar += "<td id = 'prod_Codigo'>" + val.prod_Codigo + "</td>";
              copiar += "<td id = 'prod_Descripcion'>" + val.prod_Descripcion + "</td>";
              copiar += "<td id = 'lispd_PrecioMayoristaCreate'>" + val.lispd_PrecioMinorista + "</td>";
              copiar += "<td id = 'lispd_PrecioMinoristaCreate'>" + val.lispd_DescCaja + "</td>";
              copiar += "<td id = 'lispd_DescCajaCreate'>" + val.lispd_DescCaja + "</td>";
              copiar += "<td id = 'lispd_DescGerenteCreate'>" + val.lispd_DescGerente + "</td>";
              copiar += "<td>" + '<button id="removeListaPrecioDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
              copiar += "</tr>";
              $('#tbListaPrecioDetalle').append(copiar);
              
          });

      })
}