$(suc_Telefono).on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});




$(document).ready(function () {
    $('#Sucursal').DataTable(
    {
        "searching": true,

        "oLanguage": {
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior",
            },
            "sEmptyTable": "No hay registros",
            "sInfoEmpty": "Mostrando 0 de 0 Entradas",
            "sSearch": "Buscar",
            "sLengthMenu": "Mostrar _MENU_ registros por página",
            "sInfo": "Mostrando _START_ a _END_ Entradas",
        }
    });
});


////Get Municipio
$(document).on("change", "#dep_Codigo", function () {
    GetMunicipios();
});

function GetMunicipios() {
    var CodDepartamento = $('#dep_Codigo').val();
    $.ajax({
        url: "/Sucursal/GetMunicipios",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodDepartamento: CodDepartamento }),
    })
    .done(function (data) {
        if (data.length > 0) {
            $('#mun_Codigo').empty();
            $('#mun_Codigo').append("<option value=''>Seleccione Municipio</option>");
            $.each(data, function (key, val) {
                $('#mun_Codigo').append("<option value=" + val.mun_Codigo + ">" + val.mun_Nombre + "</option>");
            });
            console.log(mun_Codigo)
            $('#mun_Codigo').trigger("chosen:updated");
        }
        else {
            $('#mun_Codigo').empty();
            $('#mun_Codigo').append("<option value=''>Seleccione Municipio</option>");
        }
    });
}

//Fin


    
$(document).ready(function () {

    var depto = $('#dep_Codigo').val();
    if (depto === '') {
        document.getElementById("mun_Codigo").disabled = true;
    }
    else {

    }
    var x = document.getElementById("mun_Codigo").disabled;
    if (x == true) {
        document.getElementById("mun_Codigo").title = "Seleccione primero un departamento";
    }
    else {
    }
});

$("#dep_Codigo").change(function () {


    var depto = $('#dep_Codigo').val();
    if (depto === '') {
        document.getElementById("mun_Codigo").disabled = true;
    }
    else {
        document.getElementById("mun_Codigo").disabled = false;
    }
    var x = document.getElementById("mun_Codigo").disabled;
    if (x == true) {

    }
    else {
        document.getElementById("mun_Codigo").title = "";
    }
});
