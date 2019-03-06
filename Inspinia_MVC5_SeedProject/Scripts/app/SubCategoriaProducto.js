﻿////Get SubCategoria
$(document).on("change", "#pcat_Id", function () {
    GetSubCategoriaProducto();   
});

function GetSubCategoriaProducto() {
    var CodCategoria = $('#pcat_Id').val();
    $.ajax({
        url: "/Producto/GetSubCategoriaProducto",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodCategoria: CodCategoria }),
    })
    .done(function (data) {
        if (data.length > 0) {
            $('#pscat_Id').empty();
            $('#pscat_Id').append("<option value=''>Seleccione SubCategoria</option>");
            $.each(data, function (key, val) {
                $('#pscat_Id').append("<option value=" + val.pscat_Id + ">" + val.pscat_Descripcion + "</option>");
            });
            $('#pscat_Id').trigger("chosen:updated");
        }
        else {
            $('#pscat_Id').empty('');
            $('#pscat_Id').append("<option value=''>Sin SubCategoria</option>");
        }
    });
}

//Fin


//Cargar Codigo Producto
$('#pscat_Id').change(function () {
    //console.log('Entra pero no hace nada');
    var cate = $('#pcat_Id').val();
    var subcate = $('#pscat_Id').val();  
    var prodcod = $('#prod_Codigo').val();
    console.log(cate);
    console.log(subcate);

    $.ajax({
        type: 'POST',
        url: '/Producto/GetValue',
        data: JSON.stringify({ pcat_Id: cate, pscat_Id: subcate }),
        contentType: 'application/json',
        success: function (data) {
            $('#CodigoPro').val(data);
            console.log(data);

        }

    })
});


//Validacion de solo letras
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toUpperCase();
    letras = " ABCDEFGHIJKLMNOPQRSTUVWXYZáéíóúabcdefghijklmnñopqrstuvwxyz1234567890-+()$.";
    especiales = "8-37-39-46";
    

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }

}


$('#prod_Descripcion').on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);

});

$('#prod_Marca').on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);

});

$('#prod_Modelo').on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);

});

$('#prod_Color').on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);

});

$('#prod_Talla').on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);

});

function solonumeros(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toUpperCase();
    letras = "1234567890";
    especiales = "8-37-39-46";


    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }

}


$("#prod_CodigoBarras").on("keypress keyup blur", function (event) {
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

//$('#btnGuardar').click(function () {
//    $("#ErrorValidacionGeneral").remove();
//    $("#errorActividad").remove();
//    $("#errorActividada").remove();
//    $("#errorcategoria").remove();
//    var unidad = $("#uni_Id").find('option:selected').val();
//    var subcategoria = $("#pscat_Id").find('option:selected').val();
//    var categoria = $("#pcat_Id").find('option:selected').val();
//    if (unidad == '') {
//        $('#unidad').text('');
//        $('#errorActividad').text('');
//        $('#validationUnidad').after('<ul id="errorActividad" class="validation-summary-errors text-danger">Campo Unidad de Medida Requerido</ul>');
//    }
//    if (subcategoria == '') {
//        $('#subcategoria').text('');
//        $('#errorActividada').text('');
//        $('#validationSubCategoria').after('<ul id="errorActividada" class="validation-summary-errors text-danger">Campo SubCategoria Requerido</ul>');
//    }
//    if (categoria == '') {
//        $('#categoria').text('');
//        $('#errorcategoria').text('');
//        $('#validationCategoria').after('<ul id="errorcategoria" class="validation-summary-errors text-danger">Campo Categoria Requerido</ul>');
//    }
//});


//$('#pscat_Id').change(function (e) {
//    var subcategoria = $.trim(this.value);
//    if (subcategoria != '') {
//        $('#subcategoria').text('');
//        $('#errorActividada').text('');
//    }
//});