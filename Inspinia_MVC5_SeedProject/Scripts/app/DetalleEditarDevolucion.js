var contador = 0;

$('#btnEditarDetalle').click(function () {
    console.log("Entroal boton")
    var CodigoProducto = $('#prod_Codigo').val();
    console.log("CodigoProducto", CodigoProducto)
    var Producto = $('#DescripcionProducto').val();
    console.log("Producto", Producto)
    var Cantidad = $('#CantidadDevolucion').val();
    console.log("Cantidad", Cantidad)
    var Comentario = $('#devd_Descripcion').val();
    console.log("Comentario", Comentario)
    var Monto = $('#MontoDevolu').val();
    console.log("Monto", Monto)

    if ($('#prod_Codigo').val() == '') {
        $('#validationCodigoProductoCreate').after('<ul id="ErrorCodigoProductoCreate" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }

    else if ($('#DescripcionProducto').val() == '') {
        $('#validationComentariosCreate').after('<ul id="ErrorProductoDescripcionCreate" class="validation-summary-errors text-danger">Campo Descripìon Requerido</ul>');
    }
    else if ($('#CantidadDevolucion').val() == '') {
        $('#validationCantidadCreate').after('<ul id="ErrorDescripcionCreate" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');

    }
    else if ($('#devd_Descripcion').val() == '') {
        $('#validationComentariosCreate').after('<ul id="ErrorProductoComentarioCreate" class="validation-summary-errors text-danger">Campo Descripìon Requerido</ul>');
    }
    else if ($('#MontoDevolu').val() == '') {
        $('#validationComentariosCreate').after('<ul id="ErrorProductoComentarioCreate" class="validation-summary-errors text-danger">Campo Descripìon Requerido</ul>');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'prod_Codigo'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'tbProducto_prod_Descripcion'>" + $('#DescripcionProducto').val() + "</td>";
        copiar += "<td id = 'devd_CantidadProducto'>" + $('#CantidadDevolucion').val() + "</td>";
        copiar += "<td id = 'devd_Descripcion'>" + $('#devd_Descripcion').val() + "</td>";
        copiar += "<td id = 'devd_Monto'>" + $('#MontoDevolu').val() + "</td>";
        copiar += "<td>" + '<button id="removeDevolucionDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblDetalleDevolucion').append(copiar);
    }
})