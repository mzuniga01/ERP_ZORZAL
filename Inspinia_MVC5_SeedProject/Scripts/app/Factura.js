$(document).ready(function () {
    $('#prod_CodigoBarras').focus();
    $('#Alcredito').hide();
    $('#factd_Cantidad').prop('disabled', true);
    $('#tblDetalleFactura').DataTable({
        "searching": false,
        "lengthChange": true,
        "responsive": true,
        "oLanguage": {
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior",
            },
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "No hay registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoEmpty": "Mostrando 0 de 0 Entradas",
            "sSearch": "Buscar",
            "sLengthMenu": "Mostrar _MENU_ registros por página",
            "sInfo": "Mostrando _START_ a _END_ Entradas"
        }
    });

    $('#factd_Impuesto').val((0).toFixed(2));
    $('#factd_PrecioUnitario').val((0).toFixed(2));
    $('#factd_Cantidad').val((0).toFixed(2));
    $('#factd_PorcentajeDescuento').val((0).toFixed(2));
    $('#factd_MontoDescuento').val((0).toFixed(2));
    $('#Impuesto').val((0).toFixed(2));
    $('#SubtotalProducto').val((0).toFixed(2));
    $('#TotalProducto').val((0).toFixed(2));
});

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8) || event.key === "." || (key == 48))
}

function pierdeFoco(e) {
}

//Factura Buscar Cliente
$("#searchCliente").keyup(function () {
    var $rows = $('#ClienteTbody tr');
    var val = $.trim($(this).val()).replace(/ +/g, ' ').toUpperCase();
    if (val.length >= 3) {
        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toUpperCase();
            return !~text.indexOf(val);
        }).hide();
    }
    else if (val.length >= 1) {
        $rows.show().filter(function () {
        }).hide();
    }
});

//Factura Seleccionar Cliente
$(document).on("click", "#tbCliente tbody tr td button#seleccionar", function () {
    $("#fact_AlCredito").prop("checked", false);
    $('#Cred1').hide();
    $('#fact_DiasCredito').val('');
    valido = document.getElementById('DiasError');
    valido.innerText = "";
    $("#factd_PorcentajeDescuento").val(0);

    idItem = $(this).closest('tr').data('id');
    rtnItem = $(this).closest('tr').data('rtn');
    nombreItem = $(this).closest('tr').data('nombrecliente');
    tpid = $(this).closest('tr').data('tpi');
    Fecha = $(this).closest('tr').data('fecha');
    Persona1 = $(this).parents("tr").find("td")[5].innerHTML;
    ConCredito = $(this).parents("tr").find("td")[6].innerHTML;
    DiasCred = $(this).parents("tr").find("td")[7].innerHTML;
    DiasCredito = parseInt(DiasCred.trim())
    $('#fact_DiasCredito').change(function () {
        var Dias = $('#fact_DiasCredito').val()
        if (Dias > DiasCredito) {
            valido = document.getElementById('DiasError');
            valido.innerText = "Dias de Crédito Autorizado, " + DiasCredito;
        }
        else {
            valido = document.getElementById('DiasError');
            valido.innerText = "";
        }
    })

    LabelIdentificacion = $(this).parents("tr").find("td")[3].innerHTML;
    valido = document.getElementById('label_identificacion');
    document.getElementById('label_identificacion').innerHTML = LabelIdentificacion + '<span style="color:red"> *</span>';
    nuevaCadena = Persona1.trim();
    ConCredito1 = ConCredito.trim();
    $("#clte_Id").val(idItem);
    $("#cliente_Identificacionxx").val(rtnItem);
    $("#cliente_Nombresxx").val(nombreItem);
    $("#tpi_Id").val(tpid);
    $("#clte_Fecha").val(Fecha);
    $('#ModalAgregarCliente').modal('hide');

    if (ConCredito1 != "Si") {
        $('#Alcredito').hide();
        $("#fact_AlCredito").prop("checked", false);
        $('#fact_DiasCredito').val('');
    }
    else {
        $('#Alcredito').show();
    }
    if (nuevaCadena == "Si") {
        $('#TerceraEdad').show();
        //Tercera Edad
        ms = Date.parse(Fecha);
        fecha1 = new Date(ms);
        var Fechas1 = fecha1.getFullYear()
        var today = new Date
        var today1 = today.getFullYear()
        var Edad = today1 - Fechas1
        if (Edad >= 60) {
            $("#MostrarTerceraEdad").prop("checked", true);
            $("#fact_AutorizarDescuento").prop("checked", true);
            $('#Cred2').show();
            $('#fact_PorcentajeDescuento').val('');
            document.getElementById("MostrarTerceraEdad").disabled = true;
            document.getElementById("fact_AutorizarDescuento").disabled = true;
        }
        else {
            $("#MostrarTerceraEdad").prop("checked", false);
            $("#fact_AutorizarDescuento").prop("checked", false);
            $('#Cred2').hide();
            $('#TerceraEdad').hide();
            $('#fact_PorcentajeDescuento').val(0);
            document.getElementById("MostrarTerceraEdad").disabled = false;
            document.getElementById("fact_AutorizarDescuento").disabled = false;
        }
    }
    else {
        $('#TerceraEdad').hide();
        document.getElementById("MostrarTerceraEdad").disabled = false;
        document.getElementById("fact_AutorizarDescuento").disabled = false;
        $('#Cred2').hide();
    }
});

