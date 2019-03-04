var contador = 0;

$('#AgregarDetalleFactura').click(function () {
    var CodigoProducto = $('#prod_Codigo').val();
    var PorcentajeDescuento = $('#factd_PorcentajeDescuento').val();
    var MontoDescuento = $('#factd_MontoDescuento').val();
    var DescripcionProducto = $('#tbProducto_prod_Descripcion').val();
    var CantidadProducto = $('#factd_Cantidad').val();
    var Subtotal = $('#SubtotalProducto').val();
    var PrecioUnitario = $('#factd_PrecioUnitario').val();
    var Impuesto = $('#factd_Impuesto').val();
    var Total = $('#TotalProducto').val();

    if (CodigoProducto == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationCodigoProductoCreate').after('<ul id="ErrorCodigoProductoCreate" class="validation-summary-errors text-danger">Campo Código Producto requerido</ul>');
    }
    else if (MontoDescuento == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationMontoDescuentoCreate').after('<ul id="ErrorMontoDescuentoCreate" class="validation-summary-errors text-danger">Campo Monto Descuento requerido</ul>');
    }
    else if (CantidadProducto == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationCantidadProductoCreate').after('<ul id="ErrorCantidadCreate" class="validation-summary-errors text-danger">Campo Cantidad requerido</ul>');
    }
    else if (Impuesto == '') {
        $('#ErrorCodigoProductoCreate').text('');
        $('#ErrorMontoDescuentoCreate').text('');
        $('#ErrorCantidadCreate').text('');
        $('#ErrorImpuestoCreate').text('');
        $('#validationImpuestoProductoCreate').after('<ul id="ErrorImpuestoCreate" class="validation-summary-errors text-danger">Campo Impuesto requerido</ul>');
    }
    else {
        //ajax para el controlador
        var FacturaDetalleEdit = GetFacturaDetalleEdit();
        $.ajax({
            url: "/Factura/SaveFacturaDetalleEdit",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ FacturaDetalleEdit: FacturaDetalleEdit, data_producto: CodigoProducto })
        })
            .done(function (datos) {
                if (datos == CodigoProducto) {
                    //alert('Es Igual.')
                    console.log('Repetido');
                    var cantfisica_nueva = $('#factd_Cantidad').val();
                    $("#tblDetalleFactura td").each(function () {
                        var prueba = $(this).text()
                        if (prueba == CodigoProducto) {
                            var idcontador = $(this).closest('tr').data('id');
                            var cantfisica_anterior = $(this).closest("tr").find("td:eq(2)").text();
                            var sumacantidades = parseInt(cantfisica_nueva) + parseInt(cantfisica_anterior);
                            console.log(sumacantidades);
                            $(this).closest('tr').remove();
                            copiar = "<tr data-id=" + idcontador + ">";
                            copiar += "<td id = 'prod_CodigoCreate'>" + CodigoProducto + "</td>";
                            copiar += "<td id = 'tbProducto_prod_DescripcionCreate'>" + DescripcionProducto + "</td>";
                            copiar += "<td id = 'factd_CantidadCreate' align='right'>" + sumacantidades + "</td>";
                            copiar += "<td id = 'Precio_UnitarioCreate' align='right'>" + PrecioUnitario + "</td>";
                            copiar += "<td id = 'ImpuestoCreate' align='right'>" + Impuesto + "</td>";
                            copiar += "<td id = 'factd_MontoDescuentoCreate' align='right'>" + MontoDescuento + "</td>";
                            copiar += "<td id = 'TotalProductoCreate' align='right'>" + Total + "</td>";
                            copiar += "<td>" + '<button id="removeFacturaDetalle" class="btn btn-danger glyphicon glyphicon-trash btn-xs eliminar" type="button"></button>' + "</td>";
                            copiar += "</tr>";
                            $('#tblDetalleFactura').append(copiar);
                        }
                    });
                } else {
                    //alert('NO ES IGUAL')
                    //Rellenar la tabla 
                    contador = contador + 1;
                    copiar = "<tr data-id=" + contador + ">";
                    copiar += "<td id = 'prod_CodigoCreate'>" + CodigoProducto + "</td>";
                    copiar += "<td id = 'tbProducto_prod_DescripcionCreate'>" + DescripcionProducto + "</td>";
                    copiar += "<td id = 'factd_CantidadCreate' align='right'>" + CantidadProducto + "</td>";
                    copiar += "<td id = 'Precio_UnitarioCreate' align='right'>" + PrecioUnitario + "</td>";
                    copiar += "<td id = 'ImpuestoCreate' align='right'>" + Impuesto + "</td>";
                    copiar += "<td id = 'factd_MontoDescuentoCreate' align='right'>" + MontoDescuento + "</td>";
                    copiar += "<td id = 'TotalProductoCreate' align='right'>" + Total + "</td>";
                    copiar += "<td>" + '<button id="removeFacturaDetalleEdit" class="btn btn-danger glyphicon glyphicon-trash btn-xs eliminar" type="button"></button>' + "</td>";
                    copiar += "</tr>";
                    $('#tblDetalleFactura').append(copiar);
                }

                //Descuento 
                var Descuento = $('#factd_MontoDescuento').val();
                var TotalDescuento = parseFloat(document.getElementById("TotalDescuento").innerHTML);

                if (document.getElementById("TotalDescuento").innerHTML == '') {
                    totalProducto = $('#factd_MontoDescuento').val();
                    document.getElementById("TotalDescuento").innerHTML = parseFloat(totalProducto);
                }
                else {
                    document.getElementById("TotalDescuento").innerHTML = parseFloat(TotalDescuento) + parseFloat(Descuento);
                }

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
                //Impuesto
                var Cantidad = CantidadProducto
                var Precio = PrecioUnitario
                var impuesto = parseFloat(document.getElementById("factd_Impuesto").value.replace(',', '.'));
                var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
                var porcentaje = parseFloat(impuesto / 100);
                var impuestos = (Cantidad * Precio) * porcentaje;
                console.log(impuestos)

                if (document.getElementById("isv").innerHTML == '') {
                    impuesto = document.getElementById("factd_Impuesto").value;
                    document.getElementById("isv").innerHTML = parseFloat(impuestos);
                }
                else {
                    document.getElementById("isv").innerHTML = parseFloat(impuestotal) + parseFloat(impuestos);
                }

                //Grantotal
                if (document.getElementById("total").innerHTML == '') {
                    var TotalEncabezado = document.getElementById("total").innerHTML = parseFloat(totalProducto) + parseFloat(impuestos) - parseFloat(Descuento);
                    $("#TotalProductoEncabezado").val(TotalEncabezado);
                }
                else {
                    var TotalEncabezado = document.getElementById("total").innerHTML = parseFloat(subtotal) + parseFloat(totalProducto) + parseFloat(impuestotal) + parseFloat(impuestos) - parseFloat(TotalDescuento) - parseFloat(Descuento);
                    $("#TotalProductoEncabezado").val(TotalEncabezado);
                }
            })
        .done(function (data) {
            $('#ErrorCodigoProductoCreate').text('');
            $('#ErrorMontoDescuentoCreate').text('');
            $('#ErrorCantidadCreate').text('');
            $('#ErrorImpuestoCreate').text('');
            //Input
            $('#prod_CodigoBarras').val('');
            $('#prod_Codigo').val('');
            $('#factd_MontoDescuento').val('');
            $('#Impuesto').val('');
            $('#tbProducto_prod_Descripcion').val('');
            $('#factd_Cantidad').val('');
            $('#SubtotalProducto').val('');
            $('#factd_PrecioUnitario').val('');
            $('#factd_Impuesto').val('');
            $('#TotalProducto').val('');
        })
    }
})

