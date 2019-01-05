


//var contador = 0;

//$('#AgregarDetalle').click(function () {
//    var CodigoProducto = $('#prod_Codigo').val();
//    var PorcentajeDescuento = $('#factd_PorcentajeDescuento').val();
//    var MontoDescuento = $('#factd_MontoDescuento').val();
//    var DescripcionProducto = $('#tbProducto_prod_Descripcion').val();
//    var CantidadProducto = $('#factd_Cantidad').val();
//    var Subtotal = $('#SubtotalProducto').val();
//    var PrecioUnitario = $('#factd_PrecioUnitario').val();
//    var Impuesto = $('#factd_Impuesto').val();
//    var Total = $('#TotalProducto').val();

//    if (CodigoProducto == '') {
//        $('#ErrorCodigoProductoCreate').text('');
//        $('#ErrorMontoDescuentoCreate').text('');
//        $('#ErrorCantidadCreate').text('');
//        $('#ErrorImpuestoCreate').text('');
//        $('#validationCodigoProductoCreate').after('<ul id="ErrorCodigoProductoCreate" class="validation-summary-errors text-danger">Campo Código Producto requerido</ul>');
//    }
//    else if (MontoDescuento == '') {
//        $('#ErrorCodigoProductoCreate').text('');
//        $('#ErrorMontoDescuentoCreate').text('');
//        $('#ErrorCantidadCreate').text('');
//        $('#ErrorImpuestoCreate').text('');
//        $('#validationMontoDescuentoCreate').after('<ul id="ErrorMontoDescuentoCreate" class="validation-summary-errors text-danger">Campo Monto Descuento requerido</ul>');
//    }
//    else if (CantidadProducto == '') {
//        $('#ErrorCodigoProductoCreate').text('');
//        $('#ErrorMontoDescuentoCreate').text('');
//        $('#ErrorCantidadCreate').text('');
//        $('#ErrorImpuestoCreate').text('');
//        $('#validationCantidadProductoCreate').after('<ul id="ErrorCantidadCreate" class="validation-summary-errors text-danger">Campo Cantidad requerido</ul>');
//    }
//    else if (Impuesto == '') {
//        $('#ErrorCodigoProductoCreate').text('');
//        $('#ErrorMontoDescuentoCreate').text('');
//        $('#ErrorCantidadCreate').text('');
//        $('#ErrorImpuestoCreate').text('');
//        $('#validationImpuestoProductoCreate').after('<ul id="ErrorImpuestoCreate" class="validation-summary-errors text-danger">Campo Impuesto requerido</ul>');
//    }
//    else {
//        contador = contador + 1;
//        copiar = "<tr data-id=" + contador + ">";
//        copiar += "<td id = 'prod_CodigoCreate'>" + CodigoProducto + "</td>";
//        copiar += "<td id = 'tbProducto_prod_DescripcionCreate'>" + DescripcionProducto + "</td>";
//        copiar += "<td id = 'factd_CantidadCreate'>" + CantidadProducto + "</td>";
//        copiar += "<td id = 'Precio_UnitarioCreate'>" + PrecioUnitario + "</td>";
//        copiar += "<td id = 'factd_MontoDescuentoCreate'>" + MontoDescuento + "</td>";
//        copiar += "<td id = 'TotalProductoCreate'>" + Total + "</td>";
//        copiar += "<td>" + '<button id="removeFacturaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
//        copiar += "</tr>";
//        $('#tblDetalleFactura').append(copiar);

//        //Subtotal 
//        var totalProducto = $('#TotalProducto').val();
//        var sutotal = parseFloat(document.getElementById("Subtotal").innerHTML);

//        if (document.getElementById("Subtotal").innerHTML == '') {
//            totalProducto = $('#TotalProducto').val();
//            document.getElementById("Subtotal").innerHTML = parseFloat(totalProducto);
//        }
//        else {
//            document.getElementById("Subtotal").innerHTML = parseFloat(sutotal) + parseFloat(totalProducto);
//        }

//        //Impuesto
//        var totalProducto = document.getElementById("TotalProducto").value;
//        var impuesto = document.getElementById("factd_Impuesto").value;
//        var impuestototal = parseFloat(document.getElementById("isv").innerHTML);
//        var porcentaje = (impuesto / 100);
//        var impuestos = (totalProducto * porcentaje);

//        if (document.getElementById("isv").innerHTML == '') {
//            impuesto = document.getElementById("factd_Impuesto").value;
//            document.getElementById("isv").innerHTML = parseFloat(impuestos);
//        }
//        else {
//            document.getElementById("isv").innerHTML = parseFloat(impuestototal) + parseFloat(impuestos);
//        }