//Modal Producto
$("#Producto").click(function () {
    ListaProductos();
})

function ListaProductos() {
    url = "/Factura/ListaProductos";
    var table = $('#tbProductoFactura').dataTable({
        destroy: true,
        resposive: true,
        ajax: {
            method: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            "dataSrc": ""
        },
        "columns": [
            { "data": "prod_Codigo" },
            { "data": "prod_Descripcion" },
            { "data": "prod_CodigoBarras" },
            { "defaultContent": "<button class='btn btn-primary btn-xs'  id='seleccionar' data-dismiss='modal'>Seleccionar</button>" }
        ],
        "searching": false,
        "lengthChange": false,
        "oLanguage": {
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior",
            },
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sEmptyTable": "No hay registros",
            "sInfoEmpty": "Mostrando 0 de 0 Entradas",
            "sSearch": "Buscar",
            "sInfo": "Mostrando _START_ a _END_ Entradas",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
        }
    });
    $('#ModalAgregarProducto').modal('show');
}

//Factura Buscar Producto
$("#search").keyup(function () {
var $rows = $('#ProductoTbody tr');
    var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
    if (val.length >= 3) {
        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
    }
    else if (val.length >= 1) {
        $rows.show().filter(function () {
        }).hide();
    }
});

// Factura Seleccionar Producto
$(document).on("click", "#tbProductoFactura tbody tr td button#seleccionar", function () {
    var currentRow = $(this).closest("tr");
    var prod_CodigoBarrasItem = currentRow.find("td:eq(2)").text();
    var prod_DescripcionItem = currentRow.find("td:eq(1)").text();
    var prod_CodigoItem = currentRow.find("td:eq(0)").text();
    var bod_Id = $('#bod_Id').val();
    $('#prod_CodigoBarras').val(prod_CodigoBarrasItem);
    document.getElementById("prod_CodigoBarras").focus();
    $('#tbProducto_prod_Descripcion').val(prod_DescripcionItem);
    $('#prod_Codigo').val(prod_CodigoItem);
    $('#ModalAgregarProducto').modal('hide');
    SeleccionProducto();
});

