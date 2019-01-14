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
            $('#pscat_Id').empty();
            $('#pscat_Id').append("<option value=" + val.mun_Codigo + ">" + " Seleccione SubCategoria</option>");
        }
    });
}

$(document).ready(function () {
    $("#prod_Codigo")[0].maxLength = 15;

})

//Fin



//codigo Producto
$(document).on("change", "#pcat_Id", "pscat_Id", function () {
    GetValue();
});

//$(document).on('change', function () {
//    var carbon = $('#pcat_Id').val();
//    var fiver = $('#pscat_Id').val();
//});

function GetValue() {
    console.log('Entra pero no hace nada');
    var cate = $('#pcat_Id').val();
    var subcate = $('#pscat_Id').val();
    $.ajax({
        url: "/Producto/GetValue",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ cate: pcat_Id, subcate: pscat_Id }),
    })
    .done(function (data) {
        $('#pcat_Id').val('');
        $('#pscat_Id').val('');
    });
}

//$("#pscat_Id").on("click", GetValue);

//function GetValue() {
//    $.ajax({
//        url: "/Producto/GetValue",
//        success: function (respuesta) {

//            var pcat_Id = $("#lista-usuarios");
//            $.each(respuesta.data, function (index, elemento) {
//                listaUsuarios.append(
//                    '<div>'
//                  + '<p>' + elemento.first_name + ' ' + elemento.last_name + '</p>'
//                  + '<img src=' + elemento.avatar + '></img>'
//                  + '</div>'
//                );
//            });
//        },
//        error: function () {
//            console.log("No se ha podido obtener la información");
//        }
//    });
//}

//function GetValue_Categoria(pagina, data) {
//    var pcat_Id = $("#pcat_Id option:selected").val();    
//    $.ajax({
//        type: "GET",
//        url: "/Producto/GetValue",
//        data: "#prod_Codigo" + pcat_Id,
//        success: function (data) {
//            content.html(data);
//        }
//    });
//}

//function GetValue_SubCategoria(pagina, data) {
//    var pscat_Id = $("#pscat_Id option:selected").val();    
//    $.ajax({
//        type: "GET",
//        url: "/Producto/GetValue",
//        data: "#prod_Codigo" + pscat_Id,
//        success: function (data) {
//            content.html(data);
//        }
//    });
//}

$(document).ready(function () {
    $.ajax({
        url: "/Producto/GetValue",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ pcat_Id: $('#pcat_Id_Id').val(), pscat_Id: $('#pscat_Id_Id').val() }),
    })
});