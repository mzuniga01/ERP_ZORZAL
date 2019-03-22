$(document).keydown(function (e) {
    if ((e.key == 'g' || e.key == 'G') && (e.ctrlKey || e.metaKey)) {
        e.preventDefault();
        //alert("Ctrl-g pressed");
        $("form").submit();
        return false;
    }
    return true;
});

//Focus
$("#ban_Nombre").focus();
$("#bcta_Numero").focus();
$("#pemi_NumeroCAI").focus();
$("#clte_Nombres").focus(); 
$("#clte_NombreComercial").focus();
$("#esfac_Descripcion").focus();
$("#mnda_Abreviatura").focus();
