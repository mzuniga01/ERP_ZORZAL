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
    nombreItem = $(this).closest('tr').data('nombrecliente'); sss
    $("#tbFactura_clte_Id").val(idItem);
    $("#tbFactura_clte_Identificacion").val(rtnItem);
    $("#tbFactura_clte_Nombres").val(nombreItem);
    $('#ModalAgregarCliente').modal('hide');


});


//Facturar RowSeleccionar Cliente
$(document).ready(function () {
    var table = $('#tbCliente').DataTable();
    $('#tbCliente tbody').on('click', 'tr', function () {
        idItem = $(this).closest('tr').data('id');
        rtnItem = $(this).closest('tr').data('rtn');
        nombreItem = $(this).closest('tr').data('nombrecliente');
        $("#tbFactura_clte_Id").val(idItem);
        $("#tbFactura_clte_Identificacion").val(rtnItem);
        $("#tbFactura_clte_Nombres").val(nombreItem);
        $('#ModalAgregarCliente').modal('hide');
    });
});



//################ Seleccionar Facturas con saldo#############

//Factura Buscar Cliente
$(document).ready(function () {
    var $rows = $('#FacturaPagoTbody tr');
    $("#searchFacturaPago").keyup(function () {
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

//Factura Seleccionar 
$(document).on("click", "#V_FacturaPago tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    codigoItem = $(this).closest('tr').data('codigo');
    montoItem = $(this).closest('tr').data('monto');
    pagoItem = $(this).closest('tr').data('pago');
    saldoItem = $(this).closest('tr').data('saldo');
    $("#fact_Id").val(idItem);
    $("#tbFactura_fact_Codigo").val(codigoItem);
    $("#MontoFactura").val(montoItem);
    $("#TotalPagado").val(pagoItem);
    $("#pago_SaldoAnterior").val(saldoItem);
    $("#SaldoAnterior").val(saldoItem);
    $('#ModalAgregaFacturaPago').modal('hide');

});


//Facturar RowSeleccionar Cliente
$(document).ready(function () {
    var table = $('#V_FacturaPago').DataTable();
    $('#V_FacturaPago tbody').on('click', 'tr', function () {
        idItem = $(this).closest('tr').data('id');
        codigoItem = $(this).closest('tr').data('codigo');
        montoItem = $(this).closest('tr').data('monto');
        pagoItem = $(this).closest('tr').data('pago');
        saldoItem = $(this).closest('tr').data('saldo');
        $("#fact_Id").val(idItem);
        $("#tbFactura_fact_Codigo").val(codigoItem);
        $("#MontoFactura").val(montoItem);
        $("#TotalPagado").val(pagoItem);
       $("#pago_SaldoAnterior").val(saldoItem);
       $("#SaldoAnterior").val(saldoItem);
        $('#ModalAgregarCliente').modal('hide');
    });
});

