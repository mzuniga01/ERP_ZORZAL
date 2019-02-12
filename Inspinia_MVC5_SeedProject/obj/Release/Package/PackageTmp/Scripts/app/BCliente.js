//$(document).on("click", "#search", function () {
//    GetBusquedaClientes();
//});

//function GetBusquedaClientes() {

//    var Identificacion = $('#identificacion').val();
//    if (Identificacion == '') {
//        Identificacion = null;
//    }
//    var Nombres = $('#Nombres').val();
//    if (Nombres == '') {
//        Nombres = null;
//    }
//    var Telefono = $('#telefono').val();
//    if (Telefono == '') {
//        Telefono = null;
//    }
//    $.ajax({
//        url: "/Cliente/GetBusquedaClientes",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ Identificacion: Identificacion, Nombres: Nombres, Telefono: Telefono }),
//    })
//    .done(function (data) {
//        if (data.length > 0) {
//            $.each(data, function (key, val) {
//                //$('#Cliente').append("<tr><td>" + val.clte_Identificacion + "</td></tr>"
//                //    );

//                    var $rows = $('#Cliente tr');
//                    $(data).each(function () {
//                        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

//                        $rows.show().filter(function () {
//                            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
//                            return !~text.indexOf(val);
//                        }).hide();
//                    });
//            });
//            console.log(data)
//        }
//        else {
//            $('#Cliente').empty();
//        }
//    });
//}

//$("#search").click(function () {
//    var $rows = $('#Cliente tr');
//    $('#identificacion').each(function () {
//        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

//        $rows.show().filter(function () {
//            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
//            return !~text.indexOf(val);
//        }).hide();
//    });
//});

//function BCliente() {
//    var $rows = $('#Cliente tr');
//    $('#identificacion').each(function () {
//        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
//        $rows.show().filter(function () {
//            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
//            return !~text.indexOf(val);
//        }).hide();
//    });

//}