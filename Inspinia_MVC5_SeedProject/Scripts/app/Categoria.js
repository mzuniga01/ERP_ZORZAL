var contador = 0;

$('#AgregarSubCategoria').click(function () {
    var Descripcion = $('#pscat_Descripcion').val();
    
    if (Descripcion == '') 
    {
        $('#ErrorDescripcionCreate').text('');
        $('#DescripcionValidation').after('<ul id="ErrorDescripcionCreate" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');

    }

    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'DescripcionCreate'>" + $('#pscat_Descripcion').val() + "</td>";
        copiar += "<td>" + '<button id="removerSubCategoria" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#SubCategoria').append(copiar)


        var SubCategoria = GetSubCategoria();

        $.ajax({
            url: "/ProductoCategoria/GuardarSubCategoria",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ tbsubcategoria: SubCategoria }),
        })
            .done(function (data) {
                $('#DescripcionCreate').val('');
                $('#ErrorMessage').text('');
                $('#ErrorDescripcion').text('');
                $('#ErrorFecha').text('');
            });
    }
});



function GetSubCategoria() {
    var SubCategoria = {
        Descripcion: $('#Descripcion').val(),
        pscat_Id : contador,
        
    };
    return SubCategoria;
}

$(document).on("click", "#SubCategoria tbody tr td button#removerSubCategoria", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var SubCategoria = {
        pscat_Id: idItem,
    };
    $.ajax({
        url: "//ProductoCategoria/GuardarSubCategoria",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ tbsubcategoria: SubCategoria }),
    });
});