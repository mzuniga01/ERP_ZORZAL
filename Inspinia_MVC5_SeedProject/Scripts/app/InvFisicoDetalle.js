//contador para la tabla detalle
var contador = 0;

//Funcion para validar datos
$('#AgregarInvFisicoDetalle').click(function () {
    var producto = $("#prod_Codigo").val();
    var barras = $("#prod_CodigoBarras").val();
    var UnidadMedida = $("#uni_Id").val();
    var uni = $('#uni_Ids').val();
    var cantidadfisica = $("#invfd_Cantidad").val();
    var cantidadsistema = $("#invfd_CantidadSistema").val();

    if (barras == '') {
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
        copiar += "<td id = 'producto'>" + $('#prod_CodigoBarras').val() + "</td>";
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
            $('#prod_CodigoBarras').val('');
            $('#invfd_CantidadSistema').val('');
            $('#prod_Descripcion').val('');
            $('#uni_Id').val('');

            $('#MessageError').text('');
            $('#errorproducto').text('');
            $('#errorcantidadfisica').text('');
            $('#errorcantidadsistema').text('');
        });
    }

})

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
    barras = $(this).closest('tr').data('delay');
    uni = $(this).closest('tr').data('keyboard');
    uniid = $(this).closest('tr').data('container');
    $("#prod_Codigo").val(id);
    $("#prod_Descripcion").val(descripcion);
    $("#prod_CodigoBarras").val(barras);
    $("#uni_Id").val(uni);
    $("#uni_Ids").val(uniid);
    console.log(id);
    seleccionar(id);
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
    var tbInventarioFisicoDetalle = getdetalle();
    var producto = $("#prod_Codigo").val();
    console.log(producto);

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
function getdetalle() {
    var guardar_detalle = {
        prod_Codigo: $('#codigo').val(),
        uni_Id: $('#uni_Ids').val(),
        invfd_Cantidad: $('#CantidadFisica').val(),
        invfd_CantidadSistema: $('#CantidadSistema').val(),
        invf_Id: $('#invf_Id').val(),
        invfd_Id: $('#invfd_Id').val(),
        invfd_UsuarioCrea: $('#invfd_UsuarioCrea').val(),
        invfd_UsuarioModifica: $('#invfd_UsuarioModifica').val(),
        invfd_FechaCrea: $('#invfd_FechaCrea').val(),
        invfd_FechaModifica: $('#invfd_FechaModifica').val()
    };
    return guardar_detalle;
}


//Encargado de Bodega
$(document).change("#bod_Id", function () {
    var responsable = $('#bod_Id').val();
    console.log(responsable);
    GetResponsableBodega();
});

function GetResponsableBodega() {
    var invf_responsable = $('#bod_Id').val();
    $.ajax({
        url: "/InventarioFisico/GetResponsableBodega",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ invf_responsable: invf_responsable }),
    })
    .done(function (data) {
        if (data.length > 0) {
            $('#invf_ResponsableBodega').empty();
            $.each(data, function (key, val) {
                $('#invf_ResponsableBodega').val(val.emp_Nombres);
            });
            $('#invf_ResponsableBodega').trigger("chosen:updated");
        }
        else {
            $('#invf_ResponsableBodega').empty();
            $('#invf_ResponsableBodega').val(val.emp_Nombres);
        }
    });
}


//Cantidad del Sistema
function seleccionar(prod_Codigo) {
    var bodega = $("#bod_Id").val();
    var guardar_cantidad = {
        bod_Id: bodega,
        prod_Codigo: prod_Codigo,
    };
    $.ajax({
        url: "/InventarioFisico/CantidadExistencias",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CantidadExistencias: guardar_cantidad }),
    }).done(function (data) {
        if (data.length > 0) {
            $('#invfd_CantidadSistema').empty();
            $.each(data, function (key, val) {
                    $('#invfd_CantidadSistema').val(val.bodd_CantidadExistente);
                
            });
            $('#invfd_CantidadSistema').trigger("chosen:updated");
        }
        else {
            $('#invfd_CantidadSistema').empty();
                $('#invfd_CantidadSistema').val(val.bodd_CantidadExistente);
       
        }
    });
}

function GetCantidadExistente() {
    var guardar_cantidad = {
        bod_Id: $('#bod_Id').val(),
        prod_Codigo: $('#prod_Codigo').val()
    };
    return guardar_cantidad;
}







//$("#seleccionar").click("#bod_Id", function () {
//    console.log("Hola");
//    var bodega = $('#bod_Id').val();
//    console.log(bodega);
//    GetCantidadExistente();
//});

//function GetCantidadExistente() {
//    var cantidad = $('#bod_Id').val();
//    var producto = $('#prod_Codigo').val();
//    $.ajax({
//        url: "/InventarioFisico/CantidadExistencias",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ bod_Id: cantidad, prod_Codigo: producto }),
//    })
//    .done(function (data) {
//        if (data.length > 0) {
//            $('#invfd_CantidadSistema').empty();
//            $.each(data, function (key, val) {
//                $('#invfd_CantidadSistema').val(val.bodd_CantidadExistente);
//            });
//            $('#invfd_CantidadSistema').trigger("chosen:updated");
//        }
//        else {
//            $('#invfd_CantidadSistema').empty();
//            $('#invfd_CantidadSistema').val(val.bodd_CantidadExistente);
//        }
//    });
//}


