$('#BtnGuardarDetalle').click(function () {
    var ProductoCodigo = $('#prod_Codigo').val();
    var PrecioMayorista = $('#lispd_PrecioMayorista').val
    var PrecioMinorista = $('#lispd_PrecioMinorista').val();
    var DescuentoCaja = $('#lispd_DescCaja').val();
    var DescuentoGerente = $('#lispd_DescGerente').val();

    if (ProductoCodigo == '') {
        $('#ErrorProductoCodigoCreate').text('');
        $('#validacionProductoCodigoCreate').after('<p id="ErrorProductoCodigoCreate" style="color:red">Campo Producto requerido</p>');
    }
    else if (PrecioMayorista == '') {
        $('#ErrorPrecioMayoristaCreate').text('');
        $('#validacionPrecioMayoristaCreate').after('<p id="ErrorPrecioMayoristaCreate" style="color:red">Campo Precio Mayorista requerido</p>');
    }
    else if (PrecioMinorista == '') {
        $('#ErrorPrecioMinoristaCreate').text('');
        $('#validacionPrecioMinoristaCreate').after('<p id="ErrorPrecioMinoristaCreate" style="color:red">Campo Precio Minorista requerido</p>');
    }
    else if (DescuentoCaja == '') {
        $('#ErrorDescuentoCajaCreate').text('');
        $('#validacionDescuentoCajaCreate').after('<p id="ErrorDescuentoCajaCreate" style="color:red">Campo Descuento Caja requerido</p>');
    }

    else if (DescuentoGerente == '') {
        $('#ErrorDescuentoGerenteCreate').text('');
        $('#validacionDescuentoGerenteCreate').after('<p id="ErrorDescuentoGerenteCreate" style="color:red">Campo Descuento Gerente requerido</p>');
    }


    else {
        var ListadoPrecioDetalle = GetListaDetalles();
        $.ajax({
            url: "/ListaPrecios/GuardarListadoPrecioDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ListadoPrecioDetalles: ListadoPrecioDetalle }),
            success: function (data) {
                console.log(data)
            }
        })
        .done(function (data) {
            if (data == 'El registro se guardo exitosamente') {
                location.reload();
                swal("El registro se guardó exitosamente!", "", "success");
            }
            else {
                location.reload();
                swal("El registro  no se guardó!", "", "error");
            }
        });
    }


    function GetListaDetalles() {

        var ListadoDetalle = {
           
            listp_Id: $('#listp_Id').val(),
            prod_Codigo: $('#prod_Codigo').val(),
            lispd_PrecioMayorista: $('#lispd_PrecioMayorista').val(),
            lispd_PrecioMinorista: $('#lispd_PrecioMinorista').val(),
            lispd_DescCaja: $('#lispd_DescCaja').val(),
            lispd_DescGerente: $('#lispd_DescGerente').val(),

        }
        return ListadoDetalle
    };
});