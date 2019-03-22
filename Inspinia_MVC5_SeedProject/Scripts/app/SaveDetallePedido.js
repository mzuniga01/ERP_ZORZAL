var contador = 0;
$('#AñadirPedidoDetalle').click(function () {
    var prod_Codigo = $('#prod_Codigo').val();
    var prod_Descripcion = $('#tbProducto_prod_Descripcion').val();
    var pedd_Cantidad = $('#pedd_Cantidad').val();
    var table = $('#PedidoDetalle').DataTable();
    //var pedd_CantidadFacturada = $('#pedd_CantidadFacturada').val();
    console.log('prod_Codigo')

    if (prod_Codigo == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationCodigoProductoCreate').after('<ul id="ErrorCodigoProductoCreate" class="validation-summary-errors text-danger">Campo Codigo Producto Requerido</ul>');
    }
    else if (prod_Descripcion == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationDescripcionProductoCreate').after('<ul id="ErrorDescripcionProductoCreate" class="validation-summary-errors text-danger">Campo Descripcion Producto Requerido</ul>');
    }

    else if (pedd_Cantidad == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationCantidadCreate').after('<ul id="ErrorCantidadCreate" class="validation-summary-errors text-danger">Campo Cantidad Requerido</ul>');
    }
    else {
        var tbPedidoDetalle = GetPedidoDetalle();
        $.ajax({
            url: "/Pedido/SavePedidoDetalles",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ PedidoDetalle: tbPedidoDetalle, data_producto: prod_Codigo }),

        }).done(function (datos) {
            if (datos == prod_Codigo) {
                console.log('Repetido');
                var cantfisica_nueva = $('#pedd_Cantidad').val();
                $("#PedidoDetalle td").each(function () {
                    //var prueba = $(this).closest("tr").find("td:eq(0)").text();
                    var prueba = $(this).text();
                    console.log(prueba)
                    //var prueba = prueba.replace(/^/n*/, '')
                    if (prueba == prod_Codigo) {
                        var idcontador = $(this).closest('tr').data('id');
                        var cantfisica_anterior = $(this).closest("tr").find("td:eq(2)").text();
                        var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);
                       
                        //$(this).closest('tr').remove();
                        table.row($(this).parents('tr')).remove().draw();

                        table.row.add([
                               $('#prod_Codigo').val(),
                               $('#tbProducto_prod_Descripcion').val(),
                               sumacantidades,
                               $('#pedd_CantidadFacturada').val(),
                               '<button id="QuitarDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>'
                        ]).draw(false);

                        //copiar += "</tr>";
                        //$('#tblPedidoDetalle').append(copiar);
                    }
                });
            } else {
                table.row.add([
                        $('#prod_Codigo').val(),
                        $('#tbProducto_prod_Descripcion').val(),
                        $('#pedd_Cantidad').val(),
                        $('#pedd_CantidadFacturada').val(),
                        '<button id="QuitarDetalle" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>'
                ]).draw(false);

                //contador = contador + 1;
                //copiar = "<tr data-id=" + contador + " data-prod_Codigo = " + $('#prod_Codigo').val() + ">";
                //copiar += "<td id = 'prod_Codigo'>" + $('#prod_Codigo').val() + "</td>";
                //copiar += "<td id = 'prod_Descripcion'>" + $('#tbProducto_prod_Descripcion').val() + "</td>";
                //copiar += "<td id = 'pedd_Cantidad'>" + $('#pedd_Cantidad').val() + "</td>";
                //copiar += "<td id = 'pedd_CantidadFacturada'>" + $('#pedd_CantidadFacturada').val() + "</td>";


            }
        }).done(function (data) {
            $('#prod_Codigo').val('');
            $('#tbProducto_prod_Descripcion').val('');
            $('#tbProducto_prod_CodigoBarras').val('');
            $('#pedd_Cantidad').val('');
            $('#MessageError').text('');
            $('#NombreError').text('');
        })
    }
});


function GetPedidoDetalle() {
    var PedidoDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        prod_Descripcion: $('#tbProducto_prod_Descripcion').val(),
        pedd_Descripcion: $('#pedd_Descripcion').val(),
        pedd_Cantidad: $('#pedd_Cantidad').val(),
        pedd_CantidadFacturada: $('#pedd_CantidadFacturada').val(),
        pedd_Id: contador,
        pedd_UsuarioCrea: contador,
    };
    return PedidoDetalle;
}


$("#pedd_Cantidad")[0].maxLength = 10; 
$("#pedd_Cantidad_Ped")[0].maxLength = 10;




$('#pedd_Cantidad_Ped').blur(function () {
    valido = document.getElementById('PCantidad');
    var motivoNC = $('#pedd_Cantidad_Ped').val();
    if (motivoNC == "") {
        $('#pedd_Cantidad_Ped').val('').focus();
        valido.innerText = "*El campo Cantidad es requerido";
    } else {
        valido.innerText = "";
    }
});


