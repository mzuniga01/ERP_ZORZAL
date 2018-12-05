$(function () {
    $("#FechaPago").datepicker({
        dateFormat: 'dd-mm-yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
    }).datepicker("setDate", new Date());

    $("#fechaarqueo").datepicker({
        dateFormat: 'yy-mm-dd'
        //,showOn: "both",
        //buttonText: "<i class='fas fa-calendar-alt'></i>"
    });

    $("#fechasarqueo").datepicker({
        dateFormat: 'yy-mm-dd'
        //,showOn: "both",
        //buttonText: "<i class='fas fa-calendar-alt'></i>"
    });

    $("#fechainicio").datepicker({
        dateFormat: 'yy-mm-dd'
        //,showOn: "both",
        //buttonText: "<i class='fas fa-calendar-alt'></i>"
    });

    $("#fechafin").datepicker({
        dateFormat: 'yy-mm-dd'
        //,showOn: "both",
        //buttonText: "<i class='fas fa-calendar-alt'></i>"
    });

    $("#fechafactura").datepicker({
        dateFormat: 'dd-mm-yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
    }).datepicker("setDate", new Date());

    $("#fechanacimiento").datepicker({
        dateFormat: 'dd-mm-yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
    }).datepicker("setDate", new Date());

    $("#fechaconstitucion").datepicker({
        dateFormat: 'dd-mm-yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
    }).datepicker("setDate", new Date());

    $("#fechaapertura").datepicker({
        dateFormat: 'dd-mm-yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
        //,showOn: "both",
        //buttonText: "<i class='fas fa-calendar-alt'></i>"
    }).datepicker("setDate", new Date());
});
