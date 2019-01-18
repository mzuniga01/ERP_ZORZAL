$(document).ready(function(){
    GetNumeroFact(1, 4)
    $("#suc_Id").val(1);
    $("#cja_Id").val(4);
})
    function GetNumeroFact(CodSucursal,CodCaja) {
        //var CodSucursal = $('#suc_Id').val();
        //var CodCaja = $('#cja_Id').val();   

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
                    alert("El Numero CAI no tiene numeracion")
                    var url = $("#RedirectTo").val();
                    location.href = url;
                }
                else {
                    $('#pemi_NumeroCAI').val(data[0]['CAI']);
                    $('#fact_Codigo').val(data[0]['CODFACTURA']);
                }
                //$.each(data, function (key, val) {
                   
                //});
                //console.log(data)
            }
            else {
            
            }
        });
    }