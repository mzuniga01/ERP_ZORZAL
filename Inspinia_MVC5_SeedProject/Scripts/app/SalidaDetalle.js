var contador = 0;
var StillAjax = false; //bool
var vbox_Codigo = 0
var CantidadExit = 0.00;
var contador = 0;
var CantidadTable_VAR = 0

$(document).ready(function () {
    table = $('#tblSalidaDetalle').DataTable();
    contadorinit = table.rows().data().length;
    contador = contadorinit;
    console.log(window.location.pathname)
})

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


$(function () {
    $("#sal_FechaElaboracion").datepicker({
        dateFormat: 'dd-mm-yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        buttonImageOnly: true,
        minDate: '0',
        inline: true,
        changeMonth: true,
        changeYear: true
    }).datepicker("setDate", new Date()), { showAnim: "fade-in" };
});
//maxDate: '0',

function validateMyForm() {
    //Bodega
    //var Bod = document.getElementById("bod_Id");
    //var Bodega = Bod.options[Bod.selectedIndex].text;

    /////////
    //TipoSalida
 
    /////////
    var Fecha = $('#sal_FechaElaboracion').val();

    var BodDestino = $("#sal_BodDestino").val()
    var validacionFactura = $("#CodigoError").text()
    FacturaCodigoError = $("#FacturaCodigoError").text()

    //var sal_RazonDevolucion = $("#sal_RazonDevolucion").val()

    var currentRow = $("#tblSalidaDetalle tbody tr");
    var Tabla = currentRow.find("td:eq(0)").text();
   
    var txtTipoSalida = document.getElementById("tsal_Id");
    var lblTipoSalida = txtTipoSalida.options[txtTipoSalida.selectedIndex].text;
    var TipoSalida = normalize(lblTipoSalida.toUpperCase());
    if (Fecha == '' || Fecha == null) {
        $('#sal_FechaElaboracionError').text('');
        $('#validationsal_FechaElaboracion').after('<ul id="sal_FechaElaboracionError" class="validation-summary-errors text-danger">Fecha Requerida</ul>');
        vFecha = false;
    }
    else {
        $('#sal_FechaElaboracionError').text('');
        vFecha = true
    }
    //if (Bodega.startsWith("Seleccione")) {
    //    $('#bod_IdError').text('');
    //    $('#validationbod_Id').after('<ul id="bod_IdError" class="validation-summary-errors text-danger">Seleccione una Bodega</ul>');
    //    vBodega = false;
    //}
    //else {
    //    $('#bod_IdError').text('');

    //    vBodega = true
    //}|| !vBodega
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
              
                if (!validacionFactura.startsWith("Factura") || FacturaCodigoError != '') {
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
                    if (!validacionFactura.startsWith("Factura") || FacturaCodigoError != '') {
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
                                            +'<button type="button" class="close" data-dismiss="alert" aria-label="Close">'
                                                    +'<span aria-hidden="true">&times;</span>'
                                            +'</button>'
                                        +'<strong>Detalle Vacio</strong>'
                                  + '</div>');
        //document.getElementById('alert_message').scrollIntoView()
        window.location.hash = '#alert_message';


        //setInterval(function () {
        //    $('#alert_message').html('');
        //    $('#prod_CodigoBarras').focus()
        //}, 5000);
    }
    else {
        $("#alert_message").css("display", "none");
        vSalidaDetalle = true;
    }
    if (!vFecha  || !vTipoSalida || !vBodDestino || !vFactura || !vDevolucion || !vSalidaDetalle) {
        return false;
    }
    else {
        return true
    }
} 
function GetSalidaDetalleBox() {
    var SalidaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        sald_Cantidad: $('#sald_Cantidad').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}
$('#tdev_Id').click(function () {
    $('#sal_RazonDevolucionError').text('');
})


$(document).on("click", "#tblBusquedaGenericaBox tbody tr td button#seleccionarBox", function () {
    var table = $('#tblSalidaDetalle').DataTable();
    //table.columns([8]).visible(false);
    var box_Codigo = this.value;
    $(this).after('<button class="btn btn-danger btn-xs" id="RemoveBox" data-dismiss="modal" value="' + box_Codigo + '">Quitar</button>');
    this.remove()
    //var prod_CodigoTabla = table.rows("td:eq(0)").text();
    //var rows = $('tr')
    //var box_Codigo = table.rows(':eq(0)').data()[0][0];
    console.log(box_Codigo)
    //$(this.value).append('<button class="btn btn-danger btn-xs" id="RemoveBox" data-dismiss="modal">Quitar</button>');
    //$(box_Codigo).$(this.remove());
    //var button = '<button class="btn btn-danger btn-xs" id="RemoveBox" data-dismiss="modal">Quitar</button>';
    var bod_Id = $('#bod_Id').val();
    $.ajax({
        url: "/Salida/GetBox",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            bod_Id: bod_Id,
            box_Codigo: box_Codigo
        }),
        success: function (data) {
            $.each(data, function (key, value) {
                SalidaDetalle = null;
                var SalidaDetalle = {
                    prod_Codigo: value.prod_Codigo,
                    sald_Cantidad: value.boxd_Cantidad,
                    box_Codigo: value.box_Codigo,
                    sald_UsuarioCrea: contador
                        };
                console.log(SalidaDetalle)
                $.ajax({
                            url: "/Salida/SaveSalidaDetalle",
                            method: "POST",
                            dataType: 'json',
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify({ SalidaDetalle: SalidaDetalle, data_producto: value.prod_Codigo }),
                }).done(function (datos) {

                     contador = contador + 1
                    table.row.add([
                        value.prod_Codigo,
                        value.prod_Descripcion,
                        value.prod_Marca,
                        value.prod_Modelo,
                        value.prod_Talla,
                        value.pcat_Nombre,
                        value.uni_Descripcion,
                        value.boxd_Cantidad,
                        '<p id="' + value.box_Codigo + '">En Caja: ' + value.box_Codigo + '</p>',
                        contador
                    ]).draw(false).node().id = '' + value.box_Codigo+'';
                        })
            })
           
        }
        })
})

