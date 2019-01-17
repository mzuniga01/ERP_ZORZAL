
//Tipo de Salida
$("#tsal_Id").change(function () {
    var TipoSal = $("#tsal_Id").val()
    console.log(TipoSal);
    if (TipoSal == "1")
    {
        $("#Prestamo").css("display", "block");

        $("#Venta").css("display", "none");
        $("#Devolucion").css("display", "none");

    }
    else {
        $("#Prestamo").css("display", "none");
        $("#Devolucion").css("display", "none");
        if (TipoSal == 2) {
            $("#Venta").css("display", "block");
        }
        else {
            $("#Venta").css("display", "none");
            $("#Prestamo").css("display", "none");

            if (TipoSal == "3") {
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
