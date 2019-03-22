var normalize = (function () {
    var from = "ÃÀÁÄÂÈÉËÊÌÍÏÎÒÓÖÔÙÚÜÛãàáäâèéëêìíïîòóöôùúüûÑñÇç",
        to = "AAAAAEEEEIIIIOOOOUUUUaaaaaeeeeiiiioooouuuunncc",
        mapping = {};

    for (var i = 0, j = from.length; i < j; i++)
        mapping[from.charAt(i)] = to.charAt(i);

    return function (str) {
        var ret = [];
        for (var i = 0, j = str.length; i < j; i++) {
            var c = str.charAt(i);
            if (mapping.hasOwnProperty(str.charAt(i)))
                ret.push(mapping[c]);
            else
                ret.push(c);
        }
        return ret.join('');
    }

})();

$("#tent_Id").click(function () {
    var valorid1 = document.getElementById('tent_Id').value;
    var valoridt = $(this).find('option:selected').text();
    var valorid = normalize(valoridt.toUpperCase());

    if (valorid == 0) {
        console.log('hola 0');

    } else if (valorid == 'COMPRA') {
        document.getElementById("compra").style.display = 'block';
        document.getElementById("Devolucion").style.display = 'none';
        document.getElementById("traslado").style.display = 'none';
    } else if (valorid == "DEVOLUCION") {
        document.getElementById("Devolucion").style.display = 'block';
        document.getElementById("compra").style.display = 'none';
        document.getElementById("traslado").style.display = 'none';
    }
    else if (valorid == "TRASLADO") {
        document.getElementById("traslado").style.display = 'block';
        document.getElementById("Devolucion").style.display = 'none';
        document.getElementById("compra").style.display = 'none';
    }
    else {
        document.getElementById("traslado").style.display = 'none';
        document.getElementById("Devolucion").style.display = 'none';
        document.getElementById("compra").style.display = 'none';
    }
});