﻿var contadorinit = 0;
var contador = 0;
var CantidadTable_VAR = 0
//var CantidadExit = 0.00;
//var CantidadAceptada = 0;
//var CantidadMinima = 0;

//$('#EditSalidaDetalle').on('hidden.bs.modal', function () {
//    console.log("Si")
//    $("#BtnsubmitEdit").attr("disabled", "disabled")
//    $('#CantidaExistenteProd').text('');
//    $(this).off('hidden.bs.modal');
//});

function RejectUnload() {
    if (contador != contadorinit) {
        $(window).on("beforeunload", function (e) {
            console.log(e);
        return "Hay pendientes";
  });
    }
    else {
         $(window).off("beforeunload");
    }

}
//$(window).on("beforeunload", function (e) {
//    //table = $('#tblSalidaDetalle').DataTable();
//    //var CantidadTable_VARCHECHK = table.rows().data().length;
//    //console.log(CantidadTable_VARCHECHK)
    
//  });
$(document).ready(function () {
     table = $('#tblSalidaDetalle').DataTable();
    contadorinit = table.rows().data().length;
    contador = contadorinit;
    console.log(window.location.pathname)
    //console.log(CantidadTable_VAR)

    // $(window).on("beforeunload", function(e) {
    //return "Hay pendientes";
    //});

    ////var table = $('#tblSalidaDetalle').DataTable();

    ////var table = $().dataTable();
    //if ( $.fn.dataTable.isDataTable( '#tblSalidaDetalle' ) ) {
    //   )
    //}
    //else {
    //    table = $('#tblSalidaDetalle').DataTable( {
    //        paging: false
    //    } );
    //}
    //// table = $('#tblSalidaDetalle').DataTable();
    ////var CantidadTable_VAR = table.rows().data().length;
    ////console.log(CantidadTable_VAR)

})



function FaturaExist() {
    $.ajax({
        url: "/Salida/FacturaExist",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ fact_Codigo: $('#fact_Codigo').val() }),
    })
        .done(function (data) {
            console.log(data);
            if (data.Message.startsWith("Factura Disponible")) {
                $('#FacturaCodigoError').text('');
                $('#CodigoError').text('');
                $('#validationFactura').after('<ul id="CodigoError" class="Factura" >' + data.Message + '</ul>');
            }
            else {
                if (data.Message.startsWith("Ya existe una Salida con ese Codigo de Factura")) {
                    var sal_Id = data.SalidaFact;
                    $('#FacturaCodigoError').text('');
                    $('#CodigoError').text('');
                    $('#validationFactura').after('<a id="FacturaCodigoError" href="/Salida/Edit/' + sal_Id + '">Haga Click aqui si desea ver la Salida de Esta Factura</a>');
                }
                else {
                    $('#FacturaCodigoError').text('');
                    $('#CodigoError').text('');
                    $('#validationFactura').after('<ul id="CodigoError" class="validation-summary-errors text-danger">' + data.Message + '</ul>');
                }

            }
        })
}
$(document).ready(function () {
    TipodeSalida()
});

var normalize = (function () {
    var from = "ÃÀÁÄÂÈÉËÊÌÍÏÎÒÓÖÔÙÚÜÛãàáäâèéëêìíïîòóöôùúüûÑñÇç",
        to = "AAAAAEEEEIIIIOOOOUUUUaaaaaeeeeiiiioooouuuunncc",
        mapping = {};

    for (var i = 0, j = from.length; i < j; i++)
        mapping[from.charAt(i)] = to.charAt(i);

    return function (str) {
        var ret = [];
        for (var i = 0, j = str.length; i < j; i++) {
            var c = str.charAt(i);
            if (mapping.hasOwnProperty(str.charAt(i)))
                ret.push(mapping[c]);
            else
                ret.push(c);
        }
        return ret.join('');
    }

})();
function TipodeSalida() {

    $('#NombreError').text("");
    var txtTipoSalida = document.getElementById("tsal_Id");
    var lblTipoSalida = txtTipoSalida.options[txtTipoSalida.selectedIndex].text;
    var valTipoSalida = normalize(lblTipoSalida.toUpperCase());
    var TipoSal = $("#tsal_Id").val()
    if (valTipoSalida == "PRESTAMO") {
        //var elemento = document.getElementById("Prestamo");
        //var reelemento = document.getElementById("Prestamo");
        $('#fact_Codigo').val('***-***-**-********');
        $('#tbFactura_fact_Codigo').val('***-***-**-********');
        //elemento.classList.remove('toggle-content');
        document.getElementById("tdev_Id").value = "0";

        //$('#sal_RazonDevolucion').val('*****'); 
        ///////////////////////////////////////
        //elemento.classList.add('toggle-content.is-visible')
        $("#Prestamo").css("display", "block");
        ///////////////////////////////////////
        //reelemento.classList.add('toggle-content')
        $("#VentaoDevolucion").css("display", "none");
        //BodegaDestino()
    }
    else {
        $("#Prestamo").css("display", "none");
        $("#Devolucion").css("display", "none");
        FaturaExist()
        if (valTipoSalida == "VENTA") {
            var fact_Codigo = $('#fact_Codigo').val();
            if (fact_Codigo == '***-***-**-********') {
                $('#fact_Codigo').val('')
            }
            document.getElementById("tdev_Id").value = "0";
            ///////////////////////////////////////
            $("#sal_BodDestino").empty();
            $("#VentaoDevolucion").css("display", "block");
            $("#TitleVenta").css("display", "block");
            ///////////////////////////////////////
            $('#div_sal_RazonDevolucion').css("display", "none");
            $("#TitleDevolucion").css("display", "none");
            ///////////////////////////////////////
            $("#Venta").css("display", "none");
        }
        else {
            $("#Venta").css("display", "none");
            $("#Prestamo").css("display", "none");

            if (valTipoSalida == "DEVOLUCION") {
                var fact_Codigo = $('#fact_Codigo').val();
                if (fact_Codigo == '***-***-**-********') {
                    $('#fact_Codigo').val('')
                }
                $('#sal_RazonDevolucion').val('');
                $("#sal_BodDestino").empty();
                $("#VentaoDevolucion").css("display", "block");
                $("#TitleDevolucion").css("display", "block");
                $('#fact_Codigo').css("display", "block");
                $('#div_sal_RazonDevolucion').css("display", "block");
                ///////////////////////////////////////
                $("#TitleVenta").css("display", "none");
            }
            else {
                $("#VentaoDevolucion").css("display", "none");
                ///////////////////////////////////////
                $("#Prestamo").css("display", "none");
            }
        }
    }
}

