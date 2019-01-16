var contador = 0;

$('#AgregarDetalleFactura').click(function () {
    var CodigoProducto = $('#prod_Codigo').val();
    var PorcentajeDescuento = $('#factd_PorcentajeDescuento').val();
    var MontoDescuento = $('#factd_MontoDescuento').val();
    var DescripcionProducto = $('#tbProducto_prod_Descripcion').val();
    var CantidadProducto = $('#factd_Cantidad').val();
    var Subtotal = $('#SubtotalProducto').val();
    var PrecioUnitario = $('#factd_PrecioUnitario').val();
    var Impuesto = $('#factd_Impuesto').val();
    var Total = $('#TotalProducto').val();
    
    if (CodigoProducto == '' )
    {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationCodigoProductoCreate').after('<ul id="ErrorCodigoProductoCreate" class="validation-summary-errors text-danger">Campo Código Producto requerido</ul>');
    }
    else if (MontoDescuento == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationMontoDescuentoCreate').after('<ul id="ErrorMontoDescuentoCreate" class="validation-summary-errors text-danger">Campo Monto Descuento requerido</ul>');
    }
    else if (CantidadProducto == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationCantidadProductoCreate').after('<ul id="ErrorCantidadCreate" class="validation-summary-errors text-danger">Campo Cantidad requerido</ul>');
    }
    else if (Impuesto == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationImpuestoProductoCreate').after('<ul id="ErrorImpuestoCreate" class="validation-summary-errors text-danger">Campo Impuesto requerido</ul>');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'prod_CodigoCreate'>" + CodigoProducto + "</td>";
        copiar += "<td id = 'tbProducto_prod_DescripcionCreate'>" + DescripcionProducto + "</td>";
        copiar += "<td id = 'factd_CantidadCreate'>" + CantidadProducto + "</td>";
        copiar += "<td id = 'Precio_UnitarioCreate'>" + PrecioUnitario + "</td>";
        copiar += "<td id = 'ImpuestoCreate'>" + Impuesto + "</td>";
        copiar += "<td id = 'factd_MontoDescuentoCreate'>" + MontoDescuento + "</td>";
        copiar += "<td id = 'TotalProductoCreate'>" + Total + "</td>";
        copiar += "<td>" + '<button id="removeFacturaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblDetalleFactura').append(copiar);

        //Subtotal 
        var totalProducto = $('#TotalProducto').val();
        var subtotal = parseFloat(document.getElementById("Subtotal").innerHTML);

        if (document.getElementById("Subtotal").innerHTML == '') {
            totalProducto = $('#TotalProducto').val();
            document.getElementById("Subtotal").innerHTML = parseFloat(totalProducto);
        }
        else {
            document.getElementById("Subtotal").innerHTML = parseFloat(subtotal) + parseFloat(totalProducto);
        }
        //Impuesto
        var totalProducto = document.getElementById("TotalProducto").value;
        var impuesto = parseFloat(document.getElementById("factd_Impuesto").value.replace(',', '.'));
        var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
        var porcentaje = parseFloat(impuesto / 100);
        var impuestos = (totalProducto * porcentaje);

        if (document.getElementById("isv").innerHTML == '') {
            impuesto = document.getElementById("factd_Impuesto").value;
            document.getElementById("isv").innerHTML = parseFloat(impuestos);
        }
        else {
            document.getElementById("isv").innerHTML = parseFloat(impuestotal) + parseFloat(impuestos);
        }

        //Grantotal
        if (document.getElementById("total").innerHTML == '') {
            document.getElementById("total").innerHTML = parseFloat(totalProducto) + parseFloat(impuestos);
        }
        else {
            document.getElementById("total").innerHTML = parseFloat(subtotal) + parseFloat(totalProducto) + parseFloat(impuestotal) + parseFloat(impuestos);
        }
                  

        var FacturaDetalle = GetFacturaDetalle();
        $.ajax({
            url: "/Factura/SaveFacturaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ FacturaDetalleC: FacturaDetalle }),
        })
        .done(function (data) {
            $('#ErrorCodigoProductoCreate').text('');
            $('#ErrorMontoDescuentoCreate').text('');
            $('#ErrorCantidadCreate').text('');
            $('#ErrorImpuestoCreate').text('');
            //Input
            $('#prod_Codigo').val('');
            $('#factd_PorcentajeDescuento').val('');
            $('#factd_MontoDescuento').val('');
            $('#tbProducto_prod_Descripcion').val('');
            $('#factd_Cantidad').val('');
            $('#SubtotalProducto').val('');
            $('#factd_PrecioUnitario').val('');
            $('#factd_Impuesto').val('');
            $('#TotalProducto').val('');
        });
    }
});

function GetFacturaDetalle() {

    var FacturaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        factd_PorcentajeDescuento: $('#factd_PorcentajeDescuento').val(),
        factd_MontoDescuento: $('#factd_MontoDescuento').val(),
        tbProducto_prod_Descripcion: $('#tbProducto_prod_Descripcion').val(),
        factd_Cantidad: $('#factd_Cantidad').val(),
        SubtotalProducto: $('#SubtotalProducto').val(),
        factd_PrecioUnitario: $('#factd_PrecioUnitario').val(),
        factd_Impuesto: $('#factd_Impuesto').val(),
        TotalProducto: $('#TotalProducto').val(),
        factd_Id: contador
    }
    return FacturaDetalle
};

$(document).on("click", "#tblDetalleFactura tbody tr td button#removeFacturaDetalle", function () {
    //Subtotal
    var SubtotalProducto = $(this).parents("tr").find("td")[6].innerHTML;
    var subtotal = parseFloat(document.getElementById("Subtotal").innerHTML);
    document.getElementById("Subtotal").innerHTML = parseFloat(subtotal) - parseFloat(SubtotalProducto);

    //Impuesto
    var impuesto = $(this).parents("tr").find("td")[4].innerHTML;
    var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
    var porcentaje = parseFloat(impuesto.replace(',', '.') / 100);
    var impuestos = (SubtotalProducto * porcentaje);
    document.getElementById("isv").innerHTML = parseFloat(impuestotal) - parseFloat(impuestos);

    //GranTotal
    document.getElementById("total").innerHTML = (parseFloat(subtotal) - parseFloat(SubtotalProducto)) + (parseFloat(impuestotal) - parseFloat(impuestos));


    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var FacturaDetalle = {
        factd_Id: idItem,
    };

    $.ajax({
        url: "/Factura/RemoveFacturaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ FacturaDetalleC: FacturaDetalle }),
    });
});

//Validacion de numeros//
function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
}

function pierdeFoco(e) {
    var valor = e.value.replace(/^0*/, '');
    e.value = valor;
}

function ponerdecimales(numero) {
    if (numero.indexOf(".") == -1) { numero += ".00" } else {
        if (numero.indexOf(".") == numero.length - 2) { numero += "0" }
    }
    return numero;
}

$('#AgregarDetalleFactura').click(function () {
    console.log(total);
    if (document.getElementById("total").innerHTML == '') {
        totalProducto = $('#TotalProducto').val();
        document.getElementById("total").innerHTML = parseFloat(totalProducto);
    }
    else {
        document.getElementById("total").innerHTML = parseFloat(subtotal) + parseFloat(totalProducto);
    }

})


