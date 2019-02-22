
//Validación Sólo letras
function validar(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-záéíóúñ\s]*$/i.test(tecla);
}

//Validar letras y números
function validarJ(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-z0-9áéíóúñ\s]*$/i.test(tecla);
}

//Correo
function correo(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-záéíóúñ@._-\s]*$/i.test(tecla);
}

//TextArea
function validarI(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9a-záéíóúñ.#_-\s]*$/i.test(tecla);
}