function validateMyFormEdit() {
    //Bodega
    //var Bod = document.getElementById("bod_Id");
    //var Bodega = Bod.options[Bod.selectedIndex].text;

    /////////
    //TipoSalida
    var txtTipoSalida = document.getElementById("tsal_Id");
    var lblTipoSalida = txtTipoSalida.options[txtTipoSalida.selectedIndex].text;
    var TipoSalida = normalize(lblTipoSalida.toUpperCase());
    /////////
    var Fecha = $('#sal_FechaElaboracion').val();

    var BodDestino = $("#sal_BodDestino").val()
    var validacionFactura = $("#CodigoError").text()
    FacturaCodigoError = $("#FacturaCodigoError").text()

    var currentRow = $("#tblSalidaDetalle tbody tr");
    var Tabla = currentRow.find("td:eq(0)").text();
    if (TipoSalida.startsWith("SELECCIONAR")) {
        $('#NombreError').text('');
        $('#validationtsal_Id').after('<ul id="NombreError" class="validation-summary-errors text-danger">Seleccione un tipo de salida</ul>');
        vTipoSalida = false;
    }
    else {
        $('#NombreError').text('');
        vTipoSalida = true;
         if (TipoSalida == "PRESTAMO" || TipoSalida == "PRESTAMOS" || TipoSalida == "TRANSLADOS") {
            if (BodDestino == 'Seleccione una Bodega de Destino' || BodDestino == "") {
                $('#sal_BodDestinoError').text('');
                $('#validationsal_BodDestino').after('<ul id="sal_BodDestinoError" class="validation-summary-errors text-danger">Seleccione una bodega de Destino</ul>');
                vBodDestino = false;
                $('#sal_BodDestino').focus()

                vFactura = true;
                vDevolucion = true;
            }
            else {
                $('#sal_BodDestinoError').text('');

                vBodDestino = true;

                vFactura = true;
                vDevolucion = true;
            }
        }
        else {
             if (TipoSalida == "VENTA" || TipoSalida == "VENTAS") {

                if (validacionFactura != "" || FacturaCodigoError != '') {
                    vFactura = false;
                    $('#fact_Codigo').focus()

                    vBodDestino = true;
                    vDevolucion = true;
                }
                else {
                    $('#validationFactura').text('');
                    vFactura = true;

                    vBodDestino = true;
                    vDevolucion = true;
                }
            }
            else {
                if (TipoSalida == "DEVOLUCION" || TipoSalida == "DEVOLUCIONES") {
                    //TipoSalida>Devolucion
                    var tdev = document.getElementById("tdev_Id");
                    var TipoSalida_tdev = tdev.options[tdev.selectedIndex].text;
                    /////////
                    if (validacionFactura != "" || FacturaCodigoError != '') {
                        $('#validationFactura').text('');
                        vDFactura = false;
                        $('#fact_Codigo').focus()
                    }
                    else {
                        vDFactura = true;
                    }
                    if (TipoSalida_tdev.startsWith("Seleccionar")) {
                        $('#sal_RazonDevolucionError').text('');
                        $('#validationsal_RazonDevolucion').after('<ul id="sal_RazonDevolucionError" class="validation-summary-errors text-danger">Campo Requerido</ul>');
                        $('#tdev_Id').focus()
                        vRazonDevolucion = false;
                    }
                    else {
                        $('#sal_RazonDevolucionError').text('');
                        vRazonDevolucion = true;
                    }
                    if (!vDFactura || !vRazonDevolucion) {
                        vDevolucion = false;

                        vFactura = true;
                        vBodDestino = true;
                    }
                    else {
                        vDevolucion = true;

                        vFactura = true;
                        vBodDestino = true;
                    }
                }
            }
        }
    }
    if (Tabla == "No hay registros") {
        vSalidaDetalle = false;
        //window.location.hash = '#alert_message';
        //$('#').focus();
        $("#alert_message").css("display", "block");
        $('#alert_message').html('<div class="alert alert-danger alert-dismissible" role="alert">'
                                            + '<button type="button" class="close" data-dismiss="alert" aria-label="Close">'
                                                    + '<span aria-hidden="true">&times;</span>'
                                            + '</button>'
                                        + '<strong>Detalle Vacio</strong>'
                                  + '</div>');
        //document.getElementById('alert_message').scrollIntoView()
        window.location.hash = '#alert_message';


        //setInterval(function () {
        //    $('#alert_message').html('');
        //    $('#prod_CodigoBarras').focus()
        //}, 5000);
    }
    else {
        vSalidaDetalle = true;
    }
    if (!vTipoSalida || !vBodDestino || !vFactura || !vDevolucion || !vSalidaDetalle) {
        return false;
    }
    else {
        return true
    }

}

$(document).ready(function () {
    $('#tblBusquedaGenerica').DataTable(
        {
            "searching": true,
            "lengthChange": true,
            "responsive": true,
            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",

            }
        });
});


$(document).ready(function () {
    $('#tblBusquedaGenericaBox').DataTable(
        {
            "searching": true,
            "lengthChange": true,
            "responsive": true,
            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            }
        });
});


