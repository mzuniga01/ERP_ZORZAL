//Consumidor Final
$("#consumidorFinal").change(function () {
    if (this.checked) {   
        $("#cliente_Identificacionxx").val('99999999999999');
        $("#cliente_Nombresxx").val('CONSUMIDOR FINAL');
        valido = document.getElementById('label_identificacion');
        valido.innerText = "RTN/Identificación";
        document.getElementById("fact_AlCredito").disabled = true;
        document.getElementById("fact_AutorizarDescuento").disabled = true;
    }
    else {
        $("#cliente_Nombresxx").val('');
        $("#cliente_Identificacionxx").val('');
        $("#clte_Id").val('');
        document.getElementById("MostrarTerceraEdad").disabled = false;
        $("#MostrarTerceraEdad").prop("checked", false);
        $("#fact_AutorizarDescuento").prop("checked", false);
        $('#Cred2').hide();
        ///Aqui tengo que borrar variables de session
        document.getElementById("fact_AlCredito").disabled = false;
        document.getElementById("fact_AutorizarDescuento").disabled = false;
    }
})

$("#consumidorFinal").change(function () {
    if (this.checked) {
        $('#ConsuFinal').modal('show');
    }
    else {

        $('#ConsuFinal').modal('hide');
    }
});
$('#AgregarConsumidorFinal').click(function () {

    var DatoConsumidorFinal = GetConsumidorFinal();
    $.ajax({
        url: "/Factura/ConsumidorFinal",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ConsumidorFinal: DatoConsumidorFinal }),
    })
    .done(function (data) {
        //Input
        $('#confi_Nombres').val();
        $('#confi_Telefono').val();
        $('#confi_Correo').val();
        $('#ConsuFinal').modal('hide');
    });
});
function GetConsumidorFinal() {

    var ConsumidorFinal = {
        fact_Id: $('#fact_Id').val(),
        confi_Nombres: $('#confi_Nombres').val(),
        confi_Telefono: $('#confi_Telefono').val(),
        confi_Correo: $('#confi_Correo').val()

    }
    return ConsumidorFinal
};

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
                        $('#factd_PorcentajeDescuento').val(val.par_PorcentajeDescuentoTE);
                        $('#factd_MontoDescuento').val(val.par_PorcentajeDescuentoTE);
                    });

                });
            }
            document.getElementById("MostrarTerceraEdad").disabled = true;
            document.getElementById("fact_AutorizarDescuento").disabled = true;
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
$("#fact_NombresTE").change(function () {
    var str = $("#fact_NombresTE").val();
    var res = str.toUpperCase();
    $("#fact_NombresTE").val(res);
});

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

function format(input) {
    $(input).change(function () {
        var str = $(input).val();
        var res = str.toUpperCase();
        $(input).val(res);
    });
    $(input).on("keypress", function () {
        $input = $(this);
        setTimeout(function () {
            $input.val($input.val().toUpperCase());
        }, 0);
    })
}


$("#fact_IdentidadTE")[0].maxLength = 13;
//Validacion de numeros//
function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
}


$("#confi_Telefono").on("keypress keyup blur", function (event) {
    var Telefono = $(this).val();
    if (Telefono == '') {
        $(this).val('+');
    }
    this.value = this.value.replace(/[a-záéíóúüñ#/=]+/ig, "");
});



$("#confi_Correo").blur(function () {
    campo = event.target;
    valido = document.getElementById('emailOK1');

    var reg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    var regOficial = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (reg.test(campo.value) && regOficial.test(campo.value)) {
        valido.innerText = "";
    } else if (reg.test(campo.value)) {
        valido.innerText = "";

    } else {
        valido.innerText = "Direccion de Correo Electronico Incorrecta";

    }
});

//Autorizar Descuento en Productos
function ValidarAutorizacion() {
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
        if (data==true) {
           
               var Porcentaje = $("#PorcentajeDescuento").val();
               $("#factd_PorcentajeDescuento").val(Porcentaje);
               $('#AutorizarDescuentoModal').modal('hide');
        }
        else
        {
            valido = document.getElementById('mensajerror');
            valido.innerText = "Usuario o contraseña incorrectos";
        }
    });
}

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
        if (data == true) {

            var Porcentaje = $("#PorcentajeDescuentoDetalle").val();
            $("#factd_PorcentajeDescuento").val(Porcentaje);
            $('#AutorizarDescuentoDetalle').modal('hide');
        }
        else {
            valido = document.getElementById('mensajerrorDetalle');
            valido.innerText = "Usuario o contraseña incorrectos";
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
    var currentRow = $(this).closest("tr");
    var prod_CodigoBarrasItem = currentRow.find("td:eq(2)").text();
    var bod_Id = $('#bod_Id').val()
    SeleccionProducto(prod_CodigoBarrasItem)
    idItem = $(this).closest('tr').data('id');
    DescItem = $(this).closest('tr').data('desc');
    ISVItem = $(this).closest('tr').data('isv');
    $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
    $("#prod_Codigo").val(idItem);
    $("#tbProducto_prod_Descripcion").val(DescItem);
    $("#factd_Impuesto").val(ISVItem);
    $('#ModalAgregarProducto').modal('hide');
});
//Factura Buscar Cliente
$(document).ready(function () {
    var $rows = $('#ClienteTbody tr');
    $("#searchCliente").keyup(function () {
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
    })
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

$("#cliente_Identificacionx").on("keypress keyup blur", function (event) {
    var Identificacion = $('#cliente_Identificacionxx').val();
    if (Identificacion == '') {
        $('#TerceraEdad').show();
        $('#Alcredito').show();
        $('#cliente_Nombresxx').val('');
        document.getElementById("MostrarTerceraEdad").disabled = false;
        $("#fact_AlCredito").prop("checked", false);

        document.getElementById("fact_AutorizarDescuento").disabled = false;
        $("#MostrarTerceraEdad").prop("checked", false);
        $("#fact_AutorizarDescuento").prop("checked", false);
    }
})

$('#consumidorFinal').change(function () {
    //GetParametro

    if (this.checked) {
        $('#ocultar').hide();
        $('#Alcredito').hide();
        $('#AutorizarD').hide();
        $('#TerceraEdad').show();
        $("#fact_AlCredito").prop("checked", false);
        $('#fact_DiasCredit').val(0);
        $('#Cred1').hide();
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
                    $('#clte_Id').val(val.par_IdConsumidorFinal);
                });

            });
        }

    }
    else {
        document.getElementById("cliente_Identificacionxx").disabled = false;
        $('#ocultar').show();
        $('#Alcredito').show();
        $('#clte_Id').val('');
        $('#AutorizarD').show();
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
    if (x == '') {
        $('#fact_DiasCredito').val(0);
    }
    else {
        $('#fact_DiasCredito').val('');
    }
});

