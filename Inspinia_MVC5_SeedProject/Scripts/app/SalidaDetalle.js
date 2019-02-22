var contador = 0;

function validateMyForm() {
    if (contador == 0) {
        $('#exampleModalCenter').modal('show');
        v1 = false;
    }
    else {
        v1 = true 
    }
    if ($('#CodigoError').text('') != '') {
        //    alert("validation failed false");
        $('#fact_Codigo').focus();
        v2 = true ;
    } else {

        v2 = true 
    }
    if ($('#tsal_Id').val('') != '') {
        //    alert("validation failed false");
        $('#NombreError').text('');
        $('#validationtsal_Id').after('<ul id="NombreError" class="validation-summary-errors text-danger">Campo Requerido</ul>');
        v3 = false
    }
    else {
        v3 = true
    }
    if (!v1 || !v2 || !v3) {
        return false($('#tsal_Id').val());
    }
    else {
        return true
    }

}
$(document).ready(function () {
    var getBrowserInfo = function () {
        var ua = navigator.userAgent, tem,
        M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
        if (/trident/i.test(M[1])) {
            tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
            return 'IE ' + (tem[1] || '');
        }
        if (M[1] === 'Chrome') {
            tem = ua.match(/\b(OPR|Edge)\/(\d+)/);
            if (tem != null) return tem.slice(1).join(' ').replace('OPR', 'Opera');
        }
        M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
        if ((tem = ua.match(/version\/(\d+)/i)) != null) M.splice(1, 1, tem[1]);
        return M.join(' ');
    };
    console.log(getBrowserInfo());

    console.log(bowser.name, bowser.version);
});

$(document).ready(function () {
    $('#tbSalidaDetalle').DataTable(
        {
            "searching": true,
            "scrollX": true,

            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sSearch": "Buscar",
                "sLengthMenu": "Mostrar _MENU_ registros por página",
                "sInfo": "Mostrando _START_ a _END_ Entradas",
            }
        });
});

////Tabla de Busqueda Generica
//$(document).ready(function () {
//    $('#Table_BuscarProducto').DataTable(
//        {
//            "searching": false,
//            "lengthChange": false,

//            "oLanguage": {
//                "oPaginate": {
//                    "sNext": "Siguiente",
//                    "sPrevious": "Anterior",
//                },
//                "sEmptyTable": "No hay registros",
//                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
//                "sSearch": "Buscar",
//                "sInfo": "Mostrando _START_ a _END_ Entradas",

//            }
//        });

//    var $rows = $('#Table_BuscarProducto tr');
//    $("#search").keyup(function () {
//        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

//        $rows.show().filter(function () {
//            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
//            return !~text.indexOf(val);
//        }).hide();
//        //$rows.show().filter(function () {
//        //    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
//        //    return !~text.indexOf(val);
//        //}).hide();

//    });
//});

$(document).on("click", "#Table_BuscarProductoD tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    contentItem = $(this).closest('tr').data('content');
    uni_IdtItem = $(this).closest('tr').data('keyboard');
    psubctItem = $(this).closest('tr').data('container');
    pcatItem = $(this).closest('tr').data('pcat');
    prod_CodigoBarrasItem = $(this).closest('tr').data('cod_barras');
    $("#prod_Codigo").val(idItem);
    $("#prod_Descripcion").val(contentItem);
    $("#uni_Id").val(uni_IdtItem);
    $("#pscat_Id").val(psubctItem);
    $("#pcat_Id").val(pcatItem);
    $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
    $("#sald_Cantidad").focus();
    //$("#cod").val(idItem);
});
//

//Agregar detalle por medio de codigo de Barra
$('#prod_CodigoBarras').focus(function () {
    ListaProductos()
})

var contador = 0;
$(document).keypress(function (e) {

    var bod_Id = $('#bod_Id').val()
    var prod_CodigoBarras = $('#prod_CodigoBarras').val()
    console.log('Hola', e.target.id);
    var IDInput = e.target.id;
    if (e.which == 13) {
        if (IDInput == 'prod_CodigoBarras') {
            /////
            $(function () {
                $("#prod_CodigoBarras").val();
                var cod_Barras = $("#prod_CodigoBarras").val();
                $.ajax({
                    url: "/Salida/GetProdCodBar",
                    method: "POST",
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({
                        bod_Id: bod_Id, prod_CodigoBarras: prod_CodigoBarras
                    }),
                }).done(function (data) {
                    console.log(data);
                    if (data.length > 0) {
                        $.each(data, function (key, value) {
                            $('#prod_Codigo').val(value.prod_Codigo);
                            $('#prod_Descripcion').val(value.prod_Descripcion);
                            $("#uni_Id").val(value.uni_Descripcion);
                            $("#pscat_Id").val(value.pscat_Descripcion);
                            $("#pcat_Id").val(value.pcat_Nombre);
                        })
                        $('#prod_CodigoBarras').text('');
                        $("#sald_Cantidad").focus();
                        ///--
                        //$.ajax({
                        //    url: "/InventarioFisico/ProductosRepetidos",
                        //    method: "POST",
                        //    dataType: 'json',
                        //    contentType: "application/json; charset=utf-8",
                        //    data: JSON.stringify({ data_producto: data_producto }),
                        //})
                        //    .done(function (datos) {
                        //        //if (datos.length > 0) {
                        //        if (datos == data_producto) {
                        //            //alert('Es Igual.')
                                    
                        //        }
                        //        else {
                        //            //alert('NO ES IGUAL')

                        //        }


                        //    })

                    }
                    else {
                        $('#prod_CodigoBarras').text('');
                        $('#validationprod_CodigoBarras').after('<ul id="Error_Barras" class="validation-summary-errors text-danger">*Producto no existe</ul>');
                    }
                });
                return false;
            });
            return false;
        }
        else
            return false;
    }
});

