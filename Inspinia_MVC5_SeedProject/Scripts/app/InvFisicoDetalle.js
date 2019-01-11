//contador para la tabla detalle
var contador = 0;

//Funcion para validar datos
$('#AgregarInvFisicoDetalle').click(function () {
    var producto = $("#prod_Codigo").val();
    var UnidadMedida = $("#uni_Id").val();
    var uni = $('#uni_Ids').val();
    var cantidadfisica = $("#invfd_Cantidad").val();
    var cantidadsistema = $("#invfd_CantidadSistema").val();

    if (producto == '') {
        $('#MessageError').text('');
        $('#errorproducto').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationproducto').after('<ul id="errorproducto" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
  else if(cantidadfisica == '')
  {
      $('#MessageError').text('');
        $('#errorproducto').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationCantidadFisica').after('<ul id="errorcantidadfisica" class="validation-summary-errors text-danger">Campo Cantidad Requerido</ul>');
    }
    else if(cantidadsistema == '')
    {
        $('#MessageError').text('');
         $('#errorproducto').text('');
         $('#errorcantidadfisica').text('');
         $('#errorcantidadsistema').text('');
         $('#validationCantidadSistema').after('<ul id="errorcantidadsistema" class="validation-summary-errors text-danger">Campo Cantidad Sistema Requerido</ul>');
   
    } else
    {
        //Rellenar la tabla 
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'producto'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'UnidadMedida'>" + $('#uni_Id').val() + "</td>";
        copiar += "<td id = 'cantidadfisica'>" + $('#invfd_Cantidad').val() + "</td>";
        copiar += "<td id = 'cantidadsistema'>" + $('#invfd_CantidadSistema').val() + "</td>";
        copiar += "<td id = 'uni' hidden='hidden'>" + $('#uni_Ids').val() + "</td>";
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
            data: JSON.stringify({ invfd: InventarioFisicoDetalle }),
        })
        .done(function (data) {
            $('#prod_Codigo').text('');
            $('#invfd_Cantidad').text('');
            $('#invfd_CantidadSistema').text('');
            $('#uni_Ids').text('');

            $('#MessageError').text('');
            $('#errorproducto').text('');
            $('#errorcantidadfisica').text('');
            $('#errorcantidadsistema').text('');
        });
    }

}
)

//funcion para el controlador
function GetInventarioFisicoDetalle() {
    var invfd = {
        prod_Codigo: $('#prod_Codigo').val(),
        uni_Id: $('#uni_Ids').val(),
        invfd_Cantidad: $('#invfd_Cantidad').val(),
        invfd_CantidadSistema: $('#invfd_CantidadSistema').val(),
        invfd_id: contador,
        invfd_UsuarioCrea: contador
        //Fecha: $('#fechaCreate').val(),
    };
    return invfd;
}


//eliminar datos agregados a la tabla
$(document).on("click", "#detalle tbody tr td button#removerInventarioFisicoDetalle", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var detalle = {
        invfd_id: idItem,
    };
    $.ajax({
        url: "/InventarioFisico/GuardarInventarioDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ inventariofisicodetalle: InventarioFisicoDetalle }),
    });
});

//Busqueda Generica de Productos
$(document).ready(function () {
    $('#BuscarProducto').DataTable(
        {
            "searching": false,
            "lengthChange": false,

            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",

            }
        });

    var $rows = $('#BuscarProducto tr');
    $("#buscar").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();

    });
});
$(document).on("click", "#BuscarProducto tbody tr td button#seleccionar", function () {
    id = $(this).closest('tr').data('id');
    descripcion = $(this).closest('tr').data('content');
    uni = $(this).closest('tr').data('keyboard');
    uniid = $(this).closest('tr').data('container');
    $("#prod_Codigo").val(id);
    $("#prod_Descripcion").val(descripcion);
    $("#uni_Id").val(uni);
    $("#uni_Ids").val(uniid);
});


