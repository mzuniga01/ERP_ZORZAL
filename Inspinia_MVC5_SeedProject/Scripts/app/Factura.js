$(document).ready(function () {
    var caja = $("#cja_Id").val();
    if (caja == '') {
        $('#alertaCaja').show();
    }
    $('#prod_CodigoBarras').focus();
    $('#Alcredito').hide();
    $('#factd_Cantidad').prop('disabled', true);
    $('#tblDetalleFactura').DataTable({
        "searching": false,
        "lengthChange": false,
        "bPaginate": false,
        "bInfo": false,
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

//Autorizar Descuento General
function ValidarAutorizacionGeneral() {
    var User = $("#Username").val();
    var Password = $("#txtPassword").val();
    $.ajax({
        url: "/Factura/AutorizarDescuento",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ User: User, Password: Password }),
    })
    .done(function (data) {
        console.log()
        if (data == true) {

            var Porcentaje = $("#PorcentajeDescuento").val();
            $("#factd_PorcentajeDescuento").val(Porcentaje);
            $('#Descuento').val(Porcentaje);
            $('#prod_CodigoBarras').focus();
            $('#AutorizarDescuentoGeneral').modal('hide');
            document.getElementById("guardardescuentoterceraedad").disabled = true;
            document.getElementById("guardarAutorizarDescuentoDetalle").disabled = true;
        }
        else {

            valido = document.getElementById('mensajerror');
            valido.innerText = "Usuario o contraseña incorrectos";
        }
    });
}
//Autorizar Descuento Detalle
function ValidarAutorizacionDetalle() {
    var User = $("#UsernameDetalle").val();
    var Password = $("#txtPasswordDetalle").val();
    $.ajax({
        url: "/Factura/AutorizarDescuentoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ User: User, Password: Password }),
    })
    .done(function (data) {
        console.log()
        if (data == true) {
            var Porcentaje = $("#PorcentajeDescuentoDetalle").val();
            var PorcentajeAnt = $("#factd_PorcentajeDescuento").val();
            var PorcentajeTotal = parseFloat(PorcentajeAnt) + parseFloat(Porcentaje);
            $("#factd_PorcentajeDescuento").val(PorcentajeTotal);
            Cantidad = $('#factd_Cantidad').val();
            data_precio = $('#factd_PrecioUnitario').val();
            data_impuesto = $('#factd_Impuesto').val();
            descuento = $('#factd_PorcentajeDescuento').val();
            PrecioSubtotal = parseFloat(Cantidad) * parseFloat(data_precio);
            $('#SubtotalProducto').val((PrecioSubtotal).toFixed(2));
            MontoDescuento = (PrecioSubtotal * (parseFloat(descuento) / 100));
            PrecioSubtotal = PrecioSubtotal - MontoDescuento;
            Impuesto = ((PrecioSubtotal) * (parseFloat(data_impuesto) / 100));
            $('#factd_MontoDescuento').val(MontoDescuento.toFixed(2));
            $('#Impuesto').val(Impuesto.toFixed(2));
            total = parseFloat((PrecioSubtotal) + Impuesto);
            $('#TotalProducto').val((total).toFixed(2));
            $('#AutorizarDescuentoDetalle').modal('hide');
            document.getElementById("guardardescuentoterceraedad").disabled = true;
            document.getElementById("guardarDescuentoGeneral").disabled = true;
        }
        else {
            valido = document.getElementById('mensajerrorDetalle');
            valido.innerText = "Usuario o contraseña incorrectos";
        }
    });
}
//Tercera Edad
$('#AgregarTerceraEdad').click(function () {
    var IdentidadTE = $('#fact_IdentidadTE').val();
    var Nombre = $('#fact_NombresTE').val();
    var FechaNacimiento = $('#fact_FechaNacimientoTE').val();

    if (IdentidadTE == '') {
        $('#ErrorIdentidadTECreate').text('');
        $('#ErrorNombreCreate').text('');
        $('#ErrorFechaNacimientoCreate').text('');
        $('#validationfact_IdentidadTECreate').after('<ul id="ErrorIdentidadTECreate" class="validation-summary-errors text-danger">Campo requerido</ul>');
    }
    else if (Nombre == '') {
        $('#ErrorIdentidadTECreate').text('');
        $('#ErrorNombreCreate').text('');
        $('#ErrorFechaNacimientoCreate').text('');
        $('#validationNombreTECreate').after('<ul id="ErrorNombreCreate" class="validation-summary-errors text-danger">Campo requerido</ul>');
    }
    else if (FechaNacimiento == '') {
        $('#ErrorIdentidadTECreate').text('');
        $('#ErrorNombreCreate').text('');
        $('#ErrorFechaNacimientoCreate').text('');
        $('#validationFechaNacimientoTECreate').after('<ul id="ErrorFechaNacimientoCreate" class="validation-summary-errors text-danger">Campo requerido</ul>');
    }
    else {
        var TerceraEdad = GetTerceraEdad();
        $.ajax({
            url: "/Factura/SaveTerceraEdad",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ TerceraEdadC: TerceraEdad }),
        })
        .done(function (data) {
            $('#ErrorIdentidadTECreate').text('');
            $('#ErrorNombreCreate').text('');
            $('#ErrorFechaNacimientoCreate').text('');
            //Input
            $('#fact_IdentidadTE').val();
            $('#fact_NombresTE').val();
            $('#fact_FechaNacimientoTE').val();
            $('#DescTerceraEdad').modal('hide');
            $("#MostrarTerceraEdad").prop("checked", true);
            $("#fact_AutorizarDescuento").prop("checked", true);
            $('#Cred2').show();
            GetParametro();
            function GetParametro() {
                $.ajax({
                    url: "/Factura/GetParametro",
                    method: "POST",
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({}),
                })
                .done(function (data) {
                    $.each(data, function (key, val) {
                        $('#fact_PorcentajeDescuento').val(val.par_PorcentajeDescuentoTE);
                        $('#Descuento').val(val.par_PorcentajeDescuentoTE);
                        //$('#factd_PorcentajeDescuento').val(val.par_PorcentajeDescuentoTE);
                        document.getElementById("guardarDescuentoGeneral").disabled = true;
                        document.getElementById("guardarAutorizarDescuentoDetalle").disabled = true;
                    });

                    console.log(data)
                });
            }

        });
    }
});
function GetTerceraEdad() {

    var TerceraEdad = {
        fact_IdentidadTE: $('#fact_IdentidadTE').val(),
        fact_NombresTE: $('#fact_NombresTE').val(),
        fact_FechaNacimientoTE: $('#fact_FechaNacimientoTE').val(),
    }
    return TerceraEdad
};

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
        //document.getElementById("MostrarTerceraEdad").disabled = false;
        //document.getElementById("fact_AutorizarDescuento").disabled = false;
        $('#Cred2').hide();
    }
});

