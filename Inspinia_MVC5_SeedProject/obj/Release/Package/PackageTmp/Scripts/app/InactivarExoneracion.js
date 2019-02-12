//Inactivar
$('#Inactivar').click(function () {
    var CodExoneracion = $('#exo_Id').val();
    var Activo = 0
    var RazonInactivo = null;
    $.ajax({
        url: "/Exoneracion/InactivarCliente",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodExoneracion: CodExoneracion, Activo: Activo }),

    })
  .done(function (data) {
      if (data.length > 0) {
          var url = $("#RedirectTo").val();
          location.href = url;
      }
      else {
          alert("Registro No Actualizado");
      }
  });
});



//Activar
$('#Activar').click(function () {
    var CodExoneracion = $('#exo_Id').val();
    var Activo = 1
    var RazonInactivo = null;
    $.ajax({
        url: "/Exoneracion/ActivarCliente",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodExoneracion: CodExoneracion, Activo: Activo}),

    })
    .done(function (data) {
        if (data.length > 0) {
            var url = $("#RedirectTo").val();
            location.href = url;
        }
        else {
            alert("Registro No Actualizado");
        }
    });
})