function EditSalidaDetalles(sald_Id) {
    $('#CantidaExistenteProd').text('');
    $("#BtnsubmitEdit").attr("disabled", "disabled")
    var bod_Id = $('#bod_Id').val();
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
                    ProductoCantidad(bod_Id, item.prod_Codigo).done(function (data) {
                        if (data.CantidadAceptada < data.CantidadMinima) {
                            //$("#BtnsubmitEdit").attr("disabled", "disabled")
                            $('#CantidaExistenteProd').text('');
                            $('#CantidaExistenteModal').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Sin existencia de este producto</ul>');
                        }
                        else {
                            $("#BtnsubmitEdit").removeAttr("disabled");
                            $('#CantidaExistenteProd').text('');
                            $('#CantidaExistenteModal').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Hay ' + data.CantidadAceptada + ' en Bodega</ul>');
                        }
                    })
                   
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
    else if (Cantidad < '0.01') {
        $('#NombreError').text('');
        $('#validationsald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad muy pequeña</ul>');
    }
    else {
        var sal_id = $('#sald_Id').val();
        var data = $("#SubmitForm").serializeArray();
        $.ajax({
            type: "Post",
            url: "/Salida/EditSalidaDetalle",
            data: data,
            success: function (result) {
                if (result.startsWith('-1'))
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
    var table = $('#tblSalidaDetalle').DataTable();
    var counter = 0;
    var bod_Id = $('#bod_Id').val();
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Unidad_Medida = $('#pscat_Id').val();
    var Cantidad = $('#sald_Cantidad').val();
    var data_producto = $("#prod_Codigo").val();
    var CodBarra_Producto = $('#prod_CodigoBarras').val();
    $('#sald_CantidadError').text();
    var vCodBarra_Producto = true;
    var vCantidad = true;

    if (CodBarra_Producto == "") {
        $('#ValidationCodigoBarrasCreateError').text('');
        $('#validationprod_CodigoBarras').after('<ul id="ValidationCodigoBarrasCreateError" class="validation-summary-errors text-danger">Campo de Barras Requerido</ul>');
        vCodBarra_Producto = false;
    }
    if (Cantidad == "") {
        $('#MessageError').text('');
        $('#sald_CantidadExedError').text('');
        $('#sald_CantidadError').text('');
        $('#sald_Cantidad').after('<ul id="sald_CantidadError" class="validation-summary-errors text-danger">Cantidad Requerida</ul>');
        vCantidad = false;
    }
    if (!vCantidad || !vCodBarra_Producto) {
        return false;
    }
    else {
        $('#ValidationCodigoBarrasCreateError').text('');
        ProductoCantidad(bod_Id, Cod_Producto).done(function (data) {
            var CantidadAceptada = data.CantidadAceptada
            var CantidadMinima = data.CantidadMinima
            if ($('#tblSalidaDetalle >tbody >tr').length > 0) {
                $('#tblSalidaDetalle >tbody >tr').each(function () {
                    var prod_CodigoTabla = $(this).find("td:eq(0)").text()
                    if (Cod_Producto == prod_CodigoTabla) {
                        var CantidadTabla = $(this).find("td:eq(7)").text();
                        CantidadExit = parseFloat(CantidadAceptada) - parseFloat(CantidadTabla);
                    }
                    else {
                        CantidadExit = CantidadAceptada
                    }
                })
            }
            else {
                CantidadExit = CantidadAceptada
            }
            console.log(CantidadExit)
            if (Producto == '') {
                $('#MessageError').text('');
                $('#CodigoError').text('');
                $('#ValidationCodigoCreateError').text('');
                $('#ValidationCodigoCreate').after('<ul id="ValidationCodigoCreateError" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
            }
            else if (Cantidad == '' || Cantidad < 0.25) {
                $('#MessageError').text('');
                $('#sald_CantidadExedError').text('');
                $('#sald_CantidadError').text('');
                $('#sald_Cantidad').after('<ul id="sald_CantidadError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
            }
            else if (Cantidad > CantidadExit) {
                $('#MessageError').text('');
                $('#sald_CantidadError').text('');
                $('#sald_CantidadExedError').text('');
                $('#sald_Cantidad').after('<ul id="sald_CantidadExedError" class="validation-summary-errors text-danger">Cantidad Superada</ul>');
            }
            else {
                $('#ValidationCodigoCreateError').text('');
                $('#sald_CantidadError').text('');
                $('#sald_CantidadExedError').text('');
                $('#CantidaExistenteProd').text('');
                var tbSalidaDetalle = GetSalidaDetalle();
                $.ajax({
                    url: "/Salida/SaveSalidaDetalle",
                    method: "POST",
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle, data_producto: data_producto }),
                }).done(function (datos) {
                    $("#prod_CodigoBarras").removeAttr("readonly");
                    if (datos == data_producto) {
                        console.log('Repetido');
                        var cantfisica_nueva = $('#sald_Cantidad').val();
                        $("#tblSalidaDetalle td").each(function () {
                            var prueba = $(this).text()
                            if (prueba == data_producto) {
                                table.row($(this).parents('tr')).remove().draw();
                                var idcontador = $(this).closest('tr').data('id');
                                var cantfisica_anterior = $(this).closest("tr").find("td:eq(7)").text();
                                var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);
                                contador = contador + 1

                                table.row.add([
                                    $('#prod_Codigo').val(),
                                    $('#prod_Descripcion').val(),
                                    $('#prod_Marca').val(),
                                    $('#prod_Modelo').val(),
                                    $('#prod_Talla').val(),
                                    $('#pcat_Id').val(),
                                    $('#uni_Id').val(),
                                    sumacantidades,
                                    '<button id = "removeSalidaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Quitar</button>',
                                    contador
                                ]).draw(false);
                            }
                        });
                    } else {
                        contador = contador + 1

                        table.row.add([
                            $('#prod_Codigo').val(),
                            $('#prod_Descripcion').val(),
                            $('#prod_Marca').val(),
                            $('#prod_Modelo').val(),
                            $('#prod_Talla').val(),
                            $('#pcat_Id').val(),
                            $('#uni_Id').val(),
                            $('#sald_Cantidad').val(),
                            '<button id = "removeSalidaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Quitar</button>',
                            contador
                        ]).draw(false);
                    }
                    }).done(function (data) {
                        RejectUnload()
                        console.log("Contador: "+contador)
                    $('#prod_Codigo').val('');
                    $('#prod_Descripcion').val('');
                    $('#pscat_Id').val('');
                    $('#uni_Id').val('');
                    $('#pcat_Id').val('');
                    $("#prod_CodigoBarras").val('');
                    $('#sald_Cantidad').val('');
                    $('#Error_Barras').text('');
                    $('#NombreError').text('');
                    $('#sald_CantidadError').text('');
                    $('#CantidaExistenteProd').text('');
                    $('#prod_CodigoBarras').focus();
                    console.log('Hola');

                });
            }
        });
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

function ValidacionCantidad() {
    var bod_Id = $('#bod_Id').val();
    var prod_Codigo = $('#prod_Codigo_SD').val();
    console.log(prod_Codigo);
    ProductoCantidad(bod_Id, prod_Codigo);
}

//sald_Cantidad_SD
function ProductoCantidad(bod_Id, prod_Codigo) {
    $("#prod_CodigoBarras").val();
    var cod_Barras = $("#prod_CodigoBarras").val();
    return $.ajax({
        url: "/Salida/Cantidad",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            bod_Id: bod_Id,
            prod_Codigo: prod_Codigo
        }),
        success: function (data) {
           var CantidadAceptada = data.CantidadAceptada
           var CantidadMinima = data.CantidadMinima
            
            //if (CantidadAceptada < CantidadMinima) {
            //    $('#CantidaExistenteProd').text('');
            //    $('#CantidaExistenteModal').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Sin existencia de este producto</ul>');
            //}
            //else {
            //    $('#CantidaExistenteProd').text('');
            //    $('#CantidaExistenteModal').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Hay ' + CantidadAceptada + ' en Bodega</ul>');
            //}
        }
    })
};



$(document).on("click", "#tblBusquedaGenerica tbody tr td button#seleccionar", function () {
    //var currentRow = $(this).closest("tr");
    //var prod_CodigoBarrasItem = currentRow.find("td:eq(8)").text();
    var prod_CodigoBarrasItem = this.value;
    var bod_Id = $('#bod_Id').val()
    $("#prod_CodigoBarras").attr("readonly", "true")
    //$('#CodigoError').text('')
    $('#sald_CantidadExedError').text('')

    console.log(prod_CodigoBarrasItem)
    Producto(bod_Id, prod_CodigoBarrasItem)
});

function Producto(bod_Id, prod_CodigoBarrasItem) {
    $("#prod_CodigoBarras").val();
    var cod_Barras = $("#prod_Codigo").val();
    var pro_CodBarras = $("#prod_CodigoBarras").val();
    $.ajax({
        url: "/Salida/GetProdCodBar",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            bod_Id: bod_Id,
            prod_CodigoBarras: prod_CodigoBarrasItem
        }),
    }).done(function (data) {
        if (data.length > 0) {
            $("#prod_CodigoBarras").attr("readonly", "true")
            $('#Error_Barras').text('');
            $('#prod_CodigoBarras').text('');
            $('#sald_Cantidad').val('');
            $.each(data, function (key, value) {
                $("#prod_CodigoBarras").val(value.prod_CodigoBarras);
                $('#prod_Codigo').val(value.prod_Codigo);
                $('#prod_Descripcion').val(value.prod_Descripcion);
                $("#uni_Id").val(value.uni_Descripcion);
                $("#pscat_Id").val(value.pscat_Descripcion);
                $("#pcat_Id").val(value.pcat_Nombre);
                $("#prod_Marca").val(value.prod_Marca);
                $("#prod_Modelo").val(value.prod_Modelo);
                $("#prod_Talla").val(value.prod_Talla);
                $("#prod_Color").val(value.prod_Color);
                ProductoCantidad(bod_Id, value.prod_Codigo);
                $('#sald_Cantidad').focus();
            })


            $("#ModalAgregarProducto").on('hidden.bs.modal', function () {
                $('#sald_Cantidad').focus();
            });
        }
        else {
            $('#Error_Barras').text('');
            $("#CantidaExistenteProd").text('');
            $('#validationprod_CodigoBarras').after('<ul id="Error_Barras" class="validation-summary-errors text-danger">Producto no existe</ul>');

            idItem = $(this).closest('tr').data('id');
            contentItem = $(this).closest('tr').data('content');
            uni_IdtItem = $(this).closest('tr').data('keyboard');
            psubctItem = $(this).closest('tr').data('container');
            pcatItem = $(this).closest('tr').data('pcat');
            prod_CodigoBarrasItem = $(this).closest('tr').data('cod_barras');
            $("#prod_Codigo").val(idItem);
            $("#prod_Descripcion").val(contentItem);
            $("#uni_Id").val(uni_IdtItem);
            $("#pscat_Id").val(psubctItem);
            $("#pcat_Id").val(pcatItem);
            $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
            $('#prod_CodigoBarras').focus();

        }
    });

    return false;
}

function ProductoCantidad(bod_Id, prod_Codigo) {
    $("#prod_CodigoBarras").val();
    $("#CantidaExistenteProd").text('');
    var cod_Barras = $("#prod_CodigoBarras").val();
    return $.ajax({
        url: "/Salida/Cantidad",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            bod_Id: bod_Id,
            prod_Codigo: prod_Codigo
        }),
        success: function (data) {
            var CantidadAceptada = data.CantidadAceptada
            var CantidadMinima = data.CantidadMinima
            var prod_CodigoCampo = $('#prod_Codigo').val();
            var currentRow = $("#tblSalidaDetalle tbody tr");
            //var DataTable = $("#tblSalidaDetalle >tbody >tr").DataTable();
            //$("#tblSalidaDetalle td").each(function () {
            var prod_CodigoTabla = currentRow.find("td:eq(0)").text().trim();
            var idcontador = $(this).closest('tr').data('id');
            var cantfisica_anterior = $(this).closest("tr").find("td:eq(7)").text();
            console.log(prod_CodigoTabla)
            if (prod_CodigoTabla !== "No hay registros") {
                currentRow.each(function () {
                    var prod_CodigoTablaeach = $(this).closest("tr").find("td:eq(0)").text();

                    if (prod_CodigoCampo == prod_CodigoTablaeach) {
                        var CantidadTabla = $(this).closest("tr").find("td:eq(7)").text();
                        var CantidatTotal = parseFloat(CantidadAceptada) + parseFloat(CantidadMinima)
                        var CantidadRestante = parseFloat(CantidadAceptada) - parseFloat(CantidadTabla)
                        if (CantidadTabla >= CantidadAceptada && CantidadRestante > 0) {
                            $("#CantidaExistenteProd").text('');
                            $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Minima Alcanzada solo hay en existencia: ' + CantidadRestante + ' de este producto</ul>');
                        }
                        else {
                            if (CantidadTabla < CantidadAceptada) {
                                CantidadAceptada = CantidadAceptada - CantidadTabla
                                $("#CantidaExistenteProd").text('');
                                $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Existente en Bodega: ' + CantidadAceptada + '</ul>');
                            }
                            else {
                                if (CantidadTabla == CantidadAceptada) {
                                    $("#CantidaExistenteProd").text('');
                                    $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Sin Existencias de este producto</ul>');
                                }
                            }
                        }
                    }
                    else {
                        $("#CantidaExistenteProd").text('');
                        $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Existente en Bodega: ' + CantidadAceptada + '</ul>');
                    }
                })
            }
            else {
                $("#CantidaExistenteProd").text('');
                Aceptada = CantidadAceptada
                if (CantidadAceptada == 0 && CantidadMinima > 0) {
                    $("#CantidaExistenteProd").text('');
                    $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Minima Alcanzada solo hay en existencia: ' + CantidadMinima + ' de este producto</ul>');
                }
                else {
                    $("#CantidaExistenteProd").text('');
                    $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Existente en Bodega: ' + Aceptada + '</ul>');
                }
            }
        }
    })
};






