$(document).ready(function () {
    GetCaja();
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




    function GetCaja() {
        var CodUsuario = $("#usu_Id").val();
        $.ajax({
            url: "/Factura/GetCaja",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodUsuario: CodUsuario }),
        })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (key, val) {
                    $("#cja_Id").val(val.cja_Id);
                    $("#cja_Descripcion").val(val.cja_Descripcion);

                    var Suc = $("#suc_Id").val();
                    GetNumeroFact(Suc, val.cja_Id)

                });
            }
            else {
                $('#alertaCaja').show();
            }
        });
    }