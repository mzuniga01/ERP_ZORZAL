$(document).ready(function () {
    var Suc= $("#suc_Id").val();
    GetNumeroFact(Suc,4)
    $("#cja_Id").val(4);
})
    function GetNumeroFact(CodSucursal,CodCaja) {
        $.ajax({
            url: "/Factura/GetNumeroFact",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodSucursal: CodSucursal, CodCaja: CodCaja }),
        })
        .done(function (data) {
            if (data.length > 0) {
                var Mensaje = data;
                if (Mensaje == -1) {
                    alert("Fecha limite de Emisión Vencida")
                    var url = $("#RedirectTo").val();
                    location.href = url;
                } else if(Mensaje == -2) {
                    alert("El Numero CAI no tiene numeracion")
                    var url = $("#RedirectTo").val();
                   console.log("hola") 
                    location.href = url;
                } 
                else {
                    $('#pemi_NumeroCAI').val(data[0]['CAI']);
                    $('#fact_Codigo').val(data[0]['CODFACTURA']);
                }

            }
        });
    }