$(document).on("click", "#tblSalidaDetalle tbody tr td button#removeSalidaDetalle", function () {
    
    idItem = $(this).closest('tr').data('id');
    var vprod_Codigo = $(this).closest("tr").find("td:eq(0)").text();
    var tbSalidaDetalle = {
        prod_Codigo: vprod_Codigo,
        sald_UsuarioCrea: vprod_Codigo
    };
    var table = $('#tblSalidaDetalle').DataTable();
    table
        .row($(this).parents('tr'))
        .remove()
        .draw();
    $.ajax({
        url: "/Salida/RemoveSalidaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle }),
        success: function (data)
        {
           contador = contador - 1
           console.log("Contador: " + contador)
           RejectUnload()
        }
    });

});


























//var prod_CodigoCampo = $('#prod_Codigo').val();
//var currentRow = $("#tblSalidaDetalle tbody tr");
//var prod_CodigoTabla = currentRow.find("td:eq(0)").text();
//var CantidadM = $('#sald_Cantidad').val();

//copiar = "<tr data-id=" + idcontador + " , tr data-prod_Codigo = " + $('#prod_Codigo').val() + ">";
//copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
//copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
//copiar += "<td id = 'Marca'>" + $('#prod_Marca').val() + "</td>";
//copiar += "<td id = 'Modelo'>" + $('#prod_Modelo').val() + "</td>";
//copiar += "<td id = 'Talla'>" + $('#prod_Talla').val() + "</td>";
//copiar += "<td id = 'Categoria'>" + $('#pcat_Id').val() + " " + $('#pscat_Id').val() + "</td>";
//copiar += "<td id = 'Unidad_Medida'>" + $('#uni_Id').val() + "</td>";
//copiar += "<td id = 'Cantidad'>" + sumacantidades + "</td>";
//copiar += "<td>" + '<button id="removeSalidaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
//copiar += "</tr>";
//$('#tblSalidaDetalle').append(copiar);

//copiar = "<tr data-id=" + contador + ", tr data-prod_Codigo = " + $('#prod_Codigo').val() + ">";
//copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
//copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
//copiar += "<td id = 'Marca'>" + $('#prod_Marca').val() + "</td>";
//copiar += "<td id = 'Modelo'>" + $('#prod_Modelo').val() + "</td>";
//copiar += "<td id = 'Talla'>" + $('#prod_Talla').val() + "</td>";
//copiar += "<td id = 'Categoria'>" + $('#pcat_Id').val() + " " + $('#pscat_Id').val() + "</td>";
//copiar += "<td id = 'Unidad_Medida'>" + $('#uni_Id').val() + "</td>";
//copiar += "<td id = 'Cantidad'>" + $('#sald_Cantidad').val() + "</td>";
//copiar += "<td>" + '<button id="removeSalidaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
//copiar += "</tr>";
//$('#tblSalidaDetalle').append(copiar);

//var t = $('#tblSalidaDetalle').DataTable();
//var counter = 0;

//t.row.add([

//]).draw(false);

//$(document).ready(function () {
//    //// Automatically add a first row of data
//    //$('#addRow').click();
//    //$('#tsal_Id').val()
//    //var TipoSalida = TipoSalida();
//    //var tblSalidaDetalle = $('#tblSalidaDetalle >tbody >tr').length;
//    //var Bodega = fBodega();
//});
//$('#FacturaError').text('');
//$('#validationFactura').after('<ul id="FacturaError" class="validation-summary-errors text-danger">Factura</ul>');
//$('#FacturaError').text('');
//$('#validationFactura').after('<ul id="FacturaError" class="validation-summary-errors text-danger">Factura</ul>');
//$('#Without').css("display", "block");
//$(function () { })
//$('#exampleModalCenter').modal('show');
//$("#exampleModalCenter").on('hidden.bs.modal', function () {
//    $('#prod_CodigoBarras').focus()
//});
//console.log(vSalidaDetalle + ' 0')
//console.log(vSalidaDetalle + ' 1')
//getCartProduct(id, returnData);

