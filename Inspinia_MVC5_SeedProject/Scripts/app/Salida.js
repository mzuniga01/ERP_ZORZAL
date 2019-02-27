

$('#fact_Codigo').change(function () {
    FaturaExist()
})

//Tipo de Salida
$("#bod_Id").click(function () {
    ChangeBodega();
})

$("#bod_Id").change(function () {
        LimpiarTable();
        BodegaDestino();
    });

$('#Productos').click(function () {
    ListaProductos()
});

$(document).ready(function () {
    TipodeSalida()
});
$("#tsal_Id").change(function () {
    TipodeSalida()
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

$(function () {
    $("#sal_FechaElaboracion").datepicker({
        dateFormat: 'yy-mm-dd',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
    }).datepicker("setDate", new Date());
});

function FaturaExist() {

    $.ajax({
        url: "/Salida/FacturaExist",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ fact_Codigo: $('#fact_Codigo').val() }),
    })
        .done(function (data) {
            $('#CodigoError').text('');
            $('#validationFactura').after('<ul id="CodigoError" class="validation-summary-errors text-danger">' + data + '</ul>');
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

function ListaProductos() {
    var vbod_Id = $('#bod_Id').val()
    var BodegaDetalle = {
        bod_Id: vbod_Id
    };
    $.ajax({
        url: "/Salida/GetProdList",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ tbBodegaDetalle: BodegaDetalle }),
    }).done(function (data) {
        $("#Body_BuscarProducto").html("");
        $.each(data, function (key, value) {
            key = "<tr tr data-id =" + value.prod_Codigo + " , tr data-content =" + value.prod_Descripcion + " tr data-container =" + value.pscat_Descripcion + " , tr data-keyboard =" + value.uni_Descripcion + " , tr data-pcat=" + value.pcat_Nombre + " , tr data-cod_Barra=" + value.prod_CodigoBarras + " >";
            key += "<td id ='prod_Codigo' >" + value.prod_Codigo + "</td>";
            key += "<td id ='prod_Descripcion' >" + value.prod_Descripcion + "</td>";
            key += "<td id = 'pcat_Nombre'>" + value.pcat_Nombre + "</td>";
            key += "<td id = 'pscat_Descripcion'>" + value.pscat_Descripcion + "</td>";
            key += "<td id = 'uni_Descripcion'>" + value.uni_Descripcion + "</td>";
            key += "<td id = 'prod_CodigoBarras'>" + value.prod_CodigoBarras + "</td>";
            key += "<td>" + "<button class='btn btn-primary btn-xs' value=" + value.prod_Codigo + " id='seleccionar' data-dismiss='modal'>Seleccionar</button>" + "</td>"
            key += "</tr>";
            $("#Body_BuscarProducto").append(key)

        })
    });
}



function TipodeSalida() {
    $('#fact_Codigo').val('***-***-**-********');
    var TipoSal = $("#tsal_Id").val()
    if (TipoSal == "1") {
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
        if (TipoSal == 2) {
            $('#fact_Codigo').val('');
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

            if (TipoSal == "3") {
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
    console.log(strUser)
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