function GetFacturaDetalleEdit() {

    var FacturaDetalleEdit = {
        prod_Codigo: $('#prod_Codigo').val(),
        factd_PorcentajeDescuento: $('#factd_PorcentajeDescuento').val(),
        factd_MontoDescuento: $('#factd_MontoDescuento').val(),
        tbProducto_prod_Descripcion: $('#tbProducto_prod_Descripcion').val(),
        factd_Cantidad: $('#factd_Cantidad').val(),
        SubtotalProducto: $('#SubtotalProducto').val(),
        factd_PrecioUnitario: $('#factd_PrecioUnitario').val(),
        factd_Impuesto: $('#factd_Impuesto').val(),
        TotalProducto: $('#TotalProducto').val(),
        factd_Id: contador
    }
    return FacturaDetalleEdit
};

$(document).on("click", "#tblDetalleFactura tbody tr td button#removeFacturaDetalleEdit", function () {

    //Descuento
    var Descuento = $(this).parents("tr").find("td")[5].innerHTML;
    var TotalDescuento = parseFloat(document.getElementById("TotalDescuento").innerHTML);
    document.getElementById("TotalDescuento").innerHTML = parseFloat(TotalDescuento) - parseFloat(Descuento);

    //Subtotal
    var Cantidad = $(this).parents("tr").find("td")[2].innerHTML;
    var Precio = $(this).parents("tr").find("td")[3].innerHTML;
    var SubtotalProducto = Cantidad * Precio;
    var subtotal = parseFloat(document.getElementById("Subtotal").innerHTML);
    document.getElementById("Subtotal").innerHTML = parseFloat(subtotal) - parseFloat(SubtotalProducto);

    //Impuesto
    var impuesto = $(this).parents("tr").find("td")[4].innerHTML;
    var impuestotal = parseFloat(document.getElementById("isv").innerHTML);
    var porcentaje = parseFloat(impuesto.replace(',', '.') / 100);
    var impuestos = (SubtotalProducto * porcentaje);
    document.getElementById("isv").innerHTML = parseFloat(impuestotal) - parseFloat(impuestos);

    //GranTotal
    var TotalEncabezado = document.getElementById("total").innerHTML = (parseFloat(subtotal) - parseFloat(SubtotalProducto)) + (parseFloat(impuestotal) - parseFloat(impuestos));
    $("#TotalProductoEncabezado").val(TotalEncabezado);

    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var FacturaDetalle = {
        factd_Id: idItem,
    };

    $.ajax({
        url: "/Factura/RemoveFacturaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ FacturaDetalleC: FacturaDetalle }),
    });
});

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
}