//        //Grantotal
//        if (document.getElementById("total").innerHTML == '') {
//            document.getElementById("total").innerHTML = parseFloat(totalProducto) + parseFloat(impuestos);
//        }
//        else {
//            document.getElementById("total").innerHTML = parseFloat(sutotal) + parseFloat(totalProducto) + parseFloat(impuestototal) + parseFloat(impuestos);
//        }


//        var FacturaDetalle = GetFacturaDetalle();
//        $.ajax({
//            url: "/Factura/SaveFacturaDetalle",
//            method: "POST",
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify({ FacturaDetalleC: FacturaDetalle }),
//        })
//        .done(function (data) {
//            $('#ErrorCodigoProductoCreate').text('');
//            $('#ErrorMontoDescuentoCreate').text('');
//            $('#ErrorCantidadCreate').text('');
//            $('#ErrorImpuestoCreate').text('');
//            //Input
//            $('#prod_Codigo').val('');
//            $('#factd_PorcentajeDescuento').val('');
//            $('#factd_MontoDescuento').val('');
//            $('#tbProducto_prod_Descripcion').val('');
//            $('#factd_Cantidad').val('');
//            $('#SubtotalProducto').val('');
//            $('#factd_PrecioUnitario').val('');
//            $('#factd_Impuesto').val('');
//            $('#TotalProducto').val('');
//        });
//    }
//});

//function GetFacturaDetalle() {

//    var FacturaDetalle = {
//        prod_Codigo: $('#prod_Codigo').val(),
//        factd_PorcentajeDescuento: $('#factd_PorcentajeDescuento').val(),
//        factd_MontoDescuento: $('#factd_MontoDescuento').val(),
//        tbProducto_prod_Descripcion: $('#tbProducto_prod_Descripcion').val(),
//        factd_Cantidad: $('#factd_Cantidad').val(),
//        SubtotalProducto: $('#SubtotalProducto').val(),
//        factd_PrecioUnitario: $('#factd_PrecioUnitario').val(),
//        factd_Impuesto: $('#factd_Impuesto').val(),
//        TotalProducto: $('#TotalProducto').val(),
//        factd_Id: contador
//    }
//    return FacturaDetalle
//};

//$(document).on("click", "#tblDetalleFactura tbody tr td button#removeFacturaDetalle", function () {
//    var TotalProducto = parseFloat(document.getElementById("TotalProductoCreate").innerHTML);
//    console.log(TotalProducto)
//    var subtotal = parseFloat(document.getElementById("Subtotal").innerHTML);
//    console.log(subtotal)
//    document.getElementById("Subtotal").innerHTML = parseFloat(subtotal) - parseFloat(TotalProducto);
//    $(this).closest('tr').remove();
//    idItem = $(this).closest('tr').data('id');
//    var FacturaDetalle = {
//        factd_Id: idItem,
//    };
//    $.ajax({
//        url: "/Factura/RemoveFacturaDetalle",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ FacturaDetalleC: FacturaDetalle }),
//    });
//});

////Validacion de numeros//
//function format(input) {
//    var num = input.value.replace(/\,/g, '');
//    if (!isNaN(num)) {
//        input.value = num;
//    }
//    else {
//        alert('Solo se permiten numeros');
//        input.value = input.value.replace(/[^\d\.]*/g, '');
//    }
//}




















//var contador = 0;

//$('#AgregarDetalle').click(function () {
//    alert('csccascsac');
   
//    var IdDeno = $('#deno_Id').val();
//    var SDCantidadSolicitada = $('#soled_CantidadSolicitada').val();
//    var SDMontoSolicitado = $('#MontoSolicitado').val();
//    var SDCantidadEntregada = $('#soled_CantidadEntregada').val();
//    var SDMontoEntregado = $('#soled_MontoEntregado').val();


//    if (IdDeno == '') {
//        $('#ErrorIdDenoCreate').text('');
//        $('#ErrorSDCantidadSolicitadaCreate').text('');
//        $('#ErrorSDMontoSolicitadoCreate').text('');
//        $('#ErrorSDCantidadEntregadaCreate').text('');
//        $('#ErrorSDMontoEntregadoCreate').text('');
//        $('#validationDenoIdCreate').after('<ul id="ErrorIdDenoCreate" class="validation-summary-errors text-danger">Campo Denominación es requerido</ul>');
//    }
        

