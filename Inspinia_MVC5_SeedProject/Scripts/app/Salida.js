$('#fact_Codigo').change(function () {
    FaturaExist()
})
$('#fact_Codigo').keypress(function () {
    FaturaExist()
})
//Tipo de Salida
$("#bod_Id").click(function () {
    var Bod = document.getElementById("bod_Id");
    var Bodega = Bod.options[Bod.selectedIndex].text;
    if (Bodega.startsWith("Seleccione")) {
        console.log(Bodega);
    } else {
        ChangeBodega();
    }
})

$("#bod_Id").change(function () {
    LimpiarTable();
    BodegaDestino();
});

$("#sal_FechaElaboracion").click(function () {
});

$('#Productos').click(function () {
    //data - toggle="modal" data - target="#ModalAgregarProducto"
    var Bod = document.getElementById("bod_Id");
    var Bodega = Bod.options[Bod.selectedIndex].text;
    if (Bodega.startsWith("Seleccione")) {
        $('#bod_IdError').text('');
        $('#validationbod_Id').after('<ul id="bod_IdError" class="validation-summary-errors text-danger">Seleccione una Bodega</ul>');
        $('#bod_Id').focus();
    }
    else {
        $('#ModalAgregarProducto').modal('show');
        $('#bod_IdError').text('')
        ListaProductos()
        //test()
    }
    //var Bodega = $("#bod_Id").val();
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

$(document).ready(function () {
});

//function __showUsers() {
//}

function ListaProductos() {
    var bod_Id = $('#bod_Id').val()
    url = "/Salida/GetProducto?bod_Id=" + bod_Id;
    $('#ModalAgregarProducto').modal('show');

    var table = $('#tblBusquedaGenerica').dataTable({
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
            { "data": "pcat_Nombre" },
            { "data": "pscat_Descripcion" },
            { "data": "uni_Descripcion" },
            { "data": "prod_CodigoBarras" },
            { "defaultContent": "<button class='btn btn-primary btn-xs'  id='seleccionar' data-dismiss='modal'>Seleccionar</button>" }
        ],
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
    })
    //$('#tblBusquedaGenerica tbody').on('click', 'button', function () {
    //    var data = table.row($(this).parents('tr')).data();
    //    alert(data[0]);
    //});
}
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

$("#Editar").click(function () {
    $("#tblSalidaDetalle >tbody >tr").each(function () {
        //document.getElementById("myDiv");  find("td:eq(0)").
        //var elem =
        //var elem =
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
    var Bodega = fBodega();
    if (Bodega.startsWith("Seleccione")) {
        console.log("Si")
    }
    else {
        console.log("No")
    }
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

    //$("#Body_BuscarProducto").remove()
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
    $.ajax({
        url: "/Salida/FacturaExist",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ fact_Codigo: $('#fact_Codigo').val() }),
    })
        .done(function (data) {
            if (data.startsWith("Factura")) {
                $('#CodigoError').text('');
                $('#validationFactura').after('<ul id="CodigoError" class="Factura" >' + data + '</ul>');
            }
            else {
                $('#CodigoError').text('');
                $('#validationFactura').after('<ul id="CodigoError" class="validation-summary-errors text-danger">' + data + '</ul>');
            }
        })
}

function BodegaDestino() {
    $.ajax({
        method: "POST",
        url: "/Salida/BodegaDestino",
        contentType: "application/json; charset=utf-8",
        dataType: 'json', data: JSON.stringify({ id: $('#bod_Id').val() }),
    }).done(function (data) {
        $("#sal_BodDestino").empty();
        $("#sal_BodDestino").append("<option placeholder='Seleccione una Bodega de Destino'>Seleccione una Bodega de Destino</option>")
        $.each(data, function (index, row) {
            $("#sal_BodDestino").append("<option value ='" + row.bod_Id + "'>" + row.bod_Nombre + "</option>")
        });
    })
};

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
//            key += "<td>" + "<button class='btn btn-primary btn-xs' value=" + value.prod_Codigo + " id='seleccionar' data-dismiss='modal'>Seleccionar</button>" + "</td>"
//            key += "</tr>";
//            $("#Body_BuscarProducto").append(key)
//        })
//    });
//}

function TipodeSalida() {
    var txtTipoSalida = document.getElementById("tsal_Id");
    var valTipoSalida = txtTipoSalida.options[txtTipoSalida.selectedIndex].text;

    var TipoSal = $("#tsal_Id").val()
    if (valTipoSalida == "Prestamo") {
        $('#fact_Codigo').val('***-***-**-********');
        $('#tbFactura_fact_Codigo').val('***-***-**-********');

        $('#sal_RazonDevolucion').val('*****');
        ///////////////////////////////////////
        $("#Prestamo").css("display", "block");
        ///////////////////////////////////////
        $("#VentaoDevolucion").css("display", "none");
        BodegaDestino()
    }
    else {
        $("#Prestamo").css("display", "none");
        $("#Devolucion").css("display", "none");
        if (valTipoSalida == "Venta") {
            var fact_Codigo = $('#fact_Codigo').val();
            if (fact_Codigo == '***-***-**-********') {
                $('#fact_Codigo').val('') 
            }
            $('#sal_RazonDevolucion').val('*****');
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
    FaturaExist()
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

function Producto(bod_Id, prod_CodigoBarrasItem) {
    $("#prod_CodigoBarras").val();
    var cod_Barras = $("#prod_Codigo").val();
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
            })
            $("#ModalAgregarProducto").on('hidden.bs.modal', function () {
                $('#prod_CodigoBarras').text('');
                $('#sald_Cantidad').text('');
                $("#sald_Cantidad").focus();
            });
        }
        else {
            $('#Error_Barras').text('');
            $('#validationprod_CodigoBarras').after('<ul id="Error_Barras" class="validation-summary-errors text-danger">*Producto no existe</ul>');

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
            $("#sald_Cantidad").focus();
        }
    });
    return false;
}

$(document).on("click", "#tblBusquedaGenerica tbody tr td button#seleccionar", function () {

    var currentRow = $(this).closest("tr");
    var prod_CodigoBarrasItem = currentRow.find("td:eq(5)").text();
    var bod_Id = $('#bod_Id').val()
    $('#CodigoError').text('')
    
    //var bod_Id = $(this).closest('tr').data('cod_barra');
    //var prod_CodigoBarrasItem = $(this).closest('tr').data('cod_barra');
    console.log(prod_CodigoBarrasItem)
    Producto(bod_Id, prod_CodigoBarrasItem)
});

$(document).keypress(function (e) {
    console.log('Hola', e.target.id);
    var IDInput = e.target.id;
    if (e.which == 13) {
        if (IDInput == 'prod_CodigoBarras') {
            var bod_Id = $('#bod_Id').val()
            var prod_CodigoBarras = $('#prod_CodigoBarras').val()
            Producto(bod_Id, prod_CodigoBarras);
            return false;
        }
        else
            return false;
    }
});
$(document).on("click", "#tblSalidaDetalle tbody tr td button#removeSalidaDetalle", function () {
    //$(this).closest('tr').remove();
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
    });
});

var table = $('#example').DataTable();

$('#example tbody').on('click', 'img.icon-delete', function () {
   
})