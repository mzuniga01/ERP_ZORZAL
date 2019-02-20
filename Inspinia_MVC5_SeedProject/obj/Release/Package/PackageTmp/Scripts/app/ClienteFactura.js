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
    console.log(DiasCredito)
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

                console.log(data)
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