$('#pcat_Id').click(function () {

    var oTable = $('#tblSalidaDetalle').dataTable();

    //find('tr:contains("' + value.replace("En Caja: ", "") + '")').remove();
    var row = oTable.find('tr:contains("' + value.replace("En Caja: ", "") + '")').eq(3);
    oTable.fnDeleteRow(row[0]);
    //var table = $('#tblSalidaDetalle').DataTable();
    //var rows = $('tr')

    //var row = table.find('tr').eq(3);
    //table.fnDeleteRow(row[0]);
    //var prod_CodigoTabla = table.rows(':eq(8)').data()[0][0];
    //table.column(9).data().each(function (value, index) {
    //    console.log(index)
    //    console.log(value)
    //    table.column(9).row(''+value+'').remove().draw(false);
    //})

})






$(document).on("click", "#tblBusquedaGenericaBox tbody tr td button#RemoveBox", function () {
    var oTable = $('#tblSalidaDetalle').dataTable();
    var table = $('#tblSalidaDetalle').DataTable();
    var box_Codigo = this.value;
    $(this).after('<button class="btn btn-primary btn-xs" value="' + box_Codigo + '" id="seleccionarBox" data-dismiss="modal">Seleccionar</button>');
    this.remove()
    //var prod_CodigoTabla = table.rows("td:eq(0)").text();
    //var rows = $('tr')
    //var box_Codigo = table.rows(':eq(0)').data()[0][0];
    console.log(box_Codigo)
    //$(this.value).append('<button class="btn btn-danger btn-xs" id="RemoveBox" data-dismiss="modal">Quitar</button>');
    //$(box_Codigo).$(this.remove());
    //var button = '<button class="btn btn-danger btn-xs" id="RemoveBox" data-dismiss="modal">Quitar</button>';
    var bod_Id = $('#bod_Id').val();
    $.ajax({
        url: "/Salida/GetBox",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            bod_Id: bod_Id,
            box_Codigo: box_Codigo
        }),
        success: function (data) {
            $.each(data, function (key, value) {
                
                table.row("#" + box_Codigo).remove().draw(false);
                SalidaDetalle = null;
                var SalidaDetalle = {
                    prod_Codigo: value.prod_Codigo,
                    sald_Cantidad: value.boxd_Cantidad,
                    box_Codigo: value.box_Codigo,
                    sald_UsuarioCrea: contador
                };
                $.ajax({
                    url: "/Salida/RemoveSalidaDetalle",
                    method: "POST",
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ SalidaDetalle: SalidaDetalle }),
                });

                //var row = oTable.find('tr:contains("' + value.replace("En Caja: ", "") + '")').eq(9);
                //oTable.fnDeleteRow(row[0]);
                //table
                //.column(8)
                //.data()
                //.each(function (value, index) {
                //    //var box_Codigo = value.replace("En Caja: ", "")
                   
                //    //$("#tblSalidaDetalle").find('tr:contains("' + value.replace("En Caja: ", "") + '")').remove();
                //    //$("#tblSalidaDetalle").find('tr:contains("' + value.replace("En Caja: ", "") + '")').remove();
                //    //table.row($(this).parents('tr(' + index + ')')).remove().draw();
                //    //value.replace("En Caja: ", "");
                //})
                
              
               
                //table
                //    .row($(this).parents('tr td:eq'))
                //    .remove()
                //    .draw();

                //$.ajax({
                //    url: "/Salida/RemoveSalidaDetalle",
                //    method: "POST",
                //    dataType: 'json',
                //    contentType: "application/json; charset=utf-8",
                //    data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle }),
                //});
              
            })

        }
    })
})

    //$.data.each(function () {
    //    var SalidaDetalle = {
    //        prod_Codigo: $('#prod_Codigo').val(),
    //        sald_Cantidad: $('#sald_Cantidad').val(),
    //        sald_UsuarioCrea: contador
    //    };
    //    $.ajax({
    //        url: "/Salida/SaveSalidaDetalle",
    //        method: "POST",
    //        dataType: 'json',
    //        contentType: "application/json; charset=utf-8",
    //        data: JSON.stringify({ SalidaDetalle: SalidaDetalle, data_producto: data_producto }),
    //    }).done(function (datos) {
    //    })
    //})

