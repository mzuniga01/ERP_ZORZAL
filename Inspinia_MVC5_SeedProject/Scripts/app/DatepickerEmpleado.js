$(function () {
    //Factura
    $("#emp_FechaNacimiento").datepicker({
        dateFormat: 'dd-mm-yy',               
        changeMonth: true,
        monthNamesShort: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        changeYear: true,
        yearRange: '1980:2000',
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
    }).datepicker("setDate", new Date());
    
        //Factura
    $("#emp_FechaNacimientoEdit").datepicker({
        dateFormat: 'dd-mm-yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        changeYear: true,
        yearRange: '1980:2000',
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
    }).datepicker();
    
});

$(function () {
    //Factura
    $("#emp_FechaIngreso").datepicker({
        dateFormat: 'dd-mm-yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
    }).datepicker("setDate", new Date());

    $("#emp_FechaIngresoEdit").datepicker({
        dateFormat: 'dd-mm-yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
    }).datepicker();

});
$(function () {
    //Factura
    $("#emp_FechaDeSalida").datepicker({
        dateFormat: 'dd-mm-yy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá']
    }).datepicker("setDate", new Date());
});


//Validacion del correo 
$('#emp_Correoelectronico').change(function (e) {
    var emailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var EmailId = this.value;
    if (emailRegex.test(EmailId)) {
        $('#ErrorCorreo').text('');
        this.style.backgroundColor = "";
    }

    else {

        $('#ErrorCorreo').text('');
        $('#MessageForCorreo').after('<ul id="ErrorCorreo" class="validation-summary-errors text-danger">Correo Electronico Es Incorrecto </ul>');
        $("#emp_Correoelectronico").focus();
    }


});

//Validar Identificacion y telefono
$("#emp_Identificacion").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
$("#emp_Telefono").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});



//Validacion de solo letras
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}
////Validacion de la direccion 
function Direccion(e) {
    //$(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " 1234567890#áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}

////Limpiar campos de datos copiados y no permitidos por el campo de nombre
function limpiaNombre() {
    var val = document.getElementById("emp_Nombres").value;
    var tam = val.length;
    for (i = 0; i < tam; i++) {
        if (!isNaN(val[i]))
            document.getElementById("emp_Nombres").value = '';
    }  
    
}

function limpiaApellido() {
    var val = document.getElementById("emp_Apellido").value;
    var tam = val.length;
    for (i = 0; i < tam; i++) {
        if (!isNaN(val[i]))
            document.getElementById("emp_Apellido").value = '';
    }

}
//function limpiaIdentificacion() {
//    var val = document.getElementById("emp_Identificacion").value;
//    var tam = val.length;
//    for (i = 0; i < tam; i++) {
//        if (!isNaN(val[i]))
//            document.getElementById("emp_Identificacion").value = '';
//    }

//}
//function limpiaTelefono() {
//    var val = document.getElementById("emp_telefono").value;
//    var tam = val.length;
//    for (i = 0; i < tam; i++) {
//        if (!isNaN(val[i]))
//            document.getElementById("emp_telefono").value = '';
//    }

//}
function limpiaTipoSangre() {
    var val = document.getElementById("emp_TipoSangre").value;
    var tam = val.length;
    for (i = 0; i < tam; i++) {
        if (!isNaN(val[i]))
            document.getElementById("emp_TipoSangre").value = '';
    }

}
function limpiaPuesto() {
    var val = document.getElementById("emp_Puesto").value;
    var tam = val.length;
    for (i = 0; i < tam; i++) {
        if (!isNaN(val[i]))
            document.getElementById("emp_Puesto").value = '';
    }

}

///Validar tipo sangre
function TipoSangre(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " -+abcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}

//Validar Los campos numericos
function format(input) {
    var num = input.value.replace(/\,/g, '');
    if (!isNaN(num)) {
        input.value = num;
    }
    else {
        //alert('Solo se permiten numeros');
        input.value = input.value.replace(/[^\d\.]*/g, '');
    }
}
//fin

function EditStudentRecord(emp_Id) {
  
    
    $("#MsjError").text("");

    $.ajax({
        url: "/Empleado/GetEmpleado",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ emp_Id }),
    })
    .done(function (data) {
        $.each(data, function (m, Model) {
            $("#emp_Id_Edit").val(Model.emp_Id);
            $("emp_Estado_Edit").val(Model.emp_Estado);
            $("#emp_RazonInactivacion_Edit").val(Model.emp_RazonInactivacion);
            
            $("#MyModal").modal();
            
        })
    })
    .fail( function( jqXHR, textStatus, errorThrown ) {
        console.log('jqXHR', jqXHR);
        console.log('textStatus', textStatus);
        console.log('errorThrown', errorThrown);
    })
}
$('#Btnsubmit').click(function () {
    //var data = $("#SubmitForm").serializeArray();
    var emp_Id = $('#emp_Id_Edit').val();
    var emp_Estado = 0
    var emp_RazonInactivacion = $('#emp_RazonInactivacion_Edit').val();
    var emp_UsuarioModifica = $('#emp_UsuarioModifica_Edit').val();
    var emp_FechaModifica = $('#emp_FechaModifica_Edit').val();
    console.log(emp_Id)
    console.log(emp_Estado)
    console.log(emp_RazonInactivacion)
    console.log(emp_UsuarioModifica)
    if (emp_RazonInactivacion == "") {
        valido = document.getElementById('Mensaje');
        valido.innerText = "La razón inactivación es requerida";
    }
    else {
        $.ajax({
            url: "/Empleado/EstadoEmpleadoRazon",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ emp_Id: emp_Id, emp_Estado: emp_Estado, emp_RazonInactivacion: emp_RazonInactivacion, emp_UsuarioModifica: emp_UsuarioModifica, emp_FechaModifica: emp_FechaModifica }),

        })
    .done(function (data) {
        if (data.length > 0) {
            var url = $("#RedirectTo").val();
            location.href = url;
        }
        else {
            alert("Registro No Actualizado");
        }
    });
    }

})
//$("#Btnsubmit").click(function () {
//    var data = $("#SubmitForm").serializeArray();
   

