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
        });
    });

$(document).on("click", "#Table_BuscarProducto tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    $("#prod_Codigo").val(idItem);
    //$("#cod").val(idItem);
});


$('#AgregarBodegaDetalle').click(function () {
    var Pcat = $('#pscat_Id').val();
    var Pscat = $('#pcat_Id').val();
    var Unidad = $('#uni_id').val();
    var Preorden = $('#bodd_PuntoReorden').val();
    var Cminima = $('#bodd_CantidadMinima').val();
    var Cmaxima = $('#bodd_CantidadMaxima').val();
    var Costo = $('#bodd_Costo').val();
    var Cpromedio = $('#bodd_CostoPromedio').val();
    var Producto = $('#prod_Codigo').val();

    if ( Pcat == '') 
    {
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorCategoria_Create').after('<ul id="ErrorCategoria_Create" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
    
    else if (Pscat == '') 
    {
        
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorSubCat_Create').after('<ul id="ErrorSubCat_Create" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
    else if (bodd_PuntoReorden == '') 
    {
        
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorPuntoReorden_Create').after('<ul id="ErrorPuntoReorden_Create" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
    else if (Cminima == '') 
    {
        
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorCantidadMinima_Create').after('<ul id="ErrorCantidadMinima_Create" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
    else if (Cmaxima == '') 
    {
        
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorantidadMaxima_Create').after('<ul id="ErrorantidadMaxima_Create" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
    else if (Costo == '') 
    {
        
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorCosto_Create').after('<ul id="ErrorCosto_Create" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
    else if (Cpromedio == '') 
    {
       
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorCostoPromedio_Create').after('<ul id="ErrorCostoPromedio_Create" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
    else if (Unidad == '') {
       
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorUnidad_Create').after('<ul id="ErrorUnidad_Create" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
    else if (Producto == '') {
       
        $('#bodd_PuntoReorden').text('');
        $('#bodd_CantidadMinima').text('');
        $('#bodd_CantidadMaxima').text('');
        $('#bodd_Costo').text('');
        $('#bodd_CostoPromedio').text('');
        $('#ErrorUnidad_Create').after('<ul id="ErrorProducto_Create" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
    }
    else {
        //Aqui importa el orden
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td>" + $('#prod_Codigo option:selected').text() + "</td>"; //para los droplist
        copiar += "<td hidden id='Producto'>" + $('#prod_Codigo option:selected').val() + "</td>"; //este tambien, aqui se captura el id 
        copiar += "<td>" + $('#pcat_Id option:selected').text() + "</td>"; 
        copiar += "<td hidden id='Pcat'>" + $('#pcat_Id option:selected').val() + "</td>"; 
        copiar += "<td>" + $('#pscat_Id option:selected').text() + "</td>"; 
        copiar += "<td hidden id='Pscat'>" + $('#pscat_Id option:selected').val() + "</td>"; 
        copiar += "<td>" + $('#uni_Id option:selected').text() + "</td>"; 
        copiar += "<td hidden id='Unidad'>" + $('#uni_Id option:selected').val() + "</td>"; 
        copiar += "<td id = 'bodd_PuntoReorden'>" + $('#bodd_PuntoReorden').val() + "</td>";// aqui va el campo y luego se llena con el id del mismo, que ya ha capturado el valor
        copiar += "<td id = 'bodd_CantidadMinima'>" + $('#bodd_CantidadMinima').val() + "</td>";
        copiar += "<td id = 'bodd_CantidadMaxima'>" + $('#bodd_CantidadMaxima').val() + "</td>";
        copiar += "<td id = 'bodd_Costo'>" + $('#bodd_Costo').val() + "</td>";
        copiar += "<td id = 'bodd_CostoPromedio'>" + $('#bodd_CostoPromedio').val() + "</td>";
        copiar += "<td>" + '<button id="removeCasoExito" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";

        $('#tblBodegadetalle_Create').append(copiar);

        var bodegad = Getbodegadetalle
        $.ajax({
            url: "/Bodega/SaveBodegaDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ bodegadetalle : bodegad }),
        })
            .done(function (data) {
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
        bodd_puntoReorden: $('#pscat_Id').val(),
        bodd_cantidadMinima: $('#pcat_Id').val(),
        bodd_puntoReorden: $('#bodd_PuntoReorden').val(),
        bodd_cantidadMinima: $('#bodd_CantidadMinima').val(),
        bodd_cantidadMaxima: $('#bodd_CantidadMaxima').val(), 
        bodd_costoPromedio: $('#bodd_Costo').val(),
        bodd_costoPromedio: $('#bodd_CostoPromedio').val(),
        tblBodegadetalle_Create: contador,
        //Fecha: $('#fechaCreate').val(),
    };
    return Getbodegadetalle;
}

$(document).on("change", "#dep_Codigo", function () {
    GetMunicipios();
});

///Get Municipio
function GetMunicipios() {
    var dep_Codigo = $('#dep_Codigo').val();
    $.ajax({
        url: "/Bodega/GetMunicipios",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ dep_Codigo: dep_Codigo }),
    })
        .done(function (data) {
            if (data.length > 0) {
                $('#mun_Codigo').empty();
                $('#mun_Codigo').append("<option value=''>Seleccione</option>");
                $.each(data, function (key, val) {
                    $('#mun_Codigo').append("<option value=" + val.mun_Codigo + ">" + val.mun_Nombre + "</option>");
                });
                $('#mun_Codigo').trigger("chosen:updated");
            }
            else {
                $('#mun_Codigo').empty();
                $('#mun_Codigo').append("<option value=''>Seleccione</option>");
            }
        });
}
