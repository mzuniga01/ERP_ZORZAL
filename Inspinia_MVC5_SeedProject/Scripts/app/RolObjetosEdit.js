$(document).ready(function () {
    $.ajax({
        url: "/Rol/GetObjetosDisponibles",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ rolId: $('#rol_Id').val() }),
    })
    .done(function (data) {
        if (data.length < 1) {
        }
        else {
            $.each(data, function (i, item) {

                newTr = '';
                newTr += '<tr data-id="' + item.obj_Id + '">'
                newTr += '<td id="objpantalla' + item.obj_Id + '">' + item.obj_Pantalla + '</td>'
                newTr += '<td><input name="id02" style="background-color:#1ab394" type="checkbox" id="check' + item.obj_Id + '" /></td>'
                newTr += '</tr>'
                $('#NoAsignadosEdit tbody').append(newTr)
            })
            $('#NoAsignadosEdit').DataTable({
                "searching": false,
                "scrollY": "300px",
                "scrollCollapse": true,
                "paging": false,
                "info": false,
                "oLanguage": {
                    "oPaginate": {
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior",
                    },
                    "sSearch": "Buscar",
                    "sLengthMenu": "Mostrar _MENU_ Registros Por Página",
                    "sInfo": "Mostrando _START_ a _END_ Entradas",

                },

            });
        }
    })
    $.ajax({
        url: "/Rol/GetObjetosAsignados",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ rolId: $('#rol_Id').val() }),
    })
    .done(function (data) {
        if (data.length < 1) {
            $('#AsignadosEdit').DataTable({
                "searching": false,
                "scrollY": "300px",
                "scrollCollapse": true,
                "paging": false,
                "info": false,
                "oLanguage": {
                    "oPaginate": {
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior",
                    },
                    "sSearch": "Buscar",
                    "sLengthMenu": "Mostrar _MENU_ Registros Por Página",
                    "sInfo": "Mostrando _START_ a _END_ Entradas"

                },

            });
            $('#AsignadosEdit> tbody > tr').each(function () {
                $(this).remove();
            })
        }
        else {
            $.each(data, function (i, item) {
                
                newTr = '';
                newTr += '<tr data-id="' + item.obj_Id + '">'
                newTr += '<td id="objpantalla' + item.obj_Id + '">' + item.obj_Pantalla + '</td>'
                newTr += '<td><input name="id02" style="background-color:#1ab394" type="checkbox" id="check' + item.obj_Id + '" /></td>'
                newTr += '</tr>'
                $('#AsignadosEdit tbody').append(newTr)
            })
            $('#AsignadosEdit').DataTable({
                "searching": false,
                "scrollY": "300px",
                "scrollCollapse": true,
                "paging": false,
                "info": false,
                "oLanguage": {
                    "oPaginate": {
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior",
                    },
                    "sSearch": "Buscar",
                    "sLengthMenu": "Mostrar _MENU_ Registros Por Página",
                    "sInfo": "Mostrando _START_ a _END_ Entradas"

                },

            });
        }
    })
    //$.extend($.fn.dataTableExt.oStdClasses, {
    //    'sPageEllipsis': 'paginate_ellipsis',
    //    'sPageNumber': 'paginate_number',
    //    'sPageNumbers': 'paginate_numbers'
    //});

    //$.fn.dataTableExt.oPagination.ellipses = {
    //    'oDefaults': {
    //        'iShowPages': 1
    //    },
    //    'fnClickHandler': function (e) {
    //        var fnCallbackDraw = e.data.fnCallbackDraw,
    //            oSettings = e.data.oSettings,
    //            sPage = e.data.sPage;

    //        if ($(this).is('[disabled]')) {
    //            return false;
    //        }

    //        oSettings.oApi._fnPageChange(oSettings, sPage);
    //        fnCallbackDraw(oSettings);

    //        return true;
    //    },
    //    // fnInit is called once for each instance of pager
    //    'fnInit': function (oSettings, nPager, fnCallbackDraw) {
    //        var oClasses = oSettings.oClasses,
    //            oLang = oSettings.oLanguage.oPaginate,
    //            that = this;

    //        var iShowPages = oSettings.oInit.iShowPages || this.oDefaults.iShowPages,
    //            iShowPagesHalf = Math.floor(iShowPages / 2);

    //        $.extend(oSettings, {
    //            _iShowPages: iShowPages,
    //            _iShowPagesHalf: iShowPagesHalf,
    //        });

    //        var oFirst = $('<a class="' + oClasses.sPageButton + ' ' + oClasses.sPageFirst + '">' + oLang.sFirst + '</a>'),
    //            oPrevious = $('<a class="' + oClasses.sPageButton + ' ' + oClasses.sPagePrevious + '">' + oLang.sPrevious + '</a>'),
    //            oNumbers = $('<span class="' + oClasses.sPageNumbers + '"></span>'),
    //            oNext = $('<a class="' + oClasses.sPageButton + ' ' + oClasses.sPageNext + '">' + oLang.sNext + '</a>'),
    //            oLast = $('<a class="' + oClasses.sPageButton + ' ' + oClasses.sPageLast + '">' + oLang.sLast + '</a>');

    //        oFirst.click({ 'fnCallbackDraw': fnCallbackDraw, 'oSettings': oSettings, 'sPage': 'first' }, that.fnClickHandler);
    //        oPrevious.click({ 'fnCallbackDraw': fnCallbackDraw, 'oSettings': oSettings, 'sPage': 'previous' }, that.fnClickHandler);
    //        oNext.click({ 'fnCallbackDraw': fnCallbackDraw, 'oSettings': oSettings, 'sPage': 'next' }, that.fnClickHandler);
    //        oLast.click({ 'fnCallbackDraw': fnCallbackDraw, 'oSettings': oSettings, 'sPage': 'last' }, that.fnClickHandler);

    //        // Draw
    //        $(nPager).append(oFirst, oPrevious, oNumbers, oNext, oLast);
    //    },
    //    // fnUpdate is only called once while table is rendered
    //    'fnUpdate': function (oSettings, fnCallbackDraw) {
    //        var oClasses = oSettings.oClasses,
    //            that = this;

    //        var tableWrapper = oSettings.nTableWrapper;

    //        // Update stateful properties
    //        this.fnUpdateState(oSettings);

    //        if (oSettings._iCurrentPage === 1) {
    //            $('.' + oClasses.sPageFirst, tableWrapper).attr('disabled', true);
    //            $('.' + oClasses.sPagePrevious, tableWrapper).attr('disabled', true);
    //        } else {
    //            $('.' + oClasses.sPageFirst, tableWrapper).removeAttr('disabled');
    //            $('.' + oClasses.sPagePrevious, tableWrapper).removeAttr('disabled');
    //        }

    //        if (oSettings._iTotalPages === 0 || oSettings._iCurrentPage === oSettings._iTotalPages) {
    //            $('.' + oClasses.sPageNext, tableWrapper).attr('disabled', true);
    //            $('.' + oClasses.sPageLast, tableWrapper).attr('disabled', true);
    //        } else {
    //            $('.' + oClasses.sPageNext, tableWrapper).removeAttr('disabled');
    //            $('.' + oClasses.sPageLast, tableWrapper).removeAttr('disabled');
    //        }

    //        var i, oNumber, oNumbers = $('.' + oClasses.sPageNumbers, tableWrapper);

    //        // Erase
    //        oNumbers.html('');

    //        for (i = oSettings._iFirstPage; i <= oSettings._iLastPage; i++) {
    //            oNumber = $('<a class="' + oClasses.sPageButton + ' ' + oClasses.sPageNumber + '">' + oSettings.fnFormatNumber(i) + '</a>');

    //            if (oSettings._iCurrentPage === i) {
    //                oNumber.attr('active', true).attr('disabled', true);
    //            } else {
    //                oNumber.click({ 'fnCallbackDraw': fnCallbackDraw, 'oSettings': oSettings, 'sPage': i - 1 }, that.fnClickHandler);
    //            }

    //            // Draw
    //            oNumbers.append(oNumber);
    //        }

    //        // Add ellipses
    //        if (1 < oSettings._iFirstPage) {
    //            oNumbers.prepend('<span class="' + oClasses.sPageEllipsis + '">...</span>');
    //        }

    //        if (oSettings._iLastPage < oSettings._iTotalPages) {
    //            oNumbers.append('<span class="' + oClasses.sPageEllipsis + '">...</span>');
    //        }
    //    },
    //    // fnUpdateState used to be part of fnUpdate
    //    // The reason for moving is so we can access current state info before fnUpdate is called
    //    'fnUpdateState': function (oSettings) {
    //        var iCurrentPage = Math.ceil((oSettings._iDisplayStart + 1) / oSettings._iDisplayLength),
    //            iTotalPages = Math.ceil(oSettings.fnRecordsDisplay() / oSettings._iDisplayLength),
    //            iFirstPage = iCurrentPage - oSettings._iShowPagesHalf,
    //            iLastPage = iCurrentPage + oSettings._iShowPagesHalf;

    //        if (iTotalPages < oSettings._iShowPages) {
    //            iFirstPage = 1;
    //            iLastPage = iTotalPages;
    //        } else if (iFirstPage < 1) {
    //            iFirstPage = 1;
    //            iLastPage = oSettings._iShowPages;
    //        } else if (iLastPage > iTotalPages) {
    //            iFirstPage = (iTotalPages - oSettings._iShowPages) + 1;
    //            iLastPage = iTotalPages;
    //        }

    //        $.extend(oSettings, {
    //            _iCurrentPage: iCurrentPage,
    //            _iTotalPages: iTotalPages,
    //            _iFirstPage: iFirstPage,
    //            _iLastPage: iLastPage
    //        });
    //    }
    //};

});
$('#Add').click(function () {
        var idRol = $('#rol_Id').val()
        var RolAcceso = []
        $('#NoAsignadosEdit> tbody > tr').each(function () {
            idItem = $(this).data('id');
            var objpantalla;
            if ($('#check' + idItem).is(':checked')) {
                active = $(this)
                var Asignados = $('#AsignadosEdit').length
                $('#NoAsignadosEdit tbody').append(active)
                $('#check' + idItem).prop('checked', false);
                $(this).remove();
                $('#AsignadosEdit tbody').append(active)
                //var idItem = $(this).attr('data-id')

                var item = {
                    obj_Id: idItem,
                }
                RolAcceso.push(item)

                //    })
                
            }
        })
        $.ajax({
            url: "/Rol/AgregarObjeto",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ idRol: idRol, RolAcceso: RolAcceso }),
            success: function (json) {
                //Recargar();
            },
            error: function () {
                $('#validationDescripcionRol').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se pudo añadir la pantalla, contacte con el administrador</ul>');
            }
        })

                    .done(function (data) {
                        if (data == '') {
                            console.log("Guardado Exito");
                            var TableLegth = $("#NoAsignadosEdit tr").length;
                            //if (TableLegth > 1) {
                            //    $("#NoAsignadosEdit tbody").empty();
                            //};
                            }
                        else {
                            console.log("Guardado Fallido");
                        }
                    });
})
//function Recargar() {
//    var idRol1 = $('#rol_Id').val()

