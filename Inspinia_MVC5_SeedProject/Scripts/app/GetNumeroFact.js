function GetNumeroFact() {
    var CodSucursal = $('#suc_Id').val();
    var CodCaja = $('#cja_Id').val();
    

    $.ajax({
        url: "/Factura/GetNumeroFact",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodSucursal: CodSucursal, CodCaja: CodCaja }),
    })
    .done(function (data) {
        if (data.length > 0) {
            $.each(data, function (key, val) {
                $('#pemi_NumeroCAI').val(val.pemi_NumeroCAI);
                $('#fact_Codigo').val(val.fact_Codigo);
            });
        }
        else {
            
        }
    });
}