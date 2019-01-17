﻿var contador = 0;


function GetSalidaDetalle() {
    var SalidaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        sal_Cantidad: $('#sal_Cantidad').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}


//Agregar

$('#AgregarDetalleSalida').click(function () {
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Unidad_Medida = $('#pscat_Id').val();
    var Cantidad = $('#sal_Cantidad').val();
    
    
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
        $('#ValidationNombreCreate').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        //copiar += "<td>" + $('#CodTipoCasoExitoCreate option:selected').text() + "</td>";
        //copiar += "<td hidden id='MunCodigo'>" + $('#mun_Codigo option:selected').val() + "</td>";
        copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
        copiar += "<td id = 'Unidad_Medida'>" + $('#pscat_Id').val() + "</td>";
        copiar += "<td id = 'Cantidad'>" + $('#sal_Cantidad').val() + "</td>";
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
                    $('#sal_Cantidad').val('');
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



    //SalidaDetalle *Editar

    function EditSalidaDetalle(sald_Id) {
        $("#MsjError").text("");
        console.log("Sii");
        $.ajax({
            url: "/Box/GetBox",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ sald_Id }),
        })
        .done(function (data) {
            $.each(data, function (i, item) {
                $("#prod_Descripcion_edit").val(item.prod_Descripcion);
                $("#prod_Marca_edit").val(item.prod_Marca);
                $("#prod_Modelo_edit").val(item.prod_Modelo);
                $("#prod_Talla_edit").val(item.prod_Talla);
                $("#prod_Color_edit").val(item.prod_Color);
                $("#uni_Descripcion_edit").val(item.uni_Descripcion);
                $("#sal_Cantidad_edit").val(item.sal_Cantidad);
                $("#prod_Codigo_edit").val(item.prod_Codigo);
                $("#MyModal").modal();
                console.log(data);
            })
        })

        .fail(function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR', jqXHR);
            console.log('textStatus', textStatus);
            console.log('errorThrown', errorThrown);

        })
    }


    $("#Btnsubmit").click(function () {
        var data = $("#SubmitForm").serializeArray();

        $.ajax({
            type: "Post",
            url: "/Box/UpdateSalidaDetalle",
            data: data,
            success: function (result) {
                if (result == '-1')
                    $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
                else
                    $("#MyModal").modal("hide");
            }
        });
    })