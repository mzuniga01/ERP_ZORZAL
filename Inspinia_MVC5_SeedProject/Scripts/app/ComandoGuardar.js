$("input:visible:first").focus();
$("Select:visible:first").focus();
$(document).keydown(function (e) {
    //Guardar Registro
    if ((e.key == 'g' || e.key == 'G') && (e.ctrlKey || e.metaKey)) {
        e.preventDefault();
        $("form").submit();
        return false;
    }

    //Nuevo Registro
    if ((e.key == 'k' || e.key == 'K') && (e.ctrlKey || e.metaKey)) {
        e.preventDefault();
        var URLactual = window.location.href;
        var divisiones = URLactual.split("/", 5);
        var ultimo1 = divisiones[0] + "/" + divisiones[1] + "/" + divisiones[2] + "/" + divisiones[3]
        var ultimo = divisiones[4]
        if (ultimo == "Create") {

        }
        else {
            URLactual = ultimo1 + "/Create";
            console.log(URLactual)
            window.location = URLactual;
        }

    }
    //Cancelar Pantalla
    if ((e.key == 'l' || e.key == 'L') && (e.ctrlKey || e.metaKey)) {
        e.preventDefault();
        var URLactual = window.location.href;
        var divisiones = URLactual.split("/",5);
        var ultimo = divisiones[0] + "/" + divisiones[1] + "/" + divisiones[2] + "/" + divisiones[3]
        window.location = ultimo;
    }

    //Editar Registro
    if ((e.key == 'e' || e.key == 'E') && (e.ctrlKey || e.metaKey)) {
        e.preventDefault();
        var URLactual = window.location.href;
        var divisiones = URLactual.split("/", 6);
        var ultimo = divisiones[4]
        var ultimo1 = divisiones[0] + "/" + divisiones[1] + "/" + divisiones[2] + "/" + divisiones[3]
        var id = divisiones[5]
        if (ultimo == "Details") {
            URLactual = ultimo1 + "/Edit" + "/" + id;
            window.location = URLactual;
        }

    }


    return true;
});
