var MontoTotal = $('#Dev_Monto').val();
function getMontoTotal(suma, MontoTotal) {
    console.log('Entra')
}
$(document).ready(function () {
    GetDetalle()
});
var contador = 0;
function GetDetalle() {

    var devolucionId = $("#dev_Id").val();
    $.ajax({
        url: "/Devolucion/GetDevolucionDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ devolucionId: devolucionId }),
    })
      .done(function (data) {
      
          $.each(data, function (key, val) {
              DevIdd = val.devd_Id;
              contador = contador + 1;
              
              //ValorMontoD = document.getElementById("Monto").innerHTML;
              copiar = "<tr data-id=" + DevIdd + ">";
              copiar += "<td id = 'a'>" + val.prod_Codigo + "</td>";
              copiar += "<td id = 'b'>" + val.prod_Descripcion + "</td>";
              copiar += "<td id = 'c'>" + val.devd_CantidadProducto + "</td>";
              copiar += "<td id = 'd' >" + val.devd_Descripcion + "</td>";
              copiar += "<td class='Monto'>" + val.devd_Monto + "</td>";
              copiar += "<td>" + "<a href='#' onclick='GetIDFactura(" + DevIdd + ")' ><span class='btn btn-warning glyphicon glyphicon-edit btn-xs'></span></a>" + "</td>";
              copiar += "</tr>";
              $('#tbDetalleDevolucion').append(copiar);

              $("#tbDetalleDevolucion tbody tr").each(function (index) {
                  var suma = 0;
                  var data = [];
              
                  $("td.Monto").each(function () {
                      data.push(parseFloat($(this).text()));
                  });
                  var suma = data.reduce(function (a, b) { return a + b; }, 0);


                  $("#Dev_Monto").val(suma);
                  $('#nocre_Monto').val(suma);
                  getMontoTotal(suma);
                  console.log("nocre_Monto", suma)

               
                  var MontoDev = $("#Dev_Monto").val();
                  $.ajax({
                      url: "/Devolucion/MontoDevolucion",
                      method: "POST",
                      dataType: 'json',
                      contentType: "application/json; charset=utf-8",
                      data: JSON.stringify({ MontoDev: MontoDev }),
                  })
                  console.log("Data",data);
                  console.log(suma);
              });
          });
      })
}


//Añadir productos en modal editar producto----------------------------------------------------------------------------
function GetIDFactura(DevIdd) {
    var url = "/Devolucion/GetDevolucionDetalleEditar?DetalleDevID=" + DevIdd;
    $("#EditarDetalleDev").modal();

    $.ajax({
            type: "Get",
            url: url,
        success: function (data) {
            $.each(data, function (key, arn) {
                $('#IdDevolucion').val(arn.dev_Id);
                console.log("dev_Id", arn.dev_Id)
                $('#devd_Id').val(arn.devd_Id);
                $('#CodigoFactura').val(arn.fact_Id);
                $('#devd_UsuarioCrea').val(arn.devd_UsuarioCrea);
                $('#devd_FechaCrea').val(arn.devd_FechaCrea);
                $('#CodigoProducto').val(arn.prod_Codigo);
                $('#CantidadDevolucion').val(arn.devd_CantidadProducto);
                $('#DescripcionProducto').val(arn.prod_Descripcion);
                $('#MontoDev').val(arn.devd_Monto);
                $('#Comentario').val(arn.devd_Descripcion);
                $('#CantFacturada').val(arn.factd_Cantidad);
                console.log("factd_Cantidad", arn.factd_Cantidad)
                $('#PrecioUnit').val(arn.factd_PrecioUnitario);
                $('#PorDescuento').val(arn.factd_PorcentajeDescuento);
                $('#PorImpuesto').val(arn.factd_Impuesto);
                var Dcantidad = document.getElementById("CantidadDevolucion").value;
                var Dprecio = document.getElementById("PrecioUnit").value;
                var Ddescuento = document.getElementById("PorDescuento").value;
                var Dimpuesto = document.getElementById("PorImpuesto").value;
                var PorcentajeImpuesto = (Dimpuesto / 100);
                console.log("PorcentajeImpuesto", PorcentajeImpuesto)
                var PorcentajeDescuento = (Ddescuento / 100);
                console.log("PorcentajeDescuento", PorcentajeDescuento)
                var SubTotal = "";
                var MontoTotal = "";
                var MontoImp = "";
                var MontoDesc = "";

                if (Dcantidad && Dprecio > 0) {
                    SubTotal = (parseFloat(Dcantidad) * parseFloat(Dprecio));
                    MontoImp += (parseFloat(SubTotal) * parseFloat(PorcentajeImpuesto));
                    MontoDesc += (parseFloat(SubTotal) * parseFloat(PorcentajeDescuento));
                    MontoTotal += ((parseFloat(SubTotal) + parseFloat(MontoImp)) - parseFloat(MontoDesc));
                };
                $("#ValorImp").val(MontoImp);
                $("#MontoDesc").val(MontoDesc);
                $("#MontoDevolucion").val(MontoTotal);
                $("#Stotal").val(SubTotal);
            });
            console.log(data);

       
        }

    });
}

//Guardar Actualizacion de Detalle Devolucion
$("#EditarDevolucionDetalle").click(function () {
    var Devd_Idd = $('#devd_Id').val();
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "POST",
        url: "/Devolucion/UpdateDevolucionDetalle",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
        }
    });

    location.reload(true);
})

