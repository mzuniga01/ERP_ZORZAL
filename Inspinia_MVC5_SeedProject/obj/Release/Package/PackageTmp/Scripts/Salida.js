$(document).ready(function () {

    $('#fact_Id').val(0);
    $('#sal_RazonDevolucion').val('*****');

})



//Tipo de Salida
$("#tsal_Id").change(function () {
    var TipoSal = $("#tsal_Id").val()
    console.log(TipoSal);
    if (TipoSal == "1")
    {
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
})


function phonenumber(inputtxt) {

    var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;

    if (inputtxt.value.match(phoneno)) {

        return true;

    }

    else {

        alert("message");

        return false;

    }

}


$('#prod_Dsescripcion').mask('0000-0000');

window.addEventListener("load", function () {
    Miform.sal_Cantidad.addEventListener("keypress", soloNumeros, false);
});

//Solo permite introducir numeros.
function soloNumeros(e) {

    //var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;

    //if (e.value.match(phoneno)) {

    //    return true;

    //}

    //else {

    //    alert("message");

    //    return false;

    //}

    var key = window.event ? e.which : e.keyCode;
    if (key < 48 || key > 57) {
        e.preventDefault();
    }
}
