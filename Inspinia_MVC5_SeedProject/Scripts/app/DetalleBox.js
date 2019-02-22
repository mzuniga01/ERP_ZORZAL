var contador = 0;
$(document).ready(function () {
    $('#Table_BuscarProducto').DataTable(
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

    var $rows = $('#Table_BuscarProducto tr');
    $("#search").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
        //$rows.show().filter(function () {
        //    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
        //    return !~text.indexOf(val);
        //}).hide();

    });
});


$(document).on("click", "#Table_BuscarProducto tbody tr td button#seleccionar", function () {

    prod_CodigoBarrasItem = $(this).closest('tr').data('html');
    idItem = $(this).closest('tr').data('id');
    contentItem = $(this).closest('tr').data('content');
    uni_IdtItem = $(this).closest('tr').data('keyboard');
    psubctItem = $(this).closest('tr').data('container');
    pcatItem = $(this).closest('tr').data('interval');
    $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
    $("#prod_Codigo").val(idItem);
    $("#prod_Descripcion").val(contentItem);
    $("#uni_Id").val(uni_IdtItem);
    $("#pscat_Id").val(psubctItem);
    $("#pcat_Id").val(pcatItem);
    $("#sald_Cantidad").focus();
    //$("#cod").val(idItem);

});

//prueba de enter

$(function () {
    $('#prod_CodigoBarras').keydown(function (e) {
        if (e.keyCode == 13) {
            $("#seleccionar").focus().click();

            $(document).on("click", "#Table_BuscarProducto tbody tr td button#seleccionar", function () {
                prod_CodigoBarrasItem = $(this).closest('tr').data('html');
                idItem = $(this).closest('tr').data('id');
                contentItem = $(this).closest('tr').data('content');
                uni_IdtItem = $(this).closest('tr').data('keyboard');
                psubctItem = $(this).closest('tr').data('container');
                pcatItem = $(this).closest('tr').data('interval');
                $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
                $("#prod_Codigo").val(idItem);
                $("#prod_Descripcion").val(contentItem);
                $("#uni_Id").val(uni_IdtItem);
                $("#pscat_Id").val(psubctItem);
                $("#pcat_Id").val(pcatItem);
                $("#sald_Cantidad").focus();
                //$("#cod").val(idItem);

            });
            console.log('prueba');
            $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
            $("#prod_Codigo").val(idItem);
            $("#prod_Descripcion").val(contentItem);
            $("#uni_Id").val(uni_IdtItem);
            $("#pscat_Id").val(psubctItem);
            $("#pcat_Id").val(pcatItem);
            $("#sald_Cantidad").focus();
            return false;
        }
    });
});


function GetSalidaDetalle() {
    var SalidaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        sal_Cantidad: $('#sald_Cantidad').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}


//Agregar

$('#AgregarDetalleSalida').click(function () {
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Unidad_Medida = $('#pscat_Id').val();
    var Cantidad = $('#sald_Cantidad').val();
    if (Cantidad == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#validationcantidad').after('<ul id="sald_Cantidad" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
    }
    else if (Producto =='' )
        {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#validationproducto').after('<ul id="prod_Descripcion" class="validation-summary-errors text-danger">Producto Requerido</ul>');
    }
    else  {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        //copiar += "<td>" + $('#CodTipoCasoExitoCreate option:selected').text() + "</td>";
        //copiar += "<td hidden id='MunCodigo'>" + $('#mun_Codigo option:selected').val() + "</td>";
        copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
        copiar += "<td id = 'Unidad_Medida'>" + $('#pscat_Id').val() + "</td>";
        copiar += "<td id = 'Cantidad'>" + $('#sald_Cantidad').val() + "</td>";
        copiar += "<td>" + '<button id="removeSalidaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tbSalidaDetalle').append(copiar);


        //Para obtener el valor y mandarlo al controlador

        var tbSalidaDetalle = GetSalidaDetalle();
        $.ajax({
            url: "/Box/SaveSalidaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle }),
        })
                .done(function (data) {
                    $('#prod_Codigo').val('');
                    $('#prod_Descripcion').val('');
                    $('#pscat_Id').val('');
                    $('#sald_Cantidad').val('');
                    $('#MessageError').text('');
                    $('#NombreError').text('');
                    console.log('Hola');
                });



    }

});

//Remover el detalle
$(document).on("click", "#tbSalidaDetalle tbody tr td button#removeSalidaDetalle", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var SalidaDetalle = {
        sald_UsuarioCrea: idItem,
    };
    $.ajax({
        url: "/Box/RemoveSalidaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ SalidaDetalle: SalidaDetalle }),
    });
});


//Detalle

function EditSalidaDetalles(sald_Id) {
    $.ajax({
        url: "/Box/getSalidaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ sald_Id: sald_Id }),

    })
        .done(function (data) {
            console.log(data);
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#sald_Id_SD").val(item.sald_Id);
                    $("#prod_Codigo_SD").val(item.prod_Codigo);
                    $("#sald_Cantidad_SD").val(item.sald_Cantidad);
                    $("#prod_Descripcion_SD").val(item.prod_Descripcion);
                    $("#box_Codigo_SD").val(item.box_Codigo);
                    //$("#pedd_FechaCrea_Ped").val(item.pedd_FechaCrea);
                    $("#EditSalidaDetalle").modal();
                })
            }
        })
}


$("#BtnsubmitMunicipio").click(function () {
    var sald_id = $('#sald_Id').val();
    var box = $('#box_Codigo').val();
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "Post",
        url: "/Box/EditSalidaDetalle",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
            else
                window.location.href = '/Box/Edit/' + box;
        }

    });
})



function GetNewSalidaDetalle() {
    var SalidaDetalle = {
        sald_Id: $('#sald_Id').val(),
        prod_Codigo: $('#prod_Codigo').val(),
        sald_Cantidad: $('#sald_Cantidad').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}


$('#btnCreateSalidaDetalle').click(function () {
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Unidad_Medida = $('#pscat_Id').val();
    var Cantidad = $('#sald_Cantidad').val();
    var sald_Id = $('#sald_Id').val();

    if (Producto == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationCodigoCreate').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    else if (Unidad_Medida == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationNombreCreate').after('<ul id="NombreError" class="validation-summary-errors text-danger">Unidad Medida Requerido</ul>');
    }
    else if (Cantidad == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#sald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
    }

    else {
        var tbSalidaDetalle = GetNewSalidaDetalle();
        $.ajax({
            url: "/Box/SaveNewDatail",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle }),
        })
        .done(function (data) {
            if (data == 'El registro se guardo exitosamente') {
                location.reload();
                swal("El registro se almacenó exitosamente!", "", "success");
            }
            else {
                location.reload();
                swal("El registro  no se almacenó!", "", "error");
            }
        });

        var PuntoEmisionDetalle = GetPuntoEmisionDetalle();
        $.ajax({
            url: "/PuntoEmision/SaveCreateNumeracion",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CreatePuntoEmisionDetalle: PuntoEmisionDetalle }),
            success: function (data) {
            }
        })
    }
});

//VALIDAR SOLO NUMEROS
$(function () {
    $("#sald_Cantidad").keydown(function (event) {
        //alert(event.keyCode);
        if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) && event.keyCode !== 190 && event.keyCode !== 110 && event.keyCode !== 8 && event.keyCode !== 9) {
            return false;
        }
    });
});

//VALIDAR CARACTERES
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
}

//Validar Solo caracteres 
function soloLetrasYNumeros(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-z0-9A-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla); }  

$("#sald_Cantidad").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});