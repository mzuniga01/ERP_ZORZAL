
//Validación Sólo letras
function validar(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-z\s]*$/i.test(tecla);
   }