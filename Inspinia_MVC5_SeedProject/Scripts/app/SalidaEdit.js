var contador = 0;
var CantidadExit = 0.00;

function EditSalidaDetalles(sald_Id) {
    $.ajax({
        url: "/Salida/getSalidaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ sald_Id: sald_Id }),
    })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#sal_Id_SD").val(item.sal_Id);
                    $("#sald_Id_SD").val(item.sald_Id);
                    $("#prod_Codigo_SD").val(item.prod_Codigo);
                    $("#sald_Cantidad_SD").val(item.sald_Cantidad);
                    $("#prod_Descripcion_SD").val(item.prod_Descripcion);
                    $("#pcat_Nombre_SD").val(item.pcat_Nombre);
                    $("#pscat_Descripcion_SD").val(item.pscat_Descripcion);
                    $("#uni_Descripcion_SD").val(item.uni_Descripcion);
                    $("#EditSalidaDetalle").modal();
                })
            }
        })
}


$("#BtnsubmitEdit").click(function () {
    var Cantidad = $('#sald_Cantidad_SD').val();
    if (Cantidad == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#validationsald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
    }
    else if (Cantidad < '0.05') {
        $('#NombreError').text('');
        $('#validationsald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad muy Pequeña</ul>');
    }
    else {
        var sal_id = $('#sald_Id').val();
        var data = $("#SubmitForm").serializeArray();
        $.ajax({
            type: "Post",
            url: "/Salida/EditSalidaDetalle",
            data: data,
            success: function (result) {
                if (result == '-1')
                    $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
                else
                    console.log(result);
                window.location.href = '/Salida/Edit/' + result;
            }
        });
    }
})


function GetSalidaDetalle() {
    var SalidaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        sald_Cantidad: $('#sald_Cantidad').val(),
        sal_Id: $('#sal_Id').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}

$('#AgregarSalidaDetalleEdit').click(function () {
    SeleccionProducto()
});


function SeleccionProducto() {
    var bodd_Id = $('#bodd_Id').val();
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var CodigoBarras = $('#prod_CodigoBarras').val();
    var Cantidad = $('#sald_Cantidad').val();
    var data_producto = $("#prod_Codigo").val();

    if (CodigoBarras == '') {
        $('#prod_CodigoBarrasError').text('');
        $('#validationprod_CodigoBarras').after('<ul id="prod_CodigoBarrasError" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
        vProducto = false;
    }
    else {
        vProducto = true;
    }
    if (Cantidad == '' && Cantidad < 0.01) {
        $('#sald_CantidadError').text('');
        $('#sald_Cantidad').after('<ul id="sald_CantidadError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
        vCantidad = false;
    }
    else {
        vCantidad = true;
    }
    if (vProducto || vCantidad) {
        var tbSalidaDetalle = GetSalidaDetalle();
        $.ajax({
            url: "/Salida/SaveSalidaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle, data_producto: data_producto }),
        }).done(function (datos) {
            if (datos == data_producto) {
                console.log('Repetido');
                var cantfisica_nueva = $('#sald_Cantidad').val();
                $("#tblSalidaDetalle td").each(function () {
                    var prueba = $(this).text()
                    if (prueba == data_producto) {
                        var idcontador = $(this).closest('tr').data('id');
                        var cantfisica_anterior = $(this).closest("tr").find("td:eq(7)").text();
                        var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);
                        console.log(sumacantidades);
                        $(this).closest('tr').remove();

                        copiar = "<tr data-id=" + idcontador + " , tr data-prod_Codigo = " + $('#prod_Codigo').val() + ">";
                        copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
                        copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
                        copiar += "<td id = 'Marca'>" + $('#prod_Marca').val() + "</td>";
                        copiar += "<td id = 'Modelo'>" + $('#prod_Modelo').val() + "</td>";
                        copiar += "<td id = 'Talla'>" + $('#prod_Talla').val() + "</td>";
                        copiar += "<td id = 'Categoria'>" + $('#pcat_Id').val() + " " + $('#pscat_Id').val() + "</td>";
                        copiar += "<td id = 'Unidad_Medida'>" + $('#uni_Id').val() + "</td>";
                        copiar += "<td id = 'Cantidad'>" + sumacantidades + "</td>";
                        copiar += "<td>" + '<button id="removeSalidaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                        copiar += "</tr>";
                        $('#tblSalidaDetalle').append(copiar);
                    }
                });
            } else {
                copiar = "<tr data-id=" + contador + " , data-prod_Codigo = " + $('#prod_Codigo').val() + ">";
                copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
                copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
                copiar += "<td id = 'Marca'>" + $('#prod_Marca').val() + "</td>";
                copiar += "<td id = 'Modelo'>" + $('#prod_Modelo').val() + "</td>";
                copiar += "<td id = 'Talla'>" + $('#prod_Talla').val() + "</td>";
                copiar += "<td id = 'Categoria'>" + $('#pcat_Id').val() + " " + $('#pscat_Id').val() + "</td>";
                copiar += "<td id = 'Unidad_Medida'>" + $('#uni_Id').val() + "</td>";
                copiar += "<td id = 'Cantidad'>" + $('#sald_Cantidad').val() + "</td>";
                copiar += "<td>" + '<button id="removeSalidaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
                copiar += "</tr>";
                $('#tblSalidaDetalle').append(copiar);
            }
        }).done(function (data) {
            $('#prod_Codigo').val('');
            $('#prod_Descripcion').val('');
            $('#pscat_Id').val('');
            $('#uni_Id').val('');
            $('#pcat_Id').val('');
            $("#prod_CodigoBarras").val('');
            $('#sald_Cantidad').val('');
            $('#Error_Barras').text('');
            $('#NombreError').text('');
            console.log('Hola');
        });
    }
    else {
    }
};
function GetNewSalidaDetalle() {
    var SalidaDetalle = {
        sal_Id: $('#sal_Id').val(),
        prod_Codigo: $('#prod_Codigo').val(),
        sald_Cantidad: $('#sald_Cantidad').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}

$('#btnCreateSalidaDetalle').click(function () {
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Cantidad = $('#sald_Cantidad').val();
    console.log('Funca');
    var tbSalidaDetalle = GetNewSalidaDetalle();
    $.ajax({
        url: "/Salida/SaveNewDatail",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle }),
    })
        .done(function (data) {
            if (data == 'El registro se guardo exitosamente') {
                location.reload();
                swal("El registro se almacenó exitosamente!", "", "success");
            }
            else {
                location.reload();
                swal("El registro  no se almacenó!", "", "error");
            }
        });
})


function DeleteSalidaDetalle(sald_Id) {
    $.ajax({
        url: "/Salida/DeleteSalidaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ sald_Id: sald_Id }),
    })
        .done(function (data) {
            console.log(data);
            location.reload();
        })
}
