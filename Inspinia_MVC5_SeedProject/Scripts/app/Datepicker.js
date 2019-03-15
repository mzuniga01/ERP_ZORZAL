$(function () {
    //Cliente
    $("#clte_FechaNacimiento").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        minDate: '-100Y',
        maxDate: '-18Y',
        prevText: 'Ant',
        nextText: 'Sig',
        changeMonth: true,
        changeYear: true,
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
    }).datepicker('setDate', new Date());

    $("#fechaNacimientoEdit").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        minDate: '-100Y',
        maxDate: '-18Y',
        prevText: 'Ant',
        nextText: 'Sig',
        changeMonth: true,
        changeYear: true,
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
    }).datepicker();
    

    var FechaInicio1 = new Date();
    $("#clte_FechaConstitucion").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        minDate: '-100Y',
        maxDate: FechaInicio1,
        prevText: 'Ant',
        nextText: 'Sig',
        changeMonth: true,
        changeYear: true,
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
    }).datepicker('setDate', new Date());

    $("#fechaConstitucionEdit").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        minDate: '-100Y',
        maxDate: '-18Y',
        prevText: 'Ant',
        nextText: 'Sig',
        changeMonth: true,
        changeYear: true,
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
    }).datepicker();

  
    //DevolucionFactura
    $("#fechaDevolucion").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig',
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
    }).datepicker('setDate', new Date()).datepicker("destroy");


    ///////////////////////////Factura     
    $("#fechafactura").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig',
        changeMonth: true,
        changeYear: true,
    }).datepicker('setDate', new Date()).datepicker("destroy");

    $("#fechafacturaEdit").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig',
        changeMonth: true,
        changeYear: true,
    }).datepicker("destroy");

    $("#fact_FechaNacimientoTE").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig',
        minDate: '-100Y',
        maxDate: '-61Y',
        changeMonth: true,
        changeYear: true,
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
    }).datepicker();

        //Lista Precio
    $("#listp_FechaInicioVigencia").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        changeMonth: true,
        changeYear: true,
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date()).datepicker("destroy");


    $("#FIV").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date()).datepicker("destroy");

    $("#FFV").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date());

    $("#FIVEdit").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    });

    $("#FFVEdit").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    });

    $("#listp_FechaFinalVigencia").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        changeMonth: true,
        changeYear: true,
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date()).datepicker("destroy");

    var FechaInicio = new Date();
    $("#pemid_FechaLimite").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        minDate: FechaInicio,
        maxDate: '+3Y',
        prevText: 'Ant',
        nextText: 'Sig',
        changeMonth: true,
        changeYear: true,
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
    }).datepicker('setDate', new Date());


    //Apertura
    $('#mocja_FechaApertura').datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date());

    //Fecha de realizacion de pago
    $("#FechaPago").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig',
        changeMonth: true,
        changeYear: true
    }).datepicker('setDate', new Date()).datepicker("destroy");





    //Arqueo
    $("#fechaapertura").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date());

    $("#fechaarqueo").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date());

    $("#fechaaceptacion").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date());
 //-------------------------------------------------------------//

    $("#fechainicio").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date());

    $("#fechafin").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date());

    

    $("#bcta_FechaApertura").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date());

    //Filtros
    $("#FechaDesde").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date());

    $("#FechaHasta").datepicker({
        dateFormat: 'mm/dd/yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        prevText: 'Ant',
        nextText: 'Sig'
    }).datepicker('setDate', new Date());

});