//    $.ajax({
//        url: "/Rol/GetObjetosDisponibles",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ rolId1: idRol1 }),
//    })
//    .done(function (data) {
//        if (data.length < 1) {
//        }
//        else {
//            $.each(data, function (i, item) {
//                newTr = '';
//                newTr += '<tr data-id="' + item.obj_Id + '">'
//                newTr += '<td id="objpantalla' + item.obj_Id + '">' + item.obj_Pantalla + '</td>'
//                newTr += '<td><input name="id02" style="background-color:#1ab394" type="checkbox" id="check' + item.obj_Id + '" /></td>'
//                newTr += '</tr>'
//                $('#NoAsignadosEdit tbody').append(newTr)
//            })
//            $('#NoAsignadosEdit').dataTable({
//                "destroy": true,
//                "searching": false,
//                "scrollY": "300px",
//                "scrollCollapse": true,
//                "paging": false,
//                "info": false,
//                "oLanguage": {
//                    "oPaginate": {
//                        "sNext": "Siguiente",
//                        "sPrevious": "Anterior",
//                    },
//                    "sSearch": "Buscar",
//                    "sLengthMenu": "Mostrar _MENU_ Registros Por Página",
//                    "sInfo": "Mostrando _START_ a _END_ Entradas"

//                },
//            })
//        }
//    })
//}
    $('#Remove').click(function () {
        var idRol = $('#rol_Id').val()
        var RolAcceso = []
        $('#AsignadosEdit> tbody > tr').each(function () {
            idItem = $(this).data('id');
            var objpantalla;

            if ($('#check' + idItem).is(':checked')) {
                active = $(this)
                $('#check' + idItem).prop('checked', false);
                $(this).remove();
                $('#NoAsignadosEdit tbody').append(active)
                var item = {
                    obj_Id: idItem,
                }
                RolAcceso.push(item)
            }
        })
        $.ajax({
            url: "/Rol/QuitarObjeto",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ idRol: idRol, RolAcceso: RolAcceso }),
        })
                    .done(function (data) {
                        if (data == '') {
                            console.log("Quitar Exito");
                            //var TableLegth = $("#AsignadosEdit tr").length;
                            //if (TableLegth > 1) {
                            //    $("#AsignadosEdit tbody").empty();
                            //};
                            //$.ajax({
                            //    url: "/Rol/GetObjetosAsignados",
                            //    method: "POST",
                            //    dataType: 'json',
                            //    contentType: "application/json; charset=utf-8",
                            //    data: JSON.stringify({ rolId: idRol }),
                            //})

                            //.done(function (data) {
                            //    if (data.length < 1) {
                            //    }
                            //    else {
                            //        $.each(data, function (i, item) {
                            //            newTr = '';
                            //            newTr += '<tr data-id="' + item.obj_Id + '">'
                            //            newTr += '<td id="objpantalla' + item.obj_Id + '">' + item.obj_Pantalla + '</td>'
                            //            newTr += '<td><input name="id02" style="background-color:#1ab394" type="checkbox" id="check' + item.obj_Id + '" /></td>'
                            //            newTr += '</tr>'
                            //            $('#AsignadosEdit tbody').append(newTr)
                            //        })
                            //        $('#AsignadosEdit').dataTable({
                            //            "destroy": true,
                            //            "searching": false,
                            //            "oLanguage": {
                            //                "oPaginate": {
                            //                    "sNext": "Siguiente",
                            //                    "sPrevious": "Anterior",
                            //                },
                            //                "sSearch": "Buscar",
                            //                "sLengthMenu": "Mostrar _MENU_ Registros Por Página",
                            //                "sInfo": "Mostrando _START_ a _END_ Entradas"

                            //            },
                            //        })
                            //    }

                            //})
                        }
                        else {
                            console.log("Quitar Fallido");
                        }
                    })
        var TableLeght = $("#NoAsignadosEdit tr").length;
        
        

    })
    $('#btnActualizarRol').click(function () {
        var rolId = $("#rol_Id").val();
        var Descripcion = $("#rol_Descripcion").val();
        if (Descripcion == '') {
            $('#DescripcionRol').text('');
            $('#errorDescripcionRol').text('');
            $('#validationDescripcionRol').after('<ul id="errorDescripcionRol" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');
        }
        else {
            $.ajax({
                url: "/Rol/UpdateRol",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ rolId: rolId, Descripcion: Descripcion }),
            })
                        .done(function (data) {
                            if (data == '') {
                                $('#validationDescripcionRol').after('<ul id="ErrorValidacionGeneral" class="validation-summary-errors text-danger">No se pudo actualizar el registro, contacte con el administrador</ul>');
                            }
                            else {
                                window.location.href = '/Rol/Index';
                            }
                        })
        }
    });