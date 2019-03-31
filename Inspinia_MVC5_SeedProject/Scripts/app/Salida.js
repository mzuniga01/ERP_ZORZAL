


//// Show an element
//var show = function (elem) {

//    // Get the natural height of the element
//    var getHeight = function () {
//        elem.style.display = 'block'; // Make it visible
//        var height = elem.scrollHeight + 'px'; // Get it's height
//        elem.style.display = ''; //  Hide it again
//        return height;
//    };

//    var height = getHeight(); // Get the natural height
//    elem.classList.add('is-visible'); // Make the element visible
//    elem.style.height = height; // Update the max-height

//    // Once the transition is complete, remove the inline max-height so the content can scale responsively
//    window.setTimeout(function () {
//        elem.style.height = '';
//    }, 350);

//};

//// Hide an element
//var hide = function (elem) {

//    // Give the element a height to change from
//    elem.style.height = elem.scrollHeight + 'px';

//    // Set the height back to 0
//    window.setTimeout(function () {
//        elem.style.height = '0';
//    }, 1);

//    // When the transition is complete, hide it
//    window.setTimeout(function () {
//        elem.classList.remove('is-visible');
//    }, 350);

//};

//// Toggle element visibility
//var toggle = function (elem, timing) {

//    // If the element is visible, hide it
//    if (elem.classList.contains('is-visible')) {
//        hide(elem);
//        return;
//    }

//    // Otherwise, show it
//    show(elem);

//};

//// Listen for click events
//document.addEventListener('click', function (event) {

//    // Make sure clicked element is our toggle
//    if (!event.target.classList.contains('#tsal_Id')) return;

//    // Prevent default link behavior
//    event.preventDefault();

//    // Get the content
//    var content = document.querySelector(event.target.hash);
//    if (!content) return;

//    // Toggle the content
//    toggle(content);

//}, false);














$('#fact_Codigo').change(function () {
    FaturaExist()
})
$('#fact_Codigo').keypress(function () {
    FaturaExist()
})

//Tipo de Salida
//$("#bod_Id").click(function () {
//    var Bod = document.getElementById("bod_Id");
//    var Bodega = Bod.options[Bod.selectedIndex].text;
//    if (Bodega.startsWith("Seleccione")) {
//        console.log(Bodega);
//    } else {
//        ChangeBodega();
//    }
//})

//$("#bod_Id").change(function () {
//    LimpiarTable();
//    BodegaDestino();
//});

