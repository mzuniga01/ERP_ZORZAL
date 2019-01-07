var contador = 0;

$('#AgregarSubCategoria').click(function () {
    var Descripcion = $('#pscat_Descripcion').val();
    var ISV = $('#pscat_ISV').val();

    if (Descripcion == '') 
    {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorISV').text('');
        $('#ErroDescripcion_Create').after('<ul id="ErrorDescripcion" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');

    }
    
    else if (ISV == '')
    {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorISV').text('');
        $('#ErrorISV_Create').after('<ul id="ErrorISV" class="validation-summary-errors text-danger">Campo ISV Requerido</ul>');
    }
    else 
    {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'Descripcion'>" + $('#pscat_Descripcion').val() + "</td>";
        copiar += "<td id = 'ISV'>" + $('#pscat_ISV').val() + "</td>";
        copiar += "<td>" + '<button id="removerSubCategoria" class="btn btn-danger btn-xs eliminar" type="button">-</button>' + "</td>";
        copiar += "</tr>";
        $('#SubCategoria').append(copiar);


        var SubCategoria = GetSubCategoria();

        $.ajax({
            url: "/ProductoCategoria/GuardarSubCategoria",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ tbsubcategoria: SubCategoria }),
        })
            .done(function (data) {
                $('#pscat_Descripcion').val('');
                $('#pscat_ISV').val('');

                $('#MessageError').text('');
                $('#ErrorDescripcion').text('');
                $('#ErrorISV').text('');
            });
    }
});



function GetSubCategoria() {
    var SubCategoria = {
        pscat_Descripcion: $('#pscat_Descripcion').val(),
        pscat_ISV: $('#pscat_ISV').val(),
        pscat_UsuarioCrea: contador,
        pscat_Id : contador,
        
    };
    return SubCategoria;
}




$(document).on("click", "#SubCategoria tbody tr td button#removerSubCategoria", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var borrar = {
        pscat_Id: idItem,
    };
    $.ajax({
        url: "/ProductoCategoria/removeSubCategoria",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ borrado: borrar }),
    });
});