//    else if (SDCantidadSolicitada == '') {
//        $('#ErrorIdDenoCreate').text('');
//        $('#ErrorSDCantidadSolicitadaCreate').text('');
//        $('#ErrorSDMontoSolicitadoCreate').text('');
//        $('#ErrorSDCantidadEntregadaCreate').text('');
//        $('#ErrorSDMontoEntregadoCreate').text('');
//        $('#validationCantidadSolicitadaCreate').after('<ul id="ErrorSDCantidadSolicitadaCreate" class="validation-summary-errors text-danger">Campo Cantidad Solicitada es requerido</ul>');
//    }

//    else if (SDMontoSolicitado == '') {
//        $('#ErrorIdDenoCreate').text('');
//        $('#ErrorSDCantidadSolicitadaCreate').text('');
//        $('#ErrorSDMontoSolicitadoCreate').text('');
//        $('#ErrorSDCantidadEntregadaCreate').text('');
//        $('#ErrorSDMontoEntregadoCreate').text('');
//        $('#validationMontosSolicitadoCreate').after('<ul id="ErrorSDMontoSolicitadoCreate" class="validation-summary-errors text-danger">Campo Monto Solicitado es requerido</ul>');
//    }

//    else if (SDCantidadEntregada == '') {
//        $('#ErrorIdDenoCreate').text('');
//        $('#ErrorSDCantidadSolicitadaCreate').text('');
//        $('#ErrorSDMontoSolicitadoCreate').text('');
//        $('#ErrorSDCantidadEntregadaCreate').text('');
//        $('#ErrorSDMontoEntregadoCreate').text('');
//        $('#validationCantidadEntregadaCreate').after('<ul id="ErrorSDCantidadEntregadaCreate" class="validation-summary-errors text-danger">Campo Cantidad Entregada es requerido</ul>');
//    }
//    else if (SDMontoEntregado == '') {
//        $('#ErrorIdDenoCreate').text('');
//        $('#ErrorSDCantidadSolicitadaCreate').text('');
//        $('#ErrorSDMontoSolicitadoCreate').text('');
//        $('#ErrorSDCantidadEntregadaCreate').text('');
//        $('#ErrorSDMontoEntregadoCreate').text('');
//        $('#validationMontoEntregadoCreate').after('<ul id="ErrorSDMontoEntregadoCreate" class="validation-summary-errors text-danger">Campo Monto Entregado es requerido</ul>');
//    }

//    else {
//        contador = contador + 1;
//        copiar = "<tr data-id=" + contador + ">";
//        copiar += "<td id = 'IdDenoCreate'>" + IdDeno + "</td>";
//        copiar += "<td id = 'SDCantidadSolicitadaCreate'>" + SDCantidadSolicitada + "</td>";
//        copiar += "<td id = 'SDMontoSolicitadoCreate'>" + SDMontoSolicitado + "</td>";
//        copiar += "<td id = 'SDCantidadEntregadaCreate'>"+ SDCantidadEntregada + "</td>";
//        copiar += "<td id = 'SDMontoEntregadoCreate'>" + SDMontoEntregado + "</td>";
//        copiar += "<td>" + '<button id="removeSolicitudEfectivoDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
//        copiar += "</tr>";
//        $('#tblSolicitudEfectivoDetalle').append(copiar);

//        var SolicitudDetalle = GetSolicitudEfectivo();
//        $.ajax({
//            url: "/SolicitudEfectivo/SaveSolicitudEfectivoDetalle",
//            method: "POST",
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify({ SolicitudEfectivoDet: SolicitudDetalle }),
//        })
//        .done(function (data) {
          

//            $('#deno_Id').val('');
//            $('#soled_CantidadSolicitada').val('');
//            $('#MontoSolicitado').val('');
//            $('#soled_CantidadEntregada').val('');
//            $('#soled_MontoEntregado').val('');
//        });
//    }
//});


//function GetSolicitudEfectivo() {

//    var SolicitudDetalle = {
//        deno_Id: $('#deno_Id').val(),
//        soled_CantidadSolicitada: $('#soled_CantidadSolicitada').val(),
//        MontoSolicitado: $('#MontoSolicitado').val(),
//        soled_CantidadEntregada: $('#soled_CantidadEntregada').val(),
//        soled_MontoEntregado: $('#soled_MontoEntregado').val(),

       
//        deno_Id: contador
//    }
//    return SolicitudDetalle
//};

//$(document).on("click", "#tblSolicitudEfectivoDetalle tbody tr td button#removeSolicitudEfectivoDetalle", function () {
//    $(this).closest('tr').remove();
//    idItem = $(this).closest('tr').data('id');
//    var SolicitudDetalle = {
//        soled_Id: idItem,
//    };
//    $.ajax({
//        url: "/Instructor/RemoveCasoExito",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ DetalleSolicitudEfectivocont: SolicitudDetalle }),
//    });
//});