//function returnData(param) {
//    console.log(param);
//}getCartProduct(myId).done(function(data) { ... })
//function ProductoCantidad(bod_Id, prod_Codigo) {
//    $.post('/Salida/Cantidad', {
//        bod_Id: bod_Id,
//        prod_Codigo: prod_Codigo
//    }, function (data) {
//        return data
//    });
//}

//var size = $('#tblSalidaDetalle >tbody >tr').length
//$('#tblSalidaDetalle >tbody >tr').length > 0

//var prod_CodigoTablaz = $(this).closest("tr").find("td:eq(0)").text();
//////var prod_CodigoTabla = $(this).find("td:eq(0)").text();
//var prod_CodigoTablae = $(this).find("td:eq(0)").innerText();
//var prod_CodigoTabla2 = $(this).find("td:eq(0)").innerText();

// var prod_CodigoTabl2a2 = $(this).data("id");

//console.log(CantidadTabla)

////var CantidadTabla = $(this).find("td:eq(7)").text();
//.done(function (data) {
//    var CantidadAceptada = data.CantidadAceptada;
//    var CantidadMinima = data.CantidadMinima;
//    console.log(data.CantidadAceptada)
//    if (CantidadAceptada == 0 && CantidadMinima > 0) {
//        $('#CantidaExistenteProd').text('');
//        $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Minima Alcanzada solo hay en existencia: ' + CantidadMinima + ' de este producto</ul>');

//    }
//    else {
//        $('#CantidaExistenteProd').text('');
//        $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Existente en Bodega: ' + CantidadAceptada + '</ul>');
//    }
//    //var ProductoCantidadAva = CantidadAceptada;
//    //return CantidadAceptada
//    //data.startsWith("Cantidad")
//    //if (typeof num1 == 'number')
//    //if (isNaN(data)) {
//    //    $('#CantidaExistenteProd').text('');
//    //    $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">' + data + '</ul>');
//    //}
//    //else {
//    //    $('#CantidaExistenteProd').text('');
//    //    $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Existente en Bodega: ' + data + '</ul>');
//    //}

//    //$('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="alert alert-success">Cantidad Existente en Bodega: ' + data + '</ul>')

//    //console.log(elemento)
//    //if (elemento != '') {
//    //    console.log(elemento)
//    //    //('#CantidaExistente').removeChild(div.lastChild);class="validation-summary-errors text-danger"

//    //    var elemento = document.getElementById('CantidaExistenteProd');
//    //    elemento.removeAttribute("class");
//    //    console.log(elemento)

//    //}
//})

//changeState(this, document.getElementById('nombre'))

//function changeState(check, element, valor) {
//    if (!check.checked) {
//        element.setAttribute('max', valor); //atributo y valor
//    } else {
//        element.removeAttribute('max'); //el atributo
//    }
//}

//Agregar detalle por medio de codigo de Barra
//$('#prod_CodigoBarras').focus(function () {
//    ListaProductos()
//})

//var prod_CodigoTabla = $(this).closest("tr").find("td:eq(0)").text();

//if (CantidadTabla >= CantidadAceptada && CantidadRestante > 0) {
//    $('#CantidaExistenteProd').text('');
//    $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Minima Alcanzada solo hay en existencia: ' + CantidadRestante + ' de este producto</ul>');
//}

//var prod_CodigoTabla = $(this).closest("tr").find("td:eq(0)").text();

//if (CantidadTabla >= CantidadAceptada && CantidadRestante > 0) {
//    $('#CantidaExistenteProd').text('');
//    $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Minima Alcanzada solo hay en existencia: ' + CantidadRestante + ' de este producto</ul>');
//}
//console.log(sumacantidades);
//$(this).closest('tr').remove();

//function DeleteSalidaDetalle(sald_Id) {
//    $.ajax({
//        url: "/Salida/DeleteSalidaDetalle",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",removeSalidaDetalle
//        data: JSON.stringify({ sald_Id: sald_Id }),
//    })
//        .done(function (data) {
//            console.log(data);
//            location.reload();
//        })
//}

//function EditSalidaDetalles(sald_Id) {
//    $.ajax({
//        url: "/Salida/getSalidaDetalle",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ sald_Id: sald_Id }),
//    })
//        .done(function (data) {
//            if (data.length > 0) {
//                $.each(data, function (i, item) {
//                    $("#sal_Id_SD").val(item.sal_Id);
//                    $("#sald_Id_SD").val(item.sald_Id);
//                    $("#prod_Codigo_SD").val(item.prod_Codigo);
//                    $("#sald_Cantidad_SD").val(item.sald_Cantidad);
//                    $("#prod_Descripcion_SD").val(item.prod_Descripcion);
//                    $("#pcat_Nombre_SD").val(item.pcat_Nombre);
//                    $("#pscat_Descripcion_SD").val(item.pscat_Descripcion);
//                    $("#uni_Descripcion_SD").val(item.uni_Descripcion);
//                    $("#EditSalidaDetalle").modal();
//                })
//            }
//        })
//}

//$("#BtnsubmitEdit").click(function () {
//    var Cantidad = $('#sald_Cantidad_SD').val();
//    if (Cantidad == '') {
//        $('#MessageError').text('');
//        $('#CodigoError').text('');
//        $('#NombreError').text('');
//        $('#validationsald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
//    }
//    else if (Cantidad < '0.05') {
//        $('#NombreError').text('');
//        $('#validationsald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad muy Pequeña</ul>');
//    }
//    else {
//        var sal_id = $('#sald_Id').val();
//        var data = $("#SubmitForm").serializeArray();
//        $.ajax({
//            type: "Post",
//            url: "/Salida/EditSalidaDetalle",
//            data: data,
//            success: function (result) {
//                if (result == '-1')
//                    $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
//                else
//                    console.log(result);
//                window.location.href = '/Salida/Edit/' + result;
//            }
//        });
//    }
//})

//////////////////////////////////////////////////////////////////////////////////////
//Edit

//function GetSalidaDetalle() {
//    var SalidaDetalle = {
//        prod_Codigo: $('#prod_Codigo').val(),
//        sald_Cantidad: $('#sald_Cantidad').val(),
//        sal_Id: $('#sal_Id').val(),
//        sald_UsuarioCrea: contador
//    };
//    return SalidaDetalle;
//}

//$('#AgregarSalidaDetalleEdit').click(function () {
//    SeleccionProducto()
//});

//function GetNewSalidaDetalle() {
//    var SalidaDetalle = {
//        sal_Id: $('#sal_Id').val(),
//        prod_Codigo: $('#prod_Codigo').val(),
//        sald_Cantidad: $('#sald_Cantidad').val(),
//        sald_UsuarioCrea: contador
//    };
//    return SalidaDetalle;
//}

//$('#btnCreateSalidaDetalle').click(function () {
//    var Cod_Producto = $('#prod_Codigo').val();
//    var Producto = $('#prod_Descripcion').val();
//    var Cantidad = $('#sald_Cantidad').val();
//    console.log('Funca');
//    var tbSalidaDetalle = GetNewSalidaDetalle();
//    $.ajax({
//        url: "/Salida/SaveNewDatail",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle }),
//    })
//        .done(function (data) {
//            if (data == 'El registro se guardo exitosamente') {
//                location.reload();
//                swal("El registro se almacenó exitosamente!", "", "success");
//            }
//            else {
//                location.reload();
//                swal("El registro  no se almacenó!", "", "error");
//            }
//        });
//})

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//var inputs = document.getElementById('sald_Cantidad')

