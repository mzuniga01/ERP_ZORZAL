$(document).ready(function () {
        //Current Date
        var d = new Date();

    $("#PuntoEmision tbody tr").each(function () {
        
        var ID = $(this).children("td:eq(0)").text();

        //FechaLimiteEmision
        var FechaLimiteEmsion = $(this).children("td:eq(4)").text();
        var i = new Date(FechaLimiteEmsion);

        if (i <= d) {
          $('#btnModalEditarEdit_'+ID).attr('disabled', true);
        }
        else {
           $('#btnModalEditarEdit_'+ID).attr('disabled', false);
        }

    });
});