function pierdeFoco(e) {
    var valor = e.value.replace(/^0*/, '');
    e.value = valor;
}

function ponerdecimales(numero) {
    if (numero.indexOf(".") == -1) { numero += ".00" } else {
        if (numero.indexOf(".") == numero.length - 2) { numero += "0" }
    }
    return numero;
}

function validar(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-z0-9A-Z\-]+$/.test(tecla);
}

//Show Tabla Detalle Factura
$(document).ready(function () {
    GetDetalle()
});
var contador = 0;
function GetDetalle() {
    var factID = $("#fact_Id").val();
    $.ajax({
        url: "/Factura/GetFacturaDetalleD",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ factID: factID }),
    })
      .done(function (data) {
          $.each(data, function (key, val) {
              Descuento = (val.factd_MontoDescuento * val.factd_Cantidad);
              ImpuestoTotal = ((val.factd_Impuesto / 100) * val.factd_PrecioUnitario) * val.factd_Cantidad;
              Subtotal = ((val.factd_Cantidad * val.factd_PrecioUnitario) + ImpuestoTotal);
              GranSubtotal = (val.factd_Cantidad * val.factd_PrecioUnitario);
              Total = (GranSubtotal + ImpuestoTotal) - Descuento;
              StudentId = val.factd_Id;
              contador = contador + 1;
              copiar = "<tr data-id=" + StudentId + ">";
              copiar += "<td id = 'prod_CodigoCreate'>" + val.prod_Codigo + "</td>";
              copiar += "<td id = 'tbProducto_prod_DescripcionCreate'>" + val.prod_Descripcion + "</td>";
              copiar += "<td id = 'factd_CantidadCreate' align='right'>" + val.factd_Cantidad + "</td>";
              copiar += "<td id = 'Precio_UnitarioCreate' align='right'>" + val.factd_PrecioUnitario + "</td>";
              copiar += "<td id = 'ImpuestoCreate' align='right'>" + val.factd_Impuesto + "</td>";
              copiar += "<td id = 'factd_MontoDescuentoCreate' align='right'>" + val.factd_MontoDescuento + "</td>";
              copiar += "<td id = 'TotalProductoCreate' align='right'>" + Subtotal + "</td>";
              copiar += "<td>" + "<a href='#' onclick='EditStudentRecord(" + StudentId + ")' ><span class='btn btn-warning glyphicon glyphicon-edit btn-xs'></span></a>" + "</td>";
              copiar += "</tr>";
              $('#tblDetalleFactura').append(copiar);
              total_col1 = 0
              SubtotalD = 0;
              GranImpuesto = 0;
              GranTotal = 0;
              $("#tblDetalleFactura tbody tr").each(function (index) {
                  DescuentoDD = $(this).children("td:eq(5)").html();
                  Cantidad = $(this).children("td:eq(2)").html();
                  ImpuestoD = $(this).children("td:eq(4)").html();
                  ValorUnitario = $(this).children("td:eq(3)").html();
                  PorcentajeImpuesto = parseFloat(ImpuestoD / 100);
                  if (ValorUnitario != '') {
                      total_col1 += parseFloat($(this).find('td').eq(5).text());
                      ValorUnitario = parseFloat(ValorUnitario);
                      SubtotalD += Cantidad * ValorUnitario;
                      GranImpuesto += (Cantidad * ValorUnitario) * PorcentajeImpuesto;
                      GranTotal += Cantidad * ValorUnitario + (Cantidad * ValorUnitario) * PorcentajeImpuesto - DescuentoDD;
                  }
              });
              document.getElementById("TotalDescuento").innerHTML = parseFloat(total_col1);
              document.getElementById("Subtotal").innerHTML = parseFloat(SubtotalD);
              document.getElementById("isv").innerHTML = parseFloat(GranImpuesto);
              document.getElementById("total").innerHTML = parseFloat(GranTotal);
          });

      })

}
//Show Modal Detalle Factura
function EditStudentRecord(StudentId) {
    var url = "/Factura/GetDetalleEdit?StudentId=" + StudentId;
    $("#FacturaDetalleEdit").modal();
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            $.each(data, function (key, arn) {

                var fechaString = arn.factd_FechaCrea.substr(6);
                var fechaActual = new Date(parseInt(fechaString));
                var mes = fechaActual.getMonth() + 1;
                var dia = fechaActual.getDate();
                var anio = fechaActual.getFullYear();
                var FechaCrea = dia + "/" + mes + "/" + anio;

                $("#factdd").val(arn.factd_Id);
                $("#IDProducto").val(arn.prod_Codigo);
                $("#DescProducto").val(arn.prod_Descripcion);
                $("#MontoDescuentoEdit").val(arn.factd_MontoDescuento);
                $("#CantidadEdit").val(arn.factd_Cantidad);
                $("#ImpuestoEdit").val(arn.factd_Impuesto);
                $("#PrecioUnitarioEdit").val(arn.factd_PrecioUnitario);
                $("#UsuCrea").val(arn.factd_UsuarioCrea);
                $("#FechaCrea").val(FechaCrea);
                var Cantidad = document.getElementById("CantidadEdit").value;
                var Precio = document.getElementById("PrecioUnitarioEdit").value;
                var Descuento = document.getElementById("MontoDescuentoEdit").value;
                var Impuesto = document.getElementById("ImpuestoEdit").value;
                var PorcentajeImpuesto = (Impuesto / 100);
                var ImpuestoTotal = (Cantidad * Precio) * PorcentajeImpuesto;
                result = "";
                result1 = "";
                if (Cantidad && Precio > 0) {
                    result += Cantidad * Precio;
                    result1 += ((Cantidad * Precio) + ImpuestoTotal - Descuento)
                }
                $("#SubtotalEdit").val(result);
                $("#TotalEdit").val(result1);
            })
        }
    })
}

