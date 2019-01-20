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
        if (idItem != '') {
            document.getElementById("Factura").disabled = false;
            document.getElementById("tbFactura_fact_Codigo").disabled = false;
            GetIDCliente(idItem);
        }
    });
});

///Consumidor Final-------------------------------------------------------------------------------------------
$("#consumidorFinal").change(function () {
    if (this.checked) {
        //Do stuff     
        console.log("Cheked");
         $("#tbFactura_clte_Identificacion").val('99999999999999');
         idItem = ('99999999999999')
        $("#tbFactura_clte_Nombres").val('Consumidor Final');
        document.getElementById("btnCliente").disabled = true;
        document.getElementById("Factura").disabled = false;
        console.log("valor",idItem)
        GetIDCliente(idItem);
    }
    else {
        $("#tbFactura_clte_Identificacion").val('');
        $("#tbFactura_clte_Nombres").val('');
        document.getElementById("btnCliente").disabled = false;
    }

})


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
                //var myDate = "/Date(1547704800000)/";
                //var jsDate = new Date(parseInt(myDate.replace(/\D/g, '')))

                //var date = new Date(parseInt(val.FactFecha.substr(6)));
               
                val.FactFecha = new Date(parseInt(val.FactFecha.replace("/Date(", "").replace(")/", ""), 10));
                //$(val.FactFecha).val(moment().format('MMM D, YYYY'));
                copiar = "<tr data-id=" + contador + " data-codigo=" + val.FactCodigo + " data-idfact=" + val.FactId + ">";
                copiar += "<td id = 'codigo'>" + val.FactCodigo + "</td>";
                copiar += "<td id = 'b'>" + val.FactFecha + "</td>";
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