//Crear nuevo detalle modal
$('#GuardarNuevoDetalle').click(function () {
    var producto = $("#prod_Codigo").val();
    var UnidadMedida = $("#uni_Id").val();
    var uni = $('#uni_Ids').val();
    var cantidadfisica = $("#invfd_Cantidad").val();
    var cantidadsistema = $("#invfd_CantidadSistema").val();

    if (producto == '') {
        $('#MessageError').text('');
        $('#errorproducto').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationproducto').after('<ul id="errorproducto" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    else if(cantidadfisica == '')
    {
        $('#MessageError').text('');
        $('#errorproducto').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationCantidadFisica').after('<ul id="errorcantidadfisica" class="validation-summary-errors text-danger">Campo Cantidad Requerido</ul>');
    }
    else if(cantidadsistema == '')
    {
        $('#MessageError').text('');
        $('#errorproducto').text('');
        $('#errorcantidadfisica').text('');
        $('#errorcantidadsistema').text('');
        $('#validationCantidadSistema').after('<ul id="errorcantidadsistema" class="validation-summary-errors text-danger">Campo Cantidad Sistema Requerido</ul>');
   
    } else
    {
            var InventarioFisicoDetalle = GetInventarioFisicoDetalle();
            $.ajax({
                url: "/InventarioFisico/NuevoDetallemodal",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ guardar_detalle: InventarioFisicoDetalle }),
            })
            .done(function (data) {
                $('#prod_Codigo').text('');
                $('#invfd_Cantidad').text('');
                $('#invfd_CantidadSistema').text('');
                $('#uni_Ids').text('');

                $('#MessageError').text('');
                $('#errorproducto').text('');
                $('#errorcantidadfisica').text('');
                $('#errorcantidadsistema').text('');
            });
        }

}
)

//funcion para el controlador
function GetInventarioFisicoDetalle() {
    var guardar_detalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        uni_Id: $('#uni_Ids').val(),
        invfd_Cantidad: $('#invfd_Cantidad').val(),
        invfd_CantidadSistema: $('#invfd_CantidadSistema').val(),
        invf_Id: $('#invf_Id').val(),
        invfd_Id: $('#invfd_Id').val(),
        invfd_UsuarioCrea: $('#invfd_UsuarioCrea').val(),
        invfd_UsuarioModifica: $('#invfd_UsuarioModifica').val(),
        invfd_FechaCrea: $('#invfd_FechaCrea').val(),
        invfd_FechaModifica: $('#invfd_FechaModifica').val()
    };
    return guardar_detalle;
}


//Actualizar detalle
function btnActualizarDetalle(invfd_Id) {

    var tbInventarioFisicoDetalle = GetInventarioFisicoDetalle();

    $.ajax({
        url: "/InventarioFisico/UpdateInvFisicoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ actualizardetalle: tbInventarioFisicoDetalle }),
    }).done(function (data) {
        if (data == '') {
            location.reload();
        }
        else if (data == '-1') {
            $('#MensajeError' + invfd_Id).text('');
            $('#ValidationMessageFor' + invfd_Id).after('<ul id="MensajeError' + invfd_Id + '" class="validation-summary-errors text-danger">No se ha podido Actualizar el registro.</ul>');
        }
        else {
            $('#MensajeError' + invfd_Id).text('');
            $('#ValidationMessageFor' + invfd_Id).after('<ul id="MensajeError' + invfd_Id + '" class="validation-summary-errors text-danger">Campo Requerido</ul>');
        }
    });
}
function GetInventarioFisicoDetalle() {
    var guardar_detalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        uni_Id: $('#uni_Ids').val(),
        invfd_Cantidad: $('#invfd_Cantidad').val(),
        invfd_CantidadSistema: $('#invfd_CantidadSistema').val(),
        invf_Id: $('#invf_Id').val(),
        invfd_Id: $('#invfd_Id').val(),
        invfd_UsuarioCrea: $('#invfd_UsuarioCrea').val(),
        invfd_UsuarioModifica: $('#invfd_UsuarioModifica').val(),
        invfd_FechaCrea: $('#invfd_FechaCrea').val(),
        invfd_FechaModifica: $('#invfd_FechaModifica').val()
    };
    return guardar_detalle;
}