// Copiar y Pegar///
$(document).ready(function () {
    $('#prod_CodigoBarras').bind("cut copy paste", function (e) {
        e.preventDefault();
    });
    $('#sal_RazonDevolucion').bind("cut copy paste", function (e) {
        e.preventDefault();
    });
});
function check(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }
    // Patron de entrada, en este caso solo acepta numeros y letras
    patron = /[A-Za-z0-9]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
}

function filterFloat(evt, input) {
    // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
    var key = window.Event ? evt.which : evt.keyCode;
    var chark = String.fromCharCode(key);
    var tempValue = input.value + chark;
    if (key >= 48 && key <= 57) {
        if (filter(tempValue) === false) {
            return false;
        } else {
            return true;
        }
    } else {
        if (key == 8 || key == 13 || key == 0) {
            return true;
        } else if (key == 46) {
            if (filter(tempValue) === false) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
}
function filter(__val__) {
    var preg = /^([0-9]+\.?[0-9]{0,2})$/;
    if (preg.test(__val__) === true) {
        return true;
    } else {
        return false;
    }

}

//function fBodega() {
//    var e = document.getElementById("bod_Id");
//    var vBodega = e.options[e.selectedIndex].text;
//    return vBodega
//};

function TipoSalida() {
    var e = document.getElementById("tsal_Id");
    var vTipoSalida = e.options[e.selectedIndex].text;
    return false;
};

$(document).ready(function () {
    $('#tbSalidaDetalle').DataTable(
        {
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
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            }
        });
});

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
            var prod_CodigoTabla = currentRow.find("td:eq(0)").text();
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

function GetSalidaDetalle() {
    var SalidaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        sald_Cantidad: $('#sald_Cantidad').val(),
        box_Codigo: vbox_Codigo,
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}
$(document).on("click", "#Datatable tbody tr td button#SeleccionarFactura", function () {
    var fact_Codigo = this.value;

    console.log(fact_Codigo);
    $('#fact_Codigo').val(fact_Codigo);
    FaturaExist()
})
function CantidadExiste(Cod_Producto, CantidadAceptada) {
    if ($('#tblSalidaDetalle >tbody >tr').length > 0) {
        $('#tblSalidaDetalle >tbody >tr').each(function () {
            var prod_CodigoTabla = $(this).find("td:eq(0)").text()
            if (Cod_Producto == prod_CodigoTabla) {
                var CantidadTabla = $(this).find("td:eq(7)").text();
                var CantidadExit = parseFloat(CantidadAceptada) - parseFloat(CantidadTabla);

                return CantidadExit
            }
            else {
                return 0
            }
        })
    }
}
//$('#sal_BodDestino').click(function () {
//    var Bod = document.getElementById("bod_Id");
//    var Bodega = Bod.options[Bod.selectedIndex].text;
//    if (Bodega.startsWith("Seleccione")) {
//        $('#bod_IdError').text('');
//        $('#validationbod_Id').after('<ul id="bod_IdError" class="validation-summary-errors text-danger">Seleccione una Bodega</ul>');
//    }
//    else {
//        $('#bod_IdError').text('');

//    }
//})

$('#AgregarSalidaDetalle').click(function () {
    if (StillAjax) {

    }
    else {
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
        $('#Error_Barras').text('');
        $('#validationprod_CodigoBarras').after('<ul id="ValidationCodigoBarrasCreateError" class="validation-summary-errors text-danger">Campo de Barras Requerido</ul>');
        vCodBarra_Producto = false;
    }
    if (Cantidad == "") {
        $('#MessageError').text('');
        $('#sald_CantidadExedError').text('');
        $('#sald_CantidadError').text('');
        $('#Error_Barras').text('');
        $('#sald_Cantidad').after('<ul id="sald_CantidadError" class="validation-summary-errors text-danger">Cantidad Requerida</ul>');
        vCantidad = false;
    }
    if (!vCantidad || !vCodBarra_Producto) {
        return false;
    }
    else {
        $('#ValidationCodigoBarrasCreateError').text('');
        $('#Error_Barras').text('');
        $('#sald_CantidadError').text('');
        StillAjax = true; //BOOLEANO
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
                    StillAjax = false;
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

    }
    


});
function SeleccionProductoTabla() {
    console.log(cantfisica_anterior)
    $("#tblSalidaDetalle td").each(function () {
        var prueba = $(this).text()
        if (prueba == data_producto) {
            var idcontador = $(this).closest('tr').data('id');
            var cantfisica_anterior = $(this).closest("tr").find("td:eq(7)").text();
            console.log(cantfisica_anterior)
        }
    })
}

$('#prod_Codigo').click(function () {
    SeleccionProductoTabla()
})