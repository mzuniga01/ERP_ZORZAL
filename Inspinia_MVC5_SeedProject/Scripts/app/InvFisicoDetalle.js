//contador para la tabla detalle
var contador = 0;

//Funcion para validar datos
$('#AgregarInvFisicoDetalle').click(function () {
    var producto = $("#prod_Codigo").val();
    var UnidadMedida = $("#uni_Id").val();
    var cantidadfisica = $("#invfd_Cantidad").val();
    var cantidadsistema = $("#invfd_CantidadSistema").val();

    if (producto == '') {
        $('#producto').text('');
        $('#errorproducto').text('');
        $('#errorunidadmedida').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationproducto').after('<ul id="errorproducto" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    else if(UnidadMedida == '')
        {
        $('#UnidadMedida').text('');
        $('#errorproducto').text('');
        $('#errorunidadmedida').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationUnidadMedida').after('<ul id="errorunidadmedida" class="validation-summary-errors text-danger">Campo Unidad de Medida Requerido</ul>');
    }else if(cantidadfisica == '0.00')
    {
        $('#cantidadfisica').text('');
        $('#errorproducto').text('');
        $('#errorunidadmedida').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationCantidadFisica').after('<ul id="errorcantidadfisica" class="validation-summary-errors text-danger">Campo Cantidad Requerido</ul>');
    }
    else if(cantidadsistema == '0.00')
    {
         $('#cantidadsistema').text('');
         $('#errorproducto').text('');
         $('#errorunidadmedida').text('');
         $('#errorcantidadfisica').text('');
         $('#errorcantidadsistema').text('');
         $('#validationCantidadSistema').after('<ul id="errorcantidadsistema" class="validation-summary-errors text-danger">Campo Cantidad Sistema Requerido</ul>');
   
    } else
    {
        //Rellenar la tabla 
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'producto'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td>" + $('#uni_Id option:selected').text() + "</td>";
        copiar += "<td hidden id='UnidadMedida'>" + $('#uni_Id option:selected').val() + "</td>";
        copiar += "<td id = 'cantidadfisica'>" + $('#invfd_Cantidad').val() + "</td>";
        copiar += "<td id = 'cantidadsistema'>" + $('#invfd_CantidadSistema').val() + "</td>";
        copiar += "<td>" + '<button id="removerInventarioFisicoDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#detalle').append(copiar);

        //ajax para el controlador
        var InventarioFisicoDetalle = GetInventarioFisicoDetalle();
        $.ajax({
            url: "/InventarioFisico/GuardarInventarioDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ inventariofisicodetalle: InventarioFisicoDetalle }),
        })
        .done(function (data) {
            $('#DescripcionCreate').val('');
            $('#MessageError').text('');
            $('#ErrorDescripcion').text('');
            $('#ErrorFecha').text('');
        });
    }

}
)

//funcion para el controlador
function GetInventarioFisicoDetalle() {
    var inventariofisicodetalles = {
        prod_Codigo: $('#producto').val(),
        invfd_Cantidad: $('#CantidadFisica').val(),
        invfd_CantidadSistema: $('#CantidadSistema').val(),
        invfd_id: contador,
        uni_Id: $('#UnidadMedida').val(),
        //Fecha: $('#fechaCreate').val(),
    };
    return inventariofisicodetalles;
}


//eliminar datos agregados a la tabla
$(document).on("click", "#detalle tbody tr td button#removerInventarioFisicoDetalle", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var SubCategoria = {
        pscat_Id: idItem,
    };
    $.ajax({
        url: "/InventarioFisico/GuardarInventarioDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ inventariofisicodetalle: InventarioFisicoDetalle }),
    });
});