function SeleccionProducto() {
    var CodBarra = $('#prod_CodigoBarras').val();
    var IDSucursal = $('#suc_Id').val();
    var IDCliente = $('#clte_Id').val();
    $.ajax({
        type: 'POST',
        url: '/Factura/BuscarCodigoBarras',
        data: JSON.stringify({ IDSucursal: IDSucursal, CodBarra: CodBarra, IDCliente: IDCliente }),
        contentType: 'application/json;',
        dataType: 'json',
        success: function (data) {
            if (data.length > 0) {
                $.each(data, function (key, val) {
                    if (val.EXISTE) {
                        data_producto = val.CODIGOPRODUCTO;
                        data_descripcion = val.DESCRIPCIONPRODUCTO;
                        data_impuesto = val.IMPUESTOPRODUCTO;
                        data_precio = val.PRECIOUNITARIO;
                        descuento = val.DESCUENTOCAJERO;
                        if (data_precio == null) {
                            $('#factd_Impuesto').val((0).toFixed(2));
                            $('#factd_PrecioUnitario').val((0).toFixed(2));
                            $('#factd_Cantidad').val(0);
                            $('#factd_PorcentajeDescuento').val((0).toFixed(2));
                            $('#factd_MontoDescuento').val((0).toFixed(2));
                            $('#Impuesto').val((0).toFixed(2));
                            $('#SubtotalProducto').val((0).toFixed(2));
                            $('#TotalProducto').val((0).toFixed(2));
                            $('#factd_Cantidad').val(0);
                            $('#factd_Cantidad').prop('disabled', true);
                            $('#msjSinPrecio').show();
                            $('#alerta').hide();
                            $('#SinCantidad').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            $('#CantidadDistinta').hide();
                        }
                        else {
                            $('#factd_Cantidad').prop('disabled', false);
                            $('#prod_Codigo').val(data_producto);
                            $('#tbProducto_prod_Descripcion').val(data_descripcion);
                            $('#factd_Impuesto').val(data_impuesto.toFixed(2));
                            $('#factd_PrecioUnitario').val(data_precio.toFixed(2));
                            $('#factd_Cantidad').val(1);
                            $('#factd_PorcentajeDescuento').val(descuento.toFixed(2));
                            $('#factd_Cantidad').prop('disabled', false);
                            $('#factd_Cantidad').focus();
                            Cantidad = $('#factd_Cantidad').val();
                            PrecioSubtotal = (parseFloat(data_precio) * parseFloat(Cantidad));
                            MontoDescuento = (PrecioSubtotal * (parseFloat(descuento) / 100));
                            $('#factd_MontoDescuento').val(MontoDescuento.toFixed(2));
                            Impuesto = ((PrecioSubtotal - MontoDescuento) * (parseFloat(data_impuesto) / 100));
                            $('#Impuesto').val(Impuesto.toFixed(2));
                            total = parseFloat((PrecioSubtotal - MontoDescuento) + Impuesto);
                            $('#SubtotalProducto').val((PrecioSubtotal).toFixed(2));
                            $('#TotalProducto').val((total).toFixed(2));
                            $('#msjSinPrecio').hide();
                            $('#alerta').hide();
                            $('#SinCantidad').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            $('#CantidadDistinta').hide();
                        }
                    }
                })
            }
        }
    });
}

//Muestra un mensaje de Si hay productos en exitencias.
$("#factd_Cantidad").on("blur", function (event) {
    GetCantidad();
});

//Enter Codigo de barras
$("#prod_CodigoBarras").keyup(function (e) {
    var prodCodigoBarrar = $("#prod_CodigoBarras").val();
    var code = e.which; 
    if (code == 13) e.preventDefault();
    if (code == 32 || code == 13 || code == 188 || code == 186) {
        if (prodCodigoBarrar == '') {
            $('#factd_Cantidad').prop('disabled', true);
        }
        else {
            BuscarCodigoBarras();
        }
    }
})

//Enter cantidad
$("#factd_Cantidad").keyup(function (e) {
    GetCantidad();//Validar que cuando no haya cantidad no permita ingresar el detalle.
    
    var code = e.which;
    if (code == 13) e.preventDefault();
    if (code == 32 || code == 13 || code == 188 || code == 186) {
        if ($("#factd_Cantidad").val() != '' && parseFloat($("#factd_Cantidad").val()) != 0)
        {
            var cantfisica_nueva = $('#factd_Cantidad').val();
            addtable(cantfisica_nueva);
        }
        else
        {
            $('#msjSinPrecio').hide();
            $('#alerta').hide();
            $('#SinCantidad').hide();
            $('#CantidadNoDisponible').hide();
            $('#NoExiste').hide();
            $('#CantidadDistinta').show();
        }
    }
});

