////Get Municipio
$(document).on("change", "#listp_Prioridad", function () {
    ListaPrecioFechaFinal();
});

function ListaPrecioFechaFinal() {
    var FechaFinal = new Date($('#listp_FechaFinalVigencia').val());
    var Prioridad = $('#listp_Prioridad').val();

    console.log(FechaFinal)
    console.log(Prioridad)
    $.ajax({
        url: "/ListaPrecios/ListaPrecioFechaFinal",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Prioridad: Prioridad }),
    })
    .done(function (data) {
     $.each(data, function (key, val) {
        console.log(data)
      
       
        var fechaString = val.FECHAVIGENCIAFINAL.substr(6);
                var fechaActual = new Date(parseInt(fechaString));
                var mes = fechaActual.getMonth() + 1;
                var dia = fechaActual.getDate();
                var anio = fechaActual.getFullYear();
                var fechaPrioridad = dia + "/" +mes + "/" +anio;
                $('#FECHAVIGENCIAFINAL').val(fechaPrioridad);
                ms = Date.parse(FechaFinal);
                fecha1 = new Date(ms);
                console.log(fecha1)
         console.log(fechaPrioridad)

         if (FechaFinal <= fechaPrioridad)
        {
            alert("Ya Hay una Lista Precio Para Esta Fecha");
    }
     });
    });
}

//Fin