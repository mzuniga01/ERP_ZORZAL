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

    if (CodigoProducto == '') {
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
        copiar += "<td id = 'factd_CantidadCreate' align='right'>" + CantidadProducto + "</td>";
        copiar += "<td id = 'Precio_UnitarioCreate' align='right'>" + PrecioUnitario + "</td>";
        copiar += "<td id = 'ImpuestoCreate' align='right'>" + Impuesto + "</td>";
        copiar += "<td id = 'factd_MontoDescuentoCreate' align='right'>" + MontoDescuento + "</td>";
        copiar += "<td id = 'TotalProductoCreate' align='right'>" + Total + "</td>";
        copiar += "<td>" + '<button id="removeFacturaDetalle" class="btn btn-danger glyphicon glyphicon-trash btn-xs eliminar" type="button"></button>' + "</td>";
        copiar += "</tr>";
        $('#tblDetalleFactura').append(copiar);
        //Descuento 
        var Descuento = $('#factd_MontoDescuento').val();
        var TotalDescuento = parseFloat(document.getElementById("TotalDescuento").innerHTML);

        if (document.getElementById("TotalDescuento").innerHTML == '') {
            totalProducto = $('#factd_MontoDescuento').val();
            document.getElementById("TotalDescuento").innerHTML = parseFloat(totalProducto);
        }
        else {
            document.getElementById("TotalDescuento").innerHTML = parseFloat(TotalDescuento) + parseFloat(Descuento);
        }

        //Subtotal 
        var totalProducto = $('#SubtotalProducto').val();
        var subtotal = parseFloat(document.getElementById("Subtotal").innerHTML);

        if (document.getElementById("Subtotal").innerHTML == '') {
            totalProducto = $('#SubtotalProducto').val();
            document.getElementById("Subtotal").innerHTML = parseFloat(totalProducto);
        }
        else {
            document.getElementById("Subtotal").innerHTML = parseFloat(subtotal) + parseFloat(totalProducto);
        }
        //Impuesto
        var Cantidad = CantidadProducto
        var Precio = PrecioUnitario
        var impuesto = parseFloat(document.getElementById("factd_Impuesto").value.replace(',', '.'));
        var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
        var porcentaje = parseFloat(impuesto / 100);
        var impuestos = (Cantidad * Precio) * porcentaje;
        console.log(impuestos)

        if (document.getElementById("isv").innerHTML == '') {
            impuesto = document.getElementById("factd_Impuesto").value;
            document.getElementById("isv").innerHTML = parseFloat(impuestos);
        }
        else {
            document.getElementById("isv").innerHTML = parseFloat(impuestotal) + parseFloat(impuestos);
        }

        //Grantotal
        if (document.getElementById("total").innerHTML == '') {
            var TotalEncabezado = document.getElementById("total").innerHTML = parseFloat(totalProducto) + parseFloat(impuestos) - parseFloat(Descuento);
            $("#TotalProductoEncabezado").val(TotalEncabezado);
        }
        else {
            var TotalEncabezado = document.getElementById("total").innerHTML = parseFloat(subtotal) + parseFloat(totalProducto) + parseFloat(impuestotal) + parseFloat(impuestos) - parseFloat(TotalDescuento) - parseFloat(Descuento);
            $("#TotalProductoEncabezado").val(TotalEncabezado);
        }


        var FacturaDetalleEdit = GetFacturaDetalleEdit();
        $.ajax({
            url: "/Factura/SaveFacturaDetalleEdit",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ FacturaDetalleEdit: FacturaDetalleEdit }),
        })
        .done(function (data) {
            $('#ErrorCodigoProductoCreate').text('');
            $('#ErrorMontoDescuentoCreate').text('');
            $('#ErrorCantidadCreate').text('');
            $('#ErrorImpuestoCreate').text('');
            //Input
            $('#prod_CodigoBarras').val('');
            $('#prod_Codigo').val('');
            $('#factd_MontoDescuento').val('');
            $('#Impuesto').val('');
            $('#tbProducto_prod_Descripcion').val('');
            $('#factd_Cantidad').val('');
            $('#SubtotalProducto').val('');
            $('#factd_PrecioUnitario').val('');
            $('#factd_Impuesto').val('');
            $('#TotalProducto').val('');
        });
    }
});

function GetFacturaDetalleEdit() {

    var FacturaDetalleEdit = {
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
    return FacturaDetalleEdit
};

$(document).on("click", "#tblDetalleFactura tbody tr td button#removeFacturaDetalle", function () {

    //Descuento
    var Descuento = $(this).parents("tr").find("td")[5].innerHTML;
    var TotalDescuento = parseFloat(document.getElementById("TotalDescuento").innerHTML);
    document.getElementById("TotalDescuento").innerHTML = parseFloat(TotalDescuento) - parseFloat(Descuento);

    //Subtotal
    var Cantidad = $(this).parents("tr").find("td")[2].innerHTML;
    var Precio = $(this).parents("tr").find("td")[3].innerHTML;
    var SubtotalProducto = Cantidad * Precio;
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
        url: "/Factura/RemoveFacturaDetalleEdit",
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

//$('#AgregarDetalleFactura').click(function () {
//    console.log(total);
//    if (document.getElementById("total").innerHTML == '') {
//        totalProducto = $('#TotalProducto').val();
//        document.getElementById("total").innerHTML = parseFloat(totalProducto);
//    }
//    else {
//        document.getElementById("total").innerHTML = parseFloat(subtotal) + parseFloat(totalProducto);
//    }

//})