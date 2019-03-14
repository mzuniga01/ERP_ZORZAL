//////////////////////////......PANTALLA__CREATE..............////////////////////////
//Validar Los campos Stringg
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[A-ZÑ ]+$/.test(tecla);
}
////Limpiar campo de campos copiados y no permitidos por el campo de nombre
function limpia() {
    var val = document.getElementById("uni_Descripcion").value;
    var tam = val.length;
    for (i = 0; i < tam; i++) {
        if (!isNaN(val[i]))
            document.getElementById("uni_Descripcion").value = '';
    }
}
///validar not copy paste uni_Descripcion///
window.onload =
    function () {
        var myInput = document.getElementById('uni_Descripcion');
        myInput.onpaste = function (e) {
            e.preventDefault();
        }

        myInput.oncopy = function (e) {
            e.preventDefault();
        }
    };
//////////////////////////////////////////////////////////////////////////////////////////////////////////////

//////////////////////////......PANTALLA__EDIT..............////////////////////////
//Validar Los campos Stringg
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[A-ZÑ ]+$/.test(tecla);
}
///validar not copy paste///
window.onload =
    function () {
        var myInput = document.getElementById('uni_Descripcion');
        myInput.onpaste = function (e) {
            e.preventDefault();
        }

        myInput.oncopy = function (e) {
            e.preventDefault();
        }
    }
