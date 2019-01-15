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
    idItem = $(this).closest('tr').data('id');
    rtnItem = $(this).closest('tr').data('rtn');
    nombreItem = $(this).closest('tr').data('nombrecliente');
    tpid = $(this).closest('tr').data('tpi');
    Fecha = $(this).closest('tr').data('fecha');
    Persona1 = $(this).parents("tr").find("td")[5].innerHTML;
    LabelIdentificacion = $(this).parents("tr").find("td")[3].innerHTML;
    valido = document.getElementById('label_identificacion');
    document.getElementById('label_identificacion').innerHTML = LabelIdentificacion + '<span style="color:red"> *</span>';
    nuevaCadena = Persona1.trim();
    $("#clte_Id").val(idItem);
    $("#clte_Identificacion").val(rtnItem);
    $("#clte_Nombres").val(nombreItem);
    $("#tpi_Id").val(tpid);
    $("#clte_Fecha").val(Fecha);
    $('#ModalAgregarCliente').modal('hide');
    if (nuevaCadena == "Si" ) {
        $('#TerceraEdad').show();
        //Tercera Edad
        ms = Date.parse(Fecha);
        fecha1 = new Date(ms);
        var Fechas1 = fecha1.getFullYear()
        var today = new Date
        var today1 = today.getFullYear()
        if (today1 - Fechas1 >= 60) {
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



