//$(document).ready(function () {
//    var tbSalida = GetAnularSalida();
//    $.ajax({
//        url: "/Salida/Anular",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ Salida: tbSalida }),
//    })
//    .done(function (data) {
//        if (data == 'El registro se guardo exitosamente') {
//            location.reload();
//            swal("El registro se almacenó exitosamente!", "", "success");
//        }
//        else {
//            location.reload();
//            swal("El registro  no se almacenó!", "", "error");
//        }
//    });

//})

$('#fact_Codigo').change(function () {

    $.ajax({
        url: "/Salida/FacturaExist",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ fact_Codigo: $('#fact_Codigo').val() }),
    })
        .done(function (data) {
            console.log(data);
            $('#CodigoError').text('');
            $('#validationFactura').after('<ul id="CodigoError" class="validation-summary-errors text-danger">'+data+'</ul>');
        })
})

function ListaProductos() {

    $.ajax({
        url: "/Salida/GetProdList",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: $('#bod_Id').val() }),
    })
      .done(function (data) {
          if (data.length > 0) {
              $("#Body_BuscarProducto").html("");
              $.each(data, function (key, val) {
                  data.pcat_Nombre, data.prod_Codigo, data.prod_CodigoBarras, data.prod_Descripcion, data.pscat_Descripcion, data.uni_Descripcion
                  //<tr tr data-id="val.prod_Codigo" , tr data-content="val.prod_Descripcion" , tr data-container="val.pscat_Descripcion" , tr data-keyboard="val.uni_Descripcion" , tr data-pcat="valCategoria.pcat_Nombre" , tr data-cod_Barras="val.prod_CodigoBarras">

                  var tr = `<tr tr data-id=`+ val.prod_Codigo + ` , tr data-content=` + val.prod_Descripcion + ` , tr data-container=` + val.pscat_Descripcion + `, tr data-keyboard=` + val.uni_Descripcion + ` , tr data-pcat=` + val.pcat_Nombre + ` , tr data-cod_Barras=` + val.prod_CodigoBarras + `>
                  <td > `+ val.prod_Codigo + ` </td>
                  <td> `+ val.prod_Descripcion + ` </td>
                  <td> `+ val.pcat_Nombre + ` </td>
                  <td> `+ val.pscat_Descripcion + ` </td>
                  <td> `+ val.uni_Descripcion + ` </td>
                  <td> `+ val.prod_CodigoBarras + ` </td>
                  <td ><button class ="btn btn-primary btn-xs" value= `+ val.prod_Codigo + ` id="seleccionar" data-dismiss="modal">Seleccionar</button> </td>
                </tr>`;
                  $("#Body_BuscarProducto").append(tr)

              })
          }
      })
}

function LimpiarTable() { $("#Body_BuscarProducto").remove() }

$(document).ready(function () {
    console.log("ready")
    ListaProductos()
});

$('#bod_Id').change(function () {
    LimpiarTable();
});

//$.when(LimpiarTable()).then(function () {

//    $(document).ready(function () {
//        console.log("ready")
//        ListaProductos()
//    });
//});

function TipodeSalida() {
    var TipoSal = $("#tsal_Id").val()
    console.log(TipoSal);
    if (TipoSal == "1") {
        $('#fact_Codigo').val('***-***-**-********');
        $('#sal_RazonDevolucion').val('*****');

        $("#Prestamo").css("display", "block");

        $("#VentaoDevolucion").css("display", "none");

    }
    else {
        $("#Prestamo").css("display", "none");
        $("#Devolucion").css("display", "none");
        if (TipoSal == 2) {
            $('#fact_Codigo').val('');
            $('#sal_RazonDevolucion').val('*****');


            $("#VentaoDevolucion").css("display", "block");
            $("#TitleVenta").css("display", "block");

            $('#div_sal_RazonDevolucion').css("display", "none");
            $("#TitleDevolucion").css("display", "none");

            $("#Venta").css("display", "none");
        }
        else {
            $("#Venta").css("display", "none");
            $("#Prestamo").css("display", "none");

            if (TipoSal == "3") {
                $('#fact_Codigo').val('');
                $('#sal_RazonDevolucion').val('');

                $("#VentaoDevolucion").css("display", "block");
                $("#TitleDevolucion").css("display", "block");
                $('#fact_Codigo').css("display", "block");
                $('#div_sal_RazonDevolucion').css("display", "block");

                $("#TitleVenta").css("display", "none");

            }
            else {
                $("#VentaoDevolucion").css("display", "none");

                $("#Prestamo").css("display", "none");

            }

        }

    }
}

//$(document).ready( function () {
//    var e = document.getElementById("bod_Id");
//    var strUser = e.options[e.selectedIndex].text;
//    console.log(strUser)
//    $("#tbBodega_bod_Nombre").val(strUser)

//});

$(document).ready(function () {
    var e = document.getElementById("estm_Id");
    var strUser = e.options[e.selectedIndex].text;
    console.log(strUser)
    $("#tbEstadoMovimiento_estm_Descripcion").val(strUser)

});

$(document).ready(function () {
    var e = document.getElementById("fact_Id");
    var strUser = e.options[e.selectedIndex].text;
    console.log(strUser)
    $("#tbFactura_fact_Codigo").val(strUser)

});
$(document).ready(function () {
    var e = document.getElementById("tsal_Id");
    var strUser = e.options[e.selectedIndex].text;
    console.log(strUser)
    $("#tbTipoSalida_tsal_Id").val(strUser)

});
//Tipo de Salida

$(document).ready(function () {
   TipodeSalida()
});
$("#tsal_Id").change(function () {
    TipodeSalida()
});

//$(document).on("change", "#dep_Codigo", function () {
//    GetMunicipios();
//});



//function phonenumber(inputtxt) {

//    var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;

//    if (inputtxt.value.match(phoneno)) {

//        return true;

//    }

//    else {

//        alert("message");

//        return false;

//    }

//}


window.addEventListener("load", function () {

    Miform.sald_Cantidad.addEventListener("keypress", soloNumeros, false);
});

//$('#prod_Dsescripcion').mask('0000-0000');
$(document).on("change", "#sald_Cantidad", function () {
    var fiel = $("#sald_Cantidad").val();
    soloNumeros(fiel);
});

//Solo permite introducir numeros.
function soloNumeros(e) {
    var key = window.event ? e.which : e.keyCode;
    //|| key == 44
    if (key == 46) {

    }
    else {
        if (key < 48 || key > 57) {
            e.preventDefault();
        }
    }}
    //var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;

    //if (e.value.match(phoneno)) {

    //    return true;

    //}

    //else {

    //    alert("message");

    //    return false;

    //}




    //Anular
    function GetAnularSalida() {
        var Salida = {
            sal_Id: $('#sal_Id').val(),
            sal_RazonAnulada: $('#sal_RazonAnulada').val(),
            sald_UsuarioCrea: contador
        };
        return Salida;
    }


    $('#btnAnularSalida').click(function () {
        var sal_Id = $('#sal_Id').val();
        var sal_RazonAnulada = $('#sal_RazonAnulada').val();

        if (sal_RazonAnulada == '') {
            $('#MessageError').text('');
            $('#CodigoError').text('');
            $('#NombreError').text('');
            $('#ValidationCodigoCreate').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Descripcion Requerido</ul>');
        }

        else {
            var tbSalida = GetAnularSalida();
            $.ajax({
                url: "/Salida/Anular",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ Salida: tbSalida }),
            })
            .done(function (data) {
                window.location.href = "/Salida/Edit" + data.sal_Id
                 
            });

        }

    });