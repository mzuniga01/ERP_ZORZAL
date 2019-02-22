var contador = 0;
//para q solo acepte letras(todo)
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}

//para q prod_CodigoBarras acepte numeros(create y editar)
$("#prod_CodigoBarras").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
//para q ent_FacturaCompra acepte numeros(create)
$("#ent_FacturaCompra").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
//para q cantidad acepte numeros(editar detalle)
    $("#cantidadEdit").on("keypress keyup blur", function (event) {
        //this.value = this.value.replace(/[^0-9\.]/g,'');
        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });
//para q cantidad acepte numeros(create)
$("#entd_Cantidad").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
//para q cantidad acepte numeros(edit)
$("#pscat_ISV_Edit").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
//para busqueda de productos
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
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",

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
    $("#entd_Cantidad").focus();
    //$("#cod").val(idItem);

});

//prueba de enter(Create)
$(document).keypress(function (e) {
    console.log('Hola', e.target.id);
    var IDInput = e.target.id;
    if (e.which == 13) {
        if (IDInput == 'prod_CodigoBarras') {
            /////
            $(function () {
                var cod_Barras = $("#prod_CodigoBarras").val();
                $.ajax({
                    url: "/Entrada/ProductosEnter",
                    method: "POST",
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({
                        cod_Barras: cod_Barras,
                    }),
                }).done(function (data) {
                    if (data.length > 0) {
                        $.each(data, function (key, val) {
                            console.log('each')
                            data_producto = val.prod_Codigo;
                            data_cantidadsistema = val.bodd_CantidadExistente;
                            data_cantidadfisica = val.invfd_Cantidad;
                            data_unidad = val.uni_Descripcion;
                            data_Descripcion = val.prod_Descripcion;
                            categoria = val.pscat_Descripcion;
                            Subcategoria = val.pscat_Id;
                            $("#prod_Codigo").val(data_producto);
                            $("#prod_Descripcion").val(data_Descripcion);
                            $("#uni_Id").val(data_unidad);
                            //$("#pscat_Id").val(Subcategoria);
                            $("#pscat_Id").val(categoria);
                            $("#entd_Cantidad").focus();
                        })
                    }
                    else {
                        $('#Error_Barras').text('');
                        $('#validationcodigoproducto').after('<ul id="Error_Barras" class="validation-summary-errors text-danger">*Producto no existe</ul>');
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


//Para mostrar datos en el modal de editar detalle entrada

$('#seleccionarModal').click(function () {
    console.log('ayo');
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
        $("#entd_Cantidad").focus();
        //$("#cod").val(idItem);

    });
});





//para eliminar detalle
$(document).on("click", "#tbentrada tbody tr td button#Eliminardetalleentrada", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');

    var Eliminar = {
        ent_Id: idItem,
    };
    $.ajax({
        url: "/Entrada/Eliminardetalleentrada",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ EntradaDetalle: Eliminar }),
    });
});


$(document).on("click", "#tbEntradaDetalle tbody tr td button#Eliminardetalleentrada_Edit", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');

    var Eliminar = {
        ent_Id: idItem,
    };
    $.ajax({
        url: "/Entrada/Eliminardetalleentrada",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ EntradaDetalle: Eliminar }),
    });
});
//para añadir codigo a la tabla temporal(Create)
$('#AgregarDetalleEntrada_Craete').click(function () {
    var codigobarra = $("#prod_CodigoBarras").val();
    var entrada = $("#ent_Id").val();
    var codigoproducto = $("#prod_Codigo").val();
    var cantidad = $("#entd_Cantidad").val();
    var unimedida = $("#uni_IdtItem").val();
    var desprod = $("#prod_Descripcion").val();

        if (entrada == '') {
            //$('#Errorentrada').text('');
            //$('#Errorcodigoproducto').text('');
            //$('#Errorcantidad').text('');
            console.log("entrada");
        }
        else if (codigoproducto == '') {
            $('#Mensajecodigo').text('');
            $('#Mensajecantidad').text('');
            $('#validationcodigoproducto').after('<ul id="Mensajecodigo" class="validation-summary-errors text-danger">Campo codigo producto Requerido</ul>');
            console.log("codigoproducto");
        }
        else if (cantidad == '') {
            $('#Mensajecodigo').text('');
            $('#Mensajecantidad').text('');
            $('#validationcantidad').after('<ul id="Mensajecantidad" class="validation-summary-errors text-danger">Campo cantidad Requerido</ul>');
            console.log("cantidad");
        }
        else if (cantidad == 0) {
            $('#Mensajecodigo').text('');
            $('#Mensajecantidad').text('');
            $('#validationcantidad').after('<ul id="Mensajecantidad" class="validation-summary-errors text-danger">La cantidad No debe ser 0.</ul>');
            console.log("cantidad");
        }
        else if (cantidad == 0.00) {
            $('#Mensajecodigo').text('');
            $('#Mensajecantidad').text('');
            $('#validationcantidad').after('<ul id="Mensajecantidad" class="validation-summary-errors text-danger">La cantidad No debe ser 0.</ul>');
            console.log("cantidad");
        }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        //copiar += "<td>" + $('#ent_Id option:selected').text() + "</td>";
        //copiar += "<td hidden id='ent_Id'>" + $('#ent_Id option:selected').val() + "</td>";

        copiar += "<td id = 'codigoproducto'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'desprod'>" + $('#prod_Descripcion').val() + "</td>";

        //copiar += "<td>" + $('#uni_Id option:selected').text() + "</td>";
        //copiar += "<td hidden id='uni_Id'>" + $('#unimedida option:selected').val() + "</td>";

        copiar += "<td id = 'Código Barra'>" + $('#prod_CodigoBarras').val() + "</td>";

        copiar += "<td id = 'cantidad'>" + $('#entd_Cantidad').val() + "</td>";

        copiar += "<td>" + '<button id="Eliminardetalleentrada_Edit" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        copiar += "</tr>";
        $('#tbEntradaDetalle').append(copiar);

        var EntradaDetalle = GetEntradaDetalle();
        $.ajax({
            url: "/Entrada/Guardardetalleentrada",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ entradadetalle: EntradaDetalle }),
        })
        .done(function (data) {
            $('#prod_CodigoBarras').val('');
            $('#prod_Codigo').val('');
            $("#uni_Id").val('');
            $('#entd_Cantidad').val('0.00');
            //
            $('#prod_Descripcion').val('');
            $('#pscat_Id').val('');

            $('#Mensajecodigo').text('');
            $('#Mensajecantidad').text('');

        });
    }
})
function GetEntradaDetalle() {
    var EntradaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        entd_Cantidad: $('#entd_Cantidad').val(),
        ent_UsuarioCrea: contador,
        ent_Id: contador,
        //Fecha: new Date($('#fechaCreate').val()),
        //Fecha: $('#fechaCreate').val(),
    };
    return EntradaDetalle;
}
////
$('#AgregarDetalleEntrada').click(function () {
    var codigobarra = $("#prod_CodigoBarras").val();
    var entrada = $("#ent_Id").val();
    var codigoproducto = $("#prod_Codigo").val();
    var cantidad = $("#entd_Cantidad").val();
    var unimedida = $("#uni_IdtItem").val();
    var desprod = $("#prod_Descripcion").val();

    if (entrada == '') {
        //$('#Errorentrada').text('');
        //$('#Errorcodigoproducto').text('');
        //$('#Errorcantidad').text('');
        console.log("entrada");
        }
        else if (codigoproducto == '') {
            $('#Mensajecodigo').text('');
            $('#Mensajecantidad').text('');
            $('#validationcodigoproducto').after('<ul id="Mensajecodigo" class="validation-summary-errors text-danger">Campo codigo producto Requerido</ul>');
            console.log("codigoproducto");
    }
    else if (cantidad == '') {
        $('#Mensajecodigo').text('');
        $('#Mensajecantidad').text('');
        $('#validationcantidad').after('<ul id="Mensajecantidad" class="validation-summary-errors text-danger">Campo cantidad Requerido</ul>');
        console.log("cantidad");
    }
    else if (cantidad == 0) {
        $('#Mensajecodigo').text('');
        $('#Mensajecantidad').text('');
        $('#validationcantidad').after('<ul id="Mensajecantidad" class="validation-summary-errors text-danger">La cantidad No debe ser 0.</ul>');
        console.log("cantidad");
    }
    else if (cantidad == 0.00) {
        $('#Mensajecodigo').text('');
        $('#Mensajecantidad').text('');
        $('#validationcantidad').after('<ul id="Mensajecantidad" class="validation-summary-errors text-danger">La cantidad No debe ser 0.</ul>');
        console.log("cantidad");
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        //copiar += "<td>" + $('#ent_Id option:selected').text() + "</td>";
        //copiar += "<td hidden id='ent_Id'>" + $('#ent_Id option:selected').val() + "</td>";

        copiar += "<td id = 'codigoproducto'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'desprod'>" + $('#prod_Descripcion').val() + "</td>";

        //copiar += "<td>" + $('#uni_Id option:selected').text() + "</td>";
        //copiar += "<td hidden id='uni_Id'>" + $('#unimedida option:selected').val() + "</td>";

        copiar += "<td id = 'Código Barra'>" + $('#prod_CodigoBarras').val() + "</td>";

        copiar += "<td id = 'cantidad'>" + $('#entd_Cantidad').val() + "</td>";

        copiar += "<td>" + '<button id="Eliminardetalleentrada" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        copiar += "</tr>";
        $('#tbentrada').append(copiar);

        var EntradaDetalle = GetEntradaDetalle();
        $.ajax({
            url: "/Entrada/Guardardetalleentrada",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ entradadetalle: EntradaDetalle }),
        })
            .done(function (data) {
                $('#prod_CodigoBarras').val('');
                $('#prod_Codigo').val('');
                $("#uni_Id").val('');
                $('#entd_Cantidad').val('0.00');
                //
                $('#prod_Descripcion').val('');
                $('#pscat_Id').val('');

                $('#Mensajecodigo').text('');
                $('#Mensajecantidad').text('');

            });
    }
})
function GetEntradaDetalle() {
    var EntradaDetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        entd_Cantidad: $('#entd_Cantidad').val(),
        ent_UsuarioCrea: contador,
        ent_Id: contador,
        //Fecha: new Date($('#fechaCreate').val()),
        //Fecha: $('#fechaCreate').val(),
    };
    return EntradaDetalle;
}


$(document).on("click", "#Table_BuscarProducto tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    contentItem = $(this).closest('tr').data('content');
    uni_IdtItem = $(this).closest('tr').data('keyboard');
    psubctItem = $(this).closest('tr').data('container');
    pcatItem = $(this).closest('tr').data('interval');
    $("#prod_Codigo").val(idItem);
    $("#prod_Descripcion").val(contentItem);
    $("#uni_Id").val(uni_IdtItem);
    $("#pscat_Id").val(psubctItem);
    $("#pcat_Id").val(pcatItem);
    //$("#cod").val(idItem);
});


//actualizar Detalle Entrada

function EditStudentRecord(entd_Id) {
    //var cantidad = $("#entd_Cantidad").val();
    //console.log(cantidad);
    //if (cantidad == "") {
    //    $("#Msjcantidad").text("No se puede Guardar UNA CANTIDAD EN 0.");
    //} else {

    $("#MsjError").text("");

    $.ajax({
        url: "/Entrada/GetDetalleEntrada",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ entd_Id }),
    })
    .done(function (data) {
        $.each(data, function (i, item) {
            $("#entd_Id").val(item.entd_Id);
            $("#ent_Id_Edit").val(item.ent_Id);
            $("#coProductoEdit").val(item.prod_Codigo);
            $("#cantidadEdit").val(item.entd_Cantidad);

            $("#MyModal").modal();

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
    var canti = $("#cantidadEdit").val();
    if (canti == 0) {
        $("#Msjcantidad").text("No se puede Guardar UNA CANTIDAD EN 0.");
    }
    else if (canti == null) {
        $("#Msjcantidad").text("No se puede Guardar UNA CANTIDAD EN 0.");
    }
    else {
        $.ajax({
            type: "Post",
            url: "/Entrada/UpdateEntradaDetalle",
            data: data,
            success: function (result) {
                if (result == '-1')
                    $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
                else
                    //$("#MyModal").modal("hide");
                    location.reload();
            }
        });
    }
    
})





//codigo para anular entradad(retorna razon)
$('#AnularEntrada').click(function () {
    var anular = $("#entd_RazonAnulada").val();
    console.log(anular);
    if (anular == '')
    {
        valido = document.getElementById('RazonANULADA');
        valido.innerText = "La razón Anulado es requerida";
    } else {
        var anularentrada = GetAnualarEntrada();
        $.ajax({
            url: "/Entrada/EstadoAnular",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ cambiaAnular: anularentrada }),
        })
        .done(function (data) {
            $("#entd_RazonAnulada").val('');
            //$('#ent_Id').val('');
            location.reload();
            //window.location.reload();
        });
    }
     window.location.reload();
})
function GetAnualarEntrada() {

    var cambiaAnular = {
        entd_RazonAnulada: $("#entd_RazonAnulada").val(),
        ent_Id: $('#ent_Id').val(),
    };
    return cambiaAnular;
}


//Encargado de Bodega
$(document).change("#prov_Id", function () {
    GetRTNproveedor();
});

function GetRTNproveedor() {
    var codigoProveedor = $('#prov_Id').val();
    $.ajax({
        url: "/Entrada/GetRTNProveedor",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ codigoProveedor: codigoProveedor }),
    })
    .done(function (data) {
        if (data.length > 0) {
            $('#Rtn').empty();
            $.each(data, function (key, val) {
                $('#Rtn').val(val.prov_RTN);
                console.log(val.codigoProveedor);
            });
            $('#Rtn').trigger("chosen:updated");
        }
        else {
            $('#Rtn').empty();
            $('#Rtn').val(val.prov_RTN);
            console.log(val.prov_Id);
        }
    });
}
