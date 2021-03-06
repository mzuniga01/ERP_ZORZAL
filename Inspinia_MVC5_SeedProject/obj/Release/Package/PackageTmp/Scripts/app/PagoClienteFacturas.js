﻿//Factura Buscar Cliente
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
    $("#tbFactura_clte_Id").val(idItem);
    $("#tbFactura_clte_Identificacion").val(rtnItem);
    $("#tbFactura_clte_Nombres").val(nombreItem);
    $('#ModalAgregarCliente').modal('hide');
    console.log(idItem)
    if (idItem != '') {
        document.getElementById("Factura").disabled = false;
        document.getElementById("tbFactura_fact_Codigo").disabled = false;
        GetIDCliente(idItem)
        GetIDClienteNC(idItem)
    }

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




///Filtrar modal de Codigo de nota credito
var CodCliente = $('#tbFactura_clte_Id').val();
console.log(CodCliente)

function GetIDClienteNC(CodCliente, idItem) {
    $.ajax({
        url: "/Pago/NotasCredito",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodCliente: CodCliente }),

        error: function () {
            alert("No se puede filtrar");
        },
        success: function (list) {
            $('#BodyNotaCreditoPagos').empty();
            console.log(list)
            $.each(list, function (key, val) {
                var EsRedimido = "No";
                var EsImpreso = "Si";
                copiar = "<tr data-codigonc=" + val.nocre_Codigo + "  data-montonc=" + val.nocre_Monto + " >";
                copiar += "<td id = 'Codigo'>" + val.nocre_Codigo + "</td>";
                copiar += "<td id = 'Monto'>" + val.nocre_Monto + "</td>";
                copiar += "<td id = 'c'>" + EsRedimido + "</td>";
                copiar += "<td id = 'd'>" + EsImpreso + "</td>";
                copiar += "<td>" + '<button id="AgregarNotaCredito" class="btn btn-primary btn-xs" type="button">Añadir</button>' + "</td>";
                copiar += "</tr>";
                $('#BodyNotaCreditoPagos').append(copiar);

            });
        }


    });

}


///filtrar modal de Factura

function GetIDCliente(CodCliente, idItem) {
    $.ajax({
        url: "/Pago/FacturasPago",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodCliente: CodCliente }),

        error: function () {
            alert("No se puede filtrar");
        },
        success: function (list) {
            $('#BodyFacturaPagos').empty();
            console.log(list)
            $.each(list, function (key, val) {
                copiar = "<tr data-fid=" + val.fact_Id + " data-codigo=" + val.fact_Codigo + "  data-monto=" + val.MontoFactura + "  data-total=" + val.TotalPagado + "  data-saldo=" + val.SaldoFactura + " data-clteid=" + val.clte_Id + " data-credito=" + val.fact_AlCredito +">";
                copiar += "<td id = 'Codigo'>" + val.fact_Codigo + "</td>";
                copiar += "<td id = 'Monto'>" + val.MontoFactura + "</td>";
                copiar += "<td id = 'c'>" + val.TotalPagado + "</td>";
                copiar += "<td id = 'd'>" + val.SaldoFactura + "</td>";
                copiar += "<td>" + '<button id="AgregarFactura" class="btn btn-primary btn-xs" type="button">Añadir</button>' + "</td>";
                copiar += "</tr>";
                $('#BodyFacturaPagos').append(copiar);

            });
        }


    });

}



//Añadir una nota de credito
$(document).on("click", "#DataTable tbody tr td button#AgregarNotaCredito", function () {
    CodigoNC = $(this).closest('tr').data('codigonc');
    Monto = $(this).closest('tr').data('montonc');
    $("#nocre_Codigo_cdto_Id").val(CodigoNC);
    $("#pago_TotalPago").val(Monto);
    $('#ModalAgregarNotaCredito').modal('hide');
});


//Añadir una Factura
$(document).on("click", "#FacturasPagos tbody tr td button#AgregarFactura", function () {
    Factura_Id = $(this).closest('tr').data('fid');
    CodigoFactura = $(this).closest('tr').data('codigo');
    Monto = $(this).closest('tr').data('monto');
    TotalPagado = $(this).closest('tr').data('total');
    SaldoAnterior = $(this).closest('tr').data('saldo');
    clte = $(this).closest('tr').data('clteid');
    credito = $(this).closest('tr').data('credito');
    $("#fact_Id").val(Factura_Id);
    $("#tbFactura_fact_Codigo").val(CodigoFactura);
    $("#MontoFactura").val(Monto);
    $("#TotalPagado").val(TotalPagado);
    $("#SaldoAnterior").val(SaldoAnterior);
    $("#pago_SaldoAnterior").val(SaldoAnterior);
    $("#tbFactura_clte_Id").val(clte);
    $("#tbFactura_fact_AlCredito").val(credito);
    $('#ModalAgregaFacturaPago').modal('hide');
});