//Editar Show Detalle Factura
$("#EditFacturaDetalle").click(function () {
    var factd_Ids = $('#factd_Id').val();
    var data = $("#SubmitForm").serializeArray();
    console.log(data)
    $.ajax({
        type: "POST",
        url: "/Factura/UpdateFacturaDetalle",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
        }
    });
    location.reload(true);
})


//Anular Factura
$(document).ready(function () {
    var Estado = $('#fact_EsAnulada').val(true);
    document.getElementById("esfac_Id").disabled = true;
    if (Estado == true) {
        $('#bottonAnular').hide();
        document.getElementById("esfac_Id").disabled = true;
        document.getElementById("fechafacturaEdit").disabled = true;
        document.getElementById("clte_Identificacion").disabled = true;
        document.getElementById("fact_AlCredito").disabled = true;
        document.getElementById("clte_Nombres").disabled = true;
        document.getElementById("fact_AutorizarDescuento").disabled = true;
        document.getElementById("btnSave").disabled = true;
        document.getElementById("AddProducto").disabled = true;
    }
});


$('#Anular').click(function () {
    var CodFactura = $('#fact_Id').val();
    var FacturaAnulado = 1
    var RazonAnulado = $('#razonAnular').val();
    if (RazonAnulado == "") {
        valido = document.getElementById('Mensaje');
        valido.innerText = "La razón Anular es requerida";
    } else {
        $.ajax({
            url: "/Factura/AnularFactura",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodFactura: CodFactura, FacturaAnulado: FacturaAnulado, RazonAnulado: RazonAnulado }),

        })
            .done(function (data) {
                if (data.length > 0) {
                    var url = $("#RedirectTo").val();
                    location.href = url;
                }
                else {
                    alert("Registro No Actualizado");
                }
            });
    }

})
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}

