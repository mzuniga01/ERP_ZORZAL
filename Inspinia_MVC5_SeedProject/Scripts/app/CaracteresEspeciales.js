﻿//Validación letras y numeros
function format(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-z0-9]*$/i.test(tecla);
}


//Validación Sólo letras
function validar(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-záéíóúñ\s]*$/i.test(tecla);
}

function Copy(string) {//solo letras y numeros
    var out = '';
    //Se añaden las letras validas
    var filtro = 'abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZáéíóúÁÉÍÓÚ ,.';
    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) != -1)
            out += string.charAt(i);

    return out;
}

//Validar letras y números
function validarJ(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-z0-9áéíóúñ._-\s]*$/i.test(tecla);
}

//Correo
function correo(e) {
    campo = event.target;
    $(campo).on('input', function (e) {
        if (!/^[a-z0-9áéíóúñ@._-\s]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ a-z0-9áéíóúñ@._-\s]+/ig, "");
        }
    });
    $(campo).blur(function () {
        var reg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        var regOficial = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;
        //Se muestra un texto a modo de ejemplo, luego va a ser un icono
        if (reg.test(campo.value) && regOficial.test(campo.value)) {
            $(this).next("p").text("");
        } else if (reg.test(campo.value)) {
            valido.innerText = "";

        } else {
            $(this).next("p").text("Dirección de Correo Electrónico Incorrecta");
            return false
        }

    })    
}

//TextArea
function validarI(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9a-záéíóúñ.#_-\s]*$/i.test(tecla);
}

//Validar Teléfono
function validartel(e) {
    campo = event.target;
    $(campo).on("input", function (event) {
        var Telefono = this.value.match(/[0-9\s]+/);
        
        if (Telefono != null) {
            this.value = '+' + ((Telefono).toString().replace(/[^ 0-9a-záéíóúñ@._-\s]\d +/ig, ""));
        }
        else {
            this.value = null;           
        }  
    });
}


///Validacion para no pegar caracteres
 function Caracteres(string) {
    var out = '';
    //Se añaden las letras validas
    var filtro = '1234567890-+ ';
    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) != -1)
            out += string.charAt(i);

    return out;
}