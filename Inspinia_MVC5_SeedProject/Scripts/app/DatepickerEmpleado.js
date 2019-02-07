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

//Validar telefono 
$("#emp_Identificacion").on("keypress keyup blur", function (event) {
    //this.value = this.value.replace(/[^0-9\.]/g,'');
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
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

$("#Btnsubmit").click(function () {
    var data = $("#SubmitForm").serializeArray();
   

    $.ajax({
        type: "Post",
        url: "/Empleado/EstadoEmpleadoRazon",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
            else
                $("#MyModal").modal("hide");
            location.reload();
        }
    });
})


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
$("#BtnRazon").click(function () {
    var data = $("#DatosRazon").serializeArray();


    $.ajax({
        type: "Post",
        url: "/Empleado/RazonSalida",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
            else
                $("#Editarmodales").modal("hide");
            location.reload();
        }
    });
})