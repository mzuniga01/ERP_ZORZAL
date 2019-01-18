//Busqueda de Cliente en Devolucion-----------
$(document).ready(function () {
    var $rows = $('#BodyCliente tr');
    $("#searchCliente").keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
        if (val.length >= 3) {
            $rows.show().filter(function () {
                var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                return !~text.indexOf(val);
            }).hide();
        }
        else if (val.length >= 1) {
            $rows.show().filter(function () {
            }).hide();
        }

    })
});


//Devolucion Seleccionar Cliente
$(document).on("click", "#ClienteModal tbody tr td button#AgregarCliente", function () {
    idItem = $(this).closest('tr').data('id');
    descItem = $(this).closest('tr').data('desc');
    $("#tbFactura_clte_Identificacion").val(idItem);
    $("#tbFactura_clte_Nombres").val(descItem);
    $('#ModalAgregarCliente').modal('hide');
    $(document).ready(function () {
    if (descItem != '') {
        document.getElementById("Factura").disabled = false;
        document.getElementById("tbFactura_fact_Codigo").disabled = false;
        GetIDCliente(idItem);
    }
    });
});

//Filtro de Modal Factura----------------------------------------------------------------------------

var CodCliente = $('#tbFactura_clte_Identificacion').val();
console.log(CodCliente)
function GetIDCliente(CodCliente, idItem) {
    $.ajax({
        url: "/Devolucion/FiltrarModal",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodCliente: CodCliente }),

        error: function () {
            console.log("si entrafiltrar1");
            alert("No se puede filtrar");
        },
        success: function (list) {
            $('#BodyFactura').empty();
            $.each(list, function (key, val) {
                contador = contador + 1;
                var myDate = "/Date(1547704800000)/";
                var jsDate = new Date(parseInt(myDate.replace(/\D/g, '')))

                //var date = new Date(parseInt(val.FactFecha.substr(6)));
                //val.FactFecha = new Date(parseInt(val.FactFecha.replace("/Date(", "").replace(")/", ""), 10));
                copiar = "<tr data-id=" + contador + " data-codigo=" + val.FactCodigo + " data-idfact=" + val.FactId + ">";
                copiar += "<td id = 'codigo'>" + val.FactCodigo + "</td>";
                copiar += "<td id = 'b'>" + val.jsDate + "</td>";
                copiar += "<td id = 'data-DescItem'>" + val.CtleRTN + "</td>";
                copiar += "<td id = 'ClienteItem'>" + val.Nombre + "</td>";
                copiar += "<td>" + '<button id="AgregarFactura" class="btn btn-primary btn-xs" type="button">Añadir</button>' + "</td>";
                copiar += "</tr>";
                $('#BodyFactura').append(copiar);
            });
            console.log(list);
        }

         
    });
  
}



