function InactivarCliente() {
    var CodCliente = $('#clte_Id').val();
    var Activo = 0
    var RazonInactivo = $('#razonInac').val();
    $.ajax({
        url: "/Cliente/InactivarCliente",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodCliente: CodCliente, Activo: Activo, RazonInactivo: RazonInactivo }),

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
}
function ActivarCliente() {
    var CodCliente = $('#clte_Id').val();
    var Activo = 1
    var RazonInactivo = null;
    $.ajax({
        url: "/Cliente/ActivarCliente",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodCliente: CodCliente, Activo: Activo, RazonInactivo: RazonInactivo }),

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
}