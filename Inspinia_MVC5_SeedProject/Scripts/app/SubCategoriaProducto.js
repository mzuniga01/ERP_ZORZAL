////Get SubCategoria
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

//$(document).ready(function () {
//    GetCategoriaProducto()
//});

$(document).keydown(function (e) {
    if ((e.key == 'g' || e.key == 'G') && (e.ctrlKey || e.metaKey)) {
        e.preventDefault();
        //alert("Ctrl-g pressed");
        $("form").submit();
        return false;
    }
    return true;
});


//$(document).ready(function () {
//var cate = $('#pcat_Id').val();
//if (depto === '') {
//    document.getElementById("pscat_Id").disabled = true;
//}
//else {

//}
//});

//Validacion de solo letras
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz1234567890";
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