$("#sal_FechaElaboracion").click(function () {
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

//$('#Productos').click(function () {
//    var Bod = document.getElementById("bod_Id");
//    var Bodega = Bod.options[Bod.selectedIndex].text;
//    if (Bodega.startsWith("Seleccione")) {
//        $('#bod_IdError').text('');
//        $('#validationbod_Id').after('<ul id="bod_IdError" class="validation-summary-errors text-danger">Seleccione una Bodega</ul>');
//        $('#bod_Id').focus();
//    }
//    else {
//        $('#ModalAgregarProducto').modal('show');
//        $('#bod_IdError').text('')
//        ListaProductos()
//    }
//});

//function ListaProductos() {
//    var bod_Id = $('#bod_Id').val()
//    url = "/Salida/GetProducto?bod_Id=" + bod_Id;
//    $('#ModalAgregarProducto').modal('show');

//    var table = $('#tblBusquedaGenerica').dataTable({
//        destroy: true,
//        resposive: true,
//        ajax: {
//            method: "POST",
//            url: url,
//            contentType: "application/json; charset=utf-8",
//            dataType: 'json',
//            "dataSrc": ""
//        },
//        "columns": [
//            { "data": "prod_Codigo" },
//            { "data": "prod_Descripcion" },
//            { "data": "pcat_Nombre" },
//            { "data": "pscat_Descripcion" },
//            { "data": "uni_Descripcion" },
//            { "data": "prod_CodigoBarras" },
//            { "defaultContent": "<button class='btn btn-primary btn-xs'  id='seleccionar' data-dismiss='modal'>Seleccionar</button>" }
//        ],
//        "oLanguage": {
//            "oPaginate": {
//                "sNext": "Siguiente",
//                "sPrevious": "Anterior",
//            },
//            "sProcessing": "Procesando...",
//            "sLengthMenu": "Mostrar _MENU_ registros",
//            "sZeroRecords": "No se encontraron resultados",
//            "sEmptyTable": "No hay registros",
//            "sInfoEmpty": "Mostrando 0 de 0 Entradas",
//            "sSearch": "Buscar",
//            "sInfo": "Mostrando _START_ a _END_ Entradas",
//            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
//        }
//    })
//}

$("#Editar").click(function () {
    $("#tblSalidaDetalle >tbody >tr").each(function () {
        console.log($(this).data("id"))
        console.log($(this).val(this.dataset))

        console.log($(this).data())
    })
});

$(document).ready(function () {
    TipodeSalida()
});
$("#tsal_Id").change(function () {
    TipodeSalida();
    //var Bodega = fBodega();
    //if (Bodega.startsWith("Seleccione")) {
    //    console.log("Si")
    //}
    //else {
    //    console.log("No")
    //}
});

$("#ChangeBodega").change(function () {
    $('#ModalChangeBodega').modal('dispose')
    window.showState = function (str) {
        var dropdown = document.getElementById('bod_Id');
        var event = document.createEvent('MouseEvents');
        event.initMouseEvent('mousedown', true, true, window);
        dropdown.dispatchEvent(event);
    }
});

function LimpiarTable() {
    $("#Body_BuscarProducto").html("");
}

function ChangeBodega() {
    var tblSalidaDetalle = $('#tblSalidaDetalle >tbody >tr').length;
    if (tblSalidaDetalle > 0) {
        $('#ModalChangeBodega').modal('show')
    }
}

function GetProdCodBar() {
    var Producto = {
        prod_CodigoBarras: $('#prod_Codigo').val(),
        bod_Id: $('#sald_Cantidad').val(),
        sald_UsuarioCrea: contador
    };
    return Producto;
}

//Anular
function GetAnularSalida() {
    var Salida = {
        sal_Id: $('#sal_Id').val(),
        sal_RazonAnulada: $('#sal_RazonAnulada').val(),
        sald_UsuarioCrea: contador
    };
    return Salida;
}

function FaturaExist() {
    $('#FacturaCodigoError').text('');
    $('#CodigoError').text('');
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

//function BodegaDestino() {
//    $.ajax({
//        method: "POST",
//        url: "/Salida/BodegaDestino",
//        contentType: "application/json; charset=utf-8",
//        dataType: 'json', data: JSON.stringify({ id: $('#bod_Id').val() }),
//    }).done(function (data) {
//        $("#sal_BodDestino").empty();
//        $("#sal_BodDestino").append("<option placeholder='Seleccione una Bodega de Destino'>Seleccione una Bodega de Destino</option>")
//        $.each(data, function (index, row) {
//            $("#sal_BodDestino").append("<option value ='" + row.bod_Id + "'>" + row.bod_Nombre + "</option>")
//        });
//    })
//};
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
    if (valTipoSalida == "PRESTAMO" || valTipoSalida== "PRESTAMOS" || valTipoSalida == "TRANSLADOS") {
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
        if (valTipoSalida == "VENTA"|| valTipoSalida == "VENTAS") {
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

            if (valTipoSalida == "DEVOLUCION" || valTipoSalida == "DEVOLUCIONES") {
                $('#fact_Codigo').val('');
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

$(document).ready(function () {
    var e = document.getElementById("tsal_Id");
    var strUser = e.options[e.selectedIndex].text;
    $("#tbTipoSalida_tsal_Id").val(strUser)
});

$('#btnAnularSalida').click(function () {
    var sal_Id = $('#sal_Id').val();
    var sal_RazonAnulada = $('#sal_RazonAnulada').val();

    if (sal_RazonAnulada == '' || sal_RazonAnulada == '*****') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#sal_RazonAnulada').after('<ul id="MessageError" class="validation-summary-errors text-danger">Campo Requerido</ul>');
    }

    else {
        var tbSalida = GetAnularSalida();
        $.ajax({
            url: "/Salida/Anular",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Salida: tbSalida }),
        })
            .done(function (data) {
                window.location.href = "/Salida/Index"
            });
    }
});

$('#prod_CodigoBarras').click(function () {
    $("#prod_CodigoBarras").removeAttr("readonly");
    $("#prod_CodigoBarras").val('');
    $('#prod_Codigo').val('');
    $('#prod_Descripcion').val('');
    $("#uni_Id").val('');
    $("#pscat_Id").val('');
    $("#pcat_Id").val('');
    $("#prod_Marca").val('');
    $("#prod_Modelo").val('');
    $("#prod_Talla").val('');
    $("#prod_Color").val('');
    $('#sald_Cantidad').val('');
    $("#CantidaExistenteProd").text('');

})

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

$(document).on("click", "#tblBusquedaGenerica tbody tr td button#seleccionar", function () {
    //var currentRow = $(this).closest("tr");
    //var prod_CodigoBarrasItem = currentRow.find("td:eq(8)").text();
    var prod_CodigoBarrasItem = this.value;
    var bod_Id = $('#bod_Id').val()
    $("#prod_CodigoBarras").attr("readonly","true")
    //$('#CodigoError').text('')
    $('#sald_CantidadExedError').text('')
    $('#ValidationCodigoBarrasCreateError').text('')
    $('#sald_CantidadError').text('')
    console.log(prod_CodigoBarrasItem)
    Producto(bod_Id, prod_CodigoBarrasItem)
});

$(document).keypress(function (e) {
    //ValidacionCantidad()

    console.log('Hola', e.target.id);
    var IDInput = e.target.id;
    if (e.which == 13) {
        if (IDInput == 'prod_CodigoBarras') {

            var bod_Id = $('#bod_Id').val()
            var prod_CodigoBarras = $('#prod_CodigoBarras').val()
            if (prod_CodigoBarras == '') {
                $('#ValidationCodigoBarrasCreateError').text('');
                $('#Error_Barras').text('');
                $('#validationprod_CodigoBarras').after('<ul id="ValidationCodigoBarrasCreateError" class="validation-summary-errors text-danger">Codigo de Barras Requerido</ul>');
            }
            else {
                $('#ValidationCodigoBarrasCreateError').text('');
                $('#Error_Barras').text('');
                $('#sald_CantidadError').text('');
                Producto(bod_Id, prod_CodigoBarras);
            }
            // var Productos = $('#prod_Codigo').val();
            //if (prod_CodigoBarras != '') {
            //    $('#sald_Cantidad').focus();

            //}
                return false;
        }
        if (IDInput == 'sald_Cantidad') {
            document.getElementById('AgregarSalidaDetalle').click();
            return false;
        }
    } else {
        if (IDInput == 'sald_Cantidad' || IDInput == 'prod_CodigoBarras' || 'sal_RazonDevolucion') {
        }
        else
            return false;
    }
   

});
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

//var table = $('#example').DataTable();

//$('#example tbody').on('click', 'img.icon-delete', function () {
//})







//$('#AgregarSalidaDetalle').click(function () {
//    if (StillAjax) {

//    }
//    else {

//    }
//    var table = $('#tblSalidaDetalle').DataTable();
//    var counter = 0;
//    var bod_Id = $('#bod_Id').val();
//    var Cod_Producto = $('#prod_Codigo').val();
//    var Producto = $('#prod_Descripcion').val();
//    var Unidad_Medida = $('#pscat_Id').val();
//    var Cantidad = $('#sald_Cantidad').val();
//    var data_producto = $("#prod_Codigo").val();
//    var CodBarra_Producto = $('#prod_CodigoBarras').val();
//    $('#sald_CantidadError').text();
//    var vCodBarra_Producto = true;
//    var vCantidad = true;

//    if (CodBarra_Producto == "") {
//        $('#ValidationCodigoBarrasCreateError').text('');
//        $('#Error_Barras').text('');
//        $('#validationprod_CodigoBarras').after('<ul id="ValidationCodigoBarrasCreateError" class="validation-summary-errors text-danger">Campo de Barras Requerido</ul>');
//        vCodBarra_Producto = false;
//    }
//    if (Cantidad == "") {
//        $('#MessageError').text('');
//        $('#sald_CantidadExedError').text('');
//        $('#sald_CantidadError').text('');
//        $('#Error_Barras').text('');
//        $('#sald_Cantidad').after('<ul id="sald_CantidadError" class="validation-summary-errors text-danger">Cantidad Requerida</ul>');
//        vCantidad = false;
//    }
//    if (!vCantidad || !vCodBarra_Producto) {
//        return false;
//    }
//    else {
//        $('#ValidationCodigoBarrasCreateError').text('');
//        $('#Error_Barras').text('');
//        $('#sald_CantidadError').text('');
//        ProductoCantidad(bod_Id, Cod_Producto).done(function (data) {
//            var CantidadAceptada = data.CantidadAceptada
//            var CantidadMinima = data.CantidadMinima
//            if ($('#tblSalidaDetalle >tbody >tr').length > 0) {
//                $('#tblSalidaDetalle >tbody >tr').each(function () {
//                    var prod_CodigoTabla = $(this).find("td:eq(0)").text()
//                    if (Cod_Producto == prod_CodigoTabla) {
//                        var CantidadTabla = $(this).find("td:eq(7)").text();
//                        CantidadExit = parseFloat(CantidadAceptada) - parseFloat(CantidadTabla);
//                    }
//                    else {
//                        CantidadExit = CantidadAceptada
//                    }
//                })
//            }
//            else {
//                CantidadExit = CantidadAceptada
//            }
//            console.log(CantidadExit)
//            if (Producto == '') {
//                $('#MessageError').text('');
//                $('#CodigoError').text('');
//                $('#ValidationCodigoCreateError').text('');
//                $('#ValidationCodigoCreate').after('<ul id="ValidationCodigoCreateError" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
//            }
//            else if (Cantidad == '' || Cantidad < 0.25) {
//                $('#MessageError').text('');
//                $('#sald_CantidadExedError').text('');
//                $('#sald_CantidadError').text('');
//                $('#sald_Cantidad').after('<ul id="sald_CantidadError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
//            }
//            else if (Cantidad > CantidadExit) {
//                $('#MessageError').text('');
//                $('#sald_CantidadError').text('');
//                $('#sald_CantidadExedError').text('');
//                $('#sald_Cantidad').after('<ul id="sald_CantidadExedError" class="validation-summary-errors text-danger">Cantidad Superada</ul>');
//            }
//            else {
//                $('#ValidationCodigoCreateError').text('');
//                $('#sald_CantidadError').text('');
//                $('#sald_CantidadExedError').text('');
//                $('#CantidaExistenteProd').text('');
//                var tbSalidaDetalle = GetSalidaDetalle();
//                $.ajax({
//                    url: "/Salida/SaveSalidaDetalle",
//                    method: "POST",
//                    dataType: 'json',
//                    contentType: "application/json; charset=utf-8",
//                    data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle, data_producto: data_producto }),
//                }).done(function (datos) {
//                    $("#prod_CodigoBarras").removeAttr("readonly");
//                    if (datos == data_producto) {
//                        console.log('Repetido');
//                        var cantfisica_nueva = $('#sald_Cantidad').val();
//                        $("#tblSalidaDetalle td").each(function () {
//                            var prueba = $(this).text()
//                            if (prueba == data_producto) {
//                                table.row($(this).parents('tr')).remove().draw();
//                                var idcontador = $(this).closest('tr').data('id');
//                                var cantfisica_anterior = $(this).closest("tr").find("td:eq(7)").text();
//                                var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);
//                                contador = contador + 1
//                                table.row.add([
//                                    $('#prod_Codigo').val(),
//                                    $('#prod_Descripcion').val(),
//                                    $('#prod_Marca').val(),
//                                    $('#prod_Modelo').val(),
//                                    $('#prod_Talla').val(),
//                                    $('#pcat_Id').val(),
//                                    $('#uni_Id').val(),
//                                    sumacantidades,
//                                    '<button id = "removeSalidaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Quitar</button>',
//                                    contador
//                                ]).draw(false);
//                            }
//                        });
//                    } else {
//                        contador = contador + 1
//                        table.row.add([
//                            $('#prod_Codigo').val(),
//                            $('#prod_Descripcion').val(),
//                            $('#prod_Marca').val(),
//                            $('#prod_Modelo').val(),
//                            $('#prod_Talla').val(),
//                            $('#pcat_Id').val(),
//                            $('#uni_Id').val(),
//                            $('#sald_Cantidad').val(),
//                            '<button id = "removeSalidaDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Quitar</button>',
//                            contador
//                        ]).draw(false);
//                    }
//                }).done(function (data) {
//                    $('#prod_Codigo').val('');
//                    $('#prod_Descripcion').val('');
//                    $('#pscat_Id').val('');
//                    $('#uni_Id').val('');
//                    $('#pcat_Id').val('');
//                    $("#prod_CodigoBarras").val('');
//                    $('#sald_Cantidad').val('');
//                    $('#Error_Barras').text('');
//                    $('#NombreError').text('');
//                    $('#sald_CantidadError').text('');
//                    $('#CantidaExistenteProd').text('');
//                    $('#prod_CodigoBarras').focus();
//                    console.log('Hola');
//                });
//            }
//        });
//    }
//});