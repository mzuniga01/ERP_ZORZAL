$(document).ready(function () {
    $("#exo_FechaIFinalVigencia").focus(function () {
        $('#ccc').html('');
    });
    $("#exo_FechaIFinalVigencia").change(function () {
        var hoy = $('#exo_FechaInicialVigencia').val();
        var fecha = $('#exo_FechaIFinalVigencia').val();
        //var fechaFormulario = Date.parse(fecha);

        if (hoy < fecha) {
            //$('#ccc').html("");
            valido = document.getElementById('ccc');
            valido.innerText = "";
            return true;
        } else {
            //$('#ccc').html("La fecha final debe ser mayor a la fecha inicial");
            valido = document.getElementById('ccc');
            valido.innerText = "La fecha final debe ser mayor a la fecha inicial";
            return false;
        }
    });
});

$(document).ready(function () {
    $("#btnGuardar").click(function () {
        var hoy = $('#exo_FechaInicialVigencia').val();
        var fecha = $('#exo_FechaIFinalVigencia').val();
        if (hoy < fecha) {
            valido = document.getElementById('ccc');
            valido.innerText = "";
        }
        else {
            valido = document.getElementById('ccc');
            valido.innerText = "La fecha final debe ser mayor a la fecha inicial";
            return false;
        }

    });
});

$(document).on("click", "#tbCliente tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    rtnItem = $(this).closest('tr').data('rtn');
    NombreCliente = $(this).closest('tr').data('name');
    $("#clte_Id").val(idItem);
    $("#tbCliente_clte_Identificacion").val(rtnItem);
    $('#ModalAgregarClientes').modal('hide');
    $("#tbCliente_clte_Nombres").val(NombreCliente)
    $("#tbCliente_clte_NombreComercial").val(NombreCliente);
});