////changeState(this, document.getElementById('nombre'))
//changeState(this, inputs, data)
//function changeState(check, element, valor) {
//    if (!check.checked) {
//        element.setAttribute('max', valor); //atributo y valor
//    } else {
//        element.removeAttribute('max'); //el atributo
//    }
//}

//$(document).on("click", "#Table_BuscarProductoD tbody tr td button#seleccionar", function () {
//    var bod_Id = $('#bod_Id').val()
//    var prod_CodigoBarrasItem = $(this).closest('tr').data('cod_barra');
//    Producto()
//});
////

//function GetSalidaDetalles() {
//    var table = '#Table_BuscarProductoD tbody tr td button#seleccionar'
//    var SalidaDetalle = {
//        prod_Codigo: $('#prod_Codigo').val(),
//        bod_Id: $('#bod_Id').val(),
//        prod_CodigoBarras: $(table).closest('tr').data('cod_barra'),
//        sald_Cantidad: $('#sald_Cantidad').val(),
//        sald_UsuarioCrea: contador
//    };
//    return SalidaDetalle;
//}

//function Producto(SalidaDetalle) {
//    var SalidaDetalles = GetSalidaDetalles
//    $("#prod_CodigoBarras").val();
//    console.log(SalidaDetalles().prod_CodigoBarras);
//    var cod_Barras = $("#prod_CodigoBarras").val();
//    $.ajax({
//        url: "/Salida/GetProdCodBar",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({
//            bod_Id: SalidaDetalle.bod_Id,
//            prod_CodigoBarras: Producto
//        }),
//    }).done(function (data) {
//        console.log(data);
//        if (data.length > 0) {
//            $.each(data, function (key, value) {
//                $("#prod_CodigoBarras").val(value.prod_CodigoBarras);
//                $('#prod_Codigo').val(value.prod_Codigo);
//                $('#prod_Descripcion').val(value.prod_Descripcion);
//                $("#uni_Id").val(value.uni_Descripcion);
//                $("#pscat_Id").val(value.pscat_Descripcion);
//                $("#pcat_Id").val(value.pcat_Nombre);
//            })
//            $('#prod_CodigoBarras').text('');
//            $('#sald_Cantidad').text('');
//            $("#sald_Cantidad").focus();
//        }
//        else {
//            $('#prod_CodigoBarras').text('');
//            $('#validationprod_CodigoBarras').after('<ul id="Error_Barras" class="validation-summary-errors text-danger">*Producto no existe</ul>');

//            idItem = $(this).closest('tr').data('id');
//            contentItem = $(this).closest('tr').data('content');
//            uni_IdtItem = $(this).closest('tr').data('keyboard');
//            psubctItem = $(this).closest('tr').data('container');
//            pcatItem = $(this).closest('tr').data('pcat');
//            prod_CodigoBarrasItem = $(this).closest('tr').data('cod_barras');
//            $("#prod_Codigo").val(idItem);
//            $("#prod_Descripcion").val(contentItem);
//            $("#uni_Id").val(uni_IdtItem);
//            $("#pscat_Id").val(psubctItem);
//            $("#pcat_Id").val(pcatItem);
//            $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
//            $("#sald_Cantidad").focus();
//        }
//    });
//    return false;
//}

//Tabla del Detalle
//$(document).ready(function () {
//    $('#tblSalidaDetalle1').DataTable(
//        {
//            "searching": true,
//            "lengthChange": false,
//            "oLanguage": {
//                "oPaginate": {
//                    "sNext": "Siguiente",
//                    "sPrevious": "Anterior",
//                },
//                "sEmptyTable": "Agregue un Producdo",
//                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
//                "sSearch": "Buscar",
//                "sInfo": "Mostrando _START_ a _END_ Entradas",
//            }
//        });

//    var $rows = $('#tblSalidaDetalle1 tr');
//    $("#search").keyup(function () {
//        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

//        $rows.show().filter(function () {
//            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
//            return !~text.indexOf(val);
//        }).hide();
//    });
//});

//$(document).ready(function () {
//    var Name = '#tbSalidaDetalle';

//    Datatable(Name)
//});

//function Datatable(Name) {
//    $(Name).DataTable(
//        {
//            "searching": false,
//            "lengthChange": false,

//            "oLanguage": {
//                "oPaginate": {
//                    "sNext": "Siguiente",
//                    "sPrevious": "Anterior",
//                },
//                "sProcessing": "Procesando...",
//                "sLengthMenu": "Mostrar _MENU_ registros",
//                "sZeroRecords": "No se encontraron resultados",
//                "sEmptyTable": "Ningún dato disponible en esta tabla",
//                "sEmptyTable": "No hay registros",
//                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
//                "sSearch": "Buscar",
//                "sInfo": "Mostrando _START_ a _END_ Entradas",
//                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",

//            }
//        });
//};

////Tabla de Busqueda Generica
//$(document).ready(function () {
//    $('#Table_BuscarProducto').DataTable(
//        {
//            "searching": false,
//            "lengthChange": false,

//            "oLanguage": {
//                "oPaginate": {
//                    "sNext": "Siguiente",
//                    "sPrevious": "Anterior",
//                },
//                "sEmptyTable": "No hay registros",
//                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
//                "sSearch": "Buscar",
//                "sInfo": "Mostrando _START_ a _END_ Entradas",

//            }
//        });

//    var $rows = $('#Table_BuscarProducto tr');
//    $("#search").keyup(function () {
//        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

//        $rows.show().filter(function () {
//            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
//            return !~text.indexOf(val);
//        }).hide();
//        //$rows.show().filter(function () {
//        //    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
//        //    return !~text.indexOf(val);
//        //}).hide();

//    });
//});

//function GetProdCodBar() {
//    var bod_Id = $('#bod_Id').val()
//    var prod_CodigoBarras = $('#prod_CodigoBarras').val()
//    $.ajax({
//        url: "/Salida/GetProdCodBar",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ bod_Id: bod_Id, prod_CodigoBarras: prod_CodigoBarras }),
//    })
//        .done(function (data) {
//            console.log(data)
//            $.each(data, function (key, value) {
//                $("#prod_CodigoBarras").val(value.prod_CodigoBarras);
//                $("#prod_Descripcion").val(value.prod_Codigo);
//                $("#sald_Cantidad").focus();
//            })
//        })
//}
//    $('#prod_CodigoBarras').keydown(function (e) {
//        if (e.keyCode == 13) {
//            var bod_Id = $('#bod_Id').val()
//            var prod_CodigoBarras = $('#prod_CodigoBarras').val()
//            $.ajax({
//                url: "/Salida/GetProdCodBar",
//                method: "POST",
//                dataType: 'json',
//                contentType: "application/json; charset=utf-8",
//                data: JSON.stringify({ bod_Id: bod_Id, prod_CodigoBarras: prod_CodigoBarras }),
//            })
//                .done(function (data) {
//                    console.log(data)
//                    $.each(data, function (key, value) {
//                        $("#prod_CodigoBarras").val(data.prod_Codigo);
//                        $("#prod_Codigo").val(data.prod_Descripcion);
//                        $("#prod_Descripcion").val(value.prod_Descripcion);
//                        $("#uni_Id").val(uni_IdtItem);
//                        $("#pscat_Id").val(psubctItem);
//                        $("#pcat_Id").val(pcatItem);
//                        $("#sald_Cantidad").focus();
//                    })
//                })
//                $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
//                $("#prod_Codigo").val(idItem);
//                $("#prod_Descripcion").val(contentItem);
//                $("#uni_Id").val(uni_IdtItem);
//                $("#pscat_Id").val(psubctItem);
//                $("#pcat_Id").val(pcatItem);
//                $("#sald_Cantidad").focus();
//            //$("#cod").val(idItem);
//            return false;
//        }
//            });
//            console.log('prueba');
//            $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
//            $("#prod_Codigo").val(idItem);
//            $("#prod_Descripcion").val(contentItem);
//            $("#uni_Id").val(uni_IdtItem);
//            $("#pscat_Id").val(psubctItem);
//            $("#pcat_Id").val(pcatItem);
//            $("#sald_Cantidad").focus();
//            return false;
//    );

