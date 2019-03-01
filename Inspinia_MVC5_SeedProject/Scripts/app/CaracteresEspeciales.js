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
            //valido.innerText = "Direccion de Correo Electronico Incorrecta";
            $(this).next("p").text("Direccion de Correo Electronico Incorrecta");
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
        //$(this).val('+');
        var Telefono = $(this).val();
        if (!/^[0-9\s+]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ 0-9\s]/ig, "");
        }
        
        console.log(Telefono)
        if (Telefono == ' ') {
         $(this).val('+');
        }
        else if (Telefono == '') {
         $(this).val('+');
        }
    });

}