function EditPedidoDetalles(pedd_Id) {
    $.ajax({
        url: "/Pedido/getPedidoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ pedd_Id: pedd_Id }),

    })
    .done(function (data) {
        if (data.length > 0) {
            $.each(data, function (i, item) {
                $("#pedd_Id_Ped").val(item.pedd_Id);
                $("#prod_Codigo_Ped").val(item.prod_Codigo);
                $("#pedd_Cantidad_Ped").val(item.pedd_Cantidad);

               
                $("#pedd_UsuarioCrea_Ped").val(item.pedd_UsuarioCrea);
                $("#pedd_FechaCrea_Ped").val(item.pedd_FechaCrea);
                $("#EditPedidoDetalle").modal();
            })
        }
    })
}

$("#BtnsubmitMunicipio").click(function () {
    var pedd_Ids = $('#pedd_Id').val();
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "Post",
        url: "/Pedido/UpdatePedidoDetalle",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");                
        }
    });

    location.reload(true);
})

$(document).ready(function () {

    var ped_Id = $('#ped_Id').val();

    $.ajax({
        url: "/Pedido/GetPedidoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Pedido: ped_Id }),
    })
        .done(function (data) {
            console.log(data);
            $("#LoadingStatus").html("Cargando....");
            var SetData = $("#SetDetalleList");
            $.each(data, function (key, val) {
                var Data = "<tr class='row_" + val.pedd_Id + "'>" +
                    "<td>" + val.pedd_Id + "</td>" +
                    "<td>" + val.prod_Codigo + "</td>" +
                    "<td>" + val.pedd_Cantidad + "</td>" +
                    "<td>" + val.pedd_CantidadFacturada + "</td>" +
                    //"<td>" + DetalleSalida[i].DepartmentName + "</td>" +
                    "<td>" + "<a href='#' class='btn btn-warning' onclick='EditPedidoDetalleM(" + val.pedd_Id + ")' ><span class='glyphicon glyphicon-edit'></span></a>" + "</td>" +
                    "<td>" + "<a href='#' class='btn btn-danger' onclick='DeleteStudentRecord(" + val.pedd_Id + ")'><span class='glyphicon glyphicon-trash'></span></a>" + "</td>" +
                    "</tr>";
                SetData.append(Data);
                $("#LoadingStatus").html(" ");;
                //$('#pscat_Id').trigger("chosen:updated");
                console.log(data);
            });



        });

});


function EditPedidoDetalleM(pedd_Id) {
    var url = "/Pedido/GetPedidoDetalleById=" + pedd_Id;
    $("#ModalTitle").html("Actualizar Pedido Detalle");
    $("#MyModal").modal();
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            var obj = JSON.parse(data);
            $("#Ppedd_Id").val(obj.ped_Id);
            $("#Pprod_Codigo").val(obj.prod_Codigo);
            $("#Ppdd_Cantidad").val(obj.pedd_Cantidad);
            $("#Ppdd_CantidadFacturada").val(obj.pedd_CantidadFacturada);


        }
    })
}
























































//$('#AñadirPedidoDetalle').click(function () {
//    var prod_Codigo = $('#prod_Codigo').val();
//    var CodigoBarra = $('#tbProducto_prod_CodigoBarras').val();
//    var prod_Descripcion = $('#tbProducto_prod_Descripcion').val();
//    var pedd_Cantidad = $('#pedd_Cantidad').val();
//    var pedd_CantidadFacturada = $('#pedd_CantidadFacturada').val();
//    var table = $('#PedidoDetalle').DataTable();

//    var tbPedidoDetalle = GetPedidoDetalle();
//    $.ajax({
//        url: "/Pedido/SavePedidoDetalles",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ PedidoDetalle: tbPedidoDetalle })
//    })
//            .done(function (datos) {
//                if (datos == prod_Codigo) {
//                    $("#PedidoDetalle td").each(function () {
//                        var prueba = $(this).text();
//                        if (prueba == prod_Codigo) {
//                            var Cantidad = $('#pedd_Cantidad').val();
//                            var CantidadSum = $(this).parents("tr").find("td")[2].innerHTML;
//                            CantidadSum = parseFloat(CantidadSum) + parseFloat(Cantidad);
//                            $(this).closest('tr').remove();
//                            table.row($(this).parents('tr')).remove().draw();
//                            table.row.add([
//                            prod_Codigo,
//                            prod_Descripcion,
//                            CantidadSum,
//                            pedd_CantidadFacturada,
//                            '<button id = "removeFacturaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">-</button>'
//                            ]).draw(false); 
//                        }
//                    });
//                }
//                else {
//                    table.row.add([
//                                prod_Codigo,
//                                prod_Descripcion,
//                                pedd_Cantidad,
//                                pedd_CantidadFacturada,
//                                '<button id="QuitarDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>'
//                    ]).draw(false);
//                }
//            })
//    });


    function GetPedidoDetalle() {
        var PedidoDetalle = {
            prod_Codigo: $('#prod_Codigo').val(),
            CodigoBarra: $('#tbProducto_prod_CodigoBarras').val(),
            prod_Descripcion: $('#tbProducto_prod_Descripcion').val(),
            pedd_Descripcion: $('#pedd_Descripcion').val(),
            pedd_Cantidad: $('#pedd_Cantidad').val(),
            pedd_CantidadFacturada: $('#pedd_CantidadFacturada').val(),
            pedd_UsuarioCrea: contador
        };
        return PedidoDetalle;
    }