//function GetSalidaDetalles() {
//    var SalidaDetalle = {
//        prod_Codigo: $('#prod_Codigo').val(),
//        sald_Cantidad: $('#sald_Cantidad').val(),
//        sald_UsuarioCrea: contador
//    };
//    return SalidaDetalle;
//}

//$("#sal_FechaElaboracion").click(function () {
//    var SalidaDetalles2c = GetSalidaDetalles
//    $("#tblSalidaDetalle").length;
//    console.log(SalidaDetalles2c().prod_Codigo);

//})

//function GetProdCodBar() {
//    var bod_Id = $('#bod_Id').val()
//    var prod_CodigoBarras = $('#prod_CodigoBarras').val()
//    $.ajax({
//        url: "/Salida/GetProdCodBar",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ bod_Id: bod_Id, prod_CodigoBarras: prod_CodigoBarras }),
//    })
//        .done(function (data) {
//            $('#CodigoError').text('');
//            $('#validationFactura').after('<ul id="CodigoError" class="validation-summary-errors text-danger">' + data + '</ul>');
//        })
//}
//function test() {
//    $.ajax({
//        method: "POST",
//        url: "/Salida/GetProdList",
//        contentType: "application/json; charset=utf-8",
//        dataType: 'json',
//    }).done(function (info) {
//        console.log(info);
//    });
//}
//function ListaProductos() {
//    var table = $('#Table_BuscarProductoD').dataTable({
//        destroy: true,
//        resposive: true,
//        ajax: {
//                method: "POST",
//                url: "/Salida/GetProdList",
//                contentType: "application/json; charset=utf-8",
//                dataType: 'json',
//                dataSrc: "d"
//        },
//        columns: [
//                     { "d": ".prod_Codigo"}
//                 ]
//    })
//}

//$(document).ready(function () {
//    console.log("ready")
//    ListaProductos()
//});

//$.when(LimpiarTable()).then(function () {
//    $(document).ready(function () {
//        console.log("ready")
//        ListaProductos()
//    });
//});

//$('#submit').attr('value = "SICambio"')

//$(document).ready( function () {
//    var e = document.getElementById("bod_Id");
//    var strUser = e.options[e.selectedIndex].text;
//    console.log(strUser)
//    $("#tbBodega_bod_Nombre").val(strUser)

//});

//$(document).ready(function () {
//    var e = document.getElementById("estm_Id");
//    var strUser = e.options[e.selectedIndex].text;
//    console.log(strUser)
//    $("#tbEstadoMovimiento_estm_Descripcion").val(strUser)

//});

//$(document).ready(function () {
//    var e = document.getElementById("tbFactura_fact_Codigo");
//    var strUser = e.options[e.selectedIndex].text;
//    console.log(strUser)
//    $("#tbFactura_fact_Codigo").val(strUser)

//});
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

//window.addEventListener("load", function () {
//    Miform.sald_Cantidad.addEventListener("keypress", soloNumeros, false);
//});

////$('#prod_Dsescripcion').mask('0000-0000');
//$(document).on("change", "#sald_Cantidad", function () {
//    var fiel = $("#sald_Cantidad").val();
//    soloNumeros(fiel);
//});

//Solo permite introducir numeros.
//function soloNumeros(e) {
//    var key = window.event ? e.which : e.keyCode;
//    //|| key == 44
//    if (key == 46) {
//    }
//    else {
//        if (key < 48 || key > 57) {
//            e.preventDefault();
//        }
//    }}
//var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;

//if (e.value.match(phoneno)) {
//    return true;

//}

//else {
//    alert("message");

//    return false;

//}
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

////})
//var masked = IMask.createMask({
//    mask: '000-000-00-00000000'
//    // ...and other options
//});

//var maskedValue = masked.resolve('71234567890');

//// mask keeps state after resolving
//console.log(masked.value);  // same as maskedValue
//// get unmasked value
//console.log(masked.unmaskedValue);

//function sald_Cantidad() {
//    //this.value = this.value.replace(/[^0-9\.]/g,'');
//    $(this).val($(this).val().replace(/[^0-9.\.]/g, ''));
//    if ((event.which != 46 || $(this).val().indexOf('') != -1) && (event.which < 48 || event.which > 57)) {
//        event.preventDefault();
//    }
//}

//$("#sald_Cantidad").on("keypress keyup blur", function (event) {
//    sald_Cantidad()
//});

//$(document).ready(function () {
//    $("#fact_Codigo").mask('999-999-99-99999999');
//});

//var dateMask = new IMask(
//    document.getElementById('fact_Codigo'),
//    {
//        mask: Date,
//        min: new Date(1990, 0, 1),
//        max: new Date(2020, 0, 1),
//        lazy: false
//    });

//$(document).ready(function () {
//    var getBrowserInfo = function () {
//        var ua = navigator.userAgent, tem,
//            M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
//        if (/trident/i.test(M[1])) {
//            tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
//            return 'IE ' + (tem[1] || '');
//        }
//        if (M[1] == 'Chrome') {
//            tem = ua.match(/\b(OPR|Edge)\/(\d+)/);
//            if (tem != null) return tem.slice(1).join(' ').replace('OPR', 'Opera');
//        }
//        M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
//        if ((tem = ua.match(/version\/(\d+)/i)) != null) M.splice(1, 1, tem[1]);
//        return M.join(' ');
//    };
//    console.log(getBrowserInfo());

//    console.log(bowser.name, bowser.version);
//});

//OLD SAVE EditSalidaDetalle
//$("#BtnsubmitEdit").click(function () {
//    //var sal_Id = $('#sal_Id_SD').val();
//    //var sald_Id = $('#sald_Id_SD').val();
//    //var prod_Codigo = $('#prod_Codigo_SD').val();
//    //var sald_Cantidad = $('#sald_Cantidad_SD').val();
//    //var sald_UsuarioCrea = $('#sald_UsuarioCrea_SD').val();
//    //var sald_FechaCrea = $('#sald_FechaCrea_SD').val();

