//Factura Buscar Cliente
$(document).ready(function () {
    var $rows = $('#ClienteTbody tr');
    $("#searchCliente").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
        console.log('val', val.length);
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

//Factura Seleccionar Cliente
$(document).on("click", "#tbCliente tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    rtnItem = $(this).closest('tr').data('rtn');
    nombreItem = $(this).closest('tr').data('nombrecliente');
    tpid = $(this).closest('tr').data('tpi');
    Fecha = $(this).closest('tr').data('fecha');
    EspersonaNatural = $(this).closest('tr').data('EsPersonaNatural');

   var espersonal = $('.clte_EsPersonaNatural')[3].checked = true;
   console.log()     
    $("#clte_Id").val(idItem);
    $("#clte_Identificacion").val(rtnItem);
    $("#clte_Nombres").val(nombreItem);
    $("#tpi_Id").val(tpid);
    $("#clte_Fecha").val(Fecha);
    $('#ModalAgregarCliente').modal('hide');
    console.log(EspersonaNatural)
    if (EspersonaNatural) {
        $('#TerceraEdad').show();
        //Tercera Edad
        ms = Date.parse(Fecha);
        fecha1 = new Date(ms);
        console.log(fecha1)
        var Fechas1 = fecha1.getFullYear()
        var today = new Date
        var today1 = today.getFullYear()
        if (today1 - Fechas1 >= 60) {

        }
        else {
            $("#MostrarTerceraEdad").prop("checked", true);
            $("#fact_AutorizarDescuento").prop("checked", true);
            $('#Cred2').show();
            $('#fact_PorcentajeDescuento').val('');
            document.getElementById("MostrarTerceraEdad").disabled = true;
            document.getElementById("fact_AutorizarDescuento").disabled = true;
        }
    }
    else {
        $('#TerceraEdad').hide();
        document.getElementById("MostrarTerceraEdad").disabled = false;
        document.getElementById("fact_AutorizarDescuento").disabled = false;
        $('#Cred2').hide();
    }
});


//Facturar RowSeleccionar Cliente
$(document).ready(function () {
    var table = $('#tbCliente').DataTable();
    $('#tbCliente tbody').on('click', 'tr', function () {
        idItem = $(this).closest('tr').data('id');
        rtnItem = $(this).closest('tr').data('rtn');
        nombreItem = $(this).closest('tr').data('nombrecliente');
        $("#clte_Id").val(idItem);
        $("#clte_Identificacion").val(rtnItem);
        $("#clte_Nombres").val(nombreItem);
        $('#ModalAgregarCliente').modal('hide');
    });
});

