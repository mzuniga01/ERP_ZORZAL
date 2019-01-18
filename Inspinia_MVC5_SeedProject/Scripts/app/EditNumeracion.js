$(document).ready(function () {
    //Current Date
    var d = new Date();
    console.log(d);


    $("#PuntoEmision tbody tr").each(function () {
        //Id PuntoEmisionDetalle
        var ID = $(this).children("td:eq(0)").text();

        //FechaLimiteEmision
        var FechaLimiteEmsion = $(this).children("td:eq(6)").text();
        var i = new Date(FechaLimiteEmsion);
        console.log(i);


        if (i <= d) {
            $('#btnModalEditarEdit_' + ID).attr('disabled', true);

        }
        else {
            $('#btnModalEditarEdit_' + ID).attr('disabled', false);

        }



    });
});