$("#fact_AutorizarDescuento").click(function () {
    var x = $('#fact_PorcentajeDescuento').val();
    if (x == '') {
        $('#fact_PorcentajeDescuento').val(0);
    }
    else {

        $('#fact_PorcentajeDescuento').val('');
    }
});
//Numeracion CIA y Numero de Factura
$(document).ready(function () {
    GetCaja();
})
function GetNumeroFact(CodSucursal, CodCaja) {
    $.ajax({
        url: "/Factura/GetNumeroFact",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodSucursal: CodSucursal, CodCaja: CodCaja }),
    })
    .done(function (data) {
        if (data.length > 0) {
            var Mensaje = data;
            if (Mensaje == -1) {
                alert("Fecha limite de Emisión Vencida")
                var url = $("#RedirectTo").val();
                location.href = url;
            } else if (Mensaje == -2) {
                alert("El Numero CAI no tiene numeracion")
                var url = $("#RedirectTo").val();
                location.href = url;
            }
            else {
                $('#pemi_NumeroCAI').val(data[0]['CAI']);
                $('#fact_Codigo').val(data[0]['CODFACTURA']);
            }

        }
    });
}

//Recuperar Caja,CAI y Numero de Factura
function GetCaja() {
    var CodUsuario = $("#usu_Id").val();
    $.ajax({
        url: "/Factura/GetCaja",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodUsuario: CodUsuario }),
    })
    .done(function (data) {
        if (data.length > 0) {
            $.each(data, function (key, val) {
                $("#cja_Id").val(val.cja_Id);
                $("#cja_Descripcion").val(val.cja_Descripcion);

                var Suc = $("#suc_Id").val();
                GetNumeroFact(Suc, val.cja_Id)

            });
        }
        else {
            $('#alertaCaja').show();
        }
    });
}
//Calulos Facturas
$(document).ready(function () {
    $("#factd_Cantidad")[0].maxLength = 7;
    $("#factd_MontoDescuento")[0].maxLength = 12;
})

$(function () {

    $("#factd_Cantidad").keyup(function (e) {

        var Cantidad = $("#factd_Cantidad").val(),
            Precio = $("#factd_PrecioUnitario").val(),
            Impuesto = $("#factd_Impuesto").val(),
            Subtotal = $("#SubtotalProducto").val(),
            result = "";
        result1 = "";

        if (Cantidad && Precio > 0) {
            result += Cantidad * Precio;
        }

        $("#SubtotalProducto").val(result);
        PorcentajeImpuesto = ((parseFloat(Impuesto) / 100) * Subtotal);

        result1 += PorcentajeImpuesto;

        $("#Impuesto").val(result1);
        var Descuento = $("#factd_PorcentajeDescuento").val();
        var Subtotal = $("#SubtotalProducto").val();
        var impuesto = $("#factd_Impuesto").val();
        var PorcentajeDescuento = (parseFloat(Descuento) / 100);
        var PorcentajeImpuesto = (parseFloat(impuesto) / 100);
        var DescuentoTotal = (parseFloat(Subtotal) * parseFloat(PorcentajeDescuento));
        var impuestotal = (Subtotal * PorcentajeImpuesto);
        result = "";

        if (PorcentajeDescuento && Cantidad == '') {
            result += (parseFloat(Subtotal) + parseFloat(impuestotal));
        }
        else if (DescuentoTotal && Cantidad == 0) {
            result += (parseFloat(Subtotal) + parseFloat(impuestotal));
        }
        else {
            result += (parseFloat(Subtotal) - parseFloat(DescuentoTotal) + parseFloat(impuestotal));
        }
        if (Descuento == '') {
            $("#factd_PorcentajeDescuento").val(0);
        }
        if (Cantidad == '') {
            $("#factd_MontoDescuento").val('');
        }
        else {
            $("#factd_MontoDescuento").val(DescuentoTotal);
        }
        if (Descuento && Cantidad == '') {
            $("#TotalProducto").val('');
        }
        else {
            $("#TotalProducto").val(result);
        }

        if ($("#factd_Cantidad").val(), $("#factd_PrecioUnitario").val() == '') {
            $("#factd_MontoDescuento").val('');
            $("#TotalProducto").val('');
        }
    });

});

//Muestra un mensaje de Si hay productos en exitencias.
$("#factd_Cantidad").on("blur", function (event) {
    GetCantidad();
});

function GetCantidad() {
    var CodSucursal = $('#suc_Id').val();
    var CodProducto = $('#prod_Codigo').val();
    var CantidadIngresada = $('#factd_Cantidad').val();
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
                    var CANTIDAD = parseFloat(can)
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



