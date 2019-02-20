$(document).ready(function () {

    $('#Efectivo').hide();
    $('#TCD').hide();
    $('#Cheque').hide();
    $('#NC').hide();

});

$(document).ready(function () {
    $("#TipoPago").change(function (evt) {
        if ($("#TipoPago").val() == 1 )
        {
            $('#Efectivo').show();
            $('#TCD').hide();
            $('#Cheque').hide();
            $('#NC').hide();
        }
        else if ($("#TipoPago").val() == 2) {      
            $('#TCD').show();
            $('#Efectivo').hide();
            $('#Cheque').hide();
            $('#NC').hide();
        }
        else if ($("#TipoPago").val() == 3) {
            $('#TCD').hide();
            $('#Efectivo').hide();
            $('#NC').hide();
            $('#Cheque').show();

        }
        else if ($("#TipoPago").val() == 4) {
            $('#TCD').hide();
            $('#Efectivo').hide();
            $('#Cheque').hide();
            $('#NC').show();
        }
        else {
            $('#Efectivo').hide();
            $('#TCD').hide();
            $('#Cheque').hide();
            $('#NC').hide();
        }
       
    });
});

//Busqueda de nota Credito-----------
$(document).ready(function () {
    var $rows = $('#BodyNC tr');
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

//Seleccinar----------------------
$(document).on("click", "#PagoNC tbody tr td button#AgregarNotaCredito", function () {
    IdItem = $(this).closest('tr').data('nocre_Id');
    CodigoItem = $(this).closest('tr').data('nocre_Codigo');
    MontoItem = $(this).closest('tr').data('nocre_Monto');
    $("#nc_Id").val(IdItem);
    $("#MontoNC").val(MontoItem);
    $("#CodigoNC").val(CodigoItem);

    $('#ModalAgregaNotaCredito').modal('hide');

    //$(document).ready(function () {
    //    if (idItem != '') {
    //        document.getElementById("btnProducto").disabled = false;
    //        document.getElementById("prod_Codigo").disabled = false;
    //        GetIDFactura(idFactitem);
    //    }
    //});
})