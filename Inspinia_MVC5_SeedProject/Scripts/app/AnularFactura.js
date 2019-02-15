$(document).ready(function () {
    var Estado = $('#fact_EsAnulada').val(true);
    document.getElementById("esfac_Id").disabled = true;
    if (Estado == true) {
        $('#bottonAnular').hide();
        document.getElementById("esfac_Id").disabled = true;
        document.getElementById("fechafacturaEdit").disabled = true;
        document.getElementById("clte_Identificacion").disabled = true;
        document.getElementById("fact_AlCredito").disabled = true;
        document.getElementById("clte_Nombres").disabled = true;
        document.getElementById("fact_AutorizarDescuento").disabled = true;
        document.getElementById("btnSave").disabled = true;
        document.getElementById("AddProducto").disabled = true;
   }
});


$('#Anular').click(function () {
    var CodFactura = $('#fact_Id').val();
    var FacturaAnulado = 1
    var RazonAnulado = $('#razonAnular').val();
    if (RazonAnulado == "") {
        valido = document.getElementById('Mensaje');
        valido.innerText = "La razón Anular es requerida";
    }else
    {
        $.ajax({
            url: "/Factura/AnularFactura",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodFactura: CodFactura, FacturaAnulado: FacturaAnulado, RazonAnulado: RazonAnulado }),

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
    
})

function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}