//function GetProdCodBar() {
//    var bod_Id = $('#bod_Id').val()
//    var prod_CodigoBarras = $('#prod_CodigoBarras').val()
//    $.ajax({
//        url: "/Salida/GetProdCodBar",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ bod_Id: bod_Id, prod_CodigoBarras: prod_CodigoBarras }),
//    })
//        .done(function (data) {
//            console.log(data)
//            $.each(data, function (key, value) {
//                $("#prod_CodigoBarras").val(value.prod_CodigoBarras);
//                $("#prod_Descripcion").val(value.prod_Codigo);
//                $("#sald_Cantidad").focus();
//            })
//        })
//}
//    $('#prod_CodigoBarras').keydown(function (e) {
//        if (e.keyCode == 13) {
//            var bod_Id = $('#bod_Id').val()
//            var prod_CodigoBarras = $('#prod_CodigoBarras').val()
//            $.ajax({
//                url: "/Salida/GetProdCodBar",
//                method: "POST",
//                dataType: 'json',
//                contentType: "application/json; charset=utf-8",
//                data: JSON.stringify({ bod_Id: bod_Id, prod_CodigoBarras: prod_CodigoBarras }),
//            })
//                .done(function (data) {
//                    console.log(data)
//                    $.each(data, function (key, value) {
//                        $("#prod_CodigoBarras").val(data.prod_Codigo);
//                        $("#prod_Codigo").val(data.prod_Descripcion);
//                        $("#prod_Descripcion").val(value.prod_Descripcion);
//                        $("#uni_Id").val(uni_IdtItem);
//                        $("#pscat_Id").val(psubctItem);
//                        $("#pcat_Id").val(pcatItem);
//                        $("#sald_Cantidad").focus();
//                    }) 
//                })
//                $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
//                $("#prod_Codigo").val(idItem);
//                $("#prod_Descripcion").val(contentItem);
//                $("#uni_Id").val(uni_IdtItem);
//                $("#pscat_Id").val(psubctItem);
//                $("#pcat_Id").val(pcatItem);
//                $("#sald_Cantidad").focus();
//            //$("#cod").val(idItem);
//            return false;
//        }
//            });
//            console.log('prueba');
//            $("#prod_CodigoBarras").val(prod_CodigoBarrasItem);
//            $("#prod_Codigo").val(idItem);
//            $("#prod_Descripcion").val(contentItem);
//            $("#uni_Id").val(uni_IdtItem);
//            $("#pscat_Id").val(psubctItem);
//            $("#pcat_Id").val(pcatItem);
//            $("#sald_Cantidad").focus();
//            return false;
//    );


//Tabla del Detalle
$(document).ready(function () {
    $('#tblSalidaDetalle1').DataTable(
        {
            "searching": true,
            "lengthChange": false,
            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sEmptyTable": "Agregue un Producdo",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",

            }
        });

    var $rows = $('#tblSalidaDetalle1 tr');
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
//


//DatePicker


function GetSalidaDetalle() {
    var SalidaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        sald_Cantidad: $('#sald_Cantidad').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}



$('#AgregarSalidaDetalle').click(function () {
    var bodd_Id = $('#bodd_Id').val();
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Unidad_Medida = $('#pscat_Id').val();
    var Cantidad = $('#sald_Cantidad').val();


    if (Producto == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationCodigoCreate').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    else if (Cantidad == '' || Cantidad < 1) {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#sald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        //copiar += "<td>" + $('#CodTipoCasoExitoCreate option:selected').text() + "</td>";
        copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
        copiar += "<td id = 'Unidad_Medida'>" + $('#pscat_Id').val() + "</td>";
        copiar += "<td id = 'Cantidad'>" + $('#sald_Cantidad').val() + "</td>";
        copiar += "<td>" + '<button id="removeSalidaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblSalidaDetalle').append(copiar);


        var tbSalidaDetalle = GetSalidaDetalle();
        $.ajax({
            url: "/Salida/SaveSalidaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle }),
        })
            .done(function (data) {
                    $('#prod_Codigo').val('');
                    $('#prod_Descripcion').val('');
                $('#pscat_Id').val(''); 
                $('#uni_Id').val(''); 
                $('#pcat_Id').val(''); 

                $("#prod_CodigoBarras").val('');
                $('#sald_Cantidad').val('0.00');
                    $('#MessageError').text('');
                    $('#NombreError').text('');
                    console.log('Hola');
                });



    }

});

