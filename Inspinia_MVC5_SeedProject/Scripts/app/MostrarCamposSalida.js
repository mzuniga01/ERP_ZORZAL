$("#tsal_Id").change(function () {

    var valor = this.value;
    if (valor == 1) {
        console.log(valor);
        document.getElementById("Prestamo").style.display = 'block';
        document.getElementById("Devolucion").style.display = 'none';
        document.getElementById("Venta").style.display = 'none';
        $('Devolucion').hide();
        $('Venta').hide();
    }
    else if (valor == 3) {
        console.log(valor);
        document.getElementById("Devolucion").style.display = 'block';
        document.getElementById("Prestamo").style.display = 'none';
        document.getElementById("Venta").style.display = 'none';
        $('Prestamo').hide();
        $('Venta').hide();
    }
    else {
        console.log(valor);
        document.getElementById("Venta").style.display = 'block';
        document.getElementById("Devolucion").style.display = 'none';
        document.getElementById("Prestamo").style.display = 'none';
        $('Devolucion').hide();
        $('Prestamo').hide();
    }
});