function GetCantidad() {
    var CodSucursal = $('#suc_Id').val();
    var CodProducto = $('#prod_Codigo').val();
    var CantidadIngresada = $('#factd_Cantidad').val();

    if (CantidadIngresada == '') {
        $('#factd_MontoDescuento').val((0).toFixed(2));
        $('#Impuesto').val((0).toFixed(2));
        $('#SubtotalProducto').val((0).toFixed(2));
        $('#TotalProducto').val((0).toFixed(2));
    }
    else {
        $.ajax({
            url: "/Factura/GetCantidad",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodSucursal: CodSucursal, CodProducto: CodProducto }),
        })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (key, val) {
                    var MENSAJE = data[0]['MENSAJE'];
                    if (MENSAJE) {
                        var can = data[0]['CANTIDAD'];
                        var CANTIDAD = parseFloat(can);
                        if (CANTIDAD < CantidadIngresada) {
                            $('#SinCantidad').show();
                            $('#msjSinPrecio').hide();
                            $('#alerta').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            $('#CantidadDistinta').hide();
                            $('#factd_Cantidad').prop('disabled', false);
                        }
                        else {
                            $('#msjSinPrecio').hide();
                            $('#alerta').hide();
                            $('#SinCantidad').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            $('#CantidadDistinta').hide();
                            //Calcular
                            Cantidad = $('#factd_Cantidad').val();
                            PrecioSubtotal = (parseFloat(data_precio) * parseFloat(Cantidad));
                            MontoDescuento = (PrecioSubtotal * (parseFloat(descuento) / 100));
                            Impuesto = ((PrecioSubtotal) * (parseFloat(data_impuesto) / 100));
                            PrecioSubtotal = PrecioSubtotal - MontoDescuento;
                            $('#factd_MontoDescuento').val(MontoDescuento.toFixed(2));
                            $('#Impuesto').val(Impuesto.toFixed(2));
                            total = parseFloat((PrecioSubtotal) + Impuesto);
                            $('#SubtotalProducto').val((PrecioSubtotal).toFixed(2));
                            $('#TotalProducto').val((total).toFixed(2));
                            $('#factd_Cantidad').prop('disabled', false);
                        }
                    }
                    else {
                        $('#CantidadNoDisponible').show();
                        $('#SinCantidad').show();
                        $('#msjSinPrecio').hide();
                        $('#alerta').hide();
                        $('#NoExiste').hide();
                        $('#CantidadDistinta').hide();
                        $('#factd_Cantidad').prop('disabled', true);
                    }
                });
            }
        });
    }
}

function addtable(cantfisica_nueva) {
    var IDSucursal = $('#suc_Id').val();
    var IDCliente = $('#clte_Id').val();
    var table = $('#tblDetalleFactura').DataTable();
    var FacturaDetalle = GetFacturaDetalle();
    $.ajax({
        url: "/Factura/SaveFacturaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ FacturaDetalleC: FacturaDetalle, data_producto: data_producto })
    })
    .done(function (datos) {
        if (datos == data_producto) {
            $("#tblDetalleFactura td").each(function () {
                var prueba = $(this).text();
                if (prueba == data_producto) {
                    var Cantidad = $('#factd_Cantidad').val();
                    var CantidadSum = $(this).parents("tr").find("td")[2].innerHTML;
                    CantidadSum = parseFloat(CantidadSum) + parseFloat(Cantidad);
                    var PrecioSum = $(this).parents("tr").find("td")[3].innerHTML;
                    var Descuento = $(this).closest("tr").find("td:eq(5)").text();
                    var impuesto = parseFloat(document.getElementById("factd_Impuesto").value.replace(',', '.'));
                    var SubTotal = (parseFloat(PrecioSum) * parseFloat(CantidadSum));
                    var MontoDescuento = (SubTotal * (parseFloat(Descuento) / 100));
                    var Impuesto = ((SubTotal) * (parseFloat(data_impuesto) / 100));
                    SubTotal = SubTotal - MontoDescuento;
                    var Total = parseFloat((SubTotal) + Impuesto);
                    $(this).closest('tr').remove();
                    table.row($(this).parents('tr')).remove().draw();
                    table.row.add([
                    data_producto,
                    data_descripcion,
                    parseFloat(CantidadSum).toFixed(2),
                    data_precio,
                    data_impuesto,
                    Descuento,
                    parseFloat(Total).toFixed(2),
                    '<button id = "removeFacturaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">-</button>'
                    ]).draw(false);
                    //LimpiarControles();
                }
            });
        }
        else {
            var CodigoProducto = $('#prod_Codigo').val();
            var DescripcionProducto = $('#tbProducto_prod_Descripcion').val();
            var Cantidad = $('#factd_Cantidad').val();
            var Total = $('#TotalProducto').val();
            table.row.add([
                        data_producto,
                        data_descripcion,
                        parseFloat(Cantidad).toFixed(2),
                        parseFloat(data_precio).toFixed(2),
                        parseFloat(data_impuesto).toFixed(2),
                        parseFloat(descuento).toFixed(2),
                        parseFloat(Total).toFixed(2),
                        '<button id = "removeFacturaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">-</button>'
            ]).draw(false);
            LimpiarControles();
        }
    })
}

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
    }
    return FacturaDetalle
};

