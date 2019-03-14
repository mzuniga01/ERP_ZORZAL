//////////////////////////......PANTALLA__CREATE..............////////////////////////

//Validar Los campos Stringg
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[A-ZÑ ]+$/.test(tecla);

};
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
};
////Limpiar campo de campos copiados y no permitidos por el campo de nombre
function limpia() {
    var val = document.getElementById("tsal_Descripcion").value;
    var tam = val.length;
    for (i = 0; i < tam; i++) {
        if (!isNaN(val[i]))
            document.getElementById("tsal_Descripcion").value = '';
    }
};
///validar not copy paste///
window.onload =
    function () {
        var myInput = document.getElementById('tsal_Descripcion');
        myInput.onpaste = function (e) {
            e.preventDefault();
            //alert("esta acción está prohibida");
        }

        myInput.oncopy = function (e) {
            e.preventDefault();
            //alert("esta acción está prohibida");
        }
    };
//////////////////////////////////////////////////////////////////////////////////////////////////////////////

//////////////////////////......PANTALLA__EDIT..............////////////////////////

//Validar Los campos Stringg
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[A-ZÑ ]+$/.test(tecla);
};
///validar not copy paste///
window.onload =
    function () {
        var myInput = document.getElementById('tsal_Descripcion');
        myInput.onpaste = function (e) {
            e.preventDefault();
        }

        myInput.oncopy = function (e) {
            e.preventDefault();
        }
    };
