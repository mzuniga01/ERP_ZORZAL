$(function () {
    //Factura
    $("#boxFechaCrea").datepicker({
        dateFormat: 'dd-mm-yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
    }).datepicker("setDate", new Date());

});
