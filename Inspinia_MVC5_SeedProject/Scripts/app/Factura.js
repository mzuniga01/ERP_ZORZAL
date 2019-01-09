$(document).ready(function () {
    $('#DataTable1').DataTable(
    {
        "searching": false,

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


$("#consumidorFinal").change(function () {
   if (this.checked) {
        //Do stuff
       $('#DatosCliente').hide();
       $("#clte_Identificacion").val('****');
       $("#clte_Nombres").val('****');
       $("#clte_Id").val(1);
        
    }
    else {
       $('#DatosCliente').show();
       $("#clte_Nombres").val('');
       $("#clte_Identificacion").val('');
    }
})



//$().change(function () {
//    var Identificacion = $("#tpi_Id").val()
//    console.log(Identificacion);
//    valido = document.getElementById('label_identificacion');
//    document.getElementById('label_identificacion').innerHTML = Identificacion;
//})
//$(document).ready(function () {
//    var Identificacion = $("#tpi_Id").val()
//    console.log(Identificacion);
//    valido = document.getElementById('label_identificacion');
//    document.getElementById('label_identificacion').innerHTML = Identificacion;;

//});

$("#consumidorFinal").ready(function () {
    if (this.checked) {
        //Do stuff
        $('#DatosCliente').hide();
        $("#clte_Identificacion").val('****');
        $("#clte_Nombres").val('****');
        $("#clte_Id").val(0);
  }
    else {
        $('#DatosCliente').show();
        $("#clte_Nombres").val('');
        $("#clte_Identificacion").val('');
    }
});
