$(document).ready(function() {
    //QUE SOLO ACEPTE
    $("#pago_TotalPago")[0].maxLength = 12;
    //VALIDAR SOLO NUMEROS

    $(function () {
        $("#pago_TotalPago").keydown(function (event) {
            //alert(event.keyCode);
            if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) && event.keyCode !== 190 && event.keyCode !== 110 && event.keyCode !== 8 && event.keyCode !== 9) {
                return false;
            }
        });

    });

   
    $('#pago_TotalPago').on('input', function (e) {
        if (!/^[0-9]+$/.test(this.value)) {
            this.value = this.value.replace(/[^/^[0-9]+$/, "");
        }
    });

});