//Edit View CheckBox
$("#fact_AlCredito").change(function () {
    if (this.checked) {
        $('#Credito').show();
    }
    else {

        $('#Credito').hide();
    }
});

$("#fact_AlCredito").ready(function () {
    if (this.checked) {
        $('#Credito').show();
    }
    else {

        $('#Credito').hide();
    }
});

$("#fact_AutorizarDescuento").change(function () {
    if (this.checked) {
        $('#Credito2').show();
    }
    else {

        $('#Credito2').hide();
    }
});

$("#fact_AutorizarDescuento").ready(function () {
    if (this.checked) {
        $('#Credito2').show();
    }
    else {

        $('#Credito2').hide();
    }
});

$(document).ready(function () {
    if (fact_AlCredito.checked) {
        $('#Credito').show();
    } else {
        $('#Credito').hide();
    }
});

$(document).ready(function () {
    if (fact_AutorizarDescuento.checked) {
        $('#Credito2').show();
    } else {
        $('#Credito2').hide();
    }
});


//Create View CheckBox
$("#fact_AlCredito").change(function () {
    if (this.checked) {
        $('#Cred1').show();
    }
    else {

        $('#Cred1').hide();
    }
});

$("#fact_AlCredito").ready(function () {
    if (this.checked) {
        $('#Cred1').show();
    }
    else {

        $('#Cred1').hide();
    }
});

$("#fact_AutorizarDescuento").change(function () {
    if (this.checked) {
        $('#Cred2').show();
    }
    else {

        $('#Cred2').hide();
    }
});

$("#fact_AutorizarDescuento").ready(function () {
    if (this.checked) {
        $('#Cred2').show();
    }
    else {

        $('#Cred2').hide();
    }
});


// Default Value
$(document).ready(function () {
    var isChecked = document.getElementById('fact_AlCredito').checked;
    if (isChecked == false) {
        $('#fact_DiasCredito').val(0);
    }
    else {
    }
});

$(document).change(function () {
    var isChecked = document.getElementById('fact_AlCredito').checked;
    if (isChecked == false) {
        $('#fact_DiasCredito').val(0);
    }
    else {
    }
});

$(document).ready(function () {
    var isChecked = document.getElementById('fact_AutorizarDescuento').checked;
    if (isChecked == false) {
        $('#fact_PorcentajeDescuento').val(0);
    }
    else {
    }
});

$(document).change(function () {
    var isChecked = document.getElementById('fact_AutorizarDescuento').checked;
    if (isChecked == false) {
        $('#fact_PorcentajeDescuento').val(0);
    }
    else {
    }
});

$("#fact_AlCredito").click(function () {
    var x = $('#fact_DiasCredito').val();
    console.log(x);
    if (x == '') {
        $('#fact_DiasCredito').val(0);
    }
    else {
        $('#fact_DiasCredito').val('');
    }
});

$("#fact_AutorizarDescuento").click(function () {
    var x = $('#fact_PorcentajeDescuento').val();
    console.log(x);
    if (x == '') {
        $('#fact_PorcentajeDescuento').val(0);
    }
    else {

        $('#fact_PorcentajeDescuento').val('');
    }
});
//Muestra un mensaje de Si hay productos en exitencias.
$("#factd_Cantidad").on("blur", function (event) {
    GetCantidad();
});

