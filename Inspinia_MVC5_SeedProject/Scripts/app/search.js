//$(document).ready(function () {
//    $('#pcat_Id').change(function () {
//        $.get('/Producto/GetScatList', { pcat_Id: $('#pcat_Id').val() }, function (data) {
//            $("#pscat_Id").empty();
//            $.each(data, function (index, row) {
//                $("#pscat_Id").append("<option value ='" + row.pscat_Id + "'>" + row.pscat_Descripcion + "</option>")
//            });
//        });
//    })
//});

$(document).ready(function () {
    $('#pcat_Id').change(function () {
        $.get('/Producto/GetScatList', { pcat_Id: $('#pcat_Id').val() }, function (data) {
            if ($("#pscat_Id").val('')) {
                $("#pscat_Id").empty();
                $.each(data, function (index, row) {
                    $("#pscat_Id").append("<option value ='" + row.pscat_Id + "'>" + row.pscat_Descripcion + "</option>")
                });
            }
            else {
                $.each(data, function (index, row) {
                    $("#pscat_Id").append("<option value ='0'>No hay Categorias</option>")
                });
            };
            
        });
    })
});

//$(document).ready(function () {
//    $('#pcat_Id').change(function () {
//        $.get('/Producto/GetScatLista', { pcat_Id: $('#pcat_Id').val() }, function (data) {
//            if ($("#pscat_Id").val('')) {
//                $("#pscat_Id").empty();
//                $.each(data, function (index, row) {
//                    $("#pscat_Id").append("<option value ='" + row.pscat_Id + "'>" + row.pscat_Descripcion + "</option>")
//                });
//            }
//            else {
//                $.each(data, function (index, row) {
//                    $("#pscat_Id").append("<option value ='0'>No hay Categorias</option>")
//                });
//            };

//        });
//    })
//});

//document.querySelector("#buscar").onchange = function () {
//    $TableFilter("#tabla", this.value);
//}

//$TableFilter = function (id, value) {
//    var rows = document.querySelectorAll(id + ' tbody tr');

//    for (var i = 0; i < rows.length; i++) {
//        var showRow = false;

//        var row = rows[i];
//        row.style.display = 'none';

//        for (var x = 0; x < row.childElementCount; x++) {
//            if (row.children[x].textContent.toLowerCase().indexOf(value.toLowerCase().trim()) > -1) {
//                showRow = true;
//                break;
//            }
//        }

//        if (showRow) {
//            row.style.display = null;
//        }
//    }
//}