function soloLetras(e) {
    key=e.keyCode || e.which;
 
    teclado=String.fromCharCode(key).toLowerCase();
 
    letras="qwertyuiopasdfghjklñzxcvbnm ";
 
    especiales="8-37-38-46-164";
 
    teclado_especial=false;
 
    for(var i in especiales){
        if(key==especiales[i]){
            teclado_especial=true;
            break;
        }
    }
 
    if(letras.indexOf(teclado)==-1 && !teclado_especial){
        return false;
    }
}



//guardar 
$(document).keydown(function (e) {
    if ((e.key == 'g' || e.key == 'G') && (e.ctrlKey || e.metaKey)) {
        e.preventDefault();
        $("form").submit();
        return false;
    }
    return true;

});