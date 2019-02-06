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
              DevIdd = val.devd_Id;
              console.log("CodigoDetalle",DevIdd)
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
                  console.log("si entra body")
                  //var ValorMontoD = $(this).parents("tr").find("td")[2].innerHTML;
                  //Calculo Total
                  var suma = 0;
                  var data = [];
                
                  //console("ValorMontoD", ValorMontoD)
                  //$(this).parents("tr").find("td")[5].innerHTML = devol;
                  $("td.Monto").each(function () {
                      data.push(parseFloat($(this).text()));
                  });
                  var suma = data.reduce(function (a, b) { return a + b; }, 0);


                  $("#Dev_Monto").val(suma);

                  console.log("Data",data);
                  console.log(suma);
              });

            
          });

      })
}

//Añadir productos en modal editar producto----------------------------------------------------------------------------
function GetIDFactura(DevIdd) {
    console.log("GetIDFactura", DevIdd)
    var url = "/Devolucion/GetDevolucionDetalleEditar?DetalleDevID=" + DevIdd;
    $("#EditarDetalleDev").modal();

    $.ajax({
            type: "Get",
            url: url,
        success: function (data) {
            $.each(data, function (key, arn) {
                $('#DevolucionID').val(arn.dev_Id);
                console.log("dev_Id", arn.dev_Id)
                $('#CodigoFactura').val(arn.fact_Id);
                $('#CodigoProducto').val(arn.prod_Codigo);
                $('#CantidadDevolucion').val(arn.devd_CantidadProducto);
                $('#DescripcionProducto').val(arn.prod_Descripcion);
                $('#MontoDev').val(arn.devd_Monto);
                $('#Comentario').val(arn.devd_Descripcion);
                $('#CantidadFacturada').val(arn.factd_Cantidad);
                console.log("factd_Cantidad", factd_Cantidad)
                $('#PrecioUnitario').val(arn.factd_PrecioUnitario);
                $('#Descuento').val(arn.factd_PorcentajeDescuento);
                $('#Impuesto').val(arn.factd_Impuesto);
            });
            console.log(data);

       
        }

    });
}


//function GuardarDetalleDevolucion() {
$("#EditDevolucionDetalle").click(function () {
    console.log("btnGuardar")
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
$(document).on("keyup", "#example1 tbody tr td input#cantidad", function () {
    var row = $(this).closest("tr");
    var suma = 0;
    var data = [];
    MontoInicial = 0;
         
    $(this).parents("tr").find("td")[3].innerHTML = Monto;
    if (Monto != 0) {
        MontoInicial += Monto;
        //Calculo Total
        $("td.sumTotal").each(function () {
            data.push(parseFloat($(this).text()));
        });
        var suma = data.reduce(function (a, b) { return a + b; }, 0);


        $("#total").val(suma);

        console.log(data);
        console.log(suma);

    }



});
