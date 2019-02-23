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

function GetProdCodBar() {
    var Producto = {
        prod_CodigoBarras: $('#prod_Codigo').val(),
        bod_Id: $('#sald_Cantidad').val(),
        sald_UsuarioCrea: contador
    };
    return Producto;
}

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

$('#fact_Codigo').change(function () {
    FaturaExist()
})

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
            $('#Table_BuscarProductoD').append(key);
        })
    });
}
function LimpiarTable() { $("#Body_BuscarProducto").removeData() }

//$(document).ready(function () {
//    console.log("ready")
//    ListaProductos()
//});
$("#bod_Id").change(function () {
    LimpiarTable();
    BodegaDestino()
});

$('#Productos').click(function () {
    ListaProductos()
});

//$.when(LimpiarTable()).then(function () {

//    $(document).ready(function () {
//        console.log("ready")
//        ListaProductos()
//    });
//});

function TipodeSalida() {
    //$('#submit').attr('value = "SICambio"')
    $('#fact_Codigo').val('***-***-**-********');
    var TipoSal = $("#tsal_Id").val()
    if (TipoSal == "1") {
        $('#fact_Codigo').val('***-***-**-********');
        $('#sal_RazonDevolucion').val('*****');
        
        $("#Prestamo").css("display", "block");

        $("#VentaoDevolucion").css("display", "none");
        BodegaDestino()
    }
    else {
        $("#Prestamo").css("display", "none");
        $("#Devolucion").css("display", "none");
        if (TipoSal == 2) {
            $('#fact_Codigo').val('');
            $('#sal_RazonDevolucion').val('*****');

            $("#sal_BodDestino").empty();
            $("#VentaoDevolucion").css("display", "block");
            $("#TitleVenta").css("display", "block");

            $('#div_sal_RazonDevolucion').css("display", "none");
            $("#TitleDevolucion").css("display", "none");

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

                $("#TitleVenta").css("display", "none");

            }
            else {
                $("#VentaoDevolucion").css("display", "none");

                $("#Prestamo").css("display", "none");

            }

        }

    }

    FaturaExist()
}

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
$(document).ready(function () {
    var e = document.getElementById("tsal_Id");
    var strUser = e.options[e.selectedIndex].text;
    console.log(strUser)
    $("#tbTipoSalida_tsal_Id").val(strUser)

});
//Tipo de Salida

$(document).ready(function () {
    TipodeSalida()
});
$("#tsal_Id").change(function () {
    TipodeSalida()
});

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




    //Anular
    function GetAnularSalida() {
        var Salida = {
            sal_Id: $('#sal_Id').val(),
            sal_RazonAnulada: $('#sal_RazonAnulada').val(),
            sald_UsuarioCrea: contador
        };
        return Salida;
    }

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