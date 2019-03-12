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
///validar not copy paste///
window.onload =
    function () {
        var myInput = document.getElementById('tdev_Descripcion');
        myInput.onpaste = function (e) {
            e.preventDefault();
            //alert("esta acción está prohibida");
        }

        myInput.oncopy = function (e) {
            e.preventDefault();
            //alert("esta acción está prohibida");
        }
    }