$(document).on("click", "#tblSalidaDetalle tbody tr td button#removeSalidaDetalle", function () {
    contador = contador - 1;
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var SalidaDetalle = {
        sald_UsuarioCrea: idItem,
    };
    $.ajax({
        url: "/Salida/RemoveSalidaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ SalidaDetalle: SalidaDetalle }),
    });
});


function DeleteSalidaDetalle(sald_Id) {
    $.ajax({
        url: "/Salida/DeleteSalidaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ sald_Id: sald_Id }),
    })
        .done(function (data) {
            console.log(data);
            location.reload();
            })
        }

function EditSalidaDetalles(sald_Id) {
    $.ajax({
        url: "/Salida/getSalidaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ sald_Id: sald_Id }),

    })
        .done(function (data) {
            console.log(data);
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    console.log(item);
                    $("#sal_Id_SD").val(item.sal_Id);
                    $("#sald_Id_SD").val(item.sald_Id);
                    $("#prod_Codigo_SD").val(item.prod_Codigo);
                    $("#sald_Cantidad_SD").val(item.sald_Cantidad);
                    $("#prod_Descripcion_SD").val(item.prod_Descripcion); 
                    $("#pcat_Nombre_SD").val(item.pcat_Nombre); 
                    $("#pscat_Descripcion_SD").val(item.pscat_Descripcion); 
                    $("#uni_Descripcion_SD").val(item.uni_Descripcion); 
                    //$("#pedd_FechaCrea_Ped").val(item.pedd_FechaCrea);
                    $("#EditSalidaDetalle").modal();
                })
            }
        })
}


$("#BtnsubmitEdit").click(function () {
    var Cantidad = $('#sald_Cantidad_SD').val();
    if (Cantidad == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#validationsald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
    }
    else {
        var sal_id = $('#sald_Id').val();
        var data = $("#SubmitForm").serializeArray();
        $.ajax({
            type: "Post",
            url: "/Salida/EditSalidaDetalle",
            data: data,
            success: function (result) {
                if (result == '-1')
                    $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
                else
                    console.log(result);
                window.location.href = '/Salida/Edit/' + result;
            }
        });
    }
})





//////////////////////////////////////////////////////////////////////////////////////
//Edit


function GetSalidaDetalle() {
    var SalidaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        sald_Cantidad: $('#sald_Cantidad').val(),
        sal_Id: $('#sal_Id').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}



$('#AgregarSalidaDetalleEdit').click(function () {
    var Producto = $('#prod_CodigoBarras').val();
    var Cantidad = $('#sald_Cantidad').val();
    if (Producto == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#validationprod_CodigoBarras').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    else if (Cantidad == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#sald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad Requerido</ul>');
    }
    else if (Cantidad < 1) {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#sald_Cantidad').after('<ul id="NombreError" class="validation-summary-errors text-danger">Cantidad No puede se Cero</ul>');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        //copiar += "<td>" + $('#CodTipoCasoExitoCreate option:selected').text() + "</td>";
        copiar += "<td id = 'Cod_Producto'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'Producto'>" + $('#prod_Descripcion').val() + "</td>";
        copiar += "<td id = 'Unidad_Medida'>" + $('#uni_Id').val() + "</td>";
        copiar += "<td id = 'Cantidad'>" + $('#sald_Cantidad').val() + "</td>";
        copiar += "<td>" + '<button id="removeSalidaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#tblSalidaDetalleEdit').append(copiar);


        var tbSalidaDetalle = GetSalidaDetalle();
        $.ajax({
            url: "/Salida/SaveSalidaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ SalidaDetalle: tbSalidaDetalle }),
        })
            .done(function (data) {
                $('#prod_Codigo').val('');
                $('#prod_Descripcion').val('');
                $('#pscat_Id').val('');
                $('#uni_Id').val('');
                $('#pcat_Id').val('');

                $("#prod_CodigoBarras").val('');
                $('#sald_Cantidad').val('0.00');
                $('#MessageError').text('');
                $('#NombreError').text('');
                console.log(data);
            });



    }

});



function GetNewSalidaDetalle() {
    var SalidaDetalle = {
        sal_Id: $('#sal_Id').val(),
        prod_Codigo: $('#prod_Codigo').val(),
        sald_Cantidad: $('#sald_Cantidad').val(),
        sald_UsuarioCrea: contador
    };
    return SalidaDetalle;
}


$('#btnCreateSalidaDetalle').click(function () {
    var Cod_Producto = $('#prod_Codigo').val();
    var Producto = $('#prod_Descripcion').val();
    var Cantidad = $('#sald_Cantidad').val();
    console.log('Funca');
    var tbSalidaDetalle = GetNewSalidaDetalle();
    $.ajax({
        url: "/Salida/SaveNewDatail",
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
})


