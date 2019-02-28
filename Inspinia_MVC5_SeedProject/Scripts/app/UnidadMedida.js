//////////////////////////......PANTALLA__CREATE..............////////////////////////
//Validar Los campos Stringg
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZÑ ]+$/.test(tecla);
}
////Limpiar campo de campos copiados y no permitidos por el campo de nombre
function limpia() {
    var val = document.getElementById("miInput").value;
    var tam = val.length;
    for (i = 0; i < tam; i++) {
        if (!isNaN(val[i]))
            document.getElementById("miInput").value = '';
    }
}
///validar not copy paste uni_Descripcion///
window.onload =
    function () {
        var myInput = document.getElementById('miInput');
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
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}
///validar not copy paste///
window.onload =
    function () {
        var myInput = document.getElementById('uni_Descripcion');
        var myInput = document.getElementById('uni_Abreviatura');
        myInput.onpaste = function (e) {
            e.preventDefault();
            //alert("esta acción está prohibida");
        }

        myInput.oncopy = function (e) {
            e.preventDefault();
            //alert("esta acción está prohibida");
        }
    }
//////////////////////-----CREATE---------////////////////////
$("#miInput").change(function () {
    var str = $("#miInput").val();
    var res = str.toUpperCase();
    $("#miInput").val(res);
});
/////////////////////////////////////////////////////////////

//////////////////////-----EDIT---------////////////////////
$("#uni_Descripcion").change(function () {
    var str = $("#uni_Descripcion").val();
    var res = str.toUpperCase();
    $("#uni_Descripcion").val(res);
});
/////////////////////////////////////////////////////////////