function LimpiarControles()
{
    $('#prod_CodigoBarras').val('');
    $('#prod_Codigo').val('');
    $('#tbProducto_prod_Descripcion').val('');
    $('#factd_Impuesto').val((0).toFixed(2));
    $('#factd_PrecioUnitario').val((0).toFixed(2));
    $('#factd_Cantidad').val((0).toFixed(2));
    $('#factd_PorcentajeDescuento').val((0).toFixed(2));
    $('#factd_MontoDescuento').val((0).toFixed(2));
    $('#Impuesto').val((0).toFixed(2));
    $('#SubtotalProducto').val((0).toFixed(2));
    $('#TotalProducto').val((0).toFixed(2));
    $('#prod_CodigoBarras').focus();
    $('#factd_Cantidad').prop('disabled', true);
}

function BuscarCodigoBarras()
{
    var CodBarra = $('#prod_CodigoBarras').val();
    var IDSucursal = $('#suc_Id').val();
    var IDCliente = $('#clte_Id').val();
    $.ajax({
        type: 'POST',
        url: '/Factura/BuscarCodigoBarras',
        data: JSON.stringify({ IDSucursal: IDSucursal, CodBarra: CodBarra, IDCliente: IDCliente }),
        contentType: 'application/json;',
        dataType: 'json',
        success: function (data) {
            if (data.length > 0) {
                $.each(data, function (key, val) {
                    if (val.EXISTE) {
                        data_producto = val.CODIGOPRODUCTO;
                        data_descripcion = val.DESCRIPCIONPRODUCTO;
                        data_impuesto = val.IMPUESTOPRODUCTO;
                        data_precio = val.PRECIOUNITARIO;
                        descuento = val.DESCUENTOCAJERO;
                        if (data_precio == null) {
                            $('#factd_Impuesto').val((0).toFixed(2));
                            $('#factd_PrecioUnitario').val((0).toFixed(2));
                            $('#factd_Cantidad').val(0);
                            $('#factd_PorcentajeDescuento').val((0).toFixed(2));
                            $('#factd_MontoDescuento').val((0).toFixed(2));
                            $('#Impuesto').val((0).toFixed(2));
                            $('#SubtotalProducto').val((0).toFixed(2));
                            $('#TotalProducto').val((0).toFixed(2));
                            $('#factd_Cantidad').val(0);
                            $('#factd_Cantidad').prop('disabled', true);
                            $('#msjSinPrecio').show();
                            $('#alerta').hide();
                            $('#SinCantidad').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                        }
                        else {
                            $('#factd_Cantidad').prop('disabled', false);
                            $('#prod_Codigo').val(data_producto);
                            $('#tbProducto_prod_Descripcion').val(data_descripcion);
                            $('#factd_Impuesto').val(data_impuesto.toFixed(2));
                            $('#factd_PrecioUnitario').val(data_precio.toFixed(2));
                            $('#factd_Cantidad').val(1);
                            $('#factd_PorcentajeDescuento').val(descuento.toFixed(2));
                            $('#factd_Cantidad').prop('disabled', false);
                            $('#factd_Cantidad').focus();
                            Cantidad = $('#factd_Cantidad').val();
                            PrecioSubtotal = (parseFloat(data_precio) * parseFloat(Cantidad));
                            MontoDescuento = (PrecioSubtotal * (parseFloat(descuento) / 100));
                            Impuesto = ((PrecioSubtotal) * (parseFloat(data_impuesto) / 100));
                            PrecioSubtotal = PrecioSubtotal - MontoDescuento;
                            $('#factd_MontoDescuento').val(MontoDescuento.toFixed(2));
                            $('#Impuesto').val(Impuesto.toFixed(2));
                            total = parseFloat((PrecioSubtotal) + Impuesto);
                            $('#SubtotalProducto').val((PrecioSubtotal).toFixed(2));
                            $('#TotalProducto').val((total).toFixed(2));
                            $('#msjSinPrecio').hide();
                            $('#alerta').hide();
                            $('#SinCantidad').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                        }
                    }
                    else {
                        $('#factd_Cantidad').prop('disabled', true);
                        $('#tbProducto_prod_Descripcion').val('');
                        $('#prod_Codigo').val('');
                        $('#factd_Impuesto').val((0).toFixed(2));
                        $('#factd_PrecioUnitario').val((0).toFixed(2));
                        $('#factd_Cantidad').val((0).toFixed(2));
                        $('#factd_PorcentajeDescuento').val((0).toFixed(2));
                        $('#factd_MontoDescuento').val((0).toFixed(2));
                        $('#Impuesto').val((0).toFixed(2));
                        $('#SubtotalProducto').val((0).toFixed(2));
                        $('#TotalProducto').val((0).toFixed(2));
                        $('#msjSinPrecio').hide();
                        $('#alerta').hide();
                        $('#SinCantidad').hide();
                        $('#CantidadNoDisponible').hide();
                        $('#NoExiste').show();
                    }
                })
            }
        }
    });
}

$(document).on("click", "#tblDetalleFactura tbody tr td button#removeFacturaDetalle", function () {
    var FacturaDetalle = {
        prod_Codigo: $(this).parents("tr").find("td")[0].innerHTML,
    };
    console.log('Remove');
    ////Descuento
    var PorcentajeDescuento = $(this).parents("tr").find("td")[5].innerHTML;
    console.log('PorcentajeDescuento', PorcentajeDescuento);
    var TotalDescuento = $(this).parents("tr").find("td")[6].innerHTML;
    console.log('TotalDescuento', TotalDescuento);
    var PrecioUnitario = $(this).parents("tr").find("td")[3].innerHTML;
    console.log('PrecioUnitario', PrecioUnitario);
    var Cantidad = $(this).parents("tr").find("td")[2].innerHTML;
    console.log('Cantidad', Cantidad);
    var table = $('#tblDetalleFactura').DataTable();
    table.row($(this).parents('tr'))
                .remove()
                .draw();

    $.ajax({
        url: "/Factura/RemoveFacturaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ FacturaDetalleC: FacturaDetalle }),
    })
    .done(function (data) {
    });



    ////Subtotal
    //var Cantidad = $(this).parents("tr").find("td")[2].innerHTML;
    //var Precio = $(this).parents("tr").find("td")[3].innerHTML;
    //var SubtotalProducto = Cantidad * Precio;
    //var subtotal = parseFloat(document.getElementById("Subtotal").innerHTML);
    //document.getElementById("Subtotal").innerHTML = parseFloat(subtotal) - parseFloat(SubtotalProducto);

    ////Impuesto
    //var impuesto = $(this).parents("tr").find("td")[4].innerHTML;
    //var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
    //var porcentaje = parseFloat(impuesto.replace(',', '.') / 100);
    //var impuestos = (SubtotalProducto * porcentaje);
    //document.getElementById("isv").innerHTML = parseFloat(impuestotal) - parseFloat(impuestos);

    ////GranTotal
    //var TotalEncabezado = document.getElementById("total").innerHTML = (parseFloat(subtotal) - parseFloat(SubtotalProducto)) + (parseFloat(impuestotal) - parseFloat(impuestos));
    //$("#TotalProductoEncabezado").val(TotalEncabezado);

    

    ////$(this).closest('tr').remove();
    //idItem = $(this).closest('tr').data('id');
    //var FacturaDetalle = {
    //    factd_Id: idItem,
    //};

    
});


