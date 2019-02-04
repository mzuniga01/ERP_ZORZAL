$(document).ready(function () {

    $('#fact_Id').val(0);
    $('#sal_RazonDevolucion').val('*****');

})

function TipodeSalida() {
    var TipoSal = $("#tsal_Id").val()
    console.log(TipoSal);
    if (TipoSal == "1") {
        $('#fact_Id').val(0);
        $('#sal_RazonDevolucion').val('*****');

        $("#Prestamo").css("display", "block");

        $("#Venta").css("display", "none");
        $("#Devolucion").css("display", "none");

    }
    else {
        $("#Prestamo").css("display", "none");
        $("#Devolucion").css("display", "none");
        if (TipoSal == 2) {
            $('#fact_Id').val('');
            $('#sal_RazonDevolucion').val('*****');


            $("#Venta").css("display", "block");

        }
        else {
            $("#Venta").css("display", "none");
            $("#Prestamo").css("display", "none");

            if (TipoSal == "3") {
                $('#fact_Id').val(0);
                $('#sal_RazonDevolucion').val('');

                $("#Devolucion").css("display", "block");
            }
            else {
                $("#Devolucion").css("display", "none");

                $("#Venta").css("display", "none");
                $("#Prestamo").css("display", "none");

            }

        }

    }
}

$(document).ready( function () {
    var e = document.getElementById("bod_Id");
    var strUser = e.options[e.selectedIndex].text;
    console.log(strUser)
    $("#tbBodega_bod_Nombre").val(strUser)

});

$(document).ready(function () {
    var e = document.getElementById("estm_Id");
    var strUser = e.options[e.selectedIndex].text;
    console.log(strUser)
    $("#tbEstadoMovimiento_estm_Descripcion").val(strUser)

});

$(document).ready(function () {
    var e = document.getElementById("fact_Id");
    var strUser = e.options[e.selectedIndex].text;
    console.log(strUser)
    $("#tbFactura_fact_Codigo").val(strUser)

});
$(document).ready(function () {
    var e = document.getElementById("tsal_Id");
    var strUser = e.options[e.selectedIndex].text;
    console.log(strUser)
    $("#tbTipoSalida_tsal_Id").val(strUser)

});
//Tipo de Salida

$(document).ready(function () {
   TipodeSalida()
});
$("#tsal_Id").change(function () {
    TipodeSalida()
});

//$(document).on("change", "#dep_Codigo", function () {
//    GetMunicipios();
//});



//function phonenumber(inputtxt) {

//    var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;

//    if (inputtxt.value.match(phoneno)) {

//        return true;

//    }

//    else {

//        alert("message");

//        return false;

//    }

//}


//$('#prod_Dsescripcion').mask('0000-0000');

//window.addEventListener("load", function () {
//    Miform.sal_Cantidad.addEventListener("keypress", soloNumeros, false);
//});

//Solo permite introducir numeros.
//function soloNumeros(e) {

//    //var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;

//    //if (e.value.match(phoneno)) {

//    //    return true;

//    //}

//    //else {

//    //    alert("message");

//    //    return false;

//    //}

//    var key = window.event ? e.which : e.keyCode;
//    if (key < 48 || key > 57) {
//        e.preventDefault();
//    }
//}


//Anular
function GetAnularSalida() {
    var Salida = {
        sal_Id: $('#sal_Id').val(),
        sal_RazonAnulada: $('#sal_RazonAnulada').val(),
        sald_UsuarioCrea: contador
    };
    return Salida;
}


$('#btnAnularSalida').click(function () {
    var sal_Id = $('#sal_Id').val();
    var sal_RazonAnulada = $('#sal_RazonAnulada').val();
    
    if (sal_RazonAnulada == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationCodigoCreate').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Descripcion Requerido</ul>');
    }

    else {
        var tbSalida = GetAnularSalida();
        $.ajax({
            url: "/Salida/Anular",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Salida: tbSalida }),
        })
        .done(function (data) {
            if (data == 'El registro se guardo exitosamente') {
                location.reload();
                swal("El registro se almacenó exitosamente!", "", "success");
            }
            else {
                location.reload();
                swal("El registro  no se almacenó!", "", "error");
            }
        });

    }

});