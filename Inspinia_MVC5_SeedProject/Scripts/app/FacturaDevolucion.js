//Busqueda de Cliente en Devolucion-----------
$(document).ready(function () {
    var $rows = $('#BodyFactura tr');
    $("#searchFactura").keyup(function () {
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

    })
});

//Seleccinar factura devolucion----------------------
$(document).on("click", "#DevFactura tbody tr td button#AgregarFactura", function () {
    idFactitem = $(this).closest('tr').data('idfact');
    CodigoItem = $(this).closest('tr').data('codigo');
    $("#tbFactura_fact_Codigo").val(CodigoItem);
    $("#fact_Id").val(idFactitem);
    console.log('facturaid1', idFactitem)
    $('#ModalAgregarFactura').modal('hide');
    $(document).ready(function () {
        if (idItem != '') {
            document.getElementById("btnProducto").disabled = false;
            document.getElementById("prod_Codigo").disabled = false;
            GetIDFactura(idFactitem);
        }
    });
})



//Filtro de Modal Producto----------------------------------------------------------------------------
var FacturaID = $('#fact_Id').val();
GetIDFactura(FacturaID);
console.log('facturaid', FacturaID)
function GetIDFactura(FacturaID, idFactitem) {
    $.ajax({
        url: "/Devolucion/FiltrarModalProducto",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ FacturaID: FacturaID }),

        error: function () {
            //alert("No se puede filtrar");
        },
        success: function (list) {
           

            $('#BodyProducto').empty();
            $.each(list, function (key, val) {
                contador = contador + 1;
                copiar = "<tr data-idCont=" + contador + " data-id=" + val.CodigoProducto + " data-desc=" + val.Descripcion + " data-valor=" + val.PrecioUnitario + " data-impuesto=" + val.PorcentajeImpu + " data-porcentaje=" + val.PorcentajeDesc + " data-cantfacturada=" + val.CantidadFacturada + ">";
                copiar += "<td id = 'idItem'>" + val.Descripcion + "</td>";
                copiar += "<td id = 'b'>" + val.CantidadFacturada + "</td>";
                copiar += "<td id = 'DescItem'>" + val.PorcentajeDesc + "</td>";
                copiar += "<td id = 'ClienteItem'>" + val.PrecioUnitario + "</td>";
                copiar += "<td>" + '<button id="Agregar" class="btn btn-primary btn-xs" type="button">Añadir</button>' + "</td>";
                copiar += "</tr>";
                $('#BodyProducto').append(copiar);
//Validacion que el producto ingresado exista en la factura a devolver-----------------------------------------------------
                $("#prod_Codigo").blur(function (list) {
                    var cod = document.getElementById("id");
                    validacion = document.getElementById('smsProducto');
                    var CodProductoIngresado = $('#prod_Codigo').val();
                    if (CodProductoIngresado != val.CodigoProducto) {
                        validacion.innerText = "Codigo de Producto Incorrecto";
                    }
                    else {
                        console.log("else")
                        validacion.innerText = "";
                    }
                });

                $("#prod_Codigo").blur(function (data) {
                    var cod = document.getElementById("id");
                    validacion = document.getElementById('smsProducto');
                    var CodProductoIngresado = $('#prod_Codigo').val();
                    if (CodProductoIngresado != val.CodigoProducto) {
                        validacion.innerText = "Codigo de Producto Incorrecto";
                    }
                    else {
                        console.log("else")
                        validacion.innerText = "";
                        data_Codproducto = val.CodigoProducto;
                        data_Descripcion = val.Descripcion;
                        data_CatFacturada = val.CantidadFacturada;
                        data_PorcentajeDesc = val.PorcentajeDesc;
                        data_PUnitario = val.PrecioUnitario;
                        console.log(data_Codproducto)
                        $('#prod_Codigo').val(data_producto);
                        $('#pcat_Id').val(data_categoria);
                        $('#pscat_Id').val(data_subcate);
                        $('#uni_Id').val(data_unidad);
                        $('#prod_Descripcion').val(data_pDescripcion)
                        }
                });
            });

            console.log(list);
        }

    });

}


//Validacion de cantidad de producto devuelto
$("#devd_CantidadProducto").blur(function () {
    valido = document.getElementById('smsCantidad');
    var CantFacturada = $('#CantidadFacturada').val();
    var CantDevolucion = $('#devd_CantidadProducto').val();
    var CodProducto = $('#prod_Codigo').val();

    if (parseFloat(CantFacturada) < parseFloat(CantDevolucion)) {
        console.log("facturada", CantFacturada)
        console.log(CantDevolucion)
        console.log("if")
        valido.innerText = "El valor debe ser menor a la cantidad facturada";
    }
    else {
        console.log("else")
        valido.innerText = "";
    }
});

//Devolucion Seleccionar Producto
$(document).on("click", "#DataTable1 tbody tr td button#Agregar", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    DescValor = $(this).closest('tr').data('valor');
    PorcentajeItem = $(this).closest('tr').data('porcentaje');
    CantidadItem = $(this).closest('tr').data('cantfacturada');
    ImpuestoItem = $(this).closest('tr').data('impuesto');
    $("#prod_Codigo").val(idItem);
    $("#tbProducto_prod_Descripcion").val(DescItem);
    $("#PrecioUnitario").val(DescValor);
    $("#Descuento").val(PorcentajeItem);
    $("#CantidadFacturada").val(CantidadItem);
    $("#Impuesto").val(ImpuestoItem);
    $('#ModalBuscarProducto').modal('hide');
});

//Devolucion Seleccionar Producto
$(document).on("click", "#DataTable1 tbody tr td button#Agregar", function () {
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    DescValor = $(this).closest('tr').data('valor');
    PorcentajeItem = $(this).closest('tr').data('porcentaje');
    CantidadItem = $(this).closest('tr').data('cantfacturada');
    ImpuestoItem = $(this).closest('tr').data('impuesto');
    $("#prod_Codigo").val(idItem);
    $("#DescripcionProducto").val(DescItem);
    $("#PrecioUnit").val(DescValor);
    $("#PorDescuento").val(PorcentajeItem);
    $("#CantidadFacturada").val(CantidadItem);
    $("#ImpuestoD").val(ImpuestoItem);
    $('#ModalAgregarProducto').modal('hide');
});