//Modal Producto
$("#Producto").click(function () {
    ListaProductos();
});

function ListaProductos() {
    url = "/Factura/ListaProductos";
    var table = $('#tbProductoFactura').dataTable({
        destroy: true,
        resposive: true,
    //    ajax: {
    //        method: "POST",
    //        url: url,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: 'json',
    //        "dataSrc": ""
    //    },
    //    "columns": [
    //        { "data": "prod_Codigo" },
    //        { "data": "prod_Descripcion" },
    //        { "data": "prod_CodigoBarras" },
    //        { "defaultContent": "<button class='btn btn-primary btn-xs'  id='seleccionar' data-dismiss='modal'>Seleccionar</button>" }
    //    ],
    //    "searching": false,
    //    "lengthChange": false,
    //    "oLanguage": {
    //        "oPaginate": {
    //            "sNext": "Siguiente",
    //            "sPrevious": "Anterior",
    //        },
    //        "sProcessing": "Procesando...",
    //        "sLengthMenu": "Mostrar _MENU_ registros",
    //        "sZeroRecords": "No se encontraron resultados",
    //        "sEmptyTable": "Ningún dato disponible en esta tabla",
    //        "sEmptyTable": "No hay registros",
    //        "sInfoEmpty": "Mostrando 0 de 0 Entradas",
    //        "sSearch": "Buscar",
    //        "sInfo": "Mostrando _START_ a _END_ Entradas",
    //        "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
    //    }
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
    $(document).on("click", "#tbProductoFactura tbody tr td button#seleccionarProducto", function () {
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
    var DescFactura = $('#Descuento').val();
    var PorcentajeDescuentoDet = $('#factd_PorcentajeDescuento').val();
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
                        descuento = parseFloat(val.DESCUENTOCAJERO) + parseFloat(PorcentajeDescuentoDet);
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
                            $('#SubtotalProducto').val((PrecioSubtotal).toFixed(2));
                            MontoDescuento = (PrecioSubtotal * (parseFloat(descuento) / 100));
                            $('#factd_MontoDescuento').val(MontoDescuento.toFixed(2));
                            Impuesto = ((PrecioSubtotal - MontoDescuento) * (parseFloat(data_impuesto) / 100));
                            $('#Impuesto').val(Impuesto.toFixed(2));
                            total = parseFloat((PrecioSubtotal - MontoDescuento) + Impuesto);
                            $('#TotalProducto').val((total).toFixed(2));
                            $('#msjSinPrecio').hide();
                            $('#alerta').hide();
                            $('#SinCantidad').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            $('#CantidadDistinta').hide();
                            GetCantidad(1);
                        }
                    }
                })
            }
        }
    });
}

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
    GetCantidad(1);//Validar que cuando no haya cantidad no permita ingresar el detalle.
    
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

function GetCantidad(Cantidad1) {
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
                        if (CANTIDAD < Cantidad1) {
                            $('#SinCantidad').show();
                            $('#msjSinPrecio').hide();
                            $('#alerta').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            $('#CantidadDistinta').hide();
                            $('#factd_Cantidad').prop('disabled', false);
                        }
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
                            $('#SubtotalProducto').val((PrecioSubtotal).toFixed(2));
                            MontoDescuento = (PrecioSubtotal * (parseFloat(descuento) / 100));
                            PrecioSubtotal = PrecioSubtotal - MontoDescuento;
                            Impuesto = ((PrecioSubtotal) * (parseFloat(data_impuesto) / 100));
                            $('#factd_MontoDescuento').val(MontoDescuento.toFixed(2));
                            $('#Impuesto').val(Impuesto.toFixed(2));
                            total = parseFloat((PrecioSubtotal) + Impuesto);
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
    console.log('FacturaDetalle',FacturaDetalle);
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
                    SubTotal = SubTotal - MontoDescuento;
                    var Impuesto = ((SubTotal) * (parseFloat(data_impuesto) / 100));
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
                    CalculoDetalle();
                    LimpiarControles();
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
            CalculoDetalle();
            LimpiarControles();
        }
    })
}

function GetFacturaDetalle() {

    var FacturaDetalle = {
        prod_Codigo:$('#prod_Codigo').val(),
        factd_PorcentajeDescuento: parseFloat($('#factd_PorcentajeDescuento').val()),
        factd_MontoDescuento: parseFloat($('#factd_MontoDescuento').val()),
        tbProducto_prod_Descripcion: $('#tbProducto_prod_Descripcion').val(),
        factd_Cantidad: parseFloat($('#factd_Cantidad').val()),
        SubtotalProducto: parseFloat($('#SubtotalProducto').val()),
        factd_PrecioUnitario: parseFloat($('#factd_PrecioUnitario').val()),
        factd_Impuesto: parseFloat($('#factd_Impuesto').val()),
        TotalProducto: $('#TotalProducto').val()
    }
    return FacturaDetalle
};

function CalculoDetalle() {
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

    //Descuento 
    var Descuento = $('#factd_MontoDescuento').val();
    var TotalDescuento = parseFloat(document.getElementById("TotalDescuento").innerHTML);
    if (document.getElementById("TotalDescuento").innerHTML == '') {
        document.getElementById("TotalDescuento").innerHTML = parseFloat(Descuento).toFixed(2);

    }
    else {
        document.getElementById("TotalDescuento").innerHTML = (parseFloat(TotalDescuento) + parseFloat(Descuento)).toFixed(2);
    }
    //Impuesto
    var Cantidad = $('#factd_Cantidad').val();
    var Precio = $('#factd_PrecioUnitario').val();
    var impuesto = parseFloat(document.getElementById("factd_Impuesto").value.replace(',', '.'));
    var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
    var porcentaje = parseFloat(impuesto / 100);
    var impuestos = ((Cantidad * Precio) - Descuento) * porcentaje;
    if (document.getElementById("isv").innerHTML == '') {
        impuesto = document.getElementById("factd_Impuesto").value;
        document.getElementById("isv").innerHTML = (parseFloat(impuestos)).toFixed(2);
    }
    else {
        document.getElementById("isv").innerHTML = (parseFloat(impuestotal) + parseFloat(impuestos)).toFixed(2);
    }

    //Grantotal
    if (document.getElementById("total").innerHTML == '') {
        var TotalEncabezado = document.getElementById("total").innerHTML = (parseFloat(totalProducto) + parseFloat(impuestos) - parseFloat(Descuento)).toFixed(2);
        $("#TotalProductoEncabezado").val(TotalEncabezado);
    }
    else {
        var TotalEncabezado = document.getElementById("total").innerHTML = (parseFloat(subtotal) + parseFloat(totalProducto) + parseFloat(impuestotal) + parseFloat(impuestos) - parseFloat(TotalDescuento) - parseFloat(Descuento)).toFixed(2);
        $("#TotalProductoEncabezado").val(TotalEncabezado);
    }
};

function LimpiarControles()
{
    $('#prod_CodigoBarras').val('');
    $('#prod_Codigo').val('');
    $('#tbProducto_prod_Descripcion').val('');
    $('#factd_Impuesto').val((0).toFixed(2));
    $('#factd_PrecioUnitario').val((0).toFixed(2));
    $('#factd_Cantidad').val((0).toFixed(2));
    var Descuento = $('#Descuento').val();
    if (Descuento == '') {
        Descuento = 0.00;
    }
    $('#factd_PorcentajeDescuento').val(parseFloat(Descuento).toFixed(2));
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
    var DescFactura = $('#Descuento').val();
    var PorcentajeDescuentoDet = $('#factd_PorcentajeDescuento').val();
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
                        descuento = parseFloat(val.DESCUENTOCAJERO) + parseFloat(PorcentajeDescuentoDet);
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
                            $('#SubtotalProducto').val((PrecioSubtotal).toFixed(2));
                            MontoDescuento = (PrecioSubtotal * (parseFloat(descuento) / 100));
                            PrecioSubtotal = PrecioSubtotal - MontoDescuento;
                            Impuesto = ((PrecioSubtotal) * (parseFloat(data_impuesto) / 100));
                            $('#factd_MontoDescuento').val(MontoDescuento.toFixed(2));
                            $('#Impuesto').val(Impuesto.toFixed(2));
                            total = parseFloat((PrecioSubtotal) + Impuesto);
                            $('#TotalProducto').val((total).toFixed(2));
                            $('#msjSinPrecio').hide();
                            $('#alerta').hide();
                            $('#SinCantidad').hide();
                            $('#CantidadNoDisponible').hide();
                            $('#NoExiste').hide();
                            GetCantidad(1);
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

    var Cantidad = $(this).parents("tr").find("td")[2].innerHTML;
    var PrecioUni = $(this).parents("tr").find("td")[3].innerHTML;

    ////Descuento
    var Descuento = $(this).parents("tr").find("td")[5].innerHTML;
    Descuento = (parseFloat(Cantidad) * parseFloat(PrecioUni)) * (parseFloat(Descuento) / 100)
    var TotalDescuento = parseFloat(document.getElementById("TotalDescuento").innerHTML);
    document.getElementById("TotalDescuento").innerHTML = (parseFloat(TotalDescuento) - parseFloat(Descuento)).toFixed(2);

    //Impuesto
    var impuesto = $(this).parents("tr").find("td")[4].innerHTML;
    var porcentaje = parseFloat(impuesto.replace(',', '.') / 100);
    ImpuestoT = ((parseFloat(Cantidad) * parseFloat(PrecioUni)-Descuento) * (parseFloat(porcentaje))).toFixed(2)
    var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
    document.getElementById("isv").innerHTML = (parseFloat(impuestotal) - parseFloat(ImpuestoT)).toFixed(2);

    //Subtotal
    var SubtotalProducto = Cantidad * PrecioUni;
    var subtotal = parseFloat(document.getElementById("Subtotal").innerHTML);
    document.getElementById("Subtotal").innerHTML = (parseFloat(subtotal) - parseFloat(SubtotalProducto)).toFixed(2);

    //GranTotal
    var Total = parseFloat(document.getElementById("total").innerHTML);
    document.getElementById("total").innerHTML = (parseFloat(Total) - parseFloat(SubtotalProducto)+parseFloat(Descuento)-parseFloat(ImpuestoT)).toFixed(2);

    var FacturaDetalle = {
        prod_Codigo: $(this).parents("tr").find("td")[0].innerHTML,
    };
    ////Descuento
    var PorcentajeDescuento = $(this).parents("tr").find("td")[5].innerHTML;
    var TotalDescuento = $(this).parents("tr").find("td")[6].innerHTML;
    var PrecioUnitario = $(this).parents("tr").find("td")[3].innerHTML;
    var Cantidad = $(this).parents("tr").find("td")[2].innerHTML;
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
});