function GetCantidad() {
    var CodSucursal = $('#suc_Id').val();
    console.log(CodSucursal)
    var CodProducto = $('#prod_Codigo').val();
    console.log(CodProducto)
    var CantidadIngresada = $('#factd_Cantidad').val();
    console.log(CantidadIngresada)


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
                console.log(MENSAJE)

                if (MENSAJE) {
                    var can = data[0]['CANTIDAD'];
                    var CANTIDAD = parseFloat(can)
                    console.log(CANTIDAD)
                    if (CANTIDAD < CantidadIngresada) {
                        alert('La cantidad de productos no esta disponible, Cantidad disponible: ' + CANTIDAD)
                        $('#factd_Impuesto').val(0.00);
                        $('#factd_Cantidad').val(''),
                        $("#SubtotalProducto").val(0.00),
                        $("#factd_PorcentajeDescuento").val(0.00),
                        $("#TotalProducto").val(0.00)
                        $('#Impuesto').val(0.00);
                        $('#factd_Cantidad').val('');
                    }
                    else if (CANTIDAD == 10) {
                        alert('Pocos productos en exitencia, cantidad existente: ' + CANTIDAD)
                    } else {
                        document.getElementById("AgregarDetalleFactura").disabled = false;
                    }

                }
                else {
                    var can = data[0]['CANTIDAD'];
                    var CANTIDAD = parseFloat(can)
                    alert('No hay productos en existencia')
                    $('#factd_Cantidad').val('');
                    $('#factd_MontoDescuento').val('');
                    $('#factd_Cantidad').val('');
                    $('#SubtotalProducto').val('');
                    $('#Impuesto').val('');
                    $('#TotalProducto').val('');
                }
            });
        }
        else {

        }
    });
}
//Factura Buscar Producto
$(document).ready(function () {
    var $rows = $('#ProductoTbody tr');
    $("#search").keyup(function () {
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

// Factura Seleccionar Producto
$(document).on("click", "#tbProductoFactura tbody tr td button#seleccionar", function () {
    idbarraItem = $(this).closest('tr').data('barra');
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    ISVItem = $(this).closest('tr').data('isv');
    $("#prod_CodigoBarras").val(idbarraItem);
    $("#prod_Codigo").val(idItem);
    $("#tbProducto_prod_CodigoBarras").val(idCbItem);
    $("#tbProducto_prod_Descripcion").val(DescItem);
    $("#factd_Impuesto").val(ISVItem);
    $('#ModalAgregarProducto').modal('hide');
});

//Facturar RowSeleccionar Producto
$(document).ready(function () {
    var table = $('#tbProductoFactura').DataTable();

    $('#tbProductoFactura tbody').on('click', 'tr', function () {
        idbarraItem = $(this).closest('tr').data('barra');
        idItem = $(this).closest('tr').data('id');
        DescItem = $(this).closest('tr').data('desc');
        ISVItem = $(this).closest('tr').data('isv');
        $("#prod_CodigoBarras").val(idbarraItem);
        $("#prod_Codigo").val(idItem);
        $("#tbProducto_prod_Descripcion").val(DescItem);
        $("#factd_Impuesto").val(ISVItem);
        $('#ModalAgregarProducto').modal('hide');
        var Cliente = $('#clte_Id').val();
        if (Cliente == '') {
            Cliente = 0;
            GetPrecio(Cliente, idItem);
        }
        else {
            GetPrecio(Cliente, idItem);
        }

        function GetPrecio(Cliente, idItem) {
            $.ajax({
                url: "/Factura/GetPrecio",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ Cliente: Cliente, idItem: idItem }),
            })
            .done(function (data) {
                var g = data;
                $("#factd_PrecioUnitario").val(g);
            });
        }
    });
});

$(function () {
    $("#CantidadEdit").keyup(function (e) {
        var Cantidad = document.getElementById("CantidadEdit").value;
        var Precio = document.getElementById("PrecioUnitarioEdit").value;
        var Descuento = document.getElementById("MontoDescuentoEdit").value;
        var Impuesto = document.getElementById("ImpuestoEdit").value;
        var PorcentajeImpuesto = (Impuesto / 100);
        var ImpuestoTotal = (Cantidad * Precio) * PorcentajeImpuesto;
        result = "";
        result1 = "";
        if (Cantidad && Precio > 0) {
            result += Cantidad * Precio;
            result1 += ((Cantidad * Precio) + ImpuestoTotal - Descuento)
        }
        $("#SubtotalEdit").val(result);
        $("#TotalEdit").val(result1);
    });
});