//    $.ajax({
//        type: "Post",
//        url: "/Empleado/EstadoEmpleadoRazon",
//        data: data,
//        success: function (result) {
//            if (result == '-1')
//                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
//            else
//                $("#MyModal").modal("hide");
//            location.reload();
//        }
//    });
//})


function RazonSalida(emp_Id) {


    $("#MsjError").text("");

    $.ajax({
        url: "/Empleado/GetEmpleadoRazon",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ emp_Id }),
    })
    .done(function (data) {
        $.each(data, function (m, Model) {
            $("#emp_Id_Edit_Razon").val(Model.emp_Id);
            $("emp_Estado_Edit_Razon").val(Model.emp_Estado);
            $("#emp_RazonSalida_Edit_Razon").val(Model.emp_RazonSalida);

            $("#Editarmodales").modal();

        })
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        console.log('jqXHR', jqXHR);
        console.log('textStatus', textStatus);
        console.log('errorThrown', errorThrown);
    })
}

$('#BtnRazon').click(function () {
    //var data = $("#SubmitForm").serializeArray();
    var emp_Id = $('#emp_Id_Edit_Razon').val();
    var emp_Estado = 0
    var emp_RazonSalida = $('#emp_RazonSalida_Edit_Razon').val();
    var emp_UsuarioModifica = $('#emp_UsuarioModifica_Edit').val();
    console.log(emp_Id)
    console.log(emp_Estado)
    console.log(emp_RazonSalida)
    if (emp_RazonSalida == "") {
        valido = document.getElementById('ErrorMessage');
        valido.innerText = "La razón Salida es requerida";
    }
    else {
        $.ajax({
            url: "/Empleado/RazonSalida",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ emp_Id: emp_Id, emp_Estado: emp_Estado, emp_RazonSalida: emp_RazonSalida }),

        })
    .done(function (data) {
        if (data.length > 0) {
            var url = $("#RedirectTo").val();
            location.href = url;
        }
        else {
            alert("Registro No Actualizado");
        }
    });
    }

})

//$("#BtnRazon").click(function () {
//    var data = $("#DatosRazon").serializeArray();


//    $.ajax({
//        type: "Post",
//        url: "/Empleado/RazonSalida",
//        data: data,
//        success: function (result) {
//            if (result == '-1')
//                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
//            else
//                $("#Editarmodales").modal("hide");
//            location.reload();
//        }
//    });
//})