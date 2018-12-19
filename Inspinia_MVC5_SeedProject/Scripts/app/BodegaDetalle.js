var contador = 0;

///Busqueda products
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
///Fin


//Agregar Detalle a Bodega
$('#AgregarBodegaDetalle').click(function () {
    var Producto = $('#prod_Codigo').val();
    var Preorden = $('#bodd_PuntoReorden').val();
    var Cminima = $('#bodd_CantidadMinima').val();
    var Cmaxima = $('#bodd_CantidadMaxima').val();
    var Costo = $('#bodd_Costo').val();
    var Cpromedio = $('#bodd_CostoPromedio').val();
    

    if (Producto == '')
    {
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorProducto_Create').after('<ul id="ErrorProducto_Create" class="validation-summary-errors text-danger">Campo Producto Requerido</ul>');
    }
    else if (Preorden == '') 
    {
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorPuntoReorden_Create').after('<ul id="ErrorPuntoReorden_Create" class="validation-summary-errors text-danger">Campo Punto Reorden Requerido</ul>');
    }
    else if (Cminima == '') 
    {
        
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorCantidadMinima_Create').after('<ul id="ErrorCantidadMinima_Create" class="validation-summary-errors text-danger">Campo Canidad Mínima Requerido</ul>');
    }
    else if (Cmaxima == '') 
    {
        
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorantidadMaxima_Create').after('<ul id="ErrorantidadMaxima_Create" class="validation-summary-errors text-danger">Campo Cantidad Máxima Requerido</ul>');
    }
    else if (Costo == '') 
    {
        
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorCosto_Create').after('<ul id="ErrorCosto_Create" class="validation-summary-errors text-danger">Campo Costo Requerido</ul>');
    }
    else if (Cpromedio == '') 
    {
       
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorCostoPromedio_Create').after('<ul id="ErrorCostoPromedio_Create" class="validation-summary-errors text-danger">Campo Costo Promedio Requerido</ul>');
    }
   
    else {
        //Aqui importa el orden
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'prod_Codigo'>" + $('#prod_Codigo').val() + "</td>";
        copiar += "<td id = 'bodd_PuntoReorden'>" + $('#bodd_PuntoReorden').val() + "</td>";// aqui va el campo y luego se llena con el id del mismo, que ya ha capturado el valor
        copiar += "<td id = 'bodd_CantidadMinima'>" + $('#bodd_CantidadMinima').val() + "</td>";
        copiar += "<td id = 'bodd_CantidadMaxima'>" + $('#bodd_CantidadMaxima').val() + "</td>";
        copiar += "<td id = 'bodd_Costo'>" + $('#bodd_Costo').val() + "</td>";
        copiar += "<td id = 'bodd_CostoPromedio'>" + $('#bodd_CostoPromedio').val() + "</td>";
        copiar += "<td>" + '<button id="removeBodegaDetalle" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";

        $('#tblBodegadetalle_Create').append(copiar);

        var BODEGADETALLE = Getbodegadetalle
        $.ajax({
            url: "/Bodega/SaveBodegaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ BODEGADETALLE: BODEGADETALLE }),
        })
            .done(function (data) {
                $('#prod_Codigo').text('');
                $('#bodd_PuntoReorden').text('');
                $('#bodd_CantidadMinima').text('');
                $('#bodd_CantidadMaxima').text('');
                $('#bodd_Costo').text('');
                $('#bodd_CostoPromedio').text('');
            });



    }
})
function Getbodegadetalle() {
    var bodegadetalle = {
        prod_Codigo: $('#prod_Codigo').val(),
        bodd_puntoReorden: $('#bodd_PuntoReorden').val(),
        bodd_cantidadMinima: $('#bodd_CantidadMinima').val(),
        bodd_cantidadMaxima: $('#bodd_CantidadMaxima').val(), 
        bodd_costo: $('#bodd_Costo').val(),
        bodd_costoPromedio: $('#bodd_CostoPromedio').val(),
        bodd_Id : contador,
        //Fecha: $('#fechaCreate').val(),
    };
    return bodegadetalle;
}
//Fin

//Remover Detalle
$(document).on("click", "#tblBodegadetalle_Create tbody tr td button#removeBodegaDetalle", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var BorrarItems = {
        bodd_Id : idItem,
    };
    $.ajax({
        url: "/Bodega/removeBodegaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ BorrarItem: BorrarItems }),
    });
});
//Fin