//    //var data = {
//    //    sal_Id: sal_Id,
//    //    sald_Id: sald_Id,
//    //    prod_Codigo: prod_Codigo,
//    //    sald_Cantidad: sald_Cantidad
//    //};
//    if (sald_Cantidad == '') {
//        $('#MessageError').text('');
//        $('#CodigoError').text('');
//        $('#NombreError').text('');
//        $('#validationsald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
//    }
//    else if (sald_Cantidad < '0.05') {
//        $('#NombreError').text('');
//        $('#validationsald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad muy Pequeña</ul>');
//    }
//    else {
//        var sal_id = $('#sald_Id').val();
//        var data = $("#SubmitForm").serializeArray();
//        $.ajax({
//            type: "Post",
//            url: "/Salida/EditSalidaDetalle",
//            data: data,
//            success: function (result) {
//                if (result == '-1')
//                    $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
//                else
//                    console.log(result);
//                window.location.href = '/Salida/Edit/' + result;
//            }
//        });
//    }
//})

//$("#Body_BuscarProducto").remove()

//function ListaProductos() {
//    var vbod_Id = $('#bod_Id').val()
//    var BodegaDetalle = {
//        bod_Id: vbod_Id
//    };
//    $.ajax({
//        url: "/Salida/GetProdList",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ tbBodegaDetalle: BodegaDetalle }),
//    }).done(function (data) {
//        $("#Body_BuscarProducto").html("");
//        $.each(data, function (key, value) {
//            key = "<tr tr data-id =" + value.prod_Codigo + " , tr data-content =" + value.prod_Descripcion + " tr data-container =" + value.pscat_Descripcion + " , tr data-keyboard =" + value.uni_Descripcion + " , tr data-pcat=" + value.pcat_Nombre + " , tr data-cod_Barra=" + value.prod_CodigoBarras + " >";
//            key += "<td id ='prod_Codigo' >" + value.prod_Codigo + "</td>";
//            key += "<td id ='prod_Descripcion' >" + value.prod_Descripcion + "</td>";
//            key += "<td id = 'pcat_Nombre'>" + value.pcat_Nombre + "</td>";
//            key += "<td id = 'pscat_Descripcion'>" + value.pscat_Descripcion + "</td>";
//            key += "<td id = 'uni_Descripcion'>" + value.uni_Descripcion + "</td>";
//            key += "<td id = 'prod_CodigoBarras'>" + value.prod_CodigoBarras + "</td>";
//            key += "<td>" + "<button class='btn btn-primary btn-xs' value=" + value.prod_Codigo + " id='seleccionar' data-dismiss='modal'>Producto</button>" + "</td>"
//            key += "</tr>";
//            $("#Body_BuscarProducto").append(key)
//        })
//    });
//}

//var bod_Id = $(this).closest('tr').data('cod_barra');
//var prod_CodigoBarrasItem = $(this).closest('tr').data('cod_barra');

//$(this).closest('tr').remove();

//$('#tblBusquedaGenerica tbody').on('click', 'button', function () {
//    var data = table.row($(this).parents('tr')).data();
//    alert(data[0]);
//});

//$('#Productos').on("click", function () {
//    test()
//    ListaProductos2()
//    });

//$('#tblBusquedaGenerica tbody').on('click', '[id*=btnDetails]', function () {
//    var data = table.row($(this).parents('tr')).data();
//});
//,
//"columnDefs": [{
//    "targets": -1,
//    "data": null,
//    "defaultContent": "<button>Click!</button>"
//}],
//function ListaProductos() {
//    var table = $("#").DataTable({
//        destroy: true,
//        responsive: true,
//        ajax: {
//            method: "POST",
//            url: "/Salida/GetProdList",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            data: function (d) {
//                return JSON.stringify(d);
//            },
//            dataSrc: "d.data"
//        },
//        columns: [
//            { "data": "prod_Codigo" }
//        ]
//    });
//}

$(document).ready(function () {
});

//function Busqueda() {
//    var tbBodegaDetalle = {
//        bod_Id: $('#prod_Codigo').val(),
//    };
//    var bod_Id = $('#bod_Id').val()
//    //var url = "/Factura/GetDetalleEdit?StudentId=" + StudentId;
//    var url = "/Salida/GetProducto?bod_Id=" + bod_Id;
//    var table = $("#table-users").DataTable({
//        destroy: true,
//        responsive: true,
//        ajax: {
//            method: "POST",
//            url: url,
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            dataSrc: "list"
//             },
//        columns: [
//            { "list": ".prod_Codigo"}
//                 ]
//    });
//}
//}

//function ListaProductos() {
//    var bod_Id = $('#bod_Id').val()
//    url = "/Salida/GetProducto?bod_Id=" + bod_Id;
//    var table = $('#table-users').dataTable({
//        destroy: true,
//        resposive: true,
//        ajax: {
//            method: "POST",
//            url: url,
//            contentType: "application/json; charset=utf-8",
//            dataType: 'json',
//            dataSrc: "list"
//        },
//        columns: [
//            { "list": "prod_Codigo" }
//        ]
//    })
//    console.log(list);
//}

//dataSrc: "d.data"
//        },
//prod_Codigo	25 - 32 - 00002
//prod_Descripcion	Sierra Electrica
//pcat_Nombre	Herramientass
//pscat_Descripcion	Herramientas para carpinteria
//uni_Descripcion	Unidad
//prod_CodigoBarras	7421212012

//function test() {
//    var bod_Id = $('#bod_Id').val()
//    url = "/Salida/GetProducto?bod_Id=" + bod_Id;
//    $.ajax({
//        method: "POST",
//        url: url,
//        contentType: "application/json; charset=utf-8",
//        dataType: 'json',
//    }).done(function (info) {
//        console.log(info);
//    });
//}

//function __showUsers() {
//}

//document.getElementById("myDiv");  find("td:eq(0)").
//var elem =
//var elem =

//if (prod_CodigoTabla.startsWith("No hay")) {
//    $("#tblSalidaDetalle >tbody >tr").each(function () {
//        if (prod_CodigoCampo == prod_CodigoTabla) {
//            var CantidadTabla = currentRow.find("td:eq(7)").text();
//            var CantidatTotal = parseFloat(CantidadAceptada) + parseFloat(CantidadMinima)
//            var CantidadRestante = parseFloat(CantidadAceptada) - parseFloat(CantidadTabla)
//            if (CantidadTabla >= CantidadAceptada && CantidadRestante > 0) {
//                $('#CantidaExistenteProd').text('');
//                $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Minima Alcanzada solo hay en existencia: ' + CantidadRestante + ' de este producto</ul>');
//            }
//            else {
//                if (CantidadTabla < CantidadAceptada) {
//                    CantidadAceptada = CantidadAceptada - CantidadTabla
//                    $('#CantidaExistenteProd').text('');
//                    $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Existente en Bodega: ' + CantidadAceptada + '</ul>');
//                }
//                else {
//                    if (CantidadTabla == CantidadAceptada) {
//                        $('#CantidaExistenteProd').text('');
//                        $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Sin Existencias de este producto</ul>');
//                    }
//                }
//            }
//        }
//    })
//}
//else {
//    Aceptada = CantidadAceptada
//    if (CantidadAceptada == 0 && CantidadMinima > 0) {
//        $('#CantidaExistenteProd').text('');
//        $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Minima Alcanzada solo hay en existencia: ' + CantidadMinima + ' de este producto</ul>');
//    }
//    else {
//        $('#CantidaExistenteProd').text('');
//        $('#CantidaExistente').after('<ul id="CantidaExistenteProd" class="validation-summary-errors text-danger">Cantidad Existente en Bodega: ' + Aceptada + '